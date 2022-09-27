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
    public class PlayerController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PlayerController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var players = _context.Player.OrderByDescending(p => p.PlayedEvents);

            return Ok(players);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var player = _context.Player.Include(p => p.Teams).ThenInclude(t => t.Players)
                .First(p => p.Id == id);

            var eventTeams = _context.EventTeam.Include(e => e.Team)
                .ThenInclude(t => t.Players)
                .Include(e => e.Event).Where(et => et.Team.Players.FirstOrDefault(p => p.Id == id) != null);

            var playerDetailModel =
                new PlayerDetailModel(player, eventTeams.Select(et => new EventTeamModel(et, true)).ToList());

            return Ok(playerDetailModel);
        }
    }
}