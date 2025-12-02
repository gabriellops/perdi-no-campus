using PerdiNoCampus.API.Models;

namespace PerdiNoCampus.API.Contracts
{
    public class CreateItemRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public ECategoriaItem CategoriaItem { get; set; }
        public string LocalEncontrado { get; set; } = string.Empty;
        public ETurno TurnoEncontrado { get; set; }
        public string UsarioNomeLocalizou { get; set; } = string.Empty;
        public int? Matricula { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public bool? FoiEntregueAPrefeitura { get; set; }
    }
}
