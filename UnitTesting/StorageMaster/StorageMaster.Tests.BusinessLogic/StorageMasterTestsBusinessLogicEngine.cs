using NUnit.Framework;
using System;
using System.Reflection;

namespace StorageMaster.Tests.BusinessLogic
{
    using StorageMaster.Core;
    using StorageMaster.Core.IO;
    using StorageMaster.Entities.Products;
    using StorageMaster.Entities.Storage;
    using System.Collections.Generic;
    using System.Linq;
    [TestFixture]
    class StorageMasterTestsBusinessLogicEngine
    {
        [Test]
        [TestCase("Gpu")]
        [TestCase("HardDrive")]
        [TestCase("Ram")]
        [TestCase("SolidStateDrive")]
        public void StorageMasterEngineTestCaseAddProductReturnsCorrectValue(string cases)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] {new ConsoleReader(), new ConsoleWriter()});

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");

            var getMethodValue = method.Invoke(instance, new object[] { $"AddProduct {cases} 120" });

            Assert.AreEqual(getMethodValue, $"Added {cases} to pool");
        }
        [Test]
        [TestCase("AutomatedWarehouse", "SafeSolutions")]
        [TestCase("DistributionCenter", "SmartTechDrive")]
        [TestCase("Warehouse", "SmartSolutions")]
        public void StorageMasterEngineTestCaseRegisterStorageReturnsCorrectValue(string casesType, string casesName)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] { new ConsoleReader(), new ConsoleWriter() });

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");

            var getMethodValue = method.Invoke(instance, new object[] { $"RegisterStorage {casesType} {casesName}" });

            Assert.AreEqual(getMethodValue, $"Registered {casesName}");
        }

        [Test]
        [TestCase("AutomatedWarehouse", "SafeSolutions", 0, "Truck")]
        [TestCase("DistributionCenter", "SmartTechDrive", 0, "Van")]
        [TestCase("Warehouse", "SmartSolutions", 0, "Semi")]
        public void StorageMasterEngineTestCaseSelectVehicleReturnsCorrectValue(string casesType, string casesName, int slot, string vehicleNames)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] { new ConsoleReader(), new ConsoleWriter() });

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");

            var getMethodValue = method.Invoke(instance, new object[] { $"RegisterStorage {casesType} {casesName}" });

            var result = method.Invoke(instance, new object[] { $"SelectVehicle {casesName} {slot}" });

            Assert.AreEqual(result, $"Selected {vehicleNames}", "Doesnt select the correct vehicle.");
        }

        [Test]
        [TestCase("DistributionCenter", "SmartTechDrive", 0, "Van")]
        [TestCase("Warehouse", "SmartSolutions", 0, "Semi")]
        public void StorageMasterEngineTestCaseLoadVehicleReturnsCorrectValue(string casesType, string casesName, int slot, string vehicleNames)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] { new ConsoleReader(), new ConsoleWriter() });

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");

            method.Invoke(instance, new object[] { $"RegisterStorage {casesType} {casesName}" });
            method.Invoke(instance, new object[] { "AddProduct Ram 120" });
            method.Invoke(instance, new object[] { "AddProduct Gpu 250" });
            method.Invoke(instance, new object[] { "AddProduct HardDrive 100" });
            method.Invoke(instance, new object[] { "AddProduct SolidStateDrive 170" });
            method.Invoke(instance, new object[] { $"SelectVehicle {casesName} {slot}" });

            Assert.AreEqual(method.Invoke(instance, new object[] { "LoadVehicle Ram Gpu HardDrive SolidStateDrive" }), $"Loaded {4}/{4} products into {vehicleNames}", "Doesnt load the correct number of products.");
        }


        [Test]
        [TestCase("AutomatedWarehouse", "SafeSolutions", "Warehouse", "SmartTech", 0, "Truck")]
        public void StorageMasterEngineTestCaseSendVehicleToReturnsCorrectValue(string casesType, string casesName, string destination, string destinatonName, int slot, string vehicleNames)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] { new ConsoleReader(), new ConsoleWriter() });

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");

            var registerStorageMethod = method.Invoke(instance, new object[] { $"RegisterStorage {casesType} {casesName}" });
            var registerStorageMethod1 = method.Invoke(instance, new object[] { $"RegisterStorage {destination} {destinatonName}" });
            
            var sendVehicleToMethod = method.Invoke(instance, new object[] { $"SendVehicleTo {casesName} {slot} {destinatonName}"});

            Assert.AreEqual(sendVehicleToMethod, $"Sent {vehicleNames} to {destinatonName} (slot {3})", "Doesnt send vehicles to the correct slot.");

        }
        [TestCase("AutomatedWarehouse", "SafeSolutions", "DistributionCenter", "SmartTech", 0, "Truck")]
        public void StorageMasterEngineTestCaseUnloadVehicleReturnsCorrectValue(string casesType, string casesName, string destination, string destinatonName, int slot, string vehicleNames)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] { new ConsoleReader(), new ConsoleWriter() });

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");
            
            var registerStorageMethod = method.Invoke(instance, new object[] { $"RegisterStorage {casesType} {casesName}" });
            var registerStorageMethod1 = method.Invoke(instance, new object[] { $"RegisterStorage {destination} {destinatonName}" });
            var loadPool = method.Invoke(instance, new object[] { "AddProduct Ram 120" });
            var loadPool2 = method.Invoke(instance, new object[] { "AddProduct Gpu 200" });
            var selectVehicleMethod = method.Invoke(instance, new object[] { $"SelectVehicle {casesName} {slot}" });
            var loadVehicleMethod = method.Invoke(instance, new object[] { "LoadVehicle Ram Gpu" });
            var sendVehicleToMethod = method.Invoke(instance, new object[] { $"SendVehicleTo {casesName} {slot} {destinatonName}" });

            var unloadVehicleTo = method.Invoke(instance, new object[] { $"UnloadVehicle SmartTech {3} "});

            Assert.AreEqual(unloadVehicleTo, $"Unloaded 2/2 products at {destinatonName}");

        }

        [TestCase("DistributionCenter", "SafeSolutions", "AutomatedWarehouse", "SmartTech", 0, "Truck")]
        public void StorageMasterEngineTestCaseGetStorageStatusReturnsCorrectValue(string casesType, string casesName, string destination, string destinatonName, int slot, string vehicleNames)
        {
            var type = typeof(Engine);

            var instance = (Engine)Activator.CreateInstance(type, new object[] { new ConsoleReader(), new ConsoleWriter() });

            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "ProcessCommand");

            var registerStorageMethod = method.Invoke(instance, new object[] { $"RegisterStorage {casesType} {casesName}" });
            var registerStorageMethod1 = method.Invoke(instance, new object[] { $"RegisterStorage {destination} {destinatonName}" });
            var loadPool = method.Invoke(instance, new object[] { "AddProduct Ram 120" });
            var loadPool2 = method.Invoke(instance, new object[] { "AddProduct Gpu 200" });
            var selectVehicleMethod = method.Invoke(instance, new object[] { $"SelectVehicle {casesName} {slot}" });
            var loadVehicleMethod = method.Invoke(instance, new object[] { "LoadVehicle Ram Gpu" });
            var sendVehicleToMethod = method.Invoke(instance, new object[] { $"SendVehicleTo {casesName} {slot} {destinatonName}" });

            var unloadVehicleTo = method.Invoke(instance, new object[] { $"UnloadVehicle SmartTech {1} " });

            var getStorageStatus = method.Invoke(instance, new object[] { $"GetStorageStatus {destinatonName}" });

            Assert.AreEqual(getStorageStatus, "Stock (0,8/1): [Gpu (1), Ram (1)]\r\nGarage: [Truck|Van]");

        }


    }
}
