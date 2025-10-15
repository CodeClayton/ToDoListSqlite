using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoListSqlite.Data;
using ToDoListSqlite.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListSqlite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
        
    {

        //Instacia o dbcontext pra executar os bgl
        private readonly AppDbContext _context;
        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        public class Dtotodo 
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool IsComplete { get; set; }
            public int IdCategoria { get; set; }
        }

        

        [HttpPost]
        public IActionResult Create([FromBody] Dtotodo todo)
        {
            //Console.WriteLine(todo);
            try
            {

                Todo newtodo = new Todo
                {
                    Name = todo.Name,
                    Description = todo.Description,
                    IsComplete = todo.IsComplete,
                    IdCategoria = todo.IdCategoria
                };

                _context.Add(newtodo);
                _context.SaveChanges();
                return Ok(todo);
            }
            catch 
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<Todo>  Todo = await _context.Todo.
                                                Include(t => t.Categoria)
                                                .ToListAsync();

                return Ok(Todo);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromBody] Dtotodo todo)
        {
            try
            {
                var t = await  _context.Todo.
                    AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == todo.Id);

                if(t != null)
                {

                    Todo newtodo = new Todo
                    {
                        Id = todo.Id,
                        Name = todo.Name,
                        Description = todo.Description,
                        IsComplete = todo.IsComplete,
                        IdCategoria = todo.IdCategoria
                    };

                    _context.Todo.Update(newtodo);
                    _context.SaveChanges();
                    return Ok(todo);

                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (int idtodo)
        {
            try
            {
                var t = _context.Todo.AsNoTracking().FirstOrDefault(t => t.Id == idtodo);
                if (t != null)
                {
                     _context.Todo.Remove(t);
                    _context.SaveChanges();
                    return Ok("todo Removida");
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch { return StatusCode(500); }
        }
    }
}
