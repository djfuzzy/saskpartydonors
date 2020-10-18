using System;
using System.Globalization;
using System.IO;
using System.Linq;
using FileHelpers;
using SaskPartyDonors.Data;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Importers
{
  public class SaskCsvImporter
  {
    private readonly SaskPartyDonorsContext _context;

    public SaskCsvImporter(SaskPartyDonorsContext context)
    {
      _context = context;
    }

    public async void ImportFromStream(Stream stream, int year)
    {
      var engine = new FileHelperEngine<SaskCsvImportedContribution>();

      var importedContributions = engine.ReadStream(new StreamReader(stream));

      foreach (var importedContribution in importedContributions)
      {
        ImportContribution(importedContribution, year);
      }

      await _context.SaveChangesAsync();
    }

    private void ImportContribution(SaskCsvImportedContribution importedContribution, int year)
    {
      try
      {
        var formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
          importedContribution.ContributorName.Replace(",", ", ").ToLower());

        if (IsContributionAlreadyImported(formattedName, importedContribution.Recipient, year))
        {
          Console.WriteLine($"Already imported contribution from {importedContribution.ContributorName} to " +
            $"{importedContribution.Recipient} in {year}.");
          return;
        }

        _context.Contributions.Add(new Contribution
        {
          Id = Guid.NewGuid(),
          ContributorName = formattedName,
          ContributorType = importedContribution.ContributorType,
          Year = year,
          Recipient = importedContribution.Recipient,
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

    private bool IsContributionAlreadyImported(string contributorName, string recipient, int year)
    {
      return _context.Contributions.Any(c =>
        c.ContributorName == contributorName
        && c.Recipient == recipient
        && c.Year == year);
    }
  }
}