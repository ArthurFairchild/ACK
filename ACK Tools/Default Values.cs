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
            CardTemplate roaring = CardTemplate.LoadFromId("LOE_002t");
            //richTextBox2.Text += "\n" + roaring.ToString() +" "+ roaring.IsCollectible +" "+roaring.Type+ " \n\n";
            var @cards = CardTemplate.TemplateList.Keys;
            var @collectable = @cards.Where(c => c.Template().IsCollectible).ToList();
            
            var @notCollectable = @cards.Except(@collectable).ToList();
            var clone = new List<Card.Cards>();
            foreach (var q in @notCollectable)
            {
                if (q.Template().Type == Card.CType.HERO || q.Template().Type == Card.CType.HEROPOWER) continue;
                if (q.Template().Cost == 0) continue;
                if (q.Template().Set == Card.CSet.CREDITS) continue;
                if (CollectableSet.ContainsKey(q.ToString().Split('_')[0])) continue;
                if (q.Template().Type == Card.CType.MINION || q.Template().Type == Card.CType.SPELL) 
                    clone.Add(q);
                

            }
            foreach (var q in clone)
            {
                richTextBox2.Text += $"\n[{q.Template().Cost} mana][{q.Template().Atk}/{q.Template().Health}] [[{q.Template().Id}]] [{q.Template().Class}] {q.Template().Name} ";
            }
        }

        public Dictionary<string, bool> CollectableSet = new Dictionary<string, bool>
        {
            {} ""
        };
        private void defaultValues_Click(object sender, EventArgs e)
        {
           
           
        }
    }
}
