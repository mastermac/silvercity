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
    public partial class Add_Stock : MetroForm
    {
        public Add_Stock()
        {
            InitializeComponent();
        }

        private void Add_Stock_Load(object sender, EventArgs e)
        {
            
        }
        private void numericUpDown8_Enter(object sender, EventArgs e)
        {
            numericUpDown8.Text = "" + Convert.ToDouble(numericUpDown9.Text) * Convert.ToDouble(numericUpDown6.Text);
            numericUpDown8.Select(0, numericUpDown8.Text.Length);
        }

        private void numericUpDown9_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown9.Select(0, numericUpDown9.Text.Length);

        }

        private void numericUpDown10_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown10.Select(0, numericUpDown10.Text.Length);

        }

        private void numericUpDown10_Enter(object sender, EventArgs e)
        {
            numericUpDown10.Select(0, numericUpDown10.Text.Length);
        }

        private void numericUpDown9_Enter(object sender, EventArgs e)
        {
            numericUpDown9.Select(0, numericUpDown9.Text.Length);
        }
        private void numericUpDown8_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown8.Select(0, numericUpDown8.Text.Length);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroTextBox3.Text = "";
            metroTextBox4.Text = "";
            metroLabel1.Text = "";
            metroButton2.Enabled = false;
            metroButton1.Focus();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroDateTime1.ResetText();
            numericUpDown2.Text = "0";
            numericUpDown3.Text = "0";
            numericUpDown4.Text = "0";
            numericUpDown5.Text = "0";
            numericUpDown6.Text = "0";
            numericUpDown7.Text = "0";
            
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            String q1 = "Select distinct stone from stone;";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            con1.Open();
            var dataReader1 = cmd1.ExecuteReader();
            int count = 0;
            metroComboBox2.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox2.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            metroComboBox2.Items.Insert(count, "OTHER");
            dataReader1.Close();

            String q2 = "Select distinct shape from Stone;";
            cmd1.CommandText = q2;

            dataReader1 = cmd1.ExecuteReader();

            count = 0;
            metroComboBox3.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox3.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            metroComboBox3.Items.Insert(count, "OTHER");
            dataReader1.Close();

            q2 = "Select distinct seller from stone;";
            cmd1.CommandText = q2;
            dataReader1 = cmd1.ExecuteReader();
            count = 0;
            metroComboBox6.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox6.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            metroComboBox6.Items.Insert(count, "OTHER");
            dataReader1.Close();
            con1.Close();
            


            metroTextBox4.Visible = false;
            metroTextBox6.Visible = false;
            metroTextBox9.Visible = false;
            metroTextBox4.Enabled = true;
            metroTextBox6.Enabled = true;
            metroTextBox9.Enabled = true;
            metroLabel7.Visible = false;
            metroLabel10.Visible = false;
            metroLabel16.Visible = false;

            metroTextBox3.Text = "";
            metroTextBox4.Text = "";
            metroTextBox6.Text = "";
            metroTextBox9.Text = "";
            metroTextBox20.Text = "";
            double temp = 0;
                MySqlConnection con2 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                con2.Open();
                q2 = "Select lot from stone;";
                MySqlCommand cmd2 = new MySqlCommand(q2, con2);
                var dataReader2 = cmd2.ExecuteReader();
                
                while(dataReader2.Read())
                    temp=Convert.ToDouble(dataReader2.GetValue(0)+"")+0;

            temp++;
            numericUpDown1.Text = ""+Convert.ToInt32(temp);
            this.Focus();
        }


        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox2.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox4.Visible = true;
                metroLabel7.Visible = true;
            }
            else
            {
                metroTextBox4.Visible = false;
                metroLabel7.Visible = false;
            }
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            numericUpDown7.Focus();
            if (Convert.ToInt32(metroComboBox2.SelectedIndex) == -1)
                MetroMessageBox.Show(this, "\n\n\nPlease Select a Stone", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                if (metroTextBox4.Visible && metroTextBox4.Text.ToString().Length <= 0)
                    MetroMessageBox.Show(this, "\n\n\nPlease Specify a Stone Name", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                else
                {
                    if (metroTextBox3.Text.ToString().Length <= 0)
                        MetroMessageBox.Show(this, "\n\n\nPlease Specify a Size", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    else
                    {
                        if (Convert.ToInt32(metroComboBox3.SelectedIndex) == -1)
                            MetroMessageBox.Show(this, "\n\n\nPlease Select a Shape", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        else
                        {
                            if (metroTextBox6.Visible && metroTextBox6.Text.ToString().Length <= 0)
                                MetroMessageBox.Show(this, "\n\n\nPlease Specify a Shape", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            else
                            {
                                if (Convert.ToInt32(metroComboBox4.SelectedIndex) == -1)
                                    MetroMessageBox.Show(this, "\n\n\nPlease Select a Unit", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                else
                                {
                                    if (Convert.ToDouble(numericUpDown6.Text) == 0 || Convert.ToDouble(numericUpDown7.Text)==0)
                                        MetroMessageBox.Show(this, "\n\n\nPlease Enter Appropriate Numeric Values", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    else
                                    {
                                        if (Convert.ToInt32(metroComboBox6.SelectedIndex) == -1)
                                            MetroMessageBox.Show(this, "\n\n\nPlease Select a Seller", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                        else
                                        {
                                            if (metroTextBox9.Visible && metroTextBox9.Text.ToString().Length <= 0)
                                                MetroMessageBox.Show(this, "\n\n\nPlease Specify a Seller", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                            else
                                            {
                                                int lot = Convert.ToInt32(numericUpDown1.Text);
                                                string date1 = metroDateTime1.Text;
                                                string dau = date1.Replace('-', '/');
                                                DateTime dvs = Convert.ToDateTime(dau);
                                                string stone = metroComboBox2.SelectedItem.ToString();
                                                if (stone.Equals("OTHER"))
                                                    stone = metroTextBox4.Text;
                                                string size = metroTextBox3.Text;
                                                string shape = metroComboBox3.SelectedItem.ToString();
                                                if (shape.Equals("OTHER"))
                                                    shape = metroTextBox6.Text;
                                                int pcs = Convert.ToInt32(numericUpDown2.Text);
                                                double quant = Convert.ToDouble(numericUpDown3.Text);
                                                String unit = metroComboBox4.SelectedItem.ToString();
                                                double cost = Convert.ToDouble(numericUpDown4.Text);
                                                double less = Convert.ToDouble(numericUpDown5.Text);
                                                double nr = Convert.ToDouble(numericUpDown6.Text);
                                                double amt = Convert.ToDouble(numericUpDown7.Text);
                                                string seller = metroComboBox6.SelectedItem.ToString();
                                                if (seller.Equals("OTHER"))
                                                    seller = metroTextBox9.Text;
                                                String specs = metroTextBox20.Text;
                    MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                    con.Open();
                    String Query = "insert into stone(lot, dop, stone, size, shape, seller, p_pcs, p_qty, p_unit, c_pcs, c_qty, c_unit, cost, less, nr, amt, cr_amt, specs) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p,@q,@r);";
                    MySqlCommand cmd = new MySqlCommand(Query, con);
                    //MessageBox.Show(Query);
                    cmd.Parameters.AddWithValue("@a", lot);
                    cmd.Parameters.AddWithValue("@b", dvs);
                    cmd.Parameters.AddWithValue("@c", stone);
                    cmd.Parameters.AddWithValue("@d", size);
                    cmd.Parameters.AddWithValue("@e", shape);
                    cmd.Parameters.AddWithValue("@f", seller);
                    cmd.Parameters.AddWithValue("@g", pcs);
                    cmd.Parameters.AddWithValue("@h", quant);
                    cmd.Parameters.AddWithValue("@i", unit);
                    cmd.Parameters.AddWithValue("@j", pcs);
                    cmd.Parameters.AddWithValue("@k", quant);
                    cmd.Parameters.AddWithValue("@l", unit);
                    cmd.Parameters.AddWithValue("@m", cost);
                    cmd.Parameters.AddWithValue("@n", less);
                    cmd.Parameters.AddWithValue("@o", nr);
                    cmd.Parameters.AddWithValue("@p", amt);
                    cmd.Parameters.AddWithValue("@q", amt);
                    cmd.Parameters.AddWithValue("@r", specs);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MetroMessageBox.Show(this, "\n\nData inserted successfully\nLot-No for this Stone is " + lot, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metroButton2.PerformClick();
                        Application.OpenForms["Home"].BringToFront();
                        Application.OpenForms["Add_Stock"].BringToFront();
                        this.Focus();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.OpenForms["Home"].BringToFront();
                        Application.OpenForms["Add_Stock"].BringToFront();
                        numericUpDown1.Select();
                        this.Focus();
                    }
                    con.Close();
                                            }
                                        }
                                    }
                                }                        
                            }
                        }
                    }
                }
            }
        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox3.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox6.Visible = true;
                metroLabel10.Visible = true;
            }
            else
            {
                metroTextBox6.Visible = false;
                metroLabel10.Visible = false;
            }

        }

        private void metroComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox6.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox9.Visible = true;
                metroLabel16.Visible = true;
            }
            else
            {
                metroTextBox9.Visible = false;
                metroLabel16.Visible = false;
            }

        }
        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            Application.OpenForms["Home"].BringToFront();
            Application.OpenForms["Home"].Focus();
            if (this.Text == "EDIT STOCK")
            {
                Application.OpenForms["all_invent"].BringToFront();
                Application.OpenForms["all_invent"].Focus();
            }
            this.Dispose();
        }
        private void numericUpDown3_Enter(object sender, EventArgs e)
        {
            numericUpDown3.Select(0, numericUpDown3.Text.Length);
        }

        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            numericUpDown2.Select(0, numericUpDown2.Text.Length);
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void numericUpDown4_Enter(object sender, EventArgs e)
        {
            numericUpDown4.Select(0, numericUpDown4.Text.Length);
        }

        private void numericUpDown5_Enter(object sender, EventArgs e)
        {
            numericUpDown5.Select(0, numericUpDown5.Text.Length);
        }

        private void numericUpDown6_Enter(object sender, EventArgs e)
        {
            double cost = Convert.ToDouble(numericUpDown4.Text);
            double less = Convert.ToDouble(numericUpDown5.Text);
            numericUpDown6.Text = "" + cost * (1 - (0.01 * less));
            numericUpDown6.Select(0, numericUpDown6.Text.Length);
            
        }

        private void numericUpDown7_Enter(object sender, EventArgs e)
        {
            double cost = Convert.ToDouble(numericUpDown4.Text);
            double less = Convert.ToDouble(numericUpDown5.Text);
            double quant = Convert.ToDouble(numericUpDown3.Text);
            numericUpDown6.Text = "" + cost * (1 - (0.01 * less));
            numericUpDown7.Text = "" + cost * (1 - (0.01 * less)) * quant;
            numericUpDown7.Select(0, numericUpDown7.Text.Length);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            numericUpDown1.ReadOnly = true;
            metroDateTime1.ResetText();
            numericUpDown2.Text = "0";
            numericUpDown3.Text = "0";
            numericUpDown4.Text = "0";
            numericUpDown5.Text = "0";
            numericUpDown6.Text = "0";
            numericUpDown7.Text = "0";
            numericUpDown8.Text = "0";
            numericUpDown9.Text = "0";
            numericUpDown10.Text = "0";

            numericUpDown1.Text = metroLabel22.Text;
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            String q1 = "Select distinct stone from stone;";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            con1.Open();
            var dataReader1 = cmd1.ExecuteReader();
            int count = 0;
            metroComboBox2.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox2.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            dataReader1.Close();

            String q2 = "Select distinct shape from Stone;";
            cmd1.CommandText = q2;

            dataReader1 = cmd1.ExecuteReader();

            count = 0;
            metroComboBox3.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox3.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            dataReader1.Close();

            q2 = "Select distinct seller from stone;";
            cmd1.CommandText = q2;
            dataReader1 = cmd1.ExecuteReader();
            count = 0;
            metroComboBox6.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox6.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            dataReader1.Close();
            con1.Close();



            metroTextBox4.Visible = false;
            metroTextBox6.Visible = false;
            metroTextBox9.Visible = false;
            metroTextBox4.Enabled = true;
            metroTextBox6.Enabled = true;
            metroTextBox9.Enabled = true;
            metroLabel7.Visible = false;
            metroLabel10.Visible = false;
            metroLabel16.Visible = false;

            metroTextBox3.Text = "";
            metroTextBox4.Text = "";
            metroTextBox6.Text = "";
            metroTextBox9.Text = "";
            metroTextBox20.Text = "";
            int temp = 0;
            MySqlConnection con2 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            con2.Open();
            q2 = "Select * from stone where lot='" + metroLabel22.Text.ToString() + "';";
            MySqlCommand cmd2 = new MySqlCommand(q2, con2);
            var dataReader2 = cmd2.ExecuteReader();

            while (dataReader2.Read())
            {
                numericUpDown1.Text = dataReader2.GetValue(0).ToString();
                metroDateTime1.Text = dataReader2.GetValue(1).ToString();
                metroComboBox2.SelectedItem = dataReader2.GetValue(2);
                metroTextBox3.Text = dataReader2.GetValue(3).ToString();
                metroComboBox3.SelectedItem = dataReader2.GetValue(4);
                metroComboBox6.SelectedItem = dataReader2.GetValue(5);
                numericUpDown2.Text = dataReader2.GetValue(6).ToString();
                numericUpDown3.Text = dataReader2.GetValue(7).ToString();
                metroLabel3.Text = dataReader2.GetValue(6).ToString();
                metroLabel4.Text = dataReader2.GetValue(7).ToString();
                metroComboBox4.SelectedItem = dataReader2.GetValue(8);
                metroLabel13.Text = dataReader2.GetValue(9).ToString();
                metroLabel14.Text = dataReader2.GetValue(10).ToString();
                numericUpDown4.Text = dataReader2.GetValue(12).ToString();
                numericUpDown5.Text = dataReader2.GetValue(13).ToString();
                numericUpDown6.Text = dataReader2.GetValue(14).ToString();
                numericUpDown7.Text = dataReader2.GetValue(15).ToString();
                metroTextBox20.Text = dataReader2.GetValue(17).ToString();
                numericUpDown9.Text = dataReader2.GetValue(10).ToString();
                numericUpDown10.Text = dataReader2.GetValue(9).ToString();
                numericUpDown8.Text = dataReader2.GetValue(16).ToString();

            }
            this.Focus();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            numericUpDown7.Focus();
            if (metroTextBox3.Text.ToString().Length <= 0)
                MetroMessageBox.Show(this, "\n\n\nPlease Specify a Size", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                if (Convert.ToDouble(numericUpDown6.Text) == 0 || Convert.ToDouble(numericUpDown7.Text) == 0)
                    MetroMessageBox.Show(this, "\n\n\nPlease Enter Appropriate Numeric Values", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                else
                {
                    if (metroTextBox9.Visible && metroTextBox9.Text.ToString().Length <= 0)
                        MetroMessageBox.Show(this, "\n\n\nPlease Specify a Seller", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    else
                    {

                        int lot = Convert.ToInt32(metroLabel22.Text.ToString());
                        string date1 = metroDateTime1.Text;
                        string dau = date1.Replace('-', '/');
                        DateTime dvs = Convert.ToDateTime(dau);
                        string stone = metroComboBox2.SelectedItem.ToString();
                        if (stone.Equals("OTHER"))
                            stone = metroTextBox4.Text;
                        string size = metroTextBox3.Text;
                        string shape = metroComboBox3.SelectedItem.ToString();
                        if (shape.Equals("OTHER"))
                            shape = metroTextBox6.Text;
                        int pcs = Convert.ToInt32(numericUpDown2.Text);
                        double quant = Convert.ToDouble(numericUpDown3.Text);
                        double c_quant = Convert.ToDouble(numericUpDown9.Text);
                        double c_pcs = Convert.ToDouble(numericUpDown10.Text);
                        double c_amt = Convert.ToDouble(numericUpDown8.Text);
                        String unit = metroComboBox4.SelectedItem.ToString();
                        double cost = Convert.ToDouble(numericUpDown4.Text);
                        double less = Convert.ToDouble(numericUpDown5.Text);
                        double nr = Convert.ToDouble(numericUpDown6.Text);
                        double amt = Convert.ToDouble(numericUpDown7.Text);
                        string seller = metroComboBox6.SelectedItem.ToString();
                        if (seller.Equals("OTHER"))
                            seller = metroTextBox9.Text;
                        String specs = metroTextBox20.Text;
                        if (quant < Convert.ToDouble(metroLabel14.Text.ToString()) || pcs < Convert.ToDouble(metroLabel13.Text.ToString()) || c_pcs > pcs || c_amt > amt || c_quant > quant)
                            MetroMessageBox.Show(this, "\n\n\nPlease Specify Proper values for Quantity and Pieces of Stone", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        else
                        {
                            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                            con.Open();
                            String Query = "update stone set dop=@b, stone=@c, size=@d, shape=@e, seller=@f, p_pcs=@g, p_qty=@h, p_unit=@i, c_pcs=@j, c_qty=@k, c_unit=@l, cost=@m, less=@n, nr=@o, amt=@p, cr_amt=@q, specs=@r, ec=ec+1 where lot='" + lot + "';";
                            MySqlCommand cmd = new MySqlCommand(Query, con);
                            cmd.Parameters.AddWithValue("@b", dvs);
                            cmd.Parameters.AddWithValue("@c", stone);
                            cmd.Parameters.AddWithValue("@d", size);
                            cmd.Parameters.AddWithValue("@e", shape);
                            cmd.Parameters.AddWithValue("@f", seller);
                            cmd.Parameters.AddWithValue("@g", pcs);
                            cmd.Parameters.AddWithValue("@h", quant);
                            cmd.Parameters.AddWithValue("@i", unit);
                            cmd.Parameters.AddWithValue("@j", c_pcs);
                            cmd.Parameters.AddWithValue("@k", c_quant);
                            cmd.Parameters.AddWithValue("@l", unit);
                            cmd.Parameters.AddWithValue("@m", cost);
                            cmd.Parameters.AddWithValue("@n", less);
                            cmd.Parameters.AddWithValue("@o", nr);
                            cmd.Parameters.AddWithValue("@p", amt);
                            cmd.Parameters.AddWithValue("@q",c_amt);
                            cmd.Parameters.AddWithValue("@r", specs);

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MetroMessageBox.Show(this, "\n\nData Updated successfully" + lot, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.OpenForms["Home"].BringToFront();
                                Application.OpenForms["all_invent"].BringToFront();
                                this.Dispose();
                            }
                            else
                            {
                                MetroMessageBox.Show(this, "\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.OpenForms["Home"].BringToFront();
                                Application.OpenForms["all_invent"].BringToFront();
                                Application.OpenForms["Add_Stock"].BringToFront();
                                numericUpDown1.Select();
                                this.Focus();
                            }
                            con.Close();
                        }
                    }
                }
            }
        }

        private void numericUpDown1_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void numericUpDown2_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown2.Select(0, numericUpDown2.Text.Length);
        }

        private void numericUpDown3_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown3.Select(0, numericUpDown3.Text.Length);
        }

        private void numericUpDown4_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown4.Select(0, numericUpDown4.Text.Length);
        }

        private void numericUpDown5_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown5.Select(0, numericUpDown5.Text.Length);
        }

        private void numericUpDown6_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown6.Select(0, numericUpDown6.Text.Length);
        }

        private void numericUpDown7_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown7.Select(0, numericUpDown7.Text.Length);
        }

        private void metroTextBox20_Enter(object sender, EventArgs e)
        {
            string stone = metroComboBox2.SelectedItem.ToString();
            if (stone.Equals("OTHER"))
                stone = metroTextBox4.Text;
            string size = metroTextBox3.Text;
            string shape = metroComboBox3.SelectedItem.ToString();
            if (shape.Equals("OTHER"))
                shape = metroTextBox6.Text;

            metroTextBox20.Text = stone + " " + size + "mm " + shape;
        }
                            }
                        }
