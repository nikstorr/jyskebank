namespace RomanConverter.Core
{
    public class ConvertToRoman
    {
        private readonly Dictionary<string, int> romanNumbersDictionary = new() 
        {
            {"I", 1}, {"IV", 4}, {"V", 5}, {"IX", 9}, {"X", 10}, {"XL", 40}, {"L", 50},
            {"XC", 90}, {"C", 100}, {"CD", 400}, {"D", 500}, {"CM", 900}, {"M", 1000}
        };

        public string Convert(int input) 
        {
            if (input <= 0)
                throw new ArgumentException("zero and negative numbers can not be converted to roman numerals");

            return ConvertArabicToRoman(input);
        }

        private string ConvertArabicToRoman(int input)
        {
            string romanResult = "";
            
            foreach (var item in romanNumbersDictionary.Reverse())
            {
                if (input <= 0) break;
                while (input >= item.Value)
                {
                    romanResult += item.Key;
                    input -= item.Value;
                }
            }
            return romanResult;
        }
    }
}
