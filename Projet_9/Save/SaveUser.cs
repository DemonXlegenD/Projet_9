using Newtonsoft.Json;
using NJSON;
using NSave;
using NSecurity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_9.Save
{
    public class SaveUser
    {
        private static SaveUser instance;

        private JsonDevelopper jsonSaver;

        private string _folderName = "Saves";

        private string _folderUserName = "Users";
        private string _fileName = "users";

        private string _fileType = ".json";
        private string _userTag = string.Empty;
        private string _filePath = string.Empty;

        public string Name
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public SaveUser()
        {
            jsonSaver = JsonDevelopper.GetInstance();
            jsonSaver.CreateFolder(_folderName);
            jsonSaver.CreateFolder(_folderName + "/" + _folderUserName);
            _filePath = _folderName + "/" + _folderUserName + "/" + _fileName + _fileType;
        }

        public SaveUser(string userName, string userUid)
        {
            jsonSaver = JsonDevelopper.GetInstance();
            jsonSaver.CreateFolder(_folderName);
            jsonSaver.CreateFolder(_folderName + "/" + _folderUserName);
            _userTag = userName + "_" + userUid;
            jsonSaver.CreateFolder(_folderName + "/" + _folderUserName + "/" + _userTag);
            _filePath = _folderName + "/" + _folderUserName + "/" + _fileName + _fileType;
        }

        public static SaveUser GetInstance(string userName, string userUid)
        {
            if (instance == null)
            {
                instance = new SaveUser(userName, userUid);
            }
            return instance;
        }

        public static SaveUser GetInstance()
        {
            if (instance == null)
            {
                instance = new SaveUser();
            }
            return instance;
        }

        public string GetFilePath()
        {
            return _filePath;
        }
        public void SaveUsersIntoFile(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<User> LoadUserFromSaveFile()
        {
            List<User> users = new List<User>();
            if (File.Exists(_filePath))
            {
                /*Console.WriteLine($"Path trouvé : {_filePath}");*/
                string json = File.ReadAllText(_filePath);
                users = JsonConvert.DeserializeObject<List<User>>(json);
                if (users == null)
                {
                    users = new List<User>();
                }
            }
            else
            {
                /*Console.WriteLine($"Path non trouvé : {_filePath}");*/
            }
            return users;
        }
    }
}
