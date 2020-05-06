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
    class StorageMasterStructureTestsSolidStateDrive
    {
        [Test]
        public void TestSolidStateDriveConstructorSetsTheCorrectValues()
        {
            var solidStateDrive = new SolidStateDrive(60);

            Assert.AreEqual(solidStateDrive.Price, 60, "Does not set the price correctly");
            Assert.AreEqual(solidStateDrive.Weight, 0.2, "Does not set the weight correctly");

        }

        [Test]
        public void TestSolidStateDriveDoesNotSetNegativePrice()
        {
            Assert.Throws<InvalidOperationException>(() => new SolidStateDrive(-60), "Sets negative price.");
        }

        [Test]
        public void TestSolidStateDriveFieldForPriceReturnsCorrectValue()
        {
            var type = typeof(SolidStateDrive);

            var field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "price");

            var instance = (Product)Activator.CreateInstance(type, new object[] { 70 });

            Assert.AreEqual(field.GetValue(instance), 70, "Field does not set correctly.");
        }

    }
}
