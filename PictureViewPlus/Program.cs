using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewPlus
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length>0)
            Application.Run(new PictureViewPlus(args[0]));
            else
            {
                Application.Run(new PictureViewPlus(""));
            }
        }
    }
}
