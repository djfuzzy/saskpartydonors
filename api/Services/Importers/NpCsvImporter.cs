using System;
using Microsoft.Extensions.Logging;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Importers
{
  public class NpCsvImporter : BaseImporter<NpCsvImportedContribution>
    {
        private const RecipientType DefaultRecipientType = RecipientType.ProvincialParty;

        private const string DefaultRegion = "SK";

        public NpCsvImporter(ILogger<NpCsvImporter> logger,
            IContributionService contributionService,
            IRecipientService recipientService)
            : base(logger, contributionService, recipientService)
        {
        }

        protected override bool MatchesRecipient(NpCsvImportedContribution importedContribution)
        {
            return importedContribution.Recipient  == "Saskatchewan Party";
        }

        protected override CreateContributionDto MapTo(NpCsvImportedContribution importedContribution, Guid recipientId)
        {
            return new CreateContributionDto
            {
                ContributorName = importedContribution.ContributorName,
                ContributorType = importedContribution.ContributorType,
                Year = importedContribution.Year,
                RecipientId = recipientId,
                Amount = importedContribution.Amount,
                Location = DefaultRegion,
                Source = ContributionSource.NationalPost
            };
        }
    }
}