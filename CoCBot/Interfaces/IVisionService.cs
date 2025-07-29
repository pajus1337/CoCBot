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
        Point? FindButtonUsingTemplate(string screenPath, string templatePath, double threshold);
    }
}