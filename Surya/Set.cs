using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using MetroFramework.Forms;
using MetroFramework;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Surya
{
    public partial class Set : MetroForm
    {

        public Set()
        {
            InitializeComponent();
        }

        private void Set_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

/*            saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Brokery;UID=root;PASSWORD=smhs;");

                ServerConnection con = new ServerConnection(con1);
                Server server = new Server(con);
                Backup source = new Backup();
                source.Action = BackupActionType.Database;
                source.Database = "Experiment";
                BackupDeviceItem destination = new BackupDeviceItem(saveFileDialog1.FileName, DeviceType.File);
                source.Devices.Add(destination);
                source.MySqlBackup(server);
                con.Disconnect();

                MetroMessageBox.Show(this, "\nDatabase BackUp has been created successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Set"].BringToFront();


            }
*/        }


        private void button2_Click(object sender, EventArgs e)
        {
            /*openFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Brokery;UID=root;PASSWORD=smhs;");

                ServerConnection con = new ServerConnection(con1);
                Server server = new Server(con);
                Restore destination = new Restore();
                destination.Action = RestoreActionType.Database;
                destination.Database = "Experiment";
                BackupDeviceItem source = new BackupDeviceItem(openFileDialog1.FileName, DeviceType.File);
                destination.Devices.Add(source);
                destination.ReplaceDatabase = true;
                destination.MySqlRestore(server);

                MetroMessageBox.Show(this, "\nDatabase has been Recovered successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Set"].BringToFront();

            }
            */
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            String user = metroTextBox1.Text.ToString().ToLower();
            String pass = metroTextBox2.Text.ToString();
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=login;UID=root;PASSWORD=smhs;");
            String q1 = "Select * from log where user='" + user + "' and pass='" + pass + "';";
            MySqlCommand cmd1 = new MySqlCommand(q1, con);
            MySqlDataReader dataReader1;
            con.Open();
            dataReader1 = cmd1.ExecuteReader();
            if (dataReader1.Read())
            {
                if (metroTextBox3.Text.ToString().Length > 0 && (metroTextBox4.Text.ToString().Length > 0) && (metroTextBox3.Text.ToString().Equals(metroTextBox4.Text.ToString())))
                {
                    MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=login;UID=root;PASSWORD=smhs;");
                    con1.Open();
                    MySqlCommand cmd = new MySqlCommand("update log set pass='" + metroTextBox3.Text.ToString() + "' where user='" + metroTextBox1.Text.ToString() + "';", con1);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MetroMessageBox.Show(this, "\n\nPassword successfully Changed!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                        Application.OpenForms["Home"].BringToFront();
                    }
            }
                else
                {
                    MetroMessageBox.Show(this, "\n\nPlease Check your New Password and Try Again!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.OpenForms["Home"].BringToFront();
                    Application.OpenForms["Set"].BringToFront();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "\n\nPlease Check your Existing UserName or Password and Try Again!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Set"].BringToFront();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            metroTextBox2.Text = "";
            metroTextBox1.Select();
            metroTextBox3.Text = "";
            metroTextBox4.Text = "";

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms["Home"].BringToFront();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            
                        saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string constring = "SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;";
                            string file = saveFileDialog1.FileName;
                            using (MySqlConnection conn = new MySqlConnection(constring))
                            {
                                using (MySqlCommand cmd = new MySqlCommand())
                                {
                                    using (MySqlBackup mb = new MySqlBackup(cmd))
                                    {
                                        cmd.Connection = conn;
                                        conn.Open();
                                        mb.ExportToFile(file);
                                        conn.Close();
                                    }
                                }
                            }
                            MetroMessageBox.Show(this, "\nDatabase BackUp has been created successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.OpenForms["Home"].BringToFront();
                            Application.OpenForms["Set"].BringToFront();
                        }
            
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string constring = "SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;";
                string file = openFileDialog1.FileName;
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(file);
                            conn.Close();
                        }
                    }
                }
                MetroMessageBox.Show(this, "\nDatabase has been Recovered successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Set"].BringToFront();
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            Close();
            Application.OpenForms["Home"].BringToFront();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = @"E:\Stock\Bill\";
                String p = saveFileDialog1.FileName.ToString();
                int l = p.LastIndexOf('\\');

                string targetPath = p.Substring(0, l);
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                CopyFolder(sourcePath, targetPath);

                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(sourcePath);

                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
                // Copy the files and overwrite destination files if they already exist.
                /*                foreach (string s in files)
                                {
                                    // Use static Path methods to extract only the file name from the path.
                                    fileName = System.IO.Path.GetFileName(s);
                                    System.IO.File.Move(sourceFile, destinationFile);
                                    destFile = System.IO.Path.Combine(targetPath, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }
                                */
            }

        }


        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = @"E:\Stock\Pictures";
                String p = saveFileDialog1.FileName.ToString();
                int l = p.LastIndexOf('\\');

                string targetPath = p.Substring(0, l);
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(targetPath);

                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    dir.Delete(true);
                }

                string[] files = System.IO.Directory.GetFiles(sourcePath);
                CopyFolder(sourcePath, targetPath);
            }
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {/*
            openFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Brokery;UID=root;PASSWORD=smhs;");

                ServerConnection con = new ServerConnection(con1);
                Server server = new Server(con);
                Restore destination = new Restore();
                destination.Action = RestoreActionType.Database;
                destination.Database = "SuryaGems";
                BackupDeviceItem source = new BackupDeviceItem(openFileDialog1.FileName, DeviceType.File);
                destination.Devices.Add(source);
                destination.ReplaceDatabase = true;
                destination.MySqlRestore(server);

                String s3 = "Update Buyer set Total=0 , Deposited=0;";
                String s4 = "Delete From Bill;";
                String s5 = "Delete From Purchase;";

                MySqlConnection con2 = new MySqlConnection("Data Source=AMO;Initial Catalog=SuryaGems;Integrated Security=True");
                con2.Open();
                int result = 0;
                MySqlCommand cmd1 = new MySqlCommand(s5, con2);
                result = cmd1.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand(s4, con2);
                result = cmd2.ExecuteNonQuery();

                MySqlCommand cmd3 = new MySqlCommand(s3, con2);
                result = cmd3.ExecuteNonQuery();

                con2.Close();

                MetroMessageBox.Show(this, "\nDatabase has been Recovered successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Set"].BringToFront();

            }*/
        }

        private void metroButton4_Click_1(object sender, EventArgs e)
        {

                        saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string constring = "SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;";
                            string file = saveFileDialog1.FileName;
                            using (MySqlConnection conn = new MySqlConnection(constring))
                            {
                                using (MySqlCommand cmd = new MySqlCommand())
                                {
                                    using (MySqlBackup mb = new MySqlBackup(cmd))
                                    {
                                        cmd.Connection = conn;
                                        conn.Open();
                                        mb.ExportToFile(file);
                                        conn.Close();
                                    }
                                }
                            }
                            String s1 = "Delete From invent;";
                String s2 = "Delete From history;";
                String s3 = "Delete From Buyer;";
                String s4 = "Delete From Seller;";
                String s5 = "Delete from sell;";
                
                MySqlConnection con2 = new MySqlConnection("SERVER=localhost;DATABASE=Brokery;UID=root;PASSWORD=smhs;");
                con2.Open();
                int result = 1;
                MySqlCommand cmd2 = new MySqlCommand(s4, con2);
                result = cmd2.ExecuteNonQuery();

                MySqlCommand cmd3 = new MySqlCommand(s3, con2);
                result = cmd3.ExecuteNonQuery();

                MySqlCommand cmd4 = new MySqlCommand(s2, con2);
                result = cmd4.ExecuteNonQuery();
                MySqlCommand cmd6 = new MySqlCommand(s5, con2);
                result = cmd6.ExecuteNonQuery();

                MySqlCommand cmd5 = new MySqlCommand(s1, con2);
                result = cmd5.ExecuteNonQuery();

                con2.Close();
                MetroMessageBox.Show(this, "\nDatabase BackUp has been created successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["Set"].BringToFront();

            }
        }

        private void metroButton7_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string constring = "SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;";
                string file = openFileDialog1.FileName;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;
                string str="";
                int rCnt = 0;
                int cCnt = 0;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(file, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                int end=0;
                String lot="";
                try
                {
                    for (rCnt = 3; rCnt <= range.Rows.Count; rCnt++)
                    {
                        for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                        {
                            str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                           
                            if (str.StartsWith("END"))
                            {
                                end = 1;
                                break;
                            }

                        }
                        if (end == 1)
                            break;
                        String[] s11 = new String[19];
                        str = str.Trim();
                        str = str.ToLower();
                        s11 = str.Split(',');
                        int i = 0;
                        for (i = 0; i < 19; i++)
                            s11[i] = s11[i].Trim();
            //            MessageBox.Show(s11[0]);
                        //lot = s11[0];
                        //double l = Convert.ToDouble(s11[0]);

                        if (s11[1].Length < 10)
                            s11[1] = "2011-01-27";

                        string[] dateTempArray = new string[3];
                        if(s11[1].Contains('-'))
                            dateTempArray = s11[1].Split('-');
                        else if (s11[1].Contains('/'))
                            dateTempArray = s11[1].Split('/');
                        if (dateTempArray[0].Length<=2)
                            s11[1] = dateTempArray[2] + "-" + dateTempArray[1] + "-" + dateTempArray[0];
                        else if(dateTempArray[0].Length==4)
                            s11[1] = dateTempArray[0] + "-" + dateTempArray[1] + "-" + dateTempArray[2];

                        if (s11[2].Length == 0)
                            s11[2] = "stone name";
                        if (s11[3].Length == 0)
                            s11[3] = "free size";
                        if (s11[4].Length == 0)
                            s11[4] = "free";
                        if (s11[5].Length == 0)
                            s11[5] = "silver city";
                        if (s11[6].Length == 0)
                            s11[6] = "0";
                        if (s11[7].Length == 0)
                            s11[7] = "0";
                        if (s11[8].Length == 0)
                            s11[8] = "cts";
                        if (s11[9].Length == 0)
                            s11[9] = "0";
                        if (s11[10].Length == 0)
                            s11[10] = "0";
                        if (s11[11].Length == 0)
                            s11[11] = "cts";
                        if (s11[12].Length == 0)
                            s11[12] = "0";
                        if (s11[13].Length == 0)
                            s11[13] = "0";
                        if (s11[14].Length == 0)
                            s11[14] = "0";
                        if (s11[15].Length == 0)
                            s11[15] = "0";
                        if (s11[16].Length == 0)
                            s11[16] = "0";

                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                        con.Open();
                        String Query = "insert into stone(lot, dop, stone, size, shape, seller, p_pcs, p_qty, p_unit, c_pcs, c_qty, c_unit, cost, less, nr, amt, cr_amt, specs, ec) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p,@q,@r,@s);";
                        MySqlCommand cmd = new MySqlCommand(Query, con);
                        //MessageBox.Show(Query);
                        lot=s11[0];
                        cmd.Parameters.AddWithValue("@a", s11[0]);
                        cmd.Parameters.AddWithValue("@b", s11[1]);
                        cmd.Parameters.AddWithValue("@c", s11[2]);
                        cmd.Parameters.AddWithValue("@d", s11[3]);
                        cmd.Parameters.AddWithValue("@e", s11[4]);
                        cmd.Parameters.AddWithValue("@f", s11[5]);
                        cmd.Parameters.AddWithValue("@g", s11[6]);
                        if (s11[8].Equals("gms"))
                        {
                            cmd.Parameters.AddWithValue("@h", Convert.ToDouble(s11[7]) * 5);
                            cmd.Parameters.AddWithValue("@i", "cts");
                            cmd.Parameters.AddWithValue("@k", Convert.ToDouble(s11[10]) * 5);
                            cmd.Parameters.AddWithValue("@l", "cts");
                        
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@h", Convert.ToDouble(s11[7]) );
                            cmd.Parameters.AddWithValue("@i", "cts");
                            cmd.Parameters.AddWithValue("@k", Convert.ToDouble(s11[10]));
                            cmd.Parameters.AddWithValue("@l", "cts");
                        }
                        //cmd.Parameters.AddWithValue("@i", s11[8]);
                        cmd.Parameters.AddWithValue("@j", s11[9]);
//                        cmd.Parameters.AddWithValue("@k", s11[10]);
                        //cmd.Parameters.AddWithValue("@l", s11[11]);
                        cmd.Parameters.AddWithValue("@m", s11[12]);
                        cmd.Parameters.AddWithValue("@n", Convert.ToDouble(s11[13])*100);
                        cmd.Parameters.AddWithValue("@o", s11[14]);
                        cmd.Parameters.AddWithValue("@p", s11[15]);
                        cmd.Parameters.AddWithValue("@q", s11[16]);
                        cmd.Parameters.AddWithValue("@r", s11[17]);
                        cmd.Parameters.AddWithValue("@s", s11[18].Substring(3));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception e1)
                {
                    MetroMessageBox.Show(this, "\nError in " + lot+"\n"+e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                MetroMessageBox.Show(this, "\nIMPORT SUCCESSFULLY COMPLETED!", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void metroButton8_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string constring = "SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;";
                string file = openFileDialog1.FileName;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;
                string str = "";
                int rCnt = 0;
                int cCnt = 0;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(file, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                int end = 0;
                String lot = "";
                try
                {
                    for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                    {
                        for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                        {
                            str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            if (str.StartsWith("END"))
                            {
                                end = 1;
                                break;
                            }
                        }
                        if (end == 1)
                            break;
                        String[] s11 = new String[7];
                        str = str.Trim();
                        s11 = str.Split(',');
                        int i = 0;
                        int l = s11[0].Length;
                        String dt = s11[0].Substring(l - 10);
                        String nm = s11[0].Substring(0, l - 10);
                        nm = nm.Trim();
                        String[] dat = new String[3];
                        dat = dt.Split('-');
                        String dt1 = "";
                        for (i = 2; i >= 0; i--)
                            dt1 = dt1 + dat[i]+"-";
                        String dt2 = dt1.Substring(0, dt1.Length - 1);
                        if (s11[5].Length == 0)
                            s11[5] = "RAW";
                        double quant = 0;
                        string puri = "";

                        if (s11[1].Length == 0)
                        {
                            quant = Convert.ToDouble(s11[2]);
                            puri = "925";
                        }
                        else
                        {
                            quant = Convert.ToDouble(s11[1]);
                            puri = "999";
                        }
                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                        con.Open();
                        String Query = "insert into metal(dat, type, name, qty, unit, amt, purity, rate, prodtype) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i);";
                        lot = s11[0];
                        MySqlCommand cmd = new MySqlCommand(Query, con);
                        cmd.Parameters.AddWithValue("@a", dt2);
                        cmd.Parameters.AddWithValue("@b", "Silver");
                        cmd.Parameters.AddWithValue("@c", nm);
                        cmd.Parameters.AddWithValue("@d", quant);
                        cmd.Parameters.AddWithValue("@e", "Kg");
                        cmd.Parameters.AddWithValue("@f", s11[4]);
                        cmd.Parameters.AddWithValue("@g", puri);
                        cmd.Parameters.AddWithValue("@h", s11[3]);
                        cmd.Parameters.AddWithValue("@i", s11[5]);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception e1)
                {
                    MetroMessageBox.Show(this, "\nError in " + lot + "\n" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                MetroMessageBox.Show(this, "\nIMPORT SUCCESSFULLY COMPLETED!", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void metroButton9_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string constring = "SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;";
                string file = openFileDialog1.FileName;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;
                string str = "";
                int rCnt = 0;
                int cCnt = 0;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(file, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                int end = 0;
                String lot = "";
                try
                {
                    for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                    {
                        for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                        {
                            str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            if (str.StartsWith("END"))
                            {
                                end = 1;
                                break;
                            }
                        }
                        if (end == 1)
                            break;
                        String[] s11 = new String[4];
                        str = str.Trim();
                        s11 = str.Split(',');
                        if (s11[3].Length == 0)
                            s11[3] = "RAW";

                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                        con.Open();
                        String Query = "insert into metal_consume(dat, type, name, qty, unit, purity, prodtype) VALUES (@a,@b,@c,@d,@e,@f,@g);";
                        MySqlCommand cmd = new MySqlCommand(Query, con);
                        cmd.Parameters.AddWithValue("@a", s11[0]);
                        cmd.Parameters.AddWithValue("@b", "Silver");
                        cmd.Parameters.AddWithValue("@c", s11[1]);
                        cmd.Parameters.AddWithValue("@d", s11[2]);
                        cmd.Parameters.AddWithValue("@e", "Kg");
                        cmd.Parameters.AddWithValue("@f", "925");
                        cmd.Parameters.AddWithValue("@g", s11[3]);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception e1)
                {
                    MetroMessageBox.Show(this, "\nError in " + lot + "\n" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                MetroMessageBox.Show(this, "\nIMPORT SUCCESSFULLY COMPLETED!", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel csv files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string constring = "SERVER=localhost;DATABASE=Silvercity;UID=root;PASSWORD=smhs;";
                string file = openFileDialog1.FileName;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;
                string str = "";
                int rCnt = 0;
                int cCnt = 0;
                int count = 1;
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(file, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                
                range = xlWorkSheet.UsedRange;
                int end = 0;
                String lot = "";
                try
                {
                    for (rCnt = 2; rCnt <= 46; rCnt++)
                    {
                        str = "";
                                            
                        for (cCnt = 2; cCnt <= 4; cCnt++)
                        {
                                str =str+ (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2+",";
                            if (str.StartsWith("END"))
                            {
                                end = 1;
                                break;
                            }
                        }

                        //MessageBox.Show(str);
                        byte[] ImageData = new byte[] { 0x20 };

                        Microsoft.Office.Interop.Excel.Range r1 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[rCnt, cCnt];      //Select cell A1
                        object cellValue = r1.Value2;
                        
                        Microsoft.Office.Interop.Excel.Picture pic = (Microsoft.Office.Interop.Excel.Picture)xlWorkSheet.Pictures(count);
                        count++;

                        if (pic != null)
                        {
                            //This code will detect what the region span of the image was
                            int startCol = (int)pic.TopLeftCell.Column;
                            int startRow = (int)pic.TopLeftCell.Row;
                            int endCol = (int)pic.BottomRightCell.Column;
                            int endRow = (int)pic.BottomRightCell.Row;


                            pic.CopyPicture(Microsoft.Office.Interop.Excel.XlPictureAppearance.xlScreen, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlBitmap);
                            if (Clipboard.ContainsImage())
                            {
                                Image img = Clipboard.GetImage();
                                PictureBox p1 = new PictureBox();
                                p1.Image = img;
                                p1.Image.Save(@"C:\Silver City\Files\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                FileStream fs;
                                BinaryReader br;

                                string FileName = @"C:\Silver City\Files\temp.jpg";
                                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                                br = new BinaryReader(fs);
                                ImageData = br.ReadBytes((int)fs.Length);
                                //MessageBox.Show("Hello");
                                br.Close();
                                fs.Close();
                                File.Delete(@"C:\Silver City\Files\temp.jpg");
                    
                            }
                        }


                        if (end == 1)
                            break;
                        String[] s11 = new String[4];
                        str = str.Trim();
                        s11 = str.Split(',');
                        //MessageBox.Show("Hello");
                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                        con.Open();
                        string CmdString = "INSERT INTO Item(code, size, pic, descrip,cate) VALUES(@FirstName, @LastName, @Image, @Address,@cat)";
                        MySqlCommand cmd = new MySqlCommand(CmdString, con);

                        cmd.Parameters.AddWithValue("@FirstName", s11[2]);
                        cmd.Parameters.AddWithValue("@LastName", s11[0]);
                        //MessageBox.Show("Hey");
                        cmd.Parameters.AddWithValue("@Image", ImageData);
                        //MessageBox.Show("Hey");
                        cmd.Parameters.AddWithValue("@Address", s11[1]);
                        cmd.Parameters.AddWithValue("@cat", "Pendant");
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception e1)
                {
                    MetroMessageBox.Show(this, "\nError in " + lot + "\n" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                MetroMessageBox.Show(this, "\nIMPORT SUCCESSFULLY COMPLETED!", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            con.Open();
            string CmdString = "Select code from Item";
            MySqlCommand cmd = new MySqlCommand(CmdString, con);
            var dataReader1 = cmd.ExecuteReader();
            List<string> localData = new List<string>();
            if (dataReader1.Read())
            {
                localData.Add(Convert.ToString(dataReader1[0]).ToLower());
            }
            dataReader1.Close();
            var dataTable = new DataTable();

            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://www.silvercityonline.com/stock/src/scripts/getAll.php?key=mastermac&table=product&columns=itemno,ringsize,stonesize,description,itemtypecode");
                dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            }
            CmdString = "INSERT INTO Item(code, size, pic, descrip,cate) VALUES(@FirstName, @LastName, @Image, @Address,@cat)";
            cmd = new MySqlCommand(CmdString, con);

            var count = 0;
            foreach(DataRow row in dataTable.Rows)
            {
                string code = row[0].ToString();
                if (localData.Count>0 && localData.Where(x => x.Contains(code.ToLower()))!=null)
                    continue;
                cmd.Parameters.AddWithValue("@FirstName", code);
                if(row[1].ToString().Length>0)
                    cmd.Parameters.AddWithValue("@LastName", row[1].ToString());
                else
                    cmd.Parameters.AddWithValue("@LastName", row[2].ToString());

                cmd.Parameters.AddWithValue("@Image", GetImageData(code));
                cmd.Parameters.AddWithValue("@Address", row[3].ToString());
                cmd.Parameters.AddWithValue("@cat", row[4].ToString());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                count++;
            }
            con.Close();
            MetroMessageBox.Show(this, "\nSYNCED "+count+" NEW ITEMS!", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private byte[] GetImageData(string code)
        {
            string someUrl = "https://www.silvercityonline.com/stock/pics/"+code+".JPG";
            byte[] ImageData = new byte[] { 0x20 };
            return ImageData;
            FileStream fs;
            BinaryReader br;

            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            folder = folder.Substring(0, folder.Length - 10) + @"\Resources\default.jpg";
            fs = new FileStream(folder, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fs);
            ImageData = br.ReadBytes((int)fs.Length);

            using (var webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(someUrl);
                    if (imageBytes.Length > 0)
                        ImageData = imageBytes;
                }
                catch (Exception e)
                {
                }
            }
            return ImageData;
        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}