using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
namespace WindowsFormsApp1
{
    class insertdata
    {
        public static string insertdata1(string result, string filenames)
        {
            string date = "20" + filenames;
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            result = result.Replace("\r", "");
            result = result.Replace("\\", "");
            result = result.Replace("font10", "");
            result = result.Replace("font", "");
            result = result.Replace("pre", "");
            result = result.Replace("'", "");
            result = result.Replace(">", "");
            result = result.Replace("<", "");
            result = result.Replace("/", "");
            result = result.Replace("size=1", "");
            string[] abc = result.Split(' ');
            string[] arr = new string[100000];
            string values = "";
            int x = 1;
            int start = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var transaction = sqlite_connect.BeginTransaction())
            {
                for (int i = 0; i < abc.Length; i++)
                {
                    string val = abc[i].Trim();
                    if (val == string.Empty) continue;

                    arr[x] = val;
                   
                    arr[x] = arr[x].Replace("\n", "");
                    arr[x] = arr[x].Replace("*", "");
                    arr[x] = arr[x].Replace("&amp;", " ");


                    if (arr[x] == "HKD")
                    {
                        if (x >= 3 + (start * 10))
                        {
                            for (int z = 3 + (start * 10); z < x; z++)
                            {
                                arr[2 + (start * 10)] = arr[2 + (start * 10)] + " " + arr[z];

                            }
                            x = 2 + (start * 10);
                        }
                    }

                    if (arr[x] == "暫停買賣")
                    {
                        for (int j = 1 + (start * 10); j < x; j++)
                        {
                            values = values + arr[j] + j;
                        }
                        string nulls = "0";
                        sqlite_cmd.CommandText = "INSERT INTO stockdates VALUES (null,'" + arr[10 * start + 1] + "',null,'" + date + "',null,null,null,null,null,null,null,null,null,null,null);";
                        sqlite_cmd.ExecuteNonQuery();//using behind every write cmd

                        start++;
                        x = (start * 10);
                    }
                    x++;

                    if (x > 10 + (start * 10))
                    {
                        for (int j = 1 + (start * 10); j <= 10 + (start * 10); j++)
                        {
                            values = values + arr[j] + j;

                        }

                        sqlite_cmd.CommandText = "INSERT INTO stockdates VALUES (null,'" + arr[10 * start + 1] + "','" + arr[10 * start + 2] + "','" + date + "'," +
                            "'" + arr[10 * start + 3] + "','" + arr[10 * start + 4] + "','" + arr[10 * start + 5] + "','" + arr[10 * start + 6] + "','" + arr[10 * start + 7] + "','" + arr[10 * start + 8] + "','" + arr[10 * start + 9] + "','" + arr[10 * start + 10] + "',null,null,null);";
                        sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                        start++;
                        x = 1 + (start * 10);
                    }

                }
                transaction.Commit();
            }
            sqlite_connect.Close();
            return values;
        }
        public static string dealdata1(string filenames, string date)
        {
            dates.createdatabasefile();
            string datess = "20" + date;
            string filenamess = "dayquot/" + filenames;
            SQLiteConnection con = new SQLiteConnection("Data source=db/myData.db");
            con.Open();
            string sql = "select * from stockdates where date='" + datess + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);

            SQLiteDataReader reader = cmd.ExecuteReader();
            int yesno = 0;
            while (reader.Read())
            {
                yesno = 1;
            }
            if (yesno == 0)
            {
                try
                {
                    StreamReader sr = new StreamReader(filenamess, Encoding.Default);

                    string resuult = sr.ReadToEnd();
                    resuult = resuult.Replace("\n", "");
                    resuult = resuult.Replace("\"", "<");
                    resuult = resuult.Replace("#", " ");
                    resuult = resuult.Replace("CAD", "HKD");
                    resuult = resuult.Replace("GBP", "HKD");
                    resuult = resuult.Replace("USD", "HKD");

                    string patterns = @"<a name = <quotations<>報價.*?3999";//匹配模式
                    Regex regexs = new Regex(patterns, RegexOptions.IgnoreCase);
                    MatchCollection matches2 = regexs.Matches(resuult);
                    StringBuilder sbs = new StringBuilder();//存放匹配结果
                    foreach (Match match in matches2)
                    {
                        string value = match.Value;
                        sbs.AppendLine(value);
                    }
                    patterns = @"1.*?3999";//匹配模式
                    regexs = new Regex(patterns, RegexOptions.IgnoreCase);
                    matches2 = regexs.Matches(sbs.ToString());
                    sbs = new StringBuilder();//存放匹配结果
                    foreach (Match match in matches2)
                    {
                        string value = match.Value;
                        sbs.AppendLine(value);
                    }
                    string results = sbs.ToString();
                    string values = insertdata1(results, date);

                    return values;
                }
                catch
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }



        public static string insertdata2(string result,string datess)
        {
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            result = result.Replace("\r", "");
            result = result.Replace("\\", "");
            result = result.Replace("font10", "");
            result = result.Replace("font", "");
            result = result.Replace("pre", "");
            result = result.Replace("'", "");
            result = result.Replace(">", "");
            result = result.Replace("<", "");
            result = result.Replace("/", "");
            result = result.Replace("size=1", "");
            string[] abc = result.Split(' ');
            string[] arr = new string[100000];
            string values = "";

            int x = 1;
            int start = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var transaction = sqlite_connect.BeginTransaction())
            {
                for (int i = 0; i < abc.Length; i++)
                {
                    string val = abc[i].Trim();
                    if (val == string.Empty) continue;

                    arr[x] = val;
                    arr[x] = arr[x].Replace(",", "");
                    arr[x] = arr[x].Replace("\n", "");
                    arr[x] = arr[x].Replace("\n", "");
                    arr[x] = arr[x].Replace("*", "");
                    arr[x] = arr[x].Replace("&amp;", " ");

                    System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
                    bool s = reg1.IsMatch(arr[x]);
                    if (s == true)
                    {
                        if (x >= 3 + (start * 10))
                        {
                            for (int z = (3 + (start * 10)); z <= x; z++)
                            {
                                arr[2 + (start * 10)] = arr[2 + (start * 10)] + " " + arr[z];

                            }

                            x = 2 + (start * 10);
                        }
                    }


                    x++;

                    if (x > 6 + (start * 10))
                    {
                        double puta = Convert.ToInt64(arr[3 + (start * 10)]);
                        double putb = Convert.ToInt64(arr[4 + (start * 10)]);
                        double putc = Convert.ToInt64(arr[5 + (start * 10)]);
                        double putd = Convert.ToInt64(arr[6 + (start * 10)]);
                        arr[7 + (start * 10)] = (Math.Round((puta * 100 / putc), 2)).ToString();
                        string puts = (Math.Round((putb * 100 / putd), 2)).ToString();
                        for (int j = 1 + (start * 10); j <= 8 + (start * 10); j++)
                        {
                            values = values + arr[j] + j;
                        }
                        sqlite_cmd.CommandText = "INSERT INTO stockdata VALUES (null,'" + arr[10 * start + 1] + "','" + arr[10 * start + 2] + "','" + datess + "','" + puta + "','" + putb + "','" + putc + "','" + putd + "','" + puts + "');";
                        sqlite_cmd.ExecuteNonQuery();//using behind every write cmd
                        start++;
                        x = 1 + (start * 10);
                    }

                }
                transaction.Commit();
            }
            sqlite_connect.Close();
            return values;
        }


        public static string dealdata2(string filenames, string date)
        {
            dates.createdatabasefile();
            string datess = "20" + date;
            string filenamess = "dayquot/"+filenames;
            SQLiteConnection con = new SQLiteConnection("Data source=db/myData.db");
            con.Open();
            string sql = "select * from stockdata where date='" + datess + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);

            SQLiteDataReader reader = cmd.ExecuteReader();
            int yesno = 0;
            while (reader.Read())
            {
                yesno = 1;
            }
            if (yesno == 0)
            {
                try
                {
                    StreamReader sr = new StreamReader(filenamess, Encoding.Default);

                    string resuult = sr.ReadToEnd();
                    resuult = resuult.Replace("\n", "");
                    resuult = resuult.Replace("\"", "<");

                    string patterns = @"<a name = <short_selling<>.*?6030 ";//匹配模式
                    Regex regexs = new Regex(patterns, RegexOptions.IgnoreCase);
                    MatchCollection matches2 = regexs.Matches(resuult);
                    StringBuilder sbs = new StringBuilder();//存放匹配结果
                    foreach (Match match in matches2)
                    {
                        string value = match.Value;
                        sbs.AppendLine(value);
                    }
                    patterns = @"1.*?6030 ";//匹配模式
                    regexs = new Regex(patterns, RegexOptions.IgnoreCase);
                    matches2 = regexs.Matches(sbs.ToString());
                    sbs = new StringBuilder();//存放匹配结果
                    foreach (Match match in matches2)
                    {
                        string value = match.Value;
                        sbs.AppendLine(value);
                    }
                    string result = sbs.ToString();
                    string values = insertdata2(result,datess);
                    return "0";
                }
                catch
                {
                    return "0";
                }

            }
            return "0";





        }
    }
}
