using System;

namespace SaskPartyDonors.Entities
{
    public class Contribution
    {
        public Guid Id { get; set; }

        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public string Recipient { get; set; }

        public decimal Amount { get; set; }
  }
}
