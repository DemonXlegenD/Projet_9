using Newtonsoft.Json;
using Projet_9.Save;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSecurity
{
    public class UserManager
    {
        private static UserManager Instance;

        private List<User> users = new List<User>();

        public User ActualUser { get; set; } = new User();

        public static UserManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserManager();
                Instance.LoadUsersFromFile(SaveUser.GetInstance().GetFilePath());
            }
            return Instance;
        }

        public void AddUser(string userName, string password)
        {
            User user = User.NewUser(userName, password);
            if (!users.Contains(user))
            {
                foreach (var _user in users)
                {
                    _user.IsConnected = false;
                }
                users.Add(user);
                ActualUser = user;
                Console.WriteLine("User ajouté");
            }
            else
            {
                Console.WriteLine("User déjà ajouté");
            }

        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
            if (user == ActualUser)
            {
                ActualUser = null;
            }
        }

        public void CheckConnexion()
        {
            User user = users.Find(u => u.IsConnected == true);
            if (user != null)
            {
                ActualUser = user;
            }
        }

        public void CheckConnexion(string username, string motDePasse)
        {
            User user = users.Find(u => u.Username == username);

            if (user != null)
            {
                string motDePasseHacheAVerifier = Security.HacherMotDePasse(motDePasse, user.Sel);
                if (motDePasseHacheAVerifier == user.Password)
                {
                    ActualUser = user;
                }
            }
        }

        public void LoadUsersFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"Path trouvé : {filePath}");
                string json = File.ReadAllText(filePath);
                users = JsonConvert.DeserializeObject<List<User>>(json);
                if(users == null)
                {
                    users = new List<User>();
                }
            }
            else
            {
                Console.WriteLine($"Path non trouvé : {filePath}");
            }
        }

        public void UnloadUsersFromFile()
        {
            users.Clear();
        }

        public void SaveUserIntoFile(string filePath)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
