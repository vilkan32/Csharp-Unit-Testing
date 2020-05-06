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
    class StorageMasterStructureTestsVan
    {
        // truck done
        [Test]
        public void TestVanLoadProductLoadsItems()
        {
            Vehicle van = new Van();

            Product gpu = new Gpu(120);

            van.LoadProduct(gpu);

            Assert.AreEqual(gpu, van.Trunk.First(), "Van does not add products correctly.");
        }
        // truck done
        [Test]
        public void TestVanLoadProductThrowsExceptionWhenFull()
        {
            Vehicle van = new Van();

            Product hardDrive = new HardDrive(120);

            van.LoadProduct(hardDrive);
            van.LoadProduct(hardDrive);

            Assert.Throws<InvalidOperationException>(() => van.LoadProduct(hardDrive), "Full van does not throw exception.");

        }
        // truck done
        [Test]
        public void TestVanUnLoadProductUnloadsTheCorretItem()
        {
            Vehicle van = new Van();

            Product hardDrive = new HardDrive(120);
            van.LoadProduct(new Gpu(100));
            van.LoadProduct(hardDrive);
            Assert.AreEqual(van.Unload(), hardDrive);

        }
        // truck done
        [Test]
        public void TestVanUnLoadProductThrowsExceptionWhenEmpty()
        {
            Vehicle van = new Van();

            Product hardDrive = new HardDrive(120);
            van.LoadProduct(new Gpu(100));
            van.Unload();
            Assert.Throws<InvalidOperationException>(() => van.Unload(), "Empty van still unloads products.");

        }
        // truck done
        [Test]
        public void TestVanPropertyIsEmptyReturnsCorrectValueWhenEmpty()
        {
            Vehicle van = new Van();

            Assert.True(van.IsEmpty, "Van should be empty");

        }
        // truck done
        [Test]
        public void TestVanPropertyIsEmptyReturnsCorrectValueWhenNotEmpty()
        {
            Vehicle van = new Van();
            Product hardDrive = new HardDrive(120);
            van.LoadProduct(new Gpu(100));
            Assert.IsFalse(van.IsEmpty, "Van should not be empty");

        }
        // truck done
        [Test]
        public void TestVanPropertyIsFullReturnsCorrectValueWhenNotFull()
        {
            Vehicle van = new Van();
            Product hardDrive = new HardDrive(120);
            van.LoadProduct(new Gpu(100));
            Assert.False(van.IsFull, "Van should not be full");

        }
        // truck done
        [Test]
        public void TestVanPropertyIsFullReturnsCorrectValueWhenFull()
        {
            Vehicle van = new Van();
            Product hardDrive = new HardDrive(120);
            van.LoadProduct(hardDrive);
            van.LoadProduct(hardDrive);
            Assert.True(van.IsFull, "Van should be full");

        }
        // truck done
        [Test]
        public void TestVanTrunkReturnsCorrectCollection()
        {
            Vehicle van = new Van();
            Product hardDrive = new HardDrive(120);
            Product gpu = new Gpu(100);
            Product ram = new Ram(40);
            List<Product> products = new List<Product>()
            {
                hardDrive,
                gpu,
                ram,
            };
            van.LoadProduct(hardDrive);
            van.LoadProduct(gpu);
            van.LoadProduct(ram);

            CollectionAssert.AreEqual(van.Trunk, products, "Added items should be the same.");

        }
        // truck done
        [Test]
        public void TestVanCapacity()
        {
            Vehicle van = new Van();

            Assert.AreEqual(van.Capacity, 2);

        }
        // truck done
        [Test]
        public void TestVanFieldsIfExist()
        {

            var type = Assembly.Load("StorageMaster").GetType("StorageMaster.Entities.Vehicles.Van");//.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "trunk");
            var testInstance = (Vehicle)Activator.CreateInstance(type);
            testInstance.LoadProduct(new HardDrive(123));

            var field  = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "trunk");
            Assert.AreEqual(field.Name, "trunk");
       //     var fieldValue = field.GetValue(testInstance);
          //  Console.WriteLine();
        }

        [Test]
        public void TestVanFieldTypeValueIsCorrect()
        {
            var hardDrive = new HardDrive(123);
           // var type = Assembly.Load("StorageMaster").GetType("StorageMaster.Entities.Vehicles.Van");
           // var testInstance = (Vehicle)Activator.CreateInstance(type);
            //   var field = type.BaseType.GetRuntimeFields().FirstOrDefault(x => x.Name == "trunk");
            //   testInstance.LoadProduct(hardDrive);
            //   var fieldValue = field.GetValue(testInstance);
            //  var result = fieldValue;
            var type = typeof(Van);
            var testInstance = (Vehicle)Activator.CreateInstance(type);
            testInstance.LoadProduct(hardDrive);
            FieldInfo field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "trunk");
            List<Product> fieldValue = (List<Product>)field.GetValue(testInstance);
            
            Assert.AreEqual(fieldValue[0], hardDrive, "Field does not contain the same value.");

        }
        // start testing for truck ...
       
    }
}
