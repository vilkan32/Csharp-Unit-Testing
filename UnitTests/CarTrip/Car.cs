using System;

namespace CarTrip
{
    public class Car
    {
        private string model;
        private double fuelAmount;
        private double fuelConsumptionPerKm;
        // done
        public Car(string model, double tankCapacity, double tank, double fuelConsumptionPerKm)
        {
            this.Model = model;
            this.TankCapacity = tankCapacity;
            this.FuelAmount = tank;
            this.FuelConsumptionPerKm = fuelConsumptionPerKm;
        }
        // done
        public double TankCapacity { get; private set; }
        // done
        public string Model
        {
            // done
            get => this.model;
            // done
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{nameof(this.Model)} is required");
                }

                this.model = value;
            }
        }

        public double FuelAmount
        {
            // done
            get => this.fuelAmount;
            // done
            set
            {
                if (value > this.TankCapacity)
                {
                    throw new ArgumentException($"{nameof(FuelAmount)} cannot be more than {nameof(this.TankCapacity)}");
                }

                this.fuelAmount = value;
            }
        }

        public double FuelConsumptionPerKm
        {
            // done
            get => this.fuelConsumptionPerKm;
            // done
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Invalid {nameof(this.FuelConsumptionPerKm)}");
                }

                this.fuelConsumptionPerKm = value;
            }
        }

        public string Drive(double distance)
        {
            double tripConsumotion = distance * this.FuelConsumptionPerKm;
            // done
            if (this.FuelAmount < tripConsumotion)
            {
                throw new InvalidOperationException("Cannot travel this distance");
            }
            // done
            this.FuelAmount -= tripConsumotion;
            return "Have a nice trip";
        }

        public void Refuel(double fuelAmount)
        {
            double totalFuelAmount = this.FuelAmount + fuelAmount;
            // done
            if (totalFuelAmount > this.TankCapacity)
            {
                throw new InvalidOperationException($"Cannot fill {fuelAmount} in the tank");
            }
            // done
            this.FuelAmount = totalFuelAmount;
        }
    }
}
