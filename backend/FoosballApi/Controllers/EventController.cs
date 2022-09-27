using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using FoosballApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoosballApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EventController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var events = _context.Event.OrderByDescending(e => e.Date).ToList();
            
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var e = _context.Event.Include(e => e.Teams)
                .ThenInclude(t => t.Team)
                .ThenInclude(t => t.Players)
                .FirstOrDefault(e => e.Id == id);

            var model = new EventModel(e, true);

            return Ok(model);
        }
    }
}