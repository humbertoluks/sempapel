using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "Andromeda", Password = "1234", Role = "employee"});
            users.Add(new User { Id = 1, Username = "Pegasus", Password = "1234", Role = "employee"});
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).First(); 
        }
    }
} 