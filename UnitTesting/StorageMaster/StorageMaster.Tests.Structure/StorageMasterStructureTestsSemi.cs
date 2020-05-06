using NUnit.Framework;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMaster.Tests.Structure

{   [TestFixture]
    class StorageMasterStructureTestsSemi
    {
        [Test]
        public void TestSemiLoadProductLoadsItems()
        {
            Vehicle semi = new Semi();

            Product gpu = new Gpu(120);

            semi.LoadProduct(gpu);

            Assert.AreEqual(semi.Trunk.First(), gpu, "Semi does not add products correctly.");
        }

        [Test]
        public void TestSemiLoadProductThrowsExceptionWhenFull()
        {
            Vehicle semi = new Semi();

            Product hardDrive = new HardDrive(120);

            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            Assert.Throws<InvalidOperationException>(() => semi.LoadProduct(hardDrive), "Full Semi does not throw exception.");

        }

        [Test]
        public void TestSemiUnLoadProductUnloadsTheCorretItem()
        {
            Vehicle semi = new Semi();

            Product hardDrive = new HardDrive(120);
            semi.LoadProduct(new Gpu(100));
            semi.LoadProduct(hardDrive);
            Assert.AreEqual(semi.Unload(), hardDrive);

        }

        [Test]
        public void TestSemiUnLoadProductThrowsExceptionWhenEmpty()
        {
            Vehicle semi = new Semi();
            Product hardDrive = new HardDrive(120);
            semi.LoadProduct(new Gpu(100));
            semi.Unload();
            Assert.Throws<InvalidOperationException>(() => semi.Unload(), "Empty Semi still unloads products.");

        }

        [Test]
        public void TestSemiPropertyIsEmptyReturnsCorrectValueWhenEmpty()
        {
            Vehicle semi = new Semi();

            Assert.True(semi.IsEmpty, "Semi should be empty");

        }

        [Test]
        public void TestSemiPropertyIsEmptyReturnsCorrectValueWhenNotEmpty()
        {
            Vehicle semi = new Semi();
            Product hardDrive = new HardDrive(120);
            semi.LoadProduct(new Gpu(100));
            Assert.IsFalse(semi.IsEmpty, "Semi should not be empty");

        }

        [Test]
        public void TestSemiPropertyIsFullReturnsCorrectValueWhenNotFull()
        {
            Vehicle semi = new Semi();
            Product hardDrive = new HardDrive(120);
            semi.LoadProduct(new Gpu(100));
            Assert.False(semi.IsFull, "Semi should not be full");

        }

        [Test]
        public void TestSemiPropertyIsFullReturnsCorrectValueWhenFull()
        {
            Vehicle semi = new Semi();
            Product hardDrive = new HardDrive(120);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(hardDrive);
            Assert.True(semi.IsFull, "Semi should be full");

        }

        [Test]
        public void TestSemiTrunkReturnsCorrectCollection()
        {
            Vehicle semi = new Semi();
            Product hardDrive = new HardDrive(120);
            Product gpu = new Gpu(100);
            Product ram = new Ram(40);
            List<Product> products = new List<Product>()
            {
                hardDrive,
                gpu,
                ram,
            };
            semi.LoadProduct(hardDrive);
            semi.LoadProduct(gpu);
            semi.LoadProduct(ram);

            CollectionAssert.AreEqual(semi.Trunk, products, "Added items should be the same.");

        }

        [Test]
        public void TestSemiCapacity()
        {
            Vehicle semi = new Semi();

            Assert.AreEqual(semi.Capacity, 10);

        }

        [Test]
        public void TestSemiFieldsIfExist()
        {

            var type = Assembly.Load("StorageMaster").GetType("StorageMaster.Entities.Vehicles.Semi");//.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).FirstOrDefault(x => x.Name == "trunk");
            var testInstance = (Vehicle)Activator.CreateInstance(type);
            testInstance.LoadProduct(new HardDrive(123));

            var field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "trunk");
            Assert.AreEqual(field.Name, "trunk", "Semi field name is not as required.");
            //     var fieldValue = field.GetValue(testInstance);
            //  Console.WriteLine();
        }

        [Test]
        public void TestSemiFieldTypeValueIsCorrect()
        {
            var hardDrive = new HardDrive(123);
            // var type = Assembly.Load("StorageMaster").GetType("StorageMaster.Entities.Vehicles.Van");
            // var testInstance = (Vehicle)Activator.CreateInstance(type);
            //   var field = type.BaseType.GetRuntimeFields().FirstOrDefault(x => x.Name == "trunk");
            //   testInstance.LoadProduct(hardDrive);
            //   var fieldValue = field.GetValue(testInstance);
            //  var result = fieldValue;
            var type = typeof(Semi);
            var testInstance = (Vehicle)Activator.CreateInstance(type);
            testInstance.LoadProduct(hardDrive);
            FieldInfo field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "trunk");
            List<Product> fieldValue = (List<Product>)field.GetValue(testInstance);

            Assert.AreEqual(fieldValue[0], hardDrive, "Field does not contain the same value.");

        }
    }
}
