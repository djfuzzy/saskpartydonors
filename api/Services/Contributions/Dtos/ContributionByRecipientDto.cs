using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Contributions.Dtos
{
  public class ContributionByRecipientDto
  {
        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public decimal Amount { get; set; }

        public string Location { get; set; }
  }
}