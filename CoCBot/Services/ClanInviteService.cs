using CoCBot.Configurations;
using CoCBot.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Services
{
    public class ClanInviteService : IClanInviteService
    {
        private readonly IEmulatorService _emulatorService;
        private readonly IVisionService _visionService;
        private readonly BotPathOptions _paths;

        public ClanInviteService(IEmulatorService emulatorService, IVisionService visionService, BotPathOptions paths)
        {
            _emulatorService = emulatorService;
            _visionService = visionService;
            _paths = paths;

        }

        public async Task RunAutoInviteAsync()
        {
            // Step 1: Open profile or Clan Castle
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "profile_icon.png"));
            await Task.Delay(1500);

            // Step 2: Click on clan icon (after opening profile)
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "clan_icon.png"));
            await Task.Delay(1500);

            // Step 3: Switch to "Clans" tab (if needed)
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "tab_clans.png"));
            await Task.Delay(1200);

            // Step 4: Click on Notice Board section
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "notice_board_tab.png"));
            await Task.Delay(1200);

            // Step 5: Click first clan on the list (e.g., Saiyaara)
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "clan_tile_1.png"));
            await Task.Delay(1300);

            // Step 6: Click "View Clan" button
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "view_clan_button.png"));
            await Task.Delay(1500);

            // TODO: Further logic like looping over members will follow later ---
        }
    }
}
