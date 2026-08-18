using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cora.Data
{
    public class Table
    {
        public string Name { get; set; }

        public Type ObjectType { get; set; }

        private Column[] allColumns { get; set; }

        public Table()
        {

        }

        public Table(string name,Type type, Column[] columns)
        {
            Name = name;
            ObjectType = type;
            allColumns = columns;
        }

        public string GetWrite()
        {
            var result = new StringBuilder();
            result.Append("INSERT INTO ");
            result.Append(Name);
            result.Append(" (");

            for (int i = 0; i < allColumns.Length; i++)
            {
                var column = allColumns[i];

                if (column.IsPrimaryKey == true) continue;

                result.Append(column.Name);

                if (i == allColumns.Length - 1)
                {
                    result.Append(')');
                }
                else
                {
                    result.Append(", ");
                }
            }
            result.Append(" VALUES (");

            for (int e = 0; e < allColumns.Length; e++)
            {
                var column = allColumns[e];

                if (column.IsPrimaryKey == true) continue;

                result.Append("@"+ column.Name);

                if (e == allColumns.Length - 1)
                {
                    result.Append(')');
                }
                else
                {
                    result.Append(',');
                }
            }

            return result.ToString();
        }
        public string GetUpdate()
        {
            var result = new StringBuilder();
            result.Append("UPDATE ");
            result.Append(Name);
            result.Append(" SET ");

            var updateKey = allColumns.Where(x => x.UpdateKey == true).FirstOrDefault();

            for (int i = 0; i < allColumns.Length; i++)
            {
                var column = allColumns[i];

                if (column.IsPrimaryKey) continue;
                if (column.UpdateKey) continue;

                result.Append(column.Name + " = @" + column.Name);

                if (i != allColumns.Length - 1)
                {
                    result.Append(", ");
                }
            }

            result.Append(" WHERE " + updateKey.Name + " =@" + updateKey.Name);

            return result.ToString();
        }
        public string GetDelete()
        {
            var updateKey = allColumns.FirstOrDefault(x => x.UpdateKey == true);

            return string.Format($"DELETE FROM {Name} WHERE {updateKey.Name} = @value");
        }
        public string GetTable()
        {
            var result = new StringBuilder();
            result.Append("CREATE TABLE IF NOT EXISTS ");
            result.Append(Name);
            result.Append(" (");

            for(int i = 0; i < allColumns.Length; i++)
            {
                var column = allColumns[i];
                var hasPrimaryKey = result.ToString().Contains("AUTOINCREMENT");

                result.Append(column.Name);
                result.Append(" " + column.DataType.ToUpper());

                if(column.IsPrimaryKey == true && hasPrimaryKey == false)
                {
                    result.Append(" PRIMARY KEY AUTOINCREMENT");
                }

                if(i == allColumns.Length - 1)
                {
                    result.Append(')');
                }else{
                    result.Append(", ");
                }

            }

            return result.ToString();
        }
    }
    public struct Column
    {
        public string Name { get; }
        public string DataType { get; }
        public bool IsPrimaryKey { get; }
        public bool UpdateKey { get;}

        public Column(string name, string dataType)
        {
            Name = name;
            DataType = dataType;
            IsPrimaryKey = false;
            UpdateKey = false;
        }
        public Column(string name, string dataType, bool isPrimaryKey, bool updateKey)
        {
            Name = name;
            DataType = dataType;
            IsPrimaryKey = isPrimaryKey;
            UpdateKey = updateKey;
        }

    }
}
