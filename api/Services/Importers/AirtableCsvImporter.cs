using System;
using System.IO;
using System.Threading.Tasks;
using FileHelpers;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Importers
{
  public class AirtableCsvImporter
  {
    private IContributionService _contributionService;

    private IRecipientService _recipientService;

    private const ContributorType DefaultContributorType = ContributorType.Corporation;

    private const RecipientType DefaultRecipientType = RecipientType.ProvincialParty;

    private const string DefaultRegion = "SK";

    private const string DefaultRecipientName = "Saskatchewan Party";

    public AirtableCsvImporter(IContributionService contributionService, IRecipientService recipientService)
    {
      _contributionService = contributionService;
      _recipientService = recipientService;
    }

    public async void ImportFromStream(Stream stream)
    {
      var engine = new FileHelperEngine<AirtableCsvImportedContribution>();
      var importedContributions = engine.ReadStream(new StreamReader(stream));
      var recipient = await _recipientService.FindOrCreate(DefaultRecipientName, DefaultRecipientType,
        DefaultRegion);

      foreach (var importedContribution in importedContributions)
      {
        await ImportContribution(importedContribution, recipient.Id);
      }
    }

    private async Task ImportContribution(AirtableCsvImportedContribution importedContribution,
      Guid recipientId)
    {
      try
      {
        var formattedName = importedContribution.ContributorName.Replace(",", ", ");

        if (await _contributionService.ExistsAsync(formattedName, recipientId, importedContribution.Year))
        {
          Console.WriteLine($"Already imported contribution from {importedContribution.ContributorName} to " +
            $"{importedContribution.ContributorName} in {importedContribution.Year}.");
          return;
        }


        await _contributionService.Create(new CreateContributionDto
        {
          ContributorName = formattedName,
          ContributorType = DefaultContributorType,
          Year = importedContribution.Year,
          RecipientId = recipientId,
          Amount = importedContribution.Amount
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Unable to import contribution from {importedContribution.ContributorName} to " +
          $"{DefaultRecipientName} in {importedContribution.Year}:");
        Console.WriteLine(ex.Message);
      }
    }
  }
}