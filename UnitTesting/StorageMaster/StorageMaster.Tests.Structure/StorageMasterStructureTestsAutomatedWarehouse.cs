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
    class StorageMasterStructureTestsAutomatedAutomatedWarehouse
    {
        [Test]
        public void TestAutomatedWarehouseReturnsCorrectName()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.AreEqual(automatedWarehouse.Name, "SmartSolutions", "AutomatedWarehouse does not set the name correctly.");
        }

        [Test]
        public void TestAutomatedWarehouseCapacityReturnsCorrectCapacity()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.AreEqual(automatedWarehouse.Capacity, 1, "AutomatedWarehouse does not set the correct capacity.");
        }


        [Test]
        public void TestAutomatedWarehouseGarageSlotsReturnsCorrectCapacity()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.AreEqual(automatedWarehouse.GarageSlots, 2, "AutomatedWarehouse does not set the correct garage slots.");
        }

        [Test]
        public void TestAutomatedWarehouseGetVehicleThrowsExceptionWhenAccessingIncorrectGarageSlot()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => automatedWarehouse.GetVehicle(11), "AutomatedWarehouse returns non existing vehicle.");
        }


        [Test]
        public void TestAutomatedWarehouseGetVehicleThrowsExceptionWhenAccessingNullGarageSlot()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => automatedWarehouse.GetVehicle(1), "AutomatedWarehouse returns non existing vehicle.");
        }

        [Test]
        [TestCase(0)]
        public void TestAutomatedWarehouseGetVehicleReturnsExistingVehicleInTheGarrage(int target)
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.AreEqual(automatedWarehouse.GetVehicle(target).GetType().Name, typeof(Truck).Name, "Does not return the same type vehicle.");
        }

        [Test]
        public void TestAutomatedWarehouseThrowsExceptionWhenDeliveryLocationIsfull()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var distributionCenter = new DistributionCenter("SmartTech");
            var warehouse = new Warehouse("SmartINC");
            warehouse.SendVehicleTo(0, distributionCenter);
            warehouse.SendVehicleTo(1, distributionCenter);

            Assert.Throws<InvalidOperationException>(() => automatedWarehouse.SendVehicleTo(0, distributionCenter), "Doesnt throw exception when delivery location is full.");

        }

        [Test]
        public void TestAutomatedWarehouseSendVehicleSendsVehicles()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var distributionCenter = new DistributionCenter("SmartTech");
            automatedWarehouse.SendVehicleTo(0, distributionCenter);

            Assert.AreEqual(automatedWarehouse.Garage.ElementAt(0), null, "When sending vehicles doesnt reflect in the current Storage.");

        }

        [Test]
        public void TestAutomatedWarehouseSendVehicleSendsVehiclesAndSetsTheDeliveryLocationCorrectly()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var distributionCenter = new DistributionCenter("SmartTech");

            automatedWarehouse.SendVehicleTo(0, distributionCenter);

            Assert.AreEqual(distributionCenter.Garage.ElementAt(3).GetType().Name, typeof(Truck).Name, "When sending vehicles doesnt reflect in the delivery Storage.");

        }


        [Test]
        public void TestAutomatedWarehouseSendVehicleSendsVehiclesAndReturnsTheCorrectSlot()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var distributionCenter = new DistributionCenter("SmartTech");


            Assert.AreEqual(automatedWarehouse.SendVehicleTo(0, distributionCenter), 3, "Doesnt return the correct added slot from delivery location.");

        }

        [Test]
        public void TestAutomatedWarehouseUnloadProductThrowsExceptionWhenStorageIsfull()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.UnloadVehicle(0);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            Assert.Throws<InvalidOperationException>(() => automatedWarehouse.UnloadVehicle(0), "Doesnt Throw Exception when storage house if full.");

        }

        [Test]
        public void TestAutomatedWarehouseUnloadProductReturnsTheCorrectNumberOfUnloadedProducts()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var ram = new Ram(123);
            automatedWarehouse.GetVehicle(0).LoadProduct(ram);
            automatedWarehouse.GetVehicle(0).LoadProduct(ram);
            automatedWarehouse.GetVehicle(0).LoadProduct(ram);
            automatedWarehouse.GetVehicle(0).LoadProduct(ram);
            automatedWarehouse.GetVehicle(0).LoadProduct(ram);

            Assert.AreEqual(automatedWarehouse.UnloadVehicle(0), 5, "Doesnt unload the correct number of products");
        }

        [Test]
        [TestCase(0)]
        public void TestInitializeGarageAutomatedWarehouseMethordWorksCorrectly(int cases)
        {
            var type = typeof(AutomatedWarehouse);
            IEnumerable<Vehicle> vehicles = new List<Vehicle>()
            {
                new Truck(),
            };
            var method = type.BaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "InitializeGarage");
            var instance = (Storage)Activator.CreateInstance(type, new object[] { "SmartSolutions" });
            var methodInvokation = method.Invoke(instance, new object[] { vehicles });
            Assert.AreEqual(instance.Garage.ElementAt(cases), vehicles.ElementAt(cases), "Doesnt set the correct vehicles in the garrage.");
        }

        [Test]
        public void TestAutomatedWarehousePrivateAddVehicleMethodWorksCorreclty()
        {
            var type = typeof(AutomatedWarehouse);
            var method = type.BaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "AddVehicle");
            var instance = (Storage)Activator.CreateInstance(type, new object[] { "SmartSolutions" });
            var methodInvokation = method.Invoke(instance, new object[] { new Truck() });
            Assert.AreEqual((int)methodInvokation + 1, instance.Garage.Where(x => x != null).Count(), "Doesnt set the correct index.");
            Console.WriteLine();
        }

        [Test]
        public void TestAutomatedWarehousePropertyIsFullReturnsTrue()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.UnloadVehicle(0);
            Assert.IsTrue(automatedWarehouse.IsFull, "AutomatedWarehouse should be full.");
        }

        [Test]
        public void TestAutomatedWarehousePropertyIsFullReturnsFalse()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var ram = new Ram(123);
            automatedWarehouse.GetVehicle(0).LoadProduct(ram);
            automatedWarehouse.UnloadVehicle(0);
            Assert.IsFalse(automatedWarehouse.IsFull, "AutomatedWarehouse should not be full.");
        }

        [Test]
        public void TestAutomatedWarehouseProperyProductsReturnsTheCorrectElements()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");
            var hardDirve = new HardDrive(123);
            automatedWarehouse.GetVehicle(0).LoadProduct(hardDirve);
            automatedWarehouse.UnloadVehicle(0);
            Assert.AreEqual(automatedWarehouse.Products.ElementAt(0), hardDirve, "Product is not the same as expected.");
        }

        [Test]
        public void TestAutomatedWarehouseProperyGarageReturnsTheCorrectVehicles()
        {
            var automatedWarehouse = new AutomatedWarehouse("SmartSolutions");

            Assert.AreEqual(automatedWarehouse.Garage.ElementAt(0).GetType().Name, typeof(Truck).Name, "Garage does not return the correct vehicle.");
        }


    }
}
