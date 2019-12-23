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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Surya
{
    public partial class Packing : MetroForm
    {
        public Packing()
        {
            InitializeComponent();
        }

        private void Packing_Load(object sender, EventArgs e)
        {
            //metroLabel1.Visible = true;
            metroGrid2.Columns[5].DefaultCellStyle.BackColor = Color.LightGreen;
            metroGrid2.Columns[6].DefaultCellStyle.BackColor = Color.LightGreen;
            metroGrid2.Columns[7].DefaultCellStyle.BackColor = Color.LightGreen;
            metroGrid2.Columns[8].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            metroGrid2.Columns[9].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            metroGrid2.Columns[10].DefaultCellStyle.BackColor = Color.LightSkyBlue;

            //metroButton2.PerformClick();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string tabl = numericUpDown1.Text.ToString();
            string strCheck = "SHOW TABLES LIKE \'inv_" + tabl + "\';";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd = new MySqlCommand(strCheck, con);
            con.Open();
            //cmd.Prepare();
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();
            if (dataReader1.Read())
            {
            }
            else
            {
                MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                //CREATE TABLE tabl(id, code, desc, lot, pcs, wt, stones, metwt, rt, subtot, nc_i, nc_u, pp_i, pp_u, pg_i, pg_u
                string query = @"CREATE TABLE inv_" + tabl + "(	id int(10) primary key auto_increment, 	code varchar(20), 	descri varchar(200), 	unit varchar(20) not null,	lot numeric(10,1), 	pcs int(10) not null, 	wt numeric(8,2) not null, 	stones int(10),	metwt numeric(8,2), 	rt numeric(10,2), 	subtot int(10) not null, 	nc_i int(10) not null default 0, 	nc_u numeric(10,2) not null default 0, 	pp_i int(10) not null default 0, 	pp_u numeric(10,2) not null default 0, 	pg_i int(10) not null default 0, 	pg_u numeric(10,2) not null default 0,	date_pack date not null,	exchg_rate numeric(5,2) not null);";
            //    MessageBox.Show(this, query);
                MySqlCommand acmd = new MySqlCommand(query, con1);
                con1.Open();
                acmd.ExecuteNonQuery();
                con1.Close();
            }
            dataReader1.Close();
            cmd.CommandText="insert into ledger(table_name, code) VALUES (@a,@b);";
            //MessageBox.Show(Query);
            cmd.Parameters.AddWithValue("@a", numericUpDown1.Text.ToString());
            cmd.Parameters.AddWithValue("@b", metroComboBox6.SelectedItem.ToString());
            
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                String rate = numericUpDown7.Text;
                metroButton6.PerformClick();
                numericUpDown7.Text = rate;
            }
            con.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroGrid2.Rows.Clear();
            metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            String q1 = "Select distinct code from item where code not in (select distinct code from ledger where table_name=\'"+numericUpDown1.Text.ToString()+"\');";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            con1.Open();
            var dataReader1 = cmd1.ExecuteReader();
            int count = 0;
            metroComboBox6.Items.Clear();
            while (dataReader1.Read())
            {
                metroComboBox6.Items.Insert(count, dataReader1.GetValue(0));
                count++;
            }
            dataReader1.Close();
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            metroButton2.Visible = true;
            metroButton2.PerformClick();
            metroButton2.Visible = false;
            
            string tabl = numericUpDown1.Text.ToString();
            string strCheck = "SHOW TABLES LIKE \'inv_"+tabl+"\'";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(strCheck, con);
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();
            if (dataReader1.Read())
            {
                metroButton4.Visible = true;
                metroButton4.PerformClick();
                metroButton4.Visible = false;
            }
            else
            {
            }
            
            con.Close();
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void numericUpDown1_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
                
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                metroGrid2.Rows.Clear();
                metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                String query = "Select item.pic,item.descrip,0, ledger.pg_r, ledger.pro_rt, ledger.ta_r, ledger.pg_u, ledger.pp_u, ledger.ta_u, item.code from item, ledger where table_name='" + numericUpDown1.Text.ToString() + "' and ledger.code=item.code order by led_id; ";
                //MessageBox.Show(query);
                MySqlCommand cmd1;
                cmd1 = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader dataReader1;
                dataReader1 = cmd1.ExecuteReader();
                int row = 0;
                double exchg_rt = 0;
                while (dataReader1.Read())
                {
                    MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                    String query1 = "Select sum(wt) as wt, sum(subtot) as subtot from inv_" + numericUpDown1.Text.ToString() + " where code='" + dataReader1.GetValue(9).ToString() + "' ;";
                    MySqlCommand cmd2;
                    cmd2 = new MySqlCommand(query1, con1);
                    con1.Open();
                    MySqlDataReader dataReader2;
                    dataReader2 = cmd2.ExecuteReader();
                    double sum_wt = 0, sum_tot = 0;
                    while (dataReader2.Read())
                    {
                        if(dataReader2.GetValue(0).ToString()!="")
                            sum_wt+=Convert.ToDouble(dataReader2.GetValue(0).ToString());
                        if(dataReader2.GetValue(1).ToString()!="")
                            sum_tot+=Convert.ToDouble(dataReader2.GetValue(1).ToString());
                    }
                    dataReader2.Close();
                    con1.Close();
                    MemoryStream ms = new MemoryStream();
                    Byte[] bindata;
                    bindata = (byte[])(dataReader1.GetValue(0));
                    ms.Write(bindata, 0, bindata.Length);
                    var imageFile = new Bitmap(ms);
                    var ratioX = (double)100 / imageFile.Width;
                    var ratioY = (double)100 / imageFile.Height;
                    var ratio = Math.Min(ratioX, ratioY);
                    var newWidth = (int)(imageFile.Width * ratio);
                    var newHeight = (int)(imageFile.Height * ratio);
                    var newImage = new Bitmap(newWidth, newHeight);
                    Graphics.FromImage(newImage).DrawImage(imageFile, 0, 0, newWidth, newHeight);
                    int index = this.metroGrid2.Rows.Count;
                    index++;
                    this.metroGrid2.Rows.Add();
                    (metroGrid2.Rows[row].Cells[0]).Value = row + 1;
                    (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(9).ToString();
                    ((DataGridViewImageCell)metroGrid2.Rows[row].Cells[2]).Value = newImage;
                    (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(1);
                    (metroGrid2.Rows[row].Cells[4]).Value = sum_wt / 5;
                    (metroGrid2.Rows[row].Cells[5]).Value = dataReader1.GetValue(3);
                    double prort = Convert.ToDouble(dataReader1.GetValue(4));
                    double ta = Convert.ToDouble(dataReader1.GetValue(5));
                    double profit = ta - (ta / (1 + (prort * .01))); //sum_tot * (prort * 0.01);
                    (metroGrid2.Rows[row].Cells[6]).Value = Convert.ToInt32(profit);
                    (metroGrid2.Rows[row].Cells[7]).Value = ta;
                    (metroGrid2.Rows[row].Cells[8]).Value = dataReader1.GetValue(6);
                    (metroGrid2.Rows[row].Cells[9]).Value = dataReader1.GetValue(7);
                    (metroGrid2.Rows[row].Cells[10]).Value = dataReader1.GetValue(8);
                    row++;
                }
                dataReader1.Close();
                cmd1.CommandText = "Select exchg_rate,date_pack from inv_" + numericUpDown1.Text.ToString() + ";";
                dataReader1 = cmd1.ExecuteReader();

                while (dataReader1.Read())
                {
                    exchg_rt = Convert.ToDouble(dataReader1.GetValue(0));
                    metroDateTime1.Text = dataReader1.GetValue(1).ToString();
                }
            numericUpDown7.Text = exchg_rt + "";
            numericUpDown7.ReadOnly= true;

        }

        private void numericUpDown7_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown7.Select(0, numericUpDown7.Text.Length);
        }

        private void numericUpDown7_Enter(object sender, EventArgs e)
        {
            numericUpDown7.Select(0, numericUpDown7.Text.Length);
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(numericUpDown7.Text) > 0)
            {
                if (metroLabel1.Text != "")
                {
                    int r = Convert.ToInt32(metroLabel3.Text);

                    String cod = metroGrid2.Rows[r].Cells[1].Value.ToString();
                    cod = cod.ToLower();
                    //                MessageBox.Show(cod);
                    Pack_Item s = new Pack_Item();
                    s.Text = "SUB-PRODUCTS OF ITEM " + metroGrid2.Rows[r].Cells[3].Value.ToString();
                    if (cod.Contains("non"))
                    {
                        s.metroPanel4.Visible = true;
                        s.metroPanel1.Visible = false;
                        s.metroButton9.PerformClick();
                    }
                    else
                    {
                        s.metroPanel4.Visible = false;
                        s.metroPanel1.Visible = true;
                        s.metroButton6.PerformClick();
                    }
                    s.metroLabel13.Text = metroLabel1.Text;
                    s.metroLabel14.Text = "inv_" + numericUpDown1.Text.ToString();
                    s.metroLabel16.Text = numericUpDown7.Text.ToString();
                    if (metroLabel1.Text.ToLower().Contains("-p-"))
                    {
                        s.metroCheckBox1.Checked = true;
                    }
                    s.metroDateTime1.Text = metroDateTime1.Text;
                    s.metroDateTime2.Text = metroDateTime1.Text;
                    s.metroDateTime1.Enabled = false;
                    s.Visible = true;
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\nPLEASE SELECT AN ITEM FIRST", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Packing"].BringToFront();
                    Application.OpenForms["Packing"].Focus();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPLEASE ENTER EXCHANGE RATE FIRST", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Packing"].BringToFront();
                Application.OpenForms["Packing"].Focus();
            }
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            metroButton2.Visible = true;
            metroButton2.PerformClick();
            metroButton2.Visible = false;

            string tabl = numericUpDown1.Text.ToString();
            string strCheck = "SHOW TABLES LIKE \'inv_" + tabl + "\'";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(strCheck, con);
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();
            if (dataReader1.Read())
            {
                metroButton4.Visible = true;
                metroButton4.PerformClick();
                metroButton4.Visible = false;
            }
            else
            {
            }

            con.Close();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Home"].BringToFront();
            this.Dispose();
        }

        private void metroGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                metroLabel3.Text = ""+r;
                metroLabel1.Text = metroGrid2.Rows[r].Cells[1].Value.ToString();
                metroButton7.Enabled = true;
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                String query = "Select lot,stones,wt,rt from inv_"+numericUpDown1.Text.ToString()+" where code='" + metroLabel1.Text + "';";
                MySqlCommand cmd1;
                cmd1 = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader dataReader1;
                dataReader1 = cmd1.ExecuteReader();
                int row = 0, pcs = 0;
                String lot = "";
                double exchg_rt = 0;
                double wt = 0,rt=0;
                while (dataReader1.Read())
                {
                    lot = dataReader1.GetValue(0).ToString();
                    pcs = Convert.ToInt32(dataReader1.GetValue(1).ToString());
                    wt = Convert.ToDouble(dataReader1.GetValue(2).ToString());
                    rt = Convert.ToDouble(dataReader1.GetValue(3).ToString());

                    MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                    String query1 = "update stone set c_pcs=c_pcs+" + pcs + ", c_qty=c_qty+" + wt + ", cr_amt=cr_amt+" + (wt*rt) + " where lot='" + lot + "';";
                    MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                    con2.Open();
                    int r = Convert.ToInt32(metroLabel3.Text);

                String cod = metroGrid2.Rows[r].Cells[1].Value.ToString();
                cod = cod.ToLower();

                if (cod.Contains("non"))
                {
                }
                else
                {
                    row = cmd2.ExecuteNonQuery();
                }
                cmd2.CommandText = "Delete from inv_" + numericUpDown1.Text.ToString() + " where code='" + metroLabel1.Text + "' and lot='" + lot + "';";
                row = cmd2.ExecuteNonQuery();
                }
                dataReader1.Close();
                cmd1.CommandText = "Delete from ledger where table_name='" + numericUpDown1.Text.ToString() + "' and code='" + metroLabel1.Text.ToString() + "';";
                cmd1.ExecuteNonQuery();
                con.Close();
                metroButton6.PerformClick();
                metroLabel1.Text = "";
        }

        private void Packing_Enter(object sender, EventArgs e)
        {
            metroButton2.Visible = true;
            metroButton2.PerformClick();
            metroButton2.Visible = false;

            string tabl = numericUpDown1.Text.ToString();
            string strCheck = "SHOW TABLES LIKE \'inv_" + tabl + "\'";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(strCheck, con);
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();
            if (dataReader1.Read())
            {
                metroButton4.Visible = true;
                metroButton4.PerformClick();
                metroButton4.Visible = false;
            }
            else
            {
            }

            con.Close();

        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            
            String path = @"C:\Silver City\Files\INV " + numericUpDown1.Text.ToString()+" "+metroDateTime1.Text + ".xls";

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns[1].ColumnWidth = 7;
            xlWorkSheet.Columns[2].ColumnWidth = 9;
            xlWorkSheet.Columns[3].ColumnWidth = 31;
            xlWorkSheet.Columns[4].ColumnWidth = 7;
            xlWorkSheet.Columns[5].ColumnWidth = 9;
            xlWorkSheet.Columns[6].ColumnWidth = 9;
            xlWorkSheet.Columns[7].ColumnWidth = 14;
            xlWorkSheet.Columns[8].ColumnWidth = 10;
            xlWorkSheet.Columns[9].ColumnWidth = 8;
            xlWorkSheet.Columns[10].ColumnWidth = 12;
            xlWorkSheet.Columns[11].ColumnWidth = 10;
            xlWorkSheet.Columns[12].ColumnWidth = 10;
            xlWorkSheet.Columns[13].ColumnWidth = 10;
            xlWorkSheet.Columns[14].ColumnWidth = 10;
            xlWorkSheet.Columns[15].ColumnWidth = 10;
            xlWorkSheet.Columns[16].ColumnWidth = 10;

            int i = 0;
            int j = 0;
            String tbnum = "" + numericUpDown1.Text;
            int row=10;
            for (int k = 1; k <= 17; k++)
            {
                Excel.Range curcell2 = (Excel.Range)xlWorkSheet.Cells[1, k];
                curcell2.EntireColumn.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell2.EntireColumn.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            }
            double gl_pro = 0, gl_amtr = 0, gl_amtu = 0, gl_exrate = 0, gl_met = 0, gl_gw = 0, gl_gpwt = 0, gl_lb = 0, gl_set = 0, gl_gch = 0;
            int cou = 0;
                
            for (i = 0; i <= metroGrid2.RowCount - 1; i++)
            {
                String query = "Select item.pic,item.descrip,item.size,ledger.*,sum(pcs),sum(metwt),sum(metwt)+(sum(wt)/5),sum(stones),sum(subtot),sum(wt)/5 from ledger, item,inv_" + tbnum + " where item.code=ledger.code and table_name='" + numericUpDown1.Text.ToString() + "' and ledger.code=inv_" + tbnum + ".code and item.code='"+metroGrid2.Rows[i].Cells[1].Value.ToString()+"';";
                //MessageBox.Show(query);
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader dataReader1;
                dataReader1 = cmd.ExecuteReader();
                while (dataReader1.Read())
                {
                    cou++;
                    //MessageBox.Show("Hey " + dataReader1.GetValue(1) + " Count = " + cou);
                    xlWorkSheet.Cells[row, 2] = dataReader1.GetValue(5).ToString();
                    double pro = 0;
                        Byte[] bindata = (byte[])(dataReader1.GetValue(0));
                        MemoryStream mStream = new MemoryStream(bindata);
                        PictureBox p = new PictureBox();
                        p.Image = Image.FromStream(mStream);
                        p.Image.Save(@"C:\Silver City\Files\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Excel.Range oRange = (Excel.Range)xlWorkSheet.Cells[row, 7];
                    float left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 75;
                    const float Imagehei = 75;
                    xlWorkSheet.Shapes.AddPicture(@"C:\Silver City\Files\temp.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, left, Top, ImageSize, Imagehei);
                    File.Delete(@"C:\Silver City\Files\temp.jpg");
                    oRange.RowHeight = 78;

                    xlWorkSheet.Cells[row, 3] = dataReader1.GetValue(1).ToString();
                    xlWorkSheet.Cells[row, 5] = dataReader1.GetValue(2).ToString();
                    xlWorkSheet.Cells[++row, 1] = "S.No";
                    Excel.Range curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    curcell.EntireRow.Font.Color = Color.Red;
                    curcell.EntireRow.Font.Underline = true;
                    xlWorkSheet.Cells[row, 2] = "Stone Lot";
                    xlWorkSheet.Cells[row, 3] = "Detailed Description";
                    xlWorkSheet.Cells[row, 4] = "Unit";
                    xlWorkSheet.Cells[row, 5] = "No of Pcs";
                    xlWorkSheet.Cells[row, 6] = "Weight";
                    xlWorkSheet.Cells[row, 7] = "No of Stones";
                    xlWorkSheet.Cells[row, 8] = "Metal Wt.";
                    xlWorkSheet.Cells[row, 9] = "Rate";
                    xlWorkSheet.Cells[row, 10] = "Sub Total";
                    xlWorkSheet.Cells[row, 11] = "Cost-INR";
                    xlWorkSheet.Cells[row, 12] = "Cost-US";
                    xlWorkSheet.Cells[row, 13] = "P.Pcs-INR";
                    xlWorkSheet.Cells[row, 14] = "P.Pcs-US";
                    xlWorkSheet.Cells[row, 15] = "P.Grm-INR";
                    xlWorkSheet.Cells[row, 16] = "P.Grm-US";
                    xlWorkSheet.Cells[++row, 3] = "Total No. of Piece";
                    xlWorkSheet.Cells[row, 5] = dataReader1.GetValue(18).ToString();
                    //MessageBox.Show(dataReader1.GetValue(18).ToString());
                    xlWorkSheet.Cells[++row, 3] = "Silver Weight";
                    xlWorkSheet.Cells[row, 4] = "gm";
                    double mtrt = 0;
                    if (Convert.ToDouble(dataReader1.GetValue(6)) > 0)
                    {
                        xlWorkSheet.Cells[row, 6] = dataReader1.GetValue(19).ToString();
                        xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(6).ToString();
                        mtrt = Convert.ToDouble(dataReader1.GetValue(6));
                    }
                    xlWorkSheet.Cells[++row, 3] = "Brass Weight";
                    xlWorkSheet.Cells[row, 4] = "gm";
                    if (Convert.ToDouble(dataReader1.GetValue(7)) > 0)
                    {
                        xlWorkSheet.Cells[row, 6] = dataReader1.GetValue(19).ToString();
                        xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(7).ToString();
                        mtrt = Convert.ToDouble(dataReader1.GetValue(7));
                    }

                    String query1 = "Select * from inv_" + tbnum + " where inv_" + tbnum + ".code='" + dataReader1.GetValue(5) + "';";
                    //MessageBox.Show(query1);
                    MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                    MySqlCommand cmd1 = new MySqlCommand(query1, con1);
                    con1.Open();
                    MySqlDataReader dataReader2;
                    dataReader2 = cmd1.ExecuteReader();
                    int count = 0;
                    double subt = 0;
                        while (dataReader2.Read())
                    {
                            xlWorkSheet.Cells[++row, 1] = ++count;
                            int lt1 = Convert.ToInt32(dataReader2.GetValue(4));
                            if (lt1 != 103 && lt1 != 866)
                                xlWorkSheet.Cells[row, 2] = lt1;
                            else
                                xlWorkSheet.Cells[row, 2] = dataReader2.GetValue(4);
                            xlWorkSheet.Cells[row, 3] = dataReader2.GetValue(2);
                            xlWorkSheet.Cells[row, 4] = dataReader2.GetValue(3);
                            xlWorkSheet.Cells[row, 5] = dataReader2.GetValue(5);
                            xlWorkSheet.Cells[row, 6] = dataReader2.GetValue(6);
                            xlWorkSheet.Cells[row, 7] = dataReader2.GetValue(7);
                            xlWorkSheet.Cells[row, 8] = dataReader2.GetValue(8);
                            gl_met += Convert.ToDouble(dataReader2.GetValue(8));
                            xlWorkSheet.Cells[row, 9] = dataReader2.GetValue(9);
                            xlWorkSheet.Cells[row, 10] = dataReader2.GetValue(10);
                            subt += Convert.ToDouble(dataReader2.GetValue(10));
                            xlWorkSheet.Cells[row, 11] = "$ " + dataReader2.GetValue(11);
                            gl_amtr += Convert.ToDouble(dataReader2.GetValue(11));
                            gl_amtu += Convert.ToDouble(dataReader2.GetValue(12));
                            xlWorkSheet.Cells[row, 12] = "" + dataReader2.GetValue(12);
                            xlWorkSheet.Cells[row, 13] = "$ " + dataReader2.GetValue(13);
                            xlWorkSheet.Cells[row, 14] = "" + dataReader2.GetValue(14);
                            xlWorkSheet.Cells[row, 15] = "$ " + dataReader2.GetValue(15);
                            xlWorkSheet.Cells[row, 16] = "" + dataReader2.GetValue(16);
                            gl_exrate = Convert.ToDouble(dataReader2.GetValue(18));
                    }
                    xlWorkSheet.Cells[++row, 3] = "Gross Weight";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    curcell.EntireRow.Font.Color = Color.Blue;
                    xlWorkSheet.Cells[row, 5] = dataReader1.GetValue(18).ToString();
                    
                    double grshu = 0;
                    if (dataReader2.GetValue(1).ToString().ToLower().Contains("non"))
                    {
                        //grshu = Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 5;
                        grshu = Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5;
                        //MessageBox.Show("20 = " + Convert.ToDouble(dataReader1.GetValue(20).ToString())+"\nTotal = "+grshu);
                        xlWorkSheet.Cells[row, 6] = Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5;
                        //    MessageBox.Show(dataReader1.GetValue(1).ToString());
                    }
                    else
                    {
                        grshu = Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 1;
                        xlWorkSheet.Cells[row, 6] = Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 1;
                    }
                    xlWorkSheet.Cells[row, 7] = dataReader1.GetValue(21).ToString();
                    xlWorkSheet.Cells[row, 8] = dataReader1.GetValue(19).ToString();

                    xlWorkSheet.Cells[++row, 3] = "LABOUR CHARGE";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    xlWorkSheet.Cells[row, 4] = "Pcs";
                    xlWorkSheet.Cells[row, 16] = Convert.ToDouble(dataReader1.GetValue(20)) - Convert.ToDouble(dataReader1.GetValue(19));
                    xlWorkSheet.Cells[row, 16].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 14] = "Total Stone Weight (GMS)";
                    xlWorkSheet.Cells[row, 14].Font.Color = Color.Brown;
                    double lb_q = 0;
                    if (dataReader1.GetValue(5).ToString().ToLower().Contains("-p-"))
                        lb_q=Convert.ToDouble(dataReader1.GetValue(18).ToString());
                    else
                        lb_q = Convert.ToDouble(dataReader1.GetValue(19).ToString());

                    xlWorkSheet.Cells[row, 6] = lb_q.ToString();
                    xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(8).ToString();
                    xlWorkSheet.Cells[row, 10] = Convert.ToDouble(dataReader1.GetValue(8).ToString()) * lb_q;
                    gl_lb += Convert.ToDouble(dataReader1.GetValue(8).ToString()) * lb_q;
                    pro += Convert.ToDouble(dataReader1.GetValue(8).ToString()) * lb_q;

                    xlWorkSheet.Cells[++row, 3] = "SETTING CHARGE";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    xlWorkSheet.Cells[row, 4] = "Pcs";
                    xlWorkSheet.Cells[row, 16] = subt - (lb_q * mtrt);
                    xlWorkSheet.Cells[row, 16].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 14] = "Total Stone Amount";
                    xlWorkSheet.Cells[row, 14].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 6] = dataReader1.GetValue(21).ToString();
                    xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(9).ToString();
                    xlWorkSheet.Cells[row, 10] = Convert.ToDouble(dataReader1.GetValue(9).ToString()) * Convert.ToDouble(dataReader1.GetValue(21).ToString());
                    gl_set += Convert.ToDouble(dataReader1.GetValue(9).ToString()) * Convert.ToDouble(dataReader1.GetValue(21).ToString());
                    pro += Convert.ToDouble(dataReader1.GetValue(9).ToString()) * Convert.ToDouble(dataReader1.GetValue(21).ToString());

                    xlWorkSheet.Cells[++row, 3] = "GOLD PLATING CHARGE";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    xlWorkSheet.Cells[row, 4] = "Gm";
                    xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(10).ToString();
                    if (dataReader2.GetValue(1).ToString().ToLower().Contains("non"))
                    {
                        xlWorkSheet.Cells[row, 6] = Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5;
                        xlWorkSheet.Cells[row, 16] = Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5;
                        gl_gpwt += (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);// Convert.ToDouble(dataReader1.GetValue(20)) * 5;
                        gl_gw += (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);// Convert.ToDouble(dataReader1.GetValue(20)) * 5;
                        xlWorkSheet.Cells[row, 10] = Convert.ToDouble(dataReader1.GetValue(10).ToString()) * (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);//Convert.ToDouble(dataReader1.GetValue(20).ToString())*5;
                        gl_gch += Convert.ToDouble(dataReader1.GetValue(10).ToString()) * (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);// Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 5;
                        pro += Convert.ToDouble(dataReader1.GetValue(10).ToString()) * (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);// Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 5;

                    }
                    else
                    {
                        xlWorkSheet.Cells[row, 6] = Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 1;
                        xlWorkSheet.Cells[row, 16] = Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 1;
                        gl_gw += Convert.ToDouble(dataReader1.GetValue(20));
                        gl_gpwt += Convert.ToDouble(dataReader1.GetValue(20));
                        xlWorkSheet.Cells[row, 10] = Convert.ToDouble(dataReader1.GetValue(10).ToString()) * Convert.ToDouble(dataReader1.GetValue(20).ToString());
                        gl_gch += Convert.ToDouble(dataReader1.GetValue(10).ToString()) * Convert.ToDouble(dataReader1.GetValue(20).ToString());
                        pro += Convert.ToDouble(dataReader1.GetValue(10).ToString()) * Convert.ToDouble(dataReader1.GetValue(20).ToString());

                    }

                    xlWorkSheet.Cells[row, 16].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 14] = "Total Gold Plating Weight";
                    xlWorkSheet.Cells[row, 14].Font.Color = Color.Brown;
                    
                    xlWorkSheet.Cells[++row, 3] = "SILVER PLATING CHARGE";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    xlWorkSheet.Cells[row, 4] = "Gm";
                    if (dataReader2.GetValue(1).ToString().ToLower().Contains("non"))
                    {
                        xlWorkSheet.Cells[row, 6] = (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);// Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 5;
                        xlWorkSheet.Cells[row, 10] = Convert.ToDouble(dataReader1.GetValue(11).ToString()) * (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5);//Convert.ToDouble(dataReader1.GetValue(20).ToString())*5;
                        pro += Convert.ToDouble(dataReader1.GetValue(11).ToString()) * (Convert.ToDouble(dataReader1.GetValue(19).ToString()) + Convert.ToDouble(dataReader1.GetValue(23).ToString()) * 5); //Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 5;
                    }
                    else
                    {
                        xlWorkSheet.Cells[row, 6] = Convert.ToDouble(dataReader1.GetValue(20).ToString()) * 1;
                        xlWorkSheet.Cells[row, 10] = Convert.ToDouble(dataReader1.GetValue(11).ToString()) * Convert.ToDouble(dataReader1.GetValue(20).ToString());
                        pro += Convert.ToDouble(dataReader1.GetValue(11).ToString()) * Convert.ToDouble(dataReader1.GetValue(20).ToString());
                    }
                    xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(11).ToString();
                    xlWorkSheet.Cells[row, 16] = Convert.ToDouble(dataReader1.GetValue(17).ToString())/grshu;//dataReader1.GetValue(15).ToString();
                    xlWorkSheet.Cells[row, 16].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 14] = "Price Per Gram in US $";
                    xlWorkSheet.Cells[row, 14].Font.Color = Color.Brown;

                    
                    xlWorkSheet.Cells[++row, 3] = "PROFIT";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    xlWorkSheet.Cells[row, 16] = dataReader1.GetValue(16).ToString();
                    xlWorkSheet.Cells[row, 16].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 14] = "Price Per Piece in US $";
                    xlWorkSheet.Cells[row, 14].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 9] = dataReader1.GetValue(12).ToString() + "%";
                    double tshu = Convert.ToDouble(dataReader1.GetValue(13).ToString());
                    double pshurt = Convert.ToDouble(dataReader1.GetValue(12).ToString());
                    double profit = tshu - (tshu / (1 + (pshurt * 0.01)));
                    gl_pro += profit;
                    xlWorkSheet.Cells[row, 10] = Convert.ToInt32(profit);

                    xlWorkSheet.Cells[++row, 3] = "Total Amount in Rs.";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Bold = true;
                    xlWorkSheet.Cells[row, 16] = dataReader1.GetValue(17).ToString();
                    xlWorkSheet.Cells[row, 16].Font.Color = Color.Brown;
                    xlWorkSheet.Cells[row, 14] = "Total Amount in US $";
                    xlWorkSheet.Cells[row, 14].Font.Color = Color.Brown;

                    xlWorkSheet.Cells[row, 10] = dataReader1.GetValue(13);

                    xlWorkSheet.Cells[++row, 3] = "Avg. Price per gram in Rs.";
                    curcell = (Excel.Range)xlWorkSheet.Cells[row, 1];
                    curcell.EntireRow.Font.Color = Color.Brown;
                    curcell.EntireRow.Font.Bold = true;
                    curcell.EntireRow.AutoFit();

                    xlWorkSheet.Cells[row, 10] = Convert.ToInt32(Convert.ToDouble(dataReader1.GetValue(13))/grshu);

                    row += 3;
                }
                dataReader1.Close();
                con.Close();
            }
            xlWorkSheet.Cells[1, 4] = "Profit in Rs.";
            Excel.Range curcell3 = (Excel.Range)xlWorkSheet.Cells[1, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[1, 6] = gl_pro;
            xlWorkSheet.Cells[1, 8] = "Date";
            xlWorkSheet.Cells[1, 10] = metroDateTime1.Text;

            xlWorkSheet.Cells[2, 4] = "Total Invoice Amount in Rs.";
            curcell3 = (Excel.Range)xlWorkSheet.Cells[2, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[2, 6] = gl_amtr;
            xlWorkSheet.Cells[2, 8] = "Invoice #";
            xlWorkSheet.Cells[2, 10] = numericUpDown1.Text;

            xlWorkSheet.Cells[3, 4] = "Total Invoice Amount in USD";
            curcell3 = (Excel.Range)xlWorkSheet.Cells[3, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[3, 6] = gl_amtu;
            xlWorkSheet.Cells[3, 8] = "Exchange Rate";
            xlWorkSheet.Cells[3, 10] = gl_exrate;

            xlWorkSheet.Cells[4, 4] = "Total Silver Used .999";
            curcell3 = (Excel.Range)xlWorkSheet.Cells[4, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[4, 6] = gl_met * .925;

            xlWorkSheet.Cells[5, 4] = "Total Silver Used .925";
            curcell3 = (Excel.Range)xlWorkSheet.Cells[5, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[5, 6] = gl_met;

            xlWorkSheet.Cells[6, 4] = "Gross Weight";
            curcell3 = (Excel.Range)xlWorkSheet.Cells[6, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[6, 6] = gl_gw;
            xlWorkSheet.Cells[6, 8] = "Total Labour Charges";
            xlWorkSheet.Cells[6, 10] = gl_lb;

            xlWorkSheet.Cells[7, 4] = "Total Gold Plating Weight";
            curcell3 = (Excel.Range)xlWorkSheet.Cells[7, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[7, 6] = gl_gpwt;
            xlWorkSheet.Cells[7, 8] = "Total Gold Plating Charges";
            xlWorkSheet.Cells[7, 10] = gl_gch;

            curcell3 = (Excel.Range)xlWorkSheet.Cells[8, 1];
            curcell3.EntireRow.Font.Bold = true;
            curcell3.EntireRow.Font.Color = Color.Blue;
            xlWorkSheet.Cells[8, 8] = "Total Setting Charges";
            xlWorkSheet.Cells[8, 10] = gl_set;
            
            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            Application.OpenForms["Home"].BringToFront();
            Close();
            System.Diagnostics.Process.Start(path);

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void metroGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            String q1 = "Select descrip from item where code='"+metroComboBox6.SelectedItem.ToString()+"';";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            con1.Open();
            var dataReader1 = cmd1.ExecuteReader();
            if (dataReader1.Read())
                metroLabel4.Text = dataReader1.GetValue(0).ToString();
            con1.Close();
        }

    }
}
