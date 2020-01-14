using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAllAddresses();
        Address GetAddress(int id);
        Address DeleteAddress(int id);
        Address AddAddress(Address address);
        Address UpdateAddress(Address changedAddress);
    }
}
