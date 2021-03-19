using System;
using Troublemaker.Framework;

namespace Troublemaker.Editor.Settings
{
    public sealed class UserCollection
    {
        public static UserCollection Instance { get; } = new UserCollection();
        private StringArchive _archive;
        
        private readonly Map<UserPassword> _passwords = new Map<UserPassword>(StringComparer.InvariantCultureIgnoreCase);

        public void Register(String username, UserPassword userPassword)
        {
            _passwords.Add(username, userPassword);
            _archive.Write($"Troublemaker/Users/{username}", userPassword.Serialize(username));
        }

        public Boolean IsKnownUser(String username) => _passwords.ContainsKey(username);
        public Boolean IsValidUser(String username, String password) => _passwords.TryGetValue(username, out UserPassword up) && up.IsValid(password);

        public void Init(StringArchive archive)
        {
            _archive = archive;
            _passwords.Clear();

            Map<String> userData = _archive.TryReadDirectory("Troublemaker/Users/");
            
            foreach (String content in userData)
            {
                (String name, UserPassword password) = UserPassword.Deserialize(content);
                _passwords.Add(name, password);
            }
        }
    }
}