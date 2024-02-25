using System.Reflection;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Laptop laptopLenovo = new(1, "Lenovo", 14, 15.300, true, "Windows");
            Laptop laptopMacbook = new(2, "Macbook", 14, 45.300, false, "MacOS");
            Type laptopType = typeof(Laptop);
            TypeInfo typelaptopInfo = laptopType.GetTypeInfo();
            Console.WriteLine($"TypeName:{typelaptopInfo.Name}");


            Console.WriteLine("Members:");
            foreach (MemberInfo member in typelaptopInfo.DeclaredMembers)
            {
                Console.WriteLine(member.Name );
            }
            Console.WriteLine("Fields:");
            foreach(FieldInfo fileInfo in typelaptopInfo.DeclaredFields) {
                Console.WriteLine(fileInfo.Name);
            }
            Console.WriteLine("MethodInfo:");
            foreach (MethodInfo methodInfo in typelaptopInfo.DeclaredMethods)
            {
                Console.WriteLine(methodInfo.Name);
            }

            Console.WriteLine("Reflection:");
            MethodInfo methodInfo1 = laptopType.GetMethod("getBrand");
            if(methodInfo1 != null)
            {
                methodInfo1.Invoke(laptopLenovo, [laptopMacbook]);
            }

        }

    }
}
