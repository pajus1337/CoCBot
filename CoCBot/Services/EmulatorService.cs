using CoCBot.Interfaces;
using CoCBot.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace CoCBot.Services
{
    public class EmulatorService : IEmulatorService
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private string? _selectedDeviceId;

        public void Connect()
        {
            if (!string.IsNullOrEmpty(_selectedDeviceId))
            {
                ExecuteADBCommand($"connect {_selectedDeviceId}");
            }
        }

        public void TakeScreenshot()
        {
            ExecuteADBCommand("exec-out screencap -p > screen.png", true);
        }

        public void ClickAt(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private void ExecuteADBCommand(string command, bool useShell = false)
        {
            var adbArguments = string.IsNullOrEmpty(_selectedDeviceId) ? command : $"-s {_selectedDeviceId} {command}";

            var startInfo = new ProcessStartInfo
            {
                FileName = useShell ? "cmd.exe" : "adb",
                Arguments = useShell ? $"/C adb {adbArguments}" : adbArguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(startInfo)?.WaitForExit();
        }


        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}
