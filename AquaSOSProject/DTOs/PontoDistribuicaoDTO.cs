namespace AquaSOS.DTOs
{
    public class PontoDistribuicaoDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public int CapacidadeTotalLitros { get; set; }
    }
}
