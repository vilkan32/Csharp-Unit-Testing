namespace Chainblock
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    public class ChainblockTransaction : IChainblock
    {

        private List<ITransaction> transactions;

        public ChainblockTransaction()
        {
            this.transactions = new List<ITransaction>();
        }
        // done
        public int Count
        {
            get => this.transactions.Count;
        }
        // done
        public void Add(ITransaction tx)
        {
            if (!this.Contains(tx))
            {
                this.transactions.Add(tx);
            }
            else
            {
                throw new InvalidOperationException($"Transaction with Id: {tx.Id} already exists.");
            }
        }

        // done
        public bool Contains(ITransaction tx)
        {
            foreach (var item in this.transactions)
            {
                if (item.CompareTo(tx) == 0)
                {
                    return true;
                }
            }

            return false;
        }
        // done
        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            if (this.Contains(id))
            {
                var result = this.transactions.Where(x => x.Id == id).First();

                result.Status = newStatus;
            }
            else
            {
                throw new ArgumentException($"Transaction with ID: {id} does not exist.");
            }
        }
        // done
        public bool Contains(int id)
        {
            foreach (var item in this.transactions)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }

            return false;
        }
        // done
        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            var newCollection = this.transactions;
            var result = newCollection.Where(x => x.Amount >= lo && x.Amount <= hi);

            if (result.Count() == 0)
            {
                return new List<ITransaction>();
            }

            return result.ToList().AsReadOnly();
        }
        // done
        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            var collection = this.transactions.AsReadOnly();
            if (collection.Count == 0)
            {
                throw new InvalidOperationException("Collection is empty");
            }
            var result = collection.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
            return result;
        }
        // done
        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            if (this.transactions.Count == 0)
            {
                throw new InvalidOperationException("Collection is empty.");
            }

            var result = this.transactions.AsReadOnly().Where(x => x.Status == status).OrderByDescending(x => x.Amount).Select(x => x.To);

            if (result.Count() == 0)
            {
                throw new InvalidOperationException("No such transactions exist.");
            }

            return result.ToList();
        }
        // done
        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            if (this.transactions.Count == 0)
            {
                throw new InvalidOperationException("Collection is empty.");
            }

            var result = this.transactions.AsReadOnly().Where(x => x.Status == status).OrderByDescending(x => x.Amount).Select(x => x.From);

            if (result.Count() == 0)
            {
                throw new InvalidOperationException("No such transactions exist.");
            }

            return result.ToList();
        }
        // done
        public ITransaction GetById(int id)
        {
            if (this.transactions.Count == 0)
            {
                throw new InvalidOperationException("Collection is empty.");
            }

            var result = this.transactions.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException("No such transaction exists.");
            }

            return result;
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ITransaction> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void RemoveTransactionById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
