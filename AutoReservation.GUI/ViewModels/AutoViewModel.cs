using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AutoReservation.GUI.ViewModels
{
    public class AutoViewModel : OverviewTemplate<AutoDto>
    {
        public AutoViewModel()
        {
            _list = new ObservableCollection<AutoDto>(target.Autos);
        }

        public override void Add()
        {
            Window AutoDetails = new AutoDetailsWindow();
            AutoDetails.DataContext = new AutoDetailsVM(AutoDetails);
            AutoDetails.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            AutoDetails.Show();
        }

        public override bool CanAdd()
        {
            return true;
        }

        public override void Modify()
        {
            Window AutoDetails = new AutoDetailsWindow();
            AutoDetails.DataContext = new AutoDetailsVM(AutoDetails, Selected);
            AutoDetails.Show();
        }

        public override bool CanModify()
        {
            if (Selected == null)
            {
                return false;
            }
            return true;
        }

        public override void Delete()
        {
            if (MessageBox.Show("Reservation wirklich löschen?", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                target.DeleteAuto(Selected);
            }
        }

        public override bool CanDelete()
        {
            if (Selected == null)
            {
                return false;
            }
            return true;
        }

        /*
        
        */

        public void sort(object sender, RoutedEventArgs e)
        {
            _list.OrderBy(o => o.Marke);
        }
    }
}
