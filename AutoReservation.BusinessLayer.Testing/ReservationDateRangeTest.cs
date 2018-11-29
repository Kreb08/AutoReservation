using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void ScenarioOkay01Test()
        {
            Reservation reservation = new Reservation {
                Von = new DateTime(2018, 5, 1),
                Bis = new DateTime(2018, 5, 2)
            };
            Assert.True(Target.DateRangeCheck(reservation));
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Reservation reservation = new Reservation {
                Von = new DateTime(2018, 2, 22),
                Bis = new DateTime(2018, 3, 10)
            };
            Assert.True(Target.DateRangeCheck(reservation));
        }

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            Reservation reservation = new Reservation {
                Von = new DateTime(2018, 5, 1, 8, 0, 0),
                Bis = new DateTime(2018, 5, 1, 8, 0, 0)
            };
            Assert.False(Target.DateRangeCheck(reservation));
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Reservation reservation = new Reservation {
                Von = new DateTime(2018, 5, 1, 18, 0, 0),
                Bis = new DateTime(2018, 5, 2, 6, 0, 0)
            };
            Assert.False(Target.DateRangeCheck(reservation));
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            Reservation reservation = new Reservation {
                Von = new DateTime(2018, 5, 1),
                Bis = new DateTime(2018, 4, 1)
            };
            Assert.False(Target.DateRangeCheck(reservation));
        }
    }
}
