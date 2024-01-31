

using NEntity;
using Newtonsoft.Json;
using NJSON;
using System.IO;
using System.Windows.Documents;
using System.Collections.Generic;

namespace NSave
{
    public class Save
    {
        static Save instance;

        private JsonDevelopper jsonSaver;
        private int _actualIndex = 1;
        private string _fileNname = "Save";
        private string _pathName = "Saves/";
        private string _fileType = ".json";

        public string Name
        {
            get { return _fileNname; }
            set { _fileNname = value; }
        }

        public Save()
        {
            jsonSaver = JsonDevelopper.GetInstance();
        }

        public static Save GetInstance()
        {
            if (instance == null)
            {
                instance = new Save();
            }
            return instance;
        }

        public void ApplySave(object data)
        {
            jsonSaver.CreateFile(data, _pathName + _fileNname + _actualIndex + _fileType);
        }
        public void ApplySave(object data, int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            jsonSaver.CreateFile(data, _pathName + _fileNname + _actualIndex + _fileType);
        }

        public object ReadSave()
        {
            object objet = jsonSaver.DeserializeJsonFromFile<object>(_pathName + _fileNname + _actualIndex + _fileType);
            return objet;
        }

        public object ReadSave(int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            object objet = jsonSaver.DeserializeJsonFromFile<object>(_pathName + _fileNname + _actualIndex + _fileType);
            return objet;
        }
        public T ReadSave<T>()
        {
            T objet = jsonSaver.DeserializeJsonFromFile<T>(_pathName + _fileNname + _actualIndex + _fileType);
            return objet;
        }


        public T ReadSave<T>(int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            T objet = jsonSaver.DeserializeJsonFromFile<T>(_pathName + _fileNname + _actualIndex + _fileType);
            return objet;
        }
        public T ReadTestSave<T>(int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new PlayerJsonConverter());

            using (StreamReader sr = new StreamReader(_pathName + _fileNname + _actualIndex + _fileType))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(reader);
            }
        }

        public void WriteSave(object data, int indexSave, List<JsonConverter> converters)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            jsonSaver.SerializeObjectToJsonFile(data, _pathName + _fileNname + _actualIndex + _fileType, converters);
        }
    }
}
