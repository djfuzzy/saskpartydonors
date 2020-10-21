using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileHelpers;
using SaskPartyDonors.Data;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Services.Importers.Dtos;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors.Services.Importers
{
  public class AirtableCsvImporter
  {
    private readonly SaskPartyDonorsContext _context;

    private IContributionService _contributionService;

    private IRecipientService _recipientService;

    private const ContributorType DefaultContributorType = ContributorType.Corporation;

    private const RecipientType DefaultRecipientType = RecipientType.ProvincialParty;

    private const string DefaultRegion = "SK";

    private const string DefaultRecipientName = "Saskatchewan Party";

    public AirtableCsvImporter(SaskPartyDonorsContext context, IContributionService contributionService,
      IRecipientService recipientService)
    {
      _context = context;
      _contributionService = contributionService;
      _recipientService = recipientService;
    }

    public async Task<ImportResultDto> ImportFromStream(Stream stream, Guid recipientId)
    {
      var engine = new FileHelperEngine<AirtableCsvImportedContribution>();
      var importedContributions = engine.ReadStream(new StreamReader(stream));
      // var recipient = await _recipientService.FindOrCreate(_context, DefaultRecipientName, DefaultRecipientType,
      //   DefaultRegion);
      var existingContriubitons = await _contributionService.List();

      var result = new ImportResultDto();

      foreach (var importedContribution in importedContributions)
      {
        var formattedName = importedContribution.ContributorName.Replace(",", ", ");

        if (existingContriubitons.Any(c =>
          c.ContributorName == formattedName
          && c.RecipientId == recipientId
          && c.Year == importedContribution.Year))
        {
          Console.WriteLine($"Already imported contribution from {importedContribution.ContributorName} to " +
            $"{importedContribution.ContributorName} in {importedContribution.Year}.");
          result.SkippedLines.Add(importedContribution);
          continue;
        }

        importedContribution.ContributorName = formattedName;
        var contribution = await ImportContribution(importedContribution, recipientId);

        if (contribution != null)
        {
          result.ImportedCount++;
          existingContriubitons.Append(contribution);
        }
        else
        {
          result.FailedCount++;
          result.FailedLines.Add(importedContribution);
        }
      }

      return result;
    }

    private async Task<ContributionDto> ImportContribution(AirtableCsvImportedContribution importedContribution,
      Guid recipientId)
    {
      try
      {
        return await _contributionService.Create(new CreateContributionDto
        {
          ContributorName = importedContribution.ContributorName,
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
        return null;
      }
    }
  }
}