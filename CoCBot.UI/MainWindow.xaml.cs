using CoCBot.Interfaces;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CoCBot.UI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly IBotController _botController;
        public MainWindow(IBotController botController)
        {
            InitializeComponent();
            _botController = botController;
        }

        private async void OnStartClick(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "Status: RUNNING";
            await _botController.StartAsync();
        }

        private void OnStopClick(object sender, RoutedEventArgs e)
        {
            _botController.Stop();
            StatusText.Text = "Status: STOPPED";
        }
    }
}
