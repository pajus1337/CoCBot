namespace CoCBot.Interfaces
{
    public interface IEmulatorService
    {
        void Connect();
        void TakeScreenshot();
        void ClickAt(int x, int y);
    }
}