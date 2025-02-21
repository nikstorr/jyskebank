namespace RomanConverter.Core
{
    public class ConvertFromRoman
    {
        private readonly Dictionary<char, int> romanNumeralsDict = new()
        {
            { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 }
        };

        public int Convert(string input)
        {
            ValidateInputString(input);

            if (!HasSingleNumeralBeforeLargerNumerals(input))
                throw new ArgumentException("input contains several smaller numerals before larger ones");

            if (!Only_I_X_C_AppearBeforeBiggerNumerals(input))
                throw new ArgumentException("only I appears before V or X");

            if (!V_L_D_MustNotRepeat(input))
                throw new ArgumentException("V, L and D must not repeat");

            if (!ContainsOnlyThreeIdenticalConsecutiveNumerals(input))
                throw new ArgumentException("input contains more than three concurrent same numerals");

            return ConvertRomanToArabic(input);
        }

        private int ConvertRomanToArabic(string input)
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char currentRomanChar = input[i];
                romanNumeralsDict.TryGetValue(currentRomanChar, out int num);
                if (i + 1 < input.Length && romanNumeralsDict[input[i + 1]] > romanNumeralsDict[currentRomanChar])
                {
                    sum -= num;
                }
                else
                {
                    sum += num;
                }
            }
            return sum;
        }

        private void ValidateInputString(string input)
        {
            if (input.Length == 0)
                throw new ArgumentException("input is empty");

            foreach(var s in input.ToCharArray())
                if (!romanNumeralsDict.ContainsKey(s))
                    throw new ArgumentException($"input contains non-roman numeral: {s}");
        }

        private bool HasSingleNumeralBeforeLargerNumerals(string input)
        {
            int i = 0;
            while (input.Length >= 3 && input.Length - i >= 3)
            {
                int a = romanNumeralsDict[input[i]];
                int b = romanNumeralsDict[input[i + 1]];
                int c = romanNumeralsDict[input[i + 2]];
                
                i++;

                if (a == b)
                    if (b < c)
                        return false;
            }
            return true;
        }

        private bool Only_I_X_C_AppearBeforeBiggerNumerals(string input)
        {
            int i = 0;
            while (input.Length >= 2 && input.Length - i >= 2)
            {
                int a = romanNumeralsDict[input[i]];
                int b = romanNumeralsDict[input[i + 1]];

                i++;

                if (a == b)
                    continue;

                if(a == 1 || a == 10 || a == 100)
                    return Only_I_AppearsBefore_V_X(a, b) && Only_X_AppearsBefore_L_C(a, b) && Only_C_AppearsBefore_D_M(a, b);
            }
            return true;
        }
        private bool Only_I_AppearsBefore_V_X(int a, int b)
        {
            if (b == 5 || b == 10)
                if (a != 1)
                    return false;

            return true;
        }

        private bool Only_X_AppearsBefore_L_C(int a, int b)
        {
            if (b == 50 || b == 100)
                if(a != 10)
                    return false;

            return true;
        }

        private bool Only_C_AppearsBefore_D_M(int a, int b)
        {
            if (b == 500 || b == 1000)
                if (a != 100)
                    return false;

            return true;
        }

        private bool ContainsOnlyThreeIdenticalConsecutiveNumerals(string input)
        {
            if (input.Length < 4)
                return true;
            
            int i = 0;
            while (input.Length >= 4 && input.Length - i >= 4)
            {
                var a = romanNumeralsDict[input[i]];
                var b = romanNumeralsDict[input[i+1]];
                var c = romanNumeralsDict[input[i+2]];
                var d = romanNumeralsDict[input[i+3]];
                return !(a == b && a == c && a == d);
            }
            return true;
        }

        private bool V_L_D_MustNotRepeat(string input)
        {
            int i = 0;
            while (input.Length >= 2 && input.Length - i >= 2)
            {
                int a = romanNumeralsDict[input[i]];
                int b = romanNumeralsDict[input[i + 1]];

                i++;

                if ((a == b) && (a == 5 || a == 50 || a == 500))
                    return false; 
            }
            return true;
        } 
    }
}
