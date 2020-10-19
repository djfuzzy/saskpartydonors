using AutoMapper;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions.Dtos;

namespace SaskPartyDonors.Profiles
{
  public class ContributionProfile : Profile
  {
    public ContributionProfile()
    {
      CreateMap<Contribution, ContributionDto>();
      CreateMap<CreateContributionDto, Contribution>();
      CreateMap<UpdateContributionDto, Contribution>();
    }
  }
}