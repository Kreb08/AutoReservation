using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto : BasisDto
    {
        private int _reservationsNr;
        public int ReservationsNr
        {
            get { return _reservationsNr; }
            set
            {
                if (_reservationsNr != value)
                {
                    _reservationsNr = value;
                    OnPropertyChanged(nameof(ReservationsNr));
                }
            }
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
                }
            }
        }

        private AutoDto _auto;
        public AutoDto Auto
        {
            get { return _auto; }
            set
            {
                if (_auto != value)
                {
                    _auto = value;
                    OnPropertyChanged(nameof(Auto));
                }
            }
        }

        private KundeDto _kunde;
        public KundeDto Kunde
        {
            get { return _kunde; }
            set
            {
                if (_kunde != value)
                {
                    _kunde = value;
                    OnPropertyChanged(nameof(Kunde));
                }
            }
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get { return _rowVersion; }
            set
            {
                if (_rowVersion != value)
                {
                    _rowVersion = value;
                    OnPropertyChanged(nameof(RowVersion));
                }
            }
        }

        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
    }
}
