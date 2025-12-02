using PerdiNoCampus.API.Models;

namespace PerdiNoCampus.API.Contracts
{
    public class UpdateItemRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ECategoriaItem CategoriaItem { get; set; }
        public string LocalEncontrado { get; set; }
        public ETurno TurnoEncontrado { get; set; }
        public string UsarioNomeLocalizou { get; set; }
        public int Matricula { get; set; }
        public string ImagemUrl { get; set; }
        public bool? FoiRecuperado { get; set; }
        public bool? FoiEntregueAPrefeitura { get; set; }
        public bool Ativo { get; set; }
    }
}
