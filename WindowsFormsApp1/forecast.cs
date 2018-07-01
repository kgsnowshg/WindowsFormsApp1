using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class forecast
    {

        public static string index(string number)
        {
            SQLiteConnection sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            string sql = "select * from stockdates where number='" + number + "' order by date DESC";
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_connect);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            int time = 0;
            string[][] rows = new string[100][];
            while (reader.Read())
            {

                string sql2 = "select * from stockdata where number='" + number + "' and date='" + reader.GetString(3) + "' order by date DESC";
                SQLiteCommand sqlite_cmd2 = new SQLiteCommand(sql2, sqlite_connect);
                SQLiteDataReader reader2 = sqlite_cmd2.ExecuteReader();
                string valuea = (Math.Round(Convert.ToDouble(reader.GetString(7)) - Convert.ToDouble(reader.GetString(8)), 2)).ToString();
                string valueb = (Math.Round(Convert.ToDouble(reader.GetString(9)) - Convert.ToDouble(reader.GetString(8)), 2)).ToString();
                string valuec = (Math.Round(Convert.ToDouble(reader.GetString(9)) - Convert.ToDouble(reader.GetString(7)), 2)).ToString();
                string valued = (Math.Round(Convert.ToDouble(reader.GetString(4)) - Convert.ToDouble(reader.GetString(8)), 2)).ToString();
                string valuee = (Math.Round(Convert.ToDouble(reader.GetString(4)) - Convert.ToDouble(reader.GetString(7)), 2)).ToString();
                string valuef = (Math.Round(Convert.ToDouble(reader.GetString(9)) - Convert.ToDouble(reader.GetString(4)), 2)).ToString();
                int yesno = 0;
                while (reader2.Read())
                {
                    time++;
                    yesno = 1;
                     rows[time] = new string[] { reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(8),
                      reader.GetString(9), reader.GetString(10), reader.GetString(11),reader2.GetString(4),reader2.GetString(6),reader2.GetString(8),
                       valuef,valuea,valueb,valuec,valued,valuee};
                    
                }
                if (yesno == 0)
                {
                    time++;
                    rows[time] = new string[] { reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(8),
                      reader.GetString(9), reader.GetString(10), reader.GetString(11),"0","0","0",valuea,valueb,valuec,valued,valuee};
                    
                }
            }
            for (int i=0;i<10;i++)
            {

            }
            return "0";
        }

        public static string readnext(string number,string date,string time)
        {


            return "0";
        }
    }
}
