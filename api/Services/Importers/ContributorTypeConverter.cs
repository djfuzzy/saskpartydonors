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
        case "Individuals":
          return (ContributorType)ContributorType.Individual;
        case "Corporate":
        case "Corporation":
        case "Corporations":
          return (ContributorType)ContributorType.Corporation;
        case "Trade Unions":
        case "Unions":
          return (ContributorType)ContributorType.TradeUnions;
        case "Other":
        case "Unincorporated Organizations or Associations":
          return (ContributorType)ContributorType.Unincorporated;
        case "Donations in Kind":
          return (ContributorType)ContributorType.Donations;
        case "-":
        case "N/A":
          return (ContributorType)ContributorType.NotApplicaple;
        default:
          throw new ArgumentOutOfRangeException($"Invalid contributor type: {from}");
      }
    }
  }
}