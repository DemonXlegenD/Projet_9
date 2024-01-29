namespace Csharp_Tpt
{
    public class PokemonsDic
    {
    	public static Dictionary<string, Dictionary<string, object>> pokemons { get; set; } = new Dictionary<string, Dictionary<string, object>>()
    	{
    	    {
    	        "Jarod", new Dictionary<string, object>()
    	        {
    	            { "Name", "Jarod" },
    	            { "Types", new List<string> { "Water" } },
    	            { "Hp", 40 },
    	            { "Att", 50 },
    	            { "AttSpe", 70 },
    	            { "Def", 50 },
    	            { "DefSpe", 50 },
    	            { "Speed", 70 }
    	        }
    	    },
    	    {
    	        "Maurad", new Dictionary<string, object>()
    	        {
    	            { "Name", "Maurad" },
    	            { "Types", new List<string> { "Grass" } },
    	            { "Hp", 35 },
    	            { "Att", 35 },
    	            { "AttSpe", 90 },
    	            { "Def", 30 },
    	            { "DefSpe", 30 },
    	            { "Speed", 100 }
    	        }
    	    },
    	    {
    	        "Francois", new Dictionary<string, object>()
    	        {
    	            { "Name", "Francois" },
    	            { "Types", new List<string> { "Fire" } },
    	            { "Hp", 50 },
    	            { "Att", 70 },
    	            { "AttSpe", 25 },
    	            { "Def", 55 },
    	            { "DefSpe", 55 },
    	            { "Speed", 60 }
    	        }
    	    },
    	    {
    	        "Jean", new Dictionary<string, object>()
    	        {
    	            { "Name", "Jean" },
    	            { "Types", new List<string> { "Water" } },
    	            { "Hp", 55 },
    	            { "Att", 60 },
    	            { "AttSpe", 30 },
    	            { "Def", 50 },
    	            { "DefSpe", 65 },
    	            { "Speed", 70 }
    	        }
    	    },
    	    {
    	        "Marie", new Dictionary<string, object>()
    	        {
    	            { "Name", "Marie" },
    	            { "Types", new List<string> { "Flying" } },
    	            { "Hp", 65 },
    	            { "Att", 75 },
    	            { "AttSpe", 35 },
    	            { "Def", 55 },
    	            { "DefSpe", 45 },
    	            { "Speed", 50 }
    	        }
    	    },
    	    {
    	        "XPFARM", new Dictionary<string, object>()
    	        {
    	            { "Name", "XPFARM" },
    	            { "Types", new List<string> { "Normal" } },
    	            { "Hp", 1 },
    	            { "Att", 1 },
    	            { "AttSpe", 1 },
    	            { "Def", 1 },
    	            { "DefSpe", 1 },
    	            { "Speed", 1 }
    	        }
    	    }
    	};
    }
}
/*
const Pokemon_Dic: Dictionary = {
	"Jarod":{"Name": "Jarod", "Types": ["Water"], "Hp": 40, "Att": 50, "AttSpe": 70, "Def": 50, "DefSpe": 50, "Speed": 70},
	"Maurad":{"Name": "Maurad", "Types": ["Grass"], "Hp": 35, "Att": 35, "AttSpe": 90, "Def": 30, "DefSpe": 30, "Speed": 100},
	"Francois":{"Name": "Francois", "Types": ["Fire"], "Hp": 50, "Att": 70, "AttSpe": 25, "Def": 55, "DefSpe": 55, "Speed": 60 },
	"Jean":{"Name": "Jean", "Types": ["Water"], "Hp": 55, "Att": 60, "AttSpe": 30, "Def": 50, "DefSpe": 65, "Speed": 70 },
	"Marie":{"Name": "Marie", "Types": ["Flying"], "Hp": 65, "Att": 75, "AttSpe": 35, "Def": 55, "DefSpe": 45, "Speed": 50 },
	"XPFARM":{"Name": "XPFARM", "Types": ["Normal"], "Hp": 1, "Att": 1, "AttSpe": 1, "Def": 1, "DefSpe": 1, "Speed": 1 },
	
	}

const PokemonLearnSet_Dic: Dictionary = {
	"jarod_learnset":{1:["Charge"],6:["Bulle"],15:["Ez"],25:["Rest"]},
	"maurad_learnset":{1:["Charge","GUN","Ez","Rest"],6:["Bulle"]},
	"francois_learnset":{1:["Charge"],6:["Bulle"]},
	"jean_learnset":{1:["Charge"],6:["Bulle"]},
	"marie_learnset":{1:["Charge"],6:["Bulle"]},
	"amaya_learnset":{1:["Charge"],6:["Bulle"]},
	"julien_learnset":{1:["Charge"],6:["Bulle"]},
	"camille_learnset":{1:["Charge"],6:["Bulle"]},
	"antoine_learnset":{1:["Charge"],6:["Bulle"]},
	"nathan_learnset":{1:["Charge"],6:["Bulle"]},
	"lucas_learnset":{1:["Charge"],6:["Bulle"]},
	"lea_learnset":{1:["Charge"],6:["Bulle"]},
	"xpfarm_learnset":{1:["Charge"],6:["Bulle"]},
}

const Pokemon_List:Array[Dictionary] = [jarod,maurad,francois,jean,marie,emilie,julien,sophie,antoine,camille,lucas,lea,nathan]
const PokemonLearnSet_List:Array[Dictionary] = [jarod_learnset,maurad_learnset,francois_learnset,jean_learnset,marie_learnset,emilie_learnset,julien_learnset,sophie_learnset,antoine_learnset,camille_learnset,lucas_learnset,lea_learnset,nathan_learnset]


const XPFARM: Dictionary ={"Name": "XPFARM", "Types": ["Normal"], "Hp": 1, "Att": 0, "AttSpe": 0, "Def": 0, "DefSpe": 0, "Speed": 1 }

const jarod: Dictionary = { "Name": "Jarod", "Types": ["Water"], "Hp": 40, "Att": 50, "AttSpe": 70, "Def": 50, "DefSpe": 50, "Speed": 70 }
const jarod_learnset: Dictionary = {"Name:":"Jarod",1:["Charge"],6:["Bulle"]}
const maurad: Dictionary = {"Name": "Maurad", "Types": ["Grass"], "Hp": 35, "Att": 35, "AttSpe": 90, "Def": 30, "DefSpe": 30, "Speed": 100 }
const maurad_learnset: Dictionary = {"Name:":"Maurad",1:["Charge"],6:["Bulle"]}
const francois: Dictionary = {"Name": "Francois", "Types": ["Fire"], "Hp": 50, "Att": 70, "AttSpe": 25, "Def": 55, "DefSpe": 55, "Speed": 60 }
const francois_learnset: Dictionary = {"Name:":"Francois",1:["Charge"],6:["Bulle"]}


const jean: Dictionary = {"Name": "Jean", "Types": ["Water"], "Hp": 55, "Att": 60, "AttSpe": 30, "Def": 50, "DefSpe": 65, "Speed": 70 }
const jean_learnset: Dictionary = {"Name":"Jean",1:["Charge"],6:["Bulle"]}
const marie: Dictionary = {"Name": "Marie", "Types": ["Flying"], "Hp": 65, "Att": 75, "AttSpe": 35, "Def": 55, "DefSpe": 45, "Speed": 50 }
const marie_learnset: Dictionary = {"Name":"Marie",1:["Charge"],6:["Bulle"]}
const emilie: Dictionary = {"Name": "Emilie", "Types": ["Grass"], "Hp": 50, "Att": 55, "AttSpe": 75, "Def": 40, "DefSpe": 60, "Speed": 80 }
const emilie_learnset: Dictionary = {"Name":"Emilie",1:["Charge"],6:["Bulle"]}

const julien: Dictionary = {"Name": "Julien", "Types": ["Electric"], "Hp": 60, "Att": 70, "AttSpe": 80, "Def": 45, "DefSpe": 55, "Speed": 65 }
const julien_learnset: Dictionary = {"Name":"Julien",1:["Charge"],6:["Bulle"]}
const sophie: Dictionary = {"Name": "Sophie", "Types": ["Psychic"], "Hp": 50, "Att": 45, "AttSpe": 70, "Def": 40, "DefSpe": 60, "Speed": 65 }
const sophie_learnset: Dictionary = {"Name":"Sophie",1:["Charge"],6:["Bulle"]}
const antoine: Dictionary = {"Name": "Antoine", "Types": ["Ground"], "Hp": 60, "Att": 75, "AttSpe": 20, "Def": 50, "DefSpe": 50, "Speed": 55 }
const antoine_learnset: Dictionary = {"Name":"Antoine",1:["Charge"],6:["Bulle"]}

const camille: Dictionary = {"Name": "Camille", "Types": ["Ice"], "Hp": 45, "Att": 55, "AttSpe": 65, "Def": 40, "DefSpe": 70, "Speed": 75 }
const camille_learnset: Dictionary = {"Name":"Camille",1:["Charge"],6:["Bulle"]}
const lucas: Dictionary = {"Name": "Lucas", "Types": ["Bug"], "Hp": 50, "Att": 65, "AttSpe": 30, "Def": 45, "DefSpe": 60, "Speed": 80 }
const lucas_learnset: Dictionary = {"Name":"Lucas",1:["Charge"],6:["Bulle"]}
const lea: Dictionary = {"Name": "Lea", "Types": ["Fairy"], "Hp": 55, "Att": 50, "AttSpe": 70, "Def": 35, "DefSpe": 65, "Speed": 60 }
const lea_learnset: Dictionary = {"Name":"Lea",1:["Charge"],6:["Bulle"]}

const nathan: Dictionary = {"Name": "Nathan", "Types": ["Steel"], "Hp": 70, "Att": 80, "AttSpe": 40, "Def": 60, "DefSpe": 50, "Speed": 45 }
const nathan_learnset: Dictionary = {"Name":"Nathan",1:["Charge"],6:["Bulle"]}

const amaya: Dictionary = {"Name": "Amaya", "Types": ["Fighting"], "Hp": 50, "Att": 80, "AttSpe": 55, "Def": 60, "DefSpe": 50, "Speed": 80 }
const amaya_learnset: Dictionary = {"Name":"Amaya",1:["Charge"],6:["Bulle"]}

#const jarod: Array = ["Jarod", ["Dark"], 40 , 50, 70, 50, 50, 70]
#const maurad: Array = ["Maurad", ["Dragon"], 35, 90, 30, 30, 100]
#const francois: Array = ["Francois", ["Fight"], 70, 25, 55, 55, 40]

 */