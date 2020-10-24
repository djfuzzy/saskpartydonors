using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaskPartyDonors.Data;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions.Dtos;

namespace SaskPartyDonors.Services.Contributions
{
  public class ContributionService : IContributionService
  {
    private readonly SaskPartyDonorsContext _context;
    private readonly IMapper _mapper;

    public ContributionService(SaskPartyDonorsContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ContributionDto> Create(CreateContributionDto input)
    {
      // TODO: Look for duplicates
      var contribution = _mapper.Map<Contribution>(input);
      contribution.Id = Guid.NewGuid();
      _context.Contributions.Add(contribution);

      await _context.SaveChangesAsync();

      return _mapper.Map<ContributionDto>(contribution);;
    }

    public async Task Delete(Guid id)
    {
        var contribution = await _context.Contributions.FindAsync(id);
        if (contribution == null)
        {
            throw new InvalidOperationException($"Contribution with Id '{id.ToString()}' does not exist.");
        }

        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string contributorName, Guid recipientId, int year)
    {
      return await _context.Contributions.AnyAsync(c =>
        c.ContributorName == contributorName
        && c.RecipientId == recipientId
        && c.Year == year);
    }

    public async Task<ContributionDto> GetById(Guid id)
    {
      return _mapper.Map<ContributionDto>(await _context.Contributions.Where(r => r.Id == id).SingleOrDefaultAsync());
    }

    public async Task<IEnumerable<ContributionDto>> List()
    {
      return _mapper.Map<List<ContributionDto>>(await _context.Contributions.Include(c => c.Recipient).ToListAsync());
    }

    public async Task<ContributionDto> Update(UpdateContributionDto input)
    {
      var contribution = await _context.Contributions.Where(r => r.Id == input.Id).SingleAsync();

      // TODO: Look for duplicates
      contribution.ContributorName = input.ContributorName;
      contribution.ContributorType = input.ContributorType;
      contribution.RecipientId = input.RecipientId;
      contribution.Year = input.Year;

      _context.Update(contribution);

      await _context.SaveChangesAsync();

      return _mapper.Map<ContributionDto>(contribution);
    }
  }
}