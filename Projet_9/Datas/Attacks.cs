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
            }


        };

    }
}
