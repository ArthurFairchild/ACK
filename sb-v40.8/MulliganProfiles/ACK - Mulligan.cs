using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using ACK;
using SmartBot.Database;
using SmartBot.Mulligan;
using SmartBot.Plugins;
using SmartBot.Plugins.API;



//Version ~3.01



namespace MulliganProfiles
{
    /// <summary>
    /// Defined separetly to avoid runtime compilation error on multiple usages of GeneralExtension
    /// </summary>
    public static class MulliganExtensionForLazy
    {
        public static CardTemplate Template(this Card.Cards card)
        {
            return CardTemplate.LoadFromId(card);
        }

        public static List<Card.Cards> ToCards(this List<string> stringList)
        {
            return stringList.Select(c => (Card.Cards)Enum.Parse(typeof(Card.Cards), c)).ToList();
        }

        public static List<Card.CRace> ToRaces(this List<string> stringList)
        {
            return stringList.Select(c => Template((Card.Cards)Enum.Parse(typeof(Card.Cards), c)).Race).ToList();
        }

        public static bool IsArena(this Bot.Mode mode)
        {
            return mode == Bot.Mode.Arena || mode == Bot.Mode.ArenaAuto;
        }
    }


    [Serializable]
    // ReSharper disable once InconsistentNaming
    public class Mulligan : MulliganProfile
    {
        public Card.Cards Nothing = Card.Cards.GAME_005; //Coin, but who cares that it's used for nothing


        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
        {

            var ret = HandleAckMulligan(choices, opponentClass, ownClass);
            return ret;
        }

        private List<Card.Cards> HandleAckMulligan(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
        {
            Bot.Log("ACK Mulligan 7.0");
            try
            {
                MulliganContainer mc;
                DeckClassification dc;
                try
                {
                    mc = new MulliganContainer(Bot.CurrentMode().ToString(),
                     choices.Select(c => c.ToString()).ToList(), opponentClass.ToString(), ownClass.ToString(),
                     Bot.CurrentDeck().Cards);
                    mc.Log("Mulligan Core Cretead succesfully");
                    dc = new DeckClassification(Bot.CurrentDeck().Cards);
                    mc.Log("Deck Classification Created succesfully");


                }
                catch (Exception)
                {
                    var mulliganTesterFileData = GetMulliganTesterData();
                    mc = new MulliganContainer(mulliganTesterFileData.Item1.ToString(),
                    choices.Select(c => c.ToString()).ToList(), opponentClass.ToString(), ownClass.ToString(),
                    mulliganTesterFileData.Item2);
                    dc = new DeckClassification(mulliganTesterFileData.Item2);
                    mc.Log("Mulligan Tester Event");
                }

                SetupMaxes(mc, dc);
                mc.Log("Core maxes created succesfully " + mc.CoreMaxes.ToString());
                if (mc.Mode == "ARENA")
                {
                    mc.Log("$$$$ Entering Arena Logic");
                    MulliganByCurve(mc, dc);
                    mc.Log("$$$$ Exiting Arena Logic");
                }
                else
                {
                    mc.Log("$$$$ Entering by style logic");
                    MulliganByStyle(mc, dc);
                    mc.Log("$$$$ Exiting by style logic");
                }

                mc.Log("============END EVENT===============");
                var @return =
                    mc.GetCardsWeKeep().Select(c => (Card.Cards)Enum.Parse(typeof(Card.Cards), c)).ToList();
                return @return;

            }
            catch (Exception e)
            {
                LogExtra(e.Message);
                Bot.Log("ACK Mulligan Failer. Consult ACK_MC_log for more details.\nSuspending the bot for manual log review " + e.Message);

            }
            var ret = new List<Card.Cards>() { Cards.TunnelTrogg, Cards.ALightintheDarkness };
            return ret;
        }

        private void LogExtra(string str)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\Logs\\ExtremeCrash.txt"))
            {
                sw.WriteLine(str);
            }
        }
        private Tuple<Bot.Mode, List<string>> GetMulliganTesterData()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
            {
                Bot.Log("Mulligan Tester Data Not Found. If you are seeing this, that means mulligan entered critical state. Recomended Action: Terminate SmartBotUI.exe");
                return new Tuple<Bot.Mode, List<string>>(Bot.Mode.RankedStandard, new List<string>());
            }
            var _mulliganTesterData =
                File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK-MulliganTester.ini").Split('~');
            Bot.Mode mode = _mulliganTesterData[14].Contains("Arena") ? Bot.Mode.Arena : _mulliganTesterData[14].Contains("Wild")?Bot.Mode.RankedWild: Bot.Mode.RankedStandard;
            List<string> cards = _mulliganTesterData[15].Split(',').ToList();

