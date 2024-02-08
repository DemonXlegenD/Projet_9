using System.Collections.Generic;

namespace NDatas
{
	public class AttacksDic
	{
    	public static Dictionary<string, Dictionary<string, object>> attacks { get; set; } = new Dictionary<string, Dictionary<string, object>>()
    	{
            {
                "Bulle", new Dictionary<string, object>()
                {
                    { "Name", "Bulle" },
                    { "Type", "Water" },
                    { "Cat", "Physical" },
                    { "Power", 20 },
                    { "Acc", 100 },
                    { "Pp", 30 }
                }
            },
            {
                "Charge", new Dictionary<string, object>()
                {
                    { "Name", "Charge" },
                    { "Type", "Normal" },
                    { "Cat", "Physical" },
                    { "Power", 20 },
                    { "Acc", 100 },
                    { "Pp", 30 }
                }
            },
            {
                "Ez", new Dictionary<string, object>()
                {
                    { "Name", "Ez" },
                    { "Type", "Normal" },
                    { "Cat", "Physical" },
                    { "Power", 40 },
                    { "Acc", 100 },
                    { "Pp", 15 }
                }
            },
            {
                "Cout", new Dictionary<string, object>()
                {
                    { "Name", "Cout" },
                    { "Type", "Grass" },
                    { "Cat", "Special" },
                    { "Power", 35 },
                    { "Acc", 90 },
                    { "Pp", 20 }
                }
            },
            {
                "Blop", new Dictionary<string, object>()
                {
                    { "Name", "Blop" },
                    { "Type", "Fire" },
                    { "Cat", "Physical" },
                    { "Power", 25 },
                    { "Acc", 100 },
                    { "Pp", 25 }
                }
            },
            {
                "Gambling", new Dictionary<string, object>()
                {
                    { "Name", "Gambling" },
                    { "Type", "Normal" },
                    { "Cat", "Special" },
                    { "Power", 150 },
                    { "Acc", 50 },
                    { "Pp", 5 }
                }
            },
            {
                "GUN", new Dictionary<string, object>()
                {
                    { "Name", "GUN" },
                    { "Type", "Normal" },
                    { "Cat", "Special" },
                    { "Power", 200 },
                    { "Acc", 80 },
                    { "Pp", 1 }
                }
            },
            {
                "Rest", new Dictionary<string, object>()
                {
                    { "Name", "Rest" },
                    { "Type", "Flying" },
                    { "Cat", "Heal" },
                    { "Power", 50 },
                    { "Acc", 100 },
                    { "Pp", 10 }
                }
            },
            {
                "SelfEsteem", new Dictionary<string, object>()
                {
                    { "Name", "SelfEsteem" },
                    { "Type", "Normal" },
                    { "Cat", "Self" },
                    { "Power", 1 },
                    { "Acc", 100 },
                    { "Pp", 10 }
                }
            },
            {
                "Terror", new Dictionary<string, object>()
                {
                    { "Name", "Terror" },
                    { "Type", "Normal" },
                    { "Cat", "Other" },
                    { "Power", -1 },
                    { "Acc", 100 },
                    { "Pp", 10 }
                }
            },
            {
                "Nothing", new Dictionary<string, object>()
                {
                    { "Name", "Nothing" },
                    { "Type", "Normal" },
                    { "Cat", "Physical" },
                    { "Power", 1 },
                    { "Acc", 1 },
                    { "Pp", 999 }
                }
            },
            {
                "Capitalism", new Dictionary<string, object>()
                {
                    { "Name", "Capitalism" },
                    { "Type", "Normal" },
                    { "Cat", "Physical" },
                    { "Power", 70 },
                    { "Acc", 80 },
                    { "Pp", 10 }
                }
            },
            {
                "Hadoken", new Dictionary<string, object>
                {
                    { "Name", "Hadoken" },
                    { "Type", "Fire" },
                    { "Cat", "Special" },
                    { "Power", 90 },
                    { "Acc", 95 },
                    { "Pp", 15 }
                }
            },
            {
    "Confusion", new Dictionary<string, object>()
    {
        { "Name", "Confusion" },
        { "Type", "Psychic" },
        { "Cat", "Special" },
        { "Power", 50 },
        { "Acc", 90 },
        { "Pp", 20 }
    }
},
{
    "ViralSpread", new Dictionary<string, object>()
    {
        { "Name", "ViralSpread" },
        { "Type", "Poison" },
        { "Cat", "Special" },
        { "Power", 40 },
        { "Acc", 80 },
        { "Pp", 25 }
    }
},
{
    "CriticalError", new Dictionary<string, object>()
    {
        { "Name", "CriticalError" },
        { "Type", "Steel" },
        { "Cat", "Physical" },
        { "Power", 60 },
        { "Acc", 70 },
        { "Pp", 15 }
    }
},
{
    "PixelStorm", new Dictionary<string, object>()
    {
        { "Name", "PixelStorm" },
        { "Type", "Electric" },
        { "Cat", "Special" },
        { "Power", 75 },
        { "Acc", 85 },
        { "Pp", 20 }
    }
},
{
    "RealityBite", new Dictionary<string, object>()
    {
        { "Name", "RealityBite" },
        { "Type", "Dark" },
        { "Cat", "Physical" },
        { "Power", 55 },
        { "Acc", 75 },
        { "Pp", 18 }
    }
},

{
    "MemeOverload", new Dictionary<string, object>()
    {
        { "Name", "MemeOverload" },
        { "Type", "Normal" },
        { "Cat", "Special" },
        { "Power", 80 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "ThunderPunch", new Dictionary<string, object>()
    {
        { "Name", "ThunderPunch" },
        { "Type", "Electric" },
        { "Cat", "Physical" },
        { "Power", 75 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "FlameWheel", new Dictionary<string, object>()
    {
        { "Name", "FlameWheel" },
        { "Type", "Fire" },
        { "Cat", "Physical" },
        { "Power", 70 },
        { "Acc", 95 },
        { "Pp", 20 }
    }
},
{
    "AquaJet", new Dictionary<string, object>()
    {
        { "Name", "AquaJet" },
        { "Type", "Water" },
        { "Cat", "Physical" },
        { "Power", 60 },
        { "Acc", 100 },
        { "Pp", 20 }
    }
},
{
    "ShadowClaw", new Dictionary<string, object>()
    {
        { "Name", "ShadowClaw" },
        { "Type", "Ghost" },
        { "Cat", "Physical" },
        { "Power", 80 },
        { "Acc", 85 },
        { "Pp", 15 }
    }
},
{
    "PsychoCut", new Dictionary<string, object>()
    {
        { "Name", "PsychoCut" },
        { "Type", "Psychic" },
        { "Cat", "Physical" },
        { "Power", 70 },
        { "Acc", 90 },
        { "Pp", 18 }
    }
},
{
    "IceShard", new Dictionary<string, object>()
    {
        { "Name", "IceShard" },
        { "Type", "Ice" },
        { "Cat", "Physical" },
        { "Power", 55 },
        { "Acc", 95 },
        { "Pp", 20 }
    }
},
{
    "DragonClaw", new Dictionary<string, object>()
    {
        { "Name", "DragonClaw" },
        { "Type", "Dragon" },
        { "Cat", "Physical" },
        { "Power", 85 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "Earthquake", new Dictionary<string, object>()
    {
        { "Name", "Earthquake" },
        { "Type", "Ground" },
        { "Cat", "Physical" },
        { "Power", 100 },
        { "Acc", 80 },
        { "Pp", 10 }
    }
},
            {
    "MeteorMash", new Dictionary<string, object>()
    {
        { "Name", "MeteorMash" },
        { "Type", "Steel" },
        { "Cat", "Physical" },
        { "Power", 90 },
        { "Acc", 85 },
        { "Pp", 10 }
    }
},
{
    "LeafBlade", new Dictionary<string, object>()
    {
        { "Name", "LeafBlade" },
        { "Type", "Grass" },
        { "Cat", "Physical" },
        { "Power", 80 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "ShadowBall", new Dictionary<string, object>()
    {
        { "Name", "ShadowBall" },
        { "Type", "Ghost" },
        { "Cat", "Special" },
        { "Power", 75 },
        { "Acc", 95 },
        { "Pp", 15 }
    }
},
{
    "BrickBreak", new Dictionary<string, object>()
    {
        { "Name", "BrickBreak" },
        { "Type", "Fighting" },
        { "Cat", "Physical" },
        { "Power", 80 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "Thunderbolt", new Dictionary<string, object>()
    {
        { "Name", "Thunderbolt" },
        { "Type", "Electric" },
        { "Cat", "Special" },
        { "Power", 90 },
        { "Acc", 100 },
        { "Pp", 15 }
    }
},
{
    "Avalanche", new Dictionary<string, object>()
    {
        { "Name", "Avalanche" },
        { "Type", "Ice" },
        { "Cat", "Physical" },
        { "Power", 70 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "MagnetBomb", new Dictionary<string, object>()
    {
        { "Name", "MagnetBomb" },
        { "Type", "Steel" },
        { "Cat", "Physical" },
        { "Power", 75 },
        { "Acc", 95 },
        { "Pp", 15 }
    }
},
{
    "FirePunch", new Dictionary<string, object>()
    {
        { "Name", "FirePunch" },
        { "Type", "Fire" },
        { "Cat", "Physical" },
        { "Power", 85 },
        { "Acc", 90 },
        { "Pp", 10 }
    }
},
{
    "DragonBreath", new Dictionary<string, object>()
    {
        { "Name", "DragonBreath" },
        { "Type", "Dragon" },
        { "Cat", "Special" },
        { "Power", 80 },
        { "Acc", 90 },
        { "Pp", 15 }
    }
},
{
    "PowerWhip", new Dictionary<string, object>()
    {
        { "Name", "PowerWhip" },
        { "Type", "Grass" },
        { "Cat", "Physical" },
        { "Power", 95 },
        { "Acc", 85 },
        { "Pp", 10 }
    }
}



        };

    }
}
