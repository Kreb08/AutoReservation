using AutoReservation.Common.DataTransferObjects;
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
        void InsertAuto(AutoDto auto);
        [OperationContract]
        void UpdateAuto(AutoDto auto);
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
        void InsertKunde(KundeDto kunde);
        [OperationContract]
        void UpdateKunde(KundeDto kunde);
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
        void InsertReservation(ReservationDto reservation);
        [OperationContract]
        void UpdateReservation(ReservationDto reservation);
        [OperationContract]
        void DeleteReservation(ReservationDto reservation);
    }
}
