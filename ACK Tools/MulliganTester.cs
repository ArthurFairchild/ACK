using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ACK;
using SmartBot.Database;
using SmartBot.Mulligan;
using SmartBot.Plugins.API;

namespace ACKTools
{
    public partial class MulliganTester : Form
    {
        public string pathToSB = "";
        public MulliganTester(string sbPath, Dictionary<string, DeckClassification> db = null)
        {
            InitializeComponent();
            pathToSB = sbPath;
            GenerateMulligan();
            CardPool = CardTemplate.TemplateList.Keys.Select(c => c).Where(w => CardTemplate.LoadFromId(w).IsCollectible).ToList();
            ClassPool = new List<Card.CClass>
            {
                Card.CClass.SHAMAN, Card.CClass.MAGE, Card.CClass.PRIEST,
                Card.CClass.HUNTER, Card.CClass.PALADIN, Card.CClass.WARRIOR,
                Card.CClass.WARLOCK, Card.CClass.ROGUE, Card.CClass.DRUID
            };
            foreach (var q in ClassPool)
            {
                myClass.Items.Add(q);
                EnemyClass.Items.Add(q);
            }
            myClass.SelectedIndex = 0;
            EnemyClass.SelectedIndex = 0;

            foreach (var q in CardPool)
            {

                cardOffer1.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer2.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer3.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer4.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");

            }
            myStyleBox.Items.AddRange(new object[] { ACK.DeckClassification.Style.Aggro, ACK.DeckClassification.Style.Midrange, ACK.DeckClassification.Style.Control });
            enemyStyleBox.Items.AddRange(new object[] { ACK.DeckClassification.Style.Aggro, ACK.DeckClassification.Style.Midrange, ACK.DeckClassification.Style.Control });
            modeBox.Items.AddRange(new object[] { Bot.Mode.Arena, Bot.Mode.RankedStandard, Bot.Mode.RankedWild});
            myStyleBox.SelectedIndex = 0;
            enemyStyleBox.SelectedIndex = 0;
            modeBox.SelectedIndex = 0;
            CheckCriticalFiles();

            oneDropCount.Value = 1;
            oneDropCoin.Value = 2;

            twoDropCount.Value = 2;
            TwoDropCoin.Value = 2;

            threeDrop.Value = 1;
            threeDropCoin.Value = 2;

            fourDrop.Value = 1;
            fourDropCoin.Value = 2;

            using (StreamReader sr = new StreamReader(pathToSB + "\\Logs\\ACK\\MatchHistory.txt"))
            {
                var line = sr.ReadLine().Split('~');
                myDeckText.Text = line[5];
            }
            deckSimChkbx.Checked = false;
            deckId.Text = "Arena/Autofill";
            _ourPlayedDecks = db;
            FillPlayedDecks();

        }
        public void DebugLog(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(DebugLog), new object[] { value });
                return;
            }
            debugFlowRchTxtBx.Text += $"\n{value}";
        }



        private void FillPlayedDecks()
        {
            foreach (var q in _ourPlayedDecks)
            {
                ourPlayedDeckLB.Items.Add(q.Key);
            }
        }


        private void CheckCriticalFiles()
        {

            if (!File.Exists(pathToSB + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
            {
                using (StreamWriter sw = new StreamWriter(pathToSB + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
                {
                    sw.WriteLine("Midrange~Midrange~1~2~2~2~1~2~0~1~false~false~false~true~Ranked");
                }
            }
            if (!File.Exists(pathToSB + "\\MulliganProfiles\\ACK.ini"))
            {
                File.Create(pathToSB + "\\MulliganProfiles\\ACK.ini");
            }
        }

        private readonly List<Card.Cards> CardPool;
        private List<Card.CClass> ClassPool;
        private bool card1chosen;
        private bool card2chosen;
        private bool card3chosen;
        private bool card4chosen;

        private void mulliganButton_Click(object sender, EventArgs e)
        {
            //applySettingsBtn_Click(sender, e);
            if (!coinBox.Checked && cardOffer4.Text != "")
                cardOffer4.SelectedItem = null;

            List<Card.Cards> myChoices = new List<Card.Cards>();
            Card.CClass myClass = Card.CClass.SHAMAN;
            Card.CClass enemyClass = Card.CClass.DRUID;
            card1chosen = cardOffer1.SelectedItem != null;
            card2chosen = cardOffer2.SelectedItem != null;
            card3chosen = cardOffer3.SelectedItem != null;
            if (!card3chosen || !card1chosen || !card2chosen) return;
            card4chosen = cardOffer4.SelectedItem != null;

            var card1 = cardOffer1.SelectedItem.ToString().Split('~')[1];
            var card2 = cardOffer2.SelectedItem.ToString().Split('~')[1];
            var card3 = cardOffer3.SelectedItem.ToString().Split('~')[1];
            var card4 = card4chosen ? cardOffer4.SelectedItem.ToString().Split('~')[1] : Card.Cards.GAME_005.ToString();

            myChoices.Add((Card.Cards)Enum.Parse(typeof(Card.Cards), card1));
            myChoices.Add((Card.Cards)Enum.Parse(typeof(Card.Cards), card2));
            myChoices.Add((Card.Cards)Enum.Parse(typeof(Card.Cards), card3));
            if (card4chosen)
                myChoices.Add((Card.Cards)Enum.Parse(typeof(Card.Cards), card4));
            myClass = (Card.CClass)Enum.Parse(typeof(Card.CClass), this.myClass.SelectedItem.ToString());
            enemyClass = (Card.CClass)Enum.Parse(typeof(Card.CClass), EnemyClass.SelectedItem.ToString());
            DebugLog("Choices, eClass, mClass setup");

            var test = SimulateMulligan(myClass, enemyClass, myChoices);
            richTextBox1.Text += "Kept\n";
            foreach (var q in test.OrderBy(c => c.Template().Cost))
            {
                if (!card4chosen && q == Card.Cards.GAME_005) continue;
                richTextBox1.Text += $"[{q.Template().Cost} mana] [{q.Template().Atk}/{q.Template().Health}] {q.Template().Name}\n";
            }
            richTextBox1.Text += "\nTossed\n";
            var tossedList = myChoices.Except(test);
            foreach (var q in tossedList)
            {
                richTextBox1.Text += $"[{q.Template().Cost} mana] [{q.Template().Atk}/{q.Template().Health}] {q.Template().Name}\n";

            }


        }
        private void button2_Click(object sender, EventArgs e)
        {
            DebugLog("Random Cards Clicked");
            Random rt = new Random();
            DebugLog("Random Mulligan Clicked");
            if (randomEnemy.Checked)
                EnemyClass.SelectedIndex = rt.Next(0, EnemyClass.Items.Count);
            bool highlander = renoAssumptionChckbx.Checked;

            var ListIndexes = cardOffer1.Items;

            int card1 = rt.Next(1, ListIndexes.Count);
            cardOffer1.SelectedIndex = card1;
            if (highlander) ListIndexes.RemoveAt(card1);

            int card2 = rt.Next(1, ListIndexes.Count);
            cardOffer2.SelectedIndex = card2;
            if (highlander) ListIndexes.RemoveAt(card2);

            int card3 = rt.Next(1, ListIndexes.Count);
            cardOffer3.SelectedIndex = card3;
            if (highlander) ListIndexes.RemoveAt(card3);

            int card4 = 0;
            if (!coinBox.Checked)
            {
                cardOffer4.SelectedItem = null;
                mulliganButton_Click(sender, e);
                return;
            }
            card4 = rt.Next(0, ListIndexes.Count);
            if (highlander) ListIndexes.RemoveAt(card4);
            cardOffer4.SelectedIndex = card4;
            DebugLog("Random Cards Selected");

            if (randomEnemyStyle.Checked)
                enemyStyleBox.SelectedIndex = rt.Next(0, enemyStyleBox.Items.Count);
            DebugLog("Enemy Style Chosen");
            DebugLog("Entering Mulligan Process");

            mulliganButton_Click(sender, e);
        }
        public List<Card.Cards> CardsToKeep = new List<Card.Cards>();

        /// <summary>
        /// Pain in the ass....
        /// </summary>
        /// <param name="ownHero">Our Hero</param>
        /// <param name="enemyHero">Enemy Hero</param>
        /// <param name="choices">Our Choices</param>
        /// <returns></returns>
        public List<Card.Cards> SimulateMulligan(Card.CClass ownHero, Card.CClass enemyHero, List<Card.Cards> choices)
        {
            richTextBox1.Text = "";
            if (choices.Count < 3)
                return new List<Card.Cards> { Card.Cards.GAME_005 }; //We are coin haters. 
            DebugLog("Entered Simulation");
            DebugLog("Starting Simulation Process");
            CardsToKeep = ACKMulligan.HandleMulligan(choices, enemyHero, ownHero);
            DebugLog("Finished Simulation Process");
            DebugLog("Finished Simulation");
            return CardsToKeep;

        }




        Dictionary<string, DeckClassification> _ourPlayedDecks = new Dictionary<string, DeckClassification>();
        public MulliganProfile ACKMulligan = null;

        public void GenerateMulligan()
        {
            string path = pathToSB + "\\MulliganProfiles\\ACK - Mulligan.cs";
            DebugLog($"Path to {path} is set");
            List<string> stringList = new List<string>();
            try
            {
                stringList.Add(File.ReadAllText(path));
                DebugLog("Read ACK - Mulliga.cs");
                using (CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    DebugLog("Starting Compilation Process");
                    CompilerParameters options = new CompilerParameters
                    {
                        GenerateInMemory = true,


                    };
                    DebugLog("Options Created");
                    options.ReferencedAssemblies.AddRange(new[]
                    {
                        "System.Core.dll",
                        $"{pathToSB}\\SmartBotUI.exe",
                        "SmartBotAPI.dll",
                        "System.Windows.Forms.dll",
                        "ACK.dll"

                    });
                    DebugLog("Libs Loaded");

                    CompilerResults compilerResults = provider.CompileAssemblyFromSource(options, stringList.ToArray());
                    DebugLog("Compiler Results Aquired");
                    if (compilerResults.Errors.Count != 0)
                    {
                        string str = $"{compilerResults.Errors.Count} Errors:";
                        for (int index = 0; index < compilerResults.Errors.Count; ++index)
                            str = str + "\r\nLine: " + compilerResults.Errors[index].Line + " - " +
                                  compilerResults.Errors[index].ErrorText;
                        MessageBox.Show(str + "\r\n\r\n", "Compilation Error");
                        return;
                    }
                    DebugLog("No Compelation Errors");
                    Assembly compiledAssembly = compilerResults.CompiledAssembly;
                    DebugLog("Assembly Compiled Errors");


                    foreach (Type type in compiledAssembly.GetTypes())
                    {
                        if (type.GetInterfaces().Contains(typeof(MulliganProfile)))
                            ACKMulligan = (MulliganProfile)Activator.CreateInstance(type);


                    }
                    DebugLog("ACK - Mulligan simulator created");

                    if (ACKMulligan != null) return;
                    MessageBox.Show("Mulligan class not found!");
                    return;
                }
            }
            catch (Exception e)
            {
                DebugLog(e.Message);
                ACKMulligan = null;
                return;
            }

        }
        private void modeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void applySettingsBtn_Click(object sender, EventArgs e)
        {
            List<Card.Cards> deck = new List<Card.Cards>();

            using (StringReader deckReader = new StringReader(this.myDeckText.Text))
            {
                string line = deckReader.ReadLine();

                if (line != null)
                {
                    var data = line.Split(',');
                    deck.AddRange(data.TakeWhile(q => q.Length >= 1).Select(q => (Card.Cards)Enum.Parse(typeof(Card.Cards), q)));
                }
            }
            using (StreamWriter sw = new StreamWriter(pathToSB + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
            {

                sw.WriteLine($"{myStyleBox.SelectedItem}~{enemyStyleBox.SelectedItem}~" +
                             $"{oneDropCount.Value}~{oneDropCoin.Value}~{twoDropCount.Value}~{TwoDropCoin.Value}~{threeDrop.Value}~{threeDropCoin.Value}" +
                             $"~{fourDrop.Value}~{fourDropCoin.Value}~{reqOneTwo.Checked}~{reqTwoThree.Checked}~{reqThreeFour.Checked}~{coinSkip.Checked}~" +
                             $"{modeBox.SelectedItem}~{string.Join(",", deck)}");
            }
            var DeckID = new DeckClassification(deck.Select(c => c.ToString()).ToList());
            deckId.Text = DeckID.Name;

        }

        private void myStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myStyleBox.SelectedIndex == 0)
            {
                oneDropCount.Value = 2;
                oneDropCoin.Value = 3;

                twoDropCount.Value = 2;
                TwoDropCoin.Value = 3;

                threeDrop.Value = 1;
                threeDropCoin.Value = 2;

                fourDrop.Value = 0;
                fourDropCoin.Value = 1;
                reqOneTwo.Checked = true;
                reqTwoThree.Checked = true;
                reqThreeFour.Checked = true;
            }
            if (myStyleBox.SelectedIndex == 1)
            {
                oneDropCount.Value = 2;
                oneDropCoin.Value = 2;

                twoDropCount.Value = 3;
                TwoDropCoin.Value = 4;

                threeDrop.Value = 2;
                threeDropCoin.Value = 2;

                fourDrop.Value = 1;
                fourDropCoin.Value = 1;
                reqOneTwo.Checked = false;
                reqTwoThree.Checked = true;
                reqThreeFour.Checked = false;
            }
            if (myStyleBox.SelectedIndex == 2)
            {
                oneDropCount.Value = 1;
                oneDropCoin.Value = 1;

                twoDropCount.Value = 2;
                TwoDropCoin.Value = 4;

                threeDrop.Value = 2;
                threeDropCoin.Value = 2;

                fourDrop.Value = 1;
                fourDropCoin.Value = 2;
                reqOneTwo.Checked = false;
                reqTwoThree.Checked = false;
                reqThreeFour.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cardOffer1.Items.Clear();
            cardOffer2.Items.Clear();
            cardOffer3.Items.Clear();
            cardOffer4.Items.Clear();
            var deck = new List<Card.Cards>();
            var allSet = CardPool.Where(
                        c =>
                            c.Template().IsCollectible &&
                            (c.Template().Class == (Card.CClass)myClass.SelectedItem ||
                             c.Template().Class == Card.CClass.NONE)).ToList();
            var setInUse = new List<Card.Cards>();
            using (StringReader deckReader = new StringReader(this.myDeckText.Text))
            {
                string line = deckReader.ReadLine();

                if (line != null)
                {
                    var data = line.Split(',');
                    deck.AddRange(data.TakeWhile(q => q.Length >= 1).Select(q => (Card.Cards)Enum.Parse(typeof(Card.Cards), q)));
                }
            }

            setInUse = deckSimChkbx.Checked ? allSet.ToList() : deck;
            foreach (var q in setInUse)
            {

                cardOffer1.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer2.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer3.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer4.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");

            }
            deckId.Text = "Arena/Autofill";
        }

        private void myClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            cardOffer1.Items.Clear();
            cardOffer2.Items.Clear();
            cardOffer3.Items.Clear();
            cardOffer4.Items.Clear();

            foreach (var q in CardPool.Where(c => c.Template().IsCollectible && (c.Template().Class == (Card.CClass)myClass.SelectedItem || c.Template().Class == Card.CClass.NONE)))
            {

                cardOffer1.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer2.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer3.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer4.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");

            }
        }
        /*
         * Card.CClass.SHAMAN, Card.CClass.MAGE, Card.CClass.PRIEST,
                Card.CClass.HUNTER, Card.CClass.PALADIN, Card.CClass.WARRIOR,
                Card.CClass.WARLOCK, Card.CClass.ROGUE, Card.CClass.DRUID
         */

        readonly Dictionary<string, int> _heroIndex = new Dictionary<string, int>
        {
            {"SHAMAN", 0 },  {"MAGE", 1 }, {"PRIEST", 2 },
            {"HUNTER", 3 }, {"PALADIN", 4 }, {"WARRIOR", 5 },
            {"WARLOCK", 6 }, {"ROGUE", 7 }, {"DRUID", 8 },
        };
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deckSimChkbx.Checked) return;
            myDeckText.Text = string.Join(",", _ourPlayedDecks[ourPlayedDeckLB.SelectedItem.ToString()].DeckList);
            myClass.SelectedIndex =
                _heroIndex[_ourPlayedDecks[ourPlayedDeckLB.SelectedItem.ToString()].DeckClass.ToString()];
            checkBox1_CheckedChanged(sender, e);
            richTextBox2.Text = $"{_ourPlayedDecks[ourPlayedDeckLB.SelectedItem.ToString()].ClassificationSummary()}";
            richTextBox3.Text = $"{_ourPlayedDecks[ourPlayedDeckLB.SelectedItem.ToString()].MulliganCoreSummary()}";
            richTextBox4.Text =
                $"{string.Join("\n", _ourPlayedDecks[ourPlayedDeckLB.SelectedItem.ToString()].DeckList.Select(c => new MinimalCardTemplate(c).Name))}";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateMulligan();
        }
    }


}
