namespace StorageMaster.Entities.Storage
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Products;
	using Vehicles;

	public abstract class Storage
	{
		private readonly Vehicle[] garage;
		private readonly List<Product> products;

		protected Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
		{
			this.Name = name;
			this.Capacity = capacity;
			this.GarageSlots = garageSlots;

			this.garage = new Vehicle[garageSlots];
			this.products = new List<Product>();

			this.InitializeGarage(vehicles);
		}
        // done for warehouse
		public string Name { get; }
        // done for warehouse
		public int Capacity { get; }
        // done for warehouse
		public int GarageSlots { get; }
        // done for warehouse
		public bool IsFull => this.products.Sum(p => p.Weight) >= this.Capacity;

		public IReadOnlyCollection<Vehicle> Garage => Array.AsReadOnly(this.garage);
        // done for warehouse 
		public IReadOnlyCollection<Product> Products => this.products.AsReadOnly();
        // done for warehouse
		public Vehicle GetVehicle(int garageSlot)
		{
			if (garageSlot >= this.garage.Length)
			{
				throw new InvalidOperationException("Invalid garage slot!");
			}

			var vehicle = this.garage[garageSlot];
			if (vehicle == null)
			{
				throw new InvalidOperationException("No vehicle in this garage slot!");
			}

			return vehicle;
		}
        // done for warehouse 
		public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
		{
			var vehicle = GetVehicle(garageSlot);

			var deliveryGarageHasFreeSlot = deliveryLocation.Garage.Any(v => v == null);
			if (!deliveryGarageHasFreeSlot)
			{
				throw new InvalidOperationException("No room in garage!");
			}

			this.garage[garageSlot] = null;

			var addedSlot = deliveryLocation.AddVehicle(vehicle);
			return addedSlot;
		}
        // done for warehouse
		public int UnloadVehicle(int garageSlot)
		{
			if (this.IsFull)
			{
				throw new InvalidOperationException("Storage is full!");
			}

			var vehicle = GetVehicle(garageSlot);

			var unloadedProductCount = 0;
			while (!vehicle.IsEmpty && !this.IsFull)
			{
				var crate = vehicle.Unload();
				this.products.Add(crate);

				unloadedProductCount++;
			}

			return unloadedProductCount;
		}
        // done for warehouse
		private void InitializeGarage(IEnumerable<Vehicle> vehicles)
		{
			var garageSlot = 0;
			foreach (var vehicle in vehicles)
			{
				this.garage[garageSlot++] = vehicle;
			}
		}
        // done for warehouse
		private int AddVehicle(Vehicle vehicle)
		{
			var freeGarageSlotIndex = Array.IndexOf(this.garage, null);
			this.garage[freeGarageSlotIndex] = vehicle;

			return freeGarageSlotIndex;
		}
	}
}