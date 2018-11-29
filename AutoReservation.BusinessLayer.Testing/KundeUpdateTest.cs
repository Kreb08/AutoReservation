using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void UpdateKundeTestGood() {
            Kunde kunde = Target.ReadKunde(1);
            kunde.Nachname += 'G';
            Target.UpdateKunde(kunde);
            Assert.Equal(kunde.Nachname, Target.ReadKunde(1).Nachname);
        }

        [Fact]
        public void UpdateKundeTestBad() {
            Kunde kunde1 = Target.ReadKunde(1);
            kunde1.Nachname += 'G';

            Kunde kunde2 = Target.ReadKunde(1);
            kunde2.Nachname += 'T';

            Target.UpdateKunde(kunde1);
            Assert.Throws<OptimisticConcurrencyException<Kunde>>(() => Target.UpdateKunde(kunde2));
        }

        [Fact]
        public void ReadKundeTest() {
            Kunde kunde = Target.ReadKunde(1);
            Assert.Equal("Nass", kunde.Nachname);
        }

        [Fact]
        public void InsertKundeTest() {
            Kunde kunde = new Kunde() {
                Vorname = "Martin",
                Nachname = "Aston",
                Geburtsdatum = new DateTime(1984, 7, 3)

            };
            Target.InsertKunde(kunde);
            Assert.NotEqual(0, kunde.Id);
        }

        [Fact]
        public void RemoveKundeTest() {
            Kunde kunde = Target.ReadKunde(1);
            Target.RemoveKunde(kunde);
            Assert.Null(Target.ReadKunde(1));
        }
    }
}
