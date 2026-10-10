using System.Globalization;

namespace CXChange.Modules.Kyc.Application.Validator
{
    internal class CheckAge
    {
        public static bool IsAgeMoreThan18(int persianYear, int persianMonth, int PersianDay)
        {
            try
            {
                PersianCalendar calendar = new PersianCalendar();
                DateTime birthDate = calendar.ToDateTime(persianYear, persianMonth, PersianDay, 0, 0, 0, 0);
                return birthDate.AddYears(18) <= DateTime.Now;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return false;
            }


        }
        public static bool IsBirthDayValid(int persianYear, int persianMonth, int persianDay)
        {
            try
            {
                PersianCalendar calendar = new PersianCalendar();
                DateTime birthDate = calendar.ToDateTime(persianYear, persianMonth, persianDay, 0, 0, 0, 0);
                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return false;
            }

        }


    }
}
