using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI {
    /// <summary>
    /// Interaktionslogik für AutoHinzufügen.xaml
    /// </summary>
    public partial class AutoHinzufügen : Window {
        public AutoHinzufügen() {
            InitializeComponent();
        }

        private void ButtonCancle(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e) {
            //Add car to Database and update list in main window
        }
    }
}
