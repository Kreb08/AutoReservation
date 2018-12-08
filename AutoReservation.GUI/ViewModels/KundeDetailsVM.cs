using AutoReservation.Common.DataTransferObjects;
using System;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class KundeDetailsVM : DetailsTemplate<KundeDto>
    {
        public KundeDetailsVM(Window window, KundeDto kunde = null) : base(window, kunde)
        {
            if (Current == null)
            {
                Current = new KundeDto() { Geburtsdatum = DateTime.Today };
            }
            Vorname = Current.Vorname;
            Nachname = Current.Nachname;
            Geburtsdatum = Current.Geburtsdatum;
        }

        private string _vorname;
        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (_vorname != value)
                {
                    _vorname = value;
                    OnPropertyChanged(nameof(Vorname));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private string _nachname;
        public string Nachname
        {
            get { return _nachname; }
            set
            {
                if (_nachname != value)
                {
                    _nachname = value;
                    OnPropertyChanged(nameof(Nachname));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private DateTime _geburtsdatum;
        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set
            {
                if (_geburtsdatum != value)
                {
                    _geburtsdatum = value;
                    OnPropertyChanged(nameof(Geburtsdatum));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public override bool CanCancle()
        {
            return true;
        }

        public override void Cancle()
        {
            Window.Close();
        }

        public override bool CanCommit()
        {
            if (Vorname == null || Vorname.Length == 0)
            {
                return false;
            }
            if (Nachname == null || Nachname.Length == 0)
            {
                return false;
            }
            return true;
        }

        public override void Commit()
        {
            if(Current.Id == 0)
            {
                AddNewKunde();
            }
            else
            {
                SaveKunde();
            }
            Window.Close();
        }

        public void AddNewKunde()
        {
            target.InsertKunde(new KundeDto()
            {
                Vorname = Vorname,
                Nachname = Nachname,
                Geburtsdatum = Geburtsdatum
            });
        }

        public void SaveKunde()
        {
            target.UpdateKunde(new KundeDto()
            {
                Id = Current.Id,
                Vorname = Vorname,
                Nachname = Nachname,
                Geburtsdatum = Geburtsdatum,
                RowVersion = Current.RowVersion
            });
        }
    }
}
