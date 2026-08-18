using Cora.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cora.Data
{
    public class Database
    {
        private ConcurrentQueue<Func<Task>> defaultQueue = new ConcurrentQueue<Func<Task>>();
        private SemaphoreSlim queueSemaphore = new SemaphoreSlim(1, 1);
        private bool isExecuting;

        public string Name { get; set; }
        public string FolderPath { get; set; }
        public string Path
        {
            get
            {
                return string.Format("{0}{1}.db",FolderPath,Name);
            }
        }
        private List<Table> allTables { get; set; }
        public void Initialize()
        {
            var connection = new SQLiteConnection($"Data Source={Path};Version=3;");

            connection.Open();

            foreach (var table in allTables)
            {
                var command = new SQLiteCommand(table.GetTable(),connection);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        #region C.R.U.D
        public void Command(params SQLiteCommand[] commands)
        {
            defaultQueue.Enqueue(() => proc_Command(commands));
            ProcessQueueAsync();
        }
        public void Command(params string[] commands)
        {
            defaultQueue.Enqueue(() => proc_Command(commands));
            ProcessQueueAsync();
        }
        public void Write(string tableName,params object[] values)
        {
            defaultQueue.Enqueue(() => proc_WriteAsync(tableName, values));
            ProcessQueueAsync();
        }
        public void Update(string tableName, params object[] values)
        {
            defaultQueue.Enqueue(() => proc_UpdateAsync(tableName, values));
            ProcessQueueAsync();
        }
        public void Delete(string tableName, params object[] values)
        {
            defaultQueue.Enqueue(() => proc_DeleteAsync(tableName, values));
            ProcessQueueAsync();
        }
        #endregion

        #region GET DATA
        public object GetData(string tableName,SQLiteConnection connection, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            if (connection == null)
            {
                throw new Exception("There's no connection to get data.");
            }

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    StringBuilder query = new StringBuilder();
                    query.Append($"SELECT * FROM {table.Name} WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {

                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        return DBMapper.Get(table.ObjectType, reader);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return null;
        }
        public object GetData(string tableName, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            SQLiteConnection connection = new SQLiteConnection($"Data Source={Path};Version=3;");

            try
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    StringBuilder query = new StringBuilder();
                    query.Append($"SELECT * FROM {table.Name} WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {

                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        return DBMapper.Get(table.ObjectType,reader,connection);
                    }

                    connection.Close();

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return null;
        }
        public async Task<object> GetDataAsync(string tableName, SQLiteConnection connection, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            if(connection == null)
            {
                throw new Exception("There's no connection to acess database.");
            }

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    StringBuilder query = new StringBuilder();
                    query.Append($"SELECT * FROM {table.Name} WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {

                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        return DBMapper.Get(table.ObjectType, reader);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return null;
        }
        public async Task<object> GetDataAsync(string tableName, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            SQLiteConnection connection = new SQLiteConnection($"Data Source={Path};Version=3;");

            try
            {
                await connection.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    StringBuilder query = new StringBuilder();
                    query.Append($"SELECT * FROM {table.Name} WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {

                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        return DBMapper.Get(table.ObjectType, reader, connection);
                    }

                    connection.Close();

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return null;
        }
        public List<object> GetDataList(string tableName, SQLiteConnection connection, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            if(connection == null)
            {
                throw new Exception("There's no connection to get data.");
            }

            List<object> result = new List<object>();
            StringBuilder query = new StringBuilder();

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    query.Append("SELECT * FROM " + table.Name + " WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {
                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    query.Append(" ORDER BY ROWID DESC");

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(DBMapper.Get(table.ObjectType, reader));
                    }
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<object> GetDataList(string tableName, params DBFilter[] filters)
        {

            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            List<object> result = new List<object>();
            StringBuilder query = new StringBuilder();

            SQLiteConnection connection = new SQLiteConnection($"Data Source={Path};Version=3;");

            try
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    query.Append("SELECT * FROM " + table.Name + " WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {
                        foreach(var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    query.Append(" ORDER BY ROWID DESC");

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(DBMapper.Get(table.ObjectType,reader,connection));
                    }
                    return result;
                }

            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        public async Task<List<object>> GetDataListAsync(string tableName, SQLiteConnection connection, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if(table == null)
            {
                throw new Exception("Database table not found.");
            }

            if(connection == null)
            {
                throw new Exception("There's no connection to get data.");
            }

            var result = new List<object>();
            var query = new StringBuilder();

            try
            {
                using (var cmd = new SQLiteCommand(connection))
                {
                    query.Append($"SELECT * FROM {table.Name} WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {
                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    query.Append($" ORDER BY ROWID DESC");

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query.ToString();

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        result.Add(DBMapper.Get(table.ObjectType, reader));
                    }

                    return result;
                }
            }
            catch
            {
                return new List<object>(0);
            }
            finally
            {
            }
        }
        public async Task<List<object>> GetDataListAsync(string tableName, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            var result = new List<object>();
            var query = new StringBuilder();

            try
            {
                using (var connection = new SQLiteConnection($"Data Source={Path};Version=3;"))
                {
                    await connection.OpenAsync();

                    using (var cmd = new SQLiteCommand(connection))
                    {
                        query.Append($"SELECT * FROM {table.Name} WHERE 1=1");

                        if (filters != null && filters.Length > 0)
                        {
                            foreach (var filter in filters)
                            {
                                query.Append(filter.Command);
                                cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                            }
                        }

                        query.Append($" ORDER BY ROWID DESC");

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = query.ToString();

                        SQLiteDataReader reader = cmd.ExecuteReader();

                        while (await reader.ReadAsync())
                        {
                            result.Add(DBMapper.Get(table.ObjectType,reader,connection));
                        }
                        connection.Close();
                        return result;
                    }
                }

            }
            catch(Exception)
            {
                throw;
            }
        }

        public int GetCountData(string tableName, params DBFilter[] filters)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }
            try
            {
                StringBuilder query = new StringBuilder();

                SQLiteConnection connection = new SQLiteConnection($"Data Source={Path};Version=3;");
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    query.Append($"SELECT COUNT(*) FROM {table.Name} WHERE 1=1");

                    if (filters != null && filters.Length > 0)
                    {

                        foreach (var filter in filters)
                        {
                            query.Append(filter.Command);
                            cmd.Parameters.AddWithValue(filter.GetKey(), filter.Value);
                        }
                    }

                    cmd.CommandText = query.ToString();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch(Exception)
            {
                throw;
            }

        }

        public async Task<List<object>> GetDataFromCommand(string tableName,string command)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            var result = new List<object>();
            var query = new StringBuilder();

            try
            {
                using (var connection = new SQLiteConnection($"Data Source={Path};Version=3;"))
                {
                    await connection.OpenAsync();

                    using (var cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = command;

                        SQLiteDataReader reader = cmd.ExecuteReader();

                        while (await reader.ReadAsync())
                        {
                            result.Add(DBMapper.Get(table.ObjectType, reader));
                        }
                        connection.Close();
                        return result;
                    }
                }

            }
            catch
            {
                return new List<object>(0);
            }
        }

        #endregion

        private async void ProcessQueueAsync()
        {

            if (isExecuting) return;
            // Evita que múltiplos métodos ProcessQueueAsync sejam executados simultaneamente
            if (queueSemaphore.CurrentCount == 0) return;

            await queueSemaphore.WaitAsync();  // Bloqueia para garantir que apenas um processo execute a fila
            try
            {
                while (defaultQueue.TryDequeue(out var action))
                {
                    isExecuting = true;
                    await action();
                }
            }
            finally
            {
                queueSemaphore.Release(); // Libera o bloqueio
                isExecuting = false;

                if(defaultQueue.IsEmpty == false)
                {
                    ProcessQueueAsync();
                }

            }
        }

        public void RequestFeedback(Action action)
        {
            defaultQueue.Enqueue(() => Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(action);
            }));

            ProcessQueueAsync();
        }

        private async Task proc_WriteAsync(string tableName, params object[] values)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            int attempts = 0;
            int maxAttempts = 15;

            while (attempts < maxAttempts)
            {
                attempts++;

                try
                {
                    using (var connection = new SQLiteConnection($"Data Source={Path};Version=3;"))
                    {
                        await connection.OpenAsync();

                        using (var command = new SQLiteCommand("PRAGMA journal_mode=WAL;", connection))
                        {
                            await command.ExecuteScalarAsync();
                        }

                        using (var transaction = connection.BeginTransaction())
                        {

                            for (int i = 0; i < values.Length; i++)
                            {
                                var value = values[i];

                                var command = DBCommand.Get(table.ObjectType, value);
                                command.Connection = connection;
                                command.Transaction = transaction;
                                command.CommandText = table.GetWrite();

                                await command.ExecuteNonQueryAsync();

                            }

                            transaction.Commit();
                        }
                        return;
                    }
                }
                catch (Exception)
                {
                    await Task.Delay(200);
                }
            }


        }
        private async Task proc_UpdateAsync(string tableName, params object[] values)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            int attempts = 0;
            int maxAttempts = 15;

            while (attempts < maxAttempts)
            {
                attempts++;
                try
                {
                    using (var connection = new SQLiteConnection($"Data Source={Path};Version=3;"))
                    {
                        await connection.OpenAsync();
                        using (var command = new SQLiteCommand("PRAGMA journal_mode=WAL;", connection))
                        {
                            await command.ExecuteScalarAsync();
                        }

                        using (var transaction = connection.BeginTransaction())
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                var value = values[i];

                                SQLiteCommand updateCommand = DBCommand.Get(table.ObjectType, value);

                                updateCommand.Connection = connection;
                                updateCommand.CommandText = table.GetUpdate();

                                await updateCommand.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                    }
                    return;
                }
                catch
                {
                    await Task.Delay(200);
                }
            }
        }
        private async Task proc_DeleteAsync(string tableName, params object[] values)
        {
            var table = GetTable(tableName);

            if (table == null)
            {
                throw new Exception("Database table not found.");
            }

            int attempts = 0;
            int maxAttempts = 15;

            while (attempts < maxAttempts)
            {
                attempts++;

                try
                {
                    using (var connection = new SQLiteConnection($"Data Source={Path};Version=3;"))
                    {
                        await connection.OpenAsync();
                        using (var command = new SQLiteCommand("PRAGMA journal_mode=WAL;", connection))
                        {
                            await command.ExecuteScalarAsync();
                        }

                        using (var transaction = connection.BeginTransaction())
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                var value = values[i];

                                SQLiteCommand deleteCommand = new SQLiteCommand(table.GetDelete(), connection);
                                deleteCommand.Parameters.AddWithValue("@value", $"{value}");

                                deleteCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }

                    }
                    return;
                }
                catch
                {
                    await Task.Delay(200);
                }
            }
        }
        private async Task proc_Command(params string[] values)
        {
            int attempts = 0;
            int maxAttempts = 15;

            while (attempts < maxAttempts)
            {
                attempts++;
                try
                {
                    using (var connection = new SQLiteConnection($"Data Source={Path};Version=3;"))
                    {
                        await connection.OpenAsync();
                        using (var command = new SQLiteCommand("PRAGMA journal_mode=WAL;", connection))
                        {
                            await command.ExecuteScalarAsync();
                        }

                        using (var transaction = connection.BeginTransaction())
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                var value = values[i];

                                using (SQLiteCommand cmdCommand = new SQLiteCommand(value, connection))
                                {
                                    await cmdCommand.ExecuteNonQueryAsync();
                                }
                            }

                            transaction.Commit();
                        }
                    }
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    await Task.Delay(200);
                }
            }
        }
        private async Task proc_Command(params SQLiteCommand[] commands)
        {
            int attempts = 0;
            int maxAttempts = 15;

            while (attempts++ < maxAttempts)
            {
                try
                {
                    using var connection = new SQLiteConnection($"Data Source={Path};Version=3;");
                    await connection.OpenAsync();

                    using (var pragma = new SQLiteCommand("PRAGMA journal_mode=WAL;", connection))
                        await pragma.ExecuteScalarAsync();

                    using var transaction = connection.BeginTransaction();

                    foreach (var cmd in commands)
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        await cmd.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    await Task.Delay(200);
                }
            }
        }

        public void AddTable(string tableName,Type type, params Column[] columns)
        {
            if (columns == null || columns.Length == 0) return;

            if (allTables == null) allTables = new List<Table>();

            allTables.Add(new Table(tableName,type,columns));
        }
        public Table GetTable(string name)
        {
            return allTables.FirstOrDefault(t => t.Name == name);
        }
    }
}
