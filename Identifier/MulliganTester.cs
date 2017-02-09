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
        public MulliganTester(string sbPath)
        {
            InitializeComponent();
            pathToSB = sbPath;
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
            myStyleBox.Items.AddRange(new object[]{ACK.DeckClassification.Style.Aggro, ACK.DeckClassification.Style.Midrange, ACK.DeckClassification.Style.Control });
            enemyStyleBox.Items.AddRange(new object[] { ACK.DeckClassification.Style.Aggro, ACK.DeckClassification.Style.Midrange, ACK.DeckClassification.Style.Control });
            modeBox.Items.AddRange(new object[]{Bot.Mode.Arena, Bot.Mode.RankedStandard});
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

        }

        private void CheckCriticalFiles()
        {
            bool _write = false;
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
            applySettingsBtn_Click(sender, e);
            richTextBox1.Text = "";//Comment


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
            myClass = (Card.CClass) Enum.Parse(typeof(Card.CClass), this.myClass.SelectedItem.ToString());
            enemyClass = (Card.CClass)Enum.Parse(typeof(Card.CClass), EnemyClass.SelectedItem.ToString());
            
            var test = SimulateMulligan(myClass, enemyClass, myChoices);
            richTextBox1.Text += "Kept\n";  
            foreach (var q in test.OrderBy(c=> c.Template().Cost))
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
            Random rt = new Random();
            if (randomEnemy.Checked)
                EnemyClass.SelectedIndex = rt.Next(0, EnemyClass.Items.Count);
            bool highlander = renoAssumptionChckbx.Checked;

            var ListIndexes = cardOffer1.Items;

            int card1 = rt.Next(0, ListIndexes.Count);
            cardOffer1.SelectedIndex = card1;
            if (highlander) ListIndexes.RemoveAt(card1);

            int card2 = rt.Next(0, ListIndexes.Count);
            cardOffer2.SelectedIndex = card2;
            if (highlander) ListIndexes.RemoveAt(card2);

            int card3 = rt.Next(0, ListIndexes.Count);
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
            if (randomEnemyStyle.Checked)
                enemyStyleBox.SelectedIndex = rt.Next(0, enemyStyleBox.Items.Count);
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
            debugRichText.Text ="";
            if (choices.Count < 3)
                return new List<Card.Cards>{Card.Cards.GAME_005}; //We are coin haters. 

            CardsToKeep = choices;
            string path = pathToSB+ "\\MulliganProfiles\\ACK - Mulligan.cs";
            debugRichText.Text += path+"\n";
            List<string> stringList = new List<string>();
            try
            {
                stringList.Add(File.ReadAllText(path));
                using (CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    
                    CompilerParameters options = new CompilerParameters
                    {
                        GenerateInMemory = true, 

                        
                    };
                    
                    options.ReferencedAssemblies.AddRange(new string[5]
                    {
                        "System.Core.dll",
                        $"{pathToSB}\\SmartBotUI.exe",
                        "SmartBotAPI.dll",
                        "System.Windows.Forms.dll",
                        "ACK.dll"

                    });
                    debugRichText.Text += "Succesfully provided References to Core, UI, API, Forms, ACK\n";
                    CompilerResults compilerResults = provider.CompileAssemblyFromSource(options, stringList.ToArray());
                    if (compilerResults.Errors.Count != 0)
                    {
                        string str = $"{compilerResults.Errors.Count} Errors:";
                        for (int index = 0; index < compilerResults.Errors.Count; ++index)
                            str = str + "\r\nLine: " + compilerResults.Errors[index].Line + " - " + compilerResults.Errors[index].ErrorText;
                        MessageBox.Show(str + "\r\n\r\n", "Compilation Error");
                        return new List<Card.Cards>();
                    }
                    debugRichText.Text += "No Compilation errors\n";
                    Assembly compiledAssembly = compilerResults.CompiledAssembly;
                    debugRichText.Text += "Assembly Compiled succesfully\n";
                    MulliganProfile mulliganProfile1 = null;
                    foreach (Type type in compiledAssembly.GetTypes())
                    {
                        if (type.GetInterfaces().Contains(typeof(MulliganProfile)))
                            mulliganProfile1 = (MulliganProfile)Activator.CreateInstance(type);
                        debugRichText.Text += $"Added reference {type.Name}\n";

                    }
                    if (mulliganProfile1 == null)
                    {
                        MessageBox.Show("Mulligan class not found!");
                        return new List<Card.Cards>();
                    }
                    debugRichText.Text += $"Mulligan Profile Loaded {mulliganProfile1}\n";

                    object[] parameters = new object[3]
                    {
                        choices,
                        enemyHero,
                        ownHero
                    };
                    
                    debugRichText.Text += $"Parameters Created\n{parameters[0]}\n{parameters[1]}\n{parameters[2]}";
                    
                        CardsToKeep = mulliganProfile1.HandleMulligan(choices, enemyHero, ownHero);
                        debugRichText.Text += $"Results passed\n{CardsToKeep.First()}";
                    

                }
            }
            catch (Exception)
            {
                return new List<Card.Cards>();
            }
            return CardsToKeep;
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
                    foreach (var q in data)
                    {
                        if (q.Length < 1) break;
                        
                        deck.Add((Card.Cards) Enum.Parse(typeof(Card.Cards), q));
                    }
                    
                }
            }
            using (StreamWriter sw = new StreamWriter(pathToSB + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
            {
                
                sw.WriteLine($"{myStyleBox.SelectedItem}~{myStyleBox.SelectedItem}~" +
                             $"{oneDropCount.Value}~{oneDropCoin.Value}~{twoDropCount.Value}~{TwoDropCoin.Value}~{threeDrop.Value}~{threeDropCoin.Value}" +
                             $"~{fourDrop.Value}~{fourDropCoin.Value}~{reqOneTwo.Checked}~{reqTwoThree.Checked}~{reqThreeFour.Checked}~{coinSkip.Checked}~" +
                             $"{modeBox.SelectedItem}~{string.Join(",", deck)}");
            }
            var DeckID = new DeckClassification(deck.Select(c=> c.ToString()).ToList());
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
                    deck.AddRange(data.TakeWhile(q => q.Length >= 1).Select(q => (Card.Cards) Enum.Parse(typeof(Card.Cards), q)));
                }
            }

            setInUse = checkBox1.Checked ? allSet.ToList() : deck;
            foreach (var q in setInUse)
            {

                cardOffer1.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer2.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer3.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer4.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");

            }
        }

        private void myClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            cardOffer1.Items.Clear();
            cardOffer2.Items.Clear();
            cardOffer3.Items.Clear();
            cardOffer4.Items.Clear();

            foreach (var q in CardPool.Where(c=> c.Template().IsCollectible && (c.Template().Class == (Card.CClass) myClass.SelectedItem || c.Template().Class == Card.CClass.NONE)))
            {
                
                cardOffer1.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer2.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer3.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");
                cardOffer4.Items.Add($"{CardTemplate.LoadFromId(q).Name}~{q}");

            }
        }
    }


}
