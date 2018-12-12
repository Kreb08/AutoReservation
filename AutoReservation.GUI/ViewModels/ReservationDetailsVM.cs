using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationDetailsVM : DetailsTemplate<ReservationDto>
    {
        public ReservationDetailsVM(Window window, OverviewTemplate<ReservationDto> parentVm, ReservationDto reservation = null) : base(window, parentVm, reservation)
        {
            if (Current == null)
            {
                Current = new ReservationDto() { Von = DateTime.Today, Bis = DateTime.Today.AddDays(1) };
            }
            Von = Current.Von;
            Bis = Current.Bis;
            Kunde = Current.Kunde;
            Auto = Current.Auto;

            Kunden = new ObservableCollection<KundeDto>(target.Kunden);

            Autos = new ObservableCollection<AutoDto>(target.Autos);
        }
        public ObservableCollection<KundeDto> Kunden { get; }
        public ObservableCollection<AutoDto> Autos { get; }

        private DateTime _von;
        public DateTime Von
        {
            get { return _von; }
            set
            {
                if (_von != value)
                {
                    _von = value;
                    OnPropertyChanged(nameof(Von));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private DateTime _bis;
        public DateTime Bis
        {
            get { return _bis; }
            set
            {
                if (_bis != value)
                {
                    _bis = value;
                    OnPropertyChanged(nameof(Bis));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public KundeDto _kunde;
        public KundeDto Kunde
        {
            get { return _kunde; }
            set
            {
                if (_kunde != value)
                {
                    _kunde = value;
                    OnPropertyChanged(nameof(Kunde));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public AutoDto _auto;
        public AutoDto Auto
        {
            get { return _auto; }
            set
            {
                if (_auto != value)
                {
                    _auto = value;
                    OnPropertyChanged(nameof(Auto));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public override bool CanCommit()
        {
            if (Von == null)
            {
                return false;
            }
            if (Bis == null)
            {
                return false;
            }
            if (Kunde == null)
            {
                return false;
            }
            if (Auto == null)
            {
                return false;
            }
            return true;
        }

        public override void Commit()
        {
            if (AddNewReservation())
            {
                ParentVm.UpdateList();
                Window.Close();
            }
        }

        public bool AddNewReservation()
        {
            try
            {
                target.InsertReservation(new ReservationDto()
                {
                    Von = Von,
                    Bis = Bis,
                    Kunde = Kunde,
                    Auto = Auto
                });
                return true;
            }
            catch (FaultException<DateRangeFault> e)
            {
                MessageBox.Show(
                    "Reservation konnte nicht hinzugefügt werden.\n" +
                    "Reservationsdauer ist zu kurz oder Datum falsch.\n\n" +
                    e.Detail.reservation,
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (FaultException<AutoUnavailableFault> e)
            {
                MessageBox.Show(
                    "Reservation konnte nicht hinzugefügt werden.\n" +
                    "Das gewählte Auto ist zur gewünschten Zeit nicht verfügbar.\n\n" +
                    e.Detail.reservation.ToString(),
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (FaultException)
            {
                MessageBox.Show("Hinzufügen Fehlgeschlagen.",
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Ein Fehler ist aufgetreten.",
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            return false;
        }

        public override bool CanCancle()
        {
            return true;
        }

        public override void Cancle()
        {
            Window.Close();
        }
    }
}
