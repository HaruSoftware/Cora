using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cora.Entities;

namespace Cora.Data
{
    public static class DataAccess
    {
        private const string DEFAULT_PATH = @"C:\Cora\Data\";

        private static List<Database> _allDatabases;
        public static void InitializeAll()
        {
            CreateDatabases();

            if (_allDatabases != null)
            {
                _allDatabases.ForEach(x => x.Initialize());
            }
        }

        public static Database Get(string name)
        {
            return _allDatabases.Where(x => x.Name == name).FirstOrDefault();
        }

        public static void DelayTask(Action task, int milliSeconds)
        {
            var app = Application.Current;

            if (app == null) return;

            var dispatcher = app.Dispatcher;

            Task.Run(async () =>
            {
                await Task.Delay(milliSeconds);

                if (dispatcher != null)
                {
                    dispatcher.Invoke(task);
                }
            });
        }

        private static void CreateDatabases()
        {
            if (_allDatabases == null) _allDatabases = new List<Database>();

            #region Users

            var usersTable = new Database()
            {
                Name = "Users",
                FolderPath = DEFAULT_PATH
            };

            usersTable.AddTable("Users", typeof(User),
            [
                new Column("Id", "INTEGER", true, false),
                new Column("InstanceId", "INTEGER", false, true),
                new Column("Username","TEXT"),
                new Column("FullName","TEXT"),
                new Column("Password","TEXT"),
                new Column("UserRole","INTEGER"),
                new Column("Permissions","TEXT")
            ]);

            _allDatabases.Add(usersTable);

            #endregion


        }
    }
}
