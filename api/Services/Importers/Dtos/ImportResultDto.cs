using System.Collections.Generic;

namespace SaskPartyDonors.Services.Importers.Dtos
{
    public class ImportResultDto
    {
        public long ImportedCount { get; set; } = 0;

        public long SkippedCount { get; set; } = 0;

        public long FailedCount { get; set; } = 0;

        public List<object> SkippedLines { get; set; } = new List<object>();

        public List<object> FailedLines { get; set; } = new List<object>();
  }
}