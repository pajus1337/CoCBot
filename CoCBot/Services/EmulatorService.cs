using CoCBot.Configurations;
using CoCBot.Helpers;
using CoCBot.Interfaces;
using CoCBot.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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

        public Task SwipeAsync(Point from, Point to, int durationMs = 300)
        {
            Swipe(from, to, durationMs);
            return Task.CompletedTask;
        }

        private void Swipe(Point from, Point to, int durationMs)
        {
            ExecuteADBCommand($"shell input swipe {from.X} {from.Y} {to.X} {to.Y} {durationMs}");
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
