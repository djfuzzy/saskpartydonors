using FileHelpers;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Importers
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class SaskCsvImportedContribution : IImportedContribution
    {
        public string Recipient { get; set; }

        [FieldConverter(typeof(ContributorTypeConverter))]
        public ContributorType ContributorType { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(ContributorNameConverter), true)]
        public string ContributorName { get; set; }
    }
}
