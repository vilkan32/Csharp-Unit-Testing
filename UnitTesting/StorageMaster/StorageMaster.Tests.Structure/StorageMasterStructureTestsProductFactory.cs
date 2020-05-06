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
    class StorageMasterStructureTestsProductFactory
    {

        [TestFixture]
        class StorageMasterStructureTestsStorageFactory
        {
            [Test]
            public void TestProductFactoryThrowsExceptionWhenTypeIsNull()
            {
                var productFactory = new ProductFactory();

                Assert.Throws<InvalidOperationException>(() => productFactory.CreateProduct(null, 100.12), "Doesnt throw exception when the type is null.");
            }

            [Test]
            public void TestProductFactoryReturnsTheCorrectTypeOfProduct()
            {
                var productFactory = new ProductFactory();

                Assert.AreEqual(productFactory.CreateProduct("Ram", 123.4).GetType().Name, typeof(Ram).Name, "Doesnt return the correct product.");

            }
        }
    }
}
