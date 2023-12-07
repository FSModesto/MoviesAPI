using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos.Session;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public SessionController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult IncludeMovie([FromBody] CreateSessionDto sessionDto)
        {
            Session session = _mapper.Map<Session>(sessionDto);
            _context.Add(session);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecoverSessionById), new { filmeId = session.MovieId, cinemaId = session.CinemaId }, session);
        }

        [HttpGet]
        public IEnumerable<ReadSessionDto> RecoverMovies()
        {
            return _mapper.Map<List<ReadSessionDto>>(_context.Sessions.ToList());
        }

        [HttpGet("{movieId}/{cinemaId}")]
        public IActionResult RecoverSessionById(int movieId, int cinemaId)
        {
            Session? session = _context.Sessions.FirstOrDefault(session => session.MovieId == movieId
                                                                        && session.CinemaId == cinemaId);
            if (session is null) return NotFound();
            ReadSessionDto sessionDto = _mapper.Map<ReadSessionDto>(session);
            return Ok(sessionDto);
        }
    }
}
