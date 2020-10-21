using System;
using FileHelpers;
using SaskPartyDonors.Entities;

namespace SaskPartyDonors.Services.Importers
{
  public class ContributorTypeConverter : ConverterBase
  {
    public override object StringToField(string from)
    {
      switch (from)
      {
        case "Individual":
          return (ContributorType)ContributorType.Individual;
        case "Corporation":
          return (ContributorType)ContributorType.Corporation;
        case "Trade Unions":
          return (ContributorType)ContributorType.TradeUnions;
        case "Unincorporated Organizations or Associations":
          return (ContributorType)ContributorType.Unincorporated;
        default:
          throw new ArgumentOutOfRangeException($"Invalid contributor type: {from}");
      }
    }

    public override string FieldToString(object fieldValue)
    {
      throw new NotImplementedException();
    }
  }
}