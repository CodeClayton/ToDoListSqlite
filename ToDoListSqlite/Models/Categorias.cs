using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListSqlite.Models
{
    public class Categorias
    {
        [Key]
        [Column("IdCategoria")]
        public int IdCategoria { get; set; }
        [Column("NomeCategoria")]
        public string NomeCategoria { get; set; } = string.Empty;

    }
}
