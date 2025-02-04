using System;

namespace PatientManagement.Services.Common
{
    public class Hl7SearchDate
    {
        public enum Hl7SearchDatePrefix
        {
            Eq, Ne, Lt, Gt, Ge, Le, Sa, Eb, Ap
        }

        public Hl7SearchDatePrefix Prefix { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public Hl7SearchDate()
        {
        }

        public Hl7SearchDate(Hl7SearchDatePrefix prefix, DateTimeOffset date)
        {
            Prefix = prefix;
            Date = date;
        }

        public static bool TryCreateFromString(string value, out Hl7SearchDate searchDate)
        {
            searchDate = new Hl7SearchDate(Hl7SearchDatePrefix.Eb, DateTimeOffset.Now);
            if (string.IsNullOrEmpty(value) || value.Length < 12)
            {
                return false;
            }

            Hl7SearchDatePrefix prefix;
            DateTimeOffset date;
            if (!Enum.TryParse(value.Substring(0, 2), true, out prefix))
            {
                return false;
            }

            if (!DateTimeOffset.TryParse(value.Substring(2), out date))
            {
                return false;
            }

            searchDate = new Hl7SearchDate(prefix, date.ToUniversalTime());

            return true;
        }
    }
}
