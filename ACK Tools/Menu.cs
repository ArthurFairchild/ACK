﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ACK;
using SmartBot.Database;

namespace ACKTools
{
    public partial class Menu : Form
    {
        private readonly string _smartBotPath = "";
        private string MatchHistoryPath = "";
        private string DeckPerformanceString = "";
        private HistoryDebugger historyForm = null;
        private MulliganTester mulliganTesterForm = null;
        public Menu()
        {
            InitializeComponent();
            //MessageBox.Show(new Uri(Assembly.GetExecutingAssembly().CodeBase).PathAndQuery);
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            textBox1.Text =$"{fvi.InternalName} v{fvi.FileVersion}";
            bool test = false;
            while (!test)
            {
                if (File.Exists(Application.StartupPath + "\\SmartBotPath.txt"))
                {
                    _smartBotPath = File.ReadLines(Application.StartupPath + "\\SmartBotPath.txt").First();
                    if (File.Exists(_smartBotPath + "\\Logs\\ACKTracker\\MatchHistory.txt"))
                        MatchHistoryPath = _smartBotPath + "\\Logs\\ACKTracker\\MatchHistory.txt";
                    if (File.Exists(_smartBotPath + "\\Logs\\ACKTracker\\DeckPerformanceHistory.txt"))
                        DeckPerformanceString = _smartBotPath + "\\Logs\\ACKTracker\\DeckPerformanceHistory.txt";
                    break;
                    //continue;
                }
                FolderBrowserDialog fbd = new FolderBrowserDialog
                {
                    Description =
                        $"Navigate to main SmartBot Folder\nExample C:/Users/Claire/Desktop/sb-v40.8/\nDon't select any other folder"
                };
                switch (fbd.ShowDialog())
                {
                    case DialogResult.Cancel:
                    case DialogResult.Abort:
                        fbd.Dispose();
                        return;
                    case DialogResult.OK:
                        if (!File.Exists(fbd.SelectedPath + "\\Logs\\ACKTracker\\MatchHistory.txt"))
                        {
                            if (
                                MessageBox.Show($"First time ACK User?",
                                    $"Necessary files not found", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                return;
                            }
                            else
                            {


                            }
                        }
                        else
                        {
                            using (
                                    StreamWriter sw =
                                        new StreamWriter(Application.StartupPath + "\\SmartBotPath.txt"))
                            {
                                sw.WriteLine(fbd.SelectedPath);
                                _smartBotPath = fbd.SelectedPath;
                            }
                            test = true;
                        }
                        break;
                    case DialogResult.None:
                        break;
                    case DialogResult.Retry:
                        break;
                    case DialogResult.Ignore:
                        break;
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Thread task = new Thread(GetMyDecksPlayedDecks) {IsBackground = true};
            task.Start();
            historyForm = new HistoryDebugger(_smartBotPath)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = false
            };
            mulliganTesterForm = new MulliganTester(_smartBotPath, db)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = false
            };

        }


        private void classifyHistoryBtn_Click(object sender, EventArgs e)
        {
            historyForm.Show();
            


        }


        public Dictionary<string, DeckClassification> db = new Dictionary<string, DeckClassification>();
        private void GetMyDecksPlayedDecks()
        {
            List<string> _myDecks = File.ReadAllLines(_smartBotPath + "\\Logs\\ACK\\MatchHistory.txt").Reverse().ToList();
            List<string> decks = new List<string>();
            Dictionary<string, bool> alreadyParsed = new Dictionary<string, bool>();
            foreach (var q in _myDecks)
            {

                
                var deck = q.Split('~')[5];
                try
                {
                    if (deck.ToCharArray()[0] == ',')
                        deck = deck.Substring(1); //some idiot put comma at the beggining of a decklist
                }
                catch (Exception)
                {
                    continue;
                }
                if (alreadyParsed.ContainsKey(deck)) continue;
                //MessageBox.Show(string.Join("/", deck));
                decks.Add(deck);
                alreadyParsed.AddOrUpdate(deck, true);
            }
            foreach (var q in alreadyParsed)
            {

                DeckClassification dc = new DeckClassification(q.Key.Split(',').ToList());
                db[dc.Name] = dc;
                

            }
            //MessageBox.Show($"{_ourPlayedDecks.First().Key}\n{_ourPlayedDecks.First().Value.DeckList}");
        }
        private void mulliganTesterBtn_Click(object sender, EventArgs e)
        {
           
            mulliganTesterForm.Show();
            

        }

        private void cardBreakdownBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HI Loser");
            var def = new Form2();
            def.Show();
        }