            return new Tuple<Bot.Mode, List<string>>(mode, cards);

        }

        private bool coinSkip = false;
        private void SetupMaxes(MulliganContainer mc, DeckClassification myDeckClassification)
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
                mc.Log("Fragile Code entered because of plugin Mulligan Container was not properly read:"+pluginNotFound.ToString());
                mc.Log(AppDomain.CurrentDomain.BaseDirectory + "MulliganProfiles\\ACK-MulliganTester.ini");
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
                {
                    mc.Log("Reading Mulligan Tester.ini");
                    var readLine = sr.ReadLine();
                    if (readLine != null)
                    {
                        var info = readLine.Split('~');
                        mc.Log("Split settings into " +string.Join("/", info));
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
                        mc.Log("Parsed Core maxes int "+ string.Join("/", values));
                        mc.CoreMaxes = new MulliganContainer.MulliganCoreData(values);
                        mc.Log("New Core Maxes setuo");
                        mc.CoreMaxes.UpdateStrictCurve(new List<bool>(){Convert.ToBoolean(info[10]), Convert.ToBoolean(info[11]) , Convert.ToBoolean(info[12]) });
                        coinSkip = Convert.ToBoolean(info[13]) && mc.Coin;
                        mc.Log("New Strict Curve setup");
                    }
                }
            }
            catch (Exception e)
            {
                mc.Log("Fragile Code entered because of some bs" + e.ToString());
                switch (myDeckClassification.DeckStyle)
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

        private void MulliganByCurve(MulliganContainer mc, DeckClassification myDeckClassification)
        {
            mc.Log("Curve/Arena Mulligan Logic Entered");
            try
            {
                mc.Log("Entered Fragile Code");

                var allowed1Drops = mc.Coin ? mc.CoreMaxes.Max1DropsCoin : mc.CoreMaxes.Max1Drops;
                var allowed2Drops = mc.Coin ? mc.CoreMaxes.Max2DropsCoin : mc.CoreMaxes.Max2Drops;
                var allowed3Drops = mc.Coin ? mc.CoreMaxes.Max3DropsCoin : mc.CoreMaxes.Max3Drops;
                var allowed4Drops = mc.Coin ? mc.CoreMaxes.Max4DropsCoin : mc.CoreMaxes.Max4Drops;

                foreach (var q in mc.OneDrops.Where(q => q.IsMinion() && mc.MinionPriorityTable[q] >= 2).OrderByDescending(q => mc.MinionPriorityTable[q]).Take(allowed1Drops))
                {
                    mc.Allow(q, mc.Coin && mc.MinionPriorityTable[q] > 5);
                }
                mc.Log("1 drops: fine " + mc.HasTurnOne);

                if (mc.CoreMaxes.RequireOneForTwo && !mc.HasTurnOne && !coinSkip)
                {
                    mc.Log("No one drops (required by MulliganContainer plugin): casting away all other minions");
                    HandleSpellsWeaponsAndUniqueInterractions(mc, myDeckClassification);
                    return;
                };
                //2 drop start
                foreach (var q in mc.TwoDrops.Where(q => q.IsMinion() && mc.MinionPriorityTable[q] >= 2).OrderByDescending(q => mc.MinionPriorityTable[q]).Take(allowed2Drops))
                {
                    mc.Allow(q, mc.MinionPriorityTable[q] > 5);
                }
                mc.Log("2 drops: fine " + mc.HasTurnTwo);

                if (mc.CoreMaxes.RequireTwoForThree && !mc.HasTurnTwo && !coinSkip)
                {
                    mc.Log("No two drops (required by MulliganContainer plugin): casting away all other minions");

                    HandleSpellsWeaponsAndUniqueInterractions(mc, myDeckClassification);
                    return;
                };

                //3 drop start
                foreach (var q in mc.ThreeDrops.Where(q => q.IsMinion() && mc.MinionPriorityTable[q] >= 1).OrderByDescending(q => mc.MinionPriorityTable[q]).Take(allowed3Drops))
                {
                    mc.Allow(q, false);

                }
                mc.Log("3 drops: fine " + mc.HasTurnThree);
                if (mc.CoreMaxes.RequireThreeForFour && !mc.HasTurnThree && !coinSkip)
                {
                    mc.Log("No three drops (required by MulliganContainer plugin): casting away all other minions");

                    HandleSpellsWeaponsAndUniqueInterractions(mc, myDeckClassification);
                    return;
                };

                foreach (var q in mc.FourDrops.Where(q => q.IsMinion() && mc.MinionPriorityTable[q] >= 4).OrderByDescending(q => mc.MinionPriorityTable[q]).Take(allowed4Drops))
                {
                    mc.Allow(q, mc.Coin);
                }
                HandleSpellsWeaponsAndUniqueInterractions(mc, myDeckClassification);
            }
            catch (Exception e)
            {
                Bot.Log("[URGENT] Please locate DebugLog.txt in Logs/ACKTracker/ folder and show it to Arthur \n\n" + e.Message);

            }
        }

        private void HandleSpellsWeaponsAndUniqueInterractions(MulliganContainer mc, DeckClassification dc)
        {
            var threatHandler = new List<Card.Cards>
            {
              //Mage
              Cards.Polymorph, Cards.PolymorphBoar,
              //Paladin
              Cards.Equality, Cards.AldorPeacekeeper, Cards.Humility, Cards.KeeperofUldaman,
              //Rogue
              Cards.Sap,
              //Priest
              Cards.ShadowWordDeath,
              //Warrior
              Cards.Execute,
              //Shaman
              Cards.Hex,
              //Hunter
              Cards.DeadlyShot,Cards.HuntersMark,
              //Druid
              Cards.Mulch,
              //Warlock
            };
            if (mc.Mode.Contains("Arena") && mc.OpponentClass.IsOneOf(HeroClass.SHAMAN) && threatHandler.Intersect(mc.Choices.ToCards()).Any())
                mc.Allow(threatHandler.Intersect(mc.Choices.ToCards()).First(), false);

            
            switch (mc.OwnClass)
            {
                case HeroClass.SHAMAN:
                    mc.Log("Shaman Case Entered");
                    mc.AllowWithCoin(Cards.TunnelTrogg, Cards.TunnelTrogg, Cards.FeralSpirit);
                    mc.AllowAsCombination(Cards.JadeClaws, Cards.TunnelTrogg);
                    mc.AllowAsCombination(Cards.BloodmageThalnos, Cards.SpiritClaws);
                    mc.AllowAsCombination(Cards.SmallTimeBuccaneer, Cards.SpiritClaws);
                    if (!mc.HasTurnOne) mc.Allow(Cards.SpiritClaws);//automatic filling of HasTun allows us to do this sorcery.
                    if(mc.Mode.Contains("Wild")) mc.Allow(Cards.LightningBolt);
                    if (mc.OpponentClass.IsOneOf(HeroClass.SHAMAN, HeroClass.WARRIOR, HeroClass.ROGUE))
                    {
                        bool weapon = mc.Choices.HasAny(Cards.JadeClaws.ToString(), Cards.SpiritClaws.ToString());
                        mc.Allow(1, Cards.JadeClaws, Cards.SpiritClaws);
                        if (mc.OpponentClass.IsOneOf(HeroClass.SHAMAN))
                        {
                            mc.Allow(Cards.LightningStorm);

                        }
                        if (mc.OpponentClass.IsOneOf(HeroClass.WARRIOR))
                        {
                            mc.Allow(1, Cards.MaelstromPortal, Cards.LightningStorm);
                            mc.AllowAsCombination(Cards.LightningBolt, Cards.TunnelTrogg);//We would already keep lighting bolt against aggro in wild automatically. This is a safety net.
                        }
                        if (mc.OpponentClass.IsOneOf(HeroClass.ROGUE, HeroClass.WARLOCK))
                        {
                            /*if(mc.OpponentStyle.IsOneOf(DeckClassification.Control)) 
                                mc.Allow(Cards.ManaTideTotem);
                             */
                            if(mc.Coin)
                                mc.Allow(Cards.Hex);//Adventurer/Edwin/Giants/4.10
                            
                        }
                    }
                    if (mc.OpponentClass.IsOneOf(HeroClass.DRUID))
                    {
                        if (mc.Mode.Contains("Wild"))
                        {
                            mc.Allow(Cards.MaelstromPortal, true); //2
                            mc.Allow(Cards.LightningStorm, false); //1 because overload punishes severly

                        }

                    }

                    mc.Log("Shaman Case Exited");
                    break;
                case HeroClass.PRIEST:

                    break;
                case HeroClass.MAGE:

                    break;
                case HeroClass.PALADIN:


                    break;
                case HeroClass.WARRIOR:
                    mc.Allow(Cards.FieryWarAxe);
                    mc.Allow(2, Cards.BloodToIchor, Cards.Upgrade, Cards.IKnowaGuy, Cards.Slam);
                    if (mc.MyChoicesClassification.DeckStyle == DeckClassification.Style.Control) mc.Allow(2, Cards.ShieldBlock, Cards.Bash, Cards.Execute);
                    break;
                case HeroClass.WARLOCK:
                    mc.Allow(1, Cards.Darkbomb, Cards.ShadowBolt);
                    if (mc.EnemyStyle == DeckClassification.Style.Aggro) mc.Allow(Cards.Demonwrath, Cards.Hellfire);

                    break;
                case HeroClass.HUNTER:
                    if (!mc.HasTurnOne && mc.Choices.ToRaces().HasRace(Card.CRace.BEAST, 1)) mc.Allow(Cards.SmugglersCrate);
                    mc.Allow(Cards.AnimalCompanion);
                    if (mc.EnemyStyle == DeckClassification.Style.Aggro) mc.Allow(Cards.ExplosiveTrap, Cards.UnleashtheHounds);

                    break;
                case HeroClass.ROGUE:
                    mc.Allow(1, Cards.PerditionsBlade, Cards.CogmastersWrench); //1 card in that order
                    mc.Allow(Cards.Backstab, Cards.CounterfeitCoin, Cards.DeadlyPoison);
                    if (mc.EnemyStyle == DeckClassification.Style.Aggro) mc.Allow(Cards.BladeFlurry, Cards.FanofKnives);
                    if (mc.EnemyStyle != DeckClassification.Style.Aggro) mc.Allow(Cards.Sap);
                    break;
                case HeroClass.DRUID:
                    mc.Allow(Cards.Innervate, Cards.JadeIdol, Cards.LivingRoots, Cards.WildGrowth, Cards.Wrath, Cards.JadeBlossom); // always allow these cards
                    if (mc.HasTurnOne && mc.Choices.ToRaces().HasRace(Card.CRace.BEAST, 1)) mc.Allow(Cards.MarkofYShaarj);
                    if (mc.EnemyStyle == DeckClassification.Style.Aggro) mc.Allow(Cards.Swipe);

                    break;
            }
            //Remove all secrets if we have mad scientist
            foreach (var card in from card in mc.Choices let cardQ = CardTemplate.LoadFromId(card) where mc.WhiteList.ContainsKey(card) && cardQ.IsSecret && mc.Choices.ToCards().HasAny(Cards.MadScientist) select card)
            {
                mc.WhiteList.Remove(card);
            }
        }

        private void MulliganByStyle(MulliganContainer mc, DeckClassification myDeckClassification)
        {
            mc.Log("Entered Mulligan by Style without an issue");
            UpdateSpecialPriorityCases(mc, myDeckClassification);

            switch (myDeckClassification.DeckStyle)
            {
                case DeckClassification.Style.Aggro:
                    mc.Log("Entering Aggro");
                    MulliganByCurve(mc, myDeckClassification);
                    break;
                case DeckClassification.Style.Midrange:
                    mc.Log("Entering Midrange");
                    MulliganByCurve(mc, myDeckClassification);
                    break;
                case DeckClassification.Style.Control:
                    mc.Log("Entering Control");
                    MulliganByCurve(mc, myDeckClassification);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateSpecialPriorityCases(MulliganContainer mc, DeckClassification dc)
        {
            mc.Log("Special Priority Cases " + dc.Dragons);
            mc.Log(dc.ClassificationSummary());
            if (dc.Dragons)
                GoThroughDragons(mc, dc);
            mc.UpdatePriorityTable(Cards.BloodmageThalnos, 0); //we only have value from him in a combination
            switch (mc.OwnClass)
            {
                case HeroClass.SHAMAN:
                    mc.UpdatePriorityTable(Cards.TotemGolem, 50);
                    mc.UpdatePriorityTable(Cards.TunnelTrogg, 50);
                    if (!dc.Pirates) break;
                    if (mc.Choices.Contains(Cards.JadeClaws.ToString()))
                    {
                        mc.UpdatePriorityTable(Cards.SmallTimeBuccaneer, 0);
                        mc.UpdatePriorityTable(Cards.TunnelTrogg, 50);
                    }
                    else if (mc.Choices.Contains(Cards.SpiritClaws.ToString()))
                    {
                        mc.UpdatePriorityTable(Cards.SmallTimeBuccaneer, 50);
                        mc.UpdatePriorityTable(Cards.TunnelTrogg, 1);
                    }
                    break;
                case HeroClass.PRIEST:
                    break;
                case HeroClass.MAGE:
                    break;
                case HeroClass.PALADIN:
                    break;
                case HeroClass.WARRIOR:

                    break;
                case HeroClass.WARLOCK:
                    break;
                case HeroClass.HUNTER:
                    break;
                case HeroClass.ROGUE:
                    break;
                case HeroClass.DRUID:
                    break;
                case HeroClass.NONE:
                    break;
                case HeroClass.JARAXXUS:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Update minion pririty tables
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="dc"></param>
        private void GoThroughDragons(MulliganContainer mc, DeckClassification dc)
        {
            mc.Log("\nEntered {GoThroughDragons}");

            mc.UpdatePriorityTable(Cards.TwilightWhelp, 3);
            mc.UpdatePriorityTable(Cards.WyrmrestAgent, mc.Coin ? 7 : 3);
            if(mc.Coin) mc.Allow(Cards.TwilightGuardian);
            mc.UpdatePriorityTable(Cards.AlexstraszasChampion, 3);
            if(mc.EnemyStyle != DeckClassification.Style.Aggro)
                mc.UpdatePriorityTable(Cards.TwilightDrake, 4);
            mc.UpdatePriorityTable(Cards.TwilightGuardian, mc.Coin ? 5: 2);
            if (mc.OpponentClass.IsOneOf(HeroClass.PRIEST))
            {
                mc.UpdatePriorityTable(Cards.TwilightDrake, mc.Coin ? 5:2);
                mc.UpdatePriorityTable(Cards.DrakonidOperative, mc.Coin ? 768 : 3);
                mc.Allow(Cards.DrakonidOperative);
            }
            if (mc.Choices.Contains(Cards.AlexstraszasChampion.ToString()) && mc.Choices.Intersect(dc.DragonCards).Any())
            {
                mc.Log("Allowing Dragon Activator for Alextraszas Champion");
                mc.Allow(mc.Choices.Intersect(dc.DragonCards).OrderBy(c=> CardTemplate.LoadFromId(c).Cost).ToList().First());
            }
            mc.Log("Existed {GoThroughDragons}\n");

        }
    }
}
