using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaskPartyDonors.Entities
{
    public class Contribution : TableEntity
    {
        public string ContributorName { get; set; }

        public ContributorType ContributorType { get; set; }

        public int Year { get; set; }

        public Guid RecipientId { get; set; }
        public Recipient Recipient { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
  }
}
