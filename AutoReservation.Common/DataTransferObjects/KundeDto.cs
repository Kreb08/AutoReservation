using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class KundeDto : BasisDto
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
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
    }
}
