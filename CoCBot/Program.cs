using System;
using System.Windows.Forms;
using CoCBot.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace CoCBot
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection()
                .AddBotServices();

            var serviceProvider = services.BuildServiceProvider();
            var form = serviceProvider.GetRequiredService<MainForm>();

            Application.Run(form);
        }
    }
}