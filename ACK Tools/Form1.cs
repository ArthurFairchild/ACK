using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SmartBot.Database;
using SmartBot.Plugins.API;
using System.Diagnostics;
using ACK;
using ACKTools.Properties;


namespace ACKTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassifyEvent();


        }

        public static DeckClassification DeckIdentity;
        private void button2_Click(object sender, EventArgs e)
        {
            if (RecomendedName.Text.Length < 2)
            {
                MessageBox.Show("You need to enter recommended deck name");
                return;
            }
            var longReport = DeckTrackerRichBox.Text + "\nIdentified as " + DeckIdentity.Name +
                              " Should Identify as " + RecomendedName.Text + "\n" +
                              DeckIdentity.ToString() + @"\n\n[Notes] " + NotesAboutTheDeck.Text;
            var shortReport = DeckTrackerRichBox.Text + "\nIdentified as " + DeckIdentity.Name +
                        " Should Identify as " + RecomendedName.Text + "\n" + NotesAboutTheDeck.Text;
            var location = Wcd.Length < 1
                ? AppDomain.CurrentDomain.BaseDirectory
                : Wcd;
            using (var sw = new StreamWriter(location + "\\Report.txt", true))
            {
                if (SaveVariablesBox.Checked)
                    if (SaveToFileBox.Checked)
                        sw.WriteLine(longReport);
                    else ReportRichTextBox.Text = longReport;
                else
                    if (SaveToFileBox.Checked)
                    sw.WriteLine(shortReport);
                else ReportRichTextBox.Text = shortReport;
            }
            DeckTrackerRichBox.Text = "";
            HearthpwnRichTextBox.Text = "";
            RecomendedName.Text = "";
            
            GameHistory.SetItemChecked(GameHistory.SelectedIndex, true);
            GameHistory.SelectedIndex++;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_SelectedChanged(object sender, EventArgs e)
        {
            
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/NEuntK6");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeckTrackerRichBox.Text = "";
            HearthpwnRichTextBox.Text = "";
            ReportRichTextBox.Text = "";
            RecomendedName.Text = "";
            NotesAboutTheDeck.Text = "";
            MulliganCoreRichTextBox.Text = "";
            VariablesRichTextBox.Text = "";
            GameHistory.Items.Clear();
        }
        public static List<string> Reports = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Correct Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (DeckTrackerRichBox.Text.Length < 2 || HearthpwnRichTextBox.Text.Length < 2)
            {
                MessageBox.Show(@"What Deck?");
                return;
            }
            if (!SaveToFileBox.Checked) return;
            SaveVariablesBox.Checked = false;
            var location = Wcd.Length < 1
                ? AppDomain.CurrentDomain.BaseDirectory
                : Wcd;
            using (var sw = new StreamWriter(location + "\\Report.txt", true))
            {
                if (SaveVariablesBox.Checked)
                    sw.WriteLine(DeckTrackerRichBox.Text + "\n\nCorrectly Classified " + DeckIdentity.Name + "\n\n" +
                    DeckIdentity.ToString() + "\n\n[Notes] " + NotesAboutTheDeck.Text);
                else sw.WriteLine(DeckTrackerRichBox.Text + "\n\nCorrectly Classified as " + DeckIdentity.Name + "\n\n" + NotesAboutTheDeck.Text);
            }
            DeckTrackerRichBox.Text = "";
            HearthpwnRichTextBox.Text = "";
            RecomendedName.Text = "";
            GameHistory.SelectedIndex++;
        }

        private void SaveBox_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = SaveToFileBox.Checked ? AppDomain.CurrentDomain.BaseDirectory : "";
            if (SaveToFileBox.Checked)
            {
                ReportRichTextBox.Text =
                    $"Upload your report file from \n\n{AppDomain.CurrentDomain.BaseDirectory}\n\n to Discord Channel.";
                ReportRichTextBox.Enabled = true;
                ReportRichTextBox.ReadOnly = true;


            }
            else ReportRichTextBox.Text = "";
        }


        public static string Wcd = "";
        private void button2_Click_1(object sender, EventArgs e)
        {
           
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(fbd.SelectedPath+"\\MatchHistory.txt"))
                {
                    MessageBox.Show("You need to select MatchHistory.txt folder. \nDefault: SmartBot/Logs/ACKTracker/ ");
                    return;
                }
            }
            Wcd = fbd.SelectedPath;
            var history = File.ReadLines(fbd.SelectedPath+"\\MatchHistory.txt").Reverse().ToList();

            
            var allDecks = history.Select(q => q.Split(new[] {"||"}, StringSplitOptions.None)).Select(deck => deck[10].Substring(1)).ToList();
            
            var index = 0;

            
            foreach (var deck in allDecks)
            {
                var cardsDeck = ParseHistoryString(deck);
                var nextClassification = new DeckClassification(cardsDeck.Select(c=> c.ToString()).ToList());
                var list = "";
                foreach (var q in nextClassification.DeckList.Distinct())
                {
                    
                    list += q.ToString() + ":" + nextClassification.DeckList.Count(c => c == q) + ";";

                }

                //HistoryListBox.Items.Add(list);
                GameHistory.Items.Add(list);
                
                index++;
            }
            if (!File.Exists(Wcd + "\\WronglyClassifiedDecks.txt")) return;
            var reportedMisqualifications = File.ReadLines(Wcd + "\\WronglyClassifiedDecks.txt").ToList();
            foreach (var q in reportedMisqualifications)
            {
                var indexK = 0;
                foreach (var w in GameHistory.Items)
                {
                    if (w.ToString() == q)
                    {
                        GameHistory.SetItemChecked(indexK, true);
                        
                        break;
                    }
                    indexK++;
                }
            }


        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private List<Card.Cards> ParseHistoryString(string deck)
        {
            
            return deck.Split(',').Select(q => (Card.Cards)Enum.Parse(typeof(Card.Cards), q)).ToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DeckTrackerRichBox.Text = GameHistory.SelectedItem.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public static string DetailedSaveLocation = "";
        private void button5_Click(object sender, EventArgs e)
        {
            using (
                var sw =
                    new StreamWriter(Wcd + "\\WronglyClassifiedDecks.txt", false))
            {
                foreach (var q in GameHistory.CheckedItems)
                {
                    sw.WriteLine($"\n\n{q.ToString()}");
                }
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void GameHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeckTrackerRichBox.Text = GameHistory.SelectedItem.ToString();
            ClassifyEvent();
        }

        public void ClassifyEvent()
        {
            if (DeckTrackerRichBox.Text.Length > 1)
            {
                HearthpwnRichTextBox.Text = "";
                using (var deckReader = new StringReader(DeckTrackerRichBox.Text))
                {
                    var line = deckReader.ReadLine();
                    var deck = new List<Card.Cards>();

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
                    MulliganCoreRichTextBox.Text = DeckIdentity.MulliganCoreSummary();
                    ReportRichTextBox.Text = CardTemplate.LoadFromId(Cards.AzureDrake).Cost.ToString();
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

                using (var deckReader = new StringReader(HearthpwnRichTextBox.Text))
                {
                    //Regex rgx = new Regex(@"[^A-Za-z0-9]+");
                    var deck = new List<Card.Cards>();
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
                    MulliganCoreRichTextBox.Text = DeckIdentity.MulliganCoreSummary();
                }
            }
        }

        private void NotesAboutTheDeck_TextChanged(object sender, EventArgs e)
        {

        }
    }

    

}

