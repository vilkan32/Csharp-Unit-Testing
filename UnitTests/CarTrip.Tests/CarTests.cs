using CarTrip;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace CarTrip.Tests
{
    [TestFixture]
    public class Tests
    {
        private Car car;
        [SetUp]
        public void SetUp()
        {
            this.car = new Car("Audi", 50, 40, 5.6);
        }

        [Test]
        public void TestIfConstructorSetsCorrectValueForModel()
        {
            Assert.AreEqual(car.Model, "Audi");
        }

        [Test]
        public void TestIfConstructorSetsCorrectValueTankCapacity()
        {
            Assert.AreEqual(car.TankCapacity, 50);
        }

        [Test]
        public void TestIfConstructorSetsCorrectValueTankAmount()
        {
            Assert.AreEqual(car.FuelAmount, 40);
        }

        [Test]
        public void TestIfConstructorSetsCorrectValueFuelConsumptionPerKm()
        {
            Assert.AreEqual(car.FuelConsumptionPerKm, 5.6);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        public void TestIfModelThrowsExceptionWhenValuesAreNullOrEmptyOrWhiteSpaces(string model)
        {
            Assert.Throws<ArgumentException>(() => new Car(model, 50, 40, 5.6));

        }

        [Test]
        [TestCase(51)]
        [TestCase(52)]
        [TestCase(60)]
        public void TestIfCarFuelAmountThrowsExceptionWhenSettingIncorrectValues(double fuelAmount)
        {
            Assert.Throws<ArgumentException>(() => new Car("Audi", 50, fuelAmount, 5.6));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void TestIfFuelConsumptionThrowsExceptionWhenValuesIsEqualOrLessThanZero(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car("Audi", 50, 40, fuelConsumption));
        }

        [Test]
        [TestCase(100)]
        [TestCase(50)]
        [TestCase(60)]
        public void TestIfDriveThrowsExceptionWhenTripAmountIsLargerThanFuelAmount(double driveDistance)
        {
            this.car = new Car("Audi", 50, 40, 10);

            Assert.Throws<InvalidOperationException>(() => car.Drive(driveDistance));
        }

        [Test]
        [TestCase(1, 30)]
        [TestCase(2, 20)]
        [TestCase(3, 10)]
        public void TestIfDriveSetsCorrectValueAfterDrivingThatDistance(double driveDistance, decimal remainingFuel)
        {
            this.car = new Car("Audi", 50, 40, 10);
            car.Drive(driveDistance);
            Assert.AreEqual(car.FuelAmount, remainingFuel);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestIfDriveSetsCorrectValueAfterDrivingThatDistance(double driveDistance)
        {
            this.car = new Car("Audi", 50, 40, 10);
            Assert.AreEqual(car.Drive(driveDistance), "Have a nice trip");
        }

        [Test]
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(50)]
        public void TestIfRefuelThrowsExceptionWhenExceedingTanksCapacity(double refuel)
        {
            this.car = new Car("Audi", 50, 40, 10);
            Assert.Throws<InvalidOperationException>(() => car.Refuel(refuel));
        }
        [Test]
        [TestCase(10, 20)]
        [TestCase(20, 30)]
        [TestCase(30, 40)]
        public void TestIfRefuelSetsCorrectValueForFuelAmount(double refuelAmount, double refuelValue)
        {
            this.car = new Car("Audi", 50, 10, 10);
            car.Refuel(refuelAmount);
            Assert.AreEqual(refuelValue, car.FuelAmount);
        }

        [Test]
        public void TestIfNameFieldSetsCorrectValue()
        {
            var type = typeof(Car);

            var instance = (Car)Activator.CreateInstance(type, new object[] { "Audi", 50, 40, 10 });

            var field = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(x => x.Name == "model");

            var value = field.GetValue(instance);

            Assert.AreEqual(value, "Audi");
        }

        [Test]
        public void TestIfFuelAmountFieldSetsCorrectValue()
        {
            var type = typeof(Car);

            var instance = (Car)Activator.CreateInstance(type, new object[] { "Audi", 50, 40, 10 });

            var field = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(x => x.Name == "fuelAmount");

            var value = (double)field.GetValue(instance);

            Assert.AreEqual(value, 40);
        }

        [Test]
        public void TestIfFuelConsumptionPerKmFieldSetsCorrectValue()
        {
            var type = typeof(Car);

            var instance = (Car)Activator.CreateInstance(type, new object[] { "Audi", 50, 40, 10 });

            var field = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(x => x.Name == "fuelConsumptionPerKm");

            var value = (double)field.GetValue(instance);

            Assert.AreEqual(value, 10);
        }
    }
}