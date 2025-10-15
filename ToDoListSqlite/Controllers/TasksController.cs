using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoListSqlite.Data;
using ToDoListSqlite.Models;
using Microsoft.EntityFrameworkCore;

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

        

        [HttpPost]
        public IActionResult Create([FromBody] Tasks task)
        {
            Console.WriteLine(task);
            try
            {
                _context.Add(task);
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

    }
}
