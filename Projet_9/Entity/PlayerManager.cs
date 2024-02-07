using Newtonsoft.Json;
using NPokemon;
using NSave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEntity
{
    public class PlayerManager
    {
        private static PlayerManager _instance;

        private Player _player;

        private SavePlayer _savePlayer;

        public static PlayerManager GetInstance()
        {
            if(_instance == null )
            {
                _instance = new PlayerManager();
            }
            return _instance;
        }

        public PlayerManager() 
        {
            _savePlayer = SavePlayer.GetInstance();
        }
        public Player GetActualPlayer()
        {
            return _player;
        }

        public void SetPlayer( Player player )
        {
            _player = player;
        }

        public void NewPlayer(string firstname, string lastname, string Uid, int age, string description, Pokemon firstPokemon)
        {
            SetPlayer(new Player(Uid, firstname, lastname, age, description, firstPokemon));
        }

        public void LoadPlayer()
        {
            List<JsonConverter> listConverter = new List<JsonConverter>
            {
                new PlayerJsonConverter(),
                new PokemonJsonConverter()
            };
            _player = _savePlayer.ReadTestSave<Player>(listConverter);
        }
        public void LoadPlayer(int index)
        {
            List<JsonConverter> listConverter = new List<JsonConverter>
            {
                new PlayerJsonConverter(),
                new PokemonJsonConverter()
            };
            _player = _savePlayer.ReadTestSave<Player>(index, listConverter);
        }
    }
}
