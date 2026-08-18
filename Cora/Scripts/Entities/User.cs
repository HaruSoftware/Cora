using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.Data;

namespace Cora.Entities
{
    public class User
    {
        public const long ADM_LOGIN_ID = 37852168;

        public long Id { get; set; }
        public long InstanceId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int UserRole { get; set; }
        public List<string> Permissions { get; set; }
        public int TotalPermissions
        {
            get
            {
                if (Permissions == null) return 0;
                return Permissions.Count;
            }
        }

        public string GetUserId => string.Format("ID: {0}", Id);
        public string GetUserRole
        {
            get
            {
                return UserRole switch
                {
                    0 => "Vendedor",
                    1 => "Administrador",
                    2 => "Master",
                    _ => "Vendedor"
                };

            }
        }

        public string GetPermissionsCount => Permissions?.Count.ToString("00");

        public string GetSales
        {
            get
            {
                return DataAccess.Get("Sales").GetCountData("Sales", new DBFilter(" AND SellerId = @SellerId", Id)).ToString("00");
            }
        }
        public string GetUserSecurityCode()
        {
            string prefix = "724328";

            string id = Id.ToString();

            if(id.Length == 1)
            {
                id = "0" + id;
            }

            return prefix + id;
        }

        public static User GetUser(long Id)
        {
            if (Id == ADM_LOGIN_ID)
            {
                return GetAdministrator();
            }

            var user = (User)DataAccess.Get("Users").GetData("Users", new DBFilter(" AND Id = @Id", Id));

            if (user == null) return GetDefaults();

            return user;
        }
        public static User GetDefaults()
        {
            User user = new User();

            user.Id = 999999;
            user.Username = "No user";
            user.Password = "";
            user.FullName = "No user";
            user.UserRole = 0;

            return user;
        }
        public static User GetAdministrator()
        {
            return new User()
            {
                Id = ADM_LOGIN_ID,
                Username = "Administrador",
                Password = InstanceManager.CurrentEnterprise.SecretKey,
                FullName = "Administrador",
                UserRole = 2,
                Permissions = []
            };
        }
        public static User GetFromSecurityCode(string code)
        {
            if (code.StartsWith("724328") == false)
            {
                return null;
            }

            code = code.Replace("724328", "");

            if (code.Contains('0'))
            {
                code = code.Replace("0", "");
            }

            return GetUser(Parsing.Long(code));
        }

    }
}
