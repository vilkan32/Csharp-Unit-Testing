namespace Chainblock.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestTransaction_ConstrutorSetsValuesCorrectly()
        {
            var transaction = new Transaction(1, TransactionStatus.Successfull, 
                "Gosho", "Pesho", 200);

            Assert.AreEqual(transaction.Id, 1);
            Assert.AreEqual(transaction.Status, TransactionStatus.Successfull);
            Assert.AreEqual(transaction.From, "Gosho");
            Assert.AreEqual(transaction.To, "Pesho");
            Assert.AreEqual(transaction.Amount, 200);
        }

        [Test]
        public void TestTransaction_CompareToComparesTransactionsCorrectly()
        {
            var transaction = new Transaction(1, TransactionStatus.Successfull,
               "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2, TransactionStatus.Successfull,
               "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(1, TransactionStatus.Successfull,
               "Gosho", "Pesho", 200);

            Assert.AreEqual(transaction.CompareTo(transaction1), -1);
            Assert.AreEqual(transaction.CompareTo(transaction2), 0);
        }

        [Test]
        public void TestTransaction_CannotTransferNegativeAmountOfMoney()
        {
            Assert.Throws<ArgumentException>(() => new Transaction(1, TransactionStatus.Successfull,
               "Gosho", "Pesho", -200));
        }

        
    }
}
