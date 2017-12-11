using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aquarium
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm(new Aquarium(new Size(500, 500), 10)));
        }
    }
}
