
using System;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Recipients
{
    public class UpdateRecipientDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public RecipientType Type { get; set; }

        public string Region { get; set; }
  }
}
