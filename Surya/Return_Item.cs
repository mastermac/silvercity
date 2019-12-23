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
    public partial class Return_Item : MetroForm
    {
        public Return_Item()
        {
            InitializeComponent();
        }

        private void Return_Item_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            String refer=metroTextBox1.Text.ToString();
            DialogResult d = new DialogResult();
            d = MetroMessageBox.Show(this, "\n\nAre You Sure that you want to retake the items of MEMO??", "CONFIRM YOUR CHOICE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (d.ToString()=="Yes")
            {
                MySqlConnection con = new MySqlConnection("Data Source=AMO;Initial Catalog=SuryaGems;Integrated Security=True");
                con.Open();

                DateTime d1 = DateTime.Now;

                String s1 = ""+d1.ToShortDateString();
                String s2 = s1.Substring(6) + "-" + s1.Substring(3, 2) + "-" + s1.Substring(0, 2);
                MySqlCommand cmd = new MySqlCommand("update Estimate set Date_Return='"+s2+"' where Ref_Id='" + metroTextBox1.Text.ToString() + "';", con);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MySqlCommand cmd1 = new MySqlCommand("update Availability set status='Available',Buyer='',Ref='' where Ref='" + metroTextBox1.Text.ToString() + "' and status='Hold';", con);
                    int result1 = cmd1.ExecuteNonQuery();
                    if (result1 > 0)
                    {
                        Close();
                        MetroMessageBox.Show(this, "\nItems of Approval Memo are now AVAILABLE!!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.OpenForms["Home"].BringToFront();
                    }
                    else
                    {
                        Close();
                        MetroMessageBox.Show(this, "\n\nInvalid Reference ID\nPlease Check and Try Again!!", "INVALID ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.OpenForms["Home"].BringToFront();
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\nThis Reference-ID doesnt Exist\nPlease Check and Try Again!!", "OOPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Return_Item"].BringToFront();
                    metroTextBox1.Text = "";
                    metroTextBox1.Select();
                }
                    con.Close();
            
            }
            else if (d.ToString()==("No"))
            {
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Return_Item"].BringToFront();
                metroTextBox1.Text = "";
                metroTextBox1.Select();
            }
            else
            {
                Close();
                Application.OpenForms["Home"].BringToFront();                
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Close();
            //Home h = new Home();
            Application.OpenForms["Home"].BringToFront();
            //h.Visible = true;

        }
    }
}
