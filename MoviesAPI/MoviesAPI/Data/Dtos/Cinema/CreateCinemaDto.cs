using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos.Cinema
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "O nome do campo é obrigatório!")]
        public string Name { get; set; }

        public int AddressId { get; set; }
    }
}
