namespace StorageMaster.Entities.Vehicles
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Products;

	public abstract class Vehicle
	{
		private readonly List<Product> trunk;

		protected Vehicle(int capacity)
		{
			this.Capacity = capacity;

			this.trunk = new List<Product>();
		}
        // done for van 
		public int Capacity { get; }
        // done for van 
		public IReadOnlyCollection<Product> Trunk => this.trunk.AsReadOnly();
        // done for van
		public bool IsFull => this.Trunk.Sum(c => c.Weight) >= Capacity;
        // done for van
		public bool IsEmpty => !this.Trunk.Any();
        // done for van,
		public void LoadProduct(Product product)
		{
			if (this.IsFull)
			{
				throw new InvalidOperationException("Vehicle is full!");
			}

			this.trunk.Add(product);
		}
        // done for van
		public Product Unload()
		{
			if (!this.trunk.Any())
			{
				throw new InvalidOperationException("No products left in vehicle!");
			}

			var product = this.trunk.Last();
			this.trunk.RemoveAt(this.trunk.Count - 1);

			return product;
		}
	}
}