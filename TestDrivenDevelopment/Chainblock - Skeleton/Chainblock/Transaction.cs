using Chainblock.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chainblock
{
    public class Transaction : ITransaction
    {
        private int id;
        private TransactionStatus status;
        private string from;
        private string to;
        private double amount;

        public Transaction(int id, TransactionStatus status, string from, string to,double amount)
        {
            this.Id = id;
            this.Status = status;
            this.From = from;
            this.To = to;
            this.Amount = amount;

        }

        public int Id
        {
            get => this.id;
            private set
            {
                this.id = value;
            }
        }
        public TransactionStatus Status
        {
            get => this.status;
            set
            {
                this.status = value;
            }
        }

        public string From
        {
            get => this.from;
            private set
            {
                this.from = value;
            }
        }

        public string To
        {
            get => this.to;
            private set
            {
                this.to = value;
            }
        }
        public double Amount
        {
            get => this.amount;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Amount cannot be negative or zero.");
                }
                this.amount = value;
            }
        }

        public int CompareTo(ITransaction other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
