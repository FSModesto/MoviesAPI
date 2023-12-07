using MoviesAPI.Data.Dtos.Session;

namespace MoviesAPI.Data.Dtos.Movie
{
    public class ReadMovieDto
    {
        public string Title { get; set; }

        public string Gender { get; set; }

        public int Duration { get; set; }

        public DateTime GetTime { get; set; } = DateTime.Now;

        public IEnumerable<ReadSessionDto> Sessions { get; set; }
    }
}
