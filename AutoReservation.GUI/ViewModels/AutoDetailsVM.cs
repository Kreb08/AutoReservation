using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.ServiceModel;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class AutoDetailsVM : DetailsTemplate<AutoDto>
    {
        public AutoDetailsVM(Window window, OverviewTemplate<AutoDto> parentVm, AutoDto auto = null) : base(window, parentVm, auto)
        {
            if(Current == null)
            {
                Current = new AutoDto();
            }
            Marke = Current.Marke;
            Tagestarif = Current.Tagestarif;
            Basistarif = Current.Basistarif;
            AutoKlasse = Current.AutoKlasse;
        }

        private string _marke;
        public string Marke
        {
            get { return _marke; }
            set
            {
                if (_marke != value)
                {
                    _marke = value;
                    OnPropertyChanged(nameof(Marke));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private int _tagestarif;
        public int Tagestarif
        {
            get { return _tagestarif; }
            set
            {
                if (_tagestarif != value)
                {
                    _tagestarif = value;
                    OnPropertyChanged(nameof(Tagestarif));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public int _basistarif;
        public int Basistarif
        {
            get { return _basistarif; }
            set
            {
                if (_basistarif != value)
                {
                    _basistarif = value;
                    OnPropertyChanged(nameof(Basistarif));
                    CommitCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public AutoKlasse _autoKlasse;
        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set
            {
                if (_autoKlasse != value)
                {
                    _autoKlasse = value;
                    OnPropertyChanged(nameof(AutoKlasse));
                    CommitCommand?.RaiseCanExecuteChanged();
                    if (_autoKlasse == AutoKlasse.Luxusklasse)
                    {
                        IsBasistarifEnabled = true;
                    } else
                    {
                        IsBasistarifEnabled = false;
                    }
                }
            }
        }

        public bool _isBasistarifEnabled;
        public bool IsBasistarifEnabled
        {
            get { return _isBasistarifEnabled; }
            set
            {
                if(_isBasistarifEnabled != value)
                {
                    _isBasistarifEnabled = value;
                    OnPropertyChanged(nameof(IsBasistarifEnabled));
                }
            }
        }

        public override bool CanCommit()
        {
            if (Marke == null || Marke.Length == 0)
            {
                return false;
            }
            if (Tagestarif <= 0)
            {
                return false;
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && Basistarif <= 0)
            {
                return false;
            }
            return true;
        }

        public override void Commit()
        {
            if(Current.Id == 0)
            {
                if (!AddNewAuto())
                {
                    return;
                }
            }
            else
            {
                if (!SaveAuto())
                {
                    return;
                }
            }
            ParentVm.UpdateList();
            Window.Close();
        }

        public bool AddNewAuto()
        {
            try
            {
                target.InsertAuto(new AutoDto()
                {
                    Marke = Marke,
                    Tagestarif = Tagestarif,
                    Basistarif = Basistarif,
                    AutoKlasse = AutoKlasse
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
                    "Ein Fehler ist aufgetreten.\n\n"+
                    e.Message,
                    "Fehler beim Hinzufügen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            return false;
        }

        public bool SaveAuto()
        {
            try
            {
                target.UpdateAuto(new AutoDto()
                {
                    Id = Current.Id,
                    Marke = Marke,
                    Tagestarif = Tagestarif,
                    Basistarif = Basistarif,
                    AutoKlasse = AutoKlasse,
                    RowVersion = Current.RowVersion
                });
                return true;
            }
            catch (FaultException<OptimisticConcurrencyFault<AutoDto>> e)
            {
                MessageBox.Show(
                    "Auto konnte nicht gespeichert werden.\n" +
                    "Auto wurde in der Zwischenzeit bereits aktualisiert.\n\n" +
                    e.Detail.CurrentEntity.ToString() + "\n" +
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
