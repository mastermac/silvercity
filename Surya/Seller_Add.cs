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
    public partial class Seller_Add : MetroForm
    {
        public Seller_Add()
        {
            InitializeComponent();
        }

        private void Seller_Add_Load(object sender, EventArgs e)
        {
            metroButton3.PerformClick();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text.ToString().Length > 0 && metroTextBox2.Text.ToString().Length > 0)
            {
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=Brokery;UID=root;PASSWORD=smhs;");
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Seller(Name,Contact,pic,Total,Deposited) values(@a,@b,@c,@d,@e)", con);
                cmd.Parameters.AddWithValue("@a", metroTextBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@b", metroTextBox2.Text.ToString());
                cmd.Parameters.AddWithValue("@c", metroLabel3.Text);
                cmd.Parameters.AddWithValue("@d", 0);
                cmd.Parameters.AddWithValue("@e", 0);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MetroMessageBox.Show(this, "\n\nSeller Added Successfully\nPlease Refer List of All Sellers for its Unique-ID", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    metroButton3.PerformClick();
                    this.Dispose();
                    Application.OpenForms["Home"].BringToFront();
                    
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\nSeller Not Added to the List", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Seller_Add"].BringToFront();

                }
                con.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPlease Enter Complete Information for a Seller..", "INCOMPLETE INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Seller_Add"].BringToFront();
            }

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "image files|*.jpg;*.png;*.gif;*.icon;.*;";

            DialogResult dres1 = openFileDialog1.ShowDialog();
            if (dres1 == DialogResult.Abort)
                return;
            if (dres1 == DialogResult.Cancel)
                return;


            //            pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            System.Drawing.Image imageFile = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            String s = openFileDialog1.FileName;
            metroLabel3.Text = s;
            int l = s.LastIndexOf("\\");
            int dot = s.LastIndexOf(".");

            String Code = s.Substring(l + 1, (dot - l - 1));
            Code = Code.ToUpper();
            var ratioX = (double)150 / imageFile.Width;
            var ratioY = (double)150 / imageFile.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(imageFile.Width * ratio);
            var newHeight = (int)(imageFile.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(imageFile, 0, 0, newWidth, newHeight);

            //          MemoryStream ms1 = new MemoryStream();
            pictureBox1.Image = newImage;
            metroTextBox1.Focus();
            //            pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            metroTextBox2.Text = "";
            pictureBox1.Image = null;
            metroLabel3.Text = "";

        }
    }
}
