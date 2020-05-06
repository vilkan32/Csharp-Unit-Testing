namespace INStock.Contracts
{
    using System;

    public interface IProduct : IComparable<IProduct>
    {
        string Label { get; }

        decimal Price { get; set; } 

        int Quantity { get; set; }
    }
}