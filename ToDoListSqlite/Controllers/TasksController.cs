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
    public class TasksController : Controller
        
    {

        //Instacia o dbcontext pra executar os bgl
        private readonly AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        public class DtoTask 
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool IsComplete { get; set; }
            public int IdCategoria { get; set; }
        }

        

        [HttpPost]
        public IActionResult Create([FromBody] DtoTask task)
        {
            //Console.WriteLine(task);
            try
            {

                Tasks newTask = new Tasks
                {
                    Name = task.Name,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    IdCategoria = task.IdCategoria
                };

                _context.Add(newTask);
                _context.SaveChanges();
                return Ok(task);
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
                List<Tasks>  tasks = await _context.Tasks.
                                                Include(t => t.Categoria)
                                                .ToListAsync();

                return Ok(tasks);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromBody] DtoTask task)
        {
            try
            {
                var t = await  _context.Tasks.
                    AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == task.Id);

                if(t != null)
                {

                    Tasks newTask = new Tasks
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        IsComplete = task.IsComplete,
                        IdCategoria = task.IdCategoria
                    };

                    _context.Tasks.Update(newTask);
                    _context.SaveChanges();
                    return Ok(task);

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
        public async Task<IActionResult> Delete (int idTask)
        {
            try
            {
                var t = _context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == idTask);
                if (t != null)
                {
                     _context.Tasks.Remove(t);
                    _context.SaveChanges();
                    return Ok("Task Removida");
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
