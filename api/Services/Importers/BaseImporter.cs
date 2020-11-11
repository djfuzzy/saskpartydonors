using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileHelpers;
using Microsoft.Extensions.Logging;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Services.Importers.Dtos;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Importers
{
    public abstract class BaseImporter<TImportModel> where TImportModel : class, IImportedContribution
    {
        public ILogger<BaseImporter<TImportModel>> Logger { get; set; }

        public IContributionService ContributionService { get; set; }

        public IRecipientService RecipientService { get; set; }

        public BaseImporter(ILogger<BaseImporter<TImportModel>> logger,
            IContributionService contributionService,
            IRecipientService recipientService)
        {
            Logger = logger;
            ContributionService = contributionService;
            RecipientService = recipientService;
        }

        public async Task<ImportResultDto> ImportFromStream(Stream stream, Guid recipientId)
        {
            PreImportValidation();

            var engine = new FileHelperEngine<TImportModel>();
            var readContributions = engine.ReadStream(new StreamReader(stream));
            var existingContriubutions = (await ContributionService.List()).ToList();
            var recipient = await RecipientService.GetById(recipientId);

            var result = new ImportResultDto
            {
                ReadCount = engine.TotalRecords
            };

            foreach (var importedContribution in readContributions)
            {
                if (!MatchesRecipient(importedContribution))
                {
                    continue;
                }

                var newContribution = MapTo(importedContribution, recipientId);

                if (ContributionAlreadyExists(existingContriubutions, newContribution))
                {
                    result.DuplicateCount++;
                    continue;
                }

                var contribution = await SaveContributionAsync(newContribution);

                if (contribution != null)
                {
                    result.ImportedCount++;
                    existingContriubutions.Append(contribution);
                }
                else
                {
                    result.FailedCount++;
                    result.FailedRecords.Add(JsonSerializer.Serialize(importedContribution));
                }
            }

            return result;
        }

        protected virtual void PreImportValidation()
        {}

        protected abstract bool MatchesRecipient(TImportModel importedContribution);

        protected abstract CreateContributionDto MapTo(TImportModel importedContribution, Guid recipientId);

        protected bool ContributionAlreadyExists(
                List<ContributionDto> existingContributions,
                CreateContributionDto contributionToCheck)
        {
            var normalizedName = NormalizeName(contributionToCheck.ContributorName, contributionToCheck.ContributorType);
            return existingContributions.Any(c =>
                c.RecipientId == contributionToCheck.RecipientId
                && c.Year == contributionToCheck.Year
                && c.Location == contributionToCheck.Location
                && NormalizeName(c.ContributorName, c.ContributorType) == normalizedName
            );
        }

        private static string NormalizeName(string input, ContributorType type)
        {
            var result = input.ToUpperInvariant().Trim().Replace(" ", "");

            if (type == ContributorType.Individual)
            {
                result = string.Join("", result.Split(',').Reverse());
            }

            result = Regex.Replace(result, "[^a-zA-Z0-9]", "");

            return result;
        }

        private async Task<ContributionDto> SaveContributionAsync(CreateContributionDto input)
        {
            try
            {
                return await ContributionService.Create(input);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Unable to import contribution from {input.ContributorName} " +
                    $"to recipient with Id {input.RecipientId} in {input.Year}:");
                Logger.LogError(ex.Message);
                return null;
            }
        }
    }
}