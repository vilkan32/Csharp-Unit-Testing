namespace INStock.Tests
{
    using INStock.Contracts;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class ProductStockTests
    {
        [Test]
        public void ProductStock_AddMethodIncreasesCountCorrectly()
        {
            var stock = new ProductStock();

            stock.Add(new Product("SSD", 12.4m, 3));

            stock.Add(new Product("HDD", 12.4m, 2));

            Assert.AreEqual(stock.Count, 2);
        }

        [Test]
        public void ProductStock_AddMethodThrowsExceptionWhenAddingInvalidProdduct()
        {
            var stock = new ProductStock();

            stock.Add(new Product("SSD", 12.4m, 3));

            Assert.Throws<ArgumentNullException>(() => stock.Add(new Product(null, 12.4m, 2)));
        }

        [Test]
        public void ProductStock_AddMethodIncreasesTheQuantityOfProductsWithTheSameLabels()
        {
            var type = typeof(ProductStock);

            var instance = (ProductStock)Activator.CreateInstance(type);

            instance.Add(new Product("SSD", 12.4m, 3));
            instance.Add(new Product("SSD", 12.4m, 3));

            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "products");

            var fieldValue = (List<IProduct>)field.GetValue(instance);
            Assert.AreEqual(fieldValue[0].Quantity, 6);
            Assert.AreEqual(instance.Count, 1);

        }

        [Test]
        public void ProductStock_AddMethodThrowsExceptionWhenAddingNullProdduct()
        {
            var stock = new ProductStock();

            Assert.Throws<ArgumentNullException>(() => stock.Add(null));
        }

        [Test]
        public void ProductStock_CountMethodReturnsCorrectNumberOfProductsInStock()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            Assert.AreEqual(stock.Count, 2);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(-1)]
        public void ProductStock_IndexerThrowsExceptionWhenGettingInvalidIndex(int index)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            IProduct result = null;
            Assert.Throws<IndexOutOfRangeException>(() => result = stock[index]);
        }

        [Test]
        [TestCase("SSD", 0)]
        [TestCase("HDD", 1)]
        public void ProductStock_IndexerGetsTheCorrectElementFromStock(string name, int index)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));

            IProduct result = stock[index];

            Assert.AreEqual(result.CompareTo(new Product(name, 12.4m, 3)), 0);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(-1)]
        public void ProductStock_IndexerThrowsExceptionWhenSettingInvalidIndex(int index)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            IProduct result = new Product("RAM", 12.4m, 3);
            Assert.Throws<IndexOutOfRangeException>(() => stock[index] = result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void ProductStock_IndexerThrowsExceptionWhenSettingNullProduct(int index)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            IProduct result = null;
            Assert.Throws<ArgumentNullException>(() => stock[index] = result);
        }

        [Test]
        public void ProductStock_IndexerSetsCorrectValue()
        {
            var type = typeof(ProductStock);

            var instance = (ProductStock)Activator.CreateInstance(type);

            instance.Add(new Product("SSD", 12.4m, 3));
            instance.Add(new Product("SSD", 12.4m, 3));
            instance.Add(new Product("HDD", 12.4m, 3));
            instance.Add(new Product("RAM", 12.4m, 3));

            instance[0] = new Product("CPU", 123.2m, 1);

            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == "products");

            var fieldValue = (List<IProduct>)field.GetValue(instance);

            Assert.AreEqual(instance[0].CompareTo(new Product("CPU", 123.2m, 1)), 0);
        }

        [Test]
        [TestCase("SSD", 12.4, 3)]
        [TestCase("RAM", 12.4, 3)]
        [TestCase("HDD", 12.4, 3)]
        public void ProductStock_ContainsMethodReturnsTrue(string name, decimal price, int quantity)
        {
            var stock = new ProductStock();
            stock.Add(new Product(name, price, quantity));
            Assert.IsTrue(stock.Contains(new Product(name, 12.4m, 3)));
        }

        [Test]
        [TestCase("SSD", 12.4, 3)]
        [TestCase("RAM", 12.4, 3)]
        [TestCase("HDD", 12.4, 3)]
        public void ProductStock_ContainsMethodReturnsFalse(string name, decimal price, int quantity)
        {
            var stock = new ProductStock();
            stock.Add(new Product(name, price, quantity));
            Assert.IsFalse(stock.Contains(new Product("GPU", 12.4m, 3)));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ProductStock_FindMethodThrowsExceptionWhenSearchingWithInvalidIndex(int index)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));

            Assert.Throws<IndexOutOfRangeException>(() => stock.Find(index));
        }

        [Test]
        [TestCase(0, "SSD")]
        [TestCase(1, "HDD")]
        [TestCase(2, "RAM")]
        public void ProductStock_FindMethodReturnsCorrectElement(int index, string name)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            stock.Add(new Product("RAM", 12.4m, 3));
            Assert.AreEqual(stock.Find(index).CompareTo(new Product(name, 12.4m, 3)), 0);
        }

        [Test]
        public void ProductStock_FindByPriceReturnCorrectNonEmptyCollection()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            stock.Add(new Product("RAM", 12.4m, 3));
            var result = stock.FindAllByPrice(12.4m);
            string[] names = new string[] { "SSD", "HDD", "RAM" };
            int i = 0;
            foreach (var item in result)
            {
               
                Assert.AreEqual(item.CompareTo(new Product(names[i++], 12.4m, 3)), 0);
            }
        }

        [Test]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        public void ProductStock_FindByPriceReturnCorrectEmptyCollection(decimal price)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            stock.Add(new Product("RAM", 12.4m, 3));

            var result = stock.FindAllByPrice(price);

            Assert.AreEqual(result.Count(), 0);
        }

        [Test]
        public void ProductStock_FindByQuantityReturnCorrectNonEmptyCollection()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            stock.Add(new Product("RAM", 12.4m, 3));
            var result = stock.FindAllByQuantity(3);
            string[] names = new string[] {"HDD", "RAM" };
            int i = 0;
            foreach (var item in result)
            {
                Assert.AreEqual(item.CompareTo(new Product(names[i++], 12.4m, 3)), 0);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void ProductStock_FindByPriceReturnsCorrectEmptyCollection(int quantity)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("HDD", 12.4m, 3));
            stock.Add(new Product("RAM", 12.4m, 3));

            var result = stock.FindAllByQuantity(quantity);

            Assert.AreEqual(result.Count(), 0);
        }

        [Test]
        [TestCase(12.4, 16.5, "CPU RAM SSD")]
        [TestCase(16, 21, "HDD CPU")]
        [TestCase(16.5, 20.7, "HDD CPU")]
        public void ProductStock_FindByPriceRangeReturnsCorrectNonEmptyCollection(decimal lo, decimal hi, string orderNames)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));
            var result = stock.FindAllInRange(lo,hi);
            var names = orderNames.Split(" ").ToArray();
            int i = 0;
            foreach (var item in result)
            {
                Assert.AreEqual(item.CompareTo(new Product(names[i++], 12.4m, 3)), 0);
            }
        }

        [Test]
        [TestCase(1,10)]
        [TestCase(-10, 1)]
        [TestCase(21, 30)]
        public void ProductStock_FindByPriceRangeReturnsCorrectEmptyCollection(decimal lo, decimal hi)
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            var result = stock.FindAllInRange(lo, hi);

            Assert.AreEqual(result.Count(), 0);
        }

        [Test]
        public void ProductStock_FindByLabelReturnsTheCorrectProduct()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            Assert.AreEqual(stock.FindByLabel("SSD").CompareTo(new Product("SSD", 12.4m, 3)), 0);
        }


        [Test]
        public void ProductStock_FindByLabelThrowsExceptionWhenProductCannotBeFound()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            Assert.Throws<ArgumentException>(() => stock.FindByLabel("GPU"));
        }

        [Test]
        public void ProductStock_FindMostExpensiveProductReturnsCorrectValue()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            var result = stock.FindMostExpensiveProduct();

            Assert.AreEqual(result.CompareTo(new Product("HDD", 20.7m, 3)), 0);

        }

        [Test]
        public void ProductStock_FindMostExpensiveProductThrowsExceptionWhenThereAreNoProducts()
        {
            var stock = new ProductStock();

            Assert.Throws<InvalidOperationException>(() => stock.FindMostExpensiveProduct());

        }

        [Test]
        public void ProductStock_TestEnumeratorReturnsCorrectValue()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            var items = new List<IProduct>() { new Product("SSD", 12.4m, 3), new Product("CPU", 16.5m, 3), new Product("HDD", 20.7m, 3), new Product("RAM", 14.9m, 3) };
            int index = 0;
            Console.WriteLine();
            foreach (var item in stock)
            {
                Assert.AreEqual(item.CompareTo(items[index++]), 0);
            }
        }

        [Test]
        public void ProductStock_RemoveMethodReturnsTrue()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            Assert.IsTrue(stock.Remove(new Product("SSD", 12.4m, 3)));

            Assert.AreEqual(stock[0].Quantity, 0);
        }

        [Test]
        public void ProductStock_RemoveMethodReturnsFalse()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            Assert.IsFalse(stock.Remove(new Product("GPU", 12.4m, 3)));

        }

        [Test]
        public void ProductStock_RandomTest()
        {
            var stock = new ProductStock();
            stock.Add(new Product("SSD", 12.4m, 3));
            stock.Add(new Product("CPU", 16.5m, 3));
            stock.Add(new Product("HDD", 20.7m, 3));
            stock.Add(new Product("RAM", 14.9m, 3));

            stock.FindByLabel("SSD").Price = 100;
        }
    }
}
