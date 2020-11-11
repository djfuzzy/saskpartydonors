namespace SaskPartyDonors.Entities
{
  public enum ContributorType
  {
    Individual,
    Corporation,
    TradeUnions,
    Unincorporated,
    Donations,
    NotApplicaple
  }

  public enum RecipientType
  {
    FederalCandidate,
    FederalParty,
    ProvincialCandidate,
    ProvincialParty,
    MunicipalCandidate
  }

  public enum ContributionSource
  {
    ElectionsSask,
    Airtable,
    NationalPost
  }
}