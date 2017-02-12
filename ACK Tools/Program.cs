using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACKTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var menu = new Menu();
            menu.FormBorderStyle = FormBorderStyle.FixedSingle;
            menu.MaximizeBox = false;
            menu.MinimizeBox = false;
            Application.Run(menu);

        }
    }
}
