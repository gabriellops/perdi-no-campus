namespace PerdiNoCampus.API.Contracts
{
    public class CreateItemRequest
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Local { get; set; }
        public DateTime Horario { get; set; }
        public string UsarioNomeLocalizou { get; set; }
        public int Matricula { get; set; }
        public string ImagemUrl { get; set; }
    }
}
