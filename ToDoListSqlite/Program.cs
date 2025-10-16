using Microsoft.Data.Sqlite;
using ToDoListSqlite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar EF com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ToDo.db"));

//APOS ISSO CRIAR DB CONTEXT
//DEPOIS CRIAR TABELAS
//RODAR COMANDO DO DBCONTEXT

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(
               "http://localhost:4200",
               "http://localhost:8080"
            )
            .AllowAnyMethod()   // Permite qualquer método HTTP 
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader();    // Permite qualquer cabeçalho HTTP
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
