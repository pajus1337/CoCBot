using CoCBot.Interfaces;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Services
{
    public class VisionService : IVisionService
    {
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
