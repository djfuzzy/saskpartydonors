using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SaskPartyDonors.Controllers
{
    [ApiController]
    [Route("api/contributions")]
    public class ContributorsController : ControllerBase
    {
        private readonly ILogger<ContributorsController> _logger;

        public ContributorsController(ILogger<ContributorsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Contributor Get()
        {
            return new Contributor
            {
                Id = Guid.NewGuid(),
                Name = "Test Smith",
                Type = "Individual",
                Recipient = "The Party",
                Year = 2019,
                Amount = 123.45M
            };
        }
    }
}
