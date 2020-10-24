using System;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Contributions.Dtos
{
  public class ContributionDto
  {
        public Guid Id { get; set; }

        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public string RecipientName { get; set; }

        public Guid RecipientId { get; set; }

        public decimal Amount { get; set; }

        public string Location { get; set; }
  }
}