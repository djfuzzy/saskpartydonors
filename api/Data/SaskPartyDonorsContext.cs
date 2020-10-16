using Microsoft.EntityFrameworkCore;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Data
{
    public class SaskPartyDonorsContext : DbContext
    {
        public SaskPartyDonorsContext(DbContextOptions<SaskPartyDonorsContext> options)
            : base(options)
        {
        }

        public DbSet<Contribution> Contributions { get; set; }
    }
}