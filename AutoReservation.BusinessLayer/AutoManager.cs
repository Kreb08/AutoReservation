using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase {

        public List<Auto> List {
            get {
                using (AutoReservationContext context = new AutoReservationContext()) {
                    return context.Autos.ToList();
                }
            }
        }

        public Auto ReadAuto(int id) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                return context.Autos.Find(id);
            }
        }

        public void InsertAuto(Auto auto) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Autos.Add(auto);
                context.SaveChanges();
            }
        }

        public void UpdateAuto(Auto updated) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Autos.Update(updated);
                try {
                    context.SaveChanges();
                } catch (DbUpdateConcurrencyException) {
                    throw CreateOptimisticConcurrencyException(context, updated);
                }
            }
        }

        public void RemoveAuto(Auto auto) {
            using (AutoReservationContext context = new AutoReservationContext()) {
                context.Autos.Remove(auto);
                context.SaveChanges();
            }
        }
    }
}