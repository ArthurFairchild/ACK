using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBot.Mulligan
{
    public static class Extension2
    {
        public static bool ContainsAll<T1>(this IList<T1> list, params T1[] items)
        {
            return !items.Except(list).Any();
        }
    }
    [Serializable]
    public class ShamanControl : MulliganProfile
    {
        /// <summary>
        /// Predifined List of good Anti Reno Lock removal cards
        /// 
        /// Anchor 1
        /// </summary>
        private readonly List<Card.Cards> _goodWarlockRemoval = new List<Card.Cards>
        {
            Cards.Hex,
            Cards.LightningBolt,
            Cards.BloodmageThalnos //I assume it's just as an activator here for jade claws. 
        };
        /// <summary>
        /// Anti Reno Lock cards
        /// 
        /// Anchor 2
        /// </summary>
        private readonly List<Card.Cards> _goodWarlockMinions = new List<Card.Cards>
        {
            Cards.Barnes,
            Cards.WhiteEyes,
            Cards.EarthElemental//ew
        };

        private string _log = "\r\n---ShamanControl Mulligan---";
        private List<Card.Cards> _choices; //Masterwai clones choices to use in his private methods. 
        private List<Card.Cards> _keep = new List<Card.Cards>();  //Defined list of cards we will keep. This is what needs to be filled in and returned in HandleMulligan

        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
        {
            List<Card.Cards> _myDeck;
            try //Entering code segment that I expect to throw exception at some point. (Mulligan Tester will fail this block)
            {
                //SmartBot will always execute this line of code. 
                _myDeck = Bot.CurrentDeck().Cards.Select(card => (Card.Cards)Enum.Parse(typeof(Card.Cards), card)).ToList();
                /*Notes
                 * If you remove try/catch statements, the line above will throw an error in mulligan tester. 
                 * This is just to preserve the mulligan flow
                 * when you are debugging your code.
                 */
            }
            catch (Exception)//Mulligan Tester is unable to call Bot.CurrentDeck().Cards, so line below is used to save mulligan flow 

            {
                /*White the bot plays, it will never reach this line of code, it is completely unreachable while your bot runs
                 * This is your safety net while using mulligan tester because mulligan tester Will read this line of code, 
                 * but it will not read the 
                 * one above */
                _myDeck = new List<Card.Cards>{Cards.SirFinleyMrrgglton, /*fill the rest, doesn't need to be 30. But as far 
                    as mulligan tester is concerned you can even put 1000 cards in here. You will only change these cards 
                    if your logic depends on something unique inside of your deck Such as: 
                    different logic if SirFinley is present, 
                    different logic if Elemental Distruction is Present. */};
                /*Additionally, you should not add any fancy logic here*/
            }
            //=======================Divider=======================
            //You can now use advanced logic based on what cards are in your deck
            //All that logic must be written down below
            // ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅ ˅
            _choices = choices; //Define our choices
            bool coin = _choices.Count == 4;
            if (coin)
            {
                //You no longer need to save the coin, but it's good practice. GAME_005 is a coin.
                _choices.Remove(Card.Cards.GAME_005);
                _keep.Add(Card.Cards.GAME_005);
            }


            Keep("-> keep vs all", Cards.LightningBolt, Cards.BloodmageThalnos, Cards.HauntedCreeper, Cards.FeralSpirit);

            switch (opponentClass)
            {
                case Card.CClass.SHAMAN:
                    Keep("-> keep vs shaman", Cards.Hex, Cards.LightningStorm, Cards.MaelstromPortal);
                    break;
                case Card.CClass.WARRIOR:
                    Keep("-> keep vs warrior", Cards.HealingWave, Cards.MaelstromPortal, Cards.LightningStorm);
                    break;
                case Card.CClass.MAGE:
                    Keep("-> keep vs mage", Cards.Barnes, Cards.WhiteEyes, Cards.EarthElemental);
                    break;
                case Card.CClass.ROGUE:
                    Keep("-> keep vs rouge", Cards.Barnes, Cards.WhiteEyes, Cards.MaelstromPortal);
                    break;
                case Card.CClass.DRUID:
                    Keep("-> keep vs druid", Cards.MaelstromPortal, Cards.LightningStorm, Cards.Barnes, Cards.WhiteEyes);
                    break;
                case Card.CClass.PRIEST:
                    Keep("-> keep vs priest", Cards.Barnes, Cards.WhiteEyes, Cards.LightningStorm);
                    if (!_keep.Contains(Cards.FeralSpirit) && !_keep.Contains(Cards.Barnes) && !_keep.Contains(Cards.LightningStorm))
                    {
                        Keep("-> bad hand vs priest", Cards.ElementalDestruction);
                    }
                    break;
                case Card.CClass.WARLOCK:
                    Keep("-> keep vs warlock", Cards.Hex, Cards.Barnes, Cards.WhiteEyes, Cards.EarthElemental);
                    if ((_keep.Contains(Cards.FeralSpirit) && (_keep.Exists(_goodWarlockMinions.Contains) || _keep.Exists(_goodWarlockRemoval.Contains))) || (_keep.Exists(_goodWarlockMinions.Contains) && _keep.Exists(_goodWarlockRemoval.Contains)))
                    {
                        Keep("-> good hand vs warlock", Cards.EmperorThaurissan);
                    }
                    break;
            }
            // With coin, keep Tunnel Trogg + Feral Spirit:
            {
                if (_choices.Contains(Cards.TunnelTrogg) && _choices.Contains(Cards.FeralSpirit) && coin)
                {
                    Keep("-> With coin keep TTrogg + FeralSpirit", Cards.TunnelTrogg, Cards.TunnelTrogg, Cards.FeralSpirit);
                }
            }

            // If we have Tunnel Trogg + Feral Spirit, we'll only have 1 mana on turn 3. Therefore we also keep a second Tunnel Trogg, or, preferably, Lightning Bolt:

            if (_keep.Contains(Cards.TunnelTrogg) && _keep.Contains(Cards.FeralSpirit))
            {
                Keep("-> Keep LightningBolt with TTrogg+FeralSpirit for turn 3", Cards.LightningBolt);
            }
            else
            {
                if (_keep.Contains(Cards.TunnelTrogg) && _keep.Contains(Cards.FeralSpirit))
                {
                    Keep("-> Keep 2nd TTrog with TTrogg+FeralSpirit for turn 3", Cards.TunnelTrogg);



                }
            }

            // Keep Jade Claws + ST Buccaneer:

            if (_choices.ContainsAll(Cards.SmallTimeBuccaneer, Cards.JadeClaws))
            {
                Keep("-> Keep Buccaneer with JadeClaws", Cards.SmallTimeBuccaneer, Cards.JadeClaws);
            }


            // Keep Tunnel Trogg + Totem Golem (also if we already have Tunnel Trogg + Feral Spirit with coin):                    

            if ((_choices.ContainsAll(Cards.TunnelTrogg, Cards.TotemGolem)) || (_keep.Contains(Cards.TunnelTrogg) && _choices.Contains(Cards.TotemGolem)))
            {
                Keep("-> Keep TTrogg + TotemGolem", Cards.TunnelTrogg, Cards.TotemGolem); // Will this give an error if TunnelTrogg is already in _keep?
            }


            // Keep Tunnel Trogg + Jade Claws (also if we already have Tunnel Trogg + Feral Spirit with coin, or Tunnel Trogg + Totem Golem):                    

            if ((_choices.ContainsAll(Cards.TunnelTrogg, Cards.JadeClaws)) || (_keep.Contains(Cards.TunnelTrogg) && _choices.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep TTrogg + JadeClaws", Cards.TunnelTrogg, Cards.TotemGolem); // Again, will this give an error if TunnelTrogg is already in _keep?
            }


            // If we have ST Buccaneer + Spirit Claws, we keep it, unless we already have Jade Claws, because in that case we already have Buccaneer+JadeClaws or TTrogg+JadeClaws:

            if ((_choices.ContainsAll(Cards.SmallTimeBuccaneer, Cards.SpiritClaws)) && (!_keep.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep Buccaneer with SpiritClaws", Cards.SmallTimeBuccaneer, Cards.SpiritClaws);
            }


            // If we have ST Buccaneer + Spirit Claws, we have 1 mana left on turn 2, so we also keep, in order of preference, a 2nd Buccaneer, Lighting Bolt or Tunnel Trogg:

            if (_keep.ContainsAll(Cards.SmallTimeBuccaneer, Cards.SpiritClaws))
            {
                Keep("-> Keep 2nd Buccaneer with Buccaneer+SpiritClaws", Cards.SmallTimeBuccaneer);
            }

            if (_keep.ContainsAll(Cards.SmallTimeBuccaneer, Cards.SpiritClaws))
            {
                Keep("-> Keep LightningBolt with Buccaneer+SpiritClaws", Cards.LightningBolt);
            }

            else
            {
                if (_keep.ContainsAll(Cards.SmallTimeBuccaneer, Cards.SpiritClaws)) //if this is a common check, make a boolean
                {
                    Keep("-> Keep TTrogg with Buccaneer+SpiritClaws", Cards.TunnelTrogg);
                }
            }
            /* This is a comment explaining extended version of code below
                bool has1drop = false;
                foreach (var c in _keep)
                {
                    if (CardTemplate.LoadFromId(c).Cost == 1 && CardTemplate.LoadFromId(c).Type == Card.CType.MINION)
                         {
                          has1drop = true;
                          break;
                         }
                }             
             */
             //Linq version of code above
            bool has1drop = _keep.Any(c => CardTemplate.LoadFromId(c).Cost == 1 && CardTemplate.LoadFromId(c).Type == Card.CType.MINION);
            if(has1drop && opponentClass != Card.CClass.WARRIOR)
                Keep("-> Keeping Flametongue", Cards.FlametongueTotem);

            //===========================My Deck setup ==================
           
            if (_myDeck.Contains(Cards.SirFinleyMrrgglton))
            {
                //Some Logic
            }


            if(_keep.Count(cards => cards == Card.Cards.AT_002) > 1)
            PrintLog();
            return _keep;
        }
        /// <summary>
        /// Method to fill in his _keep list
        /// </summary>
        /// <param name="reason">Why?</param>
        /// <param name="cards">List of cards he wants to add</param>
        private void Keep(string reason, params Card.Cards[] cards)
        {
            var count = true;
            string str = "Keep: ";
            foreach (var card in cards)
            {
                if (_choices.Contains(card))
                {
                    str += CardTemplate.LoadFromId(card).Name + ",";
                    _choices.Remove(card);
                    _keep.Add(card);
                    count = false;
                }
            }
            if (count) return;
            str = str.Remove(str.Length - 1);
            str += " Because " + reason;
            AddLog(str);
        }

        private void AddLog(string s)
        {
            _log += "\r\n" + s;
        }

        private void PrintLog()
        {
            Bot.Log(_log);
            _log = "\r\n---ShamanControl Mulligan---";
        }
    }
}