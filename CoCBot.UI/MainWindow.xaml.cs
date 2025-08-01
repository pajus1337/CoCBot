using CoCBot.Interfaces;
using CoCBot.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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
        private readonly IEmulatorSelectorService _emulatorSelectorService;
        public MainWindow(IBotController botController, IEmulatorSelectorService emulatorSelectorService)
        {
            InitializeComponent();
            _botController = botController;
            _emulatorSelectorService = emulatorSelectorService;
            var connectedDevices = _emulatorSelectorService.GetAvailableDevices();
            DeviceSelector.ItemsSource = connectedDevices;
            _emulatorSelectorService = emulatorSelectorService;
        }

        private void OnDeviceSelected(object sender, SelectionChangedEventArgs e)
        {
            if (DeviceSelector.SelectedItem is EmulatorInfo selectedEmulator)
            {
                _emulatorSelectorService.SelectDevice(selectedEmulator.DeviceId);
            }
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
