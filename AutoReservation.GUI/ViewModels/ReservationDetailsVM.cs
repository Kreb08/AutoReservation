using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationDetailsVM : DetailsTemplate<ReservationDto>
    {
        public ReservationDetailsVM(Window window, ReservationDto reservation = null) : base(window, reservation)
        {
            if (Current == null)
            {
                Current = new ReservationDto() { Von = DateTime.Today, Bis = DateTime.Today };
            }
            Von = Current.Von;
            Bis = Current.Bis;
            Kunde = Current.Kunde;
            Auto = Current.Auto;

            _kunden = new ObservableCollection<KundeDto>(target.Kunden);

            _autos = new ObservableCollection<AutoDto>(target.Autos);
        }

        private readonly ObservableCollection<KundeDto> _kunden;
        public ObservableCollection<KundeDto> Kunden
        {
            get { return _kunden; }
        }

        private readonly ObservableCollection<AutoDto> _autos;
        public ObservableCollection<AutoDto> Autos
        {
            get { return _autos; }
        }

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
            if(Auto == null)
            {
                return false;
            }
            return true;
        }

        public override void Commit()
        {
            

            if (Current.ReservationsNr == 0)
            {
                AddNewReservation();
            }
            else
            {
                SaveReservation();
            }
            Window.Close();
        }

        public void AddNewReservation()
        {
            target.InsertReservation(new ReservationDto()
            {
                Von = Von,
                Bis = Bis,
                Kunde = Kunde,
                Auto = Auto
            });
        }

        public void SaveReservation()
        {
            target.UpdateReservation(new ReservationDto()
            {
                ReservationsNr = Current.ReservationsNr,
                Von = Von,
                Bis = Bis,
                Kunde = Kunde,
                Auto = Auto,
                RowVersion = Current.RowVersion
            });
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
