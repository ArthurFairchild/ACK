﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmartBot.Plugins.API;

namespace ACK
{
   
    public class MulliganContainer

    {
        //public ACK.Style
        public class MulliganCoreData
        {
            /// <summary>
            /// Limit 1 drops without coin
            /// </summary>
            public int Max1Drops { get; set; }
            /// <summary>
            /// Limit 1 drops with coin
            /// </summary>
            public int Max1DropsCoin { get; set; }
            /// <summary>
            /// Limit 2 drops without coin
            /// </summary>
            public int Max2Drops { get; set; }
            /// <summary>
            /// Limit 2 drops with coin
            /// </summary>
            public int Max2DropsCoin { get; set; }
            /// <summary>
            /// Limit 3 dops without coin
            /// </summary>
            public int Max3Drops { get; set; }
            /// <summary>
            /// Limit 3 drops with coin
            /// </summary>
            public int Max3DropsCoin { get; set; }
            /// <summary>
            /// Limit 4 drops without coin
            /// </summary>
            public int Max4Drops { get; set; }
            /// <summary>
            /// Limit 4 drops with coin
            /// </summary>
            public int Max4DropsCoin { get; set; }

            public bool RequireOneForTwo { get; set; }
            public bool RequireTwoForThree { get; set; }
            public bool RequireThreeForFour { get; set; }



            public MulliganCoreData()
            {
                Max1Drops = 1; 
                Max1DropsCoin = 2;
                Max2Drops = 2;
                Max2DropsCoin = 2;
                Max3Drops = 1;
                Max3DropsCoin = 2;
                Max4Drops = 1;
                Max4DropsCoin = 2;
                RequireOneForTwo = false;
                RequireTwoForThree = false;
                RequireThreeForFour = false;
            }
            public MulliganCoreData(List<int> data)
            {
                if (data.Count != 8) return;
                Max1Drops = data[0]; //1
                Max1DropsCoin = data[1];//1c
                Max2Drops = data[2];//2
                Max2DropsCoin = data[3];//2c
                Max3Drops = data[4];//3
                Max3DropsCoin = data[5];//3c
                Max4Drops = data[6];//4
                Max4DropsCoin = data[7];//4c
                RequireOneForTwo = false;
                RequireTwoForThree = false;
                RequireThreeForFour = false;
            }

            /// <summary>
            /// Pass down all necessary arguments
            /// </summary>
            /// <param name="v1">max 1 drops</param>
            /// <param name="v2">max 1 drops with coin</param>
            /// <param name="v3">max 2 drops</param>
            /// <param name="v4">max 2 drops with coin</param>
            /// <param name="v5">max 3 drops</param>
            /// <param name="v6">max 3 drops with coin</param>
            /// <param name="v7">max 4 drops</param>
            /// <param name="v8">max 4 drops with coin</param>
            /// <param name="onefortwo">Strict curve, requires 1 drop for 2 drop</param>
            /// <param name="twoforthree">Strict curve requires 2 drop for 3 drop</param>
            /// <param name="threeforfour">Strict curve requires 3 drop for 4 drop</param>
            public MulliganCoreData(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, bool onefortwo, bool twoforthree, bool threeforfour)
            {
                Max1Drops = v1;
                Max1DropsCoin = v2;
                Max2Drops = v3;
                Max2DropsCoin = v4;
                Max3Drops = v5;
                Max3DropsCoin = v6;
                Max4Drops = v7;
                Max4DropsCoin = v8;
                RequireOneForTwo = onefortwo;
                RequireTwoForThree = twoforthree;
                RequireThreeForFour = threeforfour;

            }

            public void UpdateStrictCurve(bool oneForTwo, bool twoForThree, bool threeForFour)
            {
                RequireOneForTwo = oneForTwo;
                RequireTwoForThree = twoForThree;
                RequireThreeForFour = threeForFour;
            }

            public void UpdateStrictCurve(List<bool> values)
            {
                RequireOneForTwo = values[0];
                RequireTwoForThree = values[1];
                RequireThreeForFour = values[2];
            }
            /// <summary>
            /// Prints out object string
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("Container " + Max1Drops + Max1DropsCoin + Max2Drops + Max2DropsCoin + Max3Drops + Max3DropsCoin + Max4Drops + Max4DropsCoin );
            }
        }
        /// <summary>
        /// MinMax for minion drops with mana cost
        /// </summary>
        public MulliganCoreData CoreMaxes { get; set; }
        /// <summary>
        /// Mode of the game we are playing
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Our Gameplay Style. 
        /// </summary>
        public DeckClassification.Style MyStyle { get; set; }
        /// <summary> Our Deck
        /// </summary>
        public List<string> MyDeck { get; set; }
        /// <summary>
        /// Enemy Style, which will be taken either from history, or default
        /// </summary>
        public DeckClassification.Style EnemyStyle { get; set; }
        /// <summary>
        /// Write log information to file
        /// </summary>
        public bool LogData { get; set; }
        /// <summary>
        /// Our Choices
        /// </summary>
        public List<string> Choices { get; set; }
        /// <summary>
        /// Our Opponent Class
        /// </summary>
        public HeroClass OpponentClass { get; set; }
        /// <summary>
        /// Our Class
        /// </summary>
        public HeroClass OwnClass { get; set; }

        /// <summary>
        /// All 0 drops presented in choices
        /// </summary>
        public List<string> ZeroDrops { get; set; }
        /// <summary>
        /// All one drops presented in choices
        /// </summary>
        public List<string> OneDrops { get; set; }
        /// <summary>
        /// All two drops presented in choices
        /// </summary>
        public List<string> TwoDrops { get; set; }
        /// <summary>
        /// all three drops presented in choices
        /// </summary>
        public List<string> ThreeDrops { get; set; }
        /// <summary>
        /// All Four Drops presented in choices
        /// </summary>
        public List<string> FourDrops { get; set; }
        /// <summary>
        /// All Five Drops presented in choices
        /// </summary>
        public List<string> FivePlusDrops { get; set; }

        /// <summary>
        /// Automatically tells you if you have a 1 mana drop (minion)
        /// </summary>
        public bool HasTurnOne { get; set; }

        /// <summary>
        /// Automatically tells you if you have a 2 mana drop (minion)
        /// </summary>
        public bool HasTurnTwo { get; set; }

        /// <summary>
        /// Automatically tells you if you have a 3 mana drop (minion)
        /// </summary>
        public bool HasTurnThree { get; set; }

        /// <summary>
        /// Have a coin
        /// </summary>
        public bool Coin { get; set; }

