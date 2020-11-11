using FileHelpers;

namespace SaskPartyDonors.Services.Importers
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class AirtableCsvImportedContribution : IImportedContribution
    {
        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(ContributorNameConverter))]
        public string ContributorName { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }

        public int Year { get; set; }

        public string Location { get; set; }
    }
}
