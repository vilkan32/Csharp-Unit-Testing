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
    class StorageMasterStructureTestsWarehouse
    {
        [Test]
        public void TestWarehouseReturnsCorrectName()
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.AreEqual(warehouse.Name, "SmartSolutions", "Warehouse does not set the name correctly.");
        }

        [Test]
        public void TestWarehouseCapacityReturnsCorrectCapacity()
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.AreEqual(warehouse.Capacity, 10, "Warehouse does not set the correct capacity.");
        }


        [Test]
        public void TestWarehouseGarageSlotsReturnsCorrectCapacity()
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.AreEqual(warehouse.GarageSlots, 10, "Warehouse does not set the correct garage slots.");
        }

        [Test]
        public void TestWarehouseGetVehicleThrowsExceptionWhenAccessingIncorrectGarageSlot()
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => warehouse.GetVehicle(11), "Warehouse returns non existing vehicle.");
        }


        [Test]
        public void TestWarehouseGetVehicleThrowsExceptionWhenAccessingNullGarageSlot()
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => warehouse.GetVehicle(4), "Warehouse returns non existing vehicle.");
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestWarehouseGetVehicleReturnsExistingVehicleInTheGarrage(int target)
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.AreEqual(warehouse.GetVehicle(target).GetType().Name, typeof(Semi).Name, "Does not return the same type vehicle.");
        }

        [Test]
        public void TestWarehouseThrowsExceptionWhenDeliveryLocationIsfull()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var automatedWarehouse = new AutomatedWarehouse("SmartTech");

            warehouse.SendVehicleTo(0, automatedWarehouse);


            Assert.Throws<InvalidOperationException>(() => warehouse.SendVehicleTo(1, automatedWarehouse), "Doesnt throw exception when delivery location is full.");

        }

        [Test]
        public void TestWarehouseSendVehicleSendsVehicles()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var automatedWarehouse = new AutomatedWarehouse("SmartTech");

            warehouse.SendVehicleTo(0, automatedWarehouse);

            Assert.AreEqual(warehouse.Garage.ElementAt(0), null, "When sending vehicles doesnt reflect in the current Storage.");
       
        }

        [Test]
        public void TestWarehouseSendVehicleSendsVehiclesAndSetsTheDeliveryLocationCorrectly()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var automatedWarehouse = new AutomatedWarehouse("SmartTech");

            warehouse.SendVehicleTo(0, automatedWarehouse);

            Assert.AreEqual(automatedWarehouse.Garage.ElementAt(1).GetType().Name, typeof(Semi).Name, "When sending vehicles doesnt reflect in the delivery Storage.");

        }


        [Test]
        public void TestWarehouseSendVehicleSendsVehiclesAndReturnsTheCorrectSlot()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var automatedWarehouse = new AutomatedWarehouse("SmartTech");

            Assert.AreEqual(warehouse.SendVehicleTo(0, automatedWarehouse), 1, "Doesnt return the correct added slot from delivery location.");

        }

        [Test]
        public void TestWarehouseUnloadProductThrowsExceptionWhenStorageIsfull()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.UnloadVehicle(0);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            Assert.Throws<InvalidOperationException>(()=> warehouse.UnloadVehicle(0), "Doesnt Throw Exception when storage house if full.");

        }

        [Test]
        public void TestWarehouseUnloadProductReturnsTheCorrectNumberOfUnloadedProducts()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);

            Assert.AreEqual(warehouse.UnloadVehicle(0), 10, "Doesnt unload the correct number of products");
        }

        [Test]
       [TestCase(0)]
       [TestCase(1)]
       [TestCase(2)]
        public void TestInitializeGarageWarehouseMethordWorksCorrectly(int cases)
        {
            var type = typeof(Warehouse);
            IEnumerable<Vehicle> vehicles = new List<Vehicle>()
            {
                new Truck(),
                new Semi(),
                new Van()
            };
            var method = type.BaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "InitializeGarage");
            var instance = (Storage)Activator.CreateInstance(type, new object[] { "SmartSolutions"});
            var methodInvokation = method.Invoke(instance , new object[] { vehicles});
            Assert.AreEqual(instance.Garage.ElementAt(cases), vehicles.ElementAt(cases), "Doesnt set the correct vehicles in the garrage.");
        }

        [Test]
        public void TestWarehousePrivateAddVehicleMethodWorksCorreclty()
        {
            var type = typeof(Warehouse);
            var method = type.BaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "AddVehicle");
            var instance = (Storage)Activator.CreateInstance(type, new object[] { "SmartSolutions" });
            var methodInvokation = method.Invoke(instance, new object[] { new Truck() });
            Assert.AreEqual((int)methodInvokation + 1, instance.Garage.Where(x => x != null).Count(), "Doesnt set the correct index.");
            Console.WriteLine();
        }

        [Test]
        public void TestWarehousePropertyIsFullReturnsTrue()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.UnloadVehicle(0);
            Assert.IsTrue(warehouse.IsFull, "Warehouse should be full.");
        }

        [Test]
        public void TestWarehousePropertyIsFullReturnsFalse()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.UnloadVehicle(0);
            Assert.IsFalse(warehouse.IsFull, "Warehouse should not be full.");
        }

        [Test]
        public void TestWarehouseProperyProductsReturnsTheCorrectElements()
        {
            var warehouse = new Warehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            warehouse.GetVehicle(0).LoadProduct(hardDirve);
            warehouse.UnloadVehicle(0);
            Assert.AreEqual(warehouse.Products.ElementAt(0), hardDirve, "Product is not the same as expected.");
        }

        [Test]
        public void TestWarehouseProperyGarageReturnsTheCorrectVehicles()
        {
            var warehouse = new Warehouse("SmartSolutions");

            Assert.AreEqual(warehouse.Garage.ElementAt(0).GetType().Name, typeof(Semi).Name, "Garage does not return the correct vehicle.");
        }

    }
}
