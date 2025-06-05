namespace AquaSOS.DTOs
{
    public class PedidoAguaDTO
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long PontoDistribuicaoId { get; set; }
        public int QuantidadeLitros { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Status { get; set; }
    }
}
