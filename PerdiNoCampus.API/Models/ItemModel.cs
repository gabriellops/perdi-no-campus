using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PerdiNoCampus.API.Models
{
    [Table("items")]
    public class ItemModel :  BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
        
        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;

        [Column("categoria")]
        public ECategoriaItem CategoriaItem { get; set; }

        [Column("local_encontrado")]
        public string LocalEncontrado { get; set; } = string.Empty;

        [Column("turno_encontrado")]
        public ETurno TurnoEncontrado { get; set; }

        [Column("usuario_nome")]
        public string UsarioNomeLocalizou { get; set; } = string.Empty;

        [Column("matricula_usuario")]
        public int Matricula { get; set; }

        [Column("imagem_url")]
        public string ImagemUrl { get; set; }

        [Column("foi_recuperado")]
        public bool? FoiRecuperado { get; set; } = false;

        [Column("entregue_prefeitura")]
        public bool? FoiEntregueAPrefeitura { get; set; } = false;

        [Column("ativo")]
        public bool Ativo { get; set; } = true;

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        [Column("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }
    }
}
