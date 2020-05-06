using System;

namespace Repository
{
    public class StartUp
    {
        public static void Main()
        {
            Repository  asd = new Repository();

            asd.Add(new Person("George", 10, new DateTime(2009, 05, 04)));
            asd.Add(new Person("Peter", 5, new DateTime(2014, 05, 24)));

            Person foundPerson = asd.Get(0);
            Console.WriteLine($"{foundPerson.Name} is {foundPerson.Age} years old ({foundPerson.Birthdate:dd/MM/yyyy})");
            //George is 10 years old (04/05/2009)

            Person newPerson = new Person("John", 12, new DateTime(2007, 2, 3));
            asd.Update(2, newPerson); // false
            asd.Update(0, newPerson); // true

            foundPerson = asd.Get(0);
            Console.WriteLine($"{foundPerson.Name} is {foundPerson.Age} years old ({foundPerson.Birthdate:dd/MM/yyyy})");
            //John is 12 years old (03/02/2007)

            Console.WriteLine(asd.Count); // 2

            asd.Delete(5); // false
            asd.Delete(0); // true

            Console.WriteLine(asd.Count); // 1

        }
    }
}
