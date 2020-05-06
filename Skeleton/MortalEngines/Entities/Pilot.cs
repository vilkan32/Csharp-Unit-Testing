using MortalEngines.Entities.Contracts;
using MortalEngines.Entities.Machines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Pilot : IPilot
    {
        private string name;
        private IList<IMachine> machines;


        public Pilot(string name)
        {
            this.Name = name;
            this.machines = new List<IMachine>();

        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Pilot name cannot be null or empty string.");
                }

                this.name = value;
            }
        }

        public void AddMachine(IMachine machine)
        {
            if (machine == null)
            {
                throw new NullReferenceException("Null machine cannot be added to the pilot.");
            }

            this.machines.Add(machine);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.Name} - {this.machines.Count} machines");

            foreach (var item in machines)
            {
                sb.AppendLine($"- {item.Name}");
                sb.AppendLine($" *Type: {item.GetType().Name}");
                sb.AppendLine($" *Health: {item.HealthPoints}");
                sb.AppendLine($" *Attack: {item.AttackPoints}");
                sb.AppendLine($" *Defense: {item.DefensePoints}");
                if (item.Targets.Count == 0)
                {
                    sb.AppendLine(" *Targets: None");
                }
                else
                {
                    sb.AppendLine($" *Targets: {string.Join(",", item.Targets)}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
