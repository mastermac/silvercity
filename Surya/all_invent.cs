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
using Excel = Microsoft.Office.Interop.Excel;


namespace Surya
{
    public partial class all_invent : MetroForm
    {
        public all_invent()
        {
            InitializeComponent();
        }

        private void all_invent_Load(object sender, EventArgs e)
        {
            metroDateTime1.Format = DateTimePickerFormat.Custom;
            metroDateTime1.CustomFormat = "yyyy-MM-dd";
            metroDateTime2.Format = DateTimePickerFormat.Custom;
            metroDateTime2.CustomFormat = "yyyy-MM-dd";

            //metroButton4.PerformClick();
            metroGrid1.Rows.Clear();
            DataSet ds = GetDataSet();
            int index = 0;
            String s1 = "";
            double t_pcs = 0, t_qty = 0, t_amt = 0, p_pcs = 0, p_qty = 0, p_amt = 0;
            foreach (DataRow addressRow in ds.Tables[0].Rows)
            //{
            //Parallel.ForEach(ds.Tables[0].AsEnumerable(), addressRow =>
                {
                    //if (index != 0)
                      //  index = this.metroGrid1.Rows.Count;
                    index++;
                    this.metroGrid1.Rows.Add();
                    (metroGrid1.Rows[index - 1].Cells[0]).Value = index;
                    int lt1 = Convert.ToInt32(addressRow[0]);
                    if (lt1 != 103 && lt1 != 866)
                        (metroGrid1.Rows[index - 1].Cells[1]).Value = lt1;
                    else
                        (metroGrid1.Rows[index - 1].Cells[1]).Value = addressRow[0];
                    s1 = addressRow[1].ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid1.Rows[index - 1].Cells[2]).Value = d.ToShortDateString();
                    (metroGrid1.Rows[index - 1].Cells[3]).Value = addressRow[2];
                    (metroGrid1.Rows[index - 1].Cells[4]).Value = addressRow[3];
                    (metroGrid1.Rows[index - 1].Cells[5]).Value = addressRow[4];
                    (metroGrid1.Rows[index - 1].Cells[6]).Value = addressRow[5];
                    t_pcs = t_pcs + Convert.ToDouble(addressRow[6]);
                    t_qty = t_qty + Convert.ToDouble(addressRow[7]);
                    p_pcs = p_pcs + Convert.ToDouble(addressRow[9]);
                    p_qty = p_qty + Convert.ToDouble(addressRow[10]);
                    t_amt += Convert.ToDouble(addressRow[15]);
                    p_amt += Convert.ToDouble(addressRow[16]);
                    (metroGrid1.Rows[index - 1].Cells[7]).Value = Convert.ToDouble(addressRow[6]);
                    (metroGrid1.Rows[index - 1].Cells[8]).Value = Convert.ToDouble(addressRow[7]);
                    (metroGrid1.Rows[index - 1].Cells[9]).Value = addressRow[8];
                    (metroGrid1.Rows[index - 1].Cells[10]).Value = Convert.ToDouble(addressRow[9]);
                    (metroGrid1.Rows[index - 1].Cells[11]).Value = Convert.ToDouble(addressRow[10]);
                    (metroGrid1.Rows[index - 1].Cells[12]).Value = addressRow[11];
                    (metroGrid1.Rows[index - 1].Cells[13]).Value = addressRow[12];
                    (metroGrid1.Rows[index - 1].Cells[14]).Value = addressRow[13];
                    (metroGrid1.Rows[index - 1].Cells[15]).Value = addressRow[14];
                    (metroGrid1.Rows[index - 1].Cells[16]).Value = Convert.ToDouble(addressRow[15]);
                    (metroGrid1.Rows[index - 1].Cells[17]).Value = Convert.ToDouble(addressRow[16]);
                    (metroGrid1.Rows[index - 1].Cells[18]).Value = addressRow[17];
                    (metroGrid1.Rows[index - 1].Cells[19]).Value = addressRow[18];
                }//);
            metroTextBox10.Text = "" + t_pcs;
            metroTextBox8.Text = "" + t_qty;
            metroTextBox9.Text = "" + t_amt;

            metroTextBox2.Text = "" + p_pcs;
            metroTextBox3.Text = "" + p_amt;
            metroTextBox7.Text = "" + p_qty;

