using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager : ManagerBase {

        public List<Kunde> List {
            get {
                using (AutoReservationContext context = new AutoReservationContext()) {
                    return context.Kunden.ToList();
                }
            }
        }

        public Kunde ReadKunde(int id) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                return context.Kunden.Find(id);
            }
        }

        public Kunde InsertKunde(Kunde kunde) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
            return kunde;
        }

        public Kunde UpdateKunde(Kunde kunde) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Kunden.Update(kunde);
                try {
                    context.SaveChanges();
                } catch (DbUpdateConcurrencyException) {
                    throw CreateOptimisticConcurrencyException(context, kunde);
                }
            }
            return kunde;
        }

        public void RemoveKunde(Kunde kunde) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Kunden.Remove(kunde);
                context.SaveChanges();
            }
        }
    }
}