using Microsoft.EntityFrameworkCore;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Data
{
    public interface ISaskPartyDonorsContext
    {
      DbSet<Contribution> Contributions { get; set; }

      DbSet<Recipient> Recipients { get; set; }
    }
}