using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedEcommerce_DB;
using MedEcommerce_Core;

namespace MedEcommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService   _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
          var addresses = await _addressService.GetAddressesAsync();
            return Ok(addresses);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress(int id)
        {
          
            var address = await _addressService.GetAddressesByIdAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            var updatedAdd = await _addressService.UpdateAddressAsync(id,address);
            if (updatedAdd == null)
            {
                return BadRequest();
            }
            return Ok(updatedAdd);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
          var add = await _addressService.CreateAddressAsync(address);

            return CreatedAtAction("PostAddress", new { id = add.Id }, add);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var result = await _addressService.DeleteAddressAsync(id);
            if(!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
