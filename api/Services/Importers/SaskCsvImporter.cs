using System;
using System.Globalization;
using System.IO;
using FileHelpers;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Recipients;
using System.Threading.Tasks;

namespace SaskPartyDonors.Services.Importers
{
  public class SaskCsvImporter
  {
    private IContributionService _contributionService;

    private IRecipientService _recipientService;

    private const RecipientType DefaultRecipientType = RecipientType.ProvincialParty;

    private const string DefaultRegion = "SK";

    public SaskCsvImporter(IContributionService contributionService, IRecipientService recipientService)
    {
      _contributionService = contributionService;
      _recipientService = recipientService;
    }

    public async Task ImportFromStream(Stream stream, string recipientName, int year)
    {
      var engine = new FileHelperEngine<SaskCsvImportedContribution>();

      var importedContributions = engine.ReadStream(new StreamReader(stream));
      var recipient = await _recipientService.FindOrCreate(recipientName, DefaultRecipientType,
        DefaultRegion);

      foreach (var importedContribution in importedContributions)
      {
        await ImportContribution(importedContribution, recipient.Id, year);
      }
    }

    private async Task ImportContribution(SaskCsvImportedContribution importedContribution, Guid recipientId, int year)
    {
      try
      {
        var formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
          importedContribution.ContributorName.Replace(",", ", ").ToLower());

        if (await _contributionService.ExistsAsync(formattedName, recipientId, year))
        {
          Console.WriteLine($"Already imported contribution from {importedContribution.ContributorName} to " +
            $"{importedContribution.Recipient} in {year}.");
          return;
        }

        await _contributionService.Create(new CreateContributionDto
        {
          ContributorName = formattedName,
          ContributorType = importedContribution.ContributorType,
          Year = year,
          RecipientId = recipientId,
          Amount = importedContribution.Amount
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Unable to import contribution from {importedContribution.ContributorName} to " +
          $"{importedContribution.Recipient} in {year}:");
        Console.WriteLine(ex.Message);
      }
    }
  }
}