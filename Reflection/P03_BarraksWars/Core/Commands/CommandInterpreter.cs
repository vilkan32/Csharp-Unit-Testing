using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace _03BarracksFactory.Core.Commands
{
    using Contracts;
    using System.Linq;

    public class CommandInterpreter : ICommandInterpreter
    {
        
        private IRepository repository;
        private IUnitFactory unitFactory;
        private IServiceCollection serviceCollection;


        public CommandInterpreter(IRepository repository, IUnitFactory unitFactory)
        {
            this.repository = repository;
            this.unitFactory = unitFactory;
        }
        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            var firstChar = commandName[0];
            var result = string.Empty;
            result += firstChar.ToString().ToUpper();
            for (int i = 1; i < commandName.Length; i++)
            {
                result += commandName[i].ToString().ToLower();
            }
            
            Type type = Type.GetType($"_03BarracksFactory.Core.Commands.{result}");
            var instance = (IExecutable)Activator.CreateInstance(type, new object[] { data });
            serviceCollection.AddScoped<IExecutable, Command>();

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            var currType = this.GetType();
            var specified = currType.GetRuntimeFields();
            foreach (var field in fields)
            {
                foreach (var item in specified)
                {
                    if (field.CustomAttributes.Any())
                    {
                        if (field.FieldType.Name == item.FieldType.Name)
                        {
                            field.SetValue(instance, item.GetValue(this));
                        } 

                    }
                }
             
            }
            return instance;

        }
    }
}
