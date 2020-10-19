
using System.Collections.Generic;

namespace SaskPartyDonors.Entities
{
    public class Recipient : TableEntity
    {
        public string Name { get; set; }

        public RecipientType Type { get; set; }

        public string Region { get; set; }

        public IList<Contribution> Contributions { get; set; } = new List<Contribution>();
  }
}
