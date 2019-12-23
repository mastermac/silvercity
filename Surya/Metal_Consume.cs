using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Surya
{
    public partial class Metal_Consume : MetroForm
    {
        public Metal_Consume()
        {
            InitializeComponent();
        }

        private void Metal_Consume_Load(object sender, EventArgs e)
        {
            metroButton2.PerformClick();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
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
            dataReader1.Close();


            String q2 = "Select distinct unit from metal;";
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
            con1.Close();
            metroDateTime1.ResetText();
            metroTextBox3.Text = "";
            metroTextBox5.Text = "";
            metroRadioButton1.Select();
            this.Focus();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string date1 = metroDateTime1.Text;
            if(Convert.ToInt32(metroComboBox1.SelectedIndex) == -1)
                MetroMessageBox.Show(this, "\n\n\nPlease Select a Type","ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                        if (metroTextBox3.Text.ToString().Length <= 0)
                            MetroMessageBox.Show(this, "\n\n\nPlease Specify a Name", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        else
                        {
                            double quant;
                            if (!double.TryParse(metroTextBox5.Text, out quant))
                                MetroMessageBox.Show(this, "\n\n\nPlease Enter Proper Quantity", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            else
                            {
                                if (Convert.ToInt32(metroComboBox3.SelectedIndex) == -1)
                                    MetroMessageBox.Show(this, "\n\n\nPlease Select a Unit", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                else
                                {
                                            if (Convert.ToInt32(metroComboBox4.SelectedIndex) == -1)
                                                MetroMessageBox.Show(this, "\n\n\nPlease Select a Purity", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                            else
                                            {
                                                    String dau = date1.Replace('-', '/');
                                                    DateTime dvs = Convert.ToDateTime(dau);
                                                    String type = metroComboBox1.SelectedItem.ToString();
                                                    String name = metroTextBox3.Text;
                                                    String unit = metroComboBox3.SelectedItem.ToString();
                                                    String purity = metroComboBox4.SelectedItem.ToString();
                                                    String prod = "";
                                                    if (metroRadioButton1.Checked)
                                                        prod = "RAW";
                                                    else
                                                        prod = "FINISH";

                                                    MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;");
                                                    String q1 = "Select count(metal_consume.qty), sum(metal.qty)-sum(metal_consume.qty) - " + quant + " from metal, metal_consume where metal.type='" + type + "' and metal.type=metal_consume.type  and metal.prodtype='" + prod + "' and metal_consume.prodtype='"+prod+"' ;";
                                                    MySqlCommand cmd1 = new MySqlCommand(q1, con1);
                                                    con1.Open();
                                                    var dataReader1 = cmd1.ExecuteReader();
                                                    double su1=0,su2=0;
                                                    int cou=0;
                                                    while (dataReader1.Read())
                                                    {
                                                        cou = Convert.ToInt32(dataReader1.GetValue(0)+""+0);
                                                        su2 = Convert.ToDouble(dataReader1.GetValue(1)+""+0);
                                                    }
                                                    dataReader1.Close();
                                                    q1 = "Select sum(metal.qty) - " + quant + " from metal where metal.type='" + type + "' and prodtype='"+prod+"';";
                                                    cmd1.CommandText = q1;
                                                    dataReader1 = cmd1.ExecuteReader();
                                                    while (dataReader1.Read())
                                                    {
                                                        su1 = Convert.ToDouble(dataReader1.GetValue(0));
                                                    }
                                                    dataReader1.Close();
                                                    con1.Close();
                                                    if ((su1 >= 0 && cou == 0) || (su2 >= 0 && cou>0))
                                                    {
                                                    
                                                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                                                        con.Open();
                                                        String Query = "insert into metal_consume(dat, type, name, qty, unit, purity, prodtype) VALUES (@a,@b,@c,@d,@e,@f,@g);";
                                                        MySqlCommand cmd = new MySqlCommand(Query, con);
                                                        cmd.Parameters.AddWithValue("@a", dvs);
                                                        cmd.Parameters.AddWithValue("@b", type);
                                                        cmd.Parameters.AddWithValue("@c", name);
                                                        cmd.Parameters.AddWithValue("@d", quant);
                                                        cmd.Parameters.AddWithValue("@e", unit);
                                                        cmd.Parameters.AddWithValue("@f", purity);
                                                        cmd.Parameters.AddWithValue("@g", prod);
                                                        int result = cmd.ExecuteNonQuery();
                                                        if (result > 0)
                                                        {
                                                            MetroMessageBox.Show(this, "\n\nMetal Consumed successfully!!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            metroButton2.PerformClick();
                                                            Application.OpenForms["Home"].BringToFront();
                                                            Application.OpenForms["Metal_Consume"].BringToFront();

                                                            this.Focus();
                                                        }
                                                        else
                                                        {
                                                            MetroMessageBox.Show(this, "\n\n\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                                            Application.OpenForms["Home"].BringToFront();
                                                            Application.OpenForms["Metal_Consume"].BringToFront();
                                                            this.Focus();
                                                        }
                                                        con.Close();
                                                    }
                                                    else
                                                    {
                                                        MetroMessageBox.Show(this, "\nYou Don't Have Enough Metal to Consume!!\nAvailable Metal = "+(su2+quant)+"\nMetal trying to Consuming = "+quant+"", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                                        this.Focus();
                                                    }
                                                }
                                            }
                                        }                            
                                    }
                                }                    
                            }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Home"].BringToFront();
            this.Dispose();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem.ToString().Equals("Gold"))
                metroComboBox3.SelectedItem = "Grm";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Silver"))
                metroComboBox3.SelectedItem = "Kg";

        }
    }
}            
             

