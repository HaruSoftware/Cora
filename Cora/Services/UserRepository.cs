using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Cora.Models;

namespace Cora.Services
{
    public class UserRepository
    {
        private readonly string _filePath = "users.json";
        private List<User> _users;

        public UserRepository()
        {
            _users = LoadUsers();
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.Now;
            _users.Add(user);
            SaveUsers();
        }

        public User? AuthenticateUser(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }

        public List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public bool UpdateUser(User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.Username = updatedUser.Username;
                user.Password = updatedUser.Password;
                SaveUsers();
                return true;
            }
            return false;
        }

        public bool DeleteUser(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                SaveUsers();
                return true;
            }
            return false;
        }

        public bool UserExists(string username)
        {
            return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
