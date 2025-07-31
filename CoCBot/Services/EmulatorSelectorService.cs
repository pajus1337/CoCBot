using CoCBot.Interfaces;
using CoCBot.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Services
{
    public class EmulatorSelectorService : IEmulatorSelectorService
    {
        private string? _selectedDeviceId;

        public IEnumerable<EmulatorInfo> GetAvailableDevices()
        {
            var result = new List<EmulatorInfo>();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = "devices",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var lines = output.Split('\n')
                              .Select(line => line.Trim())
                              .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("List of devices"))
                              .ToList();

            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length == 2)
                {
                    result.Add(new EmulatorInfo(parts[0], parts[1]));
                }
            }

            return result;
        }

        public void SelectDevice(string deviceId)
        {
            _selectedDeviceId = deviceId;
        }

        public string? SelectedDeviceId => _selectedDeviceId;
    }
}
