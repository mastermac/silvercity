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
    public partial class EditStock : MetroForm
    {
        public EditStock()
        {
            InitializeComponent();
        }

        private void EditStock_Load(object sender, EventArgs e)
        {
            String s = metroLabel13.Text;


            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=sales;UID=root;PASSWORD=smhs;");
            String q1 = "Select distinct shape from invent order by shape;";
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

            String q2 = "Select distinct color from invent order by color;";
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

            q2 = "Select distinct purity from invent order by purity;";
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
            dataReader1.Close();


            q2 = "Select distinct certitype from invent order by certitype;";
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

            metroTextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            metroTextBox1.Text = "";

            metroTextBox4.Visible = false;
            metroTextBox6.Visible = false;
            metroTextBox8.Visible = false;
            metroTextBox9.Visible = false;
            metroTextBox4.Enabled = true;
            metroTextBox6.Enabled = true;
            metroTextBox8.Enabled = true;
            metroTextBox9.Enabled = true;
            metroLabel7.Visible = false;
            metroLabel10.Visible = false;
            metroLabel1.Visible = false;
            metroLabel16.Visible = false;

            
            q2 = "Select * from invent where id='"+s+"';";
            cmd1.CommandText = q2;
            dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                metroTextBox1.Text=dataReader1.GetValue(0).ToString();
                metroComboBox1.SelectedItem = dataReader1.GetValue(1);
                metroTextBox2.Text = dataReader1.GetValue(2).ToString();
                metroTextBox3.Text=dataReader1.GetValue(3).ToString();
                metroComboBox2.SelectedItem = dataReader1.GetValue(4);
                metroTextBox5.Text = dataReader1.GetValue(5).ToString();
                metroComboBox3.SelectedItem = dataReader1.GetValue(6);
                metroTextBox7.Text = dataReader1.GetValue(7).ToString();
                metroComboBox4.SelectedItem = dataReader1.GetValue(8);
                metroComboBox5.SelectedItem = dataReader1.GetValue(9);
                metroComboBox6.SelectedItem = dataReader1.GetValue(10);
                //metroTextBox10.Text = dataReader1.GetValue(11).ToString();
                metroTextBox11.Text = dataReader1.GetValue(12).ToString();
                metroTextBox12.Text = dataReader1.GetValue(13).ToString();
                metroTextBox13.Text = dataReader1.GetValue(14).ToString();
                metroTextBox14.Text = dataReader1.GetValue(15).ToString();
                metroComboBox7.SelectedItem = dataReader1.GetValue(16);
                
                String bl = dataReader1.GetValue(17).ToString();
                if (bl.Equals("Yes"))
                    metroRadioButton1.Select();
                else
                    metroRadioButton2.Select();

                metroTextBox16.Text = dataReader1.GetValue(18).ToString();
                metroTextBox17.Text = dataReader1.GetValue(19).ToString();
                metroTextBox18.Text = dataReader1.GetValue(20).ToString();
                metroTextBox19.Text = dataReader1.GetValue(21).ToString();
                metroTextBox20.Text = dataReader1.GetValue(22).ToString();
            }
            dataReader1.Close();
            con1.Close();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            metroTextBox2.Text = "";
            metroTextBox3.Text = "";
            metroTextBox4.Text = "";
            metroTextBox5.Text = "";
            metroTextBox7.Text = "";
            metroTextBox8.Text = "";
            metroTextBox9.Text = "";
//            metroTextBox10.Text = "";
  //          pictureBox1.Image = null;
            metroLabel1.Text = "";
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
            metroLabel1.Text = s;
            int l = s.LastIndexOf("\\");
            int dot = s.LastIndexOf(".");

            String Code = s.Substring(l + 1, (dot - l - 1));
            Code = Code.ToUpper();
            metroTextBox1.Text = Code;
            var ratioX = (double)250 / imageFile.Width;
            var ratioY = (double)250 / imageFile.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(imageFile.Width * ratio);
            var newHeight = (int)(imageFile.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(imageFile, 0, 0, newWidth, newHeight);

            //          MemoryStream ms1 = new MemoryStream();
            //pictureBox1.Image = newImage;
            metroTextBox1.Focus();
            //            pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //MemoryStream ms1 = new MemoryStream();
            //pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] img_arr1 = new byte[ms1.Length];
            //ms1.Read(img_arr1, 0, img_arr1.Length);
            /*if ((metroTextBox1.Text.ToString().Length > 0) && (metroTextBox2.Text.ToString().Length > 0) && (metroTextBox3.Text.ToString().Length > 0) && (metroTextBox10.Text.ToString().Length > 0))
            {

                MySqlConnection con = new MySqlConnection("Data Source=AMO;Initial Catalog=SuryaGems;Integrated Security=True");
                con.Open();
                double gr, g, sil, sd, fd, cs, moti;
                if (metroTextBox4.Text.ToString() == "")
                    g = 0;
                else
                    g = 0 + Convert.ToDouble(metroTextBox4.Text.ToString());

                if (metroTextBox5.Text.ToString() == "")
                    sd = 0;
                else
                    sd = 0 + Convert.ToDouble(metroTextBox5.Text.ToString());

                if (metroTextBox7.Text.ToString() == "")
                    fd = 0;
                else
                    fd = 0 + Convert.ToDouble(metroTextBox7.Text.ToString());

                if (metroTextBox8.Text.ToString() == "")
                    cs = 0;
                else
                    cs = 0 + Convert.ToDouble(metroTextBox8.Text.ToString());

                if (metroTextBox9.Text.ToString() == "")
                    moti = 0;
                else
                    moti = 0 + Convert.ToDouble(metroTextBox9.Text.ToString());
                
                gr = Convert.ToDouble(metroTextBox2.Text.ToString());
                sil = gr - (g + ((sd + fd + cs + moti) * 0.2));

                MySqlCommand cmd = new MySqlCommand("update stock set code='" + metroTextBox1.Text.ToString() + "', pic='" + metroLabel1.Text.ToString() + "', gross='" + metroTextBox2.Text.ToString() + "',Pcs='" + metroTextBox3.Text.ToString() + "', Gold='" + metroTextBox4.Text.ToString() + "', Silver='" + sil.ToString() + "',Sd='" + metroTextBox5.Text.ToString() + "',Fd='" + metroTextBox7.Text.ToString() + "',Cs='" + metroTextBox8.Text.ToString() + "',Moti='" + metroTextBox9.Text.ToString() + "',Price='" + metroTextBox10.Text.ToString() + "' where code='" + metroTextBox1.Text.ToString() + "';", con);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MetroMessageBox.Show(this, "\n\nData Updated successfully", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    metroButton3.PerformClick();
                }
                else
                    MetroMessageBox.Show(this, "\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPLEASE ENTER DETAILS PROPERLY FIRST!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["EditStock"].BringToFront();
                metroTextBox1.Select();
            }
             */ 
        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem.ToString().Equals("Other"))
            {
                metroTextBox1.Visible = true;
                metroLabel3.Visible = true;
            }
            else
            {
                metroTextBox2.Visible = false;
                metroLabel3.Visible = false;
            }
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            
            string code = metroTextBox1.Text;
            string status = metroComboBox1.SelectedItem.ToString();
            string date1 = metroTextBox2.Text;
            string cpc = metroTextBox3.Text;

            string shape = metroComboBox2.SelectedItem.ToString();
            if (shape.Equals("OTHER"))
                shape = metroTextBox4.Text;
            double wt = Convert.ToDouble(metroTextBox5.Text);
            string color = metroComboBox3.SelectedItem.ToString();
            if (color.Equals("OTHER"))
                color = metroTextBox6.Text;
            string cps = metroTextBox7.Text;
            string purity = metroComboBox4.SelectedItem.ToString();
            if (purity.Equals("OTHER"))
                purity = metroTextBox8.Text;
            string fl = metroComboBox5.SelectedItem.ToString();
            string certitype = metroComboBox6.SelectedItem.ToString();
            if (certitype.Equals("OTHER"))
                certitype = metroTextBox9.Text;
            string certilink = "OPEN";
            double rap = Convert.ToDouble(metroTextBox11.Text);
            double disc = Convert.ToDouble(metroTextBox12.Text);
            double rate = Convert.ToDouble(metroTextBox13.Text);
            double amt = Convert.ToDouble(metroTextBox14.Text);
            string bgm = metroComboBox7.SelectedItem.ToString();
            string black = "";
            if (metroRadioButton1.Checked)
                black = "YES";
            else
                black = "NO";
            double td = Convert.ToDouble(metroTextBox16.Text);
            double ta = Convert.ToDouble(metroTextBox17.Text);
            string measure = metroTextBox18.Text;
            string pc = metroTextBox19.Text;
            string specs = metroTextBox20.Text;
            if (pc.Length == 0)
                pc = "NIL";

            //MessageBox.Show("Hello2");
            String d1 = date1.Replace('-', '/');
            DateTime d = Convert.ToDateTime(d1);
            if ((date1.Length >= 8) && (code.Length > 0) && (measure.Length > 0) && (cps.Length > 0) && (cpc.Length > 0) && (wt > 0) && (amt > 0) && (rap >= 0))
            {
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=sales;UID=root;PASSWORD=smhs;");
                con.Open();
                //String Query = "insert into invent(id,status,date_pur,Cpc,shape,wt,color,Cps,purity,fl,certitype,certilink,rap,disc,rate,amt,bgm,Black,td,ta,measure,pair_code,specs) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p,@q,@r,@s,@t,@u,@v,@w);";

                String Query = "UPDATE invent set status=@b,date_pur=@c,Cpc=@d,shape=@e,wt=@f,color=@g,Cps=@h,purity=@i,fl=@j,certitype=@k,certilink=@l,rap=@m,disc=@n,rate=@o,amt=@p,bgm=@q,Black=@r,td=@s,ta=@t,measure=@u,pair_code=@v,specs=@w where id='" + metroLabel13.Text.ToString() + "';";
                
                MySqlCommand cmd = new MySqlCommand(Query, con);
                //MessageBox.Show(Query);
//                cmd.Parameters.AddWithValue("@a", code);
                cmd.Parameters.AddWithValue("@b", status);
                cmd.Parameters.AddWithValue("@c", d);
                cmd.Parameters.AddWithValue("@d", cpc);
                cmd.Parameters.AddWithValue("@e", shape);
                cmd.Parameters.AddWithValue("@f", wt);
                cmd.Parameters.AddWithValue("@g", color);
                cmd.Parameters.AddWithValue("@h", cps);
                cmd.Parameters.AddWithValue("@i", purity);
                cmd.Parameters.AddWithValue("@j", fl);
                cmd.Parameters.AddWithValue("@k", certitype);
                cmd.Parameters.AddWithValue("@l", certilink);
                cmd.Parameters.AddWithValue("@m", rap);
                cmd.Parameters.AddWithValue("@n", disc);
                cmd.Parameters.AddWithValue("@o", rate);
                cmd.Parameters.AddWithValue("@p", amt);
                cmd.Parameters.AddWithValue("@q", bgm);
                cmd.Parameters.AddWithValue("@r", black);
                cmd.Parameters.AddWithValue("@s", td);
                cmd.Parameters.AddWithValue("@t", ta);
                cmd.Parameters.AddWithValue("@u", measure);
                cmd.Parameters.AddWithValue("@v", pc);
                cmd.Parameters.AddWithValue("@w", specs);


                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    /*                        q1 = "Update Buyer set Total=Total+" + amt + " where Buyid='" + buyid + "';";
                                            cmd1.CommandText = q1;
                                            cmd1.ExecuteNonQuery();

                                            q1 = "Update Seller set Total=Total+" + amt + " where Selid='" + selid + "';";
                                            cmd1.CommandText = q1;
                                            cmd1.ExecuteNonQuery();

                      */
                    MetroMessageBox.Show(this, "\n\nData UPDATED successfully", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    metroButton2.PerformClick();
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["EditStock"].BringToFront();
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["EditStock"].BringToFront();
                }
                con.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPLEASE ENTER DETAILS PROPERLY FIRST!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["EditStock"].BringToFront();
                metroTextBox1.Select();
            }

        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            EditStock e1 = new EditStock();
            e1.Visible=true;
            this.Dispose();

        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            Application.OpenForms["Home"].BringToFront();
            Application.OpenForms["EditStock"].BringToFront();
            this.Dispose();
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
            if (metroComboBox4.SelectedItem.ToString().Equals("OTHER"))
            {
                metroTextBox8.Visible = true;
                metroLabel1.Visible = true;
            }
            else
            {
                metroTextBox8.Visible = false;
                metroLabel1.Visible = false;
            }


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

        private void metroTextBox13_Enter(object sender, EventArgs e)
        {
            if (metroTextBox11.Text.Length > 0 && metroTextBox12.Text.Length > 0)
            {
                double rap = Convert.ToDouble(metroTextBox11.Text);
                double disc = 100 - Convert.ToDouble(metroTextBox12.Text);
                disc = disc * 0.01;
                double rate = rap * disc * 66.1;
                metroTextBox13.Text = "" + rate;
            }
            else if (metroTextBox11.Text.Length == 0 || metroTextBox12.Text.Length == 0)
            {
                metroTextBox13.Text = "";
            }

        }

        private void metroTextBox14_Enter(object sender, EventArgs e)
        {
            if (metroTextBox13.Text.Length > 0 && metroTextBox5.Text.Length > 0)
            {
                double wt = Convert.ToDouble(metroTextBox5.Text);
                double rate = Convert.ToDouble(metroTextBox13.Text);
                double amt = rate * wt;
                metroTextBox14.Text = "" + amt;
            }
            else if (metroTextBox13.Text.Length == 0 || metroTextBox5.Text.Length == 0)
            {
                metroTextBox14.Text = "";
            }

        }
    }
}
