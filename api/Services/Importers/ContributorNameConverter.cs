using System.Globalization;
using FileHelpers;

namespace SaskPartyDonors.Services.Importers
{
  public class ContributorNameConverter : ConverterBase
  {
    public bool FixAllCaps { get; set; }

    public ContributorNameConverter() {}

    public ContributorNameConverter(bool fixAllCaps)
    {
      FixAllCaps = fixAllCaps;
    }

    public override object StringToField(string from)
    {
        if (FixAllCaps)
        {
          from = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
            from.ToLower());
        }

        return from.Trim()
            .Replace(",", ", ")
            .Replace("  ", " ");
    }
  }
}