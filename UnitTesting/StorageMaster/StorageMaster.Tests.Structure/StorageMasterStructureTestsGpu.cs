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
    class StorageMasterStructureTestsGpu
    {
        [Test]
        public void TestGpuConstructorSetsTheCorrectValues()
        {
            var gpu = new Gpu(60);

            Assert.AreEqual(gpu.Price, 60, "Does not set the price correctly");
            Assert.AreEqual(gpu.Weight, 0.7, "Does not set the weight correctly");

        }

        [Test]
        public void TestGpuDoesNotSetNegativePrice()
        {
            Assert.Throws<InvalidOperationException>(() => new Gpu(-60), "Sets negative price.");
        }

        [Test]
        public void TestGpuFieldForPriceReturnsCorrectValue()
        {
            var type = typeof(Gpu);

            var field = type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "price");

            var instance = (Product)Activator.CreateInstance(type, new object[] { 70 });

            Assert.AreEqual(field.GetValue(instance), 70, "Field does not set correctly.");
        }
    }
}
