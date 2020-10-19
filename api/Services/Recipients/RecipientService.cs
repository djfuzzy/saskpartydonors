using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaskPartyDonors.Data;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Recipients
{
  public class RecipientService : IRecipientService
  {
    private readonly SaskPartyDonorsContext _context;
    private readonly IMapper _mapper;

    public RecipientService(SaskPartyDonorsContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<RecipientDto> Create(CreateRecipientDto input)
    {
      // TODO: Look for duplicates
      var recipient = _mapper.Map<Recipient>(input);
      recipient.Id = Guid.NewGuid();
      _context.Add(recipient);

      await _context.SaveChangesAsync();

      return _mapper.Map<RecipientDto>(recipient);
    }

    public Task Delete(Guid id)
    {
      throw new NotImplementedException();
    }

    public async Task<RecipientDto> GetById(Guid id)
    {
      return _mapper.Map<RecipientDto>(await _context.Recipients.Where(r => r.Id == id).SingleOrDefaultAsync());
    }

    public async Task<RecipientDto> FindOrCreate(string name, RecipientType type, string region)
    {
      var recipient = await _context.Recipients.Where(
        r => r.Name == name && r.Type == type && r.Region == region).SingleOrDefaultAsync();

      if (recipient != null)
      {
        return _mapper.Map<RecipientDto>(recipient);
      }

      return await Create(new CreateRecipientDto
        {
          Name = name,
          Type = type,
          Region = region
        }
      );
    }

    public async Task<IEnumerable<RecipientDto>> List()
    {
      return _mapper.Map<List<RecipientDto>>(await _context.Recipients.ToListAsync());
    }

    public async Task<RecipientDto> Update(UpdateRecipientDto input)
    {
      var recipient = await _context.Recipients.Where(r => r.Id == input.Id).SingleAsync();

      // TODO: Look for duplicates
      recipient.Name = input.Name;
      recipient.Type = input.Type;
      recipient.Region = input.Region;

      _context.Update(recipient);

      await _context.SaveChangesAsync();

      return _mapper.Map<RecipientDto>(recipient);
    }
  }
}