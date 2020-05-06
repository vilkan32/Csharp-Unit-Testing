using NUnit.Framework;
using System;
using System.Reflection;
namespace StorageMaster.Tests.BusinessLogic
{
    using StorageMaster.Core;
    using StorageMaster.Entities.Products;
    using StorageMaster.Entities.Storage;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    [TestFixture]
    public class StorageMasterTestsBusinessLogicStorageMaster
    {
        private StorageMaster storageMaster;
        [SetUp]
        public void Setup()
        {
            this.storageMaster = new StorageMaster();
        }
        
        [Test]
        public void StorageMasterTestAddProductReturnsCorrectMessage()
        {
            Assert.AreEqual(this.storageMaster.AddProduct("Ram", 60), "Added Ram to pool", "Doesnt return the correct message.");
        }

        [Test]
        public void StorageMasterTestAddProductAddProductToProductPool()
        {
            var type = typeof(StorageMaster);

            var instance = (StorageMaster)Activator.CreateInstance(type);

            instance.AddProduct("Ram", 60);

            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "productsPool");

            var info = (Dictionary<string, Stack<Product>>)field.GetValue(instance);
            Assert.AreEqual(info.Count, 1, "Does not add products to the field");
            foreach (var item in info)
            {
                Assert.AreEqual(item.Key, "Ram", "Key does not set correctly.");
                foreach (var value in item.Value)
                {
                    Assert.AreEqual(value.Price, 60, "Product price does not set correctly.");
                    Assert.AreEqual(value.Weight, 0.1, "Product weight does not set correctly.");
                }
            }
        }

        [Test]
        public void StorageMasterRegisterStorageReturnsCorrectMessage()
        {


            Assert.AreEqual(storageMaster.RegisterStorage("Warehouse", "SmartSolutions"), "Registered SmartSolutions", "Doesnt return the correct message when registering a storage.");
        }

        [Test]
        public void StorageMasterRegisterStorageSetsTheFieldStorageRegistryCorrectly()
        {
            var type = typeof(StorageMaster);

            var instance = (StorageMaster)Activator.CreateInstance(type);
            instance.RegisterStorage("Warehouse", "SmartSolutions");

            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "storageRegistry");

