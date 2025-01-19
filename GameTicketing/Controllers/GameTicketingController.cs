using GameTicketing.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace GameTicketing.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class GameTicketingController : ControllerBase
    {
        private static readonly string[] Departments = new[]
        {
            "Player", "Developer", "Administrator", "Moderator", "StoryWriter"
        };
        
        private readonly ILogger<GameTicketingController> _logger;

        public GameTicketingController(ILogger<GameTicketingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetGameTickets")]
        public IEnumerable<Ticket> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Ticket
            {
                Id=index,
                Title = $"Ticket {index}",
                Department = Departments[Random.Shared.Next(Departments.Length)],
                Description = "Ticket description",
                Status = (TicketStatus)Random.Shared.Next(0, 4),
                CreatedDate = DateTime.Now.AddDays(index),
                UpdatedDate = DateTime.Now,
            })
                .ToArray();
        }
    }
}