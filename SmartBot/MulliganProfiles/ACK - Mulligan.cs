using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using ACK;
using SmartBot.Database;
using SmartBot.Mulligan;
using SmartBot.Plugins;
using SmartBot.Plugins.API;

//Version ~3.01



namespace MulliganProfiles
{
    public static class MulliganExtension
    {
        public static List<Card.Cards> ToCards(this List<string> list)
        {
            return list.Select(c => CardTemplate.LoadFromId(c).Id).ToList();
        }
    }

    [Serializable]
    // ReSharper disable once InconsistentNaming
    public class Mulligan : MulliganProfile
    {
        public Card.Cards Nothing = Card.Cards.GAME_005;
        public HeroClass Mage = HeroClass.MAGE;
        public HeroClass Priest = HeroClass.PRIEST;
        public HeroClass Shaman = HeroClass.SHAMAN;
        public HeroClass Warlock = HeroClass.WARLOCK;
        public HeroClass Warrior = HeroClass.WARRIOR;
        public HeroClass Paladin = HeroClass.PALADIN;
        public HeroClass Rogue = HeroClass.ROGUE;
        public HeroClass Hunter = HeroClass.HUNTER;
        public HeroClass Druid = HeroClass.DRUID;


        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
        {
            MulliganContainer mc;
            DeckClassification dc;
            try
            {
                mc = new MulliganContainer(Bot.CurrentMode().ToString(),
                    choices.Select(c => c.ToString()).ToList(), opponentClass.ToString(), ownClass.ToString(),
                    Bot.CurrentDeck().Cards);
                dc = new DeckClassification(Bot.CurrentDeck().Cards);
                mc.LogData = false; //No need to debug log when bot is succesfully running
                mc.Log(mc.ToString());
                MulliganAck(mc, dc);
                return mc.GetCardsWeKeep().ToCards();
            }
            catch (Exception) // Mulligan Tester Event
            {
                
                mc = new MulliganContainer(AppDomain.CurrentDomain.BaseDirectory+"\\MulliganProfiles\\ACK-MulliganTester.ini",
                    choices.Select(c=> c.ToString()).ToList(), opponentClass.ToString(), ownClass.ToString());
                dc = new DeckClassification(mc.MyDeck);
                mc.Log("----------------Mulligan Tester Container");
               // mc.LogData = true;
                mc.Log(mc.ToString());
                mc.Log("\n"+dc.ToString() +"\n\n");
                MulliganAck(mc, dc);
                return mc.GetCardsWeKeep().ToCards();

            }
           
        }

       



        private void MulliganAck(MulliganContainer mc, DeckClassification dc)
        {
            AdjustUserPriorities(mc, dc);
            int allowed1Drops = mc.Coin ? mc.CoreMaxes.Max1DropsCoin : mc.CoreMaxes.Max1Drops;
            int allowed2Drops = mc.Coin ? mc.CoreMaxes.Max2DropsCoin : mc.CoreMaxes.Max2Drops;
            int allowed3Drops = mc.Coin ? mc.CoreMaxes.Max3DropsCoin : mc.CoreMaxes.Max3Drops;
            int allowed4Drops = mc.Coin ? mc.CoreMaxes.Max4DropsCoin : mc.CoreMaxes.Max4Drops;
            mc.Log("[ACK Mulligan Begins]");
            //1 Drops
            foreach (
                var q in
                mc.OneDrops.Where(q => q.IsMinion() && mc.GetPriority(q) >= 2)
                    .OrderByDescending(mc.GetPriority)
                    .Take(allowed1Drops))
            {
                mc.HasTurnOne = true;
                mc.Allow(q, mc.GetPriority(q) > 5);
            }
            if (!mc.AllowCoinSkip && mc.CoreMaxes.RequireOneForTwo && !mc.HasTurnOne)
            {
                HandleSpecialScenarios(mc, dc);
                return;
            }
            ;
            //2 Drops
            foreach (
                var q in
                mc.TwoDrops.Where(q => q.IsMinion() && mc.GetPriority(q) >= 2)
                    .OrderByDescending(mc.GetPriority)
                    .Take(allowed2Drops))
            {
                mc.HasTurnTwo = true;
                mc.Allow(q, mc.GetPriority(q) > 5);
            }
            if (!mc.AllowCoinSkip && mc.CoreMaxes.RequireTwoForThree && !mc.HasTurnTwo)
            {
                HandleSpecialScenarios(mc, dc);
                return;
            }
            ;

            //3 Drops
            foreach (
                var q in
                mc.ThreeDrops.Where(q => q.IsMinion() && mc.GetPriority(q) >= 1)
                    .OrderByDescending(mc.GetPriority)
                    .Take(allowed3Drops))
            {
                mc.Allow(q, false);
                mc.HasTurnThree = true;
            }

            if (!mc.AllowCoinSkip && mc.CoreMaxes.RequireThreeForFour && !mc.HasTurnThree)
            {
                HandleSpecialScenarios(mc, dc);
                return;
            }

            foreach (
                var q in
                mc.FourDrops.Where(q => q.IsMinion() && mc.GetPriority(q) >= 4)
                    .OrderByDescending(mc.GetPriority)
                    .Take(allowed4Drops))
            {
                mc.Allow(q, mc.Coin);
            }



            HandleSpecialScenarios(mc, dc);
            mc.Log("[ACK Mulligan End]");
        }

        private void AdjustUserPriorities(MulliganContainer mc, DeckClassification dc)
        {
            //TODO adjust later
        }
        private readonly DeckClassification.Style AGGRO = DeckClassification.Style.Aggro;
        private readonly DeckClassification.Style MIDRANGE = DeckClassification.Style.Midrange;
        private readonly DeckClassification.Style CONTROL = DeckClassification.Style.Control;

        private void HandleSpecialScenarios(MulliganContainer mc, DeckClassification dc)
        {
            mc.Log("--------Special Cases Block");
            List<Card.Cards> ThreatHandler = new List<Card.Cards>
            {
                //Mage
                Cards.Polymorph,
                Cards.PolymorphBoar,
                //Paladin
                Cards.Equality,
                Cards.AldorPeacekeeper,
                Cards.Humility,
                Cards.KeeperofUldaman,
                //Rogue
                Cards.Sap,
                //Priest
                Cards.ShadowWordDeath,
                //Warrior
                Cards.Execute,
                //Shaman
                Cards.Hex,
                //Hunter
                Cards.DeadlyShot,
                Cards.HuntersMark,
                //Druid
                Cards.Mulch,
                //Warlock
            };
            if (mc.Mode.Contains("Arena") && mc.OpponentClass.IsOneOf(Shaman) &&
                ThreatHandler.Intersect(mc.Choices.ToCards()).Any())
                mc.Allow(ThreatHandler.Intersect(mc.Choices.ToCards()).First(), false);

            #region spell/weapon handler


            switch (mc.OwnClass)
            {
                case HeroClass.SHAMAN:
                    //_whiteList.AddInOrder(1, mc.Choices, false, Cards.StormforgedAxe, Cards.Powermace);
                    //mc.Allow( Cards.RockbiterWeapon, false); // [1 Cost]
                    //mc.Allow( !hasGood2 && mc.Coin ? Cards.FarSight : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( Cards.FeralSpirit, false); // [3 Cost]
                    //mc.Allow( Cards.JadeClaws, false);
                    mc.Log("[We are Shaman]");
                    mc.AllowAgainst(Warlock, Shaman, Mage, Cards.Hex, Cards.AlAkirtheWindlord, Cards.Hex, Cards.LightningStorm);
                    //mc.AllowAgainst(AGGRO , Shaman, Cards.Hex, Cards.Hex, Cards.LightningBolt, Cards.LightningStorm);
                    mc.Log("[/We are Shaman]");

                    break;
                case HeroClass.PRIEST:
                    //mc.Allow( mc.Choices.HasAny(Cards.InjuredBlademaster) ? Cards.LightoftheNaaru : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( hasGood1Or2 ? Cards.HolySmite : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( !mc.Coin ? Cards.MindVision : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( Cards.PowerWordShield, false); // [1 Cost] 
                    //mc.Allow( Cards.ShadowWordPain, false); // [2 Cost]
                    //mc.Allow( Cards.Shadowform, false); // [3 Cost]
                    //mc.Allow( (hasGood1Or2 && mc.Coin) || hasGood2 ? Cards.VelensChosen : Card.Cards.GAME_005, false); // [3 Cost]
                    break;
                case HeroClass.MAGE:
                    //mc.Allow( hasGood1 ? Cards.Frostbolt : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( mc.Choices.HasAny(Cards.ManaWyrm) && mc.Coin ? Cards.MirrorImage : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( hasGood1 ? Cards.ArcaneMissiles : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( !hasGood1And2 ? Cards.MirrorEntity : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( !hasGood1And2 || hasGood2 ? Cards.ForgottenTorch : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( hasGood1 || mc.Coin ? Cards.Flamecannon : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( Cards.UnstablePortal, mc.Coin); // [2 Cost]
                    //mc.Allow( hasGood1Or2 ? Cards.ArcaneBlast : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( hasGood1Or2 ? Cards.VolcanicPotion : Nothing, false);
                    break;
                case HeroClass.PALADIN:
                    //_whiteList.AddInOrder(1, mc.Choices, false, Cards.LightsJustice, Cards.RallyingBlade, Cards.Coghammer, Cards.SwordofJustice);
                    //mc.Allow( mc.Coin ? Cards.DivineStrength : Nothing, false);
                    //mc.Allow( hasGood2 ? Cards.NobleSacrifice : Nothing, false); // [1 Cost]
                    //mc.Allow(false, Cards.Avenge, Cards.MusterforBattle);
                    //mc.Allow( Cards.SmugglersRun, false);

                    break;
                case HeroClass.WARRIOR:
                    //mc.Allow( mc.OpponentClass == Card.CClass.PALADIN ? Cards.Whirlwind : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( hasGood2 && mc.Choices.HasAny(Cards.ShieldSlam) ? Cards.ShieldBlock : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow(false, Cards.BloodToIchor, Cards.Slam); // [2 Cost]
                    //mc.Allow( !hasGood1 ? Cards.Upgrade : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( hasGood2 && mc.Choices.HasAny(Cards.ShieldBlock) ? Cards.ShieldSlam : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( hasGood1Or2 && !mc.Choices.HasTurn(3, 3) ? Cards.Bash : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( Cards.IKnowaGuy, false);
                    break;
                case HeroClass.WARLOCK:
                    //mc.Allow(false, Cards.RenounceDarkness, Cards.MortalCoil); // [1 Cost]
                    //mc.Allow( mc.Choices.HasAny(Cards.NerubianEgg) ? Cards.PowerOverwhelming : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( !hasGood2 ? Cards.CurseofRafaam : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( hasGood1Or2 || mc.Coin ? Cards.Darkbomb : Card.Cards.GAME_005, false); // [2 Cost]

                    break;
                case HeroClass.HUNTER:
                    //_whiteList.AddInOrder(1, mc.Choices, false, Cards.Glaivezooka, Cards.EaglehornBow);

                    //mc.Allow( !hasGood1Or2 ? Cards.Tracking : Card.Cards.GAME_005, false); // [1 Cost]
                    //mc.Allow( Cards.AnimalCompanion, mc.Coin); // [3 Cost]
                    //mc.Allow( mc.OpponentClass == HeroClass.PALADIN ? Cards.UnleashtheHounds : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( mc.Choices.HasAny(Cards.KnifeJuggler) ? Cards.SnakeTrap : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( !hasGood1Or2 ? Cards.FreezingTrap : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( hasGood1Or2 && mc.Coin ? Cards.QuickShot : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( hasGood1And2 ? Cards.Powershot : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( mc.Coin ? Cards.BearTrap : Card.Cards.GAME_005, false); // [2 Cost]
                    //mc.Allow( mc.Choices.HasRace(Card.CRace.BEAST) ? Cards.SmugglersCrate : Nothing, false);
                    //mc.Allow( mc.HasTurnOne ? Cards.HiddenCache : Nothing, false);
                    break;
                case HeroClass.ROGUE:
                    //_whiteList.AddInOrder(1, mc.Choices, false, Cards.PerditionsBlade, Cards.CogmastersWrench);
                    //mc.Allow( mc.Coin || !mc.HasTurnOne ? Cards.JourneyBelow : Nothing, false);
                    //mc.Allow( Cards.Backstab, false); // [0 Cost]
                    //mc.Allow( Cards.DeadlyPoison, false); // [1 Cost]
                    //mc.Allow( mc.OpponentClass == HeroClass.PALADIN ? Cards.FanofKnives : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( mc.Choices.HasAny(Cards.Burgle) || mc.Choices.HasAny(Cards.BeneaththeGrounds) ? Cards.Preparation : Card.Cards.GAME_005, false); // [0 Cost]
                    //mc.Allow( mc.Choices.HasAny(Cards.Preparation) ? Cards.Burgle : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( mc.Choices.HasAny(Cards.Preparation) ? Cards.BeneaththeGrounds : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( Cards.CounterfeitCoin, false);

                    break;
                case HeroClass.DRUID:

                    mc.Allow(Cards.Wrath, Cards.PoweroftheWild);
                    mc.Allow(Cards.LivingRoots, mc.Coin); // [1 Cost]
                    mc.Allow(Cards.JadeIdol, Cards.JadeBlossom);

                    break;
            }
            //Made Scientist Logic
            foreach (var card in from card in mc.Choices
                let cardQ = CardTemplate.LoadFromId(card)
                where
                mc.WhiteList.ContainsKey(card) && cardQ.IsSecret && mc.Choices.HasAny(Cards.MadScientist.ToString())
                select card)
            {
                mc.WhiteList.Remove(card);
            }

            #endregion
        }

