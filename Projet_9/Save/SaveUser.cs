using Newtonsoft.Json;
using NJSON;
using NSecurity;
using System.Collections.Generic;
using System.IO;

namespace Projet_9.Save
{
    public class SaveUser
    {
        private static SaveUser instance;

        private JsonDeveloper jsonSaver;

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
            jsonSaver = JsonDeveloper.GetInstance();
            jsonSaver.CreateFolder(_folderName);
            jsonSaver.CreateFolder(_folderName + "/" + _folderUserName);
            _filePath = _folderName + "/" + _folderUserName + "/" + _fileName + _fileType;
        }

        public SaveUser(string userName, string userUid, bool needFolders = true)
        {
            jsonSaver = JsonDeveloper.GetInstance();
            _userTag = userName + "_" + userUid;
            _filePath = _folderName + "/" + _folderUserName + "/" + _fileName + _fileType;
            if (needFolders)
            {
                jsonSaver.CreateFolder(_folderName);
                jsonSaver.CreateFolder(_folderName + "/" + _folderUserName);
                jsonSaver.CreateFolder(_folderName + "/" + _folderUserName + "/" + _userTag);
            }
        }

        public static SaveUser GetInstance(string userName, string userUid)
        {
            if (instance == null)
            {
                instance = new SaveUser(userName, userUid);
            }
            return instance;
        }
        public static SaveUser GetInstance(string userName, string userUid, bool needFolders)
        {
            if (instance == null)
            {
                instance = new SaveUser(userName, userUid, needFolders);
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
