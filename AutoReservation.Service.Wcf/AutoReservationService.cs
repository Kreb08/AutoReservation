using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoManager autoManager = new AutoManager();
        private KundeManager kundeManager = new KundeManager();
        private ReservationManager reservationManager = new ReservationManager();

        public List<AutoDto> Autos
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(autoManager.List);
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(kundeManager.List);
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                WriteActualMethod();
                return DtoConverter.ConvertToDtos(reservationManager.List);
            }
        }

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");



        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            autoManager.RemoveAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            kundeManager.RemoveKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            reservationManager.Remove(DtoConverter.ConvertToEntity(reservation));
        }



        public AutoDto GetAuto(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(autoManager.ReadAuto(id));
        }

        public KundeDto GetKunde(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(kundeManager.ReadKunde(id));
        }

        public ReservationDto GetReservation(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(reservationManager.GetById(id));
        }

        

        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(autoManager.InsertAuto(DtoConverter.ConvertToEntity(auto)));
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(kundeManager.InsertKunde(DtoConverter.ConvertToEntity(kunde)));
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(reservationManager.Insert(DtoConverter.ConvertToEntity(reservation)));
            }
            catch (InvalidDateException<Reservation>)
            {
                DateRangeFault fault = new DateRangeFault
                {
                    reservation = reservation
                };
                throw new FaultException<DateRangeFault>(fault);
            }
            catch (AutoUnavailableException<Reservation>)
            {
                AutoUnavailableFault fault = new AutoUnavailableFault
                {
                    reservation = reservation
                };
                throw new FaultException<AutoUnavailableFault>(fault);
            }
        }



        public AutoDto UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(autoManager.UpdateAuto(DtoConverter.ConvertToEntity(auto)));
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                OptimisticConcurrencyFault<AutoDto> fault = new OptimisticConcurrencyFault<AutoDto>
                {
                    FaultEntity = auto,
                    CurrentEntity = DtoConverter.ConvertToDto(autoManager.ReadAuto(auto.Id))
                };
                throw new FaultException<OptimisticConcurrencyFault<AutoDto>>(fault);
            }
            
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(kundeManager.UpdateKunde(DtoConverter.ConvertToEntity(kunde)));
            }
            catch (OptimisticConcurrencyException<Kunde>)
            {
                OptimisticConcurrencyFault<KundeDto> fault = new OptimisticConcurrencyFault<KundeDto>
                {
                    FaultEntity = kunde,
                    CurrentEntity = DtoConverter.ConvertToDto(kundeManager.ReadKunde(kunde.Id))
                };
                throw new FaultException<OptimisticConcurrencyFault<KundeDto>>(fault);
            }
            
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                return DtoConverter.ConvertToDto(reservationManager.Update(DtoConverter.ConvertToEntity(reservation)));
            }
            catch (OptimisticConcurrencyException<Reservation>)
            {
                OptimisticConcurrencyFault<ReservationDto> fault = new OptimisticConcurrencyFault<ReservationDto>
                {
                    FaultEntity = reservation,
                    CurrentEntity = DtoConverter.ConvertToDto(reservationManager.GetById(reservation.ReservationsNr))
                };
                throw new FaultException<OptimisticConcurrencyFault<ReservationDto>>(fault);
            }
            catch (InvalidDateException<Reservation>)
            {
                DateRangeFault fault = new DateRangeFault
                {
                    reservation = reservation
                };
                throw new FaultException<DateRangeFault>(fault);
            }
            catch (AutoUnavailableException<Reservation>)
            {
                AutoUnavailableFault fault = new AutoUnavailableFault
                {
                    reservation = reservation
                };
                throw new FaultException<AutoUnavailableFault>(fault);
            }
        }

        public bool CheckAvailibility(ReservationDto reservation)
        {
            WriteActualMethod();
            return reservationManager.AvailibilityCheck(DtoConverter.ConvertToEntity(reservation));
        }
    }
}