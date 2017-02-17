using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ACK;
using SmartBot.Database;


namespace SmartBot.Plugins
{
    public static class Extension
    {
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, TValue value)
        {
            map[key] = value;
        }

      
    }
    [Serializable]
    public class Arthurs_Bundle___History : PluginDataContainer
    {
        [DisplayName("Games to Analyze")]
        public int GTA { get; set; }
        [DisplayName("Record Game Time")]
        public bool RGT { get; set; }
        [DisplayName("Print Summary on start")]
        public bool print { get; set; }
        

        public Arthurs_Bundle___History()
        {
            Name = "ACK - History";
            GTA = 50;
            RGT = true;
            print = true;
            Enabled = true;
        }
    }
    public class HistoryDisplay : Plugin
    {
        public override void OnPluginCreated()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACK\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACK\\");
            }
        }
        public override void OnStarted()
        {
            
        }
        public override void OnStopped()
        {
            
        }
        public override void OnVictory()
        {
            SaveGame("Won");
        }

        public override void OnDefeat()
        {
            SaveGame("Lost");
        }
        // $"{0:Time}~{1:Mode}~{2:Coin}~{3:Result}~{4:MyClass}~{5:MyDeck}~{6:MyPlayed}~{7:MyDrawn}~{8:OpponentId}~{9:OpponentClass}~{10:OpponentCards}";
        private void SaveGame(string res)
        {
            bool coin = Bot.CurrentBoard.FriendGraveyard.Contains(Card.Cards.GAME_005) ||
                        Bot.CurrentBoard.HasCardInHand(Card.Cards.GAME_005);
            var _gameCards = MyPlayedCards();
            History.GameResult curGameResult = new History.GameResult(DateTime.UtcNow.ToString(), Bot.CurrentMode().ToString(), coin? "Coin": "NoCoin", res, Bot.CurrentDeck().Class.ToString(), string.Join(",",Bot.CurrentDeck().Cards), 
              _gameCards.Key, _gameCards.Value, Bot.GetCurrentOpponentId().ToString(), Bot.CurrentBoard.EnemyClass.ToString(), GetEnemyCards() );
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACK\\MatchHistory.txt", true))
            {
                sw.WriteLine(curGameResult.ToString());
            }
        }

        private string GetEnemyCards()
        {
            List<Card> board = Bot.CurrentBoard.MinionEnemy;
            List<Card.Cards> graveyard = Bot.CurrentBoard.EnemyGraveyard;
            Card weapon = Bot.CurrentBoard.WeaponEnemy;
            List<string> emyList = board.Select(q => CardTemplate.LoadFromId(q.ToString()).Id.ToString()).ToList();
            emyList.AddRange(graveyard.Where(c => CardTemplate.LoadFromId(c).IsCollectible).Select(q => CardTemplate.LoadFromId(q).Id.ToString()));
            if(weapon != null)
                emyList.Add(weapon.Template.Id.ToString());
            Bot.Log(string.Join("," ,emyList));
            return string.Join(",", emyList);

        }

       
        private KeyValuePair<string, string> MyPlayedCards()
        {
            List<Card.Cards> graveyard = Bot.CurrentBoard.FriendGraveyard.ToList();
            List<Card> board = Bot.CurrentBoard.MinionFriend.ToList();
            List<Card.Cards> secrets = Bot.CurrentBoard.Secret.ToList();
            Card weapon = Bot.CurrentBoard.WeaponFriend;
            List<Card> hand = Bot.CurrentBoard.Hand.ToList();


            List<string> played = new List<string> { };
            played.AddRange(
                graveyard.Where(card => CardTemplate.LoadFromId(card).IsCollectible).Select(q => q.ToString()));
            played.AddRange(board.Where(card => card.Template.IsCollectible).Select(q => q.Template.Id.ToString()));
            played.AddRange(secrets.Where(card => CardTemplate.LoadFromId(card).IsCollectible).Select(q => q.ToString()));
            if (Bot.CurrentBoard.HasWeapon())
                played.Add(weapon.Template.Id.ToString());
            List<string> drawn = new List<string>();
            drawn.AddRange(hand.Where(card => card.Template.IsCollectible).Select(q => q.Template.Id.ToString()));

            string splayed = played.Aggregate("", (current, q) => current + "," + q);
            string sdrawn = drawn.Aggregate("", (current, q) => current + "," + q);
            return new KeyValuePair<string, string>(splayed, sdrawn);
        }
    }


}
    


