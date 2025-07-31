using CoCBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Interfaces
{
    public interface IEmulatorSelectorService
    {
        string? SelectedDeviceId { get; }
        IEnumerable<EmulatorInfo> GetAvailableDevices();
        void SelectDevice(string deviceId);
    }
}
