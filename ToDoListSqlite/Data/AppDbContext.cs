using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ToDoListSqlite.Models;

namespace ToDoListSqlite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        
        public DbSet<Todo> Todo { get; set; }
        public DbSet<Categorias> Categorias { get; set; }

        // APOS ISSO RODAR
        //dotnet ef migrations add Inicial
        //dotnet ef database update

        // HAS ONE ( BLOCO INTEIRO ) FK E DPS ON DELETE SEM CASCADE.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(e => e.Categoria)
                .WithMany()
                .HasForeignKey(e => e.IdCategoria)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
