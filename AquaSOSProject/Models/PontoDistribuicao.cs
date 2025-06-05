using System.ComponentModel.DataAnnotations;

namespace AquaSOS.Models
{
    public class PontoDistribuicao
    {
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Range(0, int.MaxValue)]
        public int CapacidadeTotalLitros { get; set; }
    }
}
