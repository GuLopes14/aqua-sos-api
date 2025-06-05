using System.ComponentModel.DataAnnotations;

namespace AquaSOS.Models
{
    public class PedidoAgua
    {
        public long Id { get; set; }

        [Required]
        public long UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        public long PontoDistribuicaoId { get; set; }
        public PontoDistribuicao? PontoDistribuicao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int QuantidadeLitros { get; set; }

        public DateTime DataSolicitacao { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}