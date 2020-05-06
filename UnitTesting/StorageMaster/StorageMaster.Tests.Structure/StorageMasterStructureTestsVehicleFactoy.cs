using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    class StorageMasterStructureTestsVehicleFactoy
    {
        [Test]
        public void TestVehicleFactoryThrowsExceptionWhenTypeIsNull()
        {
            var vehicleFactory = new VehicleFactory();

            Assert.Throws<InvalidOperationException>(() => vehicleFactory.CreateVehicle(null), "Doesnt throw exception when the type is null.");
        }

        [Test]
        public void TestVehicleFactoryReturnsTheCorrectTypeOfVehicle()
        {
            var vehicleFactory = new VehicleFactory();

            Assert.AreEqual(vehicleFactory.CreateVehicle("Van").GetType().Name, typeof(Van).Name, "Doesnt return the correct vehicle.");
        }
       
    }
}
