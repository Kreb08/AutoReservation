using System;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService Target { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            var autos = Target.Autos;
            Assert.Equal(4, autos.Count);
        }

        [Fact]
        public void GetKundenTest()
        {
            var kunden = Target.Kunden;
            Assert.Equal(4, kunden.Count);
        }

        [Fact]
        public void GetReservationenTest()
        {
            var reservationen = Target.Reservationen;
            Assert.Equal(4, reservationen.Count);
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.GetAuto(1);
            Assert.Equal("Fiat Punto", auto.Marke);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.GetKunde(1);
            Assert.Equal("Anna Nass", kunde.Vorname + " " + kunde.Nachname);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.GetReservation(1);
            Assert.Equal(1, reservation.Auto.Id);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.Null(Target.GetAuto(5));
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Null(Target.GetKunde(5));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.Null(Target.GetReservation(5));
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto
            {
                Marke = "Test Auto",
                AutoKlasse = AutoKlasse.Mittelklasse,
                Tagestarif = 60
            };
            auto = Target.InsertAuto(auto);
            Assert.NotEqual(0, auto.Id);
        }

        [Fact]
        public void InsertKundeTest()
        {
            KundeDto kunde = new KundeDto
            {
                Vorname = "Test",
                Nachname = "Kunde",
                Geburtsdatum = DateTime.Today
            };
            kunde = Target.InsertKunde(kunde);
            Assert.NotEqual(0, kunde.Id);
        }

        [Fact]
        public void InsertReservationTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(2),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            reservation = Target.InsertReservation(reservation);
            Assert.NotEqual(0, reservation.ReservationsNr);
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(Target.GetAuto(1));
            Assert.Null(Target.GetAuto(1));
        }

        [Fact]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(Target.GetKunde(1));
            Assert.Null(Target.GetKunde(1));
        }

        [Fact]
        public void DeleteReservationTest()
        {
            Target.DeleteReservation(Target.GetReservation(1));
            Assert.Null(Target.GetReservation(1));
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            AutoDto auto = Target.GetAuto(1);
            auto.Marke = "Test";
            auto = Target.UpdateAuto(auto);
            Assert.Equal("Test", auto.Marke);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            KundeDto kunde = Target.GetKunde(1);
            kunde.Vorname = "Test";
            kunde = Target.UpdateKunde(kunde);
            Assert.Equal("Test", kunde.Vorname);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            ReservationDto reservation = Target.GetReservation(1);
            reservation.Auto = Target.GetAuto(4);
            reservation = Target.UpdateReservation(reservation);
            Assert.Equal(4, reservation.Auto.Id);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto auto1 = Target.GetAuto(1);
            AutoDto auto2 = Target.GetAuto(1);

            auto1.Marke = "Test1";
            auto2.Marke = "Test2";

            Target.UpdateAuto(auto1);
            var exception = Assert.Throws<FaultException<OptimisticConcurrencyFault<AutoDto>>>(() => Target.UpdateAuto(auto2));
            //Assert.Equal("", exception.Detail.CurrentEntity.Marke);
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kunde1 = Target.GetKunde(1);
            KundeDto kunde2 = Target.GetKunde(1);

            kunde1.Vorname = "Test1";
            kunde2.Vorname = "Test2";

            Target.UpdateKunde(kunde1);
            Assert.Throws<FaultException<OptimisticConcurrencyFault<KundeDto>>>(() => Target.UpdateKunde(kunde2));
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto reservation1 = Target.GetReservation(1);
            ReservationDto reservation2 = Target.GetReservation(1);

            reservation1.Auto = Target.GetAuto(4);
            reservation2.Auto = Target.GetAuto(4);

            Target.UpdateReservation(reservation1);
            Assert.Throws<FaultException<OptimisticConcurrencyFault<ReservationDto>>>(() => Target.UpdateReservation(reservation2));
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Von = new DateTime(2018, 1, 10),
                Bis = new DateTime(2018, 1, 9),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Assert.Throws<FaultException<DateRangeFault>>(() => Target.InsertReservation(reservation));
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Von = new DateTime(2020, 1, 15),
                Bis = new DateTime(2020, 1, 25),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.InsertReservation(reservation));
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            ReservationDto reservation = Target.GetReservation(1);
            reservation.Bis = new DateTime(2020, 1, 9);
            Assert.Throws<FaultException<DateRangeFault>>(() => Target.UpdateReservation(reservation));
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            ReservationDto reservation = Target.GetReservation(1);
            reservation.Auto = Target.GetAuto(2);
            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.UpdateReservation(reservation));
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Von = new DateTime(2018, 1, 5),
                Bis = new DateTime(2018, 1, 15),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Assert.True(Target.CheckAvailibility(reservation));
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Von = new DateTime(2020, 1, 5),
                Bis = new DateTime(2020, 1, 15),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };
            Assert.False(Target.CheckAvailibility(reservation));
        }

        #endregion
    }
}
