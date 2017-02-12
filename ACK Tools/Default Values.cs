using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ACK;
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
        private void defaultValues_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> @int = new MulliganContainer().MinionPriorityTable;
            foreach (var q in @int.OrderBy(c=> new MinimalCardTemplate(c.Key).Cost))
                richTextBox1.Text += $"[DisplayName(\"[{new MinimalCardTemplate(q.Key).Cost}] {new MinimalCardTemplate(q.Key).Name}\")]\n" +
                                     $"public int {q.Key} {{get; set;}}\n";

            foreach (var q in @int)
                richTextBox1.Text += $"{q.Key} = {q.Value};\n";
        }
    }
}
