using System.Net;

namespace Csharp_Tpt
{
    public class Global
    {
        public static bool IsInBattle = false;
		public List<Pokemon>? PlayerPokemons;
		// public Dictionary PlayerItems = { "Pokeball":{"Num":100} }
		// public List<Pokemon_Class> PC

		// public List<Pokemon_Class> EnemyPokemons

		public enum PokemonType {	
			Normal, Fire, Water, Grass, Electric, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Steel, Dark, Fairy, Dragon, Unknown
		}

    	public  float[,] Chart { get; } = new float[18, 19]
    	{
    	    //               Normal  Fire  Water  Grass  Electric Ice   Fighting  Poison  Ground  Flying  Psychic  Bug   Rock   Ghost  Steel  Dark  Fairy  Dragon Unknown
    	    /* Normal */    {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  1.0f,    1.0f,   1.0f,   1.0f,   1.0f,    1.0f, 1.0f,  0.0f,  0.5f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Fire */      {1.0f,  0.5f, 0.5f,  2.0f,  1.0f,    2.0f,  1.0f,    1.0f,   1.0f,   1.0f,   1.0f,    2.0f, 0.5f,  1.0f,  2.0f,  1.0f, 1.0f,  0.5f,  0.0f },
    	    /* Water */     {1.0f,  2.0f, 0.5f,  0.5f,  1.0f,    1.0f,  1.0f,    1.0f,   2.0f,   1.0f,   1.0f,    1.0f, 2.0f,  1.0f,  1.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Grass */     {1.0f,  0.5f, 2.0f,  0.5f,  1.0f,    1.0f,  1.0f,    0.5f,   2.0f,   0.5f,   1.0f,    2.0f, 0.5f,  1.0f,  0.5f,  1.0f, 1.0f,  0.5f,  0.0f },
    	    /* Electric */  {1.0f,  1.0f, 2.0f,  0.5f,  0.5f,    1.0f,  1.0f,    1.0f,   0.0f,   2.0f,   1.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 1.0f,  0.5f,  0.0f },
    	    /* Ice */       {1.0f,  0.5f, 0.5f,  2.0f,  1.0f,    0.5f,  1.0f,    1.0f,   2.0f,   2.0f,   1.0f,    1.0f, 2.0f,  1.0f,  0.5f,  1.0f, 1.0f,  2.0f,  0.0f },
    	    /* Fighting */  {2.0f,  1.0f, 1.0f,  1.0f,  1.0f,    2.0f,  1.0f,    0.5f,   1.0f,   0.5f,   0.5f,    0.5f, 2.0f,  0.0f,  1.0f,  2.0f, 2.0f,  0.5f,  0.0f },
    	    /* Poison */    {1.0f,  1.0f, 1.0f,  2.0f,  1.0f,    1.0f,  1.0f,    0.5f,   0.5f,   1.0f,   1.0f,    1.0f, 0.5f,  0.5f,  1.0f,  1.0f, 2.0f,  1.0f,  0.0f },
    	    /* Ground */    {1.0f,  2.0f, 1.0f,  0.5f,  2.0f,    1.0f,  1.0f,    2.0f,   1.0f,   0.0f,   1.0f,    0.5f, 2.0f,  1.0f,  1.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Flying */    {1.0f,  1.0f, 1.0f,  2.0f,  0.5f,    2.0f,  2.0f,    1.0f,   1.0f,   1.0f,   1.0f,    0.5f, 1.0f,  1.0f,  1.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Psychic */   {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  2.0f,    2.0f,   1.0f,   1.0f,   0.5f,    1.0f, 1.0f,  1.0f,  1.0f,  0.0f, 1.0f,  1.0f,  0.0f },
    	    /* Bug */       {1.0f,  2.0f, 1.0f,  0.5f,  1.0f,    1.0f,  0.5f,    1.0f,   0.5f,   0.5f,   2.0f,    1.0f, 1.0f,  0.5f,  1.0f,  2.0f, 0.5f,  1.0f,  0.0f },
    	    /* Rock */      {1.0f,  0.5f, 2.0f,  2.0f,  1.0f,    2.0f,  0.5f,    0.5f,   2.0f,   0.5f,   1.0f,    2.0f, 1.0f,  1.0f,  2.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Ghost */     {0.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  1.0f,    1.0f,   1.0f,   1.0f,   2.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 0.5f,  1.0f,  0.0f },
    	    /* Steel */     {0.5f,  2.0f, 1.0f,  0.5f,  0.5f,    2.0f,  1.0f,    1.0f,   2.0f,   0.5f,   0.5f,    0.5f, 2.0f,  1.0f,  0.5f,  1.0f, 0.5f,  0.5f,  0.0f },
    	    /* Dark */      {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  2.0f,    1.0f,   1.0f,   1.0f,   2.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 0.5f,  1.0f,  0.0f },
    	    /* Fairy */     {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  0.5f,    2.0f,   1.0f,   1.0f,   1.0f,    0.5f, 1.0f,  1.0f,  2.0f,  1.0f, 2.0f,  2.0f,  0.0f },
    	    /* Dragon */    {1.0f,  1.0f, 1.0f,  1.0f,  0.5f,    2.0f,  1.0f,    1.0f,   1.0f,   1.0f,   1.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 2.0f,  2.0f,  0.0f }
    	};

		public PokemonType TypeToIndex(string typeName) { 
			if (typeName == "Water"){         return PokemonType.Water;}
			else if (typeName == "Fire"){        return PokemonType.Fire;}
			else if (typeName == "Grass"){       return PokemonType.Grass;}
			else if (typeName == "Electric"){    return PokemonType.Electric;}
			else if (typeName == "Normal"){      return PokemonType.Normal;}
			else if (typeName == "Ice"){         return PokemonType.Ice;}
			else if (typeName == "Fighting"){    return PokemonType.Fighting;}
			else if (typeName == "Poison"){      return PokemonType.Poison;}
			else if (typeName == "Ground"){      return PokemonType.Ground;}
			else if (typeName == "Flying"){      return PokemonType.Flying;}
			else if (typeName == "Psychic"){     return PokemonType.Psychic;}
			else if (typeName == "Bug"){         return PokemonType.Bug;}
			else if (typeName == "Rock"){        return PokemonType.Rock;}
			else if (typeName == "Ghost"){       return PokemonType.Ghost;}
			else if (typeName == "Steel"){       return PokemonType.Steel;}
			else if (typeName == "Dark"){        return PokemonType.Dark;}
			else if (typeName == "Fairy"){       return PokemonType.Fairy;}
			else if (typeName == "Dragon"){      return PokemonType.Dragon;}
            else {return PokemonType.Unknown;}
		}

		public static bool SuccessAcc(int Acc) {
			Random rnd = new Random();
			int Num = rnd.Next(1,100);

			if (Num > Acc) {
				return false;
			}
			else
            {
				return true;
            }
		}

		public static int SuccessCritical(int Speed) {
			Random rnd = new Random();
			int random = rnd.Next(1,100);
			int Chance = Speed / 512;
			if (random > Chance * 100) {return 1;}
            else {return 2;}
        }

		public static float StabCalculator(string[] PokemonTypes, string AttackType){
			if (PokemonTypes.Contains(AttackType)) { return 1.5f; }
			else { return 1.0f; }
        }

		public int DamageCalculator(Pokemon P1,Pokemon P2,Attack MoveP1,int Critical ) {

			
			// MOVE VARIABLES
			int MovePower = MoveP1.GetPower();
			string MoveType = MoveP1.GetType();
			string MoveCat = MoveP1.GetCat();
	
			// P1 VARIABLES
			int P1Attack = P1.GetAttack();
			int P1AttackSpe = P1.GetAttackSpe();
			int P1Level = P1.GetLevel();
			string[] P1Types = P1.GetTypes();
			// P2 VARIABLES
			int P2Defense = P2.GetDefense();
			int P2DefenseSpe = P2.GetDefenseSpe();
			string[] P2Types = P2.GetTypes();
			

			// COMBAT VARIABLES
			float stab;
			float type1;
			float type2;
			float random;

			string PokemonTypes1 = "Normal";
			string AttackType1 = "Normal";
			int critical = Critical;

			type1 = Chart[(int)TypeToIndex(AttackType1), (int)TypeToIndex(PokemonTypes1)];

			if (P2Types.Length > 2){type2 = Chart[(int)TypeToIndex(MoveType), (int)TypeToIndex(P2Types[1])];}
			else{ type2=1.0f; }
			random = 1;
			stab = StabCalculator(P2Types,MoveType);
			int Damage = 1;

			if (MoveCat == "Physical"){Damage = (int)Math.Floor(((2*P1Level/5 +2) * MovePower * (P1Attack / P2Defense)/50 * Critical + 2) * stab * type1 * type2 * random);}
			else{Damage = (int)Math.Floor(((2*P1Level/5 +2) * MovePower * (P1AttackSpe / P2DefenseSpe)/50 * Critical + 2) * stab * type1 * type2 * random);}
			return Damage;
        }

    	/// <summary> Calculates the catch rate of the pokemon, if it's able to catch it or not.</summary>
		public static bool Cath(Pokemon PokemonToCatch)
		{
			Random rnd = new Random();

			int rate = 1;
			int BonusBall = 1;
			int BonusStatus = 1;
			
			int a = (3 * PokemonToCatch.GetMaxHp() - 2 * PokemonToCatch.GetHp()) / 3 * PokemonToCatch.GetMaxHp() * rate * BonusBall * BonusStatus;
			return rnd.Next() > a;
		}

		public static bool OddsEscape(int SpeedP, int SpeedW)
		{
			Random rnd = new Random();
			int Attempts = 1;
			int OddEscape = ( SpeedP*32/(SpeedW/4 % 256 ) ) + 30 * Attempts;
			if (OddEscape < 255){
				if (rnd.Next() < OddEscape){return true;}
				else{ return false;}
			}
			else{return true;}
		}

		public void ChangePokemonOrder(List<Pokemon> PokemonList,int x, int y)
		{
			Pokemon X = PokemonList[x];
			Pokemon Y = PokemonList[y];

			PokemonList[x] = Y;
			PokemonList[y] = X;
		} 		

		/// <summary> Heals all the pokemons of the player </summary> <param name="PokemonList"></param>
		public void HealTeamPokemon(List<Pokemon> PokemonList)
		{
			foreach (Pokemon i in PokemonList){i.Restore();}
		}

    	public Pokemon ReadPokemonDatas(string nameP, int level = 1)
    	{
    	    Console.WriteLine("Name: " + nameP);
	
    	    Dictionary<string, object> P = PokemonsDic.pokemons[nameP];
    	    List<string> PTypes = new List<string>();
    	    foreach (string type in (List<object>)P["Types"])
    	    {
    	        PTypes.Add(type);
    	    }
	
    	    return new Pokemon((string)P["Name"], PTypes.ToArray(), (int)P["Hp"], (int)P["Att"], (int)P["AttSpe"], (int)P["Def"], (int)P["DefSpe"], (int)P["Speed"], level);
    	}
    	public Attack ReadAttacksDatas(string nameA)
    	{
    	    Dictionary<string, object> A = AttacksDic.attacks[nameA] as Dictionary<string, object>;
    	    return new Attack((string)A["Name"], (string)A["Type"], (string)A["Cat"], (int)A["Power"], (int)A["Acc"], (int)A["Pp"]);
    	}

    }
}

/*
func StartWildFight(body) -> void:
	#var ListPokemonsWild: Array[Dictionary] = [Pokemons.maurad,Pokemons.francois,Pokemons.jean]
	var ListPokemonsWild: Array[Dictionary] = [Pokemons.XPFARM,Pokemons.XPFARM,Pokemons.XPFARM]
	var EP = ListPokemonsWild[randi_range(0,ListPokemonsWild.size()-1)] #Enemy Pokemon
	EnemyPokemons.clear()

	var EPTypes:Array[String]
	for i in EP.Types:
		EPTypes.append(i)
	
	#var EP2 = ListPokemonsWild[randi_range(0,ListPokemonsWild.size()-1)] #Enemy Pokemon
	#var EPTypes2:Array[String]
	#for i in EP2.Types:
	#	EPTypes2.append(i)
	
	EnemyPokemons.append(Pokemon_Class.new(EP.Name,EPTypes,EP.Hp,EP.Att,EP.AttSpe,EP.Def,EP.DefSpe,EP.Speed,randi_range(1,3)))
	EnemyPokemons[0].Moves.append(Attack_Class.new("Count","Grass","Physical",70,80,10))
	
	#EnemyPokemons.append(Pokemon_Class.new(EP2.Name,EPTypes2,EP2.Hp,EP2.Att,EP2.AttSpe,EP2.Def,EP2.DefSpe,EP2.Speed,randi_range(1,3)))
	#EnemyPokemons[1].Moves.append(Attack_Class.new("Count","Grass","Physical",70,80,10))
	
	get_tree().root.get_node("/root/Battle").get_child(0).StartBattle(PlayerPokemons,EnemyPokemons, body)

func StartFight(body,Pokemon:Array[String],PokemonLevel:Array[int],npc) -> void:
	EnemyPokemons.clear()
	for i:int in range(Pokemon.size()):
		EnemyPokemons.append(ReadPokemonsDatas(Pokemon[i],PokemonLevel[i]))
	for i:int in range(EnemyPokemons.size()):
		EnemyPokemons[i].Moves.append(Attack_Class.new("Charge","Normal","Physical",20,100,30))

	#EnemyPokemons.append(Pokemon_Class.new(EP.Name,EPTypes,EP.Hp,EP.Att,EP.AttSpe,EP.Def,EP.DefSpe,EP.Speed,PokemonLevel))
	#EnemyPokemons[0].Moves.append(Attack_Class.new("Count","Grass","Physical",70,80,10))
	get_tree().root.get_node("/root/Battle").get_child(0).StartBattle(PlayerPokemons,EnemyPokemons, body,npc,false)

*/