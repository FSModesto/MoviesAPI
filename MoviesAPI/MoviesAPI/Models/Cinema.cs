using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do campo é obrigatório!")]
        public string Name { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
