﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
