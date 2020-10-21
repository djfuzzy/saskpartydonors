using System;
using System.Globalization;
using System.IO;
using FileHelpers;
using SaskPartyDonors.Services.Contributions.Dtos;
using SaskPartyDonors.Entities;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Recipients;
using System.Threading.Tasks;
using SaskPartyDonors.Data;
using System.Linq;
using SaskPartyDonors.Services.Importers.Dtos;

namespace SaskPartyDonors.Services.Importers
{
  public class SaskCsvImporter
  {
    private readonly SaskPartyDonorsContext _context;

    private IContributionService _contributionService;

    private IRecipientService _recipientService;

    private const RecipientType DefaultRecipientType = RecipientType.ProvincialParty;

    private const string DefaultRegion = "SK";

    public SaskCsvImporter(SaskPartyDonorsContext context, IContributionService contributionService,
      IRecipientService recipientService)
    {
      _context = context;
      _contributionService = contributionService;
      _recipientService = recipientService;
    }

    public async Task<ImportResultDto> ImportFromStream(Stream stream, Guid recipientId, int year)
    {
      var engine = new FileHelperEngine<SaskCsvImportedContribution>();

      var importedContributions = engine.ReadStream(new StreamReader(stream));
      // var recipient = await _recipientService.FindOrCreate(_context, recipientName, DefaultRecipientType,
      //   DefaultRegion);
      var existingContriubitons = await _contributionService.List();

      var result = new ImportResultDto();

      foreach (var importedContribution in importedContributions)
      {
        var formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
          importedContribution.ContributorName.Replace(",", ", ").ToLower());

        if (existingContriubitons.Any(c =>
          c.ContributorName == formattedName
          && c.RecipientId == recipientId
          && c.Year == year))
        {
          Console.WriteLine($"Already imported contribution from {importedContribution.ContributorName} to " +
            $"{importedContribution.ContributorName} in {year}.");
            result.SkippedLines.Add(new { importedContribution });
          continue;
        }

        importedContribution.ContributorName = formattedName;
        var contribution = await ImportContribution(importedContribution, recipientId, year);

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

    private async Task<ContributionDto> ImportContribution(SaskCsvImportedContribution importedContribution,
      Guid recipientId, int year)
    {
      try
      {
        return await _contributionService.Create(new CreateContributionDto
        {
          ContributorName = importedContribution.ContributorName,
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
        return null;
      }
    }
  }
}