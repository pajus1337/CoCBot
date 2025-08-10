using CoCBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Helpers
{
    public class ClickRandomizer : IClickRandomizer
    {
        private readonly Random _random = new();

        public Point GetRandomPointInRegion(Rectangle region)
        {
            var x = _random.Next(region.Left + 2, region.Right - 2);
            var y = _random.Next(region.Top + 2, region.Bottom - 2);
            return new Point(x, y);
        }

        public int GetRandomDelay(int minMs, int maxMs)
        {
            return _random.Next(minMs, maxMs + 1);
        }
    }
}
