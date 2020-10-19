using Microsoft.EntityFrameworkCore;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Data
{
    public class SaskPartyDonorsContext : DbContext, ISaskPartyDonorsContext
    {
        public SaskPartyDonorsContext(DbContextOptions<SaskPartyDonorsContext> options)
            : base(options)
        {
        }

        public DbSet<Contribution> Contributions { get; set; }

        public DbSet<Recipient> Recipients { get; set; }
    }
}