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
    public partial class Cash_Depo : MetroForm
    {
        public Cash_Depo()
        {
            InitializeComponent();
        }

        private void Cash_Depo_Load(object sender, EventArgs e)
        {

        }

        private void Cash_Depo_VisibleChanged(object sender, EventArgs e)
        {
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            String q1 = "Select id from invent, sell where id=sell.specs and status='S' and date_clear is null order by dos;";

            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                metroComboBox1.Items.Add(dataReader1.GetValue(0));
            }
            if (metroComboBox1.Items.Count > 0)
                metroComboBox1.SelectedIndex = 0;
            else
            {
                if (this.Visible)
                {
                    MetroMessageBox.Show(this, "\n\nNo More Unpaid Inventories to Deposit Money into!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Cash_Depo"].BringToFront();
                Close();
            }
            metroTextBox1.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string date1 = metroTextBox1.Text;
            String dau = date1.Replace('-', '/');
            DateTime dvs = Convert.ToDateTime(dau);
            
            
            
            double result1=0;
            String tx = metroTextBox2.PromptText;
            int leo = tx.IndexOf(".");
            double prnum=Convert.ToDouble(tx.Substring(leo+1));
            if (metroTextBox2.Text.ToString().Length > 0 && Double.TryParse(metroTextBox2.Text.ToString(), out result1) && Convert.ToDouble(metroTextBox2.Text)<=prnum)
            {
                double re = Convert.ToDouble(metroTextBox2.Text.ToString());
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
                con.Open();
                MySqlCommand cmd=new MySqlCommand("Select bid from sell where specs='"+metroComboBox1.SelectedItem.ToString() + "';",con);
                MySqlDataReader dataReader1;
                dataReader1 = cmd.ExecuteReader();

                int bid = 0;

                while (dataReader1.Read())
                {
                    bid = Convert.ToInt32(dataReader1.GetValue(0));
                }
                dataReader1.Close();

                cmd.CommandText = "update Buyer set Deposited=Deposited+"+re+ " where Buyid=" + bid + ";";

                int result = 0;
                result = cmd.ExecuteNonQuery();

                cmd.CommandText = "update Seller set Deposited=Deposited+" + re + " where Selid=10001;";
                result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            String q1 = "Insert into History(pid,dater,amt,bid) values(@a,@b,@c,@d);";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            cmd1.Parameters.AddWithValue("@a", metroComboBox1.SelectedItem.ToString().Trim());
            cmd1.Parameters.AddWithValue("@b", dvs);
            cmd1.Parameters.AddWithValue("@c", re);
            cmd1.Parameters.AddWithValue("@d", bid);

            con1.Open();
            cmd1.ExecuteNonQuery();
            

            cmd1.CommandText = "update sell set date_clear=@davs where specs='" + metroComboBox1.SelectedItem.ToString() + "' and netAmt=(Select Sum(amt) from history where pid='" + metroComboBox1.SelectedItem.ToString() + "' group by pid);";
            cmd1.Parameters.AddWithValue("@davs", dvs);
            cmd1.ExecuteNonQuery();
                MetroMessageBox.Show(this, ("\n\nData Updated successfully!"), "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
                else
                    MetroMessageBox.Show(this, "\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
                Close();
                Application.OpenForms["Home"].BringToFront();
                

            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPlease Insert Proper info First to Proceed Further..", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Cash_Depo"].BringToFront();
                metroTextBox2.Text = "";
                metroTextBox2.Select();
            }
             
        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            metroTextBox2.Text = "";
            String code = metroComboBox1.SelectedItem.ToString();
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select netamt from sell where specs='"+code+"';", con);
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();

            double tot = 0, sum = 0;

            while (dataReader1.Read())
            {
                tot = Convert.ToDouble(dataReader1.GetValue(0));
            }
            dataReader1.Close();

            cmd.CommandText = "Select Sum(amt) from history where pid='" + code + "';";
            dataReader1 = cmd.ExecuteReader();
            while (dataReader1.Read())
            {
                if (dataReader1.GetValue(0).ToString().Length>0)
                    sum = Convert.ToDouble(dataReader1.GetValue(0));
            }
            Font f=new Font("Times New Roman",8.0f,FontStyle.Italic);
            metroTextBox2.Font = f;
            metroTextBox2.PromptText = "Bal Amount= Rs. " + (tot - sum);
            
        }

        private void metroTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (metroTextBox2.Text.Length == 1)
            {
                Font f = new Font("Times New Roman", 16.0f, FontStyle.Bold);
                metroTextBox2.Font = f;
            }
            else if (metroTextBox2.Text.Length == 0)
            {
                Font f = new Font("Times New Roman", 8.0f, FontStyle.Italic);
                metroTextBox2.Font = f;
            }
        }
    }
}
