using FileHelpers;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Importers
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class NpCsvImportedContribution : IImportedContribution
    {
        [FieldConverter(typeof(YearConverter))]
        public int Year { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string ElectoralEvent { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string Source { get; set; }

        [FieldConverter(typeof(ContributorTypeConverter))]
        public ContributorType ContributorType { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(ContributorNameConverter))]
        public string ContributorName { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string ContributorCity { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string Recipient { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string PoliticalParty { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string PoliticalEntity { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        [FieldConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }
    }
}