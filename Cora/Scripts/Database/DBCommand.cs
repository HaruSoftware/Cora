using Cora.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cora.Data
{
    public static class DBCommand
    {
        public static SQLiteCommand Get(Type type, object value)
        {
            if (type == typeof(User)) return User((User)value);

            return null;
        }
        private static SQLiteCommand User(User user)
        {
            SQLiteCommand result = new SQLiteCommand();

            result.Parameters.AddWithValue("@InstanceId", user.InstanceId);
            result.Parameters.AddWithValue("@Username", user.Username);
            result.Parameters.AddWithValue("@Password", user.Password);
            result.Parameters.AddWithValue("@FullName", user.FullName);
            result.Parameters.AddWithValue("@UserRole", user.UserRole);
            result.Parameters.AddWithValue("@Permissions", JsonConvert.SerializeObject(user.Permissions));

            return result;
        }
    }
}