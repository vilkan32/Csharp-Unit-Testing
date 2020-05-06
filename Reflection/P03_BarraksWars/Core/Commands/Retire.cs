using _03BarracksFactory.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace _03BarracksFactory.Core.Commands
{
    using Core.Factories;
    using Data;
    public class Retire : Command
    {
        [Inject]
        private IRepository repository;
        [Inject]
        private IUnitFactory unitFactory;

        public Retire(string[] data) : base(data)
        {
        }

        public override string Execute()
        {
            string unitType = this.Data[1];        
            return this.repository.RemoveUnit(unitType);           
            
        }
    }
}
