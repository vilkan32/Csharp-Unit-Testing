using MortalEngines.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Core
{
    public class Engine : IEngine
    {

        private MachinesManager manager;
        public Engine()
        {
            this.manager = new MachinesManager();
        }
        public void Run()
        {
            while (true)
            {
                string[] args = Console.ReadLine().Split();

                if (args[0] == "Quit")
                {
                    break;
                }

                try
                {
                    if (args[0] == "HirePilot")
                    {
                        Console.WriteLine(manager.HirePilot(args[1]));
                    }

                    else if (args[0] == "PilotReport")
                    {
                        Console.WriteLine(manager.PilotReport(args[1]));
                    }
                    // ManufactureTank {name} {attack} {defense}

                    else if (args[0] == "ManufactureTank")
                    {
                        Console.WriteLine(this.manager.ManufactureTank(args[1], double.Parse(args[2]), double.Parse(args[3])));
                    }
                    // ManufactureFighter {name} {attack} {defense}

                    else if (args[0] == "ManufactureFighter")
                    {
                        Console.WriteLine(this.manager.ManufactureFighter(args[1], double.Parse(args[2]), double.Parse(args[3])));
                    }
                    // MachineReport { name}

                    else if (args[0] == "MachineReport")
                    {
                        Console.WriteLine(this.manager.MachineReport(args[1]));

                    }
                    // AggressiveMode {name}

                    else if (args[0] == "AggressiveMode")
                    {
                        Console.WriteLine(this.manager.ToggleFighterAggressiveMode(args[1]));
                    }
                    // DefenseMode {name}
                    else if (args[0] == "DefenseMode")
                    {
                        Console.WriteLine(this.manager.ToggleTankDefenseMode(args[1]));
                    }
                    // Engage {pilot name} {machine name}

                    else if (args[0] == "Engage")
                    {
                        Console.WriteLine(this.manager.EngageMachine(args[1], args[2]));
                    }
                    // Attack {attacking machine name} {defending machine name}

                    else if (args[0] == "Attack")
                    {
                        Console.WriteLine(this.manager.AttackMachines(args[1], args[2]));
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
