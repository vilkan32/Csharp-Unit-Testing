using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;
using StorageMaster.Entities.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    class StorageMasterStructureTestsRam
    {

        [Test]
        public void TestRamConstructorSetsTheCorrectValues()
        {
            var ram = new Ram(60);

            Assert.AreEqual(ram.Price, 60, "Does not set the price correctly");
            Assert.AreEqual(ram.Weight, 0.1, "Does not set the weight correctly");

        }

        [Test]
        public void TestRamDoesNotSetNegativePrice()
        {
            Assert.Throws<InvalidOperationException>(() => new Ram(-60), "Sets negative price.");
        }

        [Test]
        public void TestRamFieldForPriceReturnsCorrectValue()
        {
            var type = typeof(Ram);

            var field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "price");

            var instance = (Product)Activator.CreateInstance(type, new object[] { 70 });

            Assert.AreEqual(field.GetValue(instance), 70, "Field does not set correctly.");
        }

    }
}