        ///// <summary>
        ///// Secret Palaidn mulligan logic
        ///// ported from V2
        ///// </summary>
        ///// <param name="mc">game container</param>
        //private void HandleSecretPaladin(GameContainer mc)
        //{
        //    var has2Drop = mc.Choices.Any(c => c.Cost() == 2 && c.IsMinion());
        //    bool vAggro = mc.EnemyStyle.Aggresive();
        //    mc.Allow( !mc.Choices.HasTurn(1, 0) && mc.Coin && mc.Choices.HasAny(Cards.MusterforBattle) ? Cards.Avenge : Nothing, false);
        //    mc.Allow( mc.OpponentClass.Is(Shaman) && mc.Choices.HasAny(Cards.ShieldedMinibot) ? Cards.Redemption : Nothing, false);

        //    if (mc.Choices.HasAll(Cards.NobleSacrifice, Cards.Avenge, Cards.Secretkeeper) && mc.OpponentClass.Is(Shaman))
        //        mc.Allow(false, Cards.NobleSacrifice, Cards.Avenge, Cards.Secretkeeper);
        //    mc.Allow( mc.OpponentClass.Is(Shaman) && mc.Coin ? Cards.HarrisonJones : Nothing, false);
        //    mc.Allow( mc.Choices.HasAny(Cards.HauntedCreeper, Cards.NerubianEgg) && mc.Coin ? Cards.KeeperofUldaman : Nothing, false);

