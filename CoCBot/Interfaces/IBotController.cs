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
        Task RunLeagueInviteAsync();
        void Stop();
        bool IsRunning { get; }
    }
}
