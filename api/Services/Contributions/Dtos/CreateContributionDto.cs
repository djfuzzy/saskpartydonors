using System;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Contributions.Dtos
{
  public class CreateContributionDto
  {

        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public Guid RecipientId { get; set; }

      public decimal Amount { get; set; }

      public string Location { get; set; }

      public ContributionSource Source { get; set; }
    }
}