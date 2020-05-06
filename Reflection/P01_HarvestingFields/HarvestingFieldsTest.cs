 namespace P01_HarvestingFields
{
    using System;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            Type type = Type.GetType("P01_HarvestingFields.HarvestingFields");
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "HARVEST")
                {
                    break;
                }
                foreach (FieldInfo item in fields)
                {
                    if (input == "private")
                    {
                        if (item.IsPrivate == true)
                        {
                            Console.WriteLine($"private {item.FieldType.Name} {item.Name}");
                        }
                    }
                    else if (input == "protected")
                    {
                        if (item.IsFamily == true)
                        {
                            Console.WriteLine($"protected {item.FieldType.Name} {item.Name}");
                        }
                    }
                    else if (input == "public")
                    {
                        if (item.IsPublic == true)
                        {
                            Console.WriteLine($"public {item.FieldType.Name} {item.Name}");
                        }
                    }
                    else
                    {
                        string modifier = string.Empty;
                        if (item.IsPublic)
                        {
                            modifier = "public";
                        }
                        else if (item.IsFamily)
                        {
                            modifier = "protected";
                        }
                        else if (item.IsPrivate)
                        {
                            modifier = "private";
                        }
                            Console.WriteLine($"{modifier} {item.FieldType.Name} {item.Name}");                       
                    }
                }
            }
            

        }
    }
}
