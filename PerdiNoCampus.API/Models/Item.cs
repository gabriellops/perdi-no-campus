using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PerdiNoCampus.API.Models
{
    [Table("items")]
    public class Item :  BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("name")]
        public string Nome { get; set; }

        [Column("categoria")]
        public string Categoria { get; set; }

        [Column("local")]
        public string Local { get; set; }

        [Column("horario")]
        public DateTime Horario { get; set; }

        [Column("usuario_nome")]
        public string UsarioNomeLocalizou { get; set; }

        [Column("matricula")]
        public int Matricula { get; set; }

        [Column("imagem_url")]
        public string ImagemUrl { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }
    }
}
