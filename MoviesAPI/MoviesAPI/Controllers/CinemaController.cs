using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos.Cinema;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public CinemaController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult IncludeCinema([FromBody] CreateCinemaDto createCinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(createCinemaDto);
            _context.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecoverCinemaById), new { id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IEnumerable<ReadCinemaDto> RecoverCinemas([FromQuery] int? addressId = null)
        {
            if (addressId is null)
                return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());

            return _mapper.Map<List<ReadCinemaDto>>
                (_context.Cinemas.FromSqlRaw($"SELECT Id, Name, AddressId FROM cinemas where cinemas.AddressId == {addressId}").ToList());
        }

        [HttpGet("{id}")]
        public IActionResult RecoverCinemaById(int id)
        {
            Cinema? cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema is null) return NotFound();
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Cinema? cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema is null) return NotFound();
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCinema(int id)
        {
            Cinema? cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema is null) return NotFound();
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
