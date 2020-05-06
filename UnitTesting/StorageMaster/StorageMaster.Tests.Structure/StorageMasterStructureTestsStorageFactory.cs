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
    class StorageMasterStructureTestsStorageFactory
    {
        [Test]
        public void TestStorageFactoryThrowsExceptionWhenTypeIsNull()
        {
            var storageFactory = new StorageFactory();

            Assert.Throws<InvalidOperationException>(() => storageFactory.CreateStorage(null, "SmartTech"), "Doesnt throw exception when the type is null.");
        }

        [Test]
        public void TestWarehouseFactoryReturnsTheCorrectTypeOfVehicle()
        {
            var storageFactory = new StorageFactory();

            Assert.AreEqual(storageFactory.CreateStorage("Warehouse", "SmartSolutions").GetType().Name, typeof(Warehouse).Name, "Doesnt return the correct storage place.");

        }
        [Test]
        public void TestWarehouseFactoryReturnsTheCorrectNameOfTheWarehouse()
        {
            var storageFactory = new StorageFactory();

            Assert.AreEqual(storageFactory.CreateStorage("Warehouse", "SmartSolutions").Name, "SmartSolutions", "Doesnt return the correct name of the storage place.");

        }


    }
}
