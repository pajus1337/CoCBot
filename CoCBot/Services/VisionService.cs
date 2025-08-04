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
        private readonly IScreenshotStorageService _screenshotStorage;

        public VisionService(IEmulatorService emulatorService, IScreenshotStorageService screenshotStorage)
        {
            _emulatorService = emulatorService;
            _screenshotStorage = screenshotStorage;
        }

        public async Task ClickOnAsync(string templatePath)
        {
            _emulatorService.TakeScreenshot();

            var screenshotPath = _screenshotStorage.GetScreenshotPath();

            await Task.Delay(500);

            if (!File.Exists(screenshotPath) || !File.Exists(templatePath))
            {
                return;
            }

            using var source = new Image<Bgr, byte>(screenshotPath);
            using var template = new Image<Bgr, byte>(templatePath);

            var result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
            result.MinMax(out _, out double[] maxVals, out _, out Point[] maxLocs);

            // Validate match
            if (maxVals[0] < 0.8)
            {
                return; // Match not found or below threshold
            }

            var matchPoint = maxLocs[0];
            var centerX = matchPoint.X + template.Width / 2;
            var centerY = matchPoint.Y + template.Height / 2;

            _emulatorService.ClickAt(centerX, centerY);
        }

        public Point? FindButtonUsingTemplate(string screenPath, string templatePath, double threshold = 0.9)
        {
            using var screen = new Image<Bgr, byte>(screenPath);
            using var template = new Image<Bgr, byte>(templatePath);

            using var result = screen.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
            result.MinMax(out double[] minVals, out double[] maxVals, out Point[] minLocs, out Point[] maxLocs);

            if (maxVals[0] >= threshold)
            {
                var matchLocation = maxLocs[0];
                return new Point(matchLocation.X + template.Width / 2, matchLocation.Y + template.Height / 2);
            }

            return null;
        }
    }
}
