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
using System.IO;
using System.Diagnostics;
namespace Surya
{
    public partial class Item_Addd : MetroForm
    {
        public Item_Addd()
        {
            InitializeComponent();
        }

        private void Item_Addd_Load(object sender, EventArgs e)
        {
            metroButton3.PerformClick();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd;
            FileStream fs;
            BinaryReader br;

            try
            {
                if (metroTextBox2.Text.Length > 0 && metroComboBox2.SelectedIndex!=-1)
                {
                    //string FileName = metroLabel4.Text;
                    //byte[] ImageData;
                    //fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    //br = new BinaryReader(fs);
                    //ImageData = br.ReadBytes((int)fs.Length);
                    //br.Close();
                    //fs.Close();


                    string CmdString = "INSERT INTO Item(code, size, pic, descrip,cate) VALUES(@FirstName, @LastName, @Image, @Address,@cat)";
                    cmd = new MySqlCommand(CmdString, con);
                    
                    cmd.Parameters.AddWithValue("@FirstName", metroTextBox2.Text);
                    cmd.Parameters.AddWithValue("@LastName", metroTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Image", new byte[] { 0x20 });// ImageData);
                    cmd.Parameters.AddWithValue("@Address", metroTextBox3.Text);
                    cmd.Parameters.AddWithValue("@cat", metroComboBox2.SelectedItem.ToString());


                    con.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        MetroMessageBox.Show(this, "New Item Saved!!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metroButton3.PerformClick();
                        Application.OpenForms["Home"].BringToFront();
                        Application.OpenForms["Item_Addd"].BringToFront();
                        this.Focus();

                    }
                    con.Close();
                }
                else
                {
                    MetroMessageBox.Show(this,"Incomplete data!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "Please Check the data & Try Again!!"+ex.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Focus();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "*.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    metroLabel4.Text = openFileDialog1.FileName;
                    System.Drawing.Image imageFile = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                    var ratioX = (double)150 / imageFile.Width;
                    var ratioY = (double)150 / imageFile.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(imageFile.Width * ratio);
                    var newHeight = (int)(imageFile.Height * ratio);

                    var newImage = new Bitmap(newWidth, newHeight);
                    Graphics.FromImage(newImage).DrawImage(imageFile, 0, 0, newWidth, newHeight);
                    pictureBox1.Image = newImage;
                    metroTextBox1.Focus();
                }
                metroButton2.Enabled=true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            //metroButton2.Enabled = false;
            metroTextBox3.Text = "";
            metroTextBox2.Text = "";
            metroTextBox1.Text = "";
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            folder = folder.Substring(0, folder.Length - 10)+@"\Resources\default.jpg";
            metroLabel4.Text = folder;
            System.Drawing.Image imageFile = System.Drawing.Image.FromFile(folder);
            pictureBox1.Image = imageFile;
//            metroLabel4.Text = "";
            this.Focus();

        }
    }
}
