using Projet_9.Save;
using System;
using System.Collections.Generic;

namespace NSecurity
{
    public class UserManager
    {
        private static UserManager Instance;

        private List<User> users = new List<User>();

        public User ActualUser { get; set; } = null;

        public static UserManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserManager();
                Instance.LoadUsers();
            }
            return Instance;
        }

        public void NewUser(string userName, string password, bool replace = true)
        {
            
            if (!this.CheckConnexion(userName, password))
            {
                User user = new User(userName, password);
                if (!users.Contains(user))
                {
                    foreach (var _user in users)
                    {
                        _user.IsConnected = false;
                    }
                    users.Add(user);
                    SaveUser.GetInstance().SaveUsersIntoFile(users);
                    if (replace) ActualUser = user;
                    Console.WriteLine("User ajouté");
                }
                else
                {
                    Console.WriteLine("User déjà ajouté");
                }
            }
        }

        public void AddUser(User user)
        {
            if (!users.Contains(user))
            {
                foreach (var _user in users)
                {
                    _user.IsConnected = false;
                }
                users.Add(user);
                Console.WriteLine("User ajouté");
            }
            else
            {
                Console.WriteLine("User déjà ajouté");
            }

        }
        public void DeleteUser()
        {
            this.RemoveUser(ActualUser);
        }

        public void RemoveUser(User user)
        {
            if (users.Contains(user))
            {
                users.Remove(user);
                if (user == ActualUser)
                {
                    ActualUser = null;
                }
            }
        }

        public bool CheckConnexion()
        {
            User user = users.Find(u => u.IsConnected == true);
            if (user != null)
            {
                ActualUser = user;
                return true;
            }
            return false;
        }

        public bool CheckConnexion(string username, string motDePasse)
        {
            List<User> _users = users.FindAll(u => u.Username == username);
            foreach (var _user in _users)
            {
                if (_user != null)
                {
                    string motDePasseHacheAVerifier = Security.HacherMotDePasse(motDePasse, _user.Sel);
                    if (motDePasseHacheAVerifier == _user.Password)
                    {
                        ActualUser = _user;
                        return true;
                    }
                }
            }
            return false;
        }

        public void LoadUsers()
        {
            users = SaveUser.GetInstance().LoadUserFromSaveFile();
            this.CheckConnexion();
        }

        public void UnloadUsers()
        {
            users.Clear();
        }
    }
}
