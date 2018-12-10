using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationViewModel : OverviewTemplate<ReservationDto>
    {
        public ReservationViewModel()
        {
            _list = new ObservableCollection<ReservationDto>(target.Reservationen.FindAll(o => o.Bis > DateTime.Now).OrderBy(o => o.Von).ThenBy(o => o.Bis));
        }

        public override void Add()
        {
            Window DetailsWindow = new ReservationDetailsWindow();
            DetailsWindow.DataContext = new ReservationDetailsVM(DetailsWindow);
            DetailsWindow.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            DetailsWindow.Show();
        }

        public override bool CanAdd()
        {
            return true;
        }

        public override void Delete()
        {
            if (MessageBox.Show("Reservation wirklich löschen?", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                target.DeleteReservation(Selected);
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
        
        public override void Modify()
        {
            // Not used in this View
            throw new System.NotImplementedException();
        }
        
        public override bool CanModify()
        {
            // Not used in this View
            throw new System.NotImplementedException();
        }
    }
}
