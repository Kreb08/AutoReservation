using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.GUI.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationViewModel : OverviewTemplate<ReservationDto>
    {
        public ReservationViewModel()
        {
            _list = new ObservableCollection<ReservationDto>(target.Reservationen);
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

        public override void Modify()
        {
            Window DetailsWindow = new ReservationDetailsWindow();
            DetailsWindow.DataContext = new ReservationDetailsVM(DetailsWindow, Selected);
            DetailsWindow.Show();
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
            target.DeleteReservation(Selected);
        }

        public override bool CanDelete()
        {
            if (Selected == null)
            {
                return false;
            }
            return true;
        }
    }
}
