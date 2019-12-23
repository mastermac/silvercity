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
using Excel = Microsoft.Office.Interop.Excel;

namespace Surya
{
    public partial class Item_list : MetroForm
    {
        public Item_list()
        {
            InitializeComponent();
        }

        private void Item_list_Load(object sender, EventArgs e)
        {
            metroButton7.PerformClick();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            metroGrid2.Rows.Clear();
            metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            String query = "Select item.pic,item.descrip,code, size,cate from item;";
            MySqlCommand cmd1;
            cmd1 = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader dataReader1;
            dataReader1 = cmd1.ExecuteReader();
            int row = 0;
            while (dataReader1.Read())
            {
               
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
                (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(2).ToString();
                ((DataGridViewImageCell)metroGrid2.Rows[row].Cells[2]).Value = newImage;
                (metroGrid2.Rows[row].Cells[4]).Value = dataReader1.GetValue(1);
                (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(3).ToString();
                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                String q1 = "Select avg(ta_r), min(ta_r), max(ta_r) from ledger where code='" + dataReader1.GetValue(2).ToString() + "' and ta_r!=0;";
                MySqlCommand cmd2;
                cmd2 = new MySqlCommand(q1, con1);
                con1.Open();
                MySqlDataReader dataReader2;
                dataReader2 = cmd2.ExecuteReader();
                while (dataReader2.Read())
                {
                    
                    (metroGrid2.Rows[row].Cells[5]).Value = dataReader2.GetValue(0);
                    (metroGrid2.Rows[row].Cells[6]).Value = dataReader2.GetValue(1);
                    (metroGrid2.Rows[row].Cells[7]).Value = dataReader2.GetValue(2);
                }
                dataReader2.Close();
                con1.Close();
                (metroGrid2.Rows[row].Cells[8]).Value = dataReader1.GetValue(4).ToString();
                
                row++;
            }
            
            metroTextBox1.Text = "";
            metroTextBox4.Text = "";
            metroTextBox5.Text = "";
            metroTextBox6.Text = "";
            metroComboBox2.SelectedIndex = -1;
            metroComboBox3.SelectedIndex = -1;
            

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Home"].BringToFront();
            this.Dispose();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            String query = "Select item.pic,item.descrip,item.code, size, avg(ta_r) as avg,min(ta_r) as min, max(ta_r) as max,cate from item,ledger where ";

            if (Convert.ToInt32(metroComboBox2.SelectedIndex) != -1 || Convert.ToInt32(metroComboBox3.SelectedIndex) != -1 ||metroTextBox1.Text.Length>0)
            {
                metroGrid2.Rows.Clear();
                metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                int stt = 0;
                
                if (metroTextBox1.Text.Length > 0)
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
                    query = query + "item.code in (" + p1 + ") ";
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
                        query = query + metroComboBox2.SelectedItem.ToString() + " ";
                    query = query + "size in (" + p1 + ") ";

                    stt = 1;
                }
                if(stt==1)
                query = query + " and item.code=ledger.code and ta_r!=0 group by ledger.code ";
                else
                    query = query + " item.code=ledger.code and ta_r!=0 group by ledger.code ";

                if (Convert.ToInt32(metroComboBox3.SelectedIndex) != -1 && metroTextBox5.Text.Length > 0 && metroTextBox6.Text.Length > 0)
                {
                        query = query + "having avg(ta_r) between " + metroTextBox5.Text.ToString() + " and " + metroTextBox6.Text.ToString() + " ";
                        stt = 1;
                }
                query = query + ";";
                //MessageBox.Show(query);
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                metroGrid2.Rows.Clear();
                metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                MySqlCommand cmd1;
                cmd1 = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader dataReader1;
                dataReader1 = cmd1.ExecuteReader();
                int row = 0;
                while (dataReader1.Read())
                {
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
                    (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(2).ToString();
                    ((DataGridViewImageCell)metroGrid2.Rows[row].Cells[2]).Value = newImage;
                    (metroGrid2.Rows[row].Cells[4]).Value = dataReader1.GetValue(1);
                    (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(3).ToString();
                        (metroGrid2.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                        (metroGrid2.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                        (metroGrid2.Rows[row].Cells[7]).Value = dataReader1.GetValue(6);
                        (metroGrid2.Rows[row].Cells[8]).Value = dataReader1.GetValue(7);
                        row++;
                }

            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            //String nm = this.Text;

            String path = @"C:\Silver City\Files\Item List.xls";
            //            String sel = metroLabel2.Text;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[2, 4] = "LIST OF ALL ITEMS";

            Excel.Range curcell = (Excel.Range)xlWorkSheet.Cells[2, 1];

            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Size = 20;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlWorkSheet.Columns[1].ColumnWidth = 6;
            xlWorkSheet.Columns[2].ColumnWidth = 10;
            xlWorkSheet.Columns[3].ColumnWidth = 20;
            xlWorkSheet.Columns[4].ColumnWidth = 10;
            xlWorkSheet.Columns[5].ColumnWidth = 40;
            xlWorkSheet.Columns[6].ColumnWidth = 11;
            xlWorkSheet.Columns[7].ColumnWidth = 10;
            xlWorkSheet.Columns[8].ColumnWidth = 10;
            xlWorkSheet.Columns[9].ColumnWidth = 10;

            int i = 0;
            int j = 0;
            xlWorkSheet.Cells[4, 1] = "S.No";
            xlWorkSheet.Cells[4, 2] = "Code";
            curcell = (Excel.Range)xlWorkSheet.Cells[4, 1];
            curcell.EntireRow.Font.Bold = true;
            curcell.EntireRow.Font.Color = Color.Red;
            curcell.EntireRow.Font.Underline = true;
            curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            xlWorkSheet.Cells[4, 3] = "Item Picture";
            xlWorkSheet.Cells[4, 4] = "Size";
            xlWorkSheet.Cells[4, 5] = "Description";
            xlWorkSheet.Cells[4, 6] = "Avg Price";
            xlWorkSheet.Cells[4, 7] = "Min Price";
            xlWorkSheet.Cells[4, 8] = "Max Price";
            xlWorkSheet.Cells[4, 9] = "Category";

            for (i = 0; i <= metroGrid2.RowCount - 1; i++)
            {
                Excel.Range curcell2 = (Excel.Range)xlWorkSheet.Cells[i + 5, 1];
                curcell2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell2.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                for (j = 0; j <= metroGrid2.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = metroGrid2[j, i];
                    if (j==2)
                    {
                        PictureBox p = new PictureBox();
                        p.Image = (Image)metroGrid2.Rows[i].Cells[2].Value; 
                        p.Image.Save(@"C:\Silver City\Files\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Excel.Range oRange = (Excel.Range)xlWorkSheet.Cells[i + 5, j + 1];
                        float left = (float)((double)oRange.Left);
                        float Top = (float)((double)oRange.Top);
                        const float ImageSize = 95;
                        const float Imagehei = 95;
                        xlWorkSheet.Shapes.AddPicture(@"C:\Silver City\Files\temp.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, left, Top, ImageSize, Imagehei);
                        File.Delete(@"C:\Silver City\Files\temp.jpg");
                        oRange.RowHeight = 100;
                    }
                    else
                        xlWorkSheet.Cells[i + 5, j + 1] = cell.Value;
                }
            }

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
    }
}