            metroDateTime1.ResetText();
            metroDateTime2.ResetText();

            metroTextBox1.Text = "";
            metroTextBox4.Text = "";
            metroTextBox5.Text = "";
            metroTextBox6.Text = "";
            metroComboBox1.SelectedIndex = -1;
            metroComboBox2.SelectedIndex = -1;
            metroComboBox3.SelectedIndex = -1;
            metroComboBox4.SelectedIndex = -1;
            metroComboBox5.SelectedIndex = -1;


            metroGrid1.Columns[7].DefaultCellStyle.BackColor = Color.Cyan;
            metroGrid1.Columns[8].DefaultCellStyle.BackColor = Color.Cyan;
            metroGrid1.Columns[9].DefaultCellStyle.BackColor = Color.Cyan;
            metroGrid1.Columns[16].DefaultCellStyle.BackColor = Color.Cyan;

            metroGrid1.Columns[10].DefaultCellStyle.BackColor = Color.GreenYellow;
            metroGrid1.Columns[11].DefaultCellStyle.BackColor = Color.GreenYellow;
            metroGrid1.Columns[12].DefaultCellStyle.BackColor = Color.GreenYellow;
            metroGrid1.Columns[17].DefaultCellStyle.BackColor = Color.GreenYellow;



        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            /*metroGrid1.Rows.Clear();
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            int sele = 0;
            String q1 = "";
            if (metroLabel7.Text.ToString().Equals("all"))
            {
                q1 = "Select * from stone;";
                sele = 1;
            }
            else if (metroLabel7.Text.ToString() == "available")
            {
                q1 = "Select * from stone where c_pcs>0 and c_qty>0;";//order by date_in;";
                sele = 2;
            }
            else if (metroLabel7.Text == "sold")
            {
                q1 = "Select * from stone where c_pcs=0 and c_qty=0;";//order by date_in;";
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

            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            double namt = 0, nbrok = 0, wtt = 0;
            int index = 0;
            double t_pcs = 0, t_qty = 0, t_amt = 0, p_pcs = 0, p_qty = 0, p_amt = 0;
            //Task.Factory.StartNew(() =>
            //{
                while (dataReader1.Read())
                {
                    if (index != 0)
                        index = this.metroGrid1.Rows.Count;

                    index++;

                    this.metroGrid1.Rows.Add();

                    (metroGrid1.Rows[row].Cells[0]).Value = count;
                    int lt1 = Convert.ToInt32(dataReader1.GetValue(0));
                    if (lt1 != 103 && lt1 != 866)
                        (metroGrid1.Rows[row].Cells[1]).Value = lt1;
                    else
                        (metroGrid1.Rows[row].Cells[1]).Value = dataReader1.GetValue(0);
                    s1 = dataReader1.GetValue(1).ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid1.Rows[row].Cells[2]).Value = d.ToShortDateString();
                    (metroGrid1.Rows[row].Cells[3]).Value = dataReader1.GetValue(2);
                    (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(3);
                    (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                    (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                    t_pcs = t_pcs + Convert.ToDouble(dataReader1.GetValue(6));
                    t_qty = t_qty + Convert.ToDouble(dataReader1.GetValue(7));
                    p_pcs = p_pcs + Convert.ToDouble(dataReader1.GetValue(9));
                    p_qty = p_qty + Convert.ToDouble(dataReader1.GetValue(10));
                    t_amt += Convert.ToDouble(dataReader1.GetValue(15));
                    p_amt += Convert.ToDouble(dataReader1.GetValue(16));
                    (metroGrid1.Rows[row].Cells[7]).Value = Convert.ToDouble(dataReader1.GetValue(6));
                    (metroGrid1.Rows[row].Cells[8]).Value = Convert.ToDouble(dataReader1.GetValue(7));
                    (metroGrid1.Rows[row].Cells[9]).Value = dataReader1.GetValue(8);
                    (metroGrid1.Rows[row].Cells[10]).Value = Convert.ToDouble(dataReader1.GetValue(9));
                    (metroGrid1.Rows[row].Cells[11]).Value = Convert.ToDouble(dataReader1.GetValue(10));
                    (metroGrid1.Rows[row].Cells[12]).Value = dataReader1.GetValue(11);
                    (metroGrid1.Rows[row].Cells[13]).Value = dataReader1.GetValue(12);
                    (metroGrid1.Rows[row].Cells[14]).Value = dataReader1.GetValue(13);
                    (metroGrid1.Rows[row].Cells[15]).Value = dataReader1.GetValue(14);
                    (metroGrid1.Rows[row].Cells[16]).Value = Convert.ToDouble(dataReader1.GetValue(15));
                    (metroGrid1.Rows[row].Cells[17]).Value = Convert.ToDouble(dataReader1.GetValue(16));
                    (metroGrid1.Rows[row].Cells[18]).Value = dataReader1.GetValue(17);
                    (metroGrid1.Rows[row].Cells[19]).Value = dataReader1.GetValue(18);
                    count++;
                    row++;
                }
            //});
            //            metroLabel2.Text = "" + sele;
            metroTextBox10.Text = "" + t_pcs;
            metroTextBox8.Text = "" + t_qty;
            metroTextBox9.Text = "" + t_amt;

            metroTextBox2.Text = "" + p_pcs;
            metroTextBox3.Text = "" + p_amt;
            metroTextBox7.Text = "" + p_qty;

            metroDateTime1.ResetText();
            metroDateTime2.ResetText();

            metroTextBox1.Text = "";
            metroTextBox4.Text = "";
            metroTextBox5.Text = "";
            metroTextBox6.Text = "";
            metroComboBox1.SelectedIndex = -1;
            metroComboBox2.SelectedIndex = -1;
            metroComboBox3.SelectedIndex = -1;
            metroComboBox4.SelectedIndex = -1;
            metroComboBox5.SelectedIndex = -1;
            */
//            this.Refresh = true;
            all_invent ne = new all_invent();
            ne.Text=this.Text;
            ne.metroLabel7.Text = this.metroLabel7.Text;
            ne.Visible = true;
            this.Dispose();

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            
            String nm = this.Text;
            