        //    foreach (var q in mc.Choices)
        //    {
        //        var card = CardTemplate.LoadFromId(q);
        //        if (q.Cost() == 1 && q.IsMinion())
        //            mc.Allow( q, card.Divineshield || card.Health == 3);
        //        if (q.Cost() == 2 && q.IsMinion())
        //            mc.Allow( q, card.Divineshield && card.Atk == 2);
        //        if (q.Cost() == 3 && q.IsSpell() && q != Cards.DivineFavor)
        //            mc.Allow( q, false);
        //    }
        //    if (vAggro)
        //    {
        //        mc.Allow( mc.OpponentClass.Is(Druid) ? Cards.AldorPeacekeeper : Nothing, false);
        //        mc.Allow( Cards.Consecration, false);
        //        mc.Allow( mc.Coin ? Cards.PilotedShredder : Nothing, false);
        //    }
        //    else
        //    {
        //        _whiteList.Remove(Cards.IronbeakOwl);
        //        mc.Allow( mc.Coin ? Cards.MysteriousChallenger : Nothing, false);
        //        mc.Allow( mc.Coin ? Cards.TruesilverChampion : Nothing, false);
        //        mc.Allow( Cards.PilotedShredder, mc.OpponentClass.Is(Warrior) && mc.Coin);
        //    }
        //    mc.Allow( Cards.MusterforBattle, false);
        //    if (mc.Choices.HasAny(Cards.Coghammer) && has2Drop && !mc.OpponentClass.Is(Warrior))
        //        mc.Allow( Cards.Coghammer, false);
        //    else if (mc.OpponentClass.IsOneOf(Warrior, Priest, Rogue, Druid))
        //        mc.Allow( Cards.TruesilverChampion, false);
        //}






    }
}
