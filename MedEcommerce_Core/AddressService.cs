using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_DB;
using Microsoft.EntityFrameworkCore;

namespace MedEcommerce_Core
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;

        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address); 
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            var add = await _context.Addresses.FindAsync(id);
            if (add == null)
            {
                return false;
            }
            _context.Addresses.Remove(add);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesByIdAsync(int id)
        {
            return await _context.Addresses.Where(u => u.UserId == id).ToListAsync();
        }

        public async Task<Address> UpdateAddressAsync(int id, Address address)
        {
            var existingAdd = await _context.Addresses.FindAsync(id);
            if (existingAdd == null)
            {
                return null;
            }
            _context.Entry(existingAdd).CurrentValues.SetValues(address);
            await _context.SaveChangesAsync();
            return existingAdd;
        }
    }
}