            var info = (Dictionary<string, Storage>)field.GetValue(instance);
            Assert.AreEqual(info.Count, 1, "Doesnt set the count correctly.");
            foreach (var item in info)
            {
                Assert.AreEqual(item.Key, "SmartSolutions", "Doesnt set the keys correctly.");
                Assert.AreEqual(item.Value.Name, "SmartSolutions", "Doesnt set the value correctly.");
            }

        }

        [Test]
        public void StorageMasterSelectVehicleGetsTheCorrectVehicleFromGarageSlot()
        {

            storageMaster.RegisterStorage("Warehouse", "SmartSolutions");

            Assert.AreEqual(storageMaster.SelectVehicle("SmartSolutions", 1), "Selected Semi", "Doesnt find the correct vehicle");
        }

        [Test]
        public void StorageMasterSelectVehicleThrowsExceptionWhenAccessingGarageSlotThatDoesNotHaveVehicles()
        {

            storageMaster.RegisterStorage("Warehouse", "SmartSolutions");

            Assert.Throws<InvalidOperationException>(() => storageMaster.SelectVehicle("SmartSolutions", 11), "Doesnt throw exception when accessing incorrect garage slot.");
        }

        [Test]
        public void StorageMasterSelectVehicleSetsTheVehicleFieldCorrectly()
        {
            var type = typeof(StorageMaster);

            var instance = (StorageMaster)Activator.CreateInstance(type);

            instance.RegisterStorage("Warehouse", "SmartSolutions");

            instance.SelectVehicle("SmartSolutions", 1);

            var field = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Last();

            Assert.AreEqual(field.GetValue(instance).GetType().Name, "Semi", "Doesnt set the current vehicle correctly.");
        }

        [Test]
        public void StorageMasterLoadVehicleReturnsCorrectMessage()
        {

            this.storageMaster.AddProduct("Ram", 60);
            this.storageMaster.AddProduct("HardDrive", 70);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Gpu", 100);
            this.storageMaster.AddProduct("Ram", 60);
            this.storageMaster.AddProduct("HardDrive", 70);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.AddProduct("HardDrive", 100);
            this.storageMaster.RegisterStorage("Warehouse", "SmartSolutions");
            this.storageMaster.SelectVehicle("SmartSolutions", 2);
            List<string> products = new List<string>() { "Ram", "HardDrive", "Gpu" };
            List<string> productsTestTwo = new List<string>() { "Ram", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive", "HardDrive" };
            var result = this.storageMaster.LoadVehicle(products);
            Assert.AreEqual(result, "Loaded 3/3 products into Semi", "Doesnt load the correct number of products into to vehicle.");
            var resultTwo = this.storageMaster.LoadVehicle(productsTestTwo);
            Assert.AreEqual(resultTwo, "Loaded 10/12 products into Semi", "Doesnt load the correct number of items as per the capacity of the vehicle");
        }

        [Test]
        public void StorageMasterLoadVehicleThrowsExceptionWhenProductIsNotAdded()
        {
            var type = typeof(StorageMaster);

            var instance = (StorageMaster)Activator.CreateInstance(type);

            instance.RegisterStorage("DistributionCenter", "SmartSolutions");


            instance.SelectVehicle("SmartSolutions", 0);
            List<string> products = new List<string>() { "Ram", "HardDrive", "Gpu", "SolidStateDrive" };
            Assert.Throws<InvalidOperationException>(() => instance.LoadVehicle(products), "Doesnt throw exception when the product is not in the pool.");

        }

        [Test]
        public void StorageMasterSendVehicleToSendsVehicleToTheCorrectDestination()
        {

            this.storageMaster.RegisterStorage("Warehouse", "SmartSolutions");
            this.storageMaster.RegisterStorage("DistributionCenter", "SmartTech");

            var result = this.storageMaster.SendVehicleTo("SmartSolutions", 0, "SmartTech");
            Assert.AreEqual(result, "Sent Semi to SmartTech (slot 3)");
        }

        [Test]
        public void StorageMasterSendVehicleToThrowsExceptionWhenSourceIsMissing()
        {

            this.storageMaster.RegisterStorage("DistributionCenter", "SmartTech");
            Assert.Throws<InvalidOperationException>(() => this.storageMaster.SendVehicleTo("SmartSolutions", 0, "SmartTech"), "Doesnt throw exception when source storage is missing.");
        }

        [Test]
        public void StorageMasterSendVehicleToThrowsExceptionWhenDestinationIsMissing()
        {

            this.storageMaster.RegisterStorage("Warehouse", "SmartSolutions");
            Assert.Throws<InvalidOperationException>(() => this.storageMaster.SendVehicleTo("SmartSolutions", 0, "SmartTech"), "Doesnt throw exception when source storage is missing.");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(0)]
        public void StorageMasterUnloadVehicleUnloadsTheCorrectNumberOfProducts(int cases)
        {

            this.storageMaster.AddProduct("Ram", 123);
            this.storageMaster.AddProduct("HardDrive", 120);
            this.storageMaster.AddProduct("SolidStateDrive", 120);
            this.storageMaster.RegisterStorage("DistributionCenter", "SmartByu");
            this.storageMaster.SelectVehicle("SmartByu",cases);
            this.storageMaster.RegisterStorage("DistributionCenter", "SmartTech");
            this.storageMaster.LoadVehicle(new List<string>() { "Ram", "HardDrive" });
            this.storageMaster.SendVehicleTo("SmartByu", cases, "SmartTech");
            
           Assert.AreEqual(this.storageMaster.UnloadVehicle("SmartTech", 3), "Unloaded 2/2 products at SmartTech", "Doesnt return the correct number of unloaded products");
        }

        [Test]
        public void StorageMasterGetStorageStatusTesetReturnsTheCorrectValue()
        {
           
            this.storageMaster.AddProduct("Ram", 123);
            this.storageMaster.AddProduct("HardDrive", 120);
            this.storageMaster.AddProduct("Gpu", 125);
            this.storageMaster.AddProduct("SolidStateDrive", 200);
            this.storageMaster.RegisterStorage("Warehouse", "SmartSolutions");
            this.storageMaster.SelectVehicle("SmartSolutions", 2);
            List<string> products = new List<string>() { "Ram", "HardDrive", "Gpu", "SolidStateDrive" };
            this.storageMaster.LoadVehicle(products);
            this.storageMaster.UnloadVehicle("SmartSolutions", 2);

            var result = storageMaster.GetStorageStatus("SmartSolutions");

            Assert.AreEqual(result, "Stock (2/10): [Gpu (1), HardDrive (1), Ram (1), SolidStateDrive (1)]\r\nGarage: [Semi|Semi|Semi|empty|empty|empty|empty|empty|empty|empty]", "Doesnt return the correct status.");
        }

        [Test]
        public void StorageMasterGetSummaryReturnsCorrectValue()
        {
            this.storageMaster.AddProduct("Ram", 123);
            this.storageMaster.AddProduct("HardDrive", 120);
            this.storageMaster.AddProduct("Gpu", 125);
            this.storageMaster.AddProduct("SolidStateDrive", 200);
            this.storageMaster.RegisterStorage("Warehouse", "SmartSolutions");
            this.storageMaster.SelectVehicle("SmartSolutions", 2);
            List<string> products = new List<string>() { "Ram", "HardDrive", "Gpu", "SolidStateDrive" };
            this.storageMaster.LoadVehicle(products);
            this.storageMaster.UnloadVehicle("SmartSolutions", 2);

            var result = storageMaster.GetSummary();
            Assert.AreEqual(result, "SmartSolutions:\r\nStorage worth: $568,00", "Doestn return the correct sum of products in the storage.");
        }
    }
}
