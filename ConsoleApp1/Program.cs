namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadTest();
            AsyncMethod();
            GetInfoFromApi();
        }
        static void ThreadTest()
        {

            Thread t1 = new Thread(new ThreadStart(Method1));
            Thread t2 = new Thread(new ThreadStart(Method2));
            t1.Start();
            t2.Start();


            static void Method1()
            {
                Console.WriteLine("Method 1 is executing");
                Thread.Sleep(2000);
                Console.WriteLine("Method 1 finished");

            }
            static void Method2()
            {
                Console.WriteLine("Method 2 is executing");
                Thread.Sleep(3000);
                Console.WriteLine("Method 2 finished");

            }
        }
        static async Task AsyncMethod()
        {
            Console.WriteLine("start");
            await Task.Delay(3000);
            Console.WriteLine("finish");
        }

        static async Task GetInfoFromApi()
        {
            Console.WriteLine("GetInfoFromApi");
            var client= new HttpClient();
            var response = await client.GetAsync("https://jsonplaceholder.org/comments/1");
            if (response != null)
            {
                string data =  await response.Content.ReadAsStringAsync();
                Console.WriteLine($"InfoFromApi:{data}");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
