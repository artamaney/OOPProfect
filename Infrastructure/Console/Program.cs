using System;
using System.Data.SQLite;
using System.Data.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace Infrastructure
{
    public class DataBase
    {
        public static List<string> GetTimetableForGroup(string groupName)
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var dbName = string.Format(@"{0}\TimeTable.db", path);
            var connection = new SQLiteConnection(string.Format("Data Source={0};", dbName));
            connection.Open();
            var command = new SQLiteCommand(string.Format("SELECT timetable FROM TimeTable WHERE group_='{0}'", groupName), connection);
            var reader = command.ExecuteReader();
            var timetable = new List<string>();
            foreach (DbDataRecord record in reader)
            {
                timetable.Add(record["timetable"].ToString());
            }
            connection.Close();
            return timetable;
        }

        public static void AddValueToTable(string groupName, string dayOfWeek, string timeTable)
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var dbName = string.Format(@"{0}\TimeTable.db", path);
            var connection = new SQLiteConnection(string.Format("Data Source={0};", dbName));
            connection.Open();
            var command = new SQLiteCommand(string.Format("INSERT INTO TimeTable ('dayOfWeek', 'group_', 'timetable') VALUES ({0}, {1}, {2})",
                                            dayOfWeek, groupName, timeTable), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        
        public static void Main()
        {
            foreach (var timetable in GetTimetableForGroup("ФИИТ-201"))
            {
                Console.WriteLine(timetable + '\n');
            }

            AddValueToTable("ФИИТ-201", "Cуббота", "проверка");
            foreach (var timetable in GetTimetableForGroup("ФИИТ-201"))
            {
                Console.WriteLine(timetable + '\n');
            }
        }
    }
}