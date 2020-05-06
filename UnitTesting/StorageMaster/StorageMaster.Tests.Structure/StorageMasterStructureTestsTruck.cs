using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    class StorageMasterStructureTestsTruck
    {
        [Test]
        public void TestTruckLoadProductLoadsItems()
        {
            Vehicle truck = new Truck();

            Product gpu = new Gpu(120);

            truck.LoadProduct(gpu);

            Assert.AreEqual(truck.Trunk.First(), gpu, "Truck does not add products correctly.");
        }

        [Test]
        public void TestTruckLoadProductThrowsExceptionWhenFull()
        {
            Vehicle truck = new Truck();

            Product hardDrive = new HardDrive(120);

            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            Assert.Throws<InvalidOperationException>(() => truck.LoadProduct(hardDrive), "Full truck does not throw exception.");

        }

        [Test]
        public void TestTruckUnLoadProductUnloadsTheCorretItem()
        {
            Vehicle truck = new Truck();

            Product hardDrive = new HardDrive(120);
            truck.LoadProduct(new Gpu(100));
            truck.LoadProduct(hardDrive);
            Assert.AreEqual(truck.Unload(), hardDrive);

        }

        [Test]
        public void TestTruckUnLoadProductThrowsExceptionWhenEmpty()
        {
            Vehicle truck = new Truck();
            Product hardDrive = new HardDrive(120);
            truck.LoadProduct(new Gpu(100));
            truck.Unload();
            Assert.Throws<InvalidOperationException>(() => truck.Unload(), "Empty truck still unloads products.");

        }

        [Test]
        public void TestTruckPropertyIsEmptyReturnsCorrectValueWhenEmpty()
        {
            Vehicle truck = new Truck();

            Assert.True(truck.IsEmpty, "Truck should be empty");

        }

        [Test]
        public void TestTruckPropertyIsEmptyReturnsCorrectValueWhenNotEmpty()
        {
            Vehicle truck = new Truck();
            Product hardDrive = new HardDrive(120);
            truck.LoadProduct(new Gpu(100));
            Assert.IsFalse(truck.IsEmpty, "Truck should not be empty");

        }

        [Test]
        public void TestTruckPropertyIsFullReturnsCorrectValueWhenNotFull()
        {
            Vehicle truck = new Truck();
            Product hardDrive = new HardDrive(120);
            truck.LoadProduct(new Gpu(100));
            Assert.False(truck.IsFull, "Truck should not be full");

        }

        [Test]
        public void TestTruckPropertyIsFullReturnsCorrectValueWhenFull()
        {
            Vehicle truck = new Truck();
            Product hardDrive = new HardDrive(120);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(hardDrive);
            Assert.True(truck.IsFull, "Truck should be full");

        }

        [Test]
        public void TestTruckTrunkReturnsCorrectCollection()
        {
            Vehicle truck = new Truck();
            Product hardDrive = new HardDrive(120);
            Product gpu = new Gpu(100);
            Product ram = new Ram(40);
            List<Product> products = new List<Product>()
            {
                hardDrive,
                gpu,
                ram,
            };
            truck.LoadProduct(hardDrive);
            truck.LoadProduct(gpu);
            truck.LoadProduct(ram);

            CollectionAssert.AreEqual(truck.Trunk, products, "Added items should be the same.");

        }

        [Test]
        public void TestTruckCapacity()
        {
            Vehicle truck = new Truck();

            Assert.AreEqual(truck.Capacity, 5);

        }

        [Test]
        public void TestTruckFieldsIfExist()
        {

            var type = Assembly.Load("StorageMaster").GetType("StorageMaster.Entities.Vehicles.Truck");//.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "trunk");
            var testInstance = (Vehicle)Activator.CreateInstance(type);
            testInstance.LoadProduct(new HardDrive(123));

            var field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "trunk");
            Assert.AreEqual(field.Name, "trunk", "Truck field name is not as required.");
            //     var fieldValue = field.GetValue(testInstance);
            //  Console.WriteLine();
        }

        [Test]
        public void TestTruckFieldTypeValueIsCorrect()
        {
            var hardDrive = new HardDrive(123);
            // var type = Assembly.Load("StorageMaster").GetType("StorageMaster.Entities.Vehicles.Van");
            // var testInstance = (Vehicle)Activator.CreateInstance(type);
            //   var field = type.BaseType.GetRuntimeFields().FirstOrDefault(x => x.Name == "trunk");
            //   testInstance.LoadProduct(hardDrive);
            //   var fieldValue = field.GetValue(testInstance);
            //  var result = fieldValue;
            var type = typeof(Truck);
            var testInstance = (Vehicle)Activator.CreateInstance(type);
            testInstance.LoadProduct(hardDrive);
            FieldInfo field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "trunk");
            List<Product> fieldValue = (List<Product>)field.GetValue(testInstance);

            Assert.AreEqual(fieldValue[0], hardDrive, "Field does not contain the same value.");

        }

    }
}
