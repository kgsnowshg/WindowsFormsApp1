using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    class selectsql
    {
        public static string index(string statea, string stateb, string statec, string stated, string statee, string statef, string stateg, string stateh, string statek, string statei, string statej, string sstatea, string sstateb)
        {
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            string count = selectsql.counts(statec, stated, statee, statef, stateh, statei, statej, statek);
            if (Convert.ToInt32(statea) > 0)
            {
                string sql = "select top 1 * from stockdata where number=1 order by date DESC";
                SQLiteDataReader dReader = sqlite_cmd.ExecuteReader();
            }
            else
            {
                string sql = "select * from stockdates where number=1 order by date DESC limit 1";
                sqlite_cmd = new SQLiteCommand(sql, sqlite_connect);
                SQLiteDataReader dReader = sqlite_cmd.ExecuteReader();
                dReader.Read();
                string date = dReader.GetString(3);
                string sql2 = "select * from stockdates where date='"+ date+"' order by date DESC";
                SQLiteCommand sqlite_cmd2 = new SQLiteCommand(sql2, sqlite_connect);
                SQLiteDataReader reader2 = sqlite_cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    string number= reader2.GetString(1);
                    string addresult = case2(number, sstateb, statec, stated, statee, statef, stateh, statei, statej, statek, count);
                    return addresult;
                }
            }
            return "0";
        }
        public static string case1(string number, string sstateb, string stateb, string stateg)
        {
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            string sqls1 = "select * from stockdata where number='" + number + "' order by date DESC limit '" + sstateb + "'";
            SQLiteCommand sqlite_cmds1 = new SQLiteCommand(sqls1, sqlite_connect);
            SQLiteDataReader readers1 = sqlite_cmds1.ExecuteReader();
            string[][] datas = new string[1000000][];
            int time = 0;
            int result = 0;
            while (readers1.Read())
            {
                datas[time] = new string[] { readers1.GetString(6), readers1.GetString(8).ToString()};
                if ((readers1.GetString(6) == "-"))
                {
                    datas[time][0] = "0";
                }
                if ((readers1.GetString(8).ToString() == "-"))
                {
                    datas[time][1] = "0";
                }
                if (time > 0)
                {
                    result = 0;
                    switch (stateg)
                    {
                        case "1":
                            if (Convert.ToDouble(stateb) == 1)
                            {
                                if (Convert.ToDouble(datas[time - 1][0]) >= Convert.ToDouble(datas[time][0]))
                                {
                                    result++;
                                }
                            }
                            if (Convert.ToDouble(stateb) == 2)
                            {
                                if (Convert.ToDouble(datas[time - 1][0]) <= Convert.ToDouble(datas[time][0]))
                                {
                                    result++;
                                }
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(stateb) == 1)
                            {
                                if (Convert.ToDouble(datas[time - 1][1]) >= Convert.ToDouble(datas[time][1]))
                                {
                                    result++;
                                }
                            }
                            if (Convert.ToDouble(stateb) == 2)
                            {
                                if (Convert.ToDouble(datas[time - 1][1]) <= Convert.ToDouble(datas[time][1]))
                                {
                                    result++;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                if ((result < 1) && time>0)
                {
                    break;
                }
                time++;
            }
            sqlite_connect.Close();
            if (result >0)
            {
                return number;
            }
            else
            {
                return "0";
            }
        }
        public static string case2(string number, string sstateb, string statec, string stated, string statee, string statef, string stateh, string statei, string statej, string statek, string count)
        {
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            string sql3 = "select * from stockdates where number='" + number + "' order by date DESC limit '" + sstateb + "'";
            SQLiteCommand sqlite_cmd3 = new SQLiteCommand(sql3, sqlite_connect);
            SQLiteDataReader reader3 = sqlite_cmd3.ExecuteReader();
            string[][] data = new string[1000000][];
            int result = 0;
            int time=0;
            while (reader3.Read())
            {
                data[time] = new string[] { reader3.GetString(7), reader3.GetString(8), reader3.GetString(9), reader3.GetString(10) };
                if ((reader3.GetString(7)=="-"))
                {
                    data[time][0] = "0";
                }
                if ((reader3.GetString(8) == "-"))
                {
                    data[time][1] = "0";
                }
                if ((reader3.GetString(9) == "-"))
                {
                    data[time][2] = "0";
                }
                if ((reader3.GetString(11) == "-"))
                {
                    data[time][3] = "0";
                }
                System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
                bool s1 = reg1.IsMatch(reader3.GetString(7));
                bool s2 = reg1.IsMatch(reader3.GetString(8));
                bool s3 = reg1.IsMatch(reader3.GetString(9));
                bool s4 = reg1.IsMatch(reader3.GetString(10));
                if (s1==true)
                {
                    data[time][0] = "0";
                }
                if (s2 == true)
                {
                    data[time][1] = "0";
                }
                if (s3 == true)
                {
                    data[time][2] = "0";
                }
                if (s4 == true)
                {
                    data[time][3] = "0";
                }

                if (time > 0)
                {
                    result = 0;
                    switch (statec)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time-1][3]) >= Convert.ToDouble(data[time][3]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time-1][3]) <= Convert.ToDouble(data[time][3]))
                            {
                                result++;
                            }
                            break;
                        default:

                            break;
                    }
                    switch (stated)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time-1][2]) >= Convert.ToDouble(data[time][2]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time-1][2]) <= Convert.ToDouble(data[time][2]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (statee)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time-1][0]) >= Convert.ToDouble(data[time][0]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time-1][0]) <= Convert.ToDouble(data[time][0]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (statef)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time-1][1]) >= Convert.ToDouble(data[time][1]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time-1][1]) <= Convert.ToDouble(data[time][1]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (stateh)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time - 1][1]) >= Convert.ToDouble(data[time][0]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time - 1][1]) <= Convert.ToDouble(data[time][0]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (statei)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time - 1][0]) <= Convert.ToDouble(data[time][1]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time - 1][0]) <= Convert.ToDouble(data[time][1]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (statej)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time - 1][0]) <= Convert.ToDouble(data[time][2]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time - 1][0]) <= Convert.ToDouble(data[time][2]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (statek)
                    {
                        case "1":
                            if (Convert.ToDouble(data[time - 1][1]) >= Convert.ToDouble(data[time][2]))
                            {
                                result++;
                            }
                            break;
                        case "2":
                            if (Convert.ToDouble(data[time - 1][1]) <= Convert.ToDouble(data[time][2]))
                            {
                                result++;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if ((result < Convert.ToInt32(count)) && (time>0))
                {
                    break;
                }
                time++;
            }
            sqlite_connect.Close();
            if (result == Convert.ToInt32(count))
            {
                return number;
            }
            else
            {
                return "0";
            }


        }
        public static string counts(string statec, string stated, string statee, string statef, string stateh, string statei, string statej, string statek)
        {
            int i = 0;
            if (Convert.ToInt32(statec) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(stated) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(statee) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(statef) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(stateh) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(statei) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(statej) > 0)
            {
                i++;
            }
            if (Convert.ToInt32(statek) > 0)
            {
                i++;
            }
            return i.ToString();
        }
    }
}
