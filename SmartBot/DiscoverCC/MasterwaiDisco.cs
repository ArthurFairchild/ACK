using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using SmartBot.Database;
using SmartBot.Discover;
using SmartBot.Plugins.API;

namespace Discover
{
    internal class MasterwaiDisco : DiscoverPickHandler
    {
        private List<Card.Cards> _playableMinions = new List<Card.Cards>
        {
            Cards.PitFighter
            //Insert playable minions.
        };


        private string _log = "\r\n[MasterwaiDisco]"; 

        private delegate int EvalMetod(Board board);

        private static readonly Tierlist TierList = Tierlist.FromJason(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "DiscoverCC\\ParsedTierlist.json"));

        //Dictionary for custom evaluation methods
        private static readonly Dictionary<Card.Cards, EvalMetod> CardValues = new Dictionary<Card.Cards, EvalMetod>
        {
            {Cards.DefenderofArgus, DefenderofArgusValue}
        };
        
        

        public Card.Cards HandlePickDecision(Card.Cards originCard, List<Card.Cards> choices, Board board)
        {
            //Logic for special discovers
            if (originCard == Cards.ArchThiefRafaam)
            {
                Card.Cards mirrorOfDoom = Card.Cards.LOEA16_5;
                //Card.Cards timepieceOfHorror = Card.Cards.LOEA16_4;
                //Card.Cards lanternOfPower = Card.Cards.LOEA16_3;

                //Insert Rafaam logic
                AddLog("RAAAAAAAFAAAAAAAAAMMMMMMMMMMM THE SUPREMEEE ARCHEOLOGIST");
                PrintLog();
                return mirrorOfDoom;
            }
            if (originCard == Cards.Kazakus)
            {
                //Not sure how this works

                AddLog("KAZAMMMAAKUSSS CLOWNFIESTA");
                PrintLog();
                //Always pick middle -> 5 mana
                return choices[1];
            }

            //Try discover logic for usual cards, if it fails pick left card and print error to log
            try
            {
                var choice = choices.OrderByDescending(x => GetValue(x, board)).First();
                PrintLog();
                return choice;
            }
            catch (Exception e)
            {
                _log = "\r\n[-MasterwaiDisco-]";
                Bot.Log("Error in discover");
                Bot.Log(e.ToString());
                return choices[0];
            }
        }

        private int GetValue(Card.Cards card, Board board)
        {
            //if (Lethal(card, board)) return 1000; Not implemented

        
            int value; //Values saved in this var for debug printouts
            AddLog(" -- Name: " + CardTemplate.LoadFromId(card).Name);

            //Use custom eval method if there is one
            if (CardValues.ContainsKey(card))
            {
                value = CardValues[card](board);
                AddLog("Eval Value: " + value);
            }
            else
            {
                //Ask for value of card in our class for neutral cards and class cards of your class. Ask for value of the cards class if it's a class card from another class
                var cclass = CardTemplate.LoadFromId(card).Class == Card.CClass.NONE ? board.FriendClass : CardTemplate.LoadFromId(card).Class;

                value = TierList.GetCardValue(card, cclass);
                AddLog("List Value: " + value);
                AddLog("List Class: " + cclass);
            }

            //if (_playableMinions.Contains(card) && LowOnMinions(board)) return value + 20; Not implemented
            return value;
        }

        #region Custom evaluation methods

        private static int DefenderofArgusValue(Board board)
        {
            var baseVal = TierList.GetCardValue(Cards.DefenderofArgus, board.FriendClass);

            switch (board.MinionFriend.Count)
            {
                case 0:
                    return (int)(baseVal * 0.8);
                case 1:
                    return baseVal;
                default:
                    return (int)(baseVal * 1.2);
            }
        }


        //private bool Lethal(Card.Cards card, Board board)
        //{
        //    //Determine if card gives lethal
        //    return false;
        //}

        #endregion


        //Add line to logstring for current discover
        private void AddLog(string s)
        {
            _log += "\r\n" + s;
        }

        //Print log for current discover and reset string to header
        private void PrintLog()
        {
            Bot.Log(_log);
            _log = "\r\n[-MasterwaiDisco-]";
        }
    }

    [DataContract]
    public class Tierlist
    {
        [DataMember]
        public List<ArenaCardScore> Cards;

        //Load from json file
        public static Tierlist FromJason(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof (Tierlist));

            using (Stream stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    return (Tierlist)jsonSerializer.ReadObject(stream);
                }
            }
        }

        //Get value of certain card in certain class
        public int GetCardValue(Card.Cards card, Card.CClass cclass)
        {
            //If there is no specific value for the requested class, use neutral value.
            try
            {
                if ((int)Cards.Find(x => x.Id == card).Scores[cclass] == 0)
                    return (int)Cards.Find(x => x.Id == card).Scores[Card.CClass.NONE];
                else return (int)Cards.Find(x => x.Id == card).Scores[cclass];
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                
            }
            return 0;
        }
    }

    [DataContract]
    public class ArenaCardScore
    {
        [DataMember]
        public Card.Cards Id;
        [DataMember]
        public Dictionary<Card.CClass, double> Scores = new Dictionary<Card.CClass, double>
        {
            {Card.CClass.DRUID, 0},
            {Card.CClass.HUNTER, 0},
            {Card.CClass.MAGE, 0},
            {Card.CClass.PALADIN, 0},
            {Card.CClass.PRIEST, 0},
            {Card.CClass.ROGUE, 0},
            {Card.CClass.WARLOCK, 0},
            {Card.CClass.WARRIOR, 0},
            {Card.CClass.SHAMAN, 0},
            {Card.CClass.NONE, 0}
        };
    }
}