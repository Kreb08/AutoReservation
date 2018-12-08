using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : BasisDto
    {

        private int _id;
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
