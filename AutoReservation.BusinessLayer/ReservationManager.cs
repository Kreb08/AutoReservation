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

        public List<Reservation> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Reservationen.Include(o => o.Auto).Include(o => o.Kunde).ToList();
                }
            }
        }

        public Reservation GetById(int id) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                return context.Reservationen.Include(o => o.Auto).Include(o => o.Kunde).SingleOrDefault(o => o.ReservationsNr == id);
            }
        }

        public Reservation Update(Reservation reservation) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                IsValid(context, reservation);
                context.Reservationen.Update(reservation);
                try {
                    context.SaveChanges();
                } catch (DbUpdateConcurrencyException) {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
                return GetById(reservation.ReservationsNr);
            }
        }

        public Reservation Insert(Reservation reservation) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                IsValid(context, reservation);
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
            return reservation;
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
            if (!AvailibilityCheck(reservation)) {
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

        public bool AvailibilityCheck(Reservation reservation) {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                List<Reservation> reservationen = context.Reservationen
                    .Where(r => reservation.AutoId == r.AutoId && reservation.ReservationsNr != r.ReservationsNr)
                    .ToList();
                foreach (Reservation res in reservationen)
                {
                    if (reservation.Von < res.Von && reservation.Bis > res.Von)
                    {
                        return false;
                    }
                    if (reservation.Von >= res.Von && reservation.Bis <= res.Bis)
                    {
                        return false;
                    }
                    if (reservation.Von < res.Bis && reservation.Bis > res.Bis)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}