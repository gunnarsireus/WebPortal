using System;
using System.Linq;

using ServerLibrary.Model;

// Extends the String class with Matches(), a case-insensitive Contains() method.
namespace ServerLibrary.Utils
{
    public static class StringExtension
    {
        /// <param name="pointsThreshold">Points required to pass the test</param>
        /// <param name="pwlThreshold">Password length required</param>
        /// <param name="bonusLimit">Bonus limit for upper and symbol characters</param>
        /// <param name="ppDeduction">Points per deduction for bad password workarounds</param>
        /// <param name="ppDistinct">Points per distinct character</param>
        /// <param name="ppSymbol">Points per symbol</param>
        /// <param name="ppUpper">Points per upper character</param>
        public static string IsSecurePassword(this string password, int pointsThreshold = 8, int pwlThreshold = 6, int bonusLimit = 3, int ppDeduction = 1, int ppDistinct = 1, int ppSymbol = 2, int ppUpper = 1)
        {
            try
            {
                if (password.Length < pwlThreshold)
                {
                    throw new ServerValidateException("Lösenordet måste vara minst " + pwlThreshold + " tecken");
                }

                int upperBonus = 0;
                int symbolBonus = 0;
                int points = 0;

                points += ppDistinct * password.Distinct().Count();

                foreach (char c in password)
                {
                    if (char.IsLetter(c)) // letter
                    {
                        if (char.IsUpper(c) && upperBonus < bonusLimit)
                        {
                            points += ppUpper;
                            upperBonus += ppUpper;
                        }
                    }
                    else if (char.IsNumber(c)) // integer
                    {
                    }
                    else if (symbolBonus < bonusLimit) // symbol
                    {
                        points += ppSymbol;
                        symbolBonus += ppSymbol;
                    }
                }

                if (char.IsNumber(password[0]) || char.IsUpper(password[0]))
                {
                    points -= ppDeduction;
                }
                if (password.EndsWith("!"))
                {
                    points -= ppDeduction;
                }
                if (password.EndsWith("."))
                {
                    points -= ppDeduction;
                }
                if (password.EndsWith("2016"))
                {
                    points -= ppDeduction;
                }

                if (points < pointsThreshold)
                {
                    throw new ServerValidateException("För låg komplexitet, testa att blanda med siffror och specialtecken, eller förläng ditt lösenord");
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static bool Matches(this string source, string pattern)
        {
            return source.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
