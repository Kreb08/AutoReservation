using System;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        public ReservationAvailabilityTest()
        {
            // Prepare reservation
            Reservation reservation = Target.GetById(1);
            reservation.Von = DateTime.Today;
            reservation.Bis = DateTime.Today.AddDays(1);
            Target.Update(reservation);
        }

        [Fact]
        public void ScenarioOkay01Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 1,
                Von = DateTime.Today.AddDays(3),
                Bis = DateTime.Today.AddDays(5)
            };
            Assert.True(Target.AvailibilityCheck(reservation));
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 1,
                Von = DateTime.Today.AddDays(-5),
                Bis = DateTime.Today.AddDays(-3)
            };
            Assert.True(Target.AvailibilityCheck(reservation));
        }

        [Fact]
        public void ScenarioOkay03Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 2,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(1)
            };
            Assert.True(Target.AvailibilityCheck(reservation));
        }

        /*
        [Fact]
        public void ScenarioOkay04Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }
        */

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 1,
                Von = DateTime.Today.AddDays(-1),
                Bis = DateTime.Today.AddHours(12)
            };
            Assert.False(Target.AvailibilityCheck(reservation));
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 1,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(1)
            };
            Assert.False(Target.AvailibilityCheck(reservation));
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 1,
                Von = DateTime.Today.AddHours(1),
                Bis = DateTime.Today.AddHours(12)
            };
            Assert.False(Target.AvailibilityCheck(reservation));
        }

        [Fact]
        public void ScenarioNotOkay04Test()
        {
            Reservation reservation = new Reservation {
                AutoId = 1,
                Von = DateTime.Today.AddHours(12),
                Bis = DateTime.Today.AddDays(2)
            };
            Assert.False(Target.AvailibilityCheck(reservation));
        }

        /*
        [Fact]
        public void ScenarioNotOkay05Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }
        */
    }
}
