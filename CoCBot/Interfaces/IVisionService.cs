using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Interfaces
{
    public interface IVisionService
    {
        Task ClickOnAsync(string templatePath);
        Task ClickInRegionAsync(Rectangle region);
        Task<Point?> FindButtonUsingTemplateAsync(string screenPath, string templatePath, double threshold);
        Task<Point?> FindButtonUsingTemplateAsync(string templatePath, double threshold = 0.9);
        Task<List<Point>> FindAllTemplateMatchesAsync(string templatePath, double threshold = 0.9);
    }
}