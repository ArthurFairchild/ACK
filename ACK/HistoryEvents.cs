using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ACK
{
    public class History
    {
        public int NumGames { get; set; }
        public List<GameResult> GamesList { get; set; }

        public double CumulativeWinrate { get; set; }



        public History(int numberOfGames, string historyPath)
        {
            GamesList = GetAllGamesResults(historyPath, numberOfGames);
            CumulativeWinrate = GetCumulativeWinrate();
        }

        private double GetCumulativeWinrate()
        {
            int won = GamesList.Count(q => q.Result == "Won");
            return (double)won / (double)GamesList.Count;
        }

        private List<GameResult> GetAllGamesResults(string path, int numGames = 0)
        {
            int numberToTake = File.ReadAllLines(path).ToList().Count;
            NumGames = numGames >= numberToTake ? numberToTake : numGames;
            List<string> history = File.ReadAllLines(path).Reverse().Take(NumGames).ToList();
            foreach (var q in history)
            {
                GamesList.Add(new GameResult(q));
            }

            return GamesList;
        }

        public class GameResult
        {
            public string Time { get; set; } //0
            public string Mode { get; set; } //1
            public string Coin { get; set; } //2
            public string Result { get; set; } //3
            public string MyClass { get; set; } //4
            public string MyDeck { get; set; } //5
            public string MyPlayed { get; set; } //6
            public string MyDrawn { get; set; } //7
            public string OpponentId { get; set; } //8
            public string OpponentClass { get; set; } //9
            public string OpponentCards { get; set; } //10

            /// <summary>
            /// Manual Constructor
            /// </summary>
            /// <param name="time">time in UTC format</param>
            /// <param name="mode">mode</param>
            /// <param name="coin">HadCoin</param>
            /// <param name="result">Resutl {Won/Lost}</param>
            /// <param name="myClass">My Class</param>
            /// <param name="myDeck"> My Deck</param>
            /// <param name="myPlayed">My Played</param>
            /// <param name="myDrawn">My Drawn</param>
            /// <param name="opponentId">Opponent played</param>
            /// <param name="opponentClass">Opponent Class</param>
            /// <param name="opponentCards">Opponent Cards</param>
            public GameResult(string time, string mode, string coin, string result, string myClass, string myDeck,
                string myPlayed, string myDrawn, string opponentId, string opponentClass, string opponentCards)
            {
                Time = time;
                Mode = mode;
                Coin = coin;
                Result = result;
                MyClass = myClass;
                MyDeck = myDeck;
                MyPlayed = myPlayed;
                MyDrawn = myDrawn;
                OpponentId = opponentId;
                OpponentClass = opponentClass;
                OpponentCards = opponentCards;
            }

            public GameResult(string matchHistory, string deckPerformanceHistory)
            {
                var mhValues = matchHistory.Split(new[] { "||" }, StringSplitOptions.None);
                var dPh = deckPerformanceHistory.Split('~');
                Time = mhValues[0];
                Mode = mhValues[3];
                Coin = "NoCoin"; //Unfortunately Coin is not in IsCollectable(), it was never saved.
                Result = mhValues[1];
                MyClass = mhValues[4];
                MyDeck = mhValues[11].ToCharArray()[0] == ',' ? mhValues[11].Substring(1) : mhValues[11]; //idiot fix
                MyPlayed = dPh[5].ToCharArray()[0] == ',' ? dPh[5].Substring(1) : dPh[5];
                MyDrawn = dPh[4].ToCharArray()[0] == ',' ? dPh[4].Substring(1) : dPh[4]; ;
                OpponentId = mhValues[2];
                OpponentClass = mhValues[7];
                OpponentCards = mhValues[10].ToCharArray()[0] == ',' ? mhValues[10].Substring(1) : mhValues[10]; ;
            }

            public override string ToString()
            {
                //       0      1     2        3         4       5         6         7           8              9              10
                return
                    $"{Time}~{Mode}~{Coin}~{Result}~{MyClass}~{MyDeck}~{MyPlayed}~{MyDrawn}~{OpponentId}~{OpponentClass}~{OpponentCards}";
            }

            public GameResult(string newMatchHistoryString)
            {
                var mHs = newMatchHistoryString.Split('~');
                Time = mHs[0];
                Mode = mHs[1];
                Coin = mHs[2];
                Result = mHs[3];
                MyClass = mHs[4];
                MyDeck = mHs[5];
                MyPlayed = mHs[6];
                MyDrawn = mHs[7];
                OpponentId = mHs[8];
                OpponentClass = mHs[9];
                OpponentCards = mHs[10];
            }

            public List<string> GetList()
            {
                return MyDeck.Split(',').ToList();
               
            }
        }
    }
}

