namespace SaskPartyDonors.Services.Importers
{
  public static class RecipientNameMapper
  {
    public static string SaskatchewanParty = "Saskatchewan Party";

    public static string Import(string importedRecipientName)
    {
      switch (importedRecipientName)
      {
          case "Saskatchewan Party":
            return SaskatchewanParty;
          default:
            return null;
      }
    }
  }
}
