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
    public class TeamController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TeamController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var teams = _context.Team.Include(t => t.Players)
                .Include(t => t.EventTeams)
                .Where(t => t.Players.Count > 1).OrderByDescending(t => t.EventTeams.Count).ToList();

            var models = teams.Select(t => new TeamModel(t, false));

            return Ok(models);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var team = _context.Team.Include(t => t.Players)
                .Include(t => t.EventTeams)
                .ThenInclude(t => t.Event)
                .FirstOrDefault(t => t.Id == id);

            var model = new TeamModel(team);

            return Ok(model);
        }
    }
}