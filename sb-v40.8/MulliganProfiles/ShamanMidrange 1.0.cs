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
    public class ShamanJadePiratesNoFinley : MulliganProfile
    {
        
       
        private string _log = "\r\n---ShamanJadePiratesNoFinley Mulligan---";
        private List<Card.Cards> _choices; 
        private List<Card.Cards> _keep = new List<Card.Cards>();  //Defined list of cards we will keep. This is what needs to be filled in and returned in HandleMulligan

        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
        {
            _choices = choices; //Define our choices
            bool coin = _choices.Count == 4; // Defines the coin.
            if (coin)
            {
                //You no longer need to save the coin, but it's good practice. GAME_005 is a coin.
                _choices.Remove(Card.Cards.GAME_005);
                _keep.Add(Card.Cards.GAME_005);
            }

			/// Cards we always keep, vs all classes:

			Keep("-> Always keep", Cards.TotemGolem, Cards.TotemGolem); // Always keep Totem Golems

			
			/// START of mulligans vs specific classes:
			
            switch (opponentClass)
            {
                case Card.CClass.SHAMAN:
                    
					Keep("-> keep vs Shaman", Cards.LightningStorm);
					
					// vs Shaman, keep Jade Claws:
			            
					if (_choices.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.JadeClaws))
					{
						Keep("-> vs Shaman keep Jade Claws", Cards.JadeClaws);
					}
						
					// vs Shaman keep Spirit Claws if we don't have Jade Claws:
							
					if (!_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws))
					{
						Keep("-> vs Shaman keep Spirit Claws", Cards.SpiritClaws);
					}
							
                    break;
					
                case Card.CClass.WARRIOR:
                   
					Keep("-> keep vs Warrior", Cards.MaelstromPortal);
					
					// vs Warrior, keep Jade Claws:
							
					if (_choices.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.JadeClaws))
					{
						Keep("-> vs Warrior keep Jade Claws", Cards.JadeClaws);
					}
						
					// vs Warrior keep Spirit Claws if we don't have Jade Claws:
							
					if (!_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws))
					{
						Keep("-> vs Warrior keep Spirit Claws", Cards.SpiritClaws);
					}
									
					// vs Warrior keep Lightning Storm if we don't have a weapon or Maelstrom Portal:
					
					if (!_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws) && !_keep.Contains(Cards.MaelstromPortal))
					{
						Keep("-> vs Warrior if we don't have weapon keep Maelstrom Portal", Cards.LightningStorm);
					}					
					
					// vs Warrior if we have the coin, keep combo Tunnel Trogg + Lightning Bolt:
					
					if (coin && (_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && _choices.Contains(Cards.LightningBolt))
					{
						Keep("-> vs Warrior keep Lightning Bolt with Tunnel Trogg", Cards.TunnelTrogg, Cards.LightningBolt);
					}					
					
                    break;
					
                case Card.CClass.ROGUE:

					// Keep("-> keep vs Rogue", Cards.Hex); // Keep Hex vs Miracle Rogue if you encouter many of those

					// vs Rogue keep Jade Claws to kill those pirates:
							
					if (_choices.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.JadeClaws))
					{
						Keep("-> vs Rogue keep Jade Claws", Cards.JadeClaws);
					}
						
					// vs Rogue keep Spirit Claws if we don't have Jade Claws:
							
					if (!_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws))
					{
						Keep("-> vs Rogue keep Spirit Claws", Cards.SpiritClaws);
					}
						
					// vs Rogue keep Maelstrom Portal if we don't have Jade Claws or Spirit Claws:
				
					if (!_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws))
					{
						Keep("-> vs Rogue if we don't have weapon keep Maelstrom Portal", Cards.MaelstromPortal);
					}					
				
					// vs Rogue with coin keep Lightning Storm if we don't have Jade Claws or Spirit Claws or Maelstrom Portal:
				
					if (coin && !_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws) && !_keep.Contains(Cards.MaelstromPortal))
					{
						Keep("-> vs Rogue with coin if we don't have weapon or Maelstrom Portal we keep Lightning Storm", Cards.LightningStorm);
					}		
				
					break;
					
                case Card.CClass.DRUID:
				
                    Keep("-> keep vs Druid", Cards.MaelstromPortal, Cards.MaelstromPortal, Cards.LightningStorm, Cards.LightningStorm); // Keep this vs Egg Druid (lots of those currently in Wild)
                    
					break;
					
                case Card.CClass.WARLOCK:
				
                    Keep("-> keep vs Warlock", Cards.Hex);
					
                    break;
            }

			/// END of mulligans vs specific classes			
            
			/// START of mulligan rules vs all classes:
			
			// With coin, keep Feral Spirit + Tunnel Trogg:
            			
			if (coin && (_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)))
			{
				Keep("-> With coin keep Feral Spirit with Tunnel Trogg", Cards.FeralSpirit);
			}
            
			if (coin && (_choices.Contains(Cards.FeralSpirit) || _keep.Contains(Cards.FeralSpirit)))
			{
				Keep("-> With coin keep Tunnel Trogg with Feral Spirit", Cards.TunnelTrogg);
			}
			

            // If we have Tunnel Trogg + Feral Spirit, we'll only have 1 mana on turn 3. Therefore we also keep, in order of preference, Lightning Bolt, Spirit Claws and/or a second Tunnel Trogg:
						
			if (_keep.Contains(Cards.TunnelTrogg) && _keep.Contains(Cards.FeralSpirit) && _choices.Contains(Cards.LightningBolt))
            {
                Keep("-> With Tunnel Trogg + Feral Spirit keep Lightning Bolt for turn 3", Cards.LightningBolt);
            }
            
			else
            
				if (_keep.Contains(Cards.TunnelTrogg) && _keep.Contains(Cards.FeralSpirit) && _choices.Contains(Cards.SpiritClaws) && !_keep.Contains(Cards.SpiritClaws))
				{
					Keep("-> with Tunnel Trogg + Feral Spirit keep Spirit Claws for turn 3", Cards.SpiritClaws);
				}              
				
				else
				
					if (_keep.Contains(Cards.TunnelTrogg) && _keep.Contains(Cards.FeralSpirit) && _choices.Contains(Cards.TunnelTrogg))
					{
						Keep("-> with Tunnel Trogg + Feral Spirit keep 2nd Tunnel Trogg for turn 3", Cards.TunnelTrogg); // checked: no need to mention 2x TunnelTrogg here
					}
				

			// With coin, keep 2x Tunnel Trogg + Totem Golem (also if we already have Tunnel Trogg + Feral Spirit):                    
						
			if (coin && (_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && (_choices.Contains(Cards.TotemGolem) || _keep.Contains(Cards.TotemGolem)))
            {
                Keep("-> Perfect! Coin + 2x combo Tunnel Trogg + Totem Golem (now give us a Flametongue Totem, please!)", Cards.TunnelTrogg, Cards.TunnelTrogg, Cards.TotemGolem); 
            }
			
			
            // Keep Tunnel Trogg + Totem Golem (also if we already have Tunnel Trogg + Feral Spirit with coin):                    
			
            if ((_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && (_choices.Contains(Cards.TotemGolem) || _keep.Contains(Cards.TotemGolem)))
            {
                Keep("-> Keep combo Tunnel Trogg + Totem Golem)", Cards.TunnelTrogg, Cards.TotemGolem, Cards.TotemGolem); 
            }

			
            // Keep Tunnel Trogg + Jade Claws (also if we already have Tunnel Trogg + Feral Spirit with coin, or Tunnel Trogg + Totem Golem):                    
			
            if ((_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && (_choices.Contains(Cards.JadeClaws) || _keep.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep Tunnel Trogg + Jade Claws", Cards.TunnelTrogg); // Keep Tunnel Trogg
            }

			if ((_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && (_choices.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep Tunnel Trogg + Jade Claws", Cards.JadeClaws); // Keep Jade Claws if not already kept
            }
			
			
			// With coin, keep 2x Tunnel Trogg + Jade Claws:                    
			
            if (coin && (_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && (_choices.Contains(Cards.JadeClaws) && _keep.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep Tunnel Trogg + Jade Claws", Cards.TunnelTrogg, Cards.TunnelTrogg); // Keep Tunnel Trogg
            }

			if (coin && (_choices.Contains(Cards.TunnelTrogg) || _keep.Contains(Cards.TunnelTrogg)) && (_choices.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep Tunnel Trogg + Jade Claws", Cards.JadeClaws); // Keep Jade Claws if not already kept
            }
			
			
			// Keep Small-Time Buccaneer (2 if possible, with or without coin) + Jade Claws:
						
            if (_choices.Contains(Cards.SmallTimeBuccaneer) && (_choices.Contains(Cards.JadeClaws) || _keep.Contains(Cards.JadeClaws)))
            {
                Keep("-> Keep Small-Time Buccaneer with Jade Claws", Cards.SmallTimeBuccaneer, Cards.SmallTimeBuccaneer); // Keep the Buccaneer if we have or can pick Jade Claws
            }

			if (_keep.Contains(Cards.SmallTimeBuccaneer) && !_keep.Contains(Cards.JadeClaws))
            {
                Keep("-> Keep Jade Claws with Small-Time Buccaneer", Cards.JadeClaws); // Keep Jade Claws if we have Buccaneer (and if we don't already have a Jade Claws)
            }		

			
            // Keep Small-Time Buccaneer (2 if possible) + Spirit Claws, unless we already have Jade Claws:
			
            if (_choices.Contains(Cards.SmallTimeBuccaneer) && (_choices.Contains(Cards.SpiritClaws) || _keep.Contains(Cards.SpiritClaws)) && !_keep.Contains(Cards.JadeClaws))
            {
                Keep("-> Keep Small-Time Buccaneer with Spirit Claws", Cards.SmallTimeBuccaneer, Cards.SmallTimeBuccaneer); // Keep all Small-Time Buccaneers
            }

			if (_keep.Contains(Cards.SmallTimeBuccaneer) && _choices.Contains(Cards.SpiritClaws) && !_keep.Contains(Cards.SpiritClaws) && !_keep.Contains(Cards.JadeClaws))
            {
                Keep("-> Keep Spirit Claws with Small-Time Buccaneer", Cards.SpiritClaws); // Keep Spirit Claws
            }
			

            // If we have Small-Time Buccaneer + Spirit Claws, we have 1 mana left on turn 2 (or with coin we can play 2 1-drops), so we also keep a Lighting Bolt and/or Tunnel Trogg:
						
            if (_keep.Contains(Cards.SmallTimeBuccaneer) && _keep.Contains(Cards.SpiritClaws))
            {
                Keep("-> Keep with Small-Time Buccaneer + Spirit Claws", Cards.LightningBolt, Cards.TunnelTrogg);
            }

			
			// With coin, we keep the combo 2x Tunnel Trogg + Flametongue Totem:
			
			if (coin && _choices.ContainsAll(Cards.TunnelTrogg, Cards.TunnelTrogg) && _choices.Contains(Cards.FlametongueTotem))
            {
                Keep("-> With coin keep 2x Tunnel Trogg + Flametongue Totem", Cards.TunnelTrogg, Cards.TunnelTrogg, Cards.FlametongueTotem);
            }
			
			
			// If we haven't kept Small-Time Buccaneer or Tunnel Trogg, we don't have a combo. In that case we keep a Tunnel Trogg and/or Small-Time Buccaneer:
						
            if (!_keep.Contains(Cards.SmallTimeBuccaneer) && !_keep.Contains(Cards.TunnelTrogg)) 
            {
                Keep("-> Keep if no combo", Cards.SmallTimeBuccaneer, Cards.TunnelTrogg); // Without coin, keep Small-Time Buccaneer and Tunnel Trogg (53% chance to have a good combo on turn 2 if we have both).
            }
			
			if (coin && !_keep.Contains(Cards.SmallTimeBuccaneer) && !_keep.Contains(Cards.TunnelTrogg) && _choices.Contains(Cards.TunnelTrogg) && _choices.Contains(Cards.SmallTimeBuccaneer)) 
            {
                Keep("-> Keep if no combo", Cards.SmallTimeBuccaneer); // With coin, keep Small-Time Buccaneer but not Tunnel Trogg (with Buccaneer we have 58% chance of having a good combo on turn 2, and only 55% chance if we keep the Trogg).
            }


			// Always keep Spirit Claws, unless we already have Jade Claws and not vs Warlock (Renolock)
			
			if (!_keep.Contains(Cards.JadeClaws) && !_keep.Contains(Cards.SpiritClaws) && opponentClass != Card.CClass.WARLOCK)
			{
				Keep("-> Keep without Jade Claws", Cards.SpiritClaws); // Keep Spirit Claws if we don't have Jade Claws, except vs Warlock (Renolock)
			}
			
			
			// If we have Spirit Claws, keep Bloodmage Thalnos:
			
			if (_keep.Contains(Cards.SpiritClaws) && !_keep.Contains(Cards.JadeClaws))
			{
				Keep("-> Keep Bloodmage Thalnos with Spirit Claws", Cards.BloodmageThalnos); // Keep Thalnos with Spirit Claws
			}

						
			// If we have a 1-drop (Small-Time Buccaneer or Tunnel Trogg) we always keep Flametongue Totem, except vs (Pirate) Warrior:
			
			if ((_keep.Contains(Cards.SmallTimeBuccaneer) || _keep.Contains(Cards.TunnelTrogg)) && !_keep.Contains(Cards.FlametongueTotem) && opponentClass != Card.CClass.WARRIOR)
			{
				Keep("-> We have 1-drop, so we keep", Cards.FlametongueTotem);
			}
			
						
			/// END of mulligan rules vs all classes
						
			/// DO NOT CHANGE ANYTHING BELOW (except for the name of the mulligan profile in 4th line from the bottom)
			
			
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
            str += " because " + reason;
            AddLog(str);
        }

        private void AddLog(string s)
        {
            _log += "\r\n" + s;
        }

        private void PrintLog()
        {
            Bot.Log(_log);
            _log = "\r\n---ShamanJadePiratesNoFinley Mulligan---";
        }
    }
}