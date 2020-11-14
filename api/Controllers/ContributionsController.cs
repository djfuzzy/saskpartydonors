using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;

namespace SaskPartyDonors.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class ContributionsController : ControllerBase
    {
        private readonly IContributionService _contributionService;

        public ContributionsController(IContributionService contributionService)
        {
            _contributionService = contributionService;
        }

        // GET: api/Contributions/Recipients/
        [HttpGet("Recipients/{recipientId}")]
        public async Task<ActionResult<IEnumerable<ContributionByRecipientDto>>> GetContributionsByRecipientId(
            Guid recipientId)
        {
            return Ok(await _contributionService.GetByRecipientId(recipientId));
        }

        // GET: api/Contributions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributionDto>> GetContribution(Guid id)
        {
            var contribution = await _contributionService.GetById(id);

            if (contribution == null)
            {
                return NotFound();
            }

            return Ok(contribution);
        }

#if DEBUG

        // GET: api/Contributions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetContributions()
        {
            return Ok(await _contributionService.List());
        }

        // POST: api/Contributions
        [HttpPost]
        public async Task<ActionResult<ContributionDto>> PostContribution(CreateContributionDto input)
        {
            var contribution = await _contributionService.Create(input);

            return CreatedAtAction(nameof(GetContribution), new { id = contribution.Id }, contribution);
        }

        // PUT: api/Contributions/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Contribution>> PutContribution(UpdateContributionDto input)
        {
            var contribution = await _contributionService.Update(input);

            return Ok(contribution);
        }

        // DELETE: api/Contributions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contribution>> DeleteContribution(Guid id)
        {
            await _contributionService.Delete(id);

            return NoContent();
        }
#endif
    }
}
