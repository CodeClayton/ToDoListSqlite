using Microsoft.EntityFrameworkCore;
using ToDoListSqlite.Models;

namespace ToDoListSqlite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        
        public DbSet<Tasks> Tasks { get; set; }

        // APOS ISSO RODAR
        //dotnet ef migrations add Inicial
        //dotnet ef database update
    }
}
