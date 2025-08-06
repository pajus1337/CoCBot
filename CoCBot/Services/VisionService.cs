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
        private readonly IClickRandomizer _clickRandomizer;

        public VisionService(IEmulatorService emulatorService, IScreenshotStorageService screenshotStorage, IClickRandomizer clickRandomizer)
        {
            _emulatorService = emulatorService;
            _screenshotStorage = screenshotStorage;
            _clickRandomizer = clickRandomizer;
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
            var region = new Rectangle(matchPoint.X, matchPoint.Y, template.Width, template.Height);
            var randomPoint = _clickRandomizer.GetRandomPointInRegion(region);

            await Task.Delay(_clickRandomizer.GetRandomDelay(200, 500));
            _emulatorService.ClickAt(randomPoint.X, randomPoint.Y);
        }

        public async Task<Point?> FindButtonUsingTemplateAsync(string screenPath, string templatePath, double threshold = 0.9)
        {
            await Task.Delay(100);

            if (!File.Exists(screenPath) || !File.Exists(templatePath))
            {
                return null;
            }

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

        public async Task<Point?> FindButtonUsingTemplateAsync(string templatePath, double threshold = 0.9)
        {
            var screenshotPath = _screenshotStorage.GetScreenshotPath();
            _emulatorService.TakeScreenshot();
            await Task.Delay(300);

            if (!File.Exists(screenshotPath) || !File.Exists(templatePath))
            {
                return null;
            }

            using var screen = new Image<Bgr, byte>(screenshotPath);
            using var template = new Image<Bgr, byte>(templatePath);

            using var result = screen.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
            result.MinMax(out _, out double[] maxVals, out _, out Point[] maxLocs);

            if (maxVals[0] >= threshold)
            {
                var matchLocation = maxLocs[0];
                return new Point(matchLocation.X + template.Width / 2, matchLocation.Y + template.Height / 2);
            }

            return null;
        }

        public async Task<List<Point>> FindAllTemplateMatchesAsync(string templatePath, double threshold = 0.9)
        {
            var screenshotPath = _screenshotStorage.GetScreenshotPath();
            _emulatorService.TakeScreenshot();
            await Task.Delay(300);

            var matches = new List<Point>();

            if (!File.Exists(screenshotPath) || !File.Exists(templatePath))
            {
                return matches;
            }

            using var source = new Image<Bgr, byte>(screenshotPath);
            using var template = new Image<Bgr, byte>(templatePath);
            using var result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);

            result.MinMax(out _, out _, out _, out _);

            for (int y = 0; y < result.Rows; y++)
            {
                for (int x = 0; x < result.Cols; x++)
                {
                    var score = result[y, x].Intensity;
                    if (score >= threshold)
                    {
                        matches.Add(new Point(x + template.Width / 2, y + template.Height / 2));
                    }
                }
            }

            return matches;
        }
        public async Task ClickInRegionAsync(Rectangle region)
        {
            var point = _clickRandomizer.GetRandomPointInRegion(region);
            await Task.Delay(_clickRandomizer.GetRandomDelay(200, 500));
            _emulatorService.ClickAt(point.X, point.Y);
        }
    }
}
