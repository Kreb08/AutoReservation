using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Threading;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationViewModel : OverviewTemplate<ReservationDto>
    {
        public ReservationViewModel(Window mainWindow) : base(mainWindow)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((o, a) => UpdateList());
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        public override void Add()
        {
            Window DetailsWindow = new ReservationDetailsWindow();
            DetailsWindow.DataContext = new ReservationDetailsVM(DetailsWindow, this);
            DetailsWindow.Owner = mainWindow;
            DetailsWindow.Show();
        }

        public override bool CanAdd()
        {
            return true;
        }

        public override void Delete()
        {
            if (MessageBox.Show("Reservation wirklich löschen?", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                target.DeleteReservation(Selected);
            }
            catch (FaultException e)
            {
                MessageBox.Show(
                    "Löschen Fehlgeschlagen.\n\n" +
                    e.Message,
                    "Fehler beim Löschen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Ein Fehler ist aufgetreten.\n\n" +
                    e.Message,
                    "Fehler beim Löschen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            UpdateList();
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
            throw new NotImplementedException();
        }
        
        public override bool CanModify()
        {
            // Not used in this View
            throw new NotImplementedException();
        }

        protected override List<ReservationDto> GetList()
        {
            List<ReservationDto> list;
            try
            {
                if (IsFiltered)
                {
                    list = target.Reservationen.FindAll(o => o.Von <= DateTime.Now && o.Bis >= DateTime.Now);
                }
                else
                {
                    list = target.Reservationen;
                }
                return list.OrderBy(o => o.Von).ThenBy(o => o.Bis).ToList();
            }
            catch (FaultException e)
            {
                MessageBox.Show(
                    "Liste konnte nicht aktualisiert werden.\n\n" +
                    e.Message,
                    "Fehler beim Aktualisieren",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Ein Fehler ist beim aktualisieren aufgetreten.\n\n" +
                    e.Message,
                    "Fehler beim Aktualisieren",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            return new List<ReservationDto>();
        }

        private bool _isFiltered;
        public bool IsFiltered
        {
            get { return _isFiltered; }
            set
            {
                _isFiltered = value;
                OnPropertyChanged(nameof(IsFiltered));
                UpdateList();
            }
        }

        public bool CanFilter()
        {
            if(List != null && List.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
