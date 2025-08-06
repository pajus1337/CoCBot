using System.Drawing;
using System.Threading.Tasks;

namespace CoCBot.Interfaces
{
    public interface IEmulatorService
    {
        void Connect();
        void TakeScreenshot();
        void ClickAt(int x, int y);
        Task SwipeAsync(Point from, Point to, int durationMs = 300);
    }
}