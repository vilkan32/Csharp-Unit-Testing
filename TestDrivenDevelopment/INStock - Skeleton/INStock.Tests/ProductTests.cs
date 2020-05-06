namespace INStock.Tests
{
    using INStock.Contracts;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void Product_TestConstrutorSetsCorrectValue()
        {
            var product = new Product("SSD", 12.4m, 3);

            Assert.AreEqual(product.Label, "SSD");

            Assert.AreEqual(product.Price, 12.4);

            Assert.AreEqual(product.Quantity, 3);
        }

        [Test]
        public void Product_TestConstrutorThrowsExceptionWhenLabelIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(null, 12.4m, 3));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void Product_TestConstrutorThrowsExceptionWhenPriceIsNegativeOrZero(decimal price)
        {
            Assert.Throws<ArgumentException>(() => new Product("SSD", price, 3));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [TestCase(-4)]
        public void Product_TestConstrutorThrowsExceptionWhenQuantityIsNegative(int quantity)
        {
            Assert.Throws<ArgumentException>(() => new Product("SSD", 12.4m, quantity));

        }

        [Test]
        [TestCase("SSD")]
        [TestCase("HDD")]
        [TestCase("Ram")]
        [TestCase("CPU")]
        public void Product_TestConstrutorCompareToComparesCorrectly(string names)
        {

            var product = new Product(names, 12.4m, 3);

            IProduct dummyProduct = new Product(names, 12.3m, 2);

            Assert.AreEqual(0, product.CompareTo(dummyProduct));

        }

        [Test]
        [TestCase("SSD")]
        [TestCase("HDD")]
        [TestCase("Ram")]
        [TestCase("CPU")]
        public void Product_TestLabelReturnsCorrectValue(string names)
        {
            var product = new Product(names, 12.4m, 3);

            Assert.AreEqual(product.Label, names);
        }

        [Test]
        [TestCase(13.5)]
        [TestCase(14.5)]
        [TestCase(15.6)]
        public void Product_PriceReturnsCorrectValue(decimal prices)
        {
            var product = new Product("SSD", prices, 3);

            Assert.AreEqual(product.Price, prices);
        }

        [Test]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public void Product_QuantityReturnsCorrectValue(int quantity)
        {
            var product = new Product("SSD", 14.5m, quantity);

            Assert.AreEqual(product.Quantity, quantity);
        }


    }
}