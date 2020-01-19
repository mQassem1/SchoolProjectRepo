using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public class SQLAddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLAddressRepository> logger;

        public SQLAddressRepository(ApplicationDbContext context,
                                    ILogger<SQLAddressRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

      

        public Address AddAddress(Address address)
        {
            context.Addresses.Add(address);
            logger.LogInformation("Add Address Successeded");
            context.SaveChanges();
            return address;
        }

        public Address DeleteAddress(int id)
        {
            Address address = context.Addresses.Find(id);

            if(address != null)
            {
                context.Addresses.Remove(address);
                context.SaveChanges();
            }

            return address;
        }

        public Address GetAddress(int id)
        {
            return context.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return context.Addresses.ToList();
        }

        public Address UpdateAddress(Address changedAddress)
        {
            var address = context.Addresses.Attach(changedAddress);
            address.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changedAddress;
        }
    }
}
