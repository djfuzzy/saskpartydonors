using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Dtos {
  public class UpdateContributionDto {

        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public string Recipient { get; set; }

        public decimal Amount { get; set; }
  }
}