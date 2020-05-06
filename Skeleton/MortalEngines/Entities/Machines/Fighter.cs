using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities.Machines
{
    public class Fighter : BaseMachine, IFighter
    {
        private bool aggressiveMode = false;

        public Fighter(string name, double attackPoints, double defensePoints) : base(name, attackPoints, defensePoints, 200)
        {
            this.ToggleAggressiveMode();

        }
        public bool AggressiveMode
        {
            get => this.aggressiveMode;
            private set
            {
                this.aggressiveMode = value;
            }
        }

        public void ToggleAggressiveMode()
        {
            if (this.AggressiveMode == true)
            {
                this.AggressiveMode = false;
                this.AttackPoints -= 50;
                this.DefensePoints += 25;
            }

            if (this.AggressiveMode == false)
            {
                this.AggressiveMode = true;
                this.AttackPoints +=50;
                this.DefensePoints -=25;
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
            if (this.AggressiveMode == false)
            {
                sb.AppendLine(" *Aggressive: OFF");
            }
            else
            {
                sb.AppendLine(" *Aggressive: ON");
            }

            return sb.ToString().TrimEnd();
        }
    }
}


