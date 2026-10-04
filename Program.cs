using System;
using System.Windows.Forms;

namespace EasyProdutos
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Abre a janela visual do projeto do Tião
            Application.Run(new Form1());
        }
    }
}