            String path = @"C:\Silver City\Files\" + nm + ".xlsx";
//            String sel = metroLabel2.Text;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[2, 4] = nm;
            
            Excel.Range curcell = (Excel.Range)xlWorkSheet.Cells[2, 1];

            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Size = 20;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlWorkSheet.Columns[1].ColumnWidth = 5;
            xlWorkSheet.Columns[2].ColumnWidth = 10;
            xlWorkSheet.Columns[3].ColumnWidth = 12;
            xlWorkSheet.Columns[4].ColumnWidth = 22;
            xlWorkSheet.Columns[5].ColumnWidth = 9;
            xlWorkSheet.Columns[6].ColumnWidth = 10;
            xlWorkSheet.Columns[7].ColumnWidth = 18;
            xlWorkSheet.Columns[8].ColumnWidth = 7;
            xlWorkSheet.Columns[9].ColumnWidth = 8;
            xlWorkSheet.Columns[10].ColumnWidth = 6;
            xlWorkSheet.Columns[11].ColumnWidth = 7;
            xlWorkSheet.Columns[12].ColumnWidth = 8;
            xlWorkSheet.Columns[13].ColumnWidth = 6;
            xlWorkSheet.Columns[14].ColumnWidth = 6;
            xlWorkSheet.Columns[15].ColumnWidth = 6;
            xlWorkSheet.Columns[16].ColumnWidth = 10;
            xlWorkSheet.Columns[17].ColumnWidth = 13;
            xlWorkSheet.Columns[18].ColumnWidth = 12;
            xlWorkSheet.Columns[19].ColumnWidth = 40;
            xlWorkSheet.Columns[20].ColumnWidth = 4;

            int i = 0;
            int j = 0;
            xlWorkSheet.Cells[4, 1] = "S.No";
            xlWorkSheet.Cells[4, 2] = "Lot No";
            curcell = (Excel.Range)xlWorkSheet.Cells[4, 1];
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Color = Color.Red;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            
            xlWorkSheet.Cells[4, 3] = "Purchase Date";
            xlWorkSheet.Cells[4, 4] = "Stone Name";
            xlWorkSheet.Cells[4, 5] = "Size/mm";
            xlWorkSheet.Cells[4, 6] = "Shape";
            xlWorkSheet.Cells[4, 7] = "Seller Name";
            xlWorkSheet.Cells[4, 8] = "Pieces";
            xlWorkSheet.Cells[4, 9] = "Quantity";
            xlWorkSheet.Cells[4, 10] = "Unit";
            xlWorkSheet.Cells[4, 11] = "Pieces";
            xlWorkSheet.Cells[4, 12] = "Quantity";
            xlWorkSheet.Cells[4, 13] = "Unit";
            xlWorkSheet.Cells[4, 14] = "Cost";
            xlWorkSheet.Cells[4, 15] = "Less";
            xlWorkSheet.Cells[4, 16] = "Net Rate";
            xlWorkSheet.Cells[4, 17] = "Total Amt";
            xlWorkSheet.Cells[4, 18] = "Curr. Value";
            xlWorkSheet.Cells[4, 19] = "Detailed Description";
            xlWorkSheet.Cells[4, 20] = "EC";

