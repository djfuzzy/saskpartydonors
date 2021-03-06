using System;
using Microsoft.Extensions.Logging;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Importers
{
  public class SaskCsvImporter : BaseImporter<SaskCsvImportedContribution>
    {
        public int Year { get; set; }

        private const RecipientType DefaultRecipientType = RecipientType.ProvincialParty;

        private const string DefaultRegion = "SK";


        public SaskCsvImporter(ILogger<BaseImporter<SaskCsvImportedContribution>> logger,
            IContributionService contributionService,
            IRecipientService recipientService)
          : base(logger, contributionService, recipientService)
        {
        }

        protected override void PreImportValidation()
        {
            if (Year == 0)
            {
                throw new InvalidOperationException($"{nameof(Year)} has not been set.");
            }
        }

        protected override bool MatchesRecipient(SaskCsvImportedContribution importedContribution)
        {
            return true;
        }

        protected override CreateContributionDto MapTo(SaskCsvImportedContribution importedContribution, Guid recipientId)
        {
            return new CreateContributionDto
            {
                ContributorName = importedContribution.ContributorName,
                ContributorType = importedContribution.ContributorType,
                Year = Year,
                RecipientId = recipientId,
                Amount = importedContribution.Amount,
                Location = DefaultRegion,
                Source = ContributionSource.ElectionsSask
            };
        }
    }
}