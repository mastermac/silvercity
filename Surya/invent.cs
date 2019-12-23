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
using System.IO;
using MySql.Data.MySqlClient;

namespace Surya
{
    public partial class invent : MetroForm
    {
        public invent()
        {
            InitializeComponent();
        }

        private void invent_Load(object sender, EventArgs e)
        {
            metroButton2.PerformClick();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            String dos = metroTextBox1.Text;
            String id = metroTextBox2.Text;
            String name = metroComboBox2.SelectedItem.ToString();
            double disc = Convert.ToDouble(metroTextBox6.Text.ToString());
            int days = Convert.ToInt32(metroTextBox7.Text.ToString());
            double brok = Convert.ToDouble(metroTextBox8.Text.ToString());


            String ss = "";
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            con1.Open();
            double dol = 0;
            String q1 = "Select dollar from glob;";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                dol = Convert.ToDouble(dataReader1.GetValue(0));
            }
            dataReader1.Close();   
            q1 = "Select wt, color, purity, shape, rap, rate, amt from invent where id='"+id+"' and status!='S'";
            cmd1.CommandText = q1;
            dataReader1 = cmd1.ExecuteReader();
            if (dataReader1.Read())
            {
                double wt = Convert.ToDouble(dataReader1.GetValue(0));
                String color = dataReader1.GetValue(1).ToString();
                String purity = dataReader1.GetValue(2).ToString();
                String shape = dataReader1.GetValue(3).ToString();
                int rap = Convert.ToInt32(dataReader1.GetValue(4));
                double cprate = Convert.ToDouble(dataReader1.GetValue(5));
                double cpamt = Convert.ToDouble(dataReader1.GetValue(6));

                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
                con.Open();
                String q = "Select Buyid from Buyer where name='"+name+"';";
                MySqlCommand cmd = new MySqlCommand(q, con);
                
                MySqlDataReader dataReader=cmd.ExecuteReader();
                
                while (dataReader.Read())
                {
                    MySqlConnection con2 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
                    con2.Open();
                
                    String d1 = dos.Replace('-', '/');
                    DateTime d = Convert.ToDateTime(d1);
                    int bid=Convert.ToInt32(dataReader.GetValue(0));
                    MySqlCommand cmd2 = new MySqlCommand("insert into sell(dos, wt, color, purity, shape, rap, disc, dollar, rate, amt, bid, br, netamt, cprate, cpamt, pl, pp, specs, due_days) values (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k, @l, @m, @n, @o, @p, @q, @r, @s);", con2);
                    cmd2.Parameters.AddWithValue("@a", d);
                    cmd2.Parameters.AddWithValue("@b", wt);
                    cmd2.Parameters.AddWithValue("@c", color);
                    cmd2.Parameters.AddWithValue("@d", purity);
                    cmd2.Parameters.AddWithValue("@e", shape);
                    cmd2.Parameters.AddWithValue("@f", rap);
                    cmd2.Parameters.AddWithValue("@g", disc);
                    disc = 1 - (0.01 * disc);
            
                    cmd2.Parameters.AddWithValue("@h", (dol+""));
                    double rate = rap * disc * dol;
                    double amt = rate*wt;
                    double ntamt = amt * (1 - (0.01 * brok));
                    double pl = ntamt - cpamt;
                    double pp = (pl / cpamt) * 100;
                    cmd2.Parameters.AddWithValue("@i", rate);
                    cmd2.Parameters.AddWithValue("@j", amt);
                    cmd2.Parameters.AddWithValue("@k", bid);
                    cmd2.Parameters.AddWithValue("@l", brok);
                    cmd2.Parameters.AddWithValue("@m", ntamt);
                    cmd2.Parameters.AddWithValue("@n", cprate);
                    cmd2.Parameters.AddWithValue("@o", cpamt);
                    cmd2.Parameters.AddWithValue("@p", pl);
                    cmd2.Parameters.AddWithValue("@q", pp);
                    cmd2.Parameters.AddWithValue("@r", (id+""));
                    cmd2.Parameters.AddWithValue("@s", days);

                    int result = cmd2.ExecuteNonQuery();
                    if (result > 0)
                    {
                        q1 = "Update Buyer set Total=Total+" + ntamt + " where Buyid='" + bid + "';";
                        cmd2.CommandText = q1;
                        cmd2.ExecuteNonQuery();

                        q1 = "Update Seller set Total=Total+" + amt + " where Selid='10001';";
                        cmd2.CommandText = q1;
                        cmd2.ExecuteNonQuery();

                        q1 = "Update invent set Status='S' where id='"+id+"';";
                        cmd2.CommandText = q1;
                        cmd2.ExecuteNonQuery();

                        MetroMessageBox.Show(this, "\n\nData inserted successfully\nUnique-ID of this inventory is " + ss, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metroButton2.PerformClick();
                        Application.OpenForms["Home"].BringToFront();
                        Application.OpenForms["invent"].BringToFront();
                        Close();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.OpenForms["Home"].BringToFront();
                        Application.OpenForms["invent"].BringToFront();
                    }
                    con2.Close();
                }
                dataReader.Close();
                con.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPLEASE ENTER DETAILS PROPERLY FIRST!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataReader1.Close();
            con1.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            con1.Open();

            String q2 = "Select distinct Name from Buyer order by Name;";
            MySqlCommand cmd1 = new MySqlCommand(q2, con1);
            var dataReader1 = cmd1.ExecuteReader();
            int count = 0;
            metroComboBox2.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox2.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            dataReader1.Close();
            con1.Close();


            metroTextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            metroTextBox6.Text = "";
            metroTextBox7.Text = "";
            metroTextBox8.Text = "";
            
        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            if (metroComboBox1.SelectedItem.ToString().Equals("Other"))
            {
                metroTextBox2.Visible = true;
                metroLabel3.Visible = true;
            }
            else
            {
                metroTextBox2.Visible = false;
                metroLabel3.Visible = false;
            }
            */
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
