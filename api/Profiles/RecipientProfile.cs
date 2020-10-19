using AutoMapper;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Profiles
{
  public class RecipientProfile : Profile
  {
    public RecipientProfile()
    {
      CreateMap<Recipient, RecipientDto>();
      CreateMap<CreateRecipientDto, Recipient>();
      CreateMap<UpdateRecipientDto, Recipient>();
    }
  }
}