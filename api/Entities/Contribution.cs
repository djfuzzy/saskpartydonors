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

    public string Location { get; set; }

    public ContributionSource Source { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
  }
}
