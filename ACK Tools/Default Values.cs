using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SmartBot.Database;
using SmartBot.Plugins.API;
using SmartBotProfiles;

namespace ACKTools
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Default_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ProfileParameters parameters = new ProfileParameters(BaseProfile.Default);
            FillListBox(parameters);
        }



        private void Rush_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ProfileParameters parameters = new ProfileParameters(BaseProfile.Rush);
            FillListBox(parameters);
        }

        private void Face_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ProfileParameters parameters = new ProfileParameters(BaseProfile.Face);
            FillListBox(parameters);
        }

        private void FillListBox(ProfileParameters parameters)
        {

            if (richTextBox1.Text == null)
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

            using (StringReader deckReader = new StringReader(richTextBox1.Text))
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
               

            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public static class RandomExtension
    {
        public static CardTemplate Template(this Card.Cards card)
        {
            return CardTemplate.LoadFromId(card);
        }
    }
}