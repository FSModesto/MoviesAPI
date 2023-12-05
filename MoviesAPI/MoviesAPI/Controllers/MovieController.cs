using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="movieDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult IncludeMovie([FromBody] CreateMovieDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            _context.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecoverMovieById), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public IEnumerable<ReadMovieDto> RecoverMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult RecoverMovieById(int id)
        {
            Movie? movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie is null) return NotFound();
            ReadMovieDto movieDto = _mapper.Map<ReadMovieDto>(movie);
            return Ok(movieDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto)
        {
            Movie? movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie is null) return NotFound();
            _mapper.Map(movieDto, movie);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchMovie(int id, JsonPatchDocument<UpdateMovieDto> jsonPatch)
        {
            Movie? movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie is null) return NotFound();

            UpdateMovieDto movieToPatch = _mapper.Map<UpdateMovieDto>(movie);
            jsonPatch.ApplyTo(movieToPatch, ModelState);
            if (!TryValidateModel(movieToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(movieToPatch, movie);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            Movie? movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie is null) return NotFound();
            _context.Remove(movie);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
