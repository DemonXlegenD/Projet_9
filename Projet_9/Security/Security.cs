﻿using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Automation.Peers;
using NGlobalString;

namespace NSecurity
{
    public class Security
    {
        public static bool ValidationPseudo(string pseudo)
        {
            bool containsAtLeast3char = pseudo.Length >= 3;
            bool containsLowerCase = GlobalString.ContainsLowerCase(pseudo);
            bool containsUpperCase = GlobalString.ContainsUpperCase(pseudo);
            bool containsSpecialChar = GlobalString.ContainsSpecialChar(pseudo);

            if ((containsLowerCase || containsUpperCase) && !containsSpecialChar && containsAtLeast3char)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Pseudo valide");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Mot de passe invalide");
            }

            if (containsAtLeast3char)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins 3 caractères");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins 3 caractères");
            }

            if (containsLowerCase)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins une minuscule");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins une minuscule");
            }

            if (containsUpperCase)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins une majuscule");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins une majuscule");
            }

            if (!containsSpecialChar)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ne contient pas de caractères spéciaux");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ne doit pas contenir de caractères spéciaux");
            }
            Console.ForegroundColor = ConsoleColor.White;
            return containsLowerCase && containsUpperCase && !containsSpecialChar;
        }
        public static bool ValidationMotDePasse(string password)
        {
            bool containsAtLeast8char = password.Length >= 8;
            bool containsLowerCase = GlobalString.ContainsLowerCase(password);
            bool containsUpperCase = GlobalString.ContainsUpperCase(password);
            bool containsDigit = GlobalString.ContainsDigit(password);
            bool containsSpecialChar = GlobalString.ContainsSpecialChar(password);

            if (containsLowerCase && containsUpperCase && containsDigit && containsSpecialChar && containsAtLeast8char)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Mot de passe Valide");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Mot de passe invalide");
            }

            if (containsAtLeast8char)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins 8 caractères");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins 8 caractères");
            }

            if (containsLowerCase)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins une minuscule");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins une minuscule");
            }

            if (containsUpperCase)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins une majuscule");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins une majuscule");
            }

            if (containsDigit)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins un chiffre");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins un chiffre");
            }
            
            if (containsSpecialChar)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contient au moins un caractère spécial");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Doit contenir au moins un caractère spécial");
            }
            Console.ForegroundColor = ConsoleColor.White;
            return containsLowerCase && containsUpperCase && containsDigit && containsSpecialChar;
        }
        public static byte[] GenererSel()
        {
            // Utiliser un générateur de nombres aléatoires pour créer un sel
            byte[] sel = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(sel);
            }
            return sel;
        }

        public static string HacherMotDePasse(string motDePasse, byte[] sel)
        {
            using (var sha256 = SHA256.Create())
            {
                // Concaténer le mot de passe et le sel
                byte[] motDePasseSelConcatene = Encoding.UTF8.GetBytes(motDePasse + Convert.ToBase64String(sel));

                // Calculer le haché
                byte[] hache = sha256.ComputeHash(motDePasseSelConcatene);

                // Retourner le haché sous forme de chaîne hexadécimale
                return BitConverter.ToString(hache).Replace("-", "").ToLower();
            }
        }
        public static bool VerifierMotDePasse(string motDePasseAVerifier, string motDePasseStocke, string selStocke)
        {
            // Concaténer le mot de passe à vérifier avec le sel stocké
            byte[] motDePasseSelConcatene = Encoding.UTF8.GetBytes(motDePasseAVerifier + selStocke);

            // Calculer le haché à partir du mot de passe à vérifier et du sel stocké
            using (var sha256 = SHA256.Create())
            {
                byte[] hacheAVerifier = sha256.ComputeHash(motDePasseSelConcatene);

                // Convertir le haché à vérifier en une chaîne hexadécimale
                string hacheAVerifierStr = BitConverter.ToString(hacheAVerifier).Replace("-", "").ToLower();

                Console.WriteLine(hacheAVerifierStr);
                Console.WriteLine(motDePasseStocke);
                // Comparer le haché à vérifier avec le haché stocké
                return hacheAVerifierStr == motDePasseStocke;
            }
        }
    }
}
