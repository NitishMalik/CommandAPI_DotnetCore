using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;
        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var item = _context.CommandItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command obj)
        {
            _context.CommandItems.Add(obj);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command { ID = obj.ID }, obj);
        }

        [HttpPut("{id}")]
        public ActionResult PutCommand(int id, Command updateObj)
        {
            if (id != updateObj.ID)
            {
                return BadRequest();
            }
            _context.Entry(updateObj).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
    }
}