using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListClassLibrary;
using ToDoListWebAPI.Data;

namespace ToDoListWebAPI.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext todocontext;

        public ToDoController(ToDoDbContext context)
        {
            this.todocontext = context;
        }
        [HttpGet("/")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            // Retrieve ToDoItems with no CompletedDate set
            var toDoItems = await todocontext.ToDoItems.Where(item => item.CompletedDate == null).ToListAsync();

            return Ok(toDoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItemById(int id)
        {
            // Retrieve ToDoItem based on the provided Id
            var toDoItem = await todocontext.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return Ok(toDoItem);
        }

        [HttpPost("/")]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem(ToDoItem toDoItem)
        {
            // Validate the input model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new ToDoItem to the database
            todocontext.ToDoItems.Add(toDoItem);
            await todocontext.SaveChangesAsync();

            // Return the newly created ToDoItem
            return CreatedAtAction(nameof(GetToDoItemById), new { id = toDoItem.Id }, toDoItem);
        }
    }
}
