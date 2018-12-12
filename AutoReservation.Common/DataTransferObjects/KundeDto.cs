using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : BasisDto
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

        private string _vorname;
        [DataMember]
        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (_vorname != value)
                {
                    _vorname = value;
                    OnPropertyChanged(nameof(Vorname));
                    OnPropertyChanged(nameof(Fullname));
                }
            }
        }

        private string _nachname;
        [DataMember]
        public string Nachname
        {
            get { return _nachname; }
            set
            {
                if (_nachname != value)
                {
                    _nachname = value;
                    OnPropertyChanged(nameof(Nachname));
                    OnPropertyChanged(nameof(Fullname));
                }
            }
        }

        private DateTime _geburtsdatum;
        [DataMember]
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

        public string Fullname
        {
            get { return Vorname + " " + Nachname; }
        }

        public override int getId()
        {
            return Id;
        }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
    }
}
