using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        List<AutoDto> Autos {
            [OperationContract]
            get;
        }

        [OperationContract]
        AutoDto GetAuto(int id);

        [OperationContract]
        AutoDto InsertAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<AutoDto>))]
        AutoDto UpdateAuto(AutoDto auto);

        [OperationContract]
        void DeleteAuto(AutoDto auto);

        List<KundeDto> Kunden
        {
            [OperationContract]
            get;
        }

        [OperationContract]
        KundeDto GetKunde(int id);

        [OperationContract]
        KundeDto InsertKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<KundeDto>))]
        KundeDto UpdateKunde(KundeDto kunde);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        List<ReservationDto> Reservationen
        {
            [OperationContract]
            get;
        }

        [OperationContract]
        ReservationDto GetReservation(int id);

        [OperationContract]
        [FaultContract(typeof(DateRangeFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        ReservationDto InsertReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault<ReservationDto>))]
        [FaultContract(typeof(DateRangeFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        ReservationDto UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

        [OperationContract]
        bool CheckAvailibility(ReservationDto reservation);
    }
}
