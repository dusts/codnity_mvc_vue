using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListItemsController : ControllerBase
    {
        private readonly ToDoListContext _context;
        private readonly ILogger<ToDoListItemsController> _logger;

        public ToDoListItemsController(ToDoListContext context, ILogger<ToDoListItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/ToDoList
        [HttpGet(Name = "GetToDoListItems")]
        public async Task<ActionResult<IEnumerable<ToDoListItem>>> GetToDoListItems()
        {
            if (_context.ToDoListItems == null)
            {
                return Ok();
            }

            return await _context.ToDoListItems.ToArrayAsync();
        }

        // POST: api/ToDoListItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoListItem>> PostToDoListItem(ToDoListItem toDoListItem)
        {
            if (_context.ToDoListItems == null)
            {
                return Problem("Entity set 'ToDoListContext.ToDoListItems'  is null.");
            }
            _context.ToDoListItems.Add(toDoListItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoListItem", new { id = toDoListItem.Id }, toDoListItem);
        }

        // DELETE: api/ToDoListItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ToDoListItem>>> DeleteToDoListItem(int id)
        {
            if (_context.ToDoListItems == null)
            {
                return NotFound();
            }
            var toDoListItem = await _context.ToDoListItems.FindAsync(id);
            if (toDoListItem == null)
            {
                return NotFound();
            }

            _context.ToDoListItems.Remove(toDoListItem);
            await _context.SaveChangesAsync();

            if (_context.ToDoListItems == null)
            {
                return Ok();
            }

            return await _context.ToDoListItems.ToArrayAsync();
        }

        private bool ToDoListItemExists(int id)
        {
            return (_context.ToDoListItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
