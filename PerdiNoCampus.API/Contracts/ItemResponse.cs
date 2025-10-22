namespace PerdiNoCampus.API.Contracts
{
    public class ItemResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Local { get; set; }
        public DateTime Horario { get; set; }
        public string UsarioNomeLocalizou { get; set; }
        public string ImagemUrl { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
