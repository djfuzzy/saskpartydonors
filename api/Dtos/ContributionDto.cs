using System;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Dtos {
  public class ContributionDto {
        public Guid Id { get; set; }

        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public string Recipient { get; set; }

        public decimal Amount { get; set; }
  }
}