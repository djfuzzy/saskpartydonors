using System;
using System.IO;
using System.Linq;
using FileHelpers;
using SaskPartyDonors.Data;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Importers
{
  public class AirtableCsvImporter
  {
    private readonly SaskPartyDonorsContext _context;

    private static ContributorType _contributorType = ContributorType.Corporation;

    public AirtableCsvImporter(SaskPartyDonorsContext context)
    {
      _context = context;
    }

    public async void ImportFromStream(Stream stream, string recipient)
    {
      var engine = new FileHelperEngine<AirtableCsvImportedContribution>();
      var importedContributions = engine.ReadStream(new StreamReader(stream));

      foreach (var importedContribution in importedContributions)
      {
        ImportContribution(importedContribution, recipient);
      }

      await _context.SaveChangesAsync();
    }

    private void ImportContribution(AirtableCsvImportedContribution importedContribution,
      string recipientName)
    {
      try
      {
        var formattedName = importedContribution.ContributorName.Replace(",", ", ");

        if (IsContributionAlreadyImported(formattedName, importedContribution.ContributorName,
          importedContribution.Year))
        {
          Console.WriteLine($"Already imported contribution from {importedContribution.ContributorName} to " +
            $"{importedContribution.ContributorName} in {importedContribution.Year}.");
          return;
        }

        _context.Contributions.Add(new Contribution
        {
          Id = Guid.NewGuid(),
          ContributorName = formattedName,
          ContributorType = _contributorType,
          Year = importedContribution.Year,
          Recipient = recipientName,
          Amount = importedContribution.Amount
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Unable to import contribution from {importedContribution.ContributorName} to " +
          $"{recipientName} in {importedContribution.Year}:");
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