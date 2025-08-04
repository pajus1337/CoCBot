using CoCBot.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CoCBot.Controllers
{
    public class BotController : IBotController
    {
        private readonly IClanInviteService _clanInviteService;

        public bool IsRunning { get; private set; }

        public BotController(IClanInviteService clanInviteService)
        {
            _clanInviteService = clanInviteService;
        }

        public async Task StartAsync()
        {
            IsRunning = true;

            Log("[BOT] Invite automation started.");

            await _clanInviteService.RunAutoInviteAsync();

            Log("[BOT] Invite automation finished.");
            IsRunning = false;
        }

        public async Task RunLeagueInviteAsync()
        {
            IsRunning = true;
            Log("[BOT] League invite started.");
            await _clanInviteService.InvitePlayersViaMyLeagueAsync();
            Log("[BOT] League invite finished.");
            IsRunning = false;
        }
        public void Stop()
        {
            Log("[BOT] Automation stopped manually.");
            IsRunning = false;
        }

        private void Log(string msg)
        {
            var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);
            var logPath = Path.Combine(logDirectory, "bot.log");

            File.AppendAllText(logPath, msg + "\n");
        }
    }
}