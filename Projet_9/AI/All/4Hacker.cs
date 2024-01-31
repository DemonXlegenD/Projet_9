//Hacker : se refait la meme list que le joueur d’en face, fait les meilleurs choix,
//en sachant littéralement toutes les stats de tout le monde, tous les moves etc…

using System;
using System.Collections.Generic;

namespace NPokemon
{

    // Changer de pokemon, utiliser des moves, utiliser un item
    // à voir si garder le fait d'utiliser un item

    // Faire un algo qui peut checker les actions sur plusieurs tours ->
    // Faire un truc qui permet de faire des tours

    public class Hacker
    {
        private enum states{
            SELECT, // State where he decides what to do between change pokemon or attack
            MOVES, // Choose what attack to do
            CHANGE // Choose what pokemon to change with
        }


        List<Pokemon> pokemonsSelf;
        List<Pokemon> pokemonsEnemy;
        Pokemon pokemonInBattleSelf;
        Pokemon pokemonInBattleEnemy;

        int SearchLenght = 5;

        // Si un pokemon est en vie +5 points, si un pokemon ennemie est mort +5 points,
        // Si un pokemon a perdu des hp -1 point, si un pokemon ennemie a perdu des hp +1 point
        // Si pokemon ennemie pas perdu hp -1 point
        // Si un pokemon meurt -5 points

        private Pokemon GetPokemonWithString(string name,List<Pokemon> PokemonList)
        {
            foreach (var pokemon in PokemonList)
            {
                if (pokemon.GetName() == name)
                {
                    return pokemon;
                }
            }
            return null;
        }

        public Hacker(List<Pokemon> PokemonListSelf, 
            List<Pokemon> PokemonListEnemy,
            string PokemonInBattleSelf, 
            string PokemonInBattleEnemy
        ) 
        {
            pokemonsSelf = new List<Pokemon>(PokemonListSelf);
            pokemonsEnemy = new List<Pokemon>(PokemonListEnemy);

            pokemonInBattleSelf = GetPokemonWithString(PokemonInBattleSelf,PokemonListSelf);
            pokemonInBattleEnemy = GetPokemonWithString(PokemonInBattleEnemy, PokemonListEnemy);

            // Actions :
            // Change Pokemon -> Take Damage on the new pokemon
            // Attack -> (Do damage and Take damage) or (Take damage and Do damage) 
            // Check between attacks if he's dead, if yes, has to change for a pokemon, then starts an other round


        }

    }
}