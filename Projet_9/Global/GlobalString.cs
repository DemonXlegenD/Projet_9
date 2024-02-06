using System.Text.RegularExpressions;

namespace NGlobalString
{
    public class GlobalString
    {
        public static bool ContainsLowerCase(string input)
        {
            string pattern = @"[a-z]";
            return Regex.IsMatch(input, pattern);
        }

        // Fonction pour vérifier si la chaîne contient au moins une majuscule
        public static bool ContainsUpperCase(string input)
        {
            string pattern = @"[A-Z]";
            return Regex.IsMatch(input, pattern);
        }

        // Fonction pour vérifier si la chaîne contient au moins un chiffre
        public static bool ContainsDigit(string input)
        {
            string pattern = @"\d";
            return Regex.IsMatch(input, pattern);
        }

        // Fonction pour vérifier si la chaîne contient au moins un caractère spécial
        public static bool ContainsSpecialChar(string input)
        {
            string pattern = @"[^\da-zA-Z]";
            return Regex.IsMatch(input, pattern);
        }
    }
}