            for (i = 0; i <= metroGrid1.RowCount - 1; i++)
            {
                Excel.Range curcell2 = (Excel.Range)xlWorkSheet.Cells[i + 5, 1];
                curcell2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell2.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                for (j = 0; j <= metroGrid1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = metroGrid1[j, i];
                    xlWorkSheet.Cells[i + 5, j + 1] = cell.Value;
                }
            }
            xlWorkSheet.Cells[i + 6, 3] = "TOTAL";
            curcell = (Excel.Range)xlWorkSheet.Cells[i+6, 1];
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Color = Color.Blue;

            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            xlWorkSheet.Cells[i + 6, 8] = metroTextBox10.Text;
            xlWorkSheet.Cells[i + 6, 9] = metroTextBox8.Text;
            xlWorkSheet.Cells[i + 6, 11] = metroTextBox2.Text;
            xlWorkSheet.Cells[i + 6, 12] = metroTextBox7.Text;
            xlWorkSheet.Cells[i + 6, 17] = metroTextBox9.Text;
            xlWorkSheet.Cells[i + 6, 18] = metroTextBox3.Text;

            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            Application.OpenForms["Home"].BringToFront();
            Close();
            System.Diagnostics.Process.Start(path);
            //stopwatch.Stop();
            //MessageBox.Show("" + stopwatch.Elapsed);
                        

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



        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            if (metroComboBox1.SelectedItem.ToString().Equals("Buyer"))
                metroTextBox1.PromptText = "Enter Buyer Name here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Date of Selling"))
                metroTextBox1.PromptText = "Enter date in yyyy-mm-dd format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Month of Selling"))
                metroTextBox1.PromptText = "Enter month in yyyy-mm format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Cleared or Not"))
                metroTextBox1.PromptText = "Enter YES / NO ";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Date Cleared"))
                metroTextBox1.PromptText = "Enter date in yyyy-mm-dd format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Unique ID"))
                metroTextBox1.PromptText = "Please Enter Unique ID here";
        }


        private DataSet GetDataSet_F()
        {
            int sele = 0;
            int stt = 0;
            double wtt = 0;
            String q1 = "Select * from stone where ";

            if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1 && metroTextBox1.Text.Length > 0)
            {
                String[] s11 = new String[100];
                String s = metroTextBox1.Text.ToString();
                s = s.Trim();
                s = s.ToUpper();
                s11 = s.Split(',');
                int i = 0, c1 = 0;
                String p = "";
                for (i = 0; i < s11.Length; i++)
                {
                    p = p + "'" + s11[i].Trim() + "',";
                    c1++;
                }
                String p1 = p.Substring(0, (p.Length - 1));

                stt = 1;
                if (metroComboBox1.SelectedItem.ToString().Equals("Lot No"))
                    q1 = q1 + "Lot in (" + p1 + ") ";
                else if (metroComboBox1.SelectedItem.ToString().Equals("Stone Name"))
                    q1 = q1 + "Stone in (" + p1 + ") ";
                else if (metroComboBox1.SelectedItem.ToString().Equals("Size"))
                    q1 = q1 + "Size in (" + p1 + ") ";
                else if (metroComboBox1.SelectedItem.ToString().Equals("Shape"))
                    q1 = q1 + "Shape in (" + p1 + ") ";
                else if (metroComboBox1.SelectedItem.ToString().Equals("Seller Name"))
                    q1 = q1 + "Seller in (" + p1 + ") ";
            }

            if (Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 && metroTextBox4.Text.Length > 0 && Convert.ToInt32(metroComboBox5.SelectedIndex) != -1)
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
                    q1 = q1 + metroComboBox2.SelectedItem.ToString() + " ";
                if (metroComboBox5.SelectedItem.ToString().Equals("Lot No"))
                    q1 = q1 + "Lot in (" + p1 + ") ";
                else if (metroComboBox5.SelectedItem.ToString().Equals("Stone Name"))
                    q1 = q1 + "Stone in (" + p1 + ") ";
                else if (metroComboBox5.SelectedItem.ToString().Equals("Size"))
                    q1 = q1 + "Size in (" + p1 + ") ";
                else if (metroComboBox5.SelectedItem.ToString().Equals("Shape"))
                    q1 = q1 + "Shape in (" + p1 + ") ";
                else if (metroComboBox5.SelectedItem.ToString().Equals("Seller Name"))
                    q1 = q1 + "Seller in (" + p1 + ") ";

                stt = 1;
            }
            if (Convert.ToInt32(metroComboBox3.SelectedIndex) != -1)
            {
                String d1 = metroDateTime1.Text;
                String d2 = metroDateTime2.Text;
                if (stt == 1)
                    q1 = q1 + metroComboBox3.SelectedItem.ToString() + " dop between '" + d1 + "' and '" + d2 + "' ";
                else
                {
                    q1 = q1 + " dop between '" + d1 + "' and '" + d2 + "' ";
                    stt = 1;
                }
            }

            if (Convert.ToInt32(metroComboBox4.SelectedIndex) != -1 && metroTextBox5.Text.Length > 0 && metroTextBox6.Text.Length > 0)
            {
                if (stt == 1)
                    q1 = q1 + metroComboBox4.SelectedItem.ToString() + " c_qty between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                else
                {
                    q1 = q1 + " c_qty between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                    stt = 1;
                }
            }
            if (metroLabel7.Text.ToString().Equals("all"))
            {
                q1 = q1 + " order by dop;";
                sele = 1;
            }
            else if (metroLabel7.Text.ToString() == "available")
            {
                q1 = q1 + " and c_pcs>0 and c_qty>0 order by dop;";
                sele = 2;
            }
            else if (metroLabel7.Text == "sold")
            {
                q1 = q1 + " and c_pcs=0 and c_qty=0 order by dop;";
                sele = 4;
            }




            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            int row = 0;
            String s1 = "";
            int count = 1;
            MySqlConnection myConn = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            myConn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(q1, myConn);
            cmd.CommandType = CommandType.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            myConn.Close();
            return ds;
        }



        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox5.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox3.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox4.SelectedIndex) != -1)
            {
                metroGrid1.Rows.Clear();
                metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
/*                int sele = 0;
                int stt = 0;
                double wtt = 0;
                String q1 = "Select * from stone where ";
                
                if (Convert.ToInt32(metroComboBox1.SelectedIndex) != -1 && metroTextBox1.Text.Length>0)
                {
                    String[] s11 = new String[100];
                    String s = metroTextBox1.Text.ToString();
                    s = s.Trim();
                    s = s.ToUpper();
                    s11 = s.Split(',');
                    int i = 0, c1 = 0;
                    String p = "";
                    for (i = 0; i < s11.Length; i++)
                    {
                        p = p + "'" + s11[i].Trim() + "',";
                        c1++;
                    }
                    String p1 = p.Substring(0, (p.Length - 1));

                    stt = 1;
                    if (metroComboBox1.SelectedItem.ToString().Equals("Lot No"))
                        q1 = q1 + "Lot in (" + p1 + ") ";
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Stone Name"))
                        q1 = q1 + "Stone in (" + p1 + ") ";
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Size"))
                        q1 = q1 + "Size in (" + p1 + ") ";
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Shape"))
                        q1 = q1 + "Shape in (" + p1 + ") ";
                    else if (metroComboBox1.SelectedItem.ToString().Equals("Seller Name"))
                        q1 = q1 + "Seller in (" + p1 + ") ";
                }

                if (Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 && metroTextBox4.Text.Length > 0 && Convert.ToInt32(metroComboBox5.SelectedIndex) != -1)
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
                        q1 = q1 + metroComboBox2.SelectedItem.ToString()+" ";
                    if (metroComboBox5.SelectedItem.ToString().Equals("Lot No"))
                        q1 = q1 + "Lot in (" + p1 + ") ";
                    else if (metroComboBox5.SelectedItem.ToString().Equals("Stone Name"))
                        q1 = q1 + "Stone in (" + p1 + ") ";
                    else if (metroComboBox5.SelectedItem.ToString().Equals("Size"))
                        q1 = q1 + "Size in (" + p1 + ") ";
                    else if (metroComboBox5.SelectedItem.ToString().Equals("Shape"))
                        q1 = q1 + "Shape in (" + p1 + ") ";
                    else if (metroComboBox5.SelectedItem.ToString().Equals("Seller Name"))
                        q1 = q1 + "Seller in (" + p1 + ") ";
               
                        stt = 1;
                }
                if (Convert.ToInt32(metroComboBox3.SelectedIndex) != -1)
                {
                    String d1 = metroDateTime1.Text;
                    String d2 = metroDateTime2.Text;
                    if (stt == 1)
                        q1 = q1 + metroComboBox3.SelectedItem.ToString() + " dop between '" + d1 + "' and '" + d2 + "' ";
                    else
                    {
                        q1 = q1 + " dop between '" + d1 + "' and '" + d2 + "' ";
                        stt = 1;
                    }
                }

                if (Convert.ToInt32(metroComboBox4.SelectedIndex) != -1 && metroTextBox5.Text.Length > 0 && metroTextBox6.Text.Length > 0)
                {
                    if (stt == 1)
                        q1 = q1 + metroComboBox4.SelectedItem.ToString() + " c_qty between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                    else
                    {
                        q1 = q1 + " c_qty between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                        stt = 1;
                    }
                }
                if (metroLabel7.Text.ToString().Equals("all"))
                {
                    q1 = q1 + " order by dop;"; 
                    sele = 1;
                }
                else if (metroLabel7.Text.ToString() == "available")
                {
                    q1 = q1 + " and c_pcs>0 and c_qty>0 order by dop;";
                    sele = 2;
                }
                else if (metroLabel7.Text == "sold")
                {
                    q1 = q1 + " and c_pcs=0 and c_qty=0 order by dop;";
                    sele = 4;
                }
                
                


                foreach (DataGridViewColumn col in metroGrid1.Columns)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                int row = 0;
                String s1 = "";
                int count = 1;

                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
                MySqlCommand cmd1 = new MySqlCommand(q1, con1);
                MySqlDataReader dataReader1;
                con1.Open();
                dataReader1 = cmd1.ExecuteReader();*/
                DataSet ds = GetDataSet_F();
                int index = 0;
                String s1 = "";
                double t_pcs = 0, t_qty = 0, t_amt = 0, p_pcs = 0, p_qty = 0, p_amt = 0;
                foreach (DataRow addressRow in ds.Tables[0].Rows)
                {
                    if (index != 0)
                        index = this.metroGrid1.Rows.Count;
                    index++;
                    this.metroGrid1.Rows.Add();
                    (metroGrid1.Rows[index - 1].Cells[0]).Value = index;
                    int lt1 = Convert.ToInt32(addressRow[0]);
                    if (lt1 != 103 && lt1 != 866)
                        (metroGrid1.Rows[index - 1].Cells[1]).Value = lt1;
                    else
                        (metroGrid1.Rows[index - 1].Cells[1]).Value = addressRow[0];
                    s1 = addressRow[1].ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid1.Rows[index - 1].Cells[2]).Value = d.ToShortDateString();
                    (metroGrid1.Rows[index - 1].Cells[3]).Value = addressRow[2];
                    (metroGrid1.Rows[index - 1].Cells[4]).Value = addressRow[3];
                    (metroGrid1.Rows[index - 1].Cells[5]).Value = addressRow[4];
                    (metroGrid1.Rows[index - 1].Cells[6]).Value = addressRow[5];
                    t_pcs = t_pcs + Convert.ToDouble(addressRow[6]);
                    t_qty = t_qty + Convert.ToDouble(addressRow[7]);
                    p_pcs = p_pcs + Convert.ToDouble(addressRow[9]);
                    p_qty = p_qty + Convert.ToDouble(addressRow[10]);
                    t_amt += Convert.ToDouble(addressRow[15]);
                    p_amt += Convert.ToDouble(addressRow[16]);
                    (metroGrid1.Rows[index - 1].Cells[7]).Value = Convert.ToDouble(addressRow[6]);
                    (metroGrid1.Rows[index - 1].Cells[8]).Value = Convert.ToDouble(addressRow[7]);
                    (metroGrid1.Rows[index - 1].Cells[9]).Value = addressRow[8];
                    (metroGrid1.Rows[index - 1].Cells[10]).Value = Convert.ToDouble(addressRow[9]);
                    (metroGrid1.Rows[index - 1].Cells[11]).Value = Convert.ToDouble(addressRow[10]);
                    (metroGrid1.Rows[index - 1].Cells[12]).Value = addressRow[11];
                    (metroGrid1.Rows[index - 1].Cells[13]).Value = addressRow[12];
                    (metroGrid1.Rows[index - 1].Cells[14]).Value = addressRow[13];
                    (metroGrid1.Rows[index - 1].Cells[15]).Value = addressRow[14];
                    (metroGrid1.Rows[index - 1].Cells[16]).Value = Convert.ToDouble(addressRow[15]);
                    (metroGrid1.Rows[index - 1].Cells[17]).Value = Convert.ToDouble(addressRow[16]);
                    (metroGrid1.Rows[index - 1].Cells[18]).Value = addressRow[17];
                    (metroGrid1.Rows[index - 1].Cells[19]).Value = addressRow[18];
                }
                //            metroLabel2.Text = "" + sele;
                metroTextBox10.Text = "" + t_pcs;
                metroTextBox8.Text = "" + t_qty;
                metroTextBox9.Text = "" + t_amt;

                metroTextBox2.Text = "" + p_pcs;
                metroTextBox3.Text = "" + p_amt;
                metroTextBox7.Text = "" + p_qty;
        
            }
        }

        private void metroGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                int r = e.RowIndex;
                metroLabel6.Text = metroGrid1.Rows[r].Cells[1].Value.ToString();
                metroButton5.Enabled = true;
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Add_Stock e1 = new Add_Stock();
            e1.Text = "EDIT STOCK";
            e1.metroButton1.Visible = false;
            e1.metroButton2.Visible = false;
            e1.metroButton4.Visible = true;
            e1.metroButton5.Visible = true;
            e1.metroLabel23.Visible = true;
            e1.metroLabel24.Visible = true;
            e1.metroLabel25.Visible = true;
            e1.numericUpDown8.Visible = true;
            e1.numericUpDown9.Visible = true;
            e1.numericUpDown10.Visible = true;

            e1.metroLabel22.Text = metroLabel6.Text;
            e1.Visible = true;
            e1.metroButton5.PerformClick();
            e1.numericUpDown1.ReadOnly = true;
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex>=0)
            {
                int r = e.RowIndex;
                int lot = Convert.ToInt32(metroGrid1.Rows[r].Cells[1].Value.ToString());
                try
                {
                    item_history c = new item_history();
                    c.Text = "HISTORY OF " + lot;
                    c.metroLabel1.Text = "" + lot;
                    c.Visible = true;
                    c.metroButton6.Visible = true;
                    c.metroButton6.PerformClick();
                    c.metroButton6.Visible = false;
                }
                catch (Exception e1)
                {
                }
            }
        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private DataSet GetDataSet()
        {
            String q1 = "";
            if (metroLabel7.Text.ToString().Equals("all"))
                q1 = "Select * from stone;";
            else if (metroLabel7.Text.ToString() == "available")
                q1 = "Select * from stone where c_pcs>0 and c_qty>0;";//order by date_in;";
            else if (metroLabel7.Text == "sold")
                q1 = "Select * from stone where c_pcs=0 and c_qty=0;";//order by date_in;";
            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            MySqlConnection myConn = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            myConn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(q1, myConn);
            cmd.CommandType = CommandType.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            myConn.Close();
            return ds;
        }


        private void metroButton4_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            metroGrid1.Rows.Clear();
            DataSet ds = GetDataSet();
            int index = 0;
            String s1 = "";
            double t_pcs = 0, t_qty = 0, t_amt = 0, p_pcs = 0, p_qty = 0, p_amt = 0;
            foreach (DataRow addressRow in ds.Tables[0].Rows)
            {
                    if (index != 0)
                        index = this.metroGrid1.Rows.Count;
                    index++;
                    this.metroGrid1.Rows.Add();
                    (metroGrid1.Rows[index-1].Cells[0]).Value = index;
                int lt1 = Convert.ToInt32(addressRow[0]);
                if (lt1 != 103 && lt1 != 866)
                        (metroGrid1.Rows[index-1].Cells[1]).Value = lt1;
                    else
                        (metroGrid1.Rows[index-1].Cells[1]).Value = addressRow[0];
                    s1 = addressRow[1].ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid1.Rows[index-1].Cells[2]).Value = d.ToShortDateString();
                    (metroGrid1.Rows[index-1].Cells[3]).Value = addressRow[2];
                    (metroGrid1.Rows[index-1].Cells[4]).Value = addressRow[3];
                    (metroGrid1.Rows[index-1].Cells[5]).Value = addressRow[4];
                    (metroGrid1.Rows[index-1].Cells[6]).Value = addressRow[5];
                    t_pcs = t_pcs + Convert.ToDouble(addressRow[6]);
                    t_qty = t_qty + Convert.ToDouble(addressRow[7]);
                    p_pcs = p_pcs + Convert.ToDouble(addressRow[9]);
                    p_qty = p_qty + Convert.ToDouble(addressRow[10]);
                    t_amt += Convert.ToDouble(addressRow[15]);
                    p_amt += Convert.ToDouble(addressRow[16]);
                    (metroGrid1.Rows[index-1].Cells[7]).Value = Convert.ToDouble(addressRow[6]);
                    (metroGrid1.Rows[index-1].Cells[8]).Value = Convert.ToDouble(addressRow[7]);
                    (metroGrid1.Rows[index-1].Cells[9]).Value = addressRow[8];
                    (metroGrid1.Rows[index-1].Cells[10]).Value = Convert.ToDouble(addressRow[9]);
                    (metroGrid1.Rows[index-1].Cells[11]).Value = Convert.ToDouble(addressRow[10]);
                    (metroGrid1.Rows[index-1].Cells[12]).Value = addressRow[11];
                    (metroGrid1.Rows[index-1].Cells[13]).Value = addressRow[12];
                    (metroGrid1.Rows[index-1].Cells[14]).Value = addressRow[13];
                    (metroGrid1.Rows[index-1].Cells[15]).Value = addressRow[14];
                    (metroGrid1.Rows[index-1].Cells[16]).Value = Convert.ToDouble(addressRow[15]);
                    (metroGrid1.Rows[index-1].Cells[17]).Value = Convert.ToDouble(addressRow[16]);
                    (metroGrid1.Rows[index-1].Cells[18]).Value = addressRow[17];
                    (metroGrid1.Rows[index-1].Cells[19]).Value = addressRow[18];
            }
            metroTextBox10.Text = "" + t_pcs;
            metroTextBox8.Text = "" + t_qty;
            metroTextBox9.Text = "" + t_amt;

            metroTextBox2.Text = "" + p_pcs;
            metroTextBox3.Text = "" + p_amt;
            metroTextBox7.Text = "" + p_qty;

            metroDateTime1.ResetText();
            metroDateTime2.ResetText();

            metroTextBox1.Text = "";
            metroTextBox4.Text = "";
            metroTextBox5.Text = "";
            metroTextBox6.Text = "";
            metroComboBox1.SelectedIndex = -1;
            metroComboBox2.SelectedIndex = -1;
            metroComboBox3.SelectedIndex = -1;
            metroComboBox4.SelectedIndex = -1;
            metroComboBox5.SelectedIndex = -1;

//            stopwatch.Stop();
//            MessageBox.Show("" + stopwatch.Elapsed);
//            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
  //          Console.ReadLine();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (metroLabel6.Text.Length > 0)
            {
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                String query = "Select count(*) from stone where lot='" + metroLabel6.Text + "' and amt=cr_amt;";
                MySqlCommand cmd1;
                cmd1 = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader dataReader1;
                dataReader1 = cmd1.ExecuteReader();
                int row = 0;
                if (dataReader1.Read())
                {
                    MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                    String query1 = "delete from stone where lot='" + metroLabel6.Text + "';";
                    MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                    con2.Open();
                    row = cmd2.ExecuteNonQuery();
                }
                else
                {
                    MetroMessageBox.Show(this, "\n\nCANNOT DELETE THIS STONE AS IT IS ALREADY USED IN SOME PACKING LIST", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dataReader1.Close();
                con.Close();
                metroButton1.PerformClick();
            }
        }
    }
}
