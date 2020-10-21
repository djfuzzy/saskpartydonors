using FileHelpers;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Importers
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class SaskCsvImportedContribution : IImportedContribution
    {
        public string Recipient;

        [FieldConverter(typeof(ContributorTypeConverter))]
        public ContributorType ContributorType;

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(AmountConverter))]
        public decimal Amount;

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string ContributorName;
    }
}
