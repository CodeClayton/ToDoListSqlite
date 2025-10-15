using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToDoListSqlite.Models
{
    public class Tasks
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; } = string.Empty;
        [Column("Description")]
        public string Description { get; set; } = string.Empty;
        [Column("IsComplete")]
        public bool IsComplete { get; set; }

        //Quando tiver chave estrangeira e assim.
        [Column("IdCategoria")]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        
        public Categorias? Categoria { get; set; }


    }
}
