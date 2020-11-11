using System;
using Microsoft.Extensions.Logging;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Importers
{
  public class AirtableCsvImporter : BaseImporter<AirtableCsvImportedContribution>
    {
        public AirtableCsvImporter(ILogger<AirtableCsvImporter> logger,
            IContributionService contributionService,
            IRecipientService recipientService)
        : base(logger, contributionService, recipientService)
        {
        }

        protected override bool MatchesRecipient(AirtableCsvImportedContribution importedContribution)
        {
            return true;
        }

        protected override CreateContributionDto MapTo(AirtableCsvImportedContribution importedContribution, Guid recipientId)
        {
            return new CreateContributionDto
            {
                ContributorName = importedContribution.ContributorName,
                ContributorType = ContributorType.Corporation,
                Year = importedContribution.Year,
                RecipientId = recipientId,
                Amount = importedContribution.Amount,
                Location = importedContribution.Location,
                Source = ContributionSource.Airtable
            };
        }
    }
}