using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class DateRangeFault
    {
        [DataMember]
        public ReservationDto reservation { get; set; }
    }
}
