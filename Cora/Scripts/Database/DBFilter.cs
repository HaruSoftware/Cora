using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cora.Data
{
    public class DBFilter
    {
        public string Command { get; set; } 
        public object Value { get; set; }

        public DBFilter(string command, object value)
        {
            Command = command;
            Value = value;
        }
        public string GetKey()
        {
            return Regex.Match(Command, @"@\w+").Value;
        }
    }
    public class DBSpecialCommand
    {

    }
}
