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
using MySql.Data.MySqlClient;
using MetroFramework;
namespace Surya
{
    
    public partial class Mbox_Search : MetroForm
    {
        public Mbox_Search()
        {
            InitializeComponent();
        }

        private void Mbox_Search_Load(object sender, EventArgs e)
        {
            //metroTextBox1.Select();


        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["Home"].BringToFront();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
            String s = metroComboBox1.SelectedItem.ToString();
         
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=sales;UID=root;PASSWORD=smhs;");
            String q1 = "Select * from invent where id='" + s + "';";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            if (dataReader1.Read())
            {
                String re = metroLabel2.Text.ToString();
                if (re == "edit")
                {
                    EditStock e1 = new EditStock();
                    e1.metroLabel13.Text = s;
                    e1.Visible = true;
                    Close();
                }
                else if (re == "Search")
                {
                    Transaction t = new Transaction();
                    t.Text = "PAYMENT HISTORY";
                    t.Show();
                    t.metroButton1.PerformClick();
                    t.metroComboBox1.SelectedItem = "Unique ID";
                    t.metroTextBox1.Text = s;
                    t.metroButton3.PerformClick();
                    //t.metroButton2.PerformClick();
                    //this.Dispose();
                    Application.OpenForms["Mbox_Search"].Dispose();
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Transaction"].BringToFront();
                    
                }
                else if (re == "delete")
                {
                    DialogResult d = new DialogResult();
                    d=MetroMessageBox.Show(this, "\n\nAre Your Sure you want to delete this Entry?\nEntry Once deleted cant be recovered Back..", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (d == DialogResult.OK)
                    {
                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("Select id, status, bid, netamt from invent,sell where id='" + s + "' and id=sell.specs;", con);
                        MySqlDataReader dr1 = cmd.ExecuteReader();
                        double amt = 0;
                        int bid = 0;
                        String stat = "";
                        while (dr1.Read())
                        {
                            stat = dr1.GetValue(1).ToString();
                            amt = Convert.ToDouble(dr1.GetValue(3));
                            bid = Convert.ToInt32(dr1.GetValue(2));
                        }
                        dr1.Close();
                        double sum = 0;
                        cmd.CommandText = "Select Sum(amt) from history where pid='" + s + "';";
                        dr1 = cmd.ExecuteReader();
                        while (dr1.Read())
                        {
                            String s1 = Convert.ToString(dr1.GetValue(0));
                            if (s1.Length > 0)
                                sum = sum + Convert.ToDouble(s1);
                        }
                        dr1.Close();
                        cmd.CommandText = "DELETE from history where pid='" + s + "';";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE buyer set Total=Total-" + amt + ", Deposited=Deposited-" + sum + " where Buyid=" + bid + ";";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE Seller set Total=Total-" + amt + ", Deposited=Deposited-" + sum + " where Selid=10001;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE from sell where specs='" + s + "';";
                        int result = cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE from invent where id='" + s + "';";
                        result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MetroMessageBox.Show(this, "\n\nEntry Deleted Successfully", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                    }
                }
                else if (re == "balance")
                {

                }

            }
            else
            {
                DialogResult d = new DialogResult();
                d=MetroMessageBox.Show(this,"STOCK NOT FOUND!! Please Re-Enter a Valid Stock-Code to proceed Further..", "INVALID STOCK CODE", MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
                if (d.ToString() == "Retry")
                {
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Mbox_Search"].BringToFront();
                    //metroTextBox1.Select();
                }
                else
                {
                    Close();
                    Application.OpenForms["Home"].BringToFront();
                }
                
            }
            
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

        }

        private void Mbox_Search_VisibleChanged(object sender, EventArgs e)
        {
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=sales;UID=root;PASSWORD=smhs;; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
//            String q1 = "Select id from invent as i where (Select DATEDIFF(day,date_in,date_clear) from invent as i1 where i1.id=i.id)=0 order by date_in;";
            String q1 = "Select id from invent order by id ;";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                metroComboBox1.Items.Add(dataReader1.GetValue(0));
            }
        }
    }
}
