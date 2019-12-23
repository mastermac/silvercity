using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using Excel = Microsoft.Office.Interop.Excel;

using MySql.Data.MySqlClient;
namespace Surya
{
    public partial class metal_invent : MetroForm
    {
        public metal_invent()
        {
            InitializeComponent();
        }

        private void Lmi_Load(object sender, EventArgs e)
        {
            metroButton1.PerformClick();
            metroButton4.PerformClick();
            metroDateTime1.Format = DateTimePickerFormat.Custom;
            metroDateTime1.CustomFormat = "yyyy-MM-dd";
            metroDateTime2.Format = DateTimePickerFormat.Custom;
            metroDateTime2.CustomFormat = "yyyy-MM-dd";

            metroDateTime3.Format = DateTimePickerFormat.Custom;
            metroDateTime3.CustomFormat = "yyyy-MM-dd";
            metroDateTime4.Format = DateTimePickerFormat.Custom;
            metroDateTime4.CustomFormat = "yyyy-MM-dd";


        }

        private DataSet GetDataSet(String q1)
        {
            MySqlConnection myConn = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            myConn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(q1, myConn);
            cmd.CommandType = CommandType.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            myConn.Close();
            return ds;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            String q1 = "Select * from metal;", s1 = "";
            DataSet ds = GetDataSet(q1);
            
            foreach (DataGridViewColumn col in metroGrid1.Columns)
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            int row = 0, count = 1,index=0;
            double qty = 0, amt=0;
            int g_c = 0, s_c = 0, b_c = 0;
            double g_q = 0, s_q = 0, b_q = 0;
            double g_a = 0, s_a = 0, b_a = 0;

            //while (dataReader1.Read())
                foreach (DataRow addressRow in ds.Tables[0].Rows)
            {
                if (index != 0)
                    index = this.metroGrid1.Rows.Count;
                index++;
                this.metroGrid1.Rows.Add();
                (metroGrid1.Rows[row].Cells[0]).Value = count;
                s1 = addressRow[1].ToString();
                DateTime d = Convert.ToDateTime(s1);
                (metroGrid1.Rows[row].Cells[1]).Value = d.ToShortDateString();
                (metroGrid1.Rows[row].Cells[2]).Value = addressRow[3];
                (metroGrid1.Rows[row].Cells[3]).Value = addressRow[2];
                if (addressRow[2].ToString().ToLower() == "gold")
                {
                    g_c++;
                    g_q += Convert.ToDouble(addressRow[4]);
                    g_a += Convert.ToDouble(addressRow[6]);
                }
                else if (addressRow[2].ToString().ToLower() == "silver")
                {
                    s_c++;
                    s_q += Convert.ToDouble(addressRow[4]);
                    s_a += Convert.ToDouble(addressRow[6]);
                }
                else if (addressRow[2].ToString().ToLower() == "brass")
                {
                    b_c++;
                    b_q += Convert.ToDouble(addressRow[4]);
                    b_a += Convert.ToDouble(addressRow[6]);
                }
            
                (metroGrid1.Rows[row].Cells[4]).Value = addressRow[7];
//                qty = qty + Convert.ToDouble(addressRow[4));
                (metroGrid1.Rows[row].Cells[5]).Value = addressRow[4];
                (metroGrid1.Rows[row].Cells[6]).Value = addressRow[5];
                (metroGrid1.Rows[row].Cells[7]).Value = addressRow[8];
//                amt = amt + Convert.ToDouble(addressRow[6));
                (metroGrid1.Rows[row].Cells[8]).Value = addressRow[6];
                (metroGrid1.Rows[row].Cells[9]).Value = addressRow[9];

                count++;
                row++;
            }
            metroTextBox2.Text = ""+s_q;
            metroTextBox12.Text = "" + g_q;
            metroTextBox13.Text = "" + b_q;
            
            if (s_q > 0)
                metroTextBox16.Text = "" + (s_a / s_q);
            else
                metroTextBox16.Text = "0";
            
            if (g_q > 0)
                metroTextBox15.Text = "" + (g_a / g_q);
            else
                metroTextBox15.Text = "0";// +(g_a / g_c);

            if (b_q > 0)
                metroTextBox14.Text = "" + (b_a / b_q);
            else
                metroTextBox14.Text = "0";// +(g_a / g_c);
            
            metroTextBox19.Text = "" + s_a;
            metroTextBox18.Text = "" + g_a;
            metroTextBox17.Text = "" + b_a;

            metroTextBox24.Text = "" + (Convert.ToDouble(metroTextBox2.Text.ToString()) - Convert.ToDouble(metroTextBox21.Text.ToString()));
            metroTextBox23.Text = "" + (Convert.ToDouble(metroTextBox12.Text.ToString()) - Convert.ToDouble(metroTextBox20.Text.ToString()));
            metroTextBox22.Text = "" + (Convert.ToDouble(metroTextBox13.Text.ToString()) - Convert.ToDouble(metroTextBox22.Text.ToString()));

            metroDateTime1.ResetText();
            metroDateTime2.ResetText();

            metroTextBox1.Text = "";
            metroTextBox4.Text = "";
            metroTextBox5.Text = "";
            metroTextBox6.Text = "";
            metroComboBox1.SelectedIndex = -1;
            metroComboBox2.SelectedIndex = -1;
            metroComboBox3.SelectedIndex = -1;
            metroComboBox4.SelectedIndex = -1;

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            metroGrid2.Rows.Clear();
            
            metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int sele = 0;
            String q1 = "Select * from metal_consume;";

            /*            if (metroLabel1.Text.ToString().Equals("all"))
                        {
                            q1 = "Select * from invent order by date_pur;";
                            sele = 1;
                        }
                        else if (metroLabel1.Text.ToString() == "available")
                        {
                            q1 = "Select * from invent as i where i.netAmt>(Select SUM(amt) from history where history.id=i.id group by history.id) or i.id not in (Select distinct history.id from history) order by date_in;";
                            sele = 2;
                        }
                        else if (metroLabel1.Text == "sold")
                        {
                            q1 = "Select * from invent as i where i.netAmt<=(Select SUM(amt) from history where history.id=i.id group by history.id) order by date_in;";
                            sele = 4;
                        }
                        */
            foreach (DataGridViewColumn col in metroGrid2.Columns)
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int row = 0;
            String s1 = "";
            int count = 1;
            double wt = 0;

            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            double g_q = 0, s_q = 0,b_q=0;
            int index = 0;
           
            while (dataReader1.Read())
            {
            //    if (index != 0)
              //      index = this.metroGrid2.Rows.Count;
                index++;
                this.metroGrid2.Rows.Add();
                (metroGrid2.Rows[row].Cells[0]).Value = count;
                s1 = dataReader1.GetValue(1).ToString();
                DateTime d = Convert.ToDateTime(s1);
                (metroGrid2.Rows[row].Cells[1]).Value = d.ToShortDateString();
                (metroGrid2.Rows[row].Cells[2]).Value = dataReader1.GetValue(3);
                (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(2);
                if (dataReader1.GetValue(2).ToString().ToLower() == "gold")
                    g_q += Convert.ToDouble(dataReader1.GetValue(4));
                else if (dataReader1.GetValue(2).ToString().ToLower() == "silver")
                    s_q += Convert.ToDouble(dataReader1.GetValue(4));
                else if (dataReader1.GetValue(2).ToString().ToLower() == "brass")
                    b_q += Convert.ToDouble(dataReader1.GetValue(4));
                
                (metroGrid2.Rows[row].Cells[4]).Value = dataReader1.GetValue(6);
                (metroGrid2.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                (metroGrid2.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                (metroGrid2.Rows[row].Cells[7]).Value = dataReader1.GetValue(7);
                count++;
                row++;
            }
            metroTextBox21.Text = "" + s_q;
            metroTextBox20.Text = "" + g_q;
            metroTextBox3.Text = "" + b_q;

            metroTextBox24.Text = "" + (Convert.ToDouble(metroTextBox2.Text.ToString()) - Convert.ToDouble(metroTextBox21.Text.ToString()));
            metroTextBox23.Text = "" + (Convert.ToDouble(metroTextBox12.Text.ToString()) - Convert.ToDouble(metroTextBox20.Text.ToString()));
            metroTextBox22.Text = "" + (Convert.ToDouble(metroTextBox13.Text.ToString()) - Convert.ToDouble(metroTextBox22.Text.ToString()));

            //metroTextBox8.Text = "" + qty;
            metroDateTime4.ResetText();
            metroDateTime3.ResetText();

            metroTextBox7.Text = "";
            metroTextBox9.Text = "";
            metroTextBox10.Text = "";
            metroTextBox11.Text = "";
            metroComboBox6.SelectedIndex = -1;
            metroComboBox7.SelectedIndex = -1;
            metroComboBox8.SelectedIndex = -1;
            metroComboBox5.SelectedIndex = -1;

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox3.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox4.SelectedIndex) != -1)
            {
                metroGrid1.Rows.Clear();
                metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                String q1 = "Select * from metal where ", s1 = "";
                int stt = 0;
                //String q1 = "Select specs, dos, wt, color, purity, shape, rap, disc, dollar, rate, amt, name, br, netamt, cprate, cpamt, pl, pp, due_days, date_clear from sell, buyer where buyid=bid and ";
                if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1)
                {
                    stt = 1;
                    if (metroComboBox1.SelectedItem.ToString().Equals("Name"))
                        q1 = q1 + "Name like '%" + metroTextBox1.Text + "%' ";
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Purity"))
                        q1 = q1 + "purity like '%" + metroTextBox1.Text + "%' ";
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Unit"))
                        q1 = q1 + "unit like '%" + metroTextBox1.Text + "%' ";
                }
                if (Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 && metroTextBox4.Text.Length > 0)
                {
                    String[] s11 = new String[100];
                    String s = metroTextBox4.Text.ToString();
                    s = s.Trim();
                    s = s.ToUpper();
                    s11 = s.Split(',');
                    int i = 0, c1 = 0;
                    String p = "";
                    for (i = 0; i < s11.Length; i++)
                    {
                        p = p + "'" + s11[i] + "',";
                        c1++;
                    }
                    String p1 = p.Substring(0, (p.Length - 1));
                    if (stt == 1)
                        q1 = q1 + metroComboBox2.SelectedItem.ToString() + " type in (" + p1 + ") ";
                    else
                    {
                        q1 = q1 + " type in (" + p1 + ") ";
                        stt = 1;
                    }

                }
                if (Convert.ToInt32(metroComboBox3.SelectedIndex) != -1)
                {
                    String d1 = metroDateTime1.Text;
                    String d2 = metroDateTime2.Text;
                    if (stt == 1)
                        q1 = q1 + metroComboBox3.SelectedItem.ToString() + " dat between '" + d1 + "' and '" + d2 + "' ";
                    else
                    {
                        q1 = q1 + " dat between '" + d1 + "' and '" + d2 + "' ";
                        stt = 1;
                    }
                }

                if (Convert.ToInt32(metroComboBox4.SelectedIndex) != -1 && metroTextBox5.Text.Length > 0 && metroTextBox6.Text.Length > 0)
                {
                    if (stt == 1)
                        q1 = q1 + metroComboBox4.SelectedItem.ToString() + " qty between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                    else
                    {
                        q1 = q1 + " qty between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                        stt = 1;
                    }
                }
                q1 = q1 + "order by dat;";

                foreach (DataGridViewColumn col in metroGrid1.Columns)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                int row = 0, count = 1, index = 0;
                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
                MySqlCommand cmd1 = new MySqlCommand(q1, con1);
                MySqlDataReader dataReader1;
                con1.Open();
                dataReader1 = cmd1.ExecuteReader();
                double qty = 0, amt = 0;
                int g_c = 0, s_c = 0, b_c = 0;
                double g_q = 0, s_q = 0, b_q = 0;
                double g_a = 0, s_a = 0, b_a = 0;

                while (dataReader1.Read())
                {
                    if (index != 0)
                        index = this.metroGrid1.Rows.Count;
                    index++;
                    this.metroGrid1.Rows.Add();
                    (metroGrid1.Rows[row].Cells[0]).Value = count;
                    s1 = dataReader1.GetValue(1).ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid1.Rows[row].Cells[1]).Value = d.ToShortDateString();
                    (metroGrid1.Rows[row].Cells[2]).Value = dataReader1.GetValue(3);
                    (metroGrid1.Rows[row].Cells[3]).Value = dataReader1.GetValue(2);
                    (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(7);
                    if (dataReader1.GetValue(2).ToString().ToLower() == "gold")
                    {
                        g_c++;
                        g_q += Convert.ToDouble(dataReader1.GetValue(4));
                        g_a += Convert.ToDouble(dataReader1.GetValue(6));
                    }
                    else if (dataReader1.GetValue(2).ToString().ToLower() == "silver")
                    {
                        s_c++;
                        s_q += Convert.ToDouble(dataReader1.GetValue(4));
                        s_a += Convert.ToDouble(dataReader1.GetValue(6));
                    }
                    else if (dataReader1.GetValue(2).ToString().ToLower() == "brass")
                    {
                        b_c++;
                        b_q += Convert.ToDouble(dataReader1.GetValue(4));
                        b_a += Convert.ToDouble(dataReader1.GetValue(6));
                    }
            
                    (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                    (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                    (metroGrid1.Rows[row].Cells[7]).Value = dataReader1.GetValue(8);
                    (metroGrid1.Rows[row].Cells[8]).Value = dataReader1.GetValue(6);
                    (metroGrid1.Rows[row].Cells[9]).Value = dataReader1.GetValue(9);
                    count++;
                    row++;
                }
                metroTextBox2.Text = "" + s_q;
                metroTextBox12.Text = "" + g_q;
                metroTextBox13.Text = "" + b_q;
                //MessageBox.Show("" + s_a / s_q);
                if (s_q > 0)
                    metroTextBox16.Text = "" + (s_a / s_q);
                else
                    metroTextBox16.Text = "0";

                if (g_q > 0)
                    metroTextBox15.Text = "" + (g_a / g_q);
                else
                    metroTextBox15.Text = "0";// +(g_a / g_c);

                if (b_q > 0)
                    metroTextBox14.Text = "" + (b_a / b_q);
                else
                    metroTextBox14.Text = "0";// +(g_a / g_c);

                metroTextBox19.Text = "" + s_a;
                metroTextBox18.Text = "" + g_a;
                metroTextBox17.Text = "" + b_a;

                metroTextBox24.Text = "" + (Convert.ToDouble(metroTextBox2.Text.ToString()) - Convert.ToDouble(metroTextBox21.Text.ToString()));
                metroTextBox23.Text = "" + (Convert.ToDouble(metroTextBox12.Text.ToString()) - Convert.ToDouble(metroTextBox20.Text.ToString()));
                metroTextBox22.Text = "" + (Convert.ToDouble(metroTextBox13.Text.ToString()) - Convert.ToDouble(metroTextBox22.Text.ToString()));

            }

        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            if (metroComboBox1.SelectedItem.ToString().Equals("Name"))
                metroTextBox1.PromptText = "Enter Name here..";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Purity"))
                metroTextBox1.PromptText = "Enter Purity here..";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Unit"))
                metroTextBox1.PromptText = "Enter a unit here..";
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(metroComboBox5.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox6.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox7.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox8.SelectedIndex) != -1)
            {
                metroGrid2.Rows.Clear();
                metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                String q1 = "Select * from metal_consume where ", s1 = "";
                int stt = 0;
                //String q1 = "Select specs, dos, wt, color, purity, shape, rap, disc, dollar, rate, amt, name, br, netamt, cprate, cpamt, pl, pp, due_days, date_clear from sell, buyer where buyid=bid and ";
                if (Convert.ToInt32(metroComboBox8.SelectedIndex) != -1)
                {
                    stt = 1;
                    if (metroComboBox8.SelectedItem.ToString().Equals("Name"))
                        q1 = q1 + "Name like '%" + metroTextBox11.Text + "%' ";
                    else if (metroComboBox8.SelectedItem.ToString().Equals("Purity"))
                        q1 = q1 + "purity like '%" + metroTextBox11.Text + "%' ";
                    else if (metroComboBox8.SelectedItem.ToString().Equals("Unit"))
                        q1 = q1 + "unit like '%" + metroTextBox11.Text + "%' ";
                }
                if (Convert.ToInt32(metroComboBox7.SelectedIndex) != -1 && metroTextBox10.Text.Length > 0)
                {
                    String[] s11 = new String[100];
                    String s = metroTextBox10.Text.ToString();
                    s = s.Trim();
                    s = s.ToUpper();
                    s11 = s.Split(',');
                    int i = 0, c1 = 0;
                    String p = "";
                    for (i = 0; i < s11.Length; i++)
                    {
                        p = p + "'" + s11[i] + "',";
                        c1++;
                    }
                    String p1 = p.Substring(0, (p.Length - 1));
                    if (stt == 1)
                        q1 = q1 + metroComboBox7.SelectedItem.ToString() + " type in (" + p1 + ") ";
                    else
                    {
                        q1 = q1 + " type in (" + p1 + ") ";
                        stt = 1;
                    }

                }
                if (Convert.ToInt32(metroComboBox6.SelectedIndex) != -1)
                {
                    String d1 = metroDateTime4.Text;
                    String d2 = metroDateTime3.Text;
                    if (stt == 1)
                        q1 = q1 + metroComboBox6.SelectedItem.ToString() + " dat between '" + d1 + "' and '" + d2 + "' ";
                    else
                    {
                        q1 = q1 + " dat between '" + d1 + "' and '" + d2 + "' ";
                        stt = 1;
                    }
                }

                if (Convert.ToInt32(metroComboBox5.SelectedIndex) != -1 && metroTextBox9.Text.Length > 0 && metroTextBox7.Text.Length > 0)
                {
                    if (stt == 1)
                        q1 = q1 + metroComboBox5.SelectedItem.ToString() + " qty between " + metroTextBox9.Text.ToString() + " and " + metroTextBox7.Text.ToString() + " ";
                    else
                    {
                        q1 = q1 + " qty between " + metroTextBox9.Text.ToString() + " and " + metroTextBox7.Text.ToString() + " ";
                        stt = 1;
                    }
                }
                q1 = q1 + "order by dat;";
                //MessageBox.Show(q1);
                foreach (DataGridViewColumn col in metroGrid2.Columns)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                int row = 0, count = 1, index = 0;
                double wt = 0;

                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
                MySqlCommand cmd1 = new MySqlCommand(q1, con1);
                MySqlDataReader dataReader1;
                con1.Open();
                dataReader1 = cmd1.ExecuteReader();
                double g_q=0,s_q=0,b_q=0;
                while (dataReader1.Read())
                {
                    if (index != 0)
                        index = this.metroGrid2.Rows.Count;
                    index++;
                    this.metroGrid2.Rows.Add();
                    (metroGrid2.Rows[row].Cells[0]).Value = count;
                    s1 = dataReader1.GetValue(1).ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid2.Rows[row].Cells[1]).Value = d.ToShortDateString();
                    (metroGrid2.Rows[row].Cells[2]).Value = dataReader1.GetValue(3);
                    (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(2);
                    if (dataReader1.GetValue(2).ToString().ToLower() == "gold")
                        g_q += Convert.ToDouble(dataReader1.GetValue(4));
                    else if (dataReader1.GetValue(2).ToString().ToLower() == "silver")
                        s_q += Convert.ToDouble(dataReader1.GetValue(4));
                    else if (dataReader1.GetValue(2).ToString().ToLower() == "brass")
                        b_q += Convert.ToDouble(dataReader1.GetValue(4));

                    (metroGrid2.Rows[row].Cells[4]).Value = dataReader1.GetValue(6);
                    (metroGrid2.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                    (metroGrid2.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                    (metroGrid2.Rows[row].Cells[7]).Value = dataReader1.GetValue(7);
                    count++;
                    row++;
                }
                metroTextBox21.Text = "" + s_q;
                metroTextBox20.Text = "" + g_q;
                metroTextBox3.Text = "" + b_q;

                metroTextBox24.Text = "" + (Convert.ToDouble(metroTextBox2.Text.ToString()) - Convert.ToDouble(metroTextBox21.Text.ToString()));
                metroTextBox23.Text = "" + (Convert.ToDouble(metroTextBox12.Text.ToString()) - Convert.ToDouble(metroTextBox20.Text.ToString()));
                metroTextBox22.Text = "" + (Convert.ToDouble(metroTextBox13.Text.ToString()) - Convert.ToDouble(metroTextBox22.Text.ToString()));

                //metroTextBox8.Text = "" + qty;
            }

        }

        private void metroComboBox8_SelectedValueChanged(object sender, EventArgs e)
        {
            metroTextBox11.Text = "";
            if (metroComboBox8.SelectedItem.ToString().Equals("Name"))
                metroTextBox11.PromptText = "Enter Name here..";
            else if (metroComboBox8.SelectedItem.ToString().Equals("Purity"))
                metroTextBox11.PromptText = "Enter Purity here..";
            else if (metroComboBox8.SelectedItem.ToString().Equals("Unit"))
                metroTextBox11.PromptText = "Enter a unit here..";
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            String path = @"C:\Silver City\Files\Metal Inventory.xls";
            String sel = metroLabel2.Text;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns[1].ColumnWidth = 5;
            xlWorkSheet.Columns[2].ColumnWidth = 11;
            xlWorkSheet.Columns[3].ColumnWidth = 20;
            xlWorkSheet.Columns[4].ColumnWidth = 8;
            xlWorkSheet.Columns[5].ColumnWidth = 7;
            xlWorkSheet.Columns[6].ColumnWidth = 8;
            xlWorkSheet.Columns[7].ColumnWidth = 6;
            xlWorkSheet.Columns[8].ColumnWidth = 10;
            xlWorkSheet.Columns[9].ColumnWidth = 11;
            xlWorkSheet.Columns[10].ColumnWidth = 9;
            xlWorkSheet.Columns[11].ColumnWidth = 1;
            xlWorkSheet.Columns[12].ColumnWidth = 1;
            xlWorkSheet.Columns[13].ColumnWidth = 5;
            xlWorkSheet.Columns[14].ColumnWidth = 11;
            xlWorkSheet.Columns[15].ColumnWidth = 15;
            xlWorkSheet.Columns[16].ColumnWidth = 8;
            xlWorkSheet.Columns[17].ColumnWidth = 6;
            xlWorkSheet.Columns[18].ColumnWidth = 10;
            xlWorkSheet.Columns[19].ColumnWidth = 9;
            xlWorkSheet.Columns[20].ColumnWidth = 9;

            int i = 0;
            int j = 0;
            xlWorkSheet.Cells[2, 4] = "Metal Purchase Info";
            xlWorkSheet.Cells[2, 16] = "Metal Consume Info";
            Excel.Range curcell = (Excel.Range)xlWorkSheet.Cells[2, 1];
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Size = 20;

            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlWorkSheet.Cells[4, 1] = "S.No";
            xlWorkSheet.Cells[4, 2] = "Date";
            curcell = (Excel.Range)xlWorkSheet.Cells[4, 1];
                
//            Excel.Range curcell = xlApp.ActiveCell;
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Color = Color.Red;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlWorkSheet.Cells[4, 3] = "Name";
            xlWorkSheet.Cells[4, 4] = "Metal";
            xlWorkSheet.Cells[4, 5] = "Purity";
            xlWorkSheet.Cells[4, 6] = "Quantity";
            xlWorkSheet.Cells[4, 7] = "Unit";
            xlWorkSheet.Cells[4, 8] = "Rate";
            xlWorkSheet.Cells[4, 9] = "Amount";
            xlWorkSheet.Cells[4, 10] = "Type";
            xlWorkSheet.Cells[4, 11] = "";
            xlWorkSheet.Cells[4, 12] = "";
            xlWorkSheet.Cells[4, 13] = "S.No";
            xlWorkSheet.Cells[4, 14] = "Date";
            xlWorkSheet.Cells[4, 15] = "Name";
            xlWorkSheet.Cells[4, 16] = "Metal";
            xlWorkSheet.Cells[4, 17] = "Purity";
            xlWorkSheet.Cells[4, 18] = "Used";
            xlWorkSheet.Cells[4, 19] = "Unit";
            xlWorkSheet.Cells[4, 20] = "Type";
            int mg1 = 0;
            for (i = 0; i <= metroGrid1.RowCount - 1; i++)
            {
                Excel.Range curcell2 = (Excel.Range)xlWorkSheet.Cells[i + 5, 1];
                curcell2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell2.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                
                for (j = 0; j <= metroGrid1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = metroGrid1[j, i];
                    xlWorkSheet.Cells[i + 5, j + 1] = cell.Value;
                }
                mg1++;
            }
            for (i = 0; i <= metroGrid2.RowCount - 1; i++)
            {
                Excel.Range curcell2 = (Excel.Range)xlWorkSheet.Cells[i + 5, 1];
                curcell2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell2.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                for (j = 0; j <= metroGrid2.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = metroGrid2[j, i];
                    xlWorkSheet.Cells[i + 5, j + 13] = cell.Value;
                }
            }
            curcell = (Excel.Range)xlWorkSheet.Cells[i+6, 1];

            //            Excel.Range curcell = xlApp.ActiveCell;
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Color = Color.Blue;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            xlWorkSheet.Cells[i + 6, 6] = "Total Qty";
            xlWorkSheet.Cells[i + 6, 8] = "Avg. Rate";
            xlWorkSheet.Cells[i + 6, 9] = "Total Amt";
            xlWorkSheet.Cells[i + 6, 18] = "Total Used";
            xlWorkSheet.Cells[i + 6, 19] = "Available";

            xlWorkSheet.Cells[i + 7, 6] = metroTextBox2.Text;
            xlWorkSheet.Cells[i + 7, 8] = metroTextBox16.Text;
            xlWorkSheet.Cells[i + 7, 9] = metroTextBox19.Text;
            xlWorkSheet.Cells[i + 7, 18] = metroTextBox21.Text;
            xlWorkSheet.Cells[i + 7, 19] = metroTextBox24.Text;

            xlWorkSheet.Cells[i + 8, 6] = metroTextBox12.Text;
            xlWorkSheet.Cells[i + 8, 8] = metroTextBox15.Text;
            xlWorkSheet.Cells[i + 8, 9] = metroTextBox18.Text;
            xlWorkSheet.Cells[i + 8, 18] = metroTextBox20.Text;
            xlWorkSheet.Cells[i + 8, 19] = metroTextBox23.Text;

            xlWorkSheet.Cells[i + 9, 6] = metroTextBox13.Text;
            xlWorkSheet.Cells[i + 9, 8] = metroTextBox14.Text;
            xlWorkSheet.Cells[i + 9, 9] = metroTextBox17.Text;
            xlWorkSheet.Cells[i + 9, 18] = metroTextBox3.Text;
            xlWorkSheet.Cells[i + 9, 19] = metroTextBox22.Text;

            curcell = (Excel.Range)xlWorkSheet.Cells[i + 7, 3];
            curcell.Font.Bold = true;
            curcell.Font.Color = Color.Green;
            xlWorkSheet.Cells[i + 7, 3] = "SILVER :";
            
            curcell = (Excel.Range)xlWorkSheet.Cells[i + 8, 3];
            curcell.Font.Bold = true;
            curcell.Font.Color = Color.Green;
            xlWorkSheet.Cells[i + 8, 3] = "GOLD :";
            
            curcell = (Excel.Range)xlWorkSheet.Cells[i + 9, 3];
            curcell.Font.Bold = true;
            curcell.Font.Color = Color.Green;
            xlWorkSheet.Cells[i + 9, 3] = "BRONZE :";
            
            
            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            Application.OpenForms["Home"].BringToFront();
            Close();
            System.Diagnostics.Process.Start(path);

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

        private void metroGrid2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            metroButton4.PerformClick();
        }


        private void metroGrid2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            foreach (DataGridViewRow row in metroGrid2.SelectedRows)
            {
                String dt = row.Cells[1].Value.ToString();
                String[] s11 = new String[3];
                dt = dt.Trim();
                s11 = dt.Split('-');
                Array.Reverse(s11);
                dt = String.Join("-", s11);
                MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                String query1 =  "delete from metal_consume where dat='" + dt + "' and name='" + row.Cells[2].Value.ToString() + "' and type='" + row.Cells[3].Value.ToString() + "' and qty='" + row.Cells[5].Value.ToString() + "' and purity='" + row.Cells[4].Value.ToString() + "' and prodtype='" + row.Cells[7].Value.ToString() + "';";
                MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                con2.Open();
                cmd2.ExecuteNonQuery();
            }
        }


        private void metroGrid1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            metroButton1.PerformClick();
        }

        private void metroGrid1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            foreach (DataGridViewRow row in metroGrid1.SelectedRows)
            {
                String dt = row.Cells[1].Value.ToString();
                String[] s11 = new String[3];
                dt = dt.Trim();
                s11 = dt.Split('-');
                Array.Reverse(s11);
                dt = String.Join("-", s11);
                MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                String query1 = "delete from metal where dat='" + dt + "' and name='" + row.Cells[2].Value.ToString() + "' and type='" + row.Cells[3].Value.ToString() + "' and qty='" + row.Cells[5].Value.ToString() + "' and purity='" + row.Cells[4].Value.ToString() + "' and prodtype='" + row.Cells[9].Value.ToString() + "';";
                MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                con2.Open();
                cmd2.ExecuteNonQuery();
            }
        }


    }
}
