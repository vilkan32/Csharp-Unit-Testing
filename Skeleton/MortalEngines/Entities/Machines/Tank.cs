using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities.Machines
{
    public class Tank : BaseMachine, ITank
    {
        private bool defenseMode = false;

        public Tank(string name, double attackPoints, double defensePoints) : base(name,attackPoints, defensePoints,100)
        {
            this.ToggleDefenseMode();
        }
        public bool DefenseMode
        {
            get => this.defenseMode;
            private set
            {
                this.defenseMode = value;
            }
        }

        public void ToggleDefenseMode()
        {
            if (this.DefenseMode == true)
            {
                this.DefenseMode = false;
                this.AttackPoints +=40;
                this.DefensePoints -= 30;
            }

            if (this.DefenseMode == false)
            {
                this.DefenseMode = true;
                this.AttackPoints -= 40;
                this.DefensePoints += 30;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Health: {this.HealthPoints:f2}");
            sb.AppendLine($" *Attack: {this.AttackPoints:f2}");
            sb.AppendLine($" *Defense: {this.DefensePoints:f2}");
            if (this.Targets.Count == 0)
            {
                sb.AppendLine(" *Targets: None");
            }
            else
            {
                sb.AppendLine($" Targets: {string.Join(",", this.Targets)}");
            }
            if (this.DefenseMode == false)
            {
                sb.AppendLine(" *Defense: OFF");
            }
            else
            {
                sb.AppendLine(" *Defense: ON");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
