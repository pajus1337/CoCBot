using CoCBot.Configurations;
using CoCBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            // Step 1: Open profile or Clan Castle ( first using prfile icon, might change in future development ) 
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

            // Step 5: Click first clan on the list (e.g., Saiyaara) ( We will have to rework the logic on pointing the target ) 
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "clan_tile_1.png"));
            await Task.Delay(1300);

            // Step 6: Click "View Clan" button
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "view_clan_button.png"));
            await Task.Delay(1500);

            // TODO: Further logic like looping over members will follow later ---
        }

        public async Task InvitePlayersViaMyLeagueAsync()
        {
            await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "my_league_tab.png"));
            await Task.Delay(2600);

            var trophyPath = Path.Combine(_paths.TemplatePath, "trophy_icon.png");
            using var trophyTemplate = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(trophyPath);

            var invitedPoints = new HashSet<int>(); // track over Y
            int maxScrolls = 20;

            for (int i = 0; i < maxScrolls; i++)
            {
                var matches = await _visionService.FindAllTemplateMatchesAsync(trophyPath, 0.87);

                var playersToInvite = matches
                    .DistinctBy(p => p.Y / 10) // Uniue player by Y rounded to 10
                    .Where(p => !invitedPoints.Contains((p.Y / 10) * 10))
                    .OrderBy(p => p.Y)
                    .Take(5)
                    .ToList();

                foreach (var matchPoint in playersToInvite)
                {
                    var roundedY = (matchPoint.Y / 10) * 10;

                    var trophyRegion = new Rectangle(
                        matchPoint.X - trophyTemplate.Width / 2,
                        matchPoint.Y - trophyTemplate.Height / 2,
                        trophyTemplate.Width,
                        trophyTemplate.Height
                    );

                    await _visionService.ClickInRegionAsync(trophyRegion);
                    await Task.Delay(1600);

                    // Short Check if profil buttone PopUp
                    var profileBtnPath = Path.Combine(_paths.TemplatePath, "profile_button.png");
                    var found = await _visionService.FindButtonUsingTemplateAsync(profileBtnPath);

                    if (found.HasValue)
                    {
                        invitedPoints.Add(matchPoint.Y); // Consider als Invited.

                        await _visionService.ClickOnAsync(profileBtnPath);
                        await Task.Delay(2800);

                        await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "invite_button.png"));
                        await Task.Delay(2400);

                        await _visionService.ClickOnAsync(Path.Combine(_paths.TemplatePath, "back_button.png"));
                        await Task.Delay(1600);
                    }
                }

                await ScrollDownAsync();
                await Task.Delay(1800);
            }
        }

        private async Task ScrollDownAsync()
        {
            // Scroll down the player list ~ 5 positions. ( tested values ) 
            var start = new Point(450, 600);
            var end = new Point(450, 465);

            await _emulatorService.SwipeAsync(start, end, durationMs: 240);
        }
    }
}
