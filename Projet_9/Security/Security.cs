using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NSecurity
{
    public class Security
    {
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

                // Comparer le haché à vérifier avec le haché stocké
                return hacheAVerifierStr == motDePasseStocke;
            }
        }
    }
}
