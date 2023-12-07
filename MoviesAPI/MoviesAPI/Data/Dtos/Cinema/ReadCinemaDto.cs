using MoviesAPI.Data.Dtos.Address;
using MoviesAPI.Data.Dtos.Session;

namespace MoviesAPI.Data.Dtos.Cinema
{
    public class ReadCinemaDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ReadAddressDto Address { get; set; }

        public IEnumerable<ReadSessionDto> Sessions { get; set; }
    }
}
