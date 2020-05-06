namespace Chainblock.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Chainblock.Contracts;
    using NUnit.Framework;
    using System.Reflection;
    [TestFixture]
    public class ChainblockTests
    {
        [Test]
        public void Chainblock_AddMethodAddsElemets()
        {
            var transaction = new Transaction(1, 
                TransactionStatus.Successfull,
                "Gosho", "Pesho", 200);
            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);

            Assert.AreEqual(chainblock.Count, 1);
        }

        [Test]
        public void Chainblock_AddMethodThrowsExceptionWhenIdHasAlredyBeenAdded()
        {
            var transaction = new Transaction(1,
                TransactionStatus.Successfull,
                "Gosho", "Pesho", 200);
            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);

            Assert.Throws<InvalidOperationException>(() => chainblock.Add(transaction));
        }

        [Test]
        public void Chainblock_CountMethodReturnsCorrectValue()
        {
            var transaction = new Transaction(1,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);

            Assert.AreEqual(chainblock.Count, 3);
        }

        [Test]
        public void Chainblock_ContainsMethodReturnsTrue()
        {
            var transaction = new Transaction(1,
   TransactionStatus.Successfull,
   "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);

            Assert.IsTrue(chainblock.Contains(transaction));
            Assert.IsTrue(chainblock.Contains(transaction1));
            Assert.IsTrue(chainblock.Contains(transaction2));

        }

        [Test]
        public void Chainblock_ContainsMethodReturnsFalse()
        {
            var transaction = new Transaction(1,
   TransactionStatus.Successfull,
   "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 200);
            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);

            Assert.IsFalse(chainblock.Contains(transaction3));

        }

        [Test]
        public void Chainblock_ChangeTransactionStatusChangesStatusCorrectly()
        {


            var transaction = new Transaction(1,
TransactionStatus.Successfull,
"Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 200);

            var type = typeof(ChainblockTransaction);

            var instance = (ChainblockTransaction)Activator.CreateInstance(type);

            instance.Add(transaction);
            instance.Add(transaction1);
            instance.Add(transaction2);
            instance.ChangeTransactionStatus(1, TransactionStatus.Failed);
            var field = type.GetFields(BindingFlags.NonPublic| BindingFlags.Instance).FirstOrDefault(x => x.Name == "transactions");

            var instanciate = (List<ITransaction>)field.GetValue(instance);
            Console.WriteLine();
            var result = instanciate.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual(result.Status, TransactionStatus.Failed);
        }

        [Test]
        public void Chainblock_ChangeTransactionStatusThrowsExceptionWhenIdIsMissing()
        {
            var transaction = new Transaction(1,
  TransactionStatus.Successfull,
  "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 200);
            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);

            Assert.Throws<ArgumentException>(() => chainblock.ChangeTransactionStatus(8, TransactionStatus.Failed));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Chainblock_ContainsIdMethodReturnsTrue(int id)
        {
            var transaction = new Transaction(1,
 TransactionStatus.Successfull,
 "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 200);
            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);

            Assert.IsTrue(chainblock.Contains(id));
        }


        [Test]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void Chainblock_ContainsIdMethodReturnsFalse(int id)
        {
            var transaction = new Transaction(1,
 TransactionStatus.Successfull,
 "Gosho", "Pesho", 200);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 200);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 200);
            var chainblock = new ChainblockTransaction();

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);

            Assert.False(chainblock.Contains(id));
        }

        [Test]
        public void Chainblock_GetAllInAmountRangeReturnsCorrectElements()
        {
            var transaction = new Transaction(1,
TransactionStatus.Successfull,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();

            List<ITransaction> listOfTransactions = new List<ITransaction>() { transaction, transaction1, transaction2, transaction3 };

            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);

            var result = chainblock.GetAllInAmountRange(250, 400);
            Assert.AreEqual(result.Count(), 4);
            int index = 0;
            foreach (var item in result)
            {
                Assert.AreEqual(item.CompareTo(listOfTransactions[index++]), 0);
            }
        }

        [Test]
        public void Chainblock_GetAllInAmountRangeReturnsEmptyCollection()
        {
            var transaction = new Transaction(1,
TransactionStatus.Successfull,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);


            var result = chainblock.GetAllInAmountRange(401, 500);

            Assert.AreEqual(result.Count(), 0);
        }
        

        [Test]
        public void Chainblock_GetAllOrderedByAmountDescendingThenByIdReturnsCorrectCollection()
        {
            var transaction = new Transaction(1,
TransactionStatus.Successfull,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);
            List<ITransaction> listOfTransactions = new List<ITransaction>() { transaction, transaction1, transaction2, transaction3 };
            listOfTransactions = listOfTransactions.OrderByDescending(x => x.Amount).ThenBy(x => x.Id).ToList();

            var result = chainblock.GetAllOrderedByAmountDescendingThenById();

            CollectionAssert.AreEqual(result, listOfTransactions);

        }

        [Test]
        public void Chainblock_GetAllOrderedByAmountDescendingThenByIdThrwsExceptionWhenCollectionIsEmpty()
        {
            var transaction = new Transaction(1,
TransactionStatus.Successfull,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Pesho", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Successfull,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();

            Assert.Throws<InvalidOperationException>(() => chainblock.GetAllOrderedByAmountDescendingThenById());

        }


        [Test]
        public void Chainblock_GetAllReceiversWithTransactionStatusReturnsCorrectCollectionOfNames()
        {
            var transaction = new Transaction(1,
TransactionStatus.Failed,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Failed,
    "Gosho", "Stamat", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Kiro", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Failed,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);

            List<ITransaction> listOfTransactions = new List<ITransaction>() { transaction, transaction1, transaction2, transaction3 };
            var result = listOfTransactions.Where(x => x.Status == TransactionStatus.Failed).OrderByDescending(x => x.Amount).Select(x => x.To).ToList();
            var actualResult = chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Failed);
            int index = 0;

            foreach (var item in actualResult)
            {
                Assert.AreEqual(item, result.ElementAt(index++));
            }
        }

        [Test]
        public void Chainblock_GetAllReceiversWithTransactionStatusThrowsExceptionWhenCollectionIsEmpty()
        {
            var chainblock = new ChainblockTransaction();

            Assert.Throws<InvalidOperationException>(() => chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Aborted));

        }

        [Test]
        public void Chainblock_GetAllReceiversWithTransactionStatusThrowsExceptionWhenSuchStatusCannotBeFound()
        {
            var transaction = new Transaction(1,
TransactionStatus.Failed,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Failed,
    "Gosho", "Stamat", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Kiro", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Failed,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);

            Assert.Throws<InvalidOperationException>(() => chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Aborted));

        }

        [Test]
        public void Chainblock_GetAllSendersWithTransactionStatusReturnsCorrectCollectionOfNames()
        {
            var transaction = new Transaction(1,
TransactionStatus.Failed,
"Gosho1", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Failed,
    "Gosho2", "Stamat", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho3", "Kiro", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Failed,
"Gosho4", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);

            List<ITransaction> listOfTransactions = new List<ITransaction>() { transaction, transaction1, transaction2, transaction3 };
            var result = listOfTransactions.Where(x => x.Status == TransactionStatus.Failed).OrderByDescending(x => x.Amount).Select(x => x.From).ToList();
            var actualResult = chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Failed);
            int index = 0;

            foreach (var item in actualResult)
            {
                Assert.AreEqual(item, result.ElementAt(index++));
            }
        }

        [Test]
        public void Chainblock_GetAllSendersWithTransactionStatusThrowsExceptionWhenCollectionIsEmpty()
        {
            var chainblock = new ChainblockTransaction();

            Assert.Throws<InvalidOperationException>(() => chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Aborted));

        }

        [Test]
        public void Chainblock_GetAllSendersWithTransactionStatusThrowsExceptionWhenSuchStatusCannotBeFound()
        {
            var transaction = new Transaction(1,
TransactionStatus.Failed,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Failed,
    "Gosho", "Stamat", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Kiro", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Failed,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);

            Assert.Throws<InvalidOperationException>(() => chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Aborted));

        }

        [Test]
        public void Chainblock_GetByIdMethodReturnsCorrectTransaction()
        {
            var transaction = new Transaction(1,
TransactionStatus.Failed,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Failed,
    "Gosho", "Stamat", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Kiro", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Failed,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);
            var result = chainblock.GetById(1);

            Assert.AreEqual(result.CompareTo(transaction), 0);
        }

        [Test]
        public void Chainblock_GetByIdMethodThrowsExceptionWhenCollectionIsEmpty()
        {          
            var chainblock = new ChainblockTransaction();

            Assert.Throws<InvalidOperationException>(() => chainblock.GetById(1));
        }

        [Test]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(0)]
        [TestCase(-10)]
        public void Chainblock_GetByIdMethodThrowsExceptionWhenAccesingIncorrectId(int id)
        {
            var transaction = new Transaction(1,
TransactionStatus.Failed,
"Gosho", "Pesho", 250);

            var transaction1 = new Transaction(2,
    TransactionStatus.Failed,
    "Gosho", "Stamat", 300);

            var transaction2 = new Transaction(3,
    TransactionStatus.Successfull,
    "Gosho", "Kiro", 350);

            var transaction3 = new Transaction(4,
TransactionStatus.Failed,
"Gosho", "Pesho", 400);
            var chainblock = new ChainblockTransaction();
            chainblock.Add(transaction);
            chainblock.Add(transaction1);
            chainblock.Add(transaction2);
            chainblock.Add(transaction3);

            Assert.Throws<InvalidOperationException>(() => chainblock.GetById(id));

        }
    }
}
