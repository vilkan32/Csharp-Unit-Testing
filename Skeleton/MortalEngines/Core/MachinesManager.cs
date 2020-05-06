namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Entities;
    using MortalEngines.Entities.Contracts;
    using MortalEngines.Entities.Machines;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {

        private List<IPilot> pilots;
        private List<IMachine> machines;
        public MachinesManager()
        {
            this.pilots = new List<IPilot>();
            this.machines = new List<IMachine>();
        }
        public string HirePilot(string name)
        {
            if (this.pilots.FirstOrDefault(x => x.Name == name) != null)
            {
                return $"Pilot {name} is hired already";
            }

            var pilot = new Pilot(name);
            this.pilots.Add(pilot);
            return $"Pilot {name} hired";
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            var tank = this.machines.FirstOrDefault(x => x.GetType().Name == "Tank" && x.Name == name);
            if (tank != null)
            {
                return $"Machine {name} is manufactured already";
            }

            var createTank = new Tank(name, attackPoints, defensePoints);

            this.machines.Add(createTank);
            return $"Tank {name} manufactured - attack: {createTank.AttackPoints:f2}; defense: {createTank.DefensePoints:f2}";
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            var fighter = this.machines.FirstOrDefault(x => x.Name == name);
            if (fighter != null)
            {
                return $"Machine {name} is manufactured already";
            }

            var createFighter = new Fighter(name, attackPoints, defensePoints);
            this.machines.Add(createFighter);
            return $"Fighter {name} manufactured - attack: {createFighter.AttackPoints:f2}; defense: {createFighter.DefensePoints:f2}; aggressive: ON";
            

        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            var pilot = this.pilots.FirstOrDefault(x => x.Name == selectedPilotName);
            var machine = this.machines.FirstOrDefault(x => x.Name == selectedMachineName);

            if (pilot == null)
            {
                return $"Pilot {selectedPilotName} could not be found";
            }

            if (machine == null)
            {
                return $"Machine {selectedMachineName} could not be found";
            }

            if (machine.Pilot != null)
            {
                return $"Machine {selectedMachineName} is already occupied";
            }

            pilot.AddMachine(machine);

            return $"Pilot {selectedPilotName} engaged machine {selectedMachineName}";
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            var attackingMachine = this.machines.FirstOrDefault(x => x.Name == attackingMachineName);

            var defendingMachine = this.machines.FirstOrDefault(x => x.Name == defendingMachineName);

            if (attackingMachine == null)
            {
                return $"Machine {attackingMachineName} could not be found";
            }

            if (defendingMachine == null)
            {
                return $"Machine {defendingMachineName} could not be found";
            }

            if (attackingMachine.HealthPoints <= 0)
            {
                return $"Dead machine {attackingMachineName} cannot attack or be attacked";
            }

            if (defendingMachine.HealthPoints <= 0)
            {
                return $"Dead machine {defendingMachineName} cannot attack or be attacked";
            }
            attackingMachine.Attack(defendingMachine);
            return $"Machine {defendingMachineName} was attacked by machine {attackingMachineName} - current health: {defendingMachine.HealthPoints:f2}";

        }

        public string PilotReport(string pilotReporting)
        {
            var pilot = this.pilots.FirstOrDefault(x => x.Name == pilotReporting);

            return pilot.Report();
        }

        public string MachineReport(string machineName)
        {
            var machine = this.machines.FirstOrDefault(x => x.Name == machineName);

            return machine.ToString();
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            var fighter = this.machines.FirstOrDefault(x => x.Name == fighterName);
            if (fighter != null)
            {
                var result = fighter as Fighter;

                result.ToggleAggressiveMode();

                return $"Fighter {fighterName} toggled aggressive mode";
            }
            else
            {
                return $"Machine {fighterName} could not be found";
            }
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            var tank = this.machines.FirstOrDefault(x => x.Name == tankName);
            if (tank != null)
            {
                var result = tank as Tank;

                result.ToggleDefenseMode();

                return $"Tank {tankName} toggled defense mode";
            }
            else
            {
                return $"Machine {tankName} could not be found";
            }

        }
    }
}