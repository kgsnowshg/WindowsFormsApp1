using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        float sel_w = Screen.PrimaryScreen.Bounds.Width;
        float sel_h = Screen.PrimaryScreen.Bounds.Height;
        public Form1()
        {
            InitializeComponent();
            float sizex = (1920 / sel_w);
            float sizey = (1024 / sel_h);
            tabControl1.Width = Convert.ToInt32(tabControl1.Width / sizex);
            tabControl1.Height = Convert.ToInt32(tabControl1.Height / sizey);

            foreach (Control c in tabPage1.Controls)
            {

                Single currentSize = c.Font.Size * (1024 / sel_h);

                
                    c.Width = Convert.ToInt32(c.Width / sizex);
                    c.Height = Convert.ToInt32(c.Height / sizey);
                
                c.Top = Convert.ToInt32(c.Top / sizey);
                c.Left = Convert.ToInt32(c.Left / sizex);
                c.Font = new Font("微軟正黑體", currentSize);
                
            }

            foreach (Control c in tabPage2.Controls)
            {

                Single currentSize = c.Font.Size * (1024 / sel_h);

                
                    c.Width = Convert.ToInt32(c.Width / sizex);
                    c.Height = Convert.ToInt32(c.Height / sizey);
                
                c.Top = Convert.ToInt32(c.Top / sizey);
                c.Left = Convert.ToInt32(c.Left / sizex);
                c.Font = new Font("微軟正黑體", currentSize);
            }

            foreach (Control c in tabPage3.Controls)
            {

                Single currentSize = c.Font.Size * (1024 / sel_h);

                if (c.Name == "button2")
                {
                    c.Width = Convert.ToInt32(c.Width * sizex);
                    c.Height = Convert.ToInt32(c.Height * sizey);
                }
                c.Top = Convert.ToInt32(c.Top / sizey);
                c.Left = Convert.ToInt32(c.Left / sizex);
                c.Font = new Font("微軟正黑體", currentSize);
            }

            foreach (Control c in tabPage4.Controls)
            {

                Single currentSize = c.Font.Size * (1024 / sel_h);

                if (c.Name == "button2")
                {
                    c.Width = Convert.ToInt32(c.Width * sizex);
                    c.Height = Convert.ToInt32(c.Height * sizey);
                }
                c.Top = Convert.ToInt32(c.Top / sizey);
                c.Left = Convert.ToInt32(c.Left / sizex);
                c.Font = new Font("微軟正黑體", currentSize);
            }

            foreach (Control c in tabPage5.Controls)
            {

                Single currentSize = c.Font.Size * (1024 / sel_h);

                if (c.Name == "button2")
                {
                    c.Width = Convert.ToInt32(c.Width * sizex);
                    c.Height = Convert.ToInt32(c.Height * sizey);
                }
                c.Top = Convert.ToInt32(c.Top / sizey);
                c.Left = Convert.ToInt32(c.Left / sizex);
                c.Font = new Font("微軟正黑體", currentSize);
            }

            foreach (Control c in tabPage6.Controls)
            {

                Single currentSize = c.Font.Size * (1024 / sel_h);

                if (c.Name == "button2")
                {
                    c.Width = Convert.ToInt32(c.Width * sizex);
                    c.Height = Convert.ToInt32(c.Height * sizey);
                }
                c.Top = Convert.ToInt32(c.Top / sizey);
                c.Left = Convert.ToInt32(c.Left / sizex);
                c.Font = new Font("微軟正黑體", currentSize);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否下載日報表", "提示", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                dates.createfile();
                for (int starta = 1; starta < 32; starta++)
                {
                    string result = dates.url(starta);
                    label2.Text = result.ToString();
                }
                string time = DateTime.Now.ToLocalTime().ToString();
                int date = Convert.ToInt32(DateTime.Parse(time).ToString("dd"));
                for (int starta = 1; starta <= date; starta++)
                {
                    string result = dates.url2(starta);
                    label2.Text = result.ToString();
                }

                label2.Text = "狀態:正常";
            }
            else
            {
                MessageBox.Show("否");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            string number = textBox2.Text;
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 23;
            string sql = "select * from stockdates where number='" + number + "' order by date DESC";
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_connect);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Name = "號碼";
            dataGridView1.Columns[1].Name = "名稱";
            dataGridView1.Columns[2].Name = "日期";
            dataGridView1.Columns[3].Name = "最高";
            dataGridView1.Columns[4].Name = "最低";
            dataGridView1.Columns[5].Name = "收市";
            dataGridView1.Columns[6].Name = "成交量";
            dataGridView1.Columns[7].Name = "成交額";
            dataGridView1.Columns[8].Name = "沽空量";
            dataGridView1.Columns[9].Name = "沽空額";
            dataGridView1.Columns[10].Name = "沽空(%)";
            dataGridView1.Columns[11].Name = "(收市-前收市)";
            dataGridView1.Columns[12].Name = "(最高-最低)";
            dataGridView1.Columns[13].Name = "(收市-最低)";
            dataGridView1.Columns[14].Name = "(收市-最高)";
            dataGridView1.Columns[15].Name = "(前收市-最低)";
            dataGridView1.Columns[16].Name = "(前收市-最高)";

            while (reader.Read())
            {

                string valuea = (Math.Round(Convert.ToDouble(reader.GetString(7)) - Convert.ToDouble(reader.GetString(8)), 2)).ToString();
                string valueb = (Math.Round(Convert.ToDouble(reader.GetString(9)) - Convert.ToDouble(reader.GetString(8)), 2)).ToString();
                string valuec = (Math.Round(Convert.ToDouble(reader.GetString(9)) - Convert.ToDouble(reader.GetString(7)), 2)).ToString();
                string valued = (Math.Round(Convert.ToDouble(reader.GetString(4)) - Convert.ToDouble(reader.GetString(8)), 2)).ToString();
                string valuee = (Math.Round(Convert.ToDouble(reader.GetString(4)) - Convert.ToDouble(reader.GetString(7)), 2)).ToString();
                string valuef = (Math.Round(Convert.ToDouble(reader.GetString(9)) - Convert.ToDouble(reader.GetString(4)), 2)).ToString();
                string forecast1 = (Math.Round(Convert.ToDouble(valuef) + Convert.ToDouble(valuea), 2)).ToString();
                string forecast2 = (Math.Round(Convert.ToDouble(valuef) + Convert.ToDouble(valueb) + Convert.ToDouble(valuee), 2)).ToString();
                string forecast3 = (Math.Round(Convert.ToDouble(valueb) + Convert.ToDouble(valued), 2)).ToString();
                string forecast4 = (Math.Round(Convert.ToDouble(valuef) + Convert.ToDouble(valuec), 2)).ToString();
                string result = (Math.Round(Convert.ToDouble(forecast2)) + Math.Round(Convert.ToDouble(forecast1))).ToString();
                string sql2 = "select * from stockdata where number='" + number + "' and date='" + reader.GetString(3) + "' order by date DESC";
                SQLiteCommand sqlite_cmd2 = new SQLiteCommand(sql2, sqlite_connect);
                SQLiteDataReader reader2 = sqlite_cmd2.ExecuteReader();
                int yesno = 0;
                while (reader2.Read())
                {
                    yesno = 1;
                    string[] rows = new string[] { reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(8),
                      reader.GetString(9), reader.GetString(10), reader.GetString(11),reader2.GetString(4),reader2.GetString(6),reader2.GetString(8),
                       valuef,valuea,valueb,valuec,valued,valuee,forecast1,forecast2,forecast3,forecast4};
                    dataGridView1.Rows.Add(rows);
                }
                if (yesno == 0)
                {
                    string[] rows = new string[] { reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(8),
                      reader.GetString(9), reader.GetString(10), reader.GetString(11),"0","0","0",valuef,valuea,valueb,valuec,valued,valuee,forecast1,forecast2,forecast3,forecast4};
                    dataGridView1.Rows.Add(rows);
                }


            }

            sqlite_connect.Close();// Open
        }

        private void button6_Click(object sender, EventArgs e)
        {

            SQLiteConnection sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            string number = textBox3.Text;
            dataGridView2.Rows.Clear();
            dataGridView2.ColumnCount = 20;
            forecast.index(number);
            string sql = "select * from stockdates where number='" + number + "' order by date DESC";
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_connect);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.Columns[0].Name = "號碼";
            dataGridView2.Columns[1].Name = "名稱";
            dataGridView2.Columns[2].Name = "日期";
            dataGridView2.Columns[3].Name = "最高";
            dataGridView2.Columns[4].Name = "最低";
            dataGridView2.Columns[5].Name = "收市";
            dataGridView2.Columns[6].Name = "成交量";
            dataGridView2.Columns[7].Name = "成交額";
            dataGridView2.Columns[8].Name = "沽空量";
            dataGridView2.Columns[9].Name = "沽空額";
            dataGridView2.Columns[10].Name = "沽空(%)";
            dataGridView2.Columns[11].Name = "(收市-前收市)";
            dataGridView2.Columns[12].Name = "(最高-最低)";
            dataGridView2.Columns[13].Name = "(收市-最低)";
            dataGridView2.Columns[14].Name = "(收市-最高)";
            dataGridView2.Columns[15].Name = "(前收市-最低)";
            dataGridView2.Columns[16].Name = "(前收市-最高)";

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
                    yesno = 1;
                    string[] rows = new string[] { reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(8),
                      reader.GetString(9), reader.GetString(10), reader.GetString(11),reader2.GetString(4),reader2.GetString(6),reader2.GetString(8),
                       valuef,valuea,valueb,valuec,valued,valuee};
                    dataGridView2.Rows.Add(rows);
                }
                if (yesno == 0)
                {
                    string[] rows = new string[] { reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(8),
                      reader.GetString(9), reader.GetString(10), reader.GetString(11),"0","0","0",valuea,valueb,valuec,valued,valuee};
                    dataGridView2.Rows.Add(rows);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState==CheckState.Checked)
            {
                textBox5.Visible = true;
                checkBox7.Visible = true;
                checkBox8.Visible = true;
                label6.Visible = true;
                label13.Visible = true;
                checkBox9.Visible = true;
                checkBox10.Visible = true;
                checkBox19.Visible = true;
                checkBox20.Visible = true;
                label12.Visible = true;
            }
            else
            {
                textBox5.Visible = false;
                checkBox7.Visible = false;
                checkBox8.Visible = false;
                label6.Visible = false;
                label13.Visible = false;
                checkBox9.Visible = false;
                checkBox10.Visible = false;
                checkBox19.Visible = false;
                checkBox20.Visible = false;
                label12.Visible = false;
            }
        }

        


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
            {
                checkBox11.Visible = true;
                checkBox12.Visible = true;
                label7.Visible = true;
            }
            else
            {
                checkBox11.Visible = false;
                checkBox12.Visible = false;
                label7.Visible = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                checkBox13.Visible = true;
                checkBox14.Visible = true;
                label8.Visible = true;
            }
            else
            {
                checkBox13.Visible = false;
                checkBox14.Visible = false;
                label8.Visible = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {
                checkBox15.Visible = true;
                checkBox16.Visible = true;
                label9.Visible = true;
            }
            else
            {
                checkBox15.Visible = false;
                checkBox16.Visible = false;
                label9.Visible = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.CheckState == CheckState.Checked)
            {
                checkBox17.Visible = true;
                checkBox18.Visible = true;
                label10.Visible = true;
            }
            else
            {
                checkBox17.Visible = false;
                checkBox18.Visible = false;
                label10.Visible = false;
            }
        }
        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.CheckState == CheckState.Checked)
            {
                checkBox21.Visible = true;
                checkBox22.Visible = true;
                label14.Visible = true;
                checkBox26.Checked = false;
                checkBox29.Checked = false;
            }
            else
            {
                checkBox21.Visible = false;
                checkBox22.Visible = false;
                label14.Visible = false;
            }
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.CheckState == CheckState.Checked)
            {
                checkBox24.Visible = true;
                checkBox25.Visible = true;
                label15.Visible = true;
                checkBox23.Checked = false;
                checkBox33.Checked = false;
            }
            else
            {
                checkBox24.Visible = false;
                checkBox25.Visible = false;
                label15.Visible = false;
            }
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox29.CheckState == CheckState.Checked)
            {
                checkBox27.Visible = true;
                checkBox28.Visible = true;
                label16.Visible = true;
                checkBox23.Checked = false;
                checkBox33.Checked = false;
            }
            else
            {
                checkBox27.Visible = false;
                checkBox28.Visible = false;
                label16.Visible = false;
            }
        }
        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox33.CheckState == CheckState.Checked)
            {
                checkBox2.Visible = true;
                checkBox32.Visible = true;
                label17.Visible = true;
                checkBox26.Checked = false;
                checkBox29.Checked = false;
            }
            else
            {
                checkBox2.Visible = false;
                checkBox32.Visible = false;
                label17.Visible = false;
            }
        }


        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox8.Checked = false;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBox7.Checked = false;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkBox10.Checked = false;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            checkBox9.Checked = false;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            checkBox12.Checked = false;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            checkBox11.Checked = false;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            checkBox14.Checked = false;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            checkBox13.Checked = false;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            checkBox16.Checked = false;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            checkBox15.Checked = false;
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            checkBox18.Checked = false;
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            checkBox17.Checked = false;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            checkBox20.Checked = false;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            checkBox19.Checked = false;
        }
        private void checkBox30_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox31.Checked = false;
        }

        private void checkBox31_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox30.Checked = false;
        }
        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            checkBox22.Checked = false;
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            checkBox21.Checked = false;
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            checkBox25.Checked = false;
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            checkBox24.Checked = false;
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            checkBox28.Checked = false;
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            checkBox27.Checked = false;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox32.Checked = false;
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            string statea = "0";
            string stateb = "0";
            string statec = "0";
            string stated = "0";
            string statee = "0";
            string statef = "0";
            string stateg = "0";
            string stateh = "0";
            string statei = "0";
            string statej = "0";
            string statek = "0";
            string sstatea = textBox5.Text;
            string sstateb = textBox4.Text;
            int checkvalue = 0;
            if (checkBox31.CheckState == CheckState.Checked)
            {
                checkvalue = 1125;
            }
                if (checkBox1.CheckState == CheckState.Checked)
            {
                if (checkBox7.CheckState == CheckState.Checked)
                {
                    statea = "1";
                }
                if (checkBox8.CheckState == CheckState.Checked)
                {
                    statea = "2";
                }
            }
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (checkBox9.CheckState == CheckState.Checked)
                {
                    stateb = "1";
                }
                if (checkBox10.CheckState == CheckState.Checked)
                {
                    stateb = "2";
                }
                if (checkBox19.CheckState == CheckState.Checked)
                {
                    stateg = "1";
                }
                if (checkBox20.CheckState == CheckState.Checked)
                {
                    stateg = "2";
                }
            }
            if (checkBox3.CheckState == CheckState.Checked)
            {
                if (checkBox11.CheckState == CheckState.Checked)
                {
                    statec = "1";
                }
                if (checkBox12.CheckState == CheckState.Checked)
                {
                    statec = "2";
                }
            }
            if (checkBox4.CheckState == CheckState.Checked)
            {
                if (checkBox13.CheckState == CheckState.Checked)
                {
                    stated = "1";
                }
                if (checkBox14.CheckState == CheckState.Checked)
                {
                    stated = "2";
                }
            }
            if (checkBox5.CheckState == CheckState.Checked)
            {
                if (checkBox15.CheckState == CheckState.Checked)
                {
                    statee = "1";
                }
                if (checkBox16.CheckState == CheckState.Checked)
                {
                    statee = "2";
                }
            }
            if (checkBox6.CheckState == CheckState.Checked)
            {
                if (checkBox17.CheckState == CheckState.Checked)
                {
                    statef = "1";
                }
                if (checkBox18.CheckState == CheckState.Checked)
                {
                    statef = "2";
                }
            }
            if (checkBox23.CheckState == CheckState.Checked)
            {
                if (checkBox17.CheckState == CheckState.Checked)
                {
                    stateh = "1";
                }
                if (checkBox18.CheckState == CheckState.Checked)
                {
                    stateh = "2";
                }
            }
            if (checkBox26.CheckState == CheckState.Checked)
            {
                if (checkBox17.CheckState == CheckState.Checked)
                {
                    statei = "1";
                }
                if (checkBox18.CheckState == CheckState.Checked)
                {
                    statei = "2";
                }
            }
            if (checkBox29.CheckState == CheckState.Checked)
            {
                if (checkBox17.CheckState == CheckState.Checked)
                {
                    statej = "1";
                }
                if (checkBox18.CheckState == CheckState.Checked)
                {
                    statej = "2";
                }
            }
            if (checkBox33.CheckState == CheckState.Checked)
            {
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    statek = "1";
                }
                if (checkBox32.CheckState == CheckState.Checked)
                {
                    statek = "2";
                }
            }
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            string count = selectsql.counts(statec, stated, statee, statef, stateh, statei, statej, statek);
            if (Convert.ToInt32(statea) > 0)
            {
                
                string sql = "select * from stockdata where number=1 order by date DESC limit 1";
                sqlite_cmd = new SQLiteCommand(sql, sqlite_connect);
                SQLiteDataReader dReader = sqlite_cmd.ExecuteReader();
                dReader.Read();
                string date = dReader.GetString(3);
                string sql2 = "select * from stockdata where st>0 and date='" + date + "' and pp>='" + sstatea + "' order by date limit '" + checkvalue + "',1125";
                if (Convert.ToInt32(statea) == 2)
                {
                    sql2 = "select * from stockdata where st>0 and date='" + date + "' and pp<='" + sstatea + "' order by date limit '" + checkvalue + "',1125";
                }
                SQLiteCommand sqlite_cmd2 = new SQLiteCommand(sql2, sqlite_connect);
                SQLiteDataReader reader2 = sqlite_cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    string number = reader2.GetString(1);
                    string addresults1 = selectsql.case1(number, sstateb, stateb, stateg);
                    if ((Convert.ToInt32(addresults1) > 0))
                    {
                        string addresult = selectsql.case2(number, sstateb, statec, stated, statee, statef, stateh, statei, statej, statek, count);
                        if ((Convert.ToInt32(addresult) > 0))
                        {
                            string gettext = textBox6.Text;
                            textBox6.Text = gettext + Environment.NewLine + addresult;
                        }
                    }


                }
            }
            else
            {
                string sql = "select * from stockdates where number=1 order by date DESC limit 1";
                sqlite_cmd = new SQLiteCommand(sql, sqlite_connect);
                SQLiteDataReader dReader = sqlite_cmd.ExecuteReader();
                dReader.Read();
                string date = dReader.GetString(3);
                string sql2 = "select * from stockdates where high>0 and date='" + date + "' order by date limit '"+checkvalue+"',1125";
                SQLiteCommand sqlite_cmd2 = new SQLiteCommand(sql2, sqlite_connect);
                SQLiteDataReader reader2 = sqlite_cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    string number = reader2.GetString(1);
                    string addresult = selectsql.case2(number, sstateb, statec, stated, statee, statef, stateh, statei, statej, statek, count);
                    if ((Convert.ToInt32(addresult)>0))
                    {
                        string gettext = textBox6.Text;
                        textBox6.Text = gettext + Environment.NewLine + addresult;
                    }
                }
            }

            sqlite_connect.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            string text = textBox7.Text;
            string day = "30";
            if (text=="")
            {
                text="700";
            }
            if (checkBox34.CheckState == CheckState.Checked)
            {
                day = "7";
            }
            SQLiteConnection sqlite_connect;
            SQLiteCommand sqlite_cmd;
            sqlite_connect = new SQLiteConnection("Data source=db/myData.db");
            sqlite_connect.Open();// Open
            sqlite_cmd = sqlite_connect.CreateCommand();//create command
            string sql2 = "select * from stockdates where number='" + text + "' order by date DESC limit '"+day+"' ";
            SQLiteCommand sqlite_cmd2 = new SQLiteCommand(sql2, sqlite_connect);
            SQLiteDataReader reader2 = sqlite_cmd2.ExecuteReader();
            string[][] data = new string[100][];
            int time = 0;
            double low = 10000;
            double turnlow = 100000000000000;
            while (reader2.Read())
            {
                double low2 = (Convert.ToDouble(reader2.GetString(8)));
                double turnlow2 = (Convert.ToDouble(reader2.GetString(10)));
                if (low2 <= low)
                {
                    low = low2;
                }
                if (turnlow2 <= turnlow)
                {
                    turnlow = turnlow2;
                }
                data[time] = new string[] { reader2.GetString(3), reader2.GetString(9).ToString(), reader2.GetString(7).ToString(), low2.ToString(), reader2.GetString(10).ToString() };
                time++; ;
            }
            Random random = new Random();


            this.chart2.Series[0].Name = "收市";
            this.chart2.Series[1].Name = "最高";
            this.chart2.Series[2].Name = "最低";
            this.chart2.Series[2].Color = Color.Green;

            this.chart1.Series[0].Name = "成交量";
            this.chart1.Series[1].Name = "沽空量";
            chart2.ChartAreas[0].AxisY.Minimum = low;
            chart1.ChartAreas[0].AxisY.Minimum = turnlow / 2;

            int a = (Convert.ToInt32(day))-1;
            for (int i = a; i >=0; i--)
            {
                this.chart2.Series[0].Points.AddXY(data[i][0], data[i][1]);
                this.chart2.Series[1].Points.AddXY(data[i][0], data[i][2]);
                this.chart2.Series[2].Points.AddXY(data[i][0], data[i][3]);

                this.chart1.Series[0].Points.AddXY(data[i][0], data[i][4]);
            }

            string sql3 = "select * from stockdata where number='" + text + "' order by date DESC limit '"+day+"' ";
            SQLiteCommand sqlite_cmd3 = new SQLiteCommand(sql3, sqlite_connect);
            SQLiteDataReader reader3 = sqlite_cmd3.ExecuteReader();
            string[][] datas = new string[100][];
            int times = 0;
            double pturnlow = 100000000000000;
            while (reader3.Read())
            {
                double pturnlow2 = (Convert.ToDouble(reader3.GetString(4)));
                if (pturnlow2 <= pturnlow)
                {
                    pturnlow = pturnlow2;
                }
                datas[times] = new string[] { reader3.GetString(4) };
                times++;
            }
            if (times > 1)
            {
                chart1.ChartAreas[0].AxisY.Minimum = pturnlow / 10;
                for (int i = a; i >= 0; i--)
                {
                    this.chart1.Series[1].Points.AddXY(data[i][0], datas[i][0]);
                }

            }
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            checkBox35.Checked = false;
        }

        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            checkBox34.Checked = false;
        }
    }
}
