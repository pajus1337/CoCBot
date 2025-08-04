using CoCBot.Configurations;
using CoCBot.Interfaces;
using CoCBot.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace CoCBot.Services
{
    public class EmulatorService : IEmulatorService
    {
        private string? _selectedDeviceId;

        private readonly IScreenshotStorageService _screenshotStorageService;

        public EmulatorService(IScreenshotStorageService screenshotStorageService)
        {
            _screenshotStorageService = screenshotStorageService;
        }

        public void SelectDevice(string deviceId)
        {
            _selectedDeviceId = deviceId;
        }

        public void Connect()
        {
            if (!string.IsNullOrEmpty(_selectedDeviceId))
            {
                ExecuteADBCommand($"connect {_selectedDeviceId}");
            }
        }

        public void TakeScreenshot()
        {
            var adbArguments = string.IsNullOrEmpty(_selectedDeviceId) ? "exec-out screencap -p" : $"-s {_selectedDeviceId} exec-out screencap -p";

            var startInfo = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = adbArguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var screenshotPath = _screenshotStorageService.GetScreenshotPath();

            using var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            using var outputStream = process.StandardOutput.BaseStream;
            using var fileStream = File.Create(screenshotPath);
            outputStream.CopyTo(fileStream);

            process.WaitForExit();
        }

        public void ClickAt(int x, int y)
        {
            ExecuteADBCommand($"shell input tap {x} {y}");
        }

        private void ExecuteADBCommand(string command)
        {
            var adbArguments = string.IsNullOrEmpty(_selectedDeviceId) ? command : $"-s {_selectedDeviceId} {command}";

            var startInfo = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = adbArguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            process?.WaitForExit();
        }
    }
}
