using Cora.Entities;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cora.Data
{
    public static class DBMapper
    {
        public static object Get(Type type, SQLiteDataReader reader, SQLiteConnection conn = null)
        {
            try
            {
                if (type == typeof(User)) return ToUser(reader);

                throw new NotImplementedException("This method is not implemented.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static User ToUser(SQLiteDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                InstanceId = reader.GetInt64(reader.GetOrdinal("InstanceId")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                UserRole = reader.GetInt32(reader.GetOrdinal("UserRole")),
                Permissions = JsonConvert.DeserializeObject<List<string>>(reader.GetString(reader.GetOrdinal("Permissions"))),
            };
        }
    }
}