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
    class StorageMasterStructureTestsHardDrive
    {
        [Test]
        public void TestHardDriveConstructorSetsTheCorrectValues()
        {
            var hardDrive = new HardDrive(60);

            Assert.AreEqual(hardDrive.Price, 60, "Does not set the price correctly");
            Assert.AreEqual(hardDrive.Weight, 1, "Does not set the weight correctly");

        }

        [Test]
        public void TestHardDriveDoesNotSetNegativePrice()
        {
            Assert.Throws<InvalidOperationException>(() => new HardDrive(-60), "Sets negative price.");
        }

        [Test]
        public void TestHardDriveFieldForPriceReturnsCorrectValue()
        {
            var type = typeof(HardDrive);

            var field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "price");

            var instance = (Product)Activator.CreateInstance(type, new object[] { 70 });

            Assert.AreEqual(field.GetValue(instance), 70, "Field does not set correctly.");
        }

    }
}
