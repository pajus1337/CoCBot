using CoCBot.Configurations;
using CoCBot.Interfaces;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Services
{
    public class VisionService : IVisionService
    {
        private readonly IEmulatorService _emulatorService;
        private readonly BotPathOptions _paths;

        public VisionService(IEmulatorService emulatorService, BotPathOptions paths)
        {
            _emulatorService = emulatorService;
            _paths = paths;
        }


        public async Task ClickOnAsync(string templatePath)
        {
            // 1. Take screenshot and wait for file
            _emulatorService.TakeScreenshot();
            var screenshotPath = Path.Combine(_paths.ScreenshotPath, "screen.png");

            // 2. Delay to ensure file exists (in real impl: wait until file created)
            await Task.Delay(500);

            if (!File.Exists(screenshotPath) || !File.Exists(templatePath))
                return;

            // 3. Load images
            using var source = new Image<Bgr, byte>(screenshotPath);
            using var template = new Image<Bgr, byte>(templatePath);

            // 4. Match template
            var result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
            result.MinMax(out _, out double[] maxVals, out _, out Point[] maxLocs);

            // 5. Validate match
            if (maxVals[0] < 0.8)
                return; // No good match

            var matchPoint = maxLocs[0];
            var centerX = matchPoint.X + template.Width / 2;
            var centerY = matchPoint.Y + template.Height / 2;

            // 6. Click using emulator service
            _emulatorService.ClickAt(centerX, centerY);
        }

        public Point? FindButtonUsingTemplate(string screenPath, string templatePath, double threshold = 0.9)
        {
            using var screen = new Image<Bgr, byte>(screenPath);
            using var template = new Image<Bgr, byte>(templatePath);

            using var result = screen.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
            double[] minValues, maxValues;
            Point[] minLocations, maxLocations;
            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

            if (maxValues[0] >= threshold)
            {
                var matchLocation = maxLocations[0];
                return new Point(matchLocation.X + template.Width / 2, matchLocation.Y + template.Height / 2);
            }

            return null;
        }
    }
}
