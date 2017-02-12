using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACK;
using ACKTools.Properties;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace ACKTools
{
    public partial class HistoryDebugger : Form
    {
        private string _smartBotPath = "";
        
        public HistoryDebugger()
        {
            
            InitializeComponent();
        }
        public HistoryDebugger(string smartBotPath)
        {
            this._smartBotPath = smartBotPath;
           
            InitializeComponent();
            wcd = _smartBotPath + "\\Logs\\ACK\\";
            wrongClassificationPath.Text = wcd+ "WronglyClassifiedDecks.txt";
            matchHistoryPath2.Text = wcd + "MatchHistory.txt";
            if (!File.Exists(matchHistoryPath2.Text))
            {
                var cohf = new ConvertOldHistory();
                cohf.Show();
            }
            FillGameHistoryList(_smartBotPath + "\\Logs\\ACK\\MatchHistory.txt",
                        wcd + "\\WronglyClassifiedDecks.txt");
            
        }


        private void HistoryDebugger_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }
        public static string wcd = "";
       

        private void FillGameHistoryList(string matchHistoryPath, string wronglyIdentifiedPath)
        {
            MessageBox.Show(matchHistoryPath);
            List<string> history = File.ReadLines(matchHistoryPath).Reverse().Take(50).ToList();
            List<string> AllDecks = history.Select(q => q.Split('~')).Select(deck => deck[10]).ToList();

            int index = 0;


            foreach (var deck in AllDecks)
            {
                
                List<Card.Cards> CardsDeck = ParseHistoryString(deck);
                //MessageBox.Show(string.Join(",", CardsDeck));
                
                DeckClassification nextClassification = 
                    new DeckClassification(CardsDeck.Select(c=> c.Template().Id.ToString()).ToList());
                string list = nextClassification.DeckList.Distinct()
                    .Where(q => CardTemplate.LoadFromId(q).IsCollectible).Aggregate("", (current, q) => current + (q.ToString() + ":" + nextClassification.DeckList.Count(c => c == q) + ";"));

                //HistoryListBox.Items.Add(list);
                GameHistory.Items.Add(list);

                index++;
            }
            
           
        }

        private List<Card.Cards> ParseHistoryString(string deck)
        {
            //MessageBox.Show(deck);
            var omg = deck.Split(',').ToList();
            foreach (var q in omg)
            {
                try
                {
                    var card = (Card.Cards) Enum.Parse(typeof(Card.Cards), q);
                }
                catch (Exception)
                {
                    omg.Remove(q);
                    break;
                }
            }
            return omg.Select(q => (Card.Cards)Enum.Parse(typeof(Card.Cards), q)).Where(w=> w.Template().IsCollectible).ToList();
        }

        private void GameHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeckTrackerRichBox.Text = GameHistory.SelectedItem.ToString();
            ClassifyEvent();
        }
        public static DeckClassification DeckIdentity;

        public void ClassifyEvent()
        {
            if (DeckTrackerRichBox.Text.Length > 1)
            {
                HearthpwnRichTextBox.Text = "";
                using (StringReader deckReader = new StringReader(DeckTrackerRichBox.Text))
                {
                    string line = deckReader.ReadLine();
                    List<Card.Cards> deck = new List<Card.Cards>();

                    if (line != null)
                    {
                        var data = line.Split(';');
                        foreach (var q in data)
                        {
                            if (q.Length < 1) break;
                            var card = q.Split(':');

                            if (int.Parse(card[1]) == 2)
                                deck.Add((Card.Cards)Enum.Parse(typeof(Card.Cards), card[0]));
                            deck.Add((Card.Cards)Enum.Parse(typeof(Card.Cards), card[0]));
                        }
                        foreach (var q in deck.Distinct())
                        {
                            HearthpwnRichTextBox.Text += deck.Count(c => c == q) + @" " + CardTemplate.LoadFromId(q).Name + "\n";
                        }
                    }
                    DeckIdentity = new DeckClassification(deck.Select(c=> c.ToString()).ToList());
                    VariablesRichTextBox.Text = DeckIdentity.ClassificationSummary();
                    ClassificationTextBox.Text = DeckIdentity.Name;
                    SimpleClassifyField.Text = ClassificationTextBox.Text;


                }
            }
            else
            {
                //rTB.Text = "No deck in RB1";
                if (HearthpwnRichTextBox.Text == null)
                {
                    //rTB2.Text = "No Text in RB2";
                    return;
                }
                var library = new Dictionary<string, Card.Cards>();
                var allCards = CardTemplate.TemplateList.Values;
                foreach (var q in allCards)
                {
                    library[q.Name] = q.Id;
                }

                using (StringReader deckReader = new StringReader(HearthpwnRichTextBox.Text))
                {
                    //Regex rgx = new Regex(@"[^A-Za-z0-9]+");
                    List<Card.Cards> deck = new List<Card.Cards>();
                    string line;
                    while ((line = deckReader.ReadLine()) != null)
                    {

                        //var name = line.Split('=');
                        //string temp = rgx.Replace(name[0], "");
                        //newStr = newStr + "{Cards." + temp + ", 0 },\n";
                        if (line == string.Empty) break;
                        if (line.Substring(0, 1) == "2")
                            deck.Add(library[line.Substring(2)]);
                        deck.Add(library[line.Substring(2)]);
                    }
                    foreach (var q in deck.Distinct())
                    {
                        DeckTrackerRichBox.Text += string.Format(Resources.Form1_button1_Click__0___1__, q, deck.Count(c => c == q));
                    }
                    DeckIdentity = new DeckClassification(deck.Select(c=> c.ToString()).ToList());
                    VariablesRichTextBox.Text = DeckIdentity.ClassificationSummary();
                    ClassificationTextBox.Text = DeckIdentity.Name;
                    SimpleClassifyField.Text = ClassificationTextBox.Text;


                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Detailed Correct
        private void button5_Click(object sender, EventArgs e)
        {
            var location = wcd.Length < 1
               ? AppDomain.CurrentDomain.BaseDirectory
               : wcd;
            if(GameHistory.GetSelected(GameHistory.SelectedIndex))
                GameHistory.SetItemChecked(GameHistory.SelectedIndex,false);
            
            
            
            using (StreamWriter sw = new StreamWriter(location + "\\Report.txt", true))
            {
                sw.WriteLine(DeckTrackerRichBox.Text + "\n\nCorrectly Classified as " + DeckIdentity.Name + "\n");
            }
            DeckTrackerRichBox.Text = "";
            HearthpwnRichTextBox.Text = "";
            GameHistory.SelectedIndex++;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        //Page 1 Incorrect Button
        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(wcd+ "ReviewedDecks.txt", true))
            {
                sw.WriteLine(DeckTrackerRichBox.Text);
            }
            DeckTrackerRichBox.Text = "";
            HearthpwnRichTextBox.Text = "";
            GameHistory.SetItemChecked(GameHistory.SelectedIndex, true);
            GameHistory.SelectedIndex++;
        }
        //Page 2 Incorrect Button
        private void button6_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }


    }
    public static class BSExtension
    {
        public static CardTemplate Template(this Card.Cards card)
        {
            return CardTemplate.LoadFromId(card);
        }
    }
}
