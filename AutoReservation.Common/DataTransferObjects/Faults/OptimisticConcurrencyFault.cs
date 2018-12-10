using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class OptimisticConcurrencyFault<T>
    {
        [DataMember]
        public T CurrentEntity { get; set; }

        [DataMember]
        public T FaultEntity { get; set; }
    }
}
