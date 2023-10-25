using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_DB;

namespace MedEcommerce_Core
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddressesAsync();
        Task<IEnumerable<Address>> GetAddressesByIdAsync(int id);
        Task<Address> UpdateAddressAsync(int id, Address address);
        Task<Address> CreateAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(int id);
    }
}
