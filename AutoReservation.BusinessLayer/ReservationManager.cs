using System;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase {

        public Reservation GetById(int id) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                return context.Reservationen.Find(id);
            }
        }

        public void Update(Reservation reservation) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                IsValid(context, reservation);
                context.Reservationen.Update(reservation);
                try {
                    context.SaveChanges();
                } catch (DbUpdateConcurrencyException) {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        public void Insert(Reservation reservation) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                IsValid(context, reservation);
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }

        public void Remove(Reservation reservation) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Reservationen.Remove(reservation);
                context.SaveChanges();
            }
        }

        public void IsValid(AutoReservationContext context, Reservation reservation) {
            if (!DateRangeCheck(reservation)) {
                throw CreateInvalidDateException(context, reservation);
            }
            if (!AvailibilityCheck(context, reservation)) {
                throw CreateAutoUnavailableException(context, reservation);
            }
        }

        public bool DateRangeCheck(Reservation reservation) {
            TimeSpan duration = reservation.Bis.Subtract(reservation.Von);
            if (duration.TotalHours < 24.0) {
                return false;
            }
            return true;
        }

        public bool AvailibilityCheck(AutoReservationContext context, Reservation reservation) {
            List<Reservation> reservationen = context.Reservationen
                .Where(r => reservation.AutoId == r.AutoId && reservation.ReservationsNr != r.ReservationsNr)
                .ToList();
            foreach(Reservation res in reservationen) {
                if(reservation.Von < res.Von && reservation.Bis > res.Von) {
                    return false;
                }
                if(reservation.Von >= res.Von && reservation.Bis <= res.Bis) {
                    return false;
                }
                if(reservation.Von < res.Bis && reservation.Bis > res.Bis) {
                    return false;
                }
            }
            return true;
        }
    }
}