namespace P02_BlackBoxInteger
{
    using System;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            Type type = Type.GetType("P02_BlackBoxInteger.BlackBoxInteger");

            var instance = Activator.CreateInstance(type, true);

            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            var field = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            
            while (true)
            {
                string[] input = Console.ReadLine().Split('_');
                string methodName = input[0];
                if (methodName == "END")
                {
                    break;
                }
                int parameter = int.Parse(input[1]);
                foreach (var method in methods)
                {
                  

                    if (method.Name == methodName)
                    {
                        method.Invoke(instance, new object[] { parameter });
                        var result = field[0].GetValue(instance);
                        Console.WriteLine(result);
                        break;
                    }
                    
                }
            }

        }
    }
}
