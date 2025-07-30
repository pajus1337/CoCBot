using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Interfaces
{
    public interface IBotController
    {
        Task StartAsync();
        void Stop();
        bool IsRunning { get; }
    }
}
