using System;

namespace SaskPartyDonors
{
    public class Contributor
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Year { get; set; }

        public string Recipient { get; set; }

        public decimal Amount { get; set; }
  }
}
