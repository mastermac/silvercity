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
using MySql.Data.MySqlClient;

namespace Surya
{
    public partial class Metal_Purchase : MetroForm
    {
        public Metal_Purchase()
        {
            InitializeComponent();
        }

        private void Metal_Purchase_Load(object sender, EventArgs e)
        {
            metroButton2.PerformClick();
        }
        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox2.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox3.Visible = true;
                metroLabel4.Visible = true;
            }
            else
            {
                metroTextBox3.Visible = false;
                metroLabel4.Visible = false;
            }
        }

        
        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroDateTime1.ResetText();
            
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;");
            String q1 = "Select distinct type from metal;";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            con1.Open();
            var dataReader1 = cmd1.ExecuteReader();
            int count = 0;
            metroComboBox1.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox1.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            metroComboBox1.Items.Insert(count, "OTHER");
            dataReader1.Close();

            String q2 = "Select distinct name from metal";
            cmd1.CommandText = q2;

            dataReader1 = cmd1.ExecuteReader();

            count = 0;
            metroComboBox2.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox2.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            metroComboBox2.Items.Insert(count, "OTHER");
            dataReader1.Close();

            
            q2 = "Select distinct purity from metal;";
            cmd1.CommandText = q2;
            dataReader1 = cmd1.ExecuteReader();
            count = 0;
            metroComboBox4.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox4.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            metroComboBox4.Items.Insert(count, "OTHER");
            con1.Close();
            
            metroTextBox2.Visible = false;
            metroTextBox3.Visible = false;
            metroTextBox6.Visible = false;
            metroTextBox2.Enabled = true;
            metroTextBox3.Enabled = true;
            metroTextBox6.Enabled = true;
            metroLabel1.Visible = false;
            metroLabel4.Visible = false;
            metroLabel9.Visible = false;

            metroTextBox2.Text = "";
            metroTextBox3.Text = "";
            metroTextBox5.Text = "";
            metroTextBox6.Text = "";
            metroTextBox7.Text = "";
            metroTextBox8.Text = "";
            this.Focus();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox2.Visible = true;
                metroLabel1.Visible = true;
            }
            else
            {
                metroTextBox2.Visible = false;
                metroLabel1.Visible = false;
            }
            if (metroComboBox1.SelectedItem.ToString().Equals("Gold"))
                metroComboBox3.SelectedItem = "Grm";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Silver"))
                metroComboBox3.SelectedItem = "Kg";
            

        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void metroComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox4.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox6.Visible = true;
                metroLabel9.Visible = true;
            }
            else
            {
                metroTextBox6.Visible = false;
                metroLabel9.Visible = false;
            }
        }

        private void metroTextBox8_Enter(object sender, EventArgs e)
        {
            double r1;double r2;
            if (metroTextBox7.Text.Length > 0 && metroTextBox5.Text.Length > 0 && double.TryParse(metroTextBox5.Text,out r1) && double.TryParse(metroTextBox7.Text, out r2))
                    metroTextBox8.Text = "" + (r2 / r1);
            else
                metroTextBox8.Text = "";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Home"].BringToFront();
            this.Dispose();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroTextBox8.Focus();
            string date1 = metroDateTime1.Text;
            if(Convert.ToInt32(metroComboBox1.SelectedIndex) == -1)
                MetroMessageBox.Show(this, "\n\n\nPlease Select a Type","ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                if (metroTextBox2.Visible && metroTextBox2.Text.ToString().Length <= 0)
                    MetroMessageBox.Show(this, "\n\n\nPlease Specify a Type", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                else
                {
                    if (Convert.ToInt32(metroComboBox2.SelectedIndex) == -1)
                        MetroMessageBox.Show(this, "\n\n\nPlease Select a Name", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    else
                    {
                        if (metroTextBox3.Visible && metroTextBox3.Text.ToString().Length <= 0)
                            MetroMessageBox.Show(this, "\n\n\nPlease Specify a Name", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        else
                        {
                            double quant;
                            if (!double.TryParse(metroTextBox5.Text, out quant))
                                MetroMessageBox.Show(this, "\n\n\nPlease Enter Proper Quantity", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            else
                            {
                                        double amt;
                                        if (!double.TryParse(metroTextBox7.Text, out amt))
                                            MetroMessageBox.Show(this, "\n\n\nPlease Enter Proper Amount", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                        else
                                        {
                                            if (Convert.ToInt32(metroComboBox4.SelectedIndex) == -1)
                                                MetroMessageBox.Show(this, "\n\n\nPlease Select a Purity", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                            else
                                            {
                                                if (metroTextBox6.Visible && metroTextBox6.Text.ToString().Length <= 0)
                                                    MetroMessageBox.Show(this, "\n\n\nPlease Specify a Purity", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                                else
                                                {
                                                    String dau = date1.Replace('-', '/');
                                                    DateTime dvs = Convert.ToDateTime(dau);
                                                    String type = metroComboBox1.SelectedItem.ToString();
                                                    if (type == "OTHER")
                                                        type = metroTextBox2.Text;
                                                    String name = metroComboBox2.SelectedItem.ToString();
                                                    if (name == "OTHER")
                                                        name = metroTextBox3.Text;
                                                    String unit = metroComboBox3.SelectedItem.ToString();
                                                    String purity = metroComboBox4.SelectedItem.ToString();
                                                    if (purity == "OTHER")
                                                        purity = metroTextBox6.Text;
                                                    double rate = Convert.ToDouble(metroTextBox8.Text);
                                                    String prod = "";
                                                    if (metroRadioButton1.Checked)
                                                        prod = "RAW";
                                                    else
                                                        prod = "FINISH";

                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                con.Open();
                String Query = "insert into metal(dat, type, name, qty, unit, amt, purity, rate, prodtype) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i);";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@a", dvs);
                cmd.Parameters.AddWithValue("@b", type);
                cmd.Parameters.AddWithValue("@c", name);
                cmd.Parameters.AddWithValue("@d", quant);
                cmd.Parameters.AddWithValue("@e", unit);
                cmd.Parameters.AddWithValue("@f", amt);
                cmd.Parameters.AddWithValue("@g", purity);
                cmd.Parameters.AddWithValue("@h", rate);
                cmd.Parameters.AddWithValue("@i", prod);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {                   
                    MetroMessageBox.Show(this, "\n\nMetal inserted successfully!!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    metroButton2.PerformClick();
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Metal_Purchase"].BringToFront();
                    Application.OpenForms["Metal_Purchase"].Focus();
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\n\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Metal_Purchase"].BringToFront();
                    Application.OpenForms["Metal_Purchase"].Focus();
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
        }
 
