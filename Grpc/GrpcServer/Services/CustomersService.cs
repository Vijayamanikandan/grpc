using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        public ILogger<CustomersService> logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            this.logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            switch (request.UserId)
            {
                case 1:
                    output.FirstName = "Vijay";
                    output.LastName = "A";
                    output.Emailaddress = "Vijay@mail.com";
                    output.IsAlive = true;
                    output.Age = 25;
                    break;

                case 2:

                    output.FirstName = "Joe";
                    output.LastName = "Smith";
                    output.Emailaddress = "Joe@smith.com";
                    output.IsAlive = true;
                    output.Age = 35;
                    break;

                default:

                    output.FirstName = "John";
                    output.LastName = "Doe";
                    output.Emailaddress = "john@Doe.com";
                    output.IsAlive = true;
                    output.Age = 45;
                    break;

            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        { 
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Vijay",
                    LastName = "A",
                    Emailaddress = "Vijay@mail.com",
                    IsAlive = true,
                    Age = 25,
                },
                 new CustomerModel
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Emailaddress = "John@mail.com",
                    IsAlive = true,
                    Age = 35,
                },
                  new CustomerModel
                {
                    FirstName = "Joe",
                    LastName = "doe",
                    Emailaddress = "joe@mail.com",
                    IsAlive = true,
                    Age =45,
                }
            };
            foreach (var customer in customers )
            {
               await responseStream.WriteAsync(customer);
            }
        }
    }
}
