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


 


using Excel = Microsoft.Office.Interop.Excel; 

namespace Surya
{
    public partial class Buyer_List : MetroForm
    {
        public Buyer_List()
        {
            InitializeComponent();
        }

        private void Buyer_List_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
            metroGrid1.Rows.Clear();
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            double sum = 0;
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");

            String q1 = "";
            if (metroLabel1.Text.Equals("B"))
            {
                q1 = "Select * from Buyer;";
            }
            else if (metroLabel1.Text.Equals("S"))
            {
                q1 = "Select * from Seller;";
            }
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            int row = 0;
            String s2 = "", s4 = "";
            dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                s4 = "" + dataReader1.GetValue(0);
                s2 = "" + dataReader1.GetValue(1);
                double tot = Convert.ToDouble(dataReader1.GetValue(2));

                sum = tot - Convert.ToDouble(dataReader1.GetValue(3));
                int index = this.metroGrid1.Rows.Count;

                index++;
                this.metroGrid1.Rows.Add();

                (metroGrid1.Rows[row].Cells[0]).Value = row + 1;
                (metroGrid1.Rows[row].Cells[1]).Value = s4;
                (metroGrid1.Rows[row].Cells[2]).Value = s2;

                (metroGrid1.Rows[row].Cells[3]).Value = tot.ToString();
                (metroGrid1.Rows[row].Cells[4]).Value = sum.ToString();

                row++;
            }
            metroButton2.Enabled = true;
            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            int temp = 0;
            String path = "";
            if (this.Text.ToString().Equals("BUYER LIST"))
                path = @"C:\Bhandari Soft\Files\BUYER LIST.xls";
            else
            {
                path = @"C:\Bhandari Soft\Files\SELLER LIST.xls";
            }
            if (temp == 0)
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Columns[1].ColumnWidth = 5;
                xlWorkSheet.Columns[2].ColumnWidth = 5;
                xlWorkSheet.Columns[3].ColumnWidth = 20;
                xlWorkSheet.Columns[4].ColumnWidth = 12;
                xlWorkSheet.Columns[5].ColumnWidth = 18;


                int i = 0;

                int j = 0;

                /*            xlWorkSheet.Cells[2, 5] = "TOTAL AMOUNT = Rs. " + tot;
                            Excel.Range curcell1 = xlApp.ActiveCell;
                            curcell1.EntireRow.Font.Bold = true;
                            xlWorkSheet.Cells[2, 9] = stat;

                            */
                xlWorkSheet.Cells[1, 1] = "S.No";
                xlWorkSheet.Cells[1, 2] = "ID";
                Excel.Range curcell = xlApp.ActiveCell;
                curcell.EntireRow.Font.Bold = true;
                curcell.EntireRow.Font.Color = Color.Red;
                curcell.EntireRow.Font.Underline = true;
                curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                xlWorkSheet.Cells[1, 3] = "Name";
                xlWorkSheet.Cells[1, 4] = "Total Amount";
                xlWorkSheet.Cells[1, 5] = "Deposited Amount";
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

                xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                Application.OpenForms["Home"].BringToFront();
                System.Diagnostics.Process.Start(path);
            }
            else if (temp == 1)
            {

                MessageBox.Show(this, "No History To Show For this id!!\nPlease Check All the Details and Try Again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    }
}
