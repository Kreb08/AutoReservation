using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class AutoUnavailableFault
    {
        [DataMember]
        public ReservationDto reservation;
    }
}
