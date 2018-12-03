using AutoReservation.GUI.ViewModels;
using System.Windows;

namespace AutoReservation.GUI {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public AutoViewModel AutoViewModel { get; set; }

        public MainWindow() {
            InitializeComponent();

            AutoViewModel = new AutoViewModel();
            DataContext = AutoViewModel;
        }

        private void ButtonAddCar(object sender, RoutedEventArgs e) {
            AutoHinzufügen addCarWindow = new AutoHinzufügen();
            addCarWindow.DataContext = AutoViewModel;
            addCarWindow.Show();
        }
    }
}
