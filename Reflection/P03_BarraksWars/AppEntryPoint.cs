namespace _03BarracksFactory
{
    using Contracts;
    using Core;
    using Core.Factories;
    using Data;
    using Core.Commands;
    class AppEntryPoint
    {
        static void Main(string[] args)
        {
            IRepository repository = new UnitRepository();
            IUnitFactory unitFactory = new UnitFactory();
            ICommandInterpreter executable = new CommandInterpreter(repository, unitFactory);
            IRunnable engine = new Engine(repository, unitFactory, executable);
            engine.Run();
        }
    }
}
