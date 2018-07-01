using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
namespace WindowsFormsApp1
{
    class changesqltype
    {
        public static void sdouble()
        {
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var transaction = sqlite_connect.BeginTransaction())
            {
                sqlite_cmd.CommandText = "PRAGMA foreign_keys = 0;";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmd.CommandText = "CREATE TABLE sqlitestudio_temp_table AS SELECT *FROM stockdata; ";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmd.CommandText = "DROP TABLE stockdata;";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmd.CommandText = "CREATE TABLE stockdata (id INTEGER PRIMARY KEY AUTOINCREMENT,number TEXT,name TEXT,date TEXT,st TEXT,turnover TEXT,pst TEXT,pturn TEXT,pp DOUBLE); ";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmd.CommandText = "INSERT INTO stockdata (id,number,name,date,st,turnover,pst,pturn,pp) SELECT id,number,name,date,st,turnover,pst,pturn,pp FROM sqlitestudio_temp_table;";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmd.CommandText = "DROP TABLE sqlitestudio_temp_table;";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmd.CommandText = "PRAGMA foreign_keys = 1;";
                sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                transaction.Commit();
            }
            sqlite_connect.Close();
        }
        public static void stext()
        {
            SQLiteConnection sqlite_connects;
            SQLiteCommand sqlite_cmds;
            sqlite_connects = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connects.Open();// Open
            sqlite_cmds = sqlite_connects.CreateCommand();//create command
            var stopwatchs = new Stopwatch();
            stopwatchs.Start();
            using (var transactions = sqlite_connects.BeginTransaction())
            {
                sqlite_cmds.CommandText = "PRAGMA foreign_keys = 0;";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmds.CommandText = "CREATE TABLE sqlitestudio_temp_table AS SELECT *FROM stockdata; ";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmds.CommandText = "DROP TABLE stockdata;";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmds.CommandText = "CREATE TABLE stockdata (id INTEGER PRIMARY KEY AUTOINCREMENT,number TEXT,name TEXT,date TEXT,st TEXT,turnover TEXT,pst TEXT,pturn TEXT,pp TEXT); ";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmds.CommandText = "INSERT INTO stockdata (id,number,name,date,st,turnover,pst,pturn,pp) SELECT id,number,name,date,st,turnover,pst,pturn,pp FROM sqlitestudio_temp_table;";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmds.CommandText = "DROP TABLE sqlitestudio_temp_table;";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                sqlite_cmds.CommandText = "PRAGMA foreign_keys = 1;";
                sqlite_cmds.ExecuteNonQuery();//using behind every write cmd
                transactions.Commit();
            }
            sqlite_connects.Close();
        }
    }
}
