using PerdiNoCampus.API.Models;

namespace PerdiNoCampus.API.Contracts
{
    public class UpdateItemRequest
    {
        public string Nome { get; set; }
        public ECategoriaItem CategoriaItem { get; set; }
        public string LocalEncontrado { get; set; }
        public ETurno TurnoEncontrado { get; set; }
        public string ImagemUrl { get; set; }
        public bool? FoiEntregueAPrefeitura { get; set; }
        public bool? FoiRecuperado { get; set; }
    }
}
