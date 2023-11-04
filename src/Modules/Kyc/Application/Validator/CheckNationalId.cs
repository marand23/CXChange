using System.Text.RegularExpressions;

namespace CXChange.Modules.Kyc.Application.Validator
{
    internal class CheckNationalId
    {
        public static bool CheckNationalIdAuthenticity(string nationalId)
        {
            if (string.IsNullOrEmpty(nationalId))
            {
                return false;
            }
            var regex = new Regex(@"\b(\d)\1+\b");
            var isMatched = regex.IsMatch(nationalId);
            if (isMatched)
            {
                { return false; }
            }

            else
            {
                int sum = 0;
                int checkNumer = 0;
                var natNumberArray = nationalId.ToArray();
                for (int i = 0; i < nationalId.Length - 1; i++)
                {
                    sum += int.Parse(natNumberArray[i].ToString()) * (10 - i);
                }
                int rem = sum % 11;
                if (rem >= 2)
                {
                    checkNumer = 11 - rem;
                }
                else
                {
                    checkNumer = rem;
                }
                if (int.Parse(natNumberArray[9].ToString()) != checkNumer)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
