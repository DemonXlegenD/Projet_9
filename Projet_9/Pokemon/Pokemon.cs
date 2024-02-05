using NEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NPokemon
{
    public enum NonVolatileStatus
    {
        Burn,
        Freeze,
        Paralysis,
        Sleep,
        Poison,
        None
    }

    public enum VolatileStatus
    {
        Confusion,
        Curse,
        Flinch,
        Infatuation,
        Seeding,
        Taunt
    }
    public class Pokemon
    {
        // VALUES DEFAULT
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int AttackSpe { get; set; }
        public int Defense { get; set; }
        public int DefenseSpe { get; set; }
        public int Speed { get; set; }
        public List<Attack> Moves { get; set; } = new List<Attack>();
        public string[] PreviousMovesLearned { get; set; }

        // Max Values
        public int MaxHp { get; set; }
        public int MaxAttack { get; set; }
        public int MaxAttackSpe { get; set; }
        public int MaxDefense { get; set; }
        public int MaxDefenseSpe { get; set; }
        public int MaxSpeed { get; set; }

        // OTHER VALUES
        public string OriginalName { get; set; }
        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BaseAttackSpe { get; set; }
        public int BaseDefense { get; set; }
        public int BaseDefenseSpe { get; set; }
        public int BaseSpeed { get; set; }

        public int Level { get; set; } = 1;
        public int Xp { get; set; }
        public int XpNext { get; set; }

        // IV VALUE BETWEEN 0 AND 31
        public int IVHp { get; set; }
        public int IVAttack { get; set; }
        public int IVAttackSpe { get; set; }
        public int IVDefense { get; set; }
        public int IVDefenseSpe { get; set; }
        public int IVSpeed { get; set; }

        public List<VolatileStatus> STATUSVOLATILE = new List<VolatileStatus>();
        public NonVolatileStatus STATUSNONVOLATILE = NonVolatileStatus.None;

        // Formula of Gen1 of pokemon
        public int StatCalculationOtherGen1(int BaseStat, int IVStat, int StateXp = 0)
        {
            return (int)(((((BaseStat + IVStat) * 2) + (Math.Sqrt(StateXp) / 4 * Level)) / 100) + 5);
        }

        public int StatCalculationHpGen1(int StateXp = 0)
        {
            return (int)(((((BaseHp + IVHp) * 2) + (Math.Sqrt(StateXp) / 4 * Level)) / 100) + Level + 10);
        }

        // Formula of Gen3 of pokemon
        public int StatCalculationOtherGen3(int BaseStat, int IVStat, int EV = 0, int Nature = 1)
        {
            return ((((2 * BaseStat) + IVStat + (EV / 4)) * Level / 100) + 5) * Nature;
        }

        public int StatCalculationHpGen3(int EV = 0)
        {
            return (((2 * BaseHp) + IVHp + (EV / 4)) * Level / 100) + Level + 10;
        }

        public void RedoStats()
        {
            MaxHp = StatCalculationHpGen3();
            MaxAttack = StatCalculationOtherGen3(BaseAttack, IVAttack);
            MaxAttackSpe = StatCalculationOtherGen3(BaseAttackSpe, IVAttackSpe);
            MaxDefense = StatCalculationOtherGen3(BaseDefense, IVDefense);
            MaxDefenseSpe = StatCalculationOtherGen3(BaseDefenseSpe, IVDefenseSpe);
            MaxSpeed = StatCalculationOtherGen3(BaseSpeed, IVSpeed);
        }

        // Constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Pokemon(string name_, List<string> types, int hp, int attack, int attackspe, int defense, int defensespe, int speed, int level = 1)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            OriginalName = name_;
            BaseHp = hp;
            BaseAttack = attack;
            BaseAttackSpe = attackspe;
            BaseDefense = defense;
            BaseDefenseSpe = defensespe;
            BaseSpeed = speed;

            Level = level;
            XpNext = Level * 20 / 4;

            IVHp = new Random().Next(0, 32);
            IVAttack = new Random().Next(0, 32);
            IVAttackSpe = new Random().Next(0, 32);
            IVDefense = new Random().Next(0, 32);
            IVDefenseSpe = new Random().Next(0, 32);
            IVSpeed = new Random().Next(0, 32);

            RedoStats();

            // Set other values
            Name = name_;
            Types = types;
            Hp = MaxHp;
            Attack = MaxAttack;
            AttackSpe = MaxAttackSpe;
            Defense = MaxDefense;
            DefenseSpe = MaxDefenseSpe;
            Speed = MaxSpeed;
            //GiveMoves();
        }


        // func Restore() -> void: ## To restore the pokemon Hp and restore all the Pp of all his moves
        // 	Hp = MaxHp
        // 	for i in Moves:
        // 		i.ResetPp()

        public void Restore()
        {
            Hp = MaxHp;
            foreach (Attack i in Moves) { i.ResetPp(); }
        }

        public bool IsAlive()
        {
            return Hp > 0;
        }

        public void ChangeXp(int x)
        {
            Xp += x;
            if (Xp > XpNext)
            {
                int LeftOverXp = Xp - XpNext;
                Xp = LeftOverXp;
                Level += 1;
                LevelUp();
                XpNext = Level * 20 / 4;
            }
        }

        public void LevelUp()
        {
            RedoStats();
            //SetMoveToLearn();
        }

        public void TakeDamage(int x) { Hp -= x; }

        // SETTERS
        public void SetName(string NewName) { Name = NewName; }
        public void AddHp(int x) { Hp += x; }

        public void DeathHp() { Hp = 0; }

        public void SubstractAttack(int x) { Attack -= x; }
        public void SubstractAttackSpe(int x) { AttackSpe -= x; }
        public void SubstractDefense(int x) { Defense -= x; }
        public void SubstractDefenseSpe(int x) { DefenseSpe -= x; }
        public void SubstractSpeed(int x) { Speed -= x; }

        public void SetLevel(int x)
        {
            Level = x;
            RedoStats();
        }

        // public void SetMoveToLearn()
        // {
        //     Dictionary<int, string> P;
        //     string string_ = OriginalName.ToLower() + "_learnset";
        //     if (Pokemons.PokemonLearnSet_Dic.TryGetValue(string_, out P))
        //     {
        //         MoveToLearn = P[Level];
        //     }
        // }

        public int HealPercentage(int x)
        {
            Hp += (int)Math.Floor((double)(MaxHp * x) / 100);
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
            return Hp;
        }

        // GETTERS DEFAULT VALUES
        public string GetName() { return Name; }
        public List<string> GetTypes() { return Types; }
        public int GetHp() { return Hp; }
        public int GetAttack() { return Attack; }
        public int GetAttackSpe() { return AttackSpe; }
        public int GetDefense() { return Defense; }
        public int GetDefenseSpe() { return DefenseSpe; }
        public int GetSpeed() { return Speed; }
        public int GetLevel() { return Level; }

        // GETTERS MAX VALUES
        public int GetMaxHp() { return MaxHp; }
        public int GetMaxAttack() { return MaxAttack; }
        public int GetMaxAttackSpe() { return MaxAttackSpe; }
        public int GetMaxDefense() { return MaxDefense; }
        public int GetMaxDefenseSpe() { return MaxDefenseSpe; }
        public int GetMaxSpeed() { return MaxSpeed; }

        // GETTERS OTHER VALUES
        public int GetXp() { return Xp; }
        public int GetXpNext() { return XpNext; }

        public int GetBaseHp() { return BaseHp; }
        public int GetBaseAttack() { return BaseAttack; }
        public int GetBaseAttackSpe() { return BaseAttackSpe; }
        public int GetBaseDefense() { return BaseDefense; }
        public int GetBaseDefenseSpe() { return BaseDefenseSpe; }
        public int GetBaseSpeed() { return BaseSpeed; }
        public string GetOriginalName() { return OriginalName; }

        public int GetIVHp() { return IVHp; }
        public int GetIVAttack() { return IVAttack; }
        public int GetIVAttackSpe() { return IVAttackSpe; }
        public int GetIVDefense() { return IVDefense; }
        public int GetIVDefenseSpe() { return IVDefenseSpe; }
        public int GetIVSpeed() { return IVSpeed; }

        // public Array GetMoveToLearn()
        // {
        //     return MoveToLearn;
        // }

       
    }
    public class PokemonJsonConverter : JsonConverter<Pokemon>
    {
        public override void WriteJson(JsonWriter writer, Pokemon value, JsonSerializer serializer)
        {

            writer.WritePropertyName(value.Name);
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(Pokemon.Hp));
            serializer.Serialize(writer, value.Hp);

            writer.WritePropertyName(nameof(Pokemon.OriginalName));
            serializer.Serialize(writer, value.OriginalName);

            writer.WritePropertyName(nameof(Pokemon.Name));
            serializer.Serialize(writer, value.Name);

            writer.WritePropertyName(nameof(Pokemon.Xp));
            serializer.Serialize(writer, value.Xp);

            writer.WritePropertyName(nameof(Pokemon.Level));
            serializer.Serialize(writer, value.Level);

            writer.WritePropertyName(nameof(Pokemon.IVHp));
            serializer.Serialize(writer, value.IVHp);

            writer.WritePropertyName(nameof(Pokemon.IVAttack));
            serializer.Serialize(writer, value.IVAttack);

            writer.WritePropertyName(nameof(Pokemon.IVAttackSpe));
            serializer.Serialize(writer, value.IVAttackSpe);

            writer.WritePropertyName(nameof(Pokemon.IVDefense));
            serializer.Serialize(writer, value.IVDefense);

            writer.WritePropertyName(nameof(Pokemon.IVDefenseSpe));
            serializer.Serialize(writer, value.IVDefenseSpe);

            writer.WritePropertyName(nameof(Pokemon.IVSpeed));
            serializer.Serialize(writer, value.IVSpeed);

            writer.WriteEndObject();
        }
        public override Pokemon ReadJson(JsonReader reader, Type objectType, Pokemon existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

/*
 class_name Pokemon_Class extends Node

# VALUES DEFAULT

## Name of the Pokemon
var Name:String
## Types of the Pokemon (can have either 1 or 2 types)
var Types: Array[String]
## Actual Hp of the Pokemon
var Hp:int
## Actual Attack of the Pokemon
var Attack:int
## Actual Attack Special of the Pokemon
var AttackSpe:int
## Actual Defense of the Pokemon
var Defense:int
## Actual Defense Special of the Pokemon
var DefenseSpe:int
## Actual Speed of the Pokemon
var Speed:int
## The list of the Moves that the Pokemon has
var Moves:Array[Attack_Class]
## The list of the previous attacks of the pokemon
var PreviousMovesLearned:Array[String]

# Max Values
## Max value used to do damage based on max hp or restore the hp of the pokemon
var MaxHp:int
var MaxAttack:int
var MaxAttackSpe:int
var MaxDefense:int
var MaxDefenseSpe:int
var MaxSpeed:int

# OTHER VALUES
## The name that the pokemon has at the start
var OriginalName:String
## Base Hp stat used to calculate the Hp will have
var BaseHp:int
## Base Attack stat used to calculate the Attack will have
var BaseAttack:int
## Base AttackSpe stat used to calculate the AttackSpe will have
var BaseAttackSpe:int
## Base Defense stat used to calculate the Defense will have
var BaseDefense:int
## Base DefenseSpe stat used to calculate the DefenseSpe will have
var BaseDefenseSpe:int
## Base Speed stat used to calculate the Speed will have
var BaseSpeed:int

var Level:int = 1
var Xp:int
var XpNext:int

# IV VALUE BETWEEN O AND 31
var IVHp:int
var IVAttack:int
var IVAttackSpe:int
var IVDefense:int
var IVDefenseSpe:int
var IVSpeed:int

var MoveToLearn:Array

enum NonVolatileStatus{
	Burn,Freeze,Paralysis,Sleep,Poison,None
}
enum VolatileStatus{
	Confusion,Curse,Flinch,Infatuation,Seeding,Taunt
}
var STATUSNONVOLATILE:NonVolatileStatus = NonVolatileStatus.None ## Only one, won't be change if change pokemon
var STATUSVOLATILE:Array[VolatileStatus] = [] ## Can have multiple volatile status, will dissapear if changes the pokemon

## Formula of Gen1 of pokemon : Uses BaseStat, the StateXp of the stat EV, and the level of the pokemon, to return the stat that is assigned to the pokemonstat
func StatCalculationOtherGen1(BaseStat:int,IVStat:int,StateXp:int = 0) -> int:
	return ( (((BaseStat + IVStat ) * 2 + (sqrt(StateXp)/4) * Level)/100) + 5 )

## Formula of Gen1 of pokemon : Uses the base hp and the iv hp stat to return the value of the hp
func StatCalculationHpGen1(StateXp:int = 0) -> int:
	return ( (((BaseHp + IVHp) * 2 + (sqrt(StateXp)/4) * Level)/100) + Level + 10 )
	
## Formula of Gen3 of pokemon : Uses BaseStat, the StateXp of the stat EV, and the level of the pokemon, to return the stat that is assigned to the pokemonstat
func StatCalculationOtherGen3(BaseStat:int,IVStat:int,EV:int = 0,Nature:int=1) -> int:
	return ( ((((2 * BaseStat + IVStat + EV/4) * Level)/100) + 5) * Nature )

## Formula of Gen3 of pokemon : Uses the base hp and the iv hp stat to return the value of the hp
func StatCalculationHpGen3(EV:int = 0) -> int:
	return ( (((2 * BaseHp + IVHp + EV/4) * Level)/100) + Level + 10 )

# level = math.floor( xp / (2000 + (math.floor( xp / 2000) * 200)))

func RedoStats() -> void:
	MaxHp = StatCalculationHpGen3()
	MaxAttack = StatCalculationOtherGen3(BaseAttack,IVAttack)
	MaxAttackSpe = StatCalculationOtherGen3(BaseAttackSpe,IVAttackSpe)
	MaxDefense = StatCalculationOtherGen3(BaseDefense,IVDefense)
	MaxDefenseSpe = StatCalculationOtherGen3(BaseDefenseSpe,IVDefenseSpe)
	MaxSpeed = StatCalculationOtherGen3(BaseSpeed,IVSpeed)

func GiveMoves() -> void:
	#var PokemonIndexLearn = Pokemons.PokemonLearnSet_List.find(OriginalName.to_lower()+"_learnset")
	#for i in range(0,Pokemons.PokemonLearnSet_List.size()):
	#	#print(i,": ",str(Pokemons.PokemonLearnSet_List[i]))
	#	if Pokemons.PokemonLearnSet_List[i].get("Name") == OriginalName:
	#		print("True")
	#		break
	#print(PokemonIndexLearn)
	#print(OriginalName.to_lower()+"_learnset")
	
	
	var P:Dictionary 
	var string:String = OriginalName.to_lower()+"_learnset"
	
	if Pokemons.PokemonLearnSet_Dic.get(string) != null:
		P = Pokemons.PokemonLearnSet_Dic.get(string)
		for i:int in P:
			if Moves.size() >= 4 || i > Level:
				break
			for x:String in P.get(i):
				Moves.append(Global.ReadAttacksDatas(x))
				if Moves.size() >= 4:
					break

## CONSTUCTOR
func _init(name_:String,types:Array[String],hp:int,attack:int,attackspe:int,defense:int,defensespe:int,speed:int,level:int = 1) -> void:
	# OTHER VALUES
	OriginalName = name_
	BaseHp = hp
	BaseAttack = attack
	BaseAttackSpe = attackspe
	BaseDefense = defense
	BaseDefenseSpe = defensespe
	BaseSpeed = speed
	
	Level = level
	XpNext = Level * 20 / 4

	# IV VALUES
	IVHp = randi_range(0,31)
	IVAttack = randi_range(0,31)
	IVAttackSpe = randi_range(0,31)
	IVDefense = randi_range(0,31)
	IVDefenseSpe = randi_range(0,31)
	IVSpeed = randi_range(0,31)
	
	RedoStats()
	
	#VALUES DEFAULT
	Name = name_
	Types = types

	
	Hp = MaxHp
	Attack = MaxAttack
	AttackSpe = MaxAttackSpe
	Defense = MaxDefense
	DefenseSpe = MaxDefenseSpe
	Speed = MaxSpeed
	GiveMoves()
	
# CHANGERS
func Restore() -> void: ## To restore the pokemon Hp and restore all the Pp of all his moves
	Hp = MaxHp
	for i in Moves:
		i.ResetPp()

func AfterFight() -> void: ## After a fight, resets all the values other than the status and hp
	Attack = MaxAttack
	AttackSpe = MaxAttackSpe
	Defense = MaxDefenseSpe
	DefenseSpe = MaxDefenseSpe
	Speed = MaxSpeed

func IsAlive() -> bool: ## Checks if the pokemon is alive, if not, return false
	if Hp <= 0:
		return false
	return true

func ChangeXp(x:int) -> void:
	Xp += x
	if Xp > XpNext:
		var LeftOverXp = Xp - XpNext
		Xp = LeftOverXp
		Level += 1
		LevelUp()
		XpNext = Level * 20 / 4

func LevelUp() -> void: ## When the pokemon levelup
	RedoStats()
	SetMoveToLearn()

func TakeDamage(x:int) -> void: ## Takes the damage using the x 
	Hp -= x

# SETTERS
func SetName(NewName:String) -> void: ## To set the name of the pokemon if we want to change it
	Name = NewName
func SetHp(x:int) -> void: ## To change the hp (can be used for items)
	Hp += x
func DeathHp() -> void: ## Used with the is alive, so it doesnt return a negative value but just 0
	Hp = 0
func SetAttack(x:int) -> void: ## To change the attack
	Attack -= x
func SetAttackSpe(x:int) -> void: ## To change the attackspe
	AttackSpe -= x
func SetDefense(x:int) -> void: ## To change the defense
	Defense -= x
func SetDefenseSpe(x:int) -> void: ## To change the defensespe
	DefenseSpe -= x
func SetSpeed(x:int) -> void: ## To change the speed
	Speed -= x
func SetLevel(x:int) -> void: ## To change the level, redoing all the stats
	Level = x
	RedoStats()
	print(Level)

func SetMoveToLearn() -> void:
	var P:Dictionary 
	var string:String = OriginalName.to_lower()+"_learnset"
	#print(string)
	if Pokemons.PokemonLearnSet_Dic.get(string) != null:
		P = Pokemons.PokemonLearnSet_Dic.get(string)
		MoveToLearn = P.get(Level)

func HealPercentage(x:int) -> int:
	Hp += floor(float(MaxHp * x) / 100)
	if Hp > MaxHp:
		Hp = MaxHp
	return Hp

# GETTERS DEFAULT VALUES
func GetName() -> String:  ## To get the name of the pokemon
	return Name
func GetTypes() -> Array[String]: ## To get the types of the pokemon
	return Types
func GetHp() -> int: ## To get the hp of the pokemon
	return Hp
func GetAttack() -> int: ## To get the attack of the pokemon
	return Attack
func GetAttackSpe() -> int: ## To get the attackspe of the pokemon
	return AttackSpe
func GetDefense() -> int: ## To get the defense of the pokemon
	return Defense
func GetDefenseSpe() -> int: ## To get the defenspe of the pokemon
	return DefenseSpe
func GetSpeed() -> int: ## To get the speed of the pokemon
	return Speed
func GetLevel() -> int: ## To get the level of the pokemon
	return Level

# GETTERS MAX VALUES
func GetMaxHp() -> int: ## To get the maxhp of the pokemon
	return MaxHp
func GetMaxAttack() -> int: ## To get the maxattack of the pokemon
	return MaxAttack
func GetMaxAttackSpe() -> int: ## To get the maxattackspe of the pokemon
	return MaxAttackSpe
func GetMaxDefense() -> int: ## To get the maxdefense of the pokemon
	return MaxDefense
func GetMaxDefenseSpe() -> int: ## To get the maxdefensespe of the pokemon
	return MaxDefenseSpe
func GetMaxSpeed() -> int: ## To get the maxspeed of the pokemon
	return MaxSpeed

# GETTERS OTHER VALUES
func GetXp() -> int: ## To get the xp of the pokemon
	return Xp
func GetXpNext() -> int: ## To get the xpnext (needed to level up) of the pokemon
	return XpNext

func GetBaseHp() -> int: ## To get the basehp of the pokemon
	return BaseHp
func GetBaseAttack() -> int: ## To get the baseattack of the pokemon
	return BaseAttack
func GetBaseAttackSpe() -> int: ## To get the baseattackspe of the pokemon
	return BaseAttackSpe
func GetBaseDefense() -> int: ## To get the basedefense of the pokemon
	return BaseDefense
func GetBaseDefenseSpe() -> int: ## To get the basedefensespe of the pokemon
	return BaseDefenseSpe
func GetBaseSpeed() -> int: ## To get the speed of the pokemon
	return BaseSpeed
func GetOriginalName() -> String: ## To get the original name of the pokemon
	return OriginalName
	
func GetIVHp() -> int: ## To get the ivhp of the pokemon
	return IVHp
func GetIVAttack() -> int: ## To get the ivattack of the pokemon
	return IVAttack
func GetIVAttackSpe() -> int: ## To get the ivattackspe of the pokemon
	return IVAttackSpe
func GetIVDefense() -> int: ## To get the defense of the pokemon
	return IVDefense
func GetIVDefenseSpe() -> int: ## To get the defensespe of the pokemon
	return IVDefenseSpe
func GetIVSpeed() -> int: ## To get the speed of the pokemon
	return IVSpeed

func GetMoveToLearn() -> Array:
	return MoveToLearn

 */