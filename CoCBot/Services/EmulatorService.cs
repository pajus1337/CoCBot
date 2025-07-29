
using CoCBot.Interfaces;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoCBot.Services
{
    public class EmulatorService : IEmulatorService
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public void Connect()
        {
            ExecuteADBCommand("connect 127.0.0.1:5625");
        }

        public void TakeScreenshot()
        {
            ExecuteADBCommand("exec-out screencap -p > screen.png", true);
        }

        public void ClickAt(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private void ExecuteADBCommand(string command, bool useShell = false)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = useShell ? "cmd.exe" : "adb",
                Arguments = useShell ? $"/C adb {command}" : command,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process.Start(startInfo)?.WaitForExit();
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}