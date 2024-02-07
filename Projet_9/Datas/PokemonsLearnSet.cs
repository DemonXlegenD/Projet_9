using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_9.Datas
{
    public static class PokemonsLearnSet
    {
        public static Dictionary<string, Dictionary<int, object>> LearnSets { get; set; } = new Dictionary<string, Dictionary<int, object>>()
        {
            {
                "Jarod_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Bulle" } },
                    { 7, new List<string> { "Rest" } },
                    { 8, new List<string> { "Ez" } },
                }
            },
            {
                "Maurad_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "GUN" } },
                }
            },
            {
                "Francois_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Blop" } },
                }
            },
        };
    }
}
