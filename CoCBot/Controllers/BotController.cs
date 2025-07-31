w using CoCBot.Interfaces;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CoCBot.Controllers
{
    public class BotController : IBotController
    {
        private readonly IVisionService _vision;
        private readonly IEmulatorService _emulator;

        public bool IsRunning { get; private set; }

        public BotController(IVisionService vision, IEmulatorService emulator)
        {
            _vision = vision;
            _emulator = emulator;
        }

        public async Task StartAsync()
        {
            IsRunning = true;
            _emulator.Connect();

            while (IsRunning)
            {
                _emulator.TakeScreenshot();

                Point? button = _vision.FindButtonUsingTemplate("screen.png", "invite_button.png", 0.9);
                if (button != null)
                {
                    _emulator.ClickAt(button.Value.X, button.Value.Y);
                    Log("[BOT] Button clicked.");
                }
                else
                {
                    Log("[BOT] Button not found.");
                }

                await Task.Delay(5000);
            }
        }

        public void Stop() => IsRunning = false;

        private void Log(string msg)
        {
            File.AppendAllText("bot.log", msg + "\n");
        }
    }
}