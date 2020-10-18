using System;
using FileHelpers;

namespace SaskPartyDonors.Services.Importers
{
  public class AmountConverter : ConverterBase
  {
    public override object StringToField(string from)
    {
      return Convert.ToDecimal(Decimal.Parse(from.TrimStart('$')));
    }

    public override string FieldToString(object fieldValue)
    {
      throw new NotImplementedException();
    }
  }
}