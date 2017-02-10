using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ControlCenter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ThreadPoolTimer _clockTimer;
        public MainPage()
        {
            this.InitializeComponent();
            Date.Text = DateTime.Now.ToString("dddd, MMMM dd");
            Time.Text = DateTime.Now.ToString("h:mm:ss tt");
            _clockTimer = ThreadPoolTimer.CreatePeriodicTimer(_clockTimer_Tick, TimeSpan.FromMilliseconds(1000));
        }

        private async void _clockTimer_Tick(ThreadPoolTimer timer)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(
             CoreDispatcherPriority.Normal, () =>
             {
                 Time.Text = DateTime.Now.ToString("h:mm:ss tt");
             });
        }

        private void button_GroceryList_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GroceryList));
        }

        private void button_TodoList_Click(object sender, RoutedEventArgs e)
        {
            Time.Text = "Hello";
        }


    }
}
