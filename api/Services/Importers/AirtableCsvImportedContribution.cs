using FileHelpers;

namespace SaskPartyDonors.Services.Importers
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class AirtableCsvImportedContribution : IImportedContribution
    {
        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string ContributorName;

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(AmountConverter))]
        public decimal Amount;

        public int Year;

        public string Location;
    }
}
