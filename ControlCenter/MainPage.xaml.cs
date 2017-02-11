using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
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
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            ListRect.Height = ListPanel.ActualHeight;
            Date.Text = DateTime.Now.ToString("dddd, MMMM dd");
            Time.Text = DateTime.Now.ToString("h:mm:ss tt");
            _clockTimer = ThreadPoolTimer.CreatePeriodicTimer(_clockTimer_Tick, TimeSpan.FromMilliseconds(1000));
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ListRect.Height = this.ActualHeight - (ListRect.Margin.Bottom*2);
        }

        private void WindowResized(object sender, RoutedEventArgs e)
        {
            ListRect.Height = this.ActualHeight - (ListRect.Margin.Bottom * 2);
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

        private void button_ShoppingLists_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ShoppingList));
        }

        private void button_TodoLists_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TodoList));
        }


    }
}
