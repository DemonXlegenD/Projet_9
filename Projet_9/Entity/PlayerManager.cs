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

        private Player _player = null;

        private List<string> _saveFiles = new List<string>();

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

        public void SetUserUid(string userUid)
        {
            _savePlayer.UserTag = userUid;
        }
        public void SetPlayer( Player player )
        {
            _player = player;
        }

        public void NewPlayer(string firstname, string lastname, string Uid, int age, string description, Pokemon firstPokemon, string userUid)
        {
            SetPlayer(new Player(Uid, firstname, lastname, age, description, firstPokemon));
            _savePlayer.UserTag = userUid;
            SavePlayerInFile();
        }

        public void SavePlayerInFile(int index = 1)
        {
            List<JsonConverter> listConverter = new List<JsonConverter>
            {
                new PlayerJsonConverter(),
                new PokemonJsonConverter()
            };
            _savePlayer.WriteSave(_player, index, listConverter);
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

        public void LoadSave()
        {
            _saveFiles = _savePlayer.ListSaveFiles();
        }
        
        public void LoadPlayerFromSaveFile(string saveFile)
        {
            List<JsonConverter> listConverter = new List<JsonConverter>
            {
                new PlayerJsonConverter(),
                new PokemonJsonConverter()
            };
            _player = _savePlayer.ReadSaveFromFile<Player>(saveFile, listConverter);
        }
    }
}
