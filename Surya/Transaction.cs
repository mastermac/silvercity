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

 


using Excel = Microsoft.Office.Interop.Excel; 


namespace Surya
{
    public partial class Transaction : MetroForm
    {
        public Transaction()
        {
            InitializeComponent();
        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            metroButton1.PerformClick();
        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            if (metroComboBox1.SelectedItem.ToString().Equals("Buyer Name"))
                metroTextBox1.PromptText = "Please Enter Buyer Name here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Date of Inventory"))
                metroTextBox1.PromptText = "Enter date in yyyy-mm format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Paid Date"))
                metroTextBox1.PromptText = "Enter date in yyyy-mm format";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Product Type"))
                metroTextBox1.PromptText = "Please Specify Product Type here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Seller Name"))
                metroTextBox1.PromptText = "Please Enter Seller Name here";
            else if (metroComboBox1.SelectedItem.ToString().Equals("Unique ID"))
                metroTextBox1.PromptText = "Please Enter Unique ID here";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            String q1 = "Select history.pid,invent.cps,invent.date_pur,sell.dos,Buyer.Name,history.dater,history.amt from history,sell,Buyer,invent where invent.id=sell.specs and history.pid=sell.specs and sell.Bid=Buyer.Buyid and ";
            
            if (metroComboBox1.SelectedItem.ToString().Equals("Buyer Name"))
            {
                q1 = q1 + "Buyer.Name='" + metroTextBox1.Text + "' order by date_pur;";
            }
            else if (metroComboBox1.SelectedItem.ToString().Equals("Date of Inventory"))
            {
                q1 = q1 + "invent.date_pur like '%" + metroTextBox1.Text + "%' order by date_pur;";
            }
            else if (metroComboBox1.SelectedItem.ToString().Equals("Paid Date"))
            {
                q1 = q1 + "history.dater like '%" + metroTextBox1.Text + "%' order by date_pur;";
            }
            else if (metroComboBox1.SelectedItem.ToString().Equals("Date of Sell"))
            {
                q1 = q1 + "sell.dos like '%" + metroTextBox1.Text + "%' order by date_pur;";
            }
            else if (metroComboBox1.SelectedItem.ToString().Equals("Unique ID"))
            {
                q1 = q1 + "invent.id='" + metroTextBox1.Text + "';";
            }

            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            int row = 0;
            int count = 1;

            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                metroGrid1.Rows.Add();

                (metroGrid1.Rows[row].Cells[0]).Value = count;
                (metroGrid1.Rows[row].Cells[1]).Value = dataReader1.GetValue(0);
                (metroGrid1.Rows[row].Cells[2]).Value = dataReader1.GetValue(1);
                (metroGrid1.Rows[row].Cells[3]).Value = dataReader1.GetValue(2).ToString().Substring(0, 10);
                (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(3).ToString().Substring(0, 10);
                (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5).ToString().Substring(0, 10);
                (metroGrid1.Rows[row].Cells[7]).Value = dataReader1.GetValue(6);
            
                count++;
                row++;
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            int row = 0;
            int count = 1;

            String q1 = "Select history.pid,invent.cps,invent.date_pur,sell.dos,Buyer.Name,history.dater,history.amt from history,sell,Buyer,invent where invent.id=sell.specs and history.pid=sell.specs and sell.Bid=Buyer.Buyid order by date_pur;";
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                metroGrid1.Rows.Add();
                (metroGrid1.Rows[row].Cells[0]).Value = count;
                (metroGrid1.Rows[row].Cells[1]).Value = dataReader1.GetValue(0);
                (metroGrid1.Rows[row].Cells[2]).Value = dataReader1.GetValue(1);
                (metroGrid1.Rows[row].Cells[3]).Value = dataReader1.GetValue(2).ToString().Substring(0,10);
                (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(3).ToString().Substring(0, 10);
                (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5).ToString().Substring(0, 10);
                (metroGrid1.Rows[row].Cells[7]).Value = dataReader1.GetValue(6);
                count++;
                row++;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            String path = @"C:\Bhandari Soft\Files\Payment Breakup Details.xls";
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
                xlWorkSheet.Columns[4].ColumnWidth = 17;
                xlWorkSheet.Columns[5].ColumnWidth = 12;
                xlWorkSheet.Columns[6].ColumnWidth = 20;
                xlWorkSheet.Columns[7].ColumnWidth = 12;
                xlWorkSheet.Columns[8].ColumnWidth = 14;
                

                int i = 0;

                int j = 0;

                    xlWorkSheet.Cells[1, 1] = "S.No";
                    xlWorkSheet.Cells[1, 2] = "Unique ID";
                    Excel.Range curcell = (Excel.Range)xlWorkSheet.Cells[1, 2];
                    curcell.EntireRow.Font.Bold = true;
                    curcell.EntireRow.Font.Color = Color.Red;
                    curcell.EntireRow.Font.Underline = true;
                    curcell.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    curcell.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    xlWorkSheet.Cells[1, 3] = "CPS";
                    xlWorkSheet.Cells[1, 4] = "Date of Inventory";
                    xlWorkSheet.Cells[1, 5] = "Date of Sell";
                    xlWorkSheet.Cells[1, 6] = "Buyer";
                    xlWorkSheet.Cells[1, 7] = "Paid Date";
                    xlWorkSheet.Cells[1, 8] = "Amount Paid";
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
