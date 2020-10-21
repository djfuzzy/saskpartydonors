using System.Collections.Generic;

namespace SaskPartyDonors.Services.Importers.Dtos
{
    public class ImportResultDto
    {
        public long ImportedCount { get; set; } = 0;

        public long SkippedCount { get; set; } = 0;

        public long FailedCount { get; set; } = 0;

        public IList<string> SkippedRecords { get; set; } = new List<string>();

        public IList<string> FailedRecords { get; set; } = new List<string>();
  }
}