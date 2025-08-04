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
    public class ScreenshotStorageService : IScreenshotStorageService
    {
        private readonly string _screenshotDirectory;

        public ScreenshotStorageService(BotPathOptions botPaths)
        {
            _screenshotDirectory = botPaths.ScreenshotPath;
            Directory.CreateDirectory(_screenshotDirectory);
        }

        public string GetScreenshotPath()
        {
            return Path.Combine(_screenshotDirectory, "screen.png");
        }

        public string GetScreenshotDirectory()
        {
            return _screenshotDirectory;
        }
    }
}

