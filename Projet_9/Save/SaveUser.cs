using Newtonsoft.Json;
using NJSON;
using NSave;
using NSecurity;
using System;
using System.Collections.Generic;
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
        }

        public SaveUser(string userName, string userUid)
        {
            jsonSaver = JsonDevelopper.GetInstance();
            jsonSaver.CreateFolder(_folderName);
            jsonSaver.CreateFolder(_folderName + "/" + _folderUserName);
            _userTag = userName + "_" + userUid;
            jsonSaver.CreateFolder(_folderName + "/" + _folderUserName + "/" + _userTag);
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
            return _folderName + "/" + _folderUserName + "/" + _fileName + _fileType;
        }
        public void SaveUsers()
        {
            UserManager userManager = UserManager.GetInstance();
            userManager.SaveUserIntoFile(_folderName + "/" + _folderUserName + "/" + _fileName + _fileType);
        }

        public void LoadUserFromSaveFile()
        {
            UserManager userManager = UserManager.GetInstance();
            userManager.LoadUsersFromFile(_folderName + "/" + _folderUserName + "/" + _fileName + _fileType);
        }
    }
}
