using Microsoft.AspNetCore.Mvc;
using ToDoListSqlite.Data;
using ToDoListSqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoListSqlite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Categorias cat)
        {
            try
            {
                _context.Categorias.Add(cat);
                _context.SaveChanges();
                return Ok(cat);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        async public Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<Categorias> categorias = await _context.Categorias.ToListAsync();
                return Ok(categorias);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpDelete]
        async public Task<IActionResult> Delete(int idCategoria)
        {
            try
            {
                var cat = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
                if(cat != null)
                {
                    Tasks? task = _context.Tasks.FirstOrDefault(t => t.IdCategoria == idCategoria);

                    if(task != null)
                    {
                        return StatusCode(500, new { mensagem = "Categoria sendo usada em uma ou mais tasks", task });

                    }

                    _context.Categorias.Remove(cat);
                    await _context.SaveChangesAsync();
                    return Ok("Categoria Removida com sucesso");
                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
