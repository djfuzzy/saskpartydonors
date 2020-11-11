using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaskPartyDonors.Services.Recipients
{
  public interface IRecipientService
  {
    Task<IEnumerable<RecipientDto>> List();

    Task<RecipientDto> GetById(Guid id);

    // Task<RecipientDto> FindOrCreate(SaskPartyDonorsContext context, string name, RecipientType type, string region);

    Task<RecipientDto> Create(CreateRecipientDto entity);

    Task<RecipientDto> Update(UpdateRecipientDto entity);
  }
}