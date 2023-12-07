using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos.Address;
using MoviesAPI.Data.Dtos.Cinema;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public AddressController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult IncludeAddress([FromBody] CreateAddressDto createAddressDto)
        {
            Address address = _mapper.Map<Address>(createAddressDto);
            _context.Add(address);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecoverAddressById), new { id = address.Id }, address);
        }

        [HttpGet]
        public IEnumerable<ReadAddressDto> RecoverAddresses([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadAddressDto>>(_context.Addresses.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult RecoverAddressById(int id)
        {
            Address? address = _context.Addresses.FirstOrDefault(address => address.Id == id);
            if (address is null) return NotFound();
            ReadAddressDto addressDto = _mapper.Map<ReadAddressDto>(address);
            return Ok(addressDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto addressDto)
        {
            Address? address = _context.Addresses.FirstOrDefault(address => address.Id == id);
            if (address is null) return NotFound();
            _mapper.Map(addressDto, address);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            Address? address = _context.Addresses.FirstOrDefault(address => address.Id == id);
            if (address is null) return NotFound();
            _context.Remove(address);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
