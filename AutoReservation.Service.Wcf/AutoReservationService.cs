using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            autoManager.InsertAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            kundeManager.InsertKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            reservationManager.Insert(DtoConverter.ConvertToEntity(reservation));
        }

        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            autoManager.UpdateAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            kundeManager.UpdateKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            reservationManager.Update(DtoConverter.ConvertToEntity(reservation));
        }
    }
}