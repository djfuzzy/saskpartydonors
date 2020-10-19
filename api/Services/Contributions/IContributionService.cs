using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaskPartyDonors.Services.Contributions.Dtos;

namespace SaskPartyDonors.Services.Contributions
{
  public interface IContributionService
  {
    Task<IEnumerable<ContributionDto>> List();

    Task<bool> ExistsAsync(string contributorName, Guid recipientId, int year);

    Task<ContributionDto> GetById(Guid id);

    Task<ContributionDto> Create(CreateContributionDto entity);

    Task<ContributionDto> Update(UpdateContributionDto entity);

    Task Delete(Guid id);
  }
}