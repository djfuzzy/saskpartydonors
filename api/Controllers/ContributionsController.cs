using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaskPartyDonors.Data;
using SaskPartyDonors.Dtos;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionsController : ControllerBase
    {
        private readonly SaskPartyDonorsContext _context;

        public ContributionsController(SaskPartyDonorsContext context)
        {
            _context = context;
        }

        // GET: api/Contributions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetContributions()
        {
            return await _context.Contributions.Select(c =>
                new ContributionDto
                {
                    Id = c.Id,
                    ContributorName = c.ContributorName,
                    ContributorType = c.ContributorType,
                    Recipient = c.Recipient,
                    Year = c.Year,
                    Amount = c.Amount
                }).ToListAsync();
        }

        // GET: api/Contributions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributionDto>> GetContribution(Guid id)
        {
            var contribution = await _context.Contributions.FindAsync(id);

            if (contribution == null)
            {
                return NotFound();
            }

            return new ContributionDto
                {
                    Id = contribution.Id,
                    ContributorName = contribution.ContributorName,
                    ContributorType = contribution.ContributorType,
                    Recipient = contribution.Recipient,
                    Year = contribution.Year,
                    Amount = contribution.Amount
                };
        }

        // POST: api/Contributions
        [HttpPost]
        public async Task<ActionResult<ContributionDto>> PostContribution(CreateContributionDto input)
        {
            var contribution = new Contribution
            {
              Id = Guid.NewGuid(),
              ContributorName = input.ContributorName,
              ContributorType = input.ContributorType,
              Recipient = input.Recipient,
              Year = input.Year,
              Amount = input.Amount
            };

            _context.Contributions.Add(contribution);
            await _context.SaveChangesAsync();

            contribution = await _context.Contributions.FindAsync(contribution.Id);

            var result = new ContributionDto
            {
                Id = contribution.Id,
                ContributorName = contribution.ContributorName,
                ContributorType = contribution.ContributorType,
                Recipient = contribution.Recipient,
                Year = contribution.Year,
                Amount = contribution.Amount
            };

            return CreatedAtAction(nameof(GetContribution), new { id = contribution.Id }, contribution);
        }

        // PUT: api/Contributions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContribution(Guid id, UpdateContributionDto input)
        {
            var contribution = await _context.Contributions.FindAsync(id);

            if (contribution == null)
            {
                return NotFound();
            }

            contribution.ContributorName = input.ContributorName;
            contribution.ContributorType = input.ContributorType;
            contribution.Recipient = input.Recipient;
            contribution.Year = input.Year;
            contribution.Amount = input.Amount;

            _context.Entry(contribution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Contributions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contribution>> DeleteContribution(Guid id)
        {
            var contribution = await _context.Contributions.FindAsync(id);
            if (contribution == null)
            {
                return NotFound();
            }

            _context.Contributions.Remove(contribution);
            await _context.SaveChangesAsync();

            return contribution;
        }

        private bool ContributionExists(Guid id)
        {
            return _context.Contributions.Any(e => e.Id == id);
        }
    }
}
