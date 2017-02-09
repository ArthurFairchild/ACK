using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ACK;


namespace ACKTools
{
    public partial class ConvertOldHistory : Form
    {
        public ConvertOldHistory()
        {
            InitializeComponent();
            numGamesTxtBox.Items.Add("Max");
            numGamesTxtBox.Items.Add("20");
            numGamesTxtBox.Items.Add("30");
            numGamesTxtBox.Items.Add("50");
            numGamesTxtBox.Items.Add("100");
            numGamesTxtBox.Items.Add("200");
            numGamesTxtBox.Items.Add("500");
            numGamesTxtBox.Items.Add("1000");

            numGamesTxtBox.SelectedIndex = 0;
            

        }

        private List<string> matchHistoryOldtxt = new List<string>();
        private List<string> deckPerformanceHistoryTxt = new List<string>();
        private string newPath = "";
        private void MatchHistoryPthBtn_Click(object sender, EventArgs e)
        {
           OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (!ofd.FileName.Contains("MatchHistory.txt"))
            {
                MessageBox.Show(@"Please select MatchHistory.txt in ..SmartBot/Logs/ACKTracker/");
                return;
            }
            matchHistoryPath.Text = ofd.FileName;
            newPath = ofd.FileName;
            matchHistoryOldtxt = File.ReadLines(matchHistoryPath.Text).Reverse().ToList();
            int numLines = matchHistoryOldtxt.Count;
            matchHistoryCount.Text = numLines.ToString();
        }

        private void DeckPerformanceHistoryBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.FileName.Contains("DeckPerformanceHistory"))
                {
                    MessageBox.Show(@"Please select DeckPerformanceHistory.txt in ..SmartBot/Logs/ACKTracker/");
                    return;
                }
                deckPerformancePath.Text = ofd.FileName;
                deckPerformanceHistoryTxt = File.ReadLines(deckPerformancePath.Text).Reverse().ToList();
                int numLines = deckPerformanceHistoryTxt.Count;
                deckPerformanceHistoryCount.Text = numLines.ToString();
            }
        }

        private void MergeBtn_Click(object sender, EventArgs e)
        {
            int numGames = 0;
            try
            {
                numGames = Int32.Parse(numGamesTxtBox.SelectedItem.ToString());
            }
            catch (FormatException)
            {
                numGames =
                    new List<int> {Int32.Parse(deckPerformanceHistoryCount.Text), Int32.Parse(matchHistoryCount.Text)}
                        .Min();
            }
            using (StreamWriter matchHistryNew = new StreamWriter(newPath, false))
            {
                int count = 0;
                foreach (var q in matchHistoryOldtxt.Take(numGames))
                {
                    var get = new History.GameResult(q, deckPerformanceHistoryTxt[count]);
                    count++;
                    matchHistryNew.WriteLine(get.ToString());
                    
                }
            }
            MessageBox.Show(@"Succesfully Generated new MatchHistory.txt");
            this.Close();
            Application.OpenForms[0].AutoSize = true;
        }

        
    }
}
