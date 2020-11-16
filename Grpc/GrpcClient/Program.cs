using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(new HelloRequest() { Name = "Vijay" });
            //Console.WriteLine(reply.Message);
            //Console.ReadLine();

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var custClient = new Customer.CustomerClient(channel);
            var reply = await custClient.GetCustomerInfoAsync(new CustomerLookupModel() { UserId = 1 });
            Console.WriteLine($" {reply.FirstName} {reply.LastName} is {reply.Age} years old");
            Console.WriteLine($" ---------------------------------------------------------");
            Console.WriteLine($" New customers:");
            using (var call = custClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    _ = Task.Delay(1000);
                    var currentcust = call.ResponseStream.Current;
                    Console.WriteLine($" {currentcust.FirstName} {currentcust.LastName} is {currentcust.Age} years old");

                }
            }
            Console.ReadLine();
        }
    }
}
