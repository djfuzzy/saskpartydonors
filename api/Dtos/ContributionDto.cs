using System;

namespace SaskPartyDonors.Dtos {
  public class ContributionDto {
        public Guid Id { get; set; }

        public string ContributorName { get; set; }

        public string ContributorType { get; set; }

        public int Year { get; set; }

        public string Recipient { get; set; }

        public decimal Amount { get; set; }
  }
}