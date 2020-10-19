
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Recipients
{
    public class CreateRecipientDto
    {
        public string Name { get; set; }

        public RecipientType Type { get; set; }

        public string Region { get; set; }
  }
}
