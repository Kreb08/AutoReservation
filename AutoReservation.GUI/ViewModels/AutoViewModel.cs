using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.ObjectModel;

namespace AutoReservation.GUI.ViewModels
{
    public class AutoViewModel : BasisViewModel
    {
        private readonly ObservableCollection<AutoDto> _autos = new ObservableCollection<AutoDto>() {
            new AutoDto() { AutoKlasse = AutoKlasse.Standard, Marke = "Subaru", Tagestarif = 100 },
            new AutoDto() { AutoKlasse = AutoKlasse.Mittelklasse, Marke = "Audi", Tagestarif = 200 },
            new AutoDto() { AutoKlasse = AutoKlasse.Luxusklasse, Marke = "Bugatti", Tagestarif = 500, Basistarif=300 }
        };
        public ObservableCollection<AutoDto> Autos
        {
            get { return _autos; }
        }

        private AutoDto _selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set
            {
                if (_selectedAuto != value)
                {
                    _selectedAuto = value;
                    OnPropertyChanged(nameof(SelectedAuto));
                }
            }
        }
    }
}