        private void convertHistoryBtn_Click(object sender, EventArgs e)
        {
            var cohf = new ConvertOldHistory();
            cohf.Show();
            
        }
        private void mysteryBtn_Click(object sender, EventArgs e)
        {
            mysteryBtn.Enabled = false;
            CheckButtonStatus();

        }

        private void mysteryBtn2_Click(object sender, EventArgs e)
        {
            mysteryBtn2.Enabled = false;
            CheckButtonStatus();

        }
        private void mysteryBtn3_Click(object sender, EventArgs e)
        {
            mysteryBtn3.Enabled = false;
            CheckButtonStatus();


        }
        private void mysteryBtn4_Click(object sender, EventArgs e)
        {
            mysteryBtn4.Enabled = false;
            CheckButtonStatus();

        }

        private int _count = 0;
        private void CheckButtonStatus()
        {

            if (mysteryBtn.Enabled || mysteryBtn2.Enabled || mysteryBtn3.Enabled || mysteryBtn4.Enabled) return;
            mysteryBtn.Enabled = true;
            mysteryBtn2.Enabled = true;
            mysteryBtn3.Enabled = true;
            mysteryBtn4.Enabled = true;
            _count++;
            if (_count == 5)
            {
                MessageBox.Show("Just, stop, there is nothing here");
            }
            if (_count == 20)
            {
                MessageBox.Show("Ha, you might be able to pull this off");

            }
            if (_count == 50)
            {
                MessageBox.Show("In case you have not figured it out, it's pointless...");

            }
            List<Control> controlList = new List<Control>();

            GetAllControl(this, controlList);

            foreach (Control control in controlList)
            {
                if (control.GetType() == typeof(Button) && !control.Enabled)
                {
                    control.Enabled = true;
                }
            }
            DisposeAllButThis(this);



        }
        public void DisposeAllButThis(Form form)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm != form)
                {
                    frm.Close();
                }
            }
            return;
        }
        private void GetAllControl(Control c, List<Control> list)
        {
            foreach (Control control in c.Controls)
            {
                list.Add(control);

                if (control.GetType() == typeof(Panel))
                    GetAllControl(control, list);
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Available");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Snatched from SmartBotUI.exe
            List<string> seeArenaBlackList = new List<string>()
            {
                "GVG_029",
                "AT_043",
                "CS2_053",
                "EX1_365",
                "CS2_063",
                "EX1_132",
                "EX1_317",
                "NEW1_004",
                "EX1_243",
                "DS1_184",
                "GVG_016",
                "GVG_090",
                "NEW1_021",
                "NEW1_003",
                "GVG_092",
                "GVG_093",
                "AT_025"
            };
            string message = "";
            foreach (var q in seeArenaBlackList.OrderBy(w => CardTemplate.LoadFromId(w).Type ).ThenBy(c => CardTemplate.LoadFromId(c).Cost))
            {
                var temp = CardTemplate.LoadFromId(q);
                message += $"{temp.Cost} Mana {temp.Type} \t[{temp.Atk}/{temp.Health}] {temp.Name}\n";
            }
           
            MessageBox.Show(message, "All blacklisted arena cards by SmartBot");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
      
    }
}