        public Dictionary<string, bool> WhiteList { get; set; }
        /// <summary>
        /// Information from MyDeckClassification
        /// </summary>
        public DeckClassification MyDeckClassification { get; set; }
        /// <summary>
        /// Information about my choices From Classification
        /// </summary>
        public DeckClassification MyChoicesClassification { get; set; }

        public List<string> CardsToKeep { get; set; }

        /// <summary>
        /// Mulligan Container provided mode, choices, opponent class, own class and our decklist
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="choices"></param>
        /// <param name="opponentClass"></param>
        /// <param name="ownClass"></param>
        /// <param name="myDeckList"></param>
        public MulliganContainer(string mode, List<string> choices, string opponentClass,
            string ownClass, List<string> myDeckList)
        {

            Choices = choices;
            OpponentClass = (HeroClass)Enum.Parse(typeof(HeroClass), opponentClass);
            OwnClass = (HeroClass)Enum.Parse(typeof(HeroClass), ownClass);
            Coin = choices.Count > 3;

            LogData = false;
            MyDeck = myDeckList;
            MyDeckClassification = new DeckClassification(MyDeck, OwnClass);
            MyChoicesClassification = new DeckClassification(choices, OwnClass);
            Mode = mode;
            if (Mode == "ARENA")
            {
                MyStyle = DeckClassification.Style.Midrange;
                EnemyStyle = DeckClassification.Style.Midrange;
            }
            else
            {
                MyStyle = MyDeckClassification.DeckStyle;
                EnemyStyle = DeckClassification.Style.Midrange;
            }

            ZeroDrops = Choices.Where(card => new MinimalCardTemplate(card).Cost == 0).ToList();
            OneDrops = Choices.Where(card => new MinimalCardTemplate(card).Cost == 1).ToList();
            TwoDrops = Choices.Where(card => new MinimalCardTemplate(card).Cost == 2).ToList();
            ThreeDrops = Choices.Where(card => new MinimalCardTemplate(card).Cost == 3).ToList();
            FourDrops = Choices.Where(card => new MinimalCardTemplate(card).Cost == 4).ToList();
            FivePlusDrops = Choices.Where(card => new MinimalCardTemplate(card).Cost > 4).ToList();
            HasTurnOne = false;
            HasTurnTwo = false;
            HasTurnThree = false;
            WhiteList = new Dictionary<string, bool>();
            CardsToKeep = new List<string>();
            CoreMaxes = new MulliganCoreData();
        }

        public List<string> GetCardsWeKeep()
        {
            foreach (var s in from s in Choices
                              let keptOneAlready = CardsToKeep.Any(c => c.ToString() == s.ToString())
                              where WhiteList.ContainsKey(s)
                              where !keptOneAlready | WhiteList[s]
                              select s)
                CardsToKeep.Add(s);
            Log($"We kept {string.Join(",", CardsToKeep)}");
            return CardsToKeep;
        }

        /// <summary>
        /// Update whitelist in the game container
        /// </summary>
        /// <param name="minPriority">Minimum priority draft to count toward Has1/2/3drops</param>
        /// <param name="card">Card we are whitelisting</param>
        /// <param name="value">Allow duplicates?</param>
        public void Allow(object card, bool value, int minPriority = 0)
        {

            Log($"{card.ToString()} was passed with value {value}. Priority: {GetPriority(card.ToString())}");
            switch (GetCost((string)card.ToString()))
            {
                case 1:
                    if (!HasTurnOne) //Safety net against hardcoded tech cards whose priority would set it to false.
                        HasTurnOne = card.ToString().IsMinion() &&
                                     GetPriority(card.ToString()) >= minPriority;
                    break;
                case 2:
                    if (!HasTurnTwo) //Safety net against hardcoded tech cards whose priority would set it to false.

                        HasTurnTwo = card.ToString().IsMinion() &&
                                     GetPriority(card.ToString()) >= minPriority;
                    break;
                case 3:
                    if (!HasTurnThree)
                        //Safety net against hardcoded tech cards whose priority would set it to false.

                        HasTurnThree = card.ToString().IsMinion() &&
                                       GetPriority(card.ToString()) >= minPriority;
                    break;
                default:
                    break;
            }
            WhiteList[card.ToString()] = value;
            Log($"{card.ToString()} was succesfully added to WhiteList with value of {value}" +
                $"\nHas turn 1 {HasTurnOne}\tHas turn 2 {HasTurnTwo}\nHas turn 3 {HasTurnThree}\n");
        }
        /// <summary>
        /// Min Priority for Duplicates
        /// </summary>
        /// <param name="card">Card</param>
        /// <param name="minPriority">Min Priority value to allow duplicates</param>
        /// <param name="value">value</param>
        public void Allow(object card, int minPriority, bool value)
        {
            if(value && card.ToString().IsMinion() && GetPriority(card.ToString()) >= minPriority)
                Allow(card, true);
            else Allow(card, false);
        }
        /// <summary>
        /// Allows multiple cards to be whitelisted
        /// Cards written Twice with allow duplicates
        /// </summary>
        /// <param name="cards"></param>
        public void Allow(params object[] cards)
        {
            foreach (var q in cards)
            {
                Allow(q.ToString(), cards.Count(c => c == q) > 1);
            }
        }

