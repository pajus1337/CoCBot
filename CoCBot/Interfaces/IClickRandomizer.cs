using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Interfaces
{
    public interface IClickRandomizer
    {
        Point GetRandomPointInRegion(Rectangle region);
        int GetRandomDelay(int minMs, int maxMs);
    }
}
