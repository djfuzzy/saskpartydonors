using System;
using FileHelpers;

namespace SaskPartyDonors.Services.Importers
{
    public class YearConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return from == "N/A" ? -1 : Convert.ToInt32(int.Parse(from));
        }
    }
}