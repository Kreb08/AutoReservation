namespace AutoReservation.Common.DataTransferObjects
{
    public class AutoDto : BasisDto
    {

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _marke;
        public string Marke
        {
            get { return _marke; }
            set
            {
                if(_marke != value)
                {
                    _marke = value;
                    OnPropertyChanged(nameof(Marke));
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
                }
            }
        }

        private int _basistarif;
        public int Basistarif
        {
            get { return _basistarif; }
            set
            {
                if (_basistarif != value)
                {
                    _basistarif = value;
                    OnPropertyChanged(nameof(Basistarif));
                }
            }
        }

        private AutoKlasse _autoKlasse;
        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set
            {
                if (_autoKlasse != value)
                {
                    _autoKlasse = value;
                    OnPropertyChanged(nameof(AutoKlasse));
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
            => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {RowVersion}";
    }
}
