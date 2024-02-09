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
                    { 11, new List<string> { "DragonBreath" } },
                }
            },
            {
                "Maurad_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "GUN" } },
                    { 8, new List<string> { "PowerWhip" } },
                }
            },
            {
                "Francois_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Blop" } },
                    { 8, new List<string> { "FirePunch" } },
                }
            },
            {
                "Jean_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Rest" } },
                }
            },
            {
                "Marie_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "GUN" } },
                }
            },
            {
                "XPFARM_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Nothing" } },
                }
            },
            {
                "Sylvia_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Hadoken" } },
                    { 4, new List<string> { "PixelStorm" } },
                    { 5, new List<string> { "ThunderPunch" } },
                }
            },
            {
                "Lucas_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 4, new List<string> { "PsychoCut" } },
                    { 5, new List<string> { "MemeOverload" } },
                }
            },
            {
                "Aurora_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "AquaJet" } },
                    { 4, new List<string> { "Rest" } },
                    { 5, new List<string> { "IceShard" } },
                }
            },
            {
                "PikaPool_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Rest" } },
                    { 6, new List<string> { "Rest" } },
                }
            },
            {
                "Sonicachu_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "PixelStorm" } },
                    { 7, new List<string> { "MemeOverload" } },
                    { 9, new List<string> { "ThunderPunch" } }
                }
            },
            {
                "Memeosaur_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "PsychoCut" } },
                    { 5, new List<string> { "DragonBreath" } },
                    { 6, new List<string> { "DragonClaw" } },
                }
            },
            {
                "MystiWitch_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "ShadowClaw" } },
                    { 7, new List<string> { "ShadowBall" } },
                    { 9, new List<string> { "Avalanche" } },
                }
            },
            {
                "GigaGamer_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Hadoken" } },
                    { 6, new List<string> { "ThunderPunch" } },
                    { 9, new List<string> { "FlameWheel" } },
                }
            },
            {
                "PixelKnight_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "CriticalError" } },
                    { 2, new List<string> { "RealityBite" } },
                    { 3, new List<string> { "MeteorMash" } },
                    { 4, new List<string> { "MagnetBomb" } },
                }
            },
            {
                "DogeVine_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Capitalism" } },
                    { 5, new List<string> { "PowerWhip" } },
                }
            },
            {
                "TrollFlame_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 4, new List<string> { "BrickBreak" } },
                    { 5, new List<string> { "FirePunch" } }
                }
            },


            {
                "Minecraftian_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Earthquake" } },
                    { 6, new List<string> { "MemeOverload" } },
                    { 7, new List<string> { "Capitalism" } },
                }
            },
            {
                "FlappyFeather_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                }
            },
            {
                "GlitchByte_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                }
            },
            {
                "RoboSpark_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                }
            },
            {
                "PsychoPuzzle_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                }
            },
            {
                "FrostyDream_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                    { 6, new List<string> { "ThunderPunch" } },
                    { 9, new List<string> { "FlameWheel" } },
                }
            },
            {
                "PikaFairy_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                    { 9, new List<string> { "FlameWheel" } },
                }
            },
            {
                "SonicBolt_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Hadoken" } },
                    { 5, new List<string> { "Capitalism" } },
                    { 15, new List<string> { "FlameWheel" } },
                }
            },
            {
                "MemeDragon_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                }
            },
            {
                "SpiritJester_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                    { 20, new List<string> { "CriticalError" } },
                }
            },
            {
                "MetalMage_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "MeteorMash" } },
                    { 5, new List<string> { "PsychoCut" } },
                    { 14, new List<string> { "FlameWheel" } },
                }
            },
            {
                "PixelPaladin_LearnSet", new Dictionary<int, object>()
                {
                    { 1, new List<string> { "Charge" } },
                    { 5, new List<string> { "Capitalism" } },
                    { 20, new List<string> { "CriticalError" } },
                }
            },
        };
    }
}