        public void AllowAsCombination(object requirment, object comboPiece)
        {
            string[] combo = new[] {requirment.ToString(), comboPiece.ToString()};
            Log($"Combination passed {requirment} {comboPiece}");
            if (Choices.Intersect(combo).Count() == 2)
            {
                Allow(comboPiece, requirment);
                Log($"Allowing {requirment} {comboPiece}");

            }
            else Log($"Combination Rejected: Combination not present");
        }
        /// <summary>
        /// Allow cards in order until count is reached.
        /// Allow(1, Glavezooka, Eaglehorbow) will keep only glavezooka because it's first in the list
        /// </summary>
        /// <param name="count">Number of cards from the list in that order</param>
        /// <param name="cards">List of cards</param>
        public void Allow(int count, params object[] cards)
        {
            int i = 0;
            foreach (var q in cards)
            {
                if (i >= count) break;
                if (!Choices.Contains(q)) continue;
                Allow(q);
                i++;
            }
        }
        /// <summary>
        /// Allow cards from the list based on priority value
        /// It will itterate Minimalistic Priority Table and select X amount of cards with highest priority value
        /// </summary>
        /// <param name="count">number of cards to select</param>
        /// <param name="bypriority"></param>
        /// <param name="cards"></param>
        public void Allow(int count, bool bypriority, params object[] cards)
        {
          
            if (!bypriority)
                Allow(count, cards);
            else Allow(count, cards.OrderByDescending(c=> MinionPriorityTable[c.ToString()]));
        }
        /// <summary>
        /// Debug Log
        /// </summary>
        /// <param name="str">Message Log</param>
        public void Log(string str)
        {
            //if (!LogData) return;
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACKTracker\\MatchHistory.txt"))
            {
                File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACKTracker\\MatchHistory.txt");
            }
            try
            {
                using (
                    StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACK_MC_log",
                        true))
                {
                    sw.WriteLine(str);
                }
            }
            catch (Exception )
            {
                
            }
        }
        /// <summary>
        /// Clear debug log
        /// </summary>
        public void ClearLog()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACK_MC_log", "");
        }

        private static int GetCost(string card)
        {
            return new MinimalCardTemplate(card).Cost;
        }
        /// <summary>
        /// General Value Rules Table:
        /// -0 - Curt must be in the deck for value
        /// 0 - never keep
        /// 1 - Keep if nothing better pops out
        /// 2 - Keep
        /// 3 - Really good card to keep
        /// 4+ - Keep always regardless of maxes
        /// </summary>
        /// <param name="id">card id</param>
        /// <param name="value">value</param>
        public void UpdatePriorityTable(object id, int value)
        {
            var temp = new MinimalCardTemplate(id.ToString());
            if (temp.Type != CardType.MINION || temp.Cost > 4)
            {
                var logmessage = $"{temp.Name} was not updated because " + (temp.Type != CardType.MINION ? "Not a minion" : "Cost is too high to be in Mulligan Priority Table. Hardcode it to keep");
                Log(logmessage);
                return;
            }
            MinionPriorityTable[id.ToString()] = value;
            Log($"Updated {temp.Name} with new value.");
        }

        public int GetPriority(string card)
        {
            Log($"Priority for {card} has been requested." );
            return !card.IsMinion() ? 0 : MinionPriorityTable[card];
        }
        /// <summary>
        /// Priority Table
        /// </summary>
        public Dictionary<string, int> MinionPriorityTable = new Dictionary<string, int>
        {

            {"CS2_231", 0}, //[1/1]Wisp [0 mana] [NONE card]
            {"LOEA10_3", 0}, //[1/1]Murloc Tinyfin [0 mana] [NONE card]
            {"GVG_093", 0}, //[0/2]Target Dummy [0 mana] [NONE card]
            {"CS1_042", 0}, //[1/2]Goldshire Footman [1 mana] [NONE card]
            {"CS2_065", 3}, //[1/3]Voidwalker [1 mana] [WARLOCK card]
            {"CS2_168", 2}, //[2/1]Murloc Raider [1 mana] [NONE card]
            {"CS2_171", 2}, //[1/1]Stonetusk Boar [1 mana] [NONE card]
            {"CS2_189", 3}, //[1/1]Elven Archer [1 mana] [NONE card]
            {"CS2_235", 4}, //[1/3]Northshire Cleric [1 mana] [PRIEST card]
            {"DS1_175", 0}, //[1/1]Timber Wolf [1 mana] [HUNTER card]
            {"EX1_011", 1}, //[2/1]Voodoo Doctor [1 mana] [NONE card]
            {"EX1_508", 0}, //[1/1]Grimscale Oracle [1 mana] [NONE card]
            {"CS2_059", 1}, //[0/1]Blood Imp [1 mana] [WARLOCK card]
            {"CS2_146", 1}, //[2/1]Southsea Deckhand [1 mana] [NONE card]
            {"CS2_169", 0}, //[1/1]Young Dragonhawk [1 mana] [NONE card]
            {"CS2_188", 4}, //[2/1]Abusive Sergeant [1 mana] [NONE card]
            {"EX1_001", 0}, //[1/2]Lightwarden [1 mana] [NONE card]
            {"EX1_004", 1}, //[2/1]Young Priestess [1 mana] [NONE card]
            {"EX1_008", 3}, //[1/1]Argent Squire [1 mana] [NONE card]
            {"EX1_009", 0}, //[1/1]Angry Chicken [1 mana] [NONE card]
            {"EX1_010", 4}, //[2/1]Worgen Infiltrator [1 mana] [NONE card]
            {"EX1_029", 2}, //[2/1]Leper Gnome [1 mana] [NONE card]
            {"EX1_080", 2}, //[1/2]Secretkeeper [1 mana] [NONE card]
            {"EX1_243", -5}, //[3/1]Dust Devil [1 mana] [SHAMAN card]
            {"EX1_319", 5}, //[3/2]Flame Imp [1 mana] [WARLOCK card]
            {"EX1_405", 0}, //[0/4]Shieldbearer [1 mana] [NONE card]
            {"EX1_509", 0}, //[1/2]Murloc Tidecaller [1 mana] [NONE card]
            {"NEW1_012", 4}, //[1/3]Mana Wyrm [1 mana] [MAGE card]
            {"NEW1_017", 1}, //[1/2]Hungry Crab [1 mana] [NONE card]
            {"NEW1_025", 1}, //[1/2]Bloodsail Corsair [1 mana] [NONE card]
            {"LOE_010", 2}, //[2/1]Pit Snake [1 mana] [ROGUE card]
            {"LOE_018", 5}, //[1/3]Tunnel Trogg [1 mana] [SHAMAN card]
            {"LOE_076", 4}, //[1/3] Sir Finley Mrrgglton [1 mana] [NONE card]
            {"LOE_116", -1}, //[1/1]Reliquary Seeker [1 mana] [WARLOCK card]
            {"FP1_001", 10}, //[2/3]Zombie Chow [1 mana] [NONE card]
            {"FP1_011", 3}, //[1/1]Webspinner [1 mana] [HUNTER card]
            {"FP1_028", 0}, //[1/2]Undertaker [1 mana] [NONE card]
            {"GVG_009", 0}, //[2/1]Shadowbomber [1 mana] [PRIEST card]
            {"GVG_013", 2}, //[1/2]Cogmaster [1 mana] [NONE card]
            {"GVG_051", 1}, //[1/3]Warbot [1 mana] [WARRIOR card]
            {"GVG_082", 3}, //[2/1]Clockwork Gnome [1 mana] [NONE card]
            {"BRM_004", 2}, //[2/1]Twilight Whelp [1 mana] [PRIEST card]
            {"BRM_022", 0}, //[0/2]Dragon Egg [1 mana] [NONE card]
            {"AT_029", 8}, //[2/1]Buccaneer [1 mana] [ROGUE card]
            {"AT_059", -1}, //[2/1]Brave Archer [1 mana] [HUNTER card]
            {"AT_082", 2}, //[1/2]Lowly Squire [1 mana] [NONE card]
            {"AT_097", 1}, //[2/1]Tournament Attendee [1 mana] [NONE card]
            {"AT_105", 1}, //[2/4]Injured Kvaldir [1 mana] [NONE card]
            {"AT_133", 6}, //[1/2]Gadgetzan Jouster [1 mana] [NONE card]
            {"CS2_120", 3}, //[2/3]River Crocolisk [2 mana] [NONE card]
            {"CS2_121", 2}, //[2/2]Frostwolf Grunt [2 mana] [NONE card]
            {"CS2_142", 1}, //[2/2]Kobold Geomancer [2 mana] [NONE card]
            {"CS2_172", 3}, //[3/2]Bloodfen Raptor [2 mana] [NONE card]
            {"CS2_173", 0}, //[2/1]Bluegill Warrior [2 mana] [NONE card]
            {"EX1_015", 3}, //[1/1]Novice Engineer [2 mana] [NONE card]
            {"EX1_066", 3}, //[3/2]Acidic Swamp Ooze [2 mana] [NONE card]
            {"EX1_306", 1}, //[4/3]Succubus [2 mana] [WARLOCK card]
            {"EX1_506", 2}, //[2/1]Murloc Tidehunter [2 mana] [NONE card]
            {"EX1_565", 0}, //[0/3]Flametongue Totem [2 mana] [SHAMAN card]
            {"CS2_203", 0}, //[2/1]Ironbeak Owl [2 mana] [NONE card]
            {"EX1_012", 2}, //[1/1]Bloodmage Thalnos [2 mana] [NONE card]
            {"EX1_045", 2}, //[4/5]Ancient Watcher [2 mana] [NONE card]
            {"EX1_049", 3}, //[3/2]Youthful Brewmaster [2 mana] [NONE card]
            {"EX1_055", 2}, //[1/3]Mana Addict [2 mana] [NONE card]
            {"EX1_058", 3}, //[2/3]Sunfury Protector [2 mana] [NONE card]
            {"EX1_059", 2}, //[2/2]Crazed Alchemist [2 mana] [NONE card]
            {"EX1_076", 1}, //[2/2]Pint-Sized Summoner [2 mana] [NONE card]
            {"EX1_082", 3}, //[3/2]Mad Bomber [2 mana] [NONE card]
            {"EX1_096", 4}, //[2/1]Loot Hoarder [2 mana] [NONE card]
            {"EX1_100", 0}, //[0/4]Lorewalker Cho [2 mana] [NONE card]
            {"EX1_131", 2}, //[2/2]Defias Ringleader [2 mana] [ROGUE card]
            {"EX1_162", 2}, //[2/2]Dire Wolf Alpha [2 mana] [NONE card]
            {"EX1_341", 2}, //[0/5]Lightwell [2 mana] [PRIEST card]
            {"EX1_362", 0}, //[2/2]Argent Protector [2 mana] [PALADIN card]
            {"EX1_393", 3}, //[2/3]Amani Berserker [2 mana] [NONE card]
            {"EX1_402", 2}, //[1/4]Armorsmith [2 mana] [WARRIOR card]
            {"EX1_522", 1}, //[1/1]Patient Assassin [2 mana] [ROGUE card]
            {"EX1_531", 2}, //[2/2]Scavenging Hyena [2 mana] [HUNTER card]
            {"EX1_557", 3}, //[0/4]Nat Pagle [2 mana] [NONE card]
            {"EX1_603", 2}, //[2/2]Cruel Taskmaster [2 mana] [WARRIOR card]
            {"EX1_608", 3}, //[3/2]Sorcerer's Apprentice [2 mana] [MAGE card]
            {"EX1_616", 2}, //[2/2]Mana Wraith [2 mana] [NONE card]
            {"NEW1_018", 3}, //[2/3]Bloodsail Raider [2 mana] [NONE card]
            {"NEW1_019", 4}, //[3/2]Knife Juggler [2 mana] [NONE card]
            {"NEW1_020", 3}, //[3/2]Wild Pyromancer [2 mana] [NONE card]
            {"NEW1_021", 1}, //[0/7]Doomsayer [2 mana] [NONE card]
            {"NEW1_023", 3}, //[3/2]Faerie Dragon [2 mana] [NONE card]
            {"NEW1_029", 4}, //[4/4]Millhouse Manastorm [2 mana] [NONE card]
            {"NEW1_037", 2}, //[1/3]Master Swordsmith [2 mana] [NONE card]
            {"LOE_006", 2}, //[1/2]Museum Curator [2 mana] [PRIEST card]
            {"LOE_023", 4}, //[2/2]Dark Peddler [2 mana] [WARLOCK card]
            {"LOE_029", 4}, //[1/1]Jeweled Scarab [2 mana] [NONE card]
            {"LOE_046", 4}, //[3/2]Huge Toad [2 mana] [NONE card]
            {"NEW1_016", 1}, //[1/1]Captain's Parrot [2 mana] [NONE card]
            {"FP1_002", 4}, //[1/2]Haunted Creeper [2 mana] [NONE card]
            {"FP1_003", 3}, //[1/2]Echoing Ooze [2 mana] [NONE card]
            {"FP1_004", 4}, //[2/2]Mad Scientist [2 mana] [NONE card]
            {"FP1_007", 0}, //[0/2]Nerubian Egg [2 mana] [NONE card]
            {"FP1_017",2}, //[1/4]Nerub'ar Weblord [2 mana] [NONE card]
            {"FP1_024",2}, //[1/3]Unstable Ghoul [2 mana] [NONE card]
            {"GVG_002",4}, //[2/3]Snowchugger [2 mana] [MAGE card]
            {"GVG_006",5}, //[2/3]Mechwarper [2 mana] [NONE card]
            {"GVG_011",3}, //[3/2]Shrinkmeister [2 mana] [PRIEST card]
            {"GVG_018",2}, //[1/4]Mistress of Pain [2 mana] [WARLOCK card]
            {"GVG_023",5}, //[3/2]Goblin Auto-Barber [2 mana] [ROGUE card]
            {"GVG_025",2}, //[4/1]One-eyed Cheat [2 mana] [ROGUE card]
            {"GVG_030",3}, //[2/2]Anodized Robo Cub [2 mana] [DRUID card]
            {"GVG_037",3}, //[3/2]Whirling Zap-o-matic [2 mana] [SHAMAN card]
            {"GVG_039",0}, //[0/3]Vitality Totem [2 mana] [SHAMAN card]
            {"GVG_058",5}, //[2/2]Shielded Minibot [2 mana] [PALADIN card]
            {"GVG_064",3}, //[3/2]Puddlestomper [2 mana] [NONE card]
            {"GVG_067",3}, //[2/3]Stonesplinter Trogg [2 mana] [NONE card]
            {"GVG_072",4}, //[2/3]Shadowboxer [2 mana] [PRIEST card]
            {"GVG_075",3}, //[2/3]Ship's Cannon [2 mana] [NONE card]
            {"GVG_076",1}, //[1/1]Explosive Sheep [2 mana] [NONE card]
            {"GVG_081",3}, //[2/3]Gilblin Stalker [2 mana] [NONE card]
            {"GVG_085",2}, //[1/2]Annoy-o-Tron [2 mana] [NONE card]
            {"GVG_087",3}, //[2/3]Steamwheedle Sniper [2 mana] [HUNTER card]
            {"GVG_103",2}, //[1/2]Micro Machine [2 mana] [NONE card]
            {"GVG_108",3}, //[3/2]Recombobulator [2 mana] [NONE card]
            {"AT_003",3}, //[3/2]Fallen Hero [2 mana] [MAGE card]
            {"AT_021",3}, //[3/2]Tiny Knight of Evil [2 mana] [WARLOCK card]
            {"AT_026",2}, //[4/3]Wrathguard [2 mana] [WARLOCK card]
            {"AT_030",4}, //[3/2]Undercity Valiant [2 mana] [ROGUE card]
            {"AT_031",1}, //[2/2]Cutpurse [2 mana] [ROGUE card]
            {"AT_038",10}, //[2/3]Darnassus Aspirant [2 mana] [DRUID card]
            {"AT_042",2}, //[2/1]Druid of the Saber [2 mana] [DRUID card]
            {"AT_052", 8}, //[3/4]Totem Golem [2 mana] [SHAMAN card]
            {"AT_058",3}, //[3/2]King's Elekk [2 mana] [HUNTER card]
            {"AT_069",3}, //[3/2]Sparring Partner [2 mana] [WARRIOR card]
            {"AT_071",3}, //[2/3]Alexstrasza's Champion [2 mana] [WARRIOR card]
            {"AT_080",3}, //[2/3]Garrison Commander [2 mana] [NONE card]
            {"AT_084",2}, //[1/2]Lance Carrier [2 mana] [NONE card]
            {"AT_089",4}, //[3/2]Boneguard Lieutenant [2 mana] [NONE card]
            {"AT_094",3}, //[2/3]Flame Juggler [2 mana] [NONE card]
            {"AT_109",2}, //[2/4]Argent Watchman [2 mana] [NONE card]
            {"AT_116",1}, //[1/4]Wyrmrest Agent [2 mana] [PRIEST card]
            {"CS2_118", -1}, //[5/1]Magma Rager [3 mana] [NONE card]
            {"CS2_122",1}, //[2/2]Raid Leader [3 mana] [NONE card]
            {"CS2_124", -1}, //[3/1]Wolfrider [3 mana] [NONE card]
            {"CS2_125",2}, //[3/3]Ironfur Grizzly [3 mana] [NONE card]
            {"CS2_127",0}, //[1/4]Silverback Patriarch [3 mana] [NONE card]
            {"CS2_141",2}, //[2/2]Ironforge Rifleman [3 mana] [NONE card]
            {"CS2_196",3}, //[2/3]Razorfen Hunter [3 mana] [NONE card]
            {"EX1_019",3}, //[3/2]Shattered Sun Cleric [3 mana] [NONE card]
            {"EX1_084",1}, //[2/3]Warsong Commander [3 mana] [WARRIOR card]
            {"EX1_582",1}, //[1/4]Dalaran Mage [3 mana] [NONE card]
            {"CS2_117",3}, //[3/3]Earthen Ring Farseer [3 mana] [NONE card]
            {"CS2_181",5}, //[4/7]Injured Blademaster [3 mana] [NONE card]
            {"EX1_005",0}, //[4/2]Big Game Hunter [3 mana] [NONE card]
            {"EX1_006",0}, //[0/3]Alarm-o-Bot [3 mana] [NONE card]
            {"EX1_007",2}, //[1/3]Acolyte of Pain [3 mana] [NONE card]
            {"EX1_014",5}, //[5/5]King Mukla [3 mana] [NONE card]
            {"EX1_017",4}, //[4/2]Jungle Panther [3 mana] [NONE card]
            {"EX1_020",4}, //[3/1]Scarlet Crusader [3 mana] [NONE card]
            {"EX1_021",1}, //[2/3]Thrallmar Farseer [3 mana] [NONE card]
            {"EX1_044",2}, //[2/2]Questing Adventurer [3 mana] [NONE card]
            {"EX1_050",1}, //[2/2]Coldlight Oracle [3 mana] [NONE card]
            {"EX1_083",3}, //[3/3]Tinkmaster Overspark [3 mana] [NONE card]
            {"EX1_085",0}, //[3/3]Mind Control Tech [3 mana] [NONE card]
            {"EX1_089",0}, //[4/2]Arcane Golem [3 mana] [NONE card]
            {"EX1_102",2}, //[1/4]Demolisher [3 mana] [NONE card]
            {"EX1_103",2}, //[2/3]Coldlight Seer [3 mana] [NONE card]
            {"EX1_134",4}, //[3/3]SI:7 Agent [3 mana] [ROGUE card]
            {"EX1_170",2}, //[2/3]Emperor Cobra [3 mana] [NONE card]
            {"EX1_258",2}, //[2/4]Unbound Elemental [3 mana] [SHAMAN card]
            {"EX1_301",4}, //[3/5]Felguard [3 mana] [WARLOCK card]
            {"EX1_304",2}, //[3/3]Void Terror [3 mana] [WARLOCK card]
            {"EX1_382",3}, //[3/3]Aldor Peacekeeper [3 mana] [PALADIN card]
            {"EX1_390",1}, //[2/3]Tauren Warrior [3 mana] [NONE card]
            {"EX1_412",2}, //[3/3]Raging Worgen [3 mana] [NONE card]
            {"EX1_507",2}, //[3/3]Murloc Warleader [3 mana] [NONE card]
            {"EX1_556", 6}, //[2/3]Harvest Golem [3 mana] [NONE card]
            {"EX1_575",0}, //[0/3]Mana Tide Totem [3 mana] [SHAMAN card]
            {"EX1_590",3}, //[3/3]Blood Knight [3 mana] [NONE card]
            {"EX1_597",4}, //[1/5]Imp Master [3 mana] [NONE card]
            {"EX1_604",3}, //[2/4]Frothing Berserker [3 mana] [WARRIOR card]
            {"EX1_612",4}, //[4/3]Kirin Tor Mage [3 mana] [MAGE card]
            {"EX1_613",0}, //[2/2]Edwin VanCleef [3 mana] [ROGUE card]
            {"NEW1_027",1}, //[3/3]Southsea Captain [3 mana] [NONE card]
            {"tt_004",2}, //[2/3]Flesheating Ghoul [3 mana] [NONE card]
            {"LOE_019",4}, //[3/4]Unearthed Raptor [3 mana] [ROGUE card]
            {"LOE_020",1}, //[2/4]Desert Camel [3 mana] [HUNTER card]
            {"LOE_022",4}, //[3/4]Fierce Monkey [3 mana] [WARRIOR card]
            {"LOE_050",4}, //[3/2]Mounted Raptor [3 mana] [DRUID card]
            {"LOE_077",2}, //[2/4]Brann Bronzebeard [3 mana] [NONE card]
            {"FP1_005",0}, //[2/2]Shade of Naxxramas [3 mana] [NONE card]
            {"FP1_009",4}, //[2/8]Deathlord [3 mana] [NONE card]
            {"FP1_023",5}, //[3/4]Dark Cultist [3 mana] [PRIEST card]
            {"FP1_027",2}, //[1/4]Stoneskin Gargoyle [3 mana] [NONE card]
            {"FP1_029",5}, //[4/4]Dancing Swords [3 mana] [NONE card]
            {"GVG_027",3}, //[2/2]Iron Sensei [3 mana] [ROGUE card]
            {"GVG_032",2}, //[2/4]Grove Tender [3 mana] [DRUID card]
            {"GVG_044",5}, //[3/4]Spider Tank [3 mana] [NONE card]
            {"GVG_048",4}, //[3/3]Metaltooth Leaper [3 mana] [HUNTER card]
            {"GVG_065",5}, //[4/4]Ogre Brute [3 mana] [NONE card]
            {"GVG_084",0}, //[1/4]Flying Machine [3 mana] [NONE card]
            {"GVG_089",0}, //[2/4]Illuminator [3 mana] [NONE card]
            {"GVG_092",2}, //[3/2]Gnomish Experimenter [3 mana] [NONE card]
            {"GVG_095",0}, //[2/4]Goblin Sapper [3 mana] [NONE card]
            {"GVG_097",2}, //[2/3]Lil' Exorcist [3 mana] [NONE card]
            {"GVG_098",0}, //[1/4]Gnomeregan Infantry [3 mana] [NONE card]
            {"GVG_101",3}, //[4/3]Scarlet Purifier [3 mana] [PALADIN card]
            {"GVG_102",2}, //[3/3]Tinkertown Technician [3 mana] [NONE card]
            {"GVG_104",1}, //[2/3]Hobgoblin [3 mana] [NONE card]
            {"GVG_123",4}, //[3/3]Soot Spewer [3 mana] [MAGE card]
            {"BRM_002",3}, //[2/4]Flamewaker [3 mana] [MAGE card]
            {"BRM_006",5}, //[2/4]Imp Gang Boss [3 mana] [WARLOCK card]
            {"BRM_010",3}, //[2/2]Druid of the Flame [3 mana] [DRUID card]
            {"BRM_033",3}, //[2/4]Blackwing Technician [3 mana] [NONE card]
            {"AT_007",4}, //[3/4]Spellslinger [3 mana] [MAGE card]
            {"AT_014",2}, //[3/3]Shadowfiend [3 mana] [PRIEST card]
            {"AT_032",2}, //[4/3]Shady Dealer [3 mana] [ROGUE card]
            {"AT_046",3}, //[3/2]Tuskarr Totemic [3 mana] [SHAMAN card]
            {"AT_057",2}, //[4/2]Stablemaster [3 mana] [HUNTER card]
            {"AT_063t",2}, //[4/2]Dreadscale [3 mana] [HUNTER card]
            {"AT_066",1}, //[3/3]Orgrimmar Aspirant [3 mana] [WARRIOR card]
            {"AT_075",0}, //[2/4]Warhorse Trainer [3 mana] [PALADIN card]
            {"AT_083",4}, //[3/3]Dragonhawk Rider [3 mana] [NONE card]
            {"AT_086",3}, //[4/3]Saboteur [3 mana] [NONE card]
            {"AT_087",3}, //[2/1]Argent Horserider [3 mana] [NONE card]
            {"AT_092",0}, //[5/2]Ice Rager [3 mana] [NONE card]
            {"AT_095",5}, //[2/2]Silent Knight [3 mana] [NONE card]
            {"AT_100", 6}, //[3/3]Silver Hand Regent [3 mana] [NONE card]
            {"AT_106",2}, //[4/3]Light's Champion [3 mana] [NONE card]
            {"AT_110",4}, //[2/5]Coliseum Manager [3 mana] [NONE card]
            {"AT_115",1}, //[2/2]Fencing Coach [3 mana] [NONE card]
            {"AT_117",0}, //[4/2]Master of Ceremonies [3 mana] [NONE card]
            {"AT_129",4}, //[3/4]Fjola Lightbane [3 mana] [NONE card]
            {"AT_131",4}, //[3/4]Eydis Darkbane [3 mana] [NONE card]
            {"CS2_033", 6}, //[3/6]Water Elemental [4 mana] [MAGE card]
            {"CS2_119",0}, //[2/7]Oasis Snapjaw [4 mana] [NONE card]
            {"CS2_131",0}, //[2/5]Stormwind Knight [4 mana] [NONE card]
            {"CS2_147",0}, //[2/4]Gnomish Inventor [4 mana] [NONE card]
            {"CS2_179",4}, //[3/5]Sen'jin Shieldmasta [4 mana] [NONE card]
            {"CS2_182",5}, //[4/5]Chillwind Yeti [4 mana] [NONE card]
            {"CS2_197",3}, //[4/4]Ogre Magi [4 mana] [NONE card]
            {"DS1_070",0}, //[4/3]Houndmaster [4 mana] [HUNTER card]
            {"EX1_025",0}, //[2/4]Dragonling Mechanic [4 mana] [NONE card]
            {"EX1_587",0}, //[3/3]Windspeaker [4 mana] [SHAMAN card]
            {"NEW1_011",0}, //[4/3]Kor'kron Elite [4 mana] [WARRIOR card]
            {"EX1_023",0}, //[3/3]Silvermoon Guardian [4 mana] [NONE card]
            {"EX1_043",1}, //[4/1]Twilight Drake [4 mana] [NONE card]
            {"EX1_046",5}, //[4/4]Dark Iron Dwarf [4 mana] [NONE card]
            {"EX1_048",0}, //[4/3]Spellbreaker [4 mana] [NONE card]
            {"EX1_057",0}, //[5/4]Ancient Brewmaster [4 mana] [NONE card]
            {"EX1_093",0}, //[2/3]Defender of Argus [4 mana] [NONE card]
            {"EX1_166",0}, //[2/4]Keeper of the Grove [4 mana] [DRUID card]
            {"EX1_274",0}, //[3/3]Ethereal Arcanist [4 mana] [MAGE card]
            {"EX1_313",0}, //[5/6]Pit Lord [4 mana] [WARLOCK card]
            {"EX1_315",0}, //[0/4]Summoning Portal [4 mana] [WARLOCK card]
            {"EX1_335",0}, //[0/5]Lightspawn [4 mana] [PRIEST card]
            {"EX1_396",0}, //[1/7]Mogu'shan Warden [4 mana] [NONE card]
            {"EX1_398",0}, //[3/3]Arathi Weaponsmith [4 mana] [WARRIOR card]
            {"EX1_584",0}, //[2/5]Ancient Mage [4 mana] [NONE card]
            {"EX1_591",1}, //[3/5]Auchenai Soulpriest [4 mana] [PRIEST card]
            {"EX1_595",0}, //[4/2]Cult Master [4 mana] [NONE card]
            {"NEW1_014",0}, //[4/4]Master of Disguise [4 mana] [ROGUE card]
            {"NEW1_022",0}, //[3/3]Dread Corsair [4 mana] [NONE card]
            {"NEW1_026",0}, //[3/5]Violet Teacher [4 mana] [NONE card]
            {"LOE_012",1}, //[5/4]Tomb Pillager [4 mana] [ROGUE card]
            {"LOE_016",0}, //[2/6]Rumbling Elemental [4 mana] [SHAMAN card]
            {"LOE_017",0}, //[3/4]Keeper of Uldaman [4 mana] [PALADIN card]
            {"LOE_039",0}, //[3/4]Gorillabot A-3 [4 mana] [NONE card]
            {"LOE_047",0}, //[3/3]Tomb Spider [4 mana] [NONE card]
            {"LOE_051",0}, //[4/4]Jungle Moonkin [4 mana] [DRUID card]
            {"LOE_079",0}, //[3/5]Elise Starseeker [4 mana] [NONE card]
            {"LOE_107",0}, //[7/7]Eerie Statue [4 mana] [NONE card]
            {"LOE_110",0}, //[7/4]Ancient Shade [4 mana] [NONE card]
            {"EX1_062",0}, //[2/4]Old Murk-Eye [4 mana] [NONE card]
            {"FP1_016",0}, //[3/5]Wailing Soul [4 mana] [NONE card]
            {"FP1_022",4}, //[3/4]Voidcaller [4 mana] [WARLOCK card]
            {"FP1_026",3}, //[5/5]Anub'ar Ambusher [4 mana] [ROGUE card]
            {"FP1_031",0}, //[1/7]Baron Rivendare [4 mana] [NONE card]
            {"GVG_004",3}, //[5/4]Goblin Blastmage [4 mana] [MAGE card]
            {"GVG_020",1}, //[3/5]Fel Cannon [4 mana] [WARLOCK card]
            {"GVG_040",0}, //[2/5]Siltfin Spiritwalker [4 mana] [SHAMAN card]
            {"GVG_055",0}, //[2/5]Screwjank Clunker [4 mana] [WARRIOR card]
            {"GVG_066",0}, //[5/4]Dunemaul Shaman [4 mana] [SHAMAN card]
            {"GVG_068",0}, //[3/5]Burly Rockjaw Trogg [4 mana] [NONE card]
            {"GVG_071",0}, //[5/4]Lost Tallstrider [4 mana] [NONE card]
            {"GVG_074",0}, //[4/3]Kezan Mystic [4 mana] [NONE card]
            {"GVG_078",5}, //[4/5]Mechanical Yeti [4 mana] [NONE card]
            {"GVG_091",4}, //[2/5]Arcane Nullifier X-21 [4 mana] [NONE card]
            {"GVG_094",0}, //[1/4]Jeeves [4 mana] [NONE card]
            {"GVG_096", 8}, //[4/3]Piloted Shredder [4 mana] [NONE card]
            {"GVG_107",0}, //[3/2]Enhance-o Mechano [4 mana] [NONE card]
            {"GVG_109",0}, //[4/1]Mini-Mage [4 mana] [NONE card]
            {"GVG_122",0}, //[2/5]Wee Spellstopper [4 mana] [MAGE card]
            {"BRM_012",0}, //[3/6]Fireguard Destroyer [4 mana] [SHAMAN card]
            {"BRM_014",0}, //[4/4]Core Rager [4 mana] [HUNTER card]
            {"BRM_016",0}, //[2/5]Axe Flinger [4 mana] [WARRIOR card]
            {"BRM_020",3}, //[3/5]Dragonkin Sorcerer [4 mana] [NONE card]
            {"BRM_026",2}, //[5/6]Hungry Dragon [4 mana] [NONE card]
            {"AT_006",3}, //[3/5]Dalaran Aspirant [4 mana] [MAGE card]
            {"AT_011",5}, //[3/5]Holy Champion [4 mana] [PRIEST card]
            {"AT_012",3}, //[5/4]Spawn of Shadows [4 mana] [PRIEST card]
            {"AT_017",1}, //[2/6]Twilight Guardian [4 mana] [NONE card]
            {"AT_019",0}, //[1/1]Dreadsteed [4 mana] [WARLOCK card]
            {"AT_039", 6}, //[5/4]Savage Combatant [4 mana] [DRUID card]
            {"AT_040",0}, //[4/4]Wildwalker [4 mana] [DRUID card]
            {"AT_047",0}, //[4/4]Draenei Totemcarver [4 mana] [SHAMAN card]
            {"AT_067",0}, //[5/3]Magnataur Alpha [4 mana] [WARRIOR card]
            {"AT_076",0}, //[3/4]Murloc Knight [4 mana] [PALADIN card]
            {"AT_085",0}, //[2/6]Maiden of the Lake [4 mana] [NONE card]
            {"AT_091",0}, //[1/8]Tournament Medic [4 mana] [NONE card]
            {"AT_093",0}, //[2/6]Frigid Snobold [4 mana] [NONE card]
            {"AT_108",0}, //[5/3]Armored Warhorse [4 mana] [NONE card]
            {"AT_111",0}, //[3/5]Refreshment Vendor [4 mana] [NONE card]
            {"AT_114",4}, //[5/4]Evil Heckler [4 mana] [NONE card]
            {"AT_121",0}, //[4/4]Crowd Favorite [4 mana] [NONE card]
            {"AT_122",0}, //[4/4]Gormok the Impaler [4 mana] [NONE card]
            /*Whisper*/
            {AllCards.ForbiddenAncient, 0},
            {AllCards.PossessedVillager, 3},
            {AllCards.FieryBat, 3},
            {AllCards.SelflessHero, 1},
            {AllCards.VilefinInquisitor, 4},
            {AllCards.NZothsFirstMate, 3},
            {AllCards.ShifterZerus, 1},
            {AllCards.TentacleofNZoth, 2},
            {AllCards.ZealousInitiate, 1},
            {AllCards.CultSorcerer, 3},
            {AllCards.UndercityHuckster, 2},
            {AllCards.EternalSentinel, 2},
            {AllCards.DarkshireLibrarian, 1},
            {AllCards.BilefinTidehunter, 2},
            {AllCards.Duskboar, 0},
            {AllCards.NattheDarkfisher, -1},
            {AllCards.BeckonerofEvil, 5},
            {AllCards.TwilightGeomancer, 2},
            {AllCards.TwistedWorgen, 3},
            {AllCards.AddledGrizzly, 0},
            {AllCards.TwilightFlamecaller, 2},
            {AllCards.StewardofDarkshire, 2},
            {AllCards.DarkshireCouncilman, 3},
            {AllCards.BloodsailCultist, 3},
            {AllCards.RavagingGhoul, 2},
            {AllCards.AmgamRager, 2},
            {AllCards.DiscipleofCThun, 3},
            {AllCards.SilithidSwarmer, 2},
            {AllCards.SpawnofNZoth, 1},
            {AllCards.SquirmingTentacle, 4},
            {AllCards.TwilightElder, 5},
            {AllCards.FandralStaghelm, 3},
            {AllCards.KlaxxiAmberWeaver, 2},
            {AllCards.MireKeeper, 1},
            {AllCards.InfestedWolf, 1},
            {AllCards.DementedFrostcaller, 2},
            {AllCards.HoodedAcolyte, 1},
            {AllCards.ShiftingShade, 0},
            {AllCards.SouthseaSquidface, 1},
            {AllCards.XarilPoisonedMind, 0},
            {AllCards.FlamewreathedFaceless, 4},
            {AllCards.MasterofEvolution, 4},
            {AllCards.BloodhoofBrave, 2},
            {AllCards.AberrantBerserker, 0},
            {AllCards.BlackwaterPirate, 0},
            {AllCards.CThunsChosen, 0},
            {AllCards.CyclopianHorror, 1},
            {AllCards.EaterofSecrets, 0},
            {AllCards.EvolvedKobold, 0},
            {AllCards.FacelessShambler, 0},
            {AllCards.InfestedTauren, 4},
            {AllCards.MidnightDrake, 0},
            {AllCards.PollutedHoarder, 0},
            {AllCards.TwilightSummoner, 0},
            {AllCards.BladedCultist, 5},
            {AllCards.EnchantedRaven, 5},
            {AllCards.CloakedHuntress, 3},
            {AllCards.KindlyGrandmother, 4},
            {AllCards.BabblingBook, 2},
            {AllCards.MedivhsValet, 2},
            {AllCards.NightbaneTemplar, 4},
            {AllCards.DeadlyFork, 0},
            {AllCards.Swashburglar, 4},
            {AllCards.SilverwareGolem, 1},
            {AllCards.MalchezaarsImp, 3},
            {AllCards.Barnes, 4},
            {AllCards.Moroes, 2},
            {AllCards.VioletIllusionist, 2},
            {AllCards.Zoobot, 1},
            {AllCards.Arcanosmith, 2},
            {AllCards.PantrySpider, 2},
            {AllCards.NetherspiteHistorian, 2},
            {AllCards.PompousThespian, 3},
            {AllCards.ArcaneAnomaly, 4},
            {AllCards.RunicEgg, 2},
            {AllCards.PriestoftheFeast, 5},
            {AllCards.WickedWitchdoctor, 2},

            {AllCards.Alleycat, 4},
            {AllCards.KabalLackey, 2},
            {AllCards.GrimscaleChum, 3},
            {AllCards.MeanstreetMarshal, 1},
            {AllCards.MistressofMixtures, 4},
            {AllCards.PatchesthePirate, -1000},
            {AllCards.SmallTimeBuccaneer, 3},
            {AllCards.WeaselTunneler, 1},
            {AllCards.TroggBeastrager, 3},
            {AllCards.GrimestreetOutfitter, 2},
            {AllCards.ManaGeode, 3},
            {AllCards.GadgetzanFerryman, 1},
            {AllCards.JadeSwarmer, 2},
            {AllCards.HobartGrapplehammer, 3},
            {AllCards.PublicDefender, 1},
            {AllCards.BlowgillSniper, 2},
            {AllCards.DirtyRat, 0},
            {AllCards.FriendlyBartender, 3},
            {AllCards.GadgetzanSocialite, 3},
            {AllCards.GrimestreetInformant, 2},

            {AllCards.CelestialDreamer, 1},
            {AllCards.RatPack, 3},
            {AllCards.ShakyZipgunner, 3},
            {AllCards.ManicSoulcaster, 2},
            {AllCards.WickerflameBurnbristle, 2},
            {AllCards.KabalTalonpriest, 4},
            {AllCards.ShadowRager, 0},
            {AllCards.ShakutheCollector, 1},

            {AllCards.UnlicensedApothecary, 0},
            {AllCards.GrimestreetPawnbroker, 2},
            {AllCards.AuctionmasterBeardo, 3},
            {AllCards.BackstreetLeper, 1},
            {AllCards.BlubberBaron, 2},
            {AllCards.FelOrcSoulfiend, 2},

            {AllCards.GrimestreetSmuggler, 3},
            {AllCards.HiredGun, 2},
            {AllCards.KabalCourier, 2},
            {AllCards.SergeantSally, 3},
            {AllCards.StreetTrickster, 0},
            {AllCards.ToxicSewerOoze, 1},

            {AllCards.DispatchKodo, 1},
            {AllCards.ShadowSensei, 2},
            {AllCards.JinyuWaterspeaker, 4},
            {AllCards.LotusIllusionist, 3},
            {AllCards.Crystalweaver, 3},
            {AllCards.SeadevilStinger, 2},

            {AllCards.GrimyGadgeteer, 1},
            {AllCards.BackroomBouncer, 1},
            {AllCards.DaringReporter, 1},
            {AllCards.GenzotheShark, 1},
            {AllCards.HozenHealer, 2},
            {AllCards.JadeSpirit, 3},
            {AllCards.KabalChemist, 1},
            {AllCards.Kazakus, 1},
            {AllCards.KookyChemist, 1},
            {AllCards.NagaCorsair, 1},
            {AllCards.TanarisHogchopper, 0},
            {AllCards.WorgenGreaser, 0},

        };



    }

}