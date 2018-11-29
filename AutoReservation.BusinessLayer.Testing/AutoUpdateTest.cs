using System;
using System.Collections.Generic;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTestGood() {
            Auto auto = Target.ReadAuto(1);
            auto.Tagestarif += 10;
            Target.UpdateAuto(auto);
            Assert.Equal(auto.Tagestarif, Target.ReadAuto(1).Tagestarif);
        }

        [Fact]
        public void UpdateAutoTestBad()
        {
            Auto auto1 = Target.ReadAuto(1);
            auto1.Tagestarif += 10;

            Auto auto2 = Target.ReadAuto(1);
            auto2.Tagestarif += 20;

            Target.UpdateAuto(auto1);

            Assert.Throws<OptimisticConcurrencyException<Auto>>(() => Target.UpdateAuto(auto2));
        }

        [Fact]
        public void ReadAutoTest() {
            Auto auto = Target.ReadAuto(1);
            Assert.Equal("Fiat Punto", auto.Marke);
        }

        [Fact]
        public void InsertAutoTest() {
            Auto auto = new LuxusklasseAuto() {
                Marke = "Aston Martin",
                Tagestarif = 500,
                Basistarif = 100
            };
            Target.InsertAuto(auto);
            Assert.NotEqual(0, auto.Id);
        }

        [Fact]
        public void RemoveAutoTest() {
            Auto auto = Target.ReadAuto(1);
            Target.RemoveAuto(auto);
            Assert.Null(Target.ReadAuto(1));
        }
    }
}
