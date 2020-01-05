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

using Excel = Microsoft.Office.Interop.Excel; 


namespace Surya
{
    public partial class All_Stock : MetroForm
    {
        public All_Stock()
        {
            InitializeComponent();
        }

        private void All_Stock_Load(object sender, EventArgs e)
        {
            metroDateTime1.Format = DateTimePickerFormat.Custom;
            metroDateTime1.CustomFormat = "yyyy-MM-dd";
            metroDateTime2.Format = DateTimePickerFormat.Custom;
            metroDateTime2.CustomFormat = "yyyy-MM-dd";

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            int sele = 0;
            String q1 = "";
            if (metroLabel1.Text.ToString().Equals("all"))
            {
                q1 = "Select * from invent order by date_pur;";
                sele = 1;
            }
            else if (metroLabel1.Text.ToString() == "available")
            {
                q1 = "Select * from invent as i where i.netAmt>(Select SUM(amt) from history where history.id=i.id group by history.id) or i.id not in (Select distinct history.id from history) order by date_in;";
                sele = 2;
            }
            else if (metroLabel1.Text == "sold")
            {
                q1 = "Select * from invent as i where i.netAmt<=(Select SUM(amt) from history where history.id=i.id group by history.id) order by date_in;";
                sele = 4;
            }
            
            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            int row = 0;
            String s1 = "";
            int count = 1;
            double wt = 0;
 
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            double namt = 0;
            int index = 0;
            while (dataReader1.Read())
            {
                if(index!=0)
                    index = this.metroGrid1.Rows.Count;

                index++;

                this.metroGrid1.Rows.Add();

                (metroGrid1.Rows[row].Cells[0]).Value = count;
                (metroGrid1.Rows[row].Cells[1]).Value = dataReader1.GetValue(0);
                (metroGrid1.Rows[row].Cells[2]).Value = dataReader1.GetValue(1);
                s1 = dataReader1.GetValue(2).ToString();
                DateTime d = Convert.ToDateTime(s1);
                (metroGrid1.Rows[row].Cells[3]).Value = d.ToShortDateString();
                (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(3);
                (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                wt = wt + Convert.ToDouble(dataReader1.GetValue(5));
                (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                    
                (metroGrid1.Rows[row].Cells[7]).Value = dataReader1.GetValue(6);
                (metroGrid1.Rows[row].Cells[8]).Value = dataReader1.GetValue(7);
                (metroGrid1.Rows[row].Cells[9]).Value = dataReader1.GetValue(8);
                (metroGrid1.Rows[row].Cells[10]).Value = dataReader1.GetValue(9);
                (metroGrid1.Rows[row].Cells[11]).Value = dataReader1.GetValue(10);
                
                (metroGrid1.Rows[row].Cells[12]).Value = dataReader1.GetValue(11);
                (metroGrid1.Rows[row].Cells[13]).Value = dataReader1.GetValue(12);
                (metroGrid1.Rows[row].Cells[14]).Value = dataReader1.GetValue(13);
                (metroGrid1.Rows[row].Cells[15]).Value = dataReader1.GetValue(14);
                (metroGrid1.Rows[row].Cells[16]).Value = dataReader1.GetValue(15);
                namt = namt + Convert.ToDouble(dataReader1.GetValue(15));
                (metroGrid1.Rows[row].Cells[17]).Value = dataReader1.GetValue(16);
                (metroGrid1.Rows[row].Cells[18]).Value = dataReader1.GetValue(17);
                (metroGrid1.Rows[row].Cells[19]).Value = dataReader1.GetValue(18);
                (metroGrid1.Rows[row].Cells[20]).Value = dataReader1.GetValue(19);
                (metroGrid1.Rows[row].Cells[21]).Value = dataReader1.GetValue(20);
                (metroGrid1.Rows[row].Cells[22]).Value = dataReader1.GetValue(21);
                (metroGrid1.Rows[row].Cells[23]).Value = dataReader1.GetValue(22);
                //(metroGrid1.Rows[row].Cells[24]).Value = dataReader1.GetValue(23);
                count++;
                row++;
            }
            metroLabel2.Text = "" + sele;
            metroTextBox2.Text = ""+namt;
            metroTextBox3.Text = "" + wt;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            String path = "";
            String sel = metroLabel2.Text;
            if (sel.Equals("1"))
            {
                path = @"C:\Bhandari Soft\Files\All Transactions.xlsx";
            }
            else if (sel.Equals("2"))
            {
                path = @"C:\Bhandari Soft\Files\Pending Transactions.xlsx";
            }
            else if (sel.Equals("4"))
            {
                path = @"C:\Bhandari Soft\Files\Cleared Transactions.xlsx";
            }
            
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns[1].ColumnWidth = 5;
            xlWorkSheet.Columns[2].ColumnWidth = 10;
            xlWorkSheet.Columns[3].ColumnWidth = 10;
            xlWorkSheet.Columns[4].ColumnWidth = 15;
            xlWorkSheet.Columns[5].ColumnWidth = 9;
            xlWorkSheet.Columns[6].ColumnWidth = 10;
            xlWorkSheet.Columns[7].ColumnWidth = 9;
            xlWorkSheet.Columns[8].ColumnWidth = 9;
            xlWorkSheet.Columns[9].ColumnWidth = 9;
            xlWorkSheet.Columns[10].ColumnWidth = 9;
            xlWorkSheet.Columns[11].ColumnWidth = 13;
            xlWorkSheet.Columns[12].ColumnWidth = 11;
            xlWorkSheet.Columns[13].ColumnWidth = 30;
            xlWorkSheet.Columns[14].ColumnWidth = 9;
            xlWorkSheet.Columns[15].ColumnWidth = 9;
            xlWorkSheet.Columns[16].ColumnWidth = 10;
            xlWorkSheet.Columns[17].ColumnWidth = 13;
            xlWorkSheet.Columns[18].ColumnWidth = 6;
            xlWorkSheet.Columns[19].ColumnWidth = 8;
            xlWorkSheet.Columns[20].ColumnWidth = 6;
            xlWorkSheet.Columns[21].ColumnWidth = 6;
            xlWorkSheet.Columns[22].ColumnWidth = 20;
            xlWorkSheet.Columns[23].ColumnWidth = 9;
            xlWorkSheet.Columns[24].ColumnWidth = 20;
            

            int i = 0;

            int j = 0;
            xlWorkSheet.Cells[1, 1] = "S.No";
            xlWorkSheet.Cells[1, 2] = "Unique ID";
            Excel.Range curcell = xlApp.ActiveCell;
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Color = Color.Red;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlWorkSheet.Cells[1, 3] = "Status";
            xlWorkSheet.Cells[1, 4] = "Date of Purchase";
            xlWorkSheet.Cells[1, 5] = "CPC";
            xlWorkSheet.Cells[1, 6] = "Shape";
            xlWorkSheet.Cells[1, 7] = "Weight (ct)";
            xlWorkSheet.Cells[1, 8] = "Color";
            xlWorkSheet.Cells[1, 9] = "CPS";
            xlWorkSheet.Cells[1, 10] = "Purity";
            
            xlWorkSheet.Cells[1, 11] = "FL";
            xlWorkSheet.Cells[1, 12] = "CertiType";
            xlWorkSheet.Cells[1, 13] = "Certi Link";
            xlWorkSheet.Cells[1, 14] = "RAP";
            xlWorkSheet.Cells[1, 15] = "Disc";
            xlWorkSheet.Cells[1, 16] = "Rate";
            xlWorkSheet.Cells[1, 17] = "Amount";
            xlWorkSheet.Cells[1, 18] = "BGM";
            xlWorkSheet.Cells[1, 19] = "Black";
            xlWorkSheet.Cells[1, 20] = "TD";
            xlWorkSheet.Cells[1, 21] = "TA";
            xlWorkSheet.Cells[1, 22] = "Measure";
            xlWorkSheet.Cells[1, 23] = "Pair Code";
            xlWorkSheet.Cells[1, 24] = "Specs";

            for (i = 0; i <= metroGrid1.RowCount - 1; i++)
            {
                Excel.Range curcell2 = (Excel.Range)xlWorkSheet.Cells[i + 2, 1];
                curcell2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell2.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                for (j = 0; j <= metroGrid1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = metroGrid1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                }
            }

            xlWorkSheet.Cells[i + 3, 5] = "TOTAL";
            xlWorkSheet.Cells[i + 3, 12] = metroTextBox2.Text;


            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            Application.OpenForms["Home"].BringToFront();
            System.Diagnostics.Process.Start(path);
            
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroLabel1.Text = "Hello";
        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            if (metroComboBox1.SelectedItem.ToString().Equals("Color"))
                metroTextBox1.PromptText = "Enter Color Code here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Date of Inventory"))
                metroTextBox1.PromptText = "Enter date in yyyy-mm-dd format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Month of Inventory"))
                metroTextBox1.PromptText = "Enter month in yyyy-mm format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Certificate Type"))
                metroTextBox1.PromptText = "Please Specify Certificate Code here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Shape"))
                metroTextBox1.PromptText = "Please Enter Shape Name here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Status"))
                metroTextBox1.PromptText = "Available / Sold / Memo / Hold / K / B";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Unique ID"))
                metroTextBox1.PromptText = "Please Enter Unique ID here";
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox3.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox4.SelectedIndex) != -1)
            {
                metroGrid1.Rows.Clear();
                metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                int sele = 0;
                String q1 = "";
                int stt = 0;
                if (metroLabel1.Text.ToString().Equals("all"))
                {
                    q1 = "Select * from invent where ";
                    sele = 1;
                }
                else if (metroLabel1.Text.ToString() == "available")
                {
                    q1 = "Select * from invent where i.netAmt>(Select SUM(amt) from history where history.id=i.id group by history.id) or i.id not in (Select distinct history.id from history) and ";//order by date_in;";
                    sele = 2;
                }
                else if (metroLabel1.Text == "sold")
                {
                    q1 = "Select * from invent where i.netAmt<=(Select SUM(amt) from history where history.id=i.id group by history.id) and ";//order by date_in;";
                    sele = 4;
                }
                if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1)
                {
                    if (metroComboBox1.SelectedItem.ToString().Equals("Certificate Type"))
                    {
                        q1 = q1 + "certitype='" + metroTextBox1.Text + "' ";
                    }
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Shape"))
                    {
                        q1 = q1 + "shape='" + metroTextBox1.Text + "' ";
                    }
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Status"))
                    {
                        q1 = q1 + "status='" + metroTextBox1.Text + "' ";
                    }
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Unique ID"))
                    {
                        q1 = q1 + "id='" + metroTextBox1.Text + "' ";
                    }
                    stt = 1;
                }
                if (Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 && metroTextBox4.Text.Length > 0)
                {
                    String[] s11 = new String[100];
                    String s = metroTextBox4.Text.ToString();
                    s = s.Trim();
                    s = s.ToUpper();
                    s11 = s.Split(',');
                    int i = 0, c1 = 0;
                    String p = "";
                    for (i = 0; i < s11.Length; i++)
                    {
                        p = p + "'" + s11[i] + "',";
                        c1++;
                    }
                    String p1 = p.Substring(0, (p.Length - 1));
                    if (stt == 1)
                        q1 = q1 + metroComboBox2.SelectedItem.ToString() + " color in (" + p1 + ") ";
                    else
                    {
                        q1 = q1 + " color in (" + p1 + ") ";
                        stt = 1;
                    }
                }
                if (Convert.ToInt32(metroComboBox3.SelectedIndex) != -1)
                {
                    String d1 = metroDateTime1.Text;
                    String d2 = metroDateTime2.Text;
                    if (stt == 1)
                        q1 = q1 + metroComboBox3.SelectedItem.ToString() + " date_pur between '" + d1 + "' and '" + d2 + "' ";
                    else
                    {
                        q1 = q1 + " date_pur between '" + d1 + "' and '" + d2 + "' ";
                        stt = 1;
                    }
                }

                if (Convert.ToInt32(metroComboBox4.SelectedIndex) != -1 && metroTextBox5.Text.Length > 0 && metroTextBox6.Text.Length > 0)
                {
                    if (stt == 1)
                        q1 = q1 + metroComboBox4.SelectedItem.ToString() + " wt between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                    else
                    {
                        q1 = q1 + " wt between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                        stt = 1;
                    }
                }
                q1 = q1 + "order by date_pur;";
                foreach (DataGridViewColumn col in metroGrid1.Columns)
                {
                    //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                int row = 0;
                String s1 = "";
                int count = 1;
                double wt = 0;

                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
                MySqlCommand cmd1 = new MySqlCommand(q1, con1);
                MySqlDataReader dataReader1;
                con1.Open();
                dataReader1 = cmd1.ExecuteReader();
                double namt = 0;
                int index = 0;
                while (dataReader1.Read())
                {


                    if (index != 0)
                        index = this.metroGrid1.Rows.Count;

                    index++;

                    this.metroGrid1.Rows.Add();

                    (metroGrid1.Rows[row].Cells[0]).Value = count;
                    (metroGrid1.Rows[row].Cells[1]).Value = dataReader1.GetValue(0);
                    (metroGrid1.Rows[row].Cells[2]).Value = dataReader1.GetValue(1);
                    s1 = dataReader1.GetValue(2).ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid1.Rows[row].Cells[3]).Value = d.ToShortDateString();
                    (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(3);
                    (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                    wt = wt + Convert.ToDouble(dataReader1.GetValue(5));
                    (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                    (metroGrid1.Rows[row].Cells[7]).Value = dataReader1.GetValue(6);
                    (metroGrid1.Rows[row].Cells[8]).Value = dataReader1.GetValue(7);
                    (metroGrid1.Rows[row].Cells[9]).Value = dataReader1.GetValue(8);
                    (metroGrid1.Rows[row].Cells[10]).Value = dataReader1.GetValue(9);
                    (metroGrid1.Rows[row].Cells[11]).Value = dataReader1.GetValue(10);

                    (metroGrid1.Rows[row].Cells[12]).Value = dataReader1.GetValue(11);
                    (metroGrid1.Rows[row].Cells[13]).Value = dataReader1.GetValue(12);
                    (metroGrid1.Rows[row].Cells[14]).Value = dataReader1.GetValue(13);
                    (metroGrid1.Rows[row].Cells[15]).Value = dataReader1.GetValue(14);
                    (metroGrid1.Rows[row].Cells[16]).Value = dataReader1.GetValue(15);
                    namt = namt + Convert.ToDouble(dataReader1.GetValue(15));
                    (metroGrid1.Rows[row].Cells[17]).Value = dataReader1.GetValue(16);
                    (metroGrid1.Rows[row].Cells[18]).Value = dataReader1.GetValue(17);
                    String td = "", ta = "";
                    if (Convert.ToInt32(dataReader1.GetValue(18)) == 0)
                        td = "N.A";
                    else
                        td = "" + dataReader1.GetValue(18);
                    if (Convert.ToInt32(dataReader1.GetValue(19)) == 0)
                        ta = "N.A";
                    else
                        ta = "" + dataReader1.GetValue(19);

                    (metroGrid1.Rows[row].Cells[19]).Value = td;
                    (metroGrid1.Rows[row].Cells[20]).Value = ta;
                    (metroGrid1.Rows[row].Cells[21]).Value = dataReader1.GetValue(20);
                    (metroGrid1.Rows[row].Cells[22]).Value = dataReader1.GetValue(21);
                    (metroGrid1.Rows[row].Cells[23]).Value = dataReader1.GetValue(22);
                    //(metroGrid1.Rows[row].Cells[24]).Value = dataReader1.GetValue(23);
                    count++;
                    row++;
                }
                metroLabel2.Text = "" + sele;
                metroTextBox2.Text = "" + namt;
                metroTextBox3.Text = "" + wt;
            }
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

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                int r = e.RowIndex;
                String s = metroGrid1.Rows[r].Cells[1].Value.ToString();
                String path = @"C:\Bhandari Soft\Certi\"+s+".pdf";
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch (Exception e1)
                {
                    path = @"C:\Bhandari Soft\Certi\" + s + ".doc";
                    try
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    catch (Exception e2)
                    {
                        path = @"C:\Bhandari Soft\Certi\" + s + ".docx";
                        try
                        {
                            System.Diagnostics.Process.Start(path);
                        }
                        catch (Exception e3)
                        {
                            MetroMessageBox.Show(this, "\n\nNO CERTIFICATE FOUND FOR THIS ENTRY!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

        private void metroGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                int r = e.RowIndex;
                String s = metroGrid1.Rows[r].Cells[2].Value.ToString();
                metroLabel6.Text = metroGrid1.Rows[r].Cells[1].Value.ToString();

                if(s.Equals("S"))
                {
                    MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
                    MySqlCommand cmd1 = new MySqlCommand("Select date_clear from sell where specs='"+metroLabel6.Text+"';", con1);
                    MySqlDataReader dataReader1;
                    con1.Open();
                    String c = "";
                    dataReader1 = cmd1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        c = dataReader1.GetValue(0).ToString();
                    }
                    if (c.Length==0)
                        metroButton5.Enabled = true;
                    else
                        metroButton5.Enabled = false;
                    metroButton6.Enabled=true;
                    metroButton4.Enabled = false;
                    metroButton7.Enabled = true;

                }
                else
                {
                    metroButton4.Enabled=true;
                    metroButton5.Enabled = false;
                    metroButton7.Enabled=true;
                    metroButton6.Enabled=true;

                }


        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            EditStock e1 = new EditStock();
            e1.metroLabel13.Text = metroLabel6.Text;
            e1.Visible = true;
            
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            invent e1 = new invent();
            e1.metroTextBox2.Text = metroLabel6.Text;
            e1.Visible = true;
            
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Cash_Depo e1 = new Cash_Depo();
            e1.Visible = true;
            e1.metroComboBox1.SelectedItem = metroLabel6.Text;
            
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            String s = metroLabel6.Text.ToString();
            DialogResult d = new DialogResult();
            d = MetroMessageBox.Show(this, "\n\nAre Your Sure you want to delete this Entry?\nEntry Once deleted cant be recovered Back..", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
                    metroButton1.PerformClick();
                }
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
