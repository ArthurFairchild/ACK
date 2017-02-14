using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ACK;
using SmartBot.Database;
using SmartBot.Mulligan;
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
                AssignCoreMaxes(mc, dc);
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
                AssignCoreMaxes(mc, dc);
                MulliganAck(mc, dc);
                return mc.GetCardsWeKeep().ToCards();

            }
           
        }

        private static void AssignCoreMaxes(MulliganContainer mc, DeckClassification dc)
        {
            
            try
            {
                var test =
                    Bot.GetPlugins().Find(c => c.DataContainer.Name == "ACK - Mulligan Container").GetProperties();
                mc.Log("Mulligan Container plugin communication was succesful");
                var values = (from q in test where q.Value is int select (int)q.Value).ToList();
                mc.Log(string.Join("/", values));
                var curve = (from q in test where q.Value is bool select (bool)q.Value).ToList();
                mc.Log(string.Join("/", curve));
                mc.CoreMaxes = new MulliganContainer.MulliganCoreData(values);
                mc.CoreMaxes.UpdateStrictCurve(curve);

                mc.Log("Strickt Requirments were setup without an issue");
                mc.Log("Overrwriten Mulligan Core Maxes: " + mc.CoreMaxes.ToString());

            }
            catch (NullReferenceException pluginNotFound)
            {
                mc.Log("Fragile Code entered because of plugin Mulligan Container was not properly read:" + pluginNotFound.ToString());
                mc.Log(AppDomain.CurrentDomain.BaseDirectory + "MulliganProfiles\\ACK-MulliganTester.ini");
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
                {
                    mc.Log("Reading Mulligan Tester.ini");
                    var readLine = sr.ReadLine();
                    if (readLine != null)
                    {
                        var info = readLine.Split('~');
                        mc.Log("Split settings into " + string.Join("/", info));
                        mc.MyStyle = (DeckClassification.Style)Enum.Parse(typeof(DeckClassification.Style), info[0]);
                        mc.Log("Parsed my Style");
                        mc.EnemyStyle = (DeckClassification.Style)Enum.Parse(typeof(DeckClassification.Style), info[1]);
                        mc.Log("Parsed enemy Style");
                        var values = new List<int>();

                        foreach (var q in info)
                        {
                            try
                            {
                                int val = int.Parse(q);
                                values.Add(val);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        mc.Log("Parsed Core maxes int " + string.Join("/", values));
                        mc.CoreMaxes = new MulliganContainer.MulliganCoreData(values);
                        mc.Log("New Core Maxes setuo");
                        mc.CoreMaxes.UpdateStrictCurve(new List<bool>() { Convert.ToBoolean(info[10]), Convert.ToBoolean(info[11]), Convert.ToBoolean(info[12]) });
                        mc.AllowCoinSkip = Convert.ToBoolean(info[13]) && mc.Coin;
                        mc.Log("New Strict Curve setup");
                    }
                }
            }
            catch (Exception e)
            {
                mc.Log("Fragile Code entered because of some bs" + e.ToString());
                switch (dc.DeckStyle)
                {
                    case DeckClassification.Style.Aggro:
                        mc.CoreMaxes = new MulliganContainer.MulliganCoreData(2, 3, 2, 3, 1, 2, 0, 1, true, true, false);
                        break;
                    case DeckClassification.Style.Midrange:
                        mc.CoreMaxes = new MulliganContainer.MulliganCoreData(2, 2, 3, 4, 2, 2, 0, 1, false, true, false);
                        break;
                    case DeckClassification.Style.Control:
                        mc.CoreMaxes = new MulliganContainer.MulliganCoreData(1, 1, 2, 4, 2, 2, 1, 2, false, false, false);


                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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
            AdjustInteractionsBasedOnCombinations(mc, dc);
            //1 Drops
            foreach (
                var q in
                mc.OneDrops.Where(q => (q.IsWeapon() || q.IsMinion()) && mc.GetPriority(q) >= 2)
                    .OrderByDescending(mc.GetPriority)
                    .Take(allowed1Drops))
            {
                mc.Log("One Drop "+q);
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
                mc.TwoDrops.Where(q => (q.IsWeapon() || q.IsMinion()) && mc.GetPriority(q) >= 2)
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
                mc.ThreeDrops.Where(q => (q.IsWeapon() || q.IsMinion()) && mc.GetPriority(q) >= 1)
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
                mc.FourDrops.Where(q => (q.IsWeapon() || q.IsMinion()) && mc.GetPriority(q) >= 4)
                    .OrderByDescending(mc.GetPriority)
                    .Take(allowed4Drops))
            {
                mc.Allow(q, mc.Coin);
            }



            HandleSpecialScenarios(mc, dc);
            mc.Log("[ACK Mulligan End]");
        }

        private void AdjustInteractionsBasedOnCombinations(MulliganContainer mc, DeckClassification dc)
        {
            if (mc.Choices.Any(c => c.IsWeapon() && CardTemplate.LoadFromId(c).Cost <= 3) && mc.Iam(AGGRO))
                mc.UpdatePriorityTable(Cards.SouthseaDeckhand.ToString(), 2);
            if (dc.Dragons)
            {
                mc.Log("\nEntered {GoThroughDragons}");

                mc.UpdatePriorityTable(Cards.TwilightWhelp, 3);
                mc.UpdatePriorityTable(Cards.WyrmrestAgent, mc.Coin ? 7 : 3);
                if (mc.Coin) mc.Allow(Cards.TwilightGuardian);
                mc.UpdatePriorityTable(Cards.AlexstraszasChampion, 3);
                if (mc.EnemyStyle != DeckClassification.Style.Aggro)
                    mc.UpdatePriorityTable(Cards.TwilightDrake, 4);
                mc.UpdatePriorityTable(Cards.TwilightGuardian, mc.Coin ? 5 : 2);
                if (mc.Against(Priest))
                {
                    mc.UpdatePriorityTable(Cards.TwilightDrake, mc.Coin ? 5 : 2);
                    mc.UpdatePriorityTable(Cards.DrakonidOperative, mc.Coin ? 768 : 3);
                    mc.Allow(Cards.DrakonidOperative);//He is above 5 mana, and won't be handled by core
                }
                if (mc.Choices.Contains(Cards.AlexstraszasChampion.ToString()) && mc.Choices.Intersect(dc.DragonCards).Any())
                {
                    mc.Log("Allowing Dragon Activator for Alextraszas Champion");
                    mc.Allow(mc.Choices.Intersect(dc.DragonCards).OrderBy(c => CardTemplate.LoadFromId(c).Cost).ToList().First());
                }
                mc.Log("Existed {GoThroughDragons}\n");
            }

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
                case HeroClass.SHAMAN://Cards like Totem Golem Are handled by Core
                    //_whiteList.AddInOrder(1, mc.Choices, false, Cards.StormforgedAxe, Cards.Powermace);
                    //mc.Allow( Cards.RockbiterWeapon, false); // [1 Cost]
                    //mc.Allow( !hasGood2 && mc.Coin ? Cards.FarSight : Card.Cards.GAME_005, false); // [3 Cost]
                    //mc.Allow( Cards.FeralSpirit, false); // [3 Cost]
                    //mc.Allow( Cards.JadeClaws, false);
                    mc.Log("[We are Shaman]");
                    mc.Allow(1, Cards.JadeClaws, Cards.SpiritClaws);
                    mc.AllowOnCoin(Cards.TunnelTrogg, Cards.TunnelTrogg , Cards.FeralSpirit);
                    mc.AllowAsCombination(Cards.JadeClaws, Cards.TunnelTrogg);
                    mc.AllowAsCombination(Cards.BloodmageThalnos, Cards.SpiritClaws);
                    mc.AllowAsCombination(Cards.SmallTimeBuccaneer, Cards.SpiritClaws);
                    mc.AllowOnCoin(mc.HasTurnOne && mc.HasTurnTwo ? Cards.FlamewreathedFaceless: Nothing);
                    mc.Log(mc.HasTurnOne+ " " +mc.HasTurnTwo);
                    if (!mc.HasTurnOne && !mc.Choices.Contains(Cards.JadeClaws.ToString())) mc.Allow(Cards.SpiritClaws);//automatic filling of HasTun allows us to do this sorcery.
                    if (mc.Mode.Contains("Wild"))
                        mc.AllowAgainst(AGGRO, Cards.LightningBolt);

                    if (mc.Against(Shaman, Warrior, Rogue))
                    {
                        mc.Allow(1, Cards.JadeClaws, Cards.SpiritClaws);
                        mc.AllowAgainst(Shaman, Cards.LightningStorm);
                        if(mc.Against(Warrior))
                        {
                            mc.AllowAgainst(AGGRO, mc.Coin ? Cards.LightningBolt: Nothing);
                            mc.AllowAsCombination(Cards.LightningBolt, Cards.TunnelTrogg);//We would already keep lighting bolt against aggro in wild automatically. This is a safety net.
                            mc.Allow(1, Cards.MaelstromPortal, Cards.LightningStorm);
                        }
                        mc.AllowAgainst(CONTROL, Rogue, Cards.Hex, Cards.ManaTideTotem);
                        mc.AllowAgainst(Rogue, Cards.Hex); //Against Edwin/Questing
                    }
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
