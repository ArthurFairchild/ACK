using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public Card.Cards Nothing = Card.Cards.GAME_005;//Who needs a coin? 
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
            MulliganContainer t =  EvalueaContainer(choices, opponentClass, ownClass); //point proven [what did it accomplish again?]
            return t.GetCardsWeKeep().ToCards();
            
        }

        public MulliganContainer EvalueaContainer(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
        {
            MulliganContainer mc;
            DeckClassification dc;
            try
            {
                mc = new MulliganContainer(Bot.CurrentMode().ToString(),
                    choices.Select(c => c.ToString()).ToList(), opponentClass.ToString(), ownClass.ToString(),
                    Bot.CurrentDeck().Cards);
                dc = new DeckClassification(Bot.CurrentDeck().Cards);
                Bot.Log("[ACK - Mulligan] Treating our deck as " + dc.Name.ToLower());
                mc.LogData = true; //No need to debug log when bot is succesfully running [disagree]
                mc.Log(mc.ToString());
                AssignCoreMaxes(mc, dc);
                MulliganAck(mc, dc);
                return mc;
            }
            catch (Exception e) // Mulligan Tester Event
            {
                Bot.Log("[ACK - Mulligan] Encountered an Error " + e.Message + ", using Mulligan Tester Values.");
                mc =
                    new MulliganContainer(
                        AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK-MulliganTester.ini",
                        choices.Select(c => c.ToString()).ToList(), opponentClass.ToString(), ownClass.ToString());
                dc = new DeckClassification(mc.MyDeck);
                mc.Log("----------------Mulligan Tester Container");
                mc.LogData = true;
                mc.Log(e.Message + "\n\n" + e);
                mc.Log(mc.ToString());
                mc.Log("\n" + dc.ToString() + "\n\n");
                AssignCoreMaxes(mc, dc);
                MulliganAck(mc, dc);
                return mc;

            }
            finally
            {
                Bot.Log("[ACK] Complete");

            }
        }
        public static Dictionary<Card.CClass, DeckClassification.Style> ClassStyleDictionary =
            new Dictionary<Card.CClass, DeckClassification.Style>();
        private static void AssignCoreMaxes(MulliganContainer mc, DeckClassification dc)
        {
            
            try
            {
               
                var test =
                      Bot.GetPlugins().Find(c => c.DataContainer.Name == "ACK - Mulligan Container").GetProperties();
                foreach (var q in test.Where(c => c.Key.Length >= 12))
                {
                    if (q.Key.Substring(12).Length < 4) continue; //against unconventional naming techniques
                    try
                    {
                        ClassStyleDictionary.AddOrUpdate
                            ((Card.CClass) Enum.Parse(typeof(Card.CClass), q.Key.Substring(12).ToUpper()),(DeckClassification.Style) q.Value);
                    }
                    catch (Exception exception)
                    {
                        Bot.Log("[ACK Error]" +exception.Message);
                        mc.Log("[ACK Error]" + exception.Message);
                        
                    }

                }
                if (string.Equals(test["ModeAssumption"].ToString(), "FromPlugin", StringComparison.Ordinal))
                {
                    mc.EnemyStyle = ClassStyleDictionary[(Card.CClass) mc.OpponentClass];
                }
                Bot.Log("[ACK - Mulligan] Treating enemy as " + mc.EnemyStyle.ToString().ToLower() +" " +mc.OpponentClass.ToString().ToLower());
                mc.Log("Mulligan Container plugin communication was succesful");
                var values = (from q in test where q.Value is int select (int)q.Value).ToList();
                mc.Log(string.Join("/", values));
                var curve = (from q in test where q.Value is bool select (bool)q.Value).ToList();
                var modeAdjustment = (from q in test where q.Value is string select (string) q.Value).ToList();
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
            if (mc.Choices.Count(c=> c.ToString() == Cards.TotemGolem.ToString()) == 2)
                mc.HasTurnThree = true;
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
                mc.UpdatePriorityTable(Cards.TwilightDrake, 5);
                mc.UpdatePriorityTable(Cards.TwilightWhelp, 3);
                mc.UpdatePriorityTable(Cards.WyrmrestAgent, mc.Coin ? 7 : 3);
                if (mc.Coin) mc.Allow(Cards.TwilightGuardian);
                mc.UpdatePriorityTable(Cards.AlexstraszasChampion, 3);
                if (mc.EnemyStyle != DeckClassification.Style.Aggro)
                    mc.UpdatePriorityTable(Cards.TwilightDrake, 4);
                mc.UpdatePriorityTable(Cards.TwilightGuardian, mc.Coin ? 5 : 2);
                if (mc.Against(Priest))
                {
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
            if (mc.Against(Paladin, Hunter, Rogue, Warrior))
            {
                mc.UpdatePriorityTable(Cards.AcidicSwampOoze,
                    mc.Choices.Any(c => c != Cards.AcidicSwampOoze.ToString() && CardTemplate.LoadFromId(c).Cost == 2)
                        ? 3
                        : 0);
                mc.AllowOnCoin(Cards.AcidicSwampOoze);
            }
            if (mc.Against(Warlock))
            {
                mc.UpdatePriorityTable(Cards.DaringReporter, 4);
            }
            if(!mc.Iam(AGGRO) && mc.Iam(Warrior)) mc.UpdatePriorityTable(Cards.BloodhoofBrave, 4);

        }

        private void AdjustUserPriorities(MulliganContainer mc, DeckClassification dc)
        {
            return;
            using (StreamReader sr = new StreamReader("ACK-Mulligan.ini"))
            {
                //TODO adjust later
            }
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
           
            if(mc.Iam(CONTROL))mc.Allow(Cards.Doomsayer);
            if (mc.Against(CONTROL) && mc.Against(Priest, Warlock, Mage))
            {
                if(mc.WhiteList.ContainsKey(Cards.MistressofMixtures.ToString()))
                     mc.WhiteList.Remove(Cards.MistressofMixtures.ToString());
            }
            switch (mc.OwnClass)
            {
                case HeroClass.SHAMAN:
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
                    mc.AllowAgainst(Priest, Cards.FlametongueTotem);
                    //mc.AllowAgainst(AGGRO , Shaman, Cards.Hex, Cards.Hex, Cards.LightningBolt, Cards.LightningStorm);
                    mc.Log("[/We are Shaman]");

                    break;
                case HeroClass.PRIEST:
                    if (mc.Iam(CONTROL) && !mc.Against(AGGRO))
                    {
                        mc.AllowAsCombination(Cards.InjuredBlademaster, Cards.CircleofHealing);
                        mc.AllowAsCombination(Cards.InjuredBlademaster, Cards.LightoftheNaaru);
                    }
                    mc.AllowAgainst(AGGRO,Cards.HolySmite, Cards.ShadowWordPain);
                    mc.Allow(mc.HasTurnOne||mc.Coin,  Cards.PowerWordShield); // [1 Cost] 
                    mc.Allow( mc.HasTurnTwo , Cards.VelensChosen); // [3 Cost]
                    break;
                case HeroClass.MAGE:
                    mc.AllowAgainst(AGGRO, Cards.Frostbolt, Cards.ArcaneBlast,
                        Cards.ForbiddenFlame, Cards.ForgottenTorch, Cards.Flamecannon, Cards.VolcanicPotion);
                    if(mc.Coin)mc.AllowAsCombination(Cards.ManaWyrm, Cards.MirrorImage);
                    mc.Allow( Cards.UnstablePortal, mc.Coin); // [2 Cost]
                    
                    break;
                case HeroClass.PALADIN:
                    mc.AllowAsCombination(Cards.MusterforBattle, Cards.Avenge);
                    mc.AllowAsCombination(Cards.KnifeJuggler, Cards.NobleSacrifice);
                    mc.AllowAsCombination(Cards.Secretkeeper, Cards.Avenge, Cards.NobleSacrifice);
                    if(mc.Coin) mc.AllowAsCombination(Cards.Redemption, Cards.ShieldedMinibot);
                    if(mc.HasTurnOne && mc.HasTurnTwo && mc.HasTurnThree && mc.Coin && mc.Against(Warrior, Hunter))mc.Allow(Cards.HarrisonJones);
                    if (mc.Coin)
                    {
                        mc.AllowAsCombination(Cards.HauntedCreeper, Cards.KeeperofUldaman);
                        mc.AllowAsCombination(Cards.NerubianEgg, Cards.KeeperofUldaman);
                    }
                    if (mc.Against(AGGRO))
                    {
                        mc.AllowAgainst(Druid, Cards.AldorPeacekeeper);
                        mc.AllowOnCoin(Cards.Consecration);
                        mc.AllowOnCoin(Cards.PilotedShredder);
                    }
                    else
                    {
                        mc.AllowOnCoin(Cards.MysteriousChallenger, Cards.TruesilverChampion);
                        mc.AllowAgainst(Warrior, CONTROL, Cards.PilotedShredder, Cards.PilotedShredder);
                    }
                    mc.Allow(Cards.MusterforBattle);
                    if(mc.HasTurnTwo) mc.Allow(Cards.Coghammer);
                    mc.AllowAgainst(Priest, Rogue, Druid, Cards.TruesilverChampion);
                    mc.Allow(1, Cards.LightsJustice, Cards.RallyingBlade, Cards.Coghammer, Cards.SwordofJustice);
                    mc.Allow(Cards.SmugglersRun);
                    if (mc.Mode.Contains("Arena"))
                    {
                        mc.AllowOnCoin(Cards.DivineStrength);
                    }
                    

                    break;
                case HeroClass.WARRIOR:
                    
                    mc.Allow(Cards.FieryWarAxe);
                    if (mc.Mode.Contains("Arena"))
                    {
                        mc.AllowAgainst(Paladin, Cards.Whirlwind);
                        if(!mc.HasTurnOne)mc.Allow(Cards.IKnowaGuy);

                    }
                    if (mc.Iam(CONTROL))
                    {
                        mc.AllowAsCombination(Cards.ShieldSlam, Cards.ShieldBlock);
                        mc.Allow(Cards.BloodToIchor, Cards.Slam);
                        if(mc.HasTurnTwo && mc.Against(MIDRANGE)) mc.AllowAsCombination(Cards.ShieldBlock, Cards.ShieldSlam);//debatable
                        if(!mc.HasTurnThree && mc.HasTurnTwo) mc.Allow(Cards.Bash);
                    }
                    mc.AllowAgainst(CONTROL, Cards.Execute);
                    break;
                case HeroClass.WARLOCK:
                    if(mc.Iam(CONTROL))mc.Allow(Cards.RenounceDarkness, Cards.MortalCoil, Cards.Darkbomb);
                    mc.AllowAgainst(AGGRO, Cards.Demonwrath );
                    mc.AllowAgainst(Warrior, Priest, Rogue, Cards.ShadowBolt);
                    mc.AllowAsCombination(Cards.NerubianEgg, Cards.PowerOverwhelming);
                    if(mc.HasTurnOne || mc.HasTurnTwo) mc.Allow(Cards.Darkbomb);
                    
                    break;
                case HeroClass.HUNTER:
                    //_whiteList.AddInOrder(1, mc.Choices, false, Cards.Glaivezooka, Cards.EaglehornBow);
                    mc.Allow(1, Cards.Glaivezooka, Cards.EaglehornBow);
                    
                    mc.Allow( !(mc.HasTurnOne && mc.HasTurnTwo) , Cards.Tracking); // [1 Cost]
                    mc.Allow(mc.HasTurnTwo, Cards.AnimalCompanion);
                    mc.AllowAgainst(AGGRO, Cards.UnleashtheHounds);
                    mc.AllowAsCombination(Cards.KnifeJuggler, Cards.SnakeTrap);
                    mc.Allow(mc.HasTurnOne && mc.HasTurnTwo, Cards.Powershot);
                    mc.AllowOnCoin(Cards.BearTrap);
                    mc.Allow(Cards.SmugglersCrate);
                    mc.Allow( mc.HasTurnOne, Cards.HiddenCache );
                    break;
                case HeroClass.ROGUE:
                    mc.Allow(1, Cards.PerditionsBlade, Cards.CogmastersWrench);
                    mc.Allow( mc.Coin || !mc.HasTurnOne , Cards.JourneyBelow);
                    mc.Allow( Cards.Backstab, Cards.DeadlyPoison, Cards.CounterfeitCoin); // [0 Cost]
                    mc.AllowAgainst(AGGRO,  Cards.FanofKnives ); // [3 Cost]
                    mc.AllowAsCombination(Cards.Preparation, Cards.BeneaththeGrounds); //Meme fiesta won't work.[You are a fucking idiot lol]
                    mc.AllowAsCombination(Cards.CounterfeitCoin, Cards.CounterfeitCoin, Cards.EdwinVanCleef);
                    

                    break;
                case HeroClass.DRUID:

                    mc.Allow(Cards.Wrath, Cards.PoweroftheWild, 
                        Cards.JadeIdol, Cards.JadeBlossom,
                        Cards.Innervate, Cards.Innervate, Cards.WildGrowth);//double allows duplicate [there is ought to be a better way of writing that]
                    mc.Allow(Cards.LivingRoots, mc.Coin); // [1 Cost]
                   

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

       


    }
}
