using Newtonsoft.Json;
using System;

namespace NSecurity
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] Sel { get; set; }

        public bool IsConnected { get; set; } = false;

        public string PlayerId { get; set; } = string.Empty;

        public static User NewUser(string username, string password)
        {
            Guid uniqueIdUser = Guid.NewGuid();
            Guid uniqueIdPlayer = Guid.NewGuid();
            User user = new User();
            user.Id = uniqueIdUser.ToString();
            user.Username = username;
            user.Sel = Security.GenererSel();
            user.Password = Security.HacherMotDePasse(password, user.Sel);
            user.PlayerId = uniqueIdPlayer.ToString();
            user.IsConnected = true;
            return user;
        }

        public static User GetActualUser()
        {
            UserManager userManager = UserManager.GetInstance();
            return userManager.ActualUser;
        }

        public static void LoadUser(string username, string password)
        {
            UserManager userManager = UserManager.GetInstance();
            userManager.CheckConnexion(username, password);
        }

        public void DisconnectedUser()
        {
            IsConnected = false;
            if (IsConnected)
            {
                IsConnected = false;
                Console.WriteLine("User est déconnecté");
            }
            else
            {
                Console.WriteLine("User est déjà déconnecté");
            }
        }
        public void DeleteUser(string username, string password)
        {
            UserManager userManager = UserManager.GetInstance();
            userManager.RemoveUser(this);
        }
    }

    public class UserJsonConverter : JsonConverter<User>
    {
        public override void WriteJson(JsonWriter writer, User value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("User");
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(User.Id));
            serializer.Serialize(writer, value.Id);

            writer.WritePropertyName(nameof(User.Username));
            serializer.Serialize(writer, value.Username);

            writer.WritePropertyName(nameof(User.Password));
            serializer.Serialize(writer, value.Password);

            writer.WritePropertyName(nameof(User.Sel));
            serializer.Serialize(writer, value.Sel);

            writer.WritePropertyName(nameof(User.PlayerId));
            serializer.Serialize(writer, value.PlayerId);

            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override User ReadJson(JsonReader reader, Type objectType, User existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            User user = new User();

            // Avancer vers le début de l'objet JSON
            reader.Read();

            // Lire les propriétés de l'objet JSON
            while (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = reader.Value.ToString();

                switch (propertyName)
                {
                    case "User":
                        // Avancer vers le début de l'objet user
                        reader.Read();
                        break;

                    case nameof(User.Id):
                        // Lire la valeur de la propriété Age
                        reader.Read();
                        user.Id = serializer.Deserialize<string>(reader);
                        break;

                    case nameof(User.Username):
                        // Lire la valeur de la propriété FirstName
                        reader.Read();
                        user.Username = serializer.Deserialize<string>(reader);
                        break;

                    case nameof(User.Password):
                        // Lire la valeur de la propriété LastName
                        reader.Read();
                        user.Password = serializer.Deserialize<string>(reader);
                        break;

                    case nameof(User.Sel):
                        // Lire la valeur de la propriété Age
                        reader.Read();
                        user.Sel = serializer.Deserialize<byte[]>(reader);
                        break;

                    case nameof(User.PlayerId):
                        // Lire la valeur de la propriété Age
                        reader.Read();
                        user.PlayerId = serializer.Deserialize<string>(reader);
                        break;

                    default:
                        // Ignorer les propriétés inconnues si nécessaire
                        reader.Read();
                        reader.Skip();
                        break;
                }
                // Passer à la propriété suivante
                reader.Read();
            }

            return user;
        }
    }
}