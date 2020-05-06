using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Repository
    {
        private Dictionary<int, Person> data;

        private int counter;

        public int Count { get { return this.data.Count; } }

        public Repository()
        {
            this.data = new Dictionary<int, Person>();
           
        }

        public void Add(Person person)
        {
            this.data.Add(counter++, person);
        }

        public Person Get(int id)
        {
            return this.data.Where(x => x.Key == id).Select(x => x.Value).FirstOrDefault();
        }

        public bool Update(int id, Person newPerson)
        {

            if (this.data.ContainsKey(id))
            {
                this.data[id] = newPerson;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Delete(int id)
        {           
               return this.data.Remove(id);           
        }
        
    }
}
