using System.Globalization;

namespace CXChange.Modules.Kyc.Domain.User.ValueObjects
{
    public record BirthDay
    {
        public BirthDay(int day, int month, int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }
        private int Day { get; init; }
        private int Month { get; init; }
        private int Year { get; init; }
        public DateTime GetGregorianBirthday()
        {
            PersianCalendar cla = new PersianCalendar();

            return cla.ToDateTime(Year, Month, Day, 0, 0, 0, 0).Date;

        }
        public override string ToString()
        {
            return $"{Year}/{Month}/{Day}";
        }
        public DateTime GetPersianBirthDay()
        {
            return new DateTime(Year, Month, Day, 0, 0, 0, new PersianCalendar());
        }
        public BirthDaySnapshot GetSnapShot()
        {
            return new BirthDaySnapshot { Day = Day, Month = Month, Year = Year };
        }


    }

}
