using System.Collections.Generic;

namespace SaskPartyDonors.Services.Importers.Dtos
{
    public class ImportResultDto
    {
        public long ReadCount { get; set; } = 0;

        public long ImportedCount { get; set; } = 0;

        public long DuplicateCount { get; set; } = 0;

        public long FailedCount { get; set; } = 0;

        public IList<string> FailedRecords { get; set; } = new List<string>();
  }
}