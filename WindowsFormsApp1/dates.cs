using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    class dates
    {
        public static int createdatabasefile()
        {
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            if (!Directory.Exists("db"))
            {
                Directory.CreateDirectory("db");
            }
            if (!File.Exists(Application.StartupPath + @"\db\myData.db"))
            {
                SQLiteConnection.CreateFile("db/myData.db");
            }
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            //建立資料庫連線

            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command

            sqlite_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS stockdates (id INTEGER PRIMARY KEY AUTOINCREMENT, number TEXT, name TEXT,
                                      date TEXT, prv TEXT, bid TEXT, ask TEXT, high TEXT, low TEXT, closing TEXT, st TEXT, 
                                      turnover TEXT, pst TEXT, pturn TEXT, pp TEXT)";
            //create table header
            //INTEGER PRIMARY KEY AUTOINCREMENT=>auto increase index
            sqlite_cmd.ExecuteNonQuery(); //using behind every write cmd
            sqlite_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS stockdata (id INTEGER PRIMARY KEY AUTOINCREMENT, number TEXT, name TEXT,
                                      date TEXT, st TEXT, turnover TEXT, pst TEXT, pturn TEXT, pp TEXT)";
            //create table header
            //INTEGER PRIMARY KEY AUTOINCREMENT=>auto increase index
            sqlite_cmd.ExecuteNonQuery(); //using behind every write cmd

            sqlite_connect.Close();
            return 0;
        }
        public static int createfile()
        {
            string str = System.AppDomain.CurrentDomain.BaseDirectory;
            str = str + "dayquot";
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            return 0;
        }
        public static string url(int x)
        {
            string dates=x.ToString();
            DateTime dt = DateTime.Now;
            int year = Convert.ToInt32(dt.Year.ToString())-2000;
            int montha = Convert.ToInt32(dt.Month.ToString()) - 1;
            string month = montha.ToString();
            if (montha < 10)
            {
                month = "0" + montha.ToString();
            }
            if (x<10)
            {
                dates = "0" + x.ToString();
            }
            int date = Convert.ToInt32(year.ToString()+ month.ToString()+ dates.ToString());
            string result = "狀態:正在下載" + date.ToString() + "日報表";
            download(year.ToString(), month.ToString(), dates.ToString());
            return result;
        }
        public static string url2(int x)
        {
            string dates = x.ToString();
            DateTime dt = DateTime.Now;
            int year = Convert.ToInt32(dt.Year.ToString())-2000;
            int montha = Convert.ToInt32(dt.Month.ToString());
            string month = montha.ToString();
            if (montha < 10)
            {
                month = "0" + montha.ToString();
            }
            if (x < 10)
            {
                dates = "0" + x.ToString();
            }
            int date = Convert.ToInt32(year.ToString() + month.ToString() + dates.ToString());
            string result = "狀態:正在下載" + date.ToString() + "日報表";
            download(year.ToString(), month.ToString(), dates.ToString());
            return result;
        }

        public static string download(string yy, string mm, string dd)
        {
            string url = "https://www.hkex.com.hk/chi/stat/smstat/dayquot/d";
            url = url + yy + mm + dd + "c.htm";
            string filenames = "d" + yy + mm + dd + "c.htm";
            try
            {
                string str = System.AppDomain.CurrentDomain.BaseDirectory;
                str = str + "dayquot\\d" + yy + mm + dd + "c.htm";
                string date = yy + mm + dd;
                string filename = System.AppDomain.CurrentDomain.BaseDirectory + "dayquot\\d" + yy + mm + dd + "c.htm";
                if (!File.Exists(filename))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                    System.IO.Stream dataStream = httpResponse.GetResponseStream();
                    byte[] buffer = new byte[8192];
                
                    FileStream fs = new FileStream(str,
                    FileMode.Create, FileAccess.Write);
                    int size = 0;
                    do
                    {
                        size = dataStream.Read(buffer, 0, buffer.Length);
                        if (size > 0)
                            fs.Write(buffer, 0, size);
                    } while (size > 0);
                    fs.Close();

                    httpResponse.Close();
                }
                insertdata.dealdata1(filenames,date);
                insertdata.dealdata2(filenames, date);
            }
            catch (WebException)
            {

            }
            return url;
        }
    }
}