using System.ComponentModel.DataAnnotations;

namespace AquaSOS.Models
{
    public class Usuario
    {
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
