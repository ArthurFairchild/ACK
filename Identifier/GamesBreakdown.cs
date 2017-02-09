using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACKTools
{
    public partial class GamesBreakdown : Form
    {
        public GamesBreakdown()
        {
            InitializeComponent();
        }

        private void loadHistory_Click(object sender, EventArgs e)
        {
            Graphics gr = cardPerformanceGrid.CreateGraphics();
            Pen myPen = new Pen(Brushes.Black, 1);
            Font myFont = new Font("Arial", 10);
            int lines = Convert.ToInt32(5);
            float x = 0f;
            float y = 0f;
            float xspace = cardPerformanceGrid.Width / lines;
            float yspace = cardPerformanceGrid.Height / lines;
            for (int i = 0; i == 4; i++)
            {
                gr.DrawLine(myPen, x,y,x,cardPerformanceGrid.Height);
                x += xspace;
            }
        }
    }
}
