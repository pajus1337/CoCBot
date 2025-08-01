using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Configurations
{
    public class BotPathOptions
    {
        public string TemplatePath { get; init; } = Path.Combine(AppContext.BaseDirectory, "Assets", "Templates");
        public string ScreenshotPath { get; init; } = Path.Combine(AppContext.BaseDirectory, "Assets", "Screenshots");
    }
}
