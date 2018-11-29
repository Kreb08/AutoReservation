using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void UpdateReservationTestGood() {
            Reservation reservation = Target.GetById(2);
            reservation.Von.AddDays(1);
            Target.Update(reservation);
            Assert.Equal(reservation.Von, Target.GetById(2).Von);
        }

        [Fact]
        public void UpdateReservationTestBad() {
            Reservation reservation1 = Target.GetById(2);
            reservation1.Von.AddDays(1);

            Reservation reservation2 = Target.GetById(2);
            reservation2.Bis.AddDays(2);

            Target.Update(reservation1);

            Assert.Throws<OptimisticConcurrencyException<Reservation>>(() => Target.Update(reservation2));
        }

        [Fact]
        public void ReadReservationTest() {
            Reservation reservation = Target.GetById(2);
            Assert.Equal(2, reservation.AutoId);
        }

        [Fact]
        public void InsertReservationTest() {
            Reservation reservation = new Reservation() {
                AutoId = 4,
                KundeId = 4,
                Von = new DateTime(2018, 11, 29),
                Bis = new DateTime(2018, 12, 10)
            };
            Target.Insert(reservation);
            Assert.NotEqual(0, reservation.ReservationsNr);
        }

        [Fact]
        public void RemoveReservationTest() {
            Reservation reservation = Target.GetById(3);
            Target.Remove(reservation);
            Assert.Null(Target.GetById(3));
        }
    }
}
