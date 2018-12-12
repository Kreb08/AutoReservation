using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.ServiceModel;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class KundeDetailsVM : DetailsTemplate<KundeDto>
    {
        public KundeDetailsVM(Window window, OverviewTemplate<KundeDto> parentVm, KundeDto kunde = null) : base(window, parentVm, kunde)
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
                if (!AddNewKunde())
                {
                    return;
                }
            }
            else
            {
                if (!SaveKunde())
                {
                    return;
                }
            }
            ParentVm.UpdateList();
            Window.Close();
        }

        public bool AddNewKunde()
        {
            try
            {
                target.InsertKunde(new KundeDto()
                {
                    Vorname = Vorname,
                    Nachname = Nachname,
                    Geburtsdatum = Geburtsdatum
                });
                return true;
            }
            catch (FaultException e)
            {
                MessageBox.Show(
                    "Hinzufügen Fehlgeschlagen.\n\n" +
                    e.Message,
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Ein Fehler ist aufgetreten.\n\n" +
                    e.Message,
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            return false;
        }

        public bool SaveKunde()
        {
            try
            {
                target.UpdateKunde(new KundeDto()
                {
                    Id = Current.Id,
                    Vorname = Vorname,
                    Nachname = Nachname,
                    Geburtsdatum = Geburtsdatum,
                    RowVersion = Current.RowVersion
                });
                return true;
            }
            catch (FaultException<OptimisticConcurrencyFault<KundeDto>> e)
            {
                MessageBox.Show(
                    "Kunde konnte nicht gespeichert werden.\n" +
                    "Kunde wurde in der Zwischenzeit bereits aktualisiert.\n\n" +
                    e.Detail.CurrentEntity.ToString() + "\n"+
                    e.Detail.FaultEntity.ToString(),
                    "Fehler beim Speichern",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (FaultException e)
            {
                MessageBox.Show(
                    "Speichern Fehlgeschlagen.\n\n" +
                    e.Message,
                    "Fehler beim Speichern",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Ein Fehler ist aufgetreten.\n\n" +
                    e.Message,
                    "Fehler beim Speichern",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            return false;
        }
    }
}
