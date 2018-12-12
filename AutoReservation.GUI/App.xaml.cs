using AutoReservation.GUI.ViewModels;
using System.Windows;

namespace AutoReservation.GUI {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {

        public AppVm AppVm{ get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            AppVm = new AppVm(MainWindow);
            MainWindow.DataContext = AppVm;
            MainWindow.Show();
        }

    }
}
