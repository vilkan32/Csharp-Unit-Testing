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
    class StorageMasterStructureTestsDistributionCenter
    {


        [Test]
        public void TestDistributionCenterReturnsCorrectName()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.AreEqual(distributionCenter.Name, "SmartSolutions", "DistributionCenter does not set the name correctly.");
        }

        [Test]
        public void TestDistributionCenterCapacityReturnsCorrectCapacity()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.AreEqual(distributionCenter.Capacity, 2, "DistributionCenter does not set the correct capacity.");
        }


        [Test]
        public void TestDistributionCenterGarageSlotsReturnsCorrectCapacity()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.AreEqual(distributionCenter.GarageSlots, 5, "DistributionCenter does not set the correct garage slots.");
        }

        [Test]
        public void TestDistributionCenterGetVehicleThrowsExceptionWhenAccessingIncorrectGarageSlot()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => distributionCenter.GetVehicle(11), "DistributionCenter returns non existing vehicle.");
        }


        [Test]
        public void TestDistributionCenterGetVehicleThrowsExceptionWhenAccessingNullGarageSlot()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => distributionCenter.GetVehicle(4), "DistributionCenter returns non existing vehicle.");
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestDistributionCenterGetVehicleReturnsExistingVehicleInTheGarrage(int target)
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.AreEqual(distributionCenter.GetVehicle(target).GetType().Name, typeof(Van).Name, "Does not return the same type vehicle.");
        }

        [Test]
        public void TestDistributionCenterThrowsExceptionWhenDeliveryLocationIsfull()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var automatedDistributionCenter = new AutomatedWarehouse("SmartTech");

            distributionCenter.SendVehicleTo(0, automatedDistributionCenter);


            Assert.Throws<InvalidOperationException>(() => distributionCenter.SendVehicleTo(1, automatedDistributionCenter), "Doesnt throw exception when delivery location is full.");

        }

        [Test]
        public void TestDistributionCenterSendVehicleSendsVehicles()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var automatedDistributionCenter = new AutomatedWarehouse("SmartTech");

            distributionCenter.SendVehicleTo(0, automatedDistributionCenter);

            Assert.AreEqual(distributionCenter.Garage.ElementAt(0), null, "When sending vehicles doesnt reflect in the current Storage.");

        }

        [Test]
        public void TestDistributionCenterSendVehicleSendsVehiclesAndSetsTheDeliveryLocationCorrectly()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var automatedDistributionCenter = new AutomatedWarehouse("SmartTech");

            distributionCenter.SendVehicleTo(0, automatedDistributionCenter);

            Assert.AreEqual(automatedDistributionCenter.Garage.ElementAt(1).GetType().Name, typeof(Van).Name, "When sending vehicles doesnt reflect in the delivery Storage.");

        }


        [Test]
        public void TestDistributionCenterSendVehicleSendsVehiclesAndReturnsTheCorrectSlot()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var automatedDistributionCenter = new AutomatedWarehouse("SmartTech");

            Assert.AreEqual(distributionCenter.SendVehicleTo(0, automatedDistributionCenter), 1, "Doesnt return the correct added slot from delivery location.");

        }

        [Test]
        public void TestDistributionCenterUnloadProductThrowsExceptionWhenStorageIsfull()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var hardDirve = new HardDrive(123);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);
            distributionCenter.UnloadVehicle(0);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);
            Assert.Throws<InvalidOperationException>(() => distributionCenter.UnloadVehicle(0), "Doesnt Throw Exception when storage house if full.");

        }

        [Test]
        public void TestDistributionCenterUnloadProductReturnsTheCorrectNumberOfUnloadedProducts()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var hardDirve = new HardDrive(123);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);

            Assert.AreEqual(distributionCenter.UnloadVehicle(0), 2, "Doesnt unload the correct number of products");
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestInitializeGarageDistributionCenterMethordWorksCorrectly(int cases)
        {
            var type = typeof(DistributionCenter);
            IEnumerable<Vehicle> vehicles = new List<Vehicle>()
            {
                new Truck(),
                new Semi(),
                new Van()
            };
            var method = type.BaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "InitializeGarage");
            var instance = (Storage)Activator.CreateInstance(type, new object[] { "SmartSolutions" });
            var methodInvokation = method.Invoke(instance, new object[] { vehicles });
            Assert.AreEqual(instance.Garage.ElementAt(cases), vehicles.ElementAt(cases), "Doesnt set the correct vehicles in the garrage.");
        }

        [Test]
        public void TestDistributionCenterPrivateAddVehicleMethodWorksCorreclty()
        {
            var type = typeof(DistributionCenter);
            var method = type.BaseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "AddVehicle");
            var instance = (Storage)Activator.CreateInstance(type, new object[] { "SmartSolutions" });
            var methodInvokation = method.Invoke(instance, new object[] { new Truck() });
            Assert.AreEqual((int)methodInvokation + 1, instance.Garage.Where(x => x != null).Count(), "Doesnt set the correct index.");
            Console.WriteLine();
        }

        [Test]
        public void TestDistributionCenterPropertyIsFullReturnsTrue()
        {
            var DistributionCenter = new DistributionCenter("SmartSolutions");
            var hardDirve = new HardDrive(123);
            DistributionCenter.GetVehicle(0).LoadProduct(hardDirve);
            DistributionCenter.GetVehicle(0).LoadProduct(hardDirve);        
            DistributionCenter.UnloadVehicle(0);
            Assert.IsTrue(DistributionCenter.IsFull, "DistributionCenter should be full.");
        }

        [Test]
        public void TestDistributionCenterPropertyIsFullReturnsFalse()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var hardDirve = new HardDrive(123);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);           
            distributionCenter.UnloadVehicle(0);
            Assert.IsFalse(distributionCenter.IsFull, "DistributionCenter should not be full.");
        }

        [Test]
        public void TestDistributionCenterProperyProductsReturnsTheCorrectElements()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");
            var hardDirve = new HardDrive(123);
            distributionCenter.GetVehicle(0).LoadProduct(hardDirve);
            distributionCenter.UnloadVehicle(0);
            Assert.AreEqual(distributionCenter.Products.ElementAt(0), hardDirve, "Product is not the same as expected.");
        }

        [Test]
        public void TestDistributionCenterProperyGarageReturnsTheCorrectVehicles()
        {
            var distributionCenter = new DistributionCenter("SmartSolutions");

            Assert.AreEqual(distributionCenter.Garage.ElementAt(0).GetType().Name, typeof(Van).Name, "Garage does not return the correct vehicle.");
        }


    }
}
