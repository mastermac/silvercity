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
    public partial class Buyer_Add : MetroForm
    {
        public Buyer_Add()
        {
            InitializeComponent();
        }

        private void Buyer_Add_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            metroTextBox2.Text = "";
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text.ToString().Length > 0 && metroTextBox2.Text.ToString().Length > 0)
            {
                double tot = 0+Convert.ToDouble(metroTextBox2.Text);
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Buyer(Name,Total,Deposited) values(@a,@b,@c)", con);
                cmd.Parameters.AddWithValue("@a", metroTextBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@b", tot);
                cmd.Parameters.AddWithValue("@c", 0);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MetroMessageBox.Show(this, "\n\nBuyer Added Successfully\nPlease Refer List of All Buyers for its Unique-ID", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    metroButton3.PerformClick();
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Buyer_Add"].BringToFront();
                    this.Dispose();
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\nBuyer Not Added to the List", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                }
                con.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPlease Enter Complete Information for a Buyer..", "INCOMPLETE INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Buyer_Add"].BringToFront();
            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }
        }
    }

