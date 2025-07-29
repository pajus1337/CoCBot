using CoCBot.Controllers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCBot.Forms
{
    public class MainForm : Form
    {
        private readonly BotController _bot;
        private Label statusLabel;
        private Button startButton, stopButton;

        public MainForm(BotController bot)
        {
            _bot = bot;

            Text = "CoC Invite Bot (.NET)";
            Size = new Size(300, 200);

            statusLabel = new Label() { Text = "Status: STOPPED", AutoSize = true, Location = new Point(90, 30) };
            startButton = new Button() { Text = "Start", Location = new Point(50, 70), Size = new Size(80, 30) };
            stopButton = new Button() { Text = "Stop", Location = new Point(150, 70), Size = new Size(80, 30) };

            startButton.Click += async (_, __) =>
            {
                statusLabel.Text = "Status: RUNNING";
                await _bot.StartAsync();
            };

            stopButton.Click += (_, __) =>
            {
                _bot.Stop();
                statusLabel.Text = "Status: STOPPED";
            };

            Controls.Add(statusLabel);
            Controls.Add(startButton);
            Controls.Add(stopButton);
        }
    }
}