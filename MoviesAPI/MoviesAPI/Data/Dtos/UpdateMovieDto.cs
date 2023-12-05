using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class UpdateMovieDto
    {
        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "A duração do filme é obrigatório.")]
        [Range(70, 600, ErrorMessage = "A duração deve ser entre 70 e 600 minutos.")]
        public int Duration { get; set; }
    }
}
