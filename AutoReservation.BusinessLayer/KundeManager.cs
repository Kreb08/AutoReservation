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

        public void InsertKunde(Kunde kunde) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
        }

        public void UpdateKunde(Kunde updated) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Kunden.Update(updated);
                try {
                    context.SaveChanges();
                } catch (DbUpdateConcurrencyException) {
                    throw CreateOptimisticConcurrencyException(context, updated);
                }
            }
        }

        public void RemoveKunde(Kunde kunde) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Kunden.Remove(kunde);
                context.SaveChanges();
            }
        }
    }
}