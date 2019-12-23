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
    public partial class Pack_Item : MetroForm
    {
        public Pack_Item()
        {
            InitializeComponent();
        }

        private void Pack_Item_Load(object sender, EventArgs e)
        {
            metroButton1.Show();
            metroButton1.PerformClick();
            metroButton1.Hide();
        }

        private void metroLabel11_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void metroLabel19_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = metroDateTime1.Text;
                string dau = date1.Replace('-', '/');
                DateTime dvs = Convert.ToDateTime(dau);
                double exchg = Convert.ToDouble(metroLabel16.Text.ToString());                                
                String table = metroLabel14.Text;
                String code = metroLabel13.Text;
                double lot = Convert.ToDouble(numericUpDown1.Text.ToString());
                int pcs = Convert.ToInt32(numericUpDown2.Text.ToString());
                double wt=Convert.ToDouble(numericUpDown3.Text.ToString())*5;
                double metal_wt = Convert.ToDouble(numericUpDown5.Text.ToString());
                int stone = Convert.ToInt32(numericUpDown4.Text.ToString());
                //Select Count(*) from stone where lot=1 and c_pcs>="+pcs+" and c_qty>="+wt+";";
                int exp=0;
                if (exp == 0)
                {
                    string strCheck = "Select Count(*) from stone where lot='" + lot + "' and c_pcs>=" + stone + " and c_qty>=" + wt + ";";
                    //MessageBox.Show(strCheck);
                    MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                    MySqlCommand cmd = new MySqlCommand(strCheck, con);
                    con.Open();
                    MySqlDataReader dataReader1;
                    dataReader1 = cmd.ExecuteReader();
                    if (dataReader1.Read() && Convert.ToDouble(dataReader1.GetValue(0)) > 0)
                    {
                        string sp = "", unit = "";
                        double rate = 0;

                        MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                        String query = "Select specs, c_unit, nr from stone where lot='" + lot + "';";
                        //string query = "insert into " + table + "(code, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@j);";
                        MySqlCommand acmd = new MySqlCommand(query, con1);
                        con1.Open();
                        MySqlDataReader dataReader2;
                        dataReader2 = acmd.ExecuteReader();
                        while (dataReader2.Read())
                        {
                            sp = dataReader2.GetValue(0).ToString();
                            unit = dataReader2.GetValue(1).ToString();
                            rate = Convert.ToDouble(dataReader2.GetValue(2).ToString());
                        }
                        if (rate == 0)
                            rate = exchg;
                        dataReader2.Close();
                        query = "insert into " + table + "(code,descri,unit, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l);";
                        acmd.CommandText = query;
                        acmd.Parameters.AddWithValue("@a", code);
                        acmd.Parameters.AddWithValue("@b", sp);
                        acmd.Parameters.AddWithValue("@c", unit);
                        acmd.Parameters.AddWithValue("@d", lot);
                        acmd.Parameters.AddWithValue("@e", pcs);
                        acmd.Parameters.AddWithValue("@f", wt);
                        acmd.Parameters.AddWithValue("@g", stone);
                        acmd.Parameters.AddWithValue("@h", metal_wt);
                        acmd.Parameters.AddWithValue("@i", rate);
                        double met_rt = 0;
                        if (Convert.ToDouble(numericUpDown10.Text.ToString()) > 0)
                            met_rt = Convert.ToDouble(numericUpDown10.Text.ToString());
                        else
                            met_rt = Convert.ToDouble(numericUpDown11.Text.ToString());
                        double amt = wt * rate;
                        double subtot = amt + (met_rt * metal_wt);
                        acmd.Parameters.AddWithValue("@j", subtot);
                        acmd.Parameters.AddWithValue("@k", dvs);
                        acmd.Parameters.AddWithValue("@l", exchg);


                        int result = acmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                            String query1 = "update stone set c_pcs=c_pcs-" + stone + ", c_qty=c_qty-" + wt + ", cr_amt=cr_amt-" + amt + " where lot='" + lot + "';";
                            MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                            con2.Open();
                            int r1 = cmd2.ExecuteNonQuery();
                            if (r1 > 0)
                            {
                                metroButton6.PerformClick();
                            }
                        }
                        con1.Close();
                        numericUpDown1.Focus();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\n\nPlease Enter Correct Details!", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        metroButton6.PerformClick();
                    }
                    dataReader1.Close();
                    //                cmd.CommandText = "insert into " + metroLabel14.Text + "(code, lot, pcs, wt, stones, metwt, rt, subtot, nc_i, nc_u, pp_i, pp_u, pg_i, pg_u, date_pack, exchg_rate) VALUES (@a,@b);";

                    con.Close();
                }
                else
                {
                    MetroMessageBox.Show(this, "\nYou Cannot enter a sub-product with Same Lot No.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e1)
            {
                MetroMessageBox.Show(this, "\n\nSome Error Occured!!"+e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
                if (Convert.ToDouble(numericUpDown10.Text.ToString()) > 0 && Convert.ToDouble(numericUpDown11.Text.ToString()) > 0 && (Convert.ToDouble(numericUpDown10.Text.ToString()) == 0 && Convert.ToDouble(numericUpDown11.Text.ToString()) == 0))
                    MetroMessageBox.Show(this, "\n\nSome Error Occured!!", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                else
                {
                    double sil_rt = Convert.ToDouble(numericUpDown10.Text.ToString());
                    double br_rt = Convert.ToDouble(numericUpDown11.Text.ToString());
                    double lab_rt = Convert.ToDouble(numericUpDown9.Text.ToString());
                    double set_rt = Convert.ToDouble(numericUpDown8.Text.ToString());
                    double gold_rt = Convert.ToDouble(numericUpDown7.Text.ToString());
                    double silpla_rt = Convert.ToDouble(numericUpDown6.Text.ToString());
                    double pro_rt = Convert.ToDouble(numericUpDown12.Text.ToString());
                    string tabel = metroLabel14.Text.ToString();
                    int ind = tabel.IndexOf('_');
                    string tabl = tabel.Substring(ind + 1);
                    string code = metroLabel13.Text.ToString();
                    double exchg_rate = Convert.ToDouble(metroLabel16.Text);
                    string up = "UPDATE ledger set sil_rt=" + sil_rt + ", br_rt=" + br_rt + ", lab_rt=" + lab_rt + ", set_wt=" + set_rt + ", gold_rt=" + gold_rt + ", silplate_rt=" + silpla_rt + ", pro_rt=" + pro_rt + " where table_name='" + tabl + "' and code='" + code + "';";
                    MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                    MySqlCommand cmd = new MySqlCommand(up, con);
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        if (metroPanel1.Visible)
                        {

                            String mai = "";
                            if (metroCheckBox1.Checked == false)
                                mai = "((metwt*" + lab_rt + ")+(stones*" + set_rt + ")+(((wt*0.2) + metwt)*(" + (gold_rt + silpla_rt) + "))+subtot)*(1+(0.01*" + pro_rt + "))";
                            else
                                mai = "((pcs*" + lab_rt + ")+(stones*" + set_rt + ")+(((wt*0.2) + metwt)*(" + (gold_rt + silpla_rt) + "))+subtot)*(1+(0.01*" + pro_rt + "))";

                            double met_rt = 0;
                            if (Convert.ToDouble(numericUpDown10.Text.ToString()) > 0)
                                met_rt = Convert.ToDouble(numericUpDown10.Text.ToString());
                            else
                                met_rt = Convert.ToDouble(numericUpDown11.Text.ToString());
                            cmd.CommandText = "UPDATE " + tabel + " set subtot=(wt*rt)+(metwt*" + met_rt + ") where code='"+code+"' ;";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "UPDATE " + tabel + " set nc_i=" + mai + ", nc_u=(" + mai + "/" + exchg_rate + ")*1.1 where code='" + code + "';";
                            cmd.ExecuteNonQuery();
                            try
                            {
                                cmd.CommandText = "UPDATE " + tabel + " set pp_i=nc_i/pcs, pp_u=nc_u/pcs where pcs>0 and code='" + code + "';";
                                cmd.ExecuteNonQuery();
                            }
                            finally
                            {
                                cmd.CommandText = "UPDATE " + tabel + " set pg_i=nc_i/((wt*0.2)+(metwt)), pg_u=nc_u/((wt*0.2)+(metwt)) where code='" + code + "';";
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Update Done");
                                metroButton6.PerformClick();
                            }
                        }
                        else if (metroPanel4.Visible)
                        {
                            String mai = "";
                            if (metroCheckBox1.Checked == false)
                                mai = "((metwt*" + lab_rt + ")+(stones*" + set_rt + ")+(((wt) + metwt)*(" + (gold_rt + silpla_rt) + "))+subtot)*(1+(0.01*" + pro_rt + "))";
                            else
                                mai = "((pcs*" + lab_rt + ")+(stones*" + set_rt + ")+(((wt) + metwt)*(" + (gold_rt + silpla_rt) + "))+subtot)*(1+(0.01*" + pro_rt + "))";

                            double met_rt = 0;
                            if (Convert.ToDouble(numericUpDown10.Text.ToString()) > 0)
                                met_rt = Convert.ToDouble(numericUpDown10.Text.ToString());
                            else
                                met_rt = Convert.ToDouble(numericUpDown11.Text.ToString());
                            cmd.CommandText = "UPDATE " + tabel + " set subtot=((pcs*rt)+(metwt*"+sil_rt+")) where code='" + code + "';";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "UPDATE " + tabel + " set nc_i=" + mai + ", nc_u=(" + mai + "/" + exchg_rate + ")*1.1 where code='" + code + "';";
                            cmd.ExecuteNonQuery();
                            try
                            {
                                cmd.CommandText = "UPDATE " + tabel + " set pp_i=nc_i/pcs, pp_u=nc_u/pcs where pcs>0 and code='" + code + "';";
                                cmd.ExecuteNonQuery();
                            }
                            finally
                            {
                                cmd.CommandText = "UPDATE " + tabel + " set pg_i=nc_i/((wt)+(metwt)), pg_u=nc_u/((wt)+(metwt)) ;";
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Update Done");
                                metroButton9.PerformClick();
                            }

                        }
                    }
                    else
                        MessageBox.Show("Update Not Done");

                }
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            //metroButton2.PerformClick();
            numericUpDown1.Text = "0";
            numericUpDown2.Text = "0";
            numericUpDown3.Text = "0";
            numericUpDown4.Text = "0";
            numericUpDown5.Text = "0";
            metroButton10.Visible = false;
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            metroGrid2.Rows.Clear();
            metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            String tabl = metroLabel14.Text.ToString();
            String code = metroLabel13.Text.ToString();
            int ind=tabl.IndexOf('_');
            String tabel=tabl.Substring(ind+1);
            String query = "Select * from " + tabl + ", ledger where " + tabl + ".code='" + code + "' and ledger.table_name=" + tabel + " and ledger.code='"+code+"' and ledger.code="+tabl+".code ;";
            //MessageBox.Show(query);
            MySqlCommand cmd1;
            cmd1 = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader dataReader1;
            dataReader1 = cmd1.ExecuteReader();
            int row = 0;
            while (dataReader1.Read())
            {
                int index = this.metroGrid2.Rows.Count;
                index++;
                this.metroGrid2.Rows.Add();
                (metroGrid2.Rows[row].Cells[0]).Value = row + 1;
                int lt1 = Convert.ToInt32(dataReader1.GetValue(4));
                if (lt1 != 103 && lt1 != 866)
                    (metroGrid2.Rows[row].Cells[1]).Value = lt1;
                else
                    (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(4);
                
//                (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(4).ToString();
                (metroGrid2.Rows[row].Cells[2]).Value = dataReader1.GetValue(2).ToString();
                (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(3);
                (metroGrid2.Rows[row].Cells[4]).Value = dataReader1.GetValue(5);
                (metroGrid2.Rows[row].Cells[5]).Value = dataReader1.GetValue(6);
                (metroGrid2.Rows[row].Cells[6]).Value = dataReader1.GetValue(7);
                (metroGrid2.Rows[row].Cells[7]).Value = dataReader1.GetValue(8);
                (metroGrid2.Rows[row].Cells[8]).Value = dataReader1.GetValue(9);
                (metroGrid2.Rows[row].Cells[9]).Value = dataReader1.GetValue(10);
                (metroGrid2.Rows[row].Cells[10]).Value = dataReader1.GetValue(11);
                (metroGrid2.Rows[row].Cells[11]).Value = dataReader1.GetValue(12);
                (metroGrid2.Rows[row].Cells[12]).Value = dataReader1.GetValue(13);
                (metroGrid2.Rows[row].Cells[13]).Value = dataReader1.GetValue(14);
                (metroGrid2.Rows[row].Cells[14]).Value = dataReader1.GetValue(15);
                (metroGrid2.Rows[row].Cells[15]).Value = dataReader1.GetValue(16);

                row++;
            }
            double tot_p = 0, tot_sil = 0;
            for (int i = 0; i <= metroGrid2.RowCount - 1; i++)
            {
                tot_p += Convert.ToDouble(metroGrid2.Rows[i].Cells[6].Value);
                tot_sil += Convert.ToDouble(metroGrid2.Rows[i].Cells[7].Value);
            }
            metroTextBox3.Text = "" + tot_p;
            metroTextBox2.Text = "" + tot_sil;

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroLabel13.Text.ToLower().Contains("non"))
                metroButton9.PerformClick();
            else
                metroButton6.PerformClick();
            string tabel = metroLabel14.Text.ToString();
            int ind = tabel.IndexOf('_');
            string tabl = tabel.Substring(ind + 1);

            string strCheck = "Select sil_rt, br_rt, lab_rt, set_wt, gold_rt, silplate_rt, pro_rt from ledger where table_name='" + tabl + "' and code='" + metroLabel13.Text + "';";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd = new MySqlCommand(strCheck, con);
            con.Open();
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();
            while (dataReader1.Read())
            {
                numericUpDown10.Text = dataReader1.GetValue(0).ToString();
                numericUpDown11.Text = dataReader1.GetValue(1).ToString();
                numericUpDown9.Text = dataReader1.GetValue(2).ToString();
                numericUpDown8.Text = dataReader1.GetValue(3).ToString();
                numericUpDown7.Text = dataReader1.GetValue(4).ToString();
                numericUpDown6.Text = dataReader1.GetValue(5).ToString();
                numericUpDown12.Text = dataReader1.GetValue(6).ToString();
            }
            if (metroLabel13.Text.ToLower().Contains("non"))
                metroTextBox1.Select();
            else
                numericUpDown1.Select();

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
                metroButton5.Visible = true;
                metroButton5.PerformClick();
                metroButton5.Visible = false;
            this.Dispose();
            Application.OpenForms["Home"].BringToFront();
            Application.OpenForms["Packing"].BringToFront();
            Application.OpenForms["Packing"].Focus();
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }
        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            numericUpDown2.Select(0, numericUpDown2.Text.Length);
        }
        private void numericUpDown3_Enter(object sender, EventArgs e)
        {
            numericUpDown3.Select(0, numericUpDown3.Text.Length);
        }
        private void numericUpDown4_Enter(object sender, EventArgs e)
        {
            numericUpDown4.Select(0, numericUpDown4.Text.Length);
        }
        private void numericUpDown5_Enter(object sender, EventArgs e)
        {
            numericUpDown5.Select(0, numericUpDown5.Text.Length);
        }
        private void numericUpDown6_Enter(object sender, EventArgs e)
        {
            numericUpDown6.Select(0, numericUpDown6.Text.Length);
        }
        private void numericUpDown7_Enter(object sender, EventArgs e)
        {
            numericUpDown7.Select(0, numericUpDown7.Text.Length);
        }
        private void numericUpDown8_Enter(object sender, EventArgs e)
        {
            numericUpDown8.Select(0, numericUpDown8.Text.Length);
        }
        private void numericUpDown9_Enter(object sender, EventArgs e)
        {
            numericUpDown9.Select(0, numericUpDown9.Text.Length);
        }
        private void numericUpDown10_Enter(object sender, EventArgs e)
        {
            numericUpDown10.Select(0, numericUpDown10.Text.Length);
        }
        private void numericUpDown11_Enter(object sender, EventArgs e)
        {
            numericUpDown11.Select(0, numericUpDown11.Text.Length);
        }
        private void numericUpDown12_Enter(object sender, EventArgs e)
        {
            numericUpDown12.Select(0, numericUpDown12.Text.Length);
        }

        private void metroGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                metroLabel17.Text = r.ToString();
                metroButton4.Enabled = true;
                if (metroPanel1.Visible)
                {
                    metroButton10.Visible = true;
                    numericUpDown1.Text = metroGrid2.Rows[r].Cells[1].Value.ToString();
                    numericUpDown2.Text = metroGrid2.Rows[r].Cells[4].Value.ToString();
                    numericUpDown3.Text = "" + Convert.ToDouble(metroGrid2.Rows[r].Cells[5].Value + "") / 5;
                    numericUpDown4.Text = metroGrid2.Rows[r].Cells[6].Value.ToString();
                    numericUpDown5.Text = metroGrid2.Rows[r].Cells[7].Value.ToString();
                }
                else
                {
                    metroButton8.Visible = true;
                    metroTextBox1.Text = metroGrid2.Rows[r].Cells[2].Value.ToString();
                    numericUpDown16.Text = metroGrid2.Rows[r].Cells[4].Value.ToString();
                    numericUpDown15.Text = "" + Convert.ToDouble(metroGrid2.Rows[r].Cells[5].Value + "");
                    numericUpDown14.Text = metroGrid2.Rows[r].Cells[8].Value.ToString();
                    numericUpDown13.Text = metroGrid2.Rows[r].Cells[7].Value.ToString();
                }
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            int ind = Convert.ToInt32(metroLabel17.Text.ToString());
            int pcs = Convert.ToInt32(metroGrid2.Rows[ind].Cells[4].Value.ToString());
            int lot = Convert.ToInt32(metroGrid2.Rows[ind].Cells[1].Value.ToString());
            double wt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[5].Value.ToString());
            double rt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[8].Value.ToString());
            double stone = Convert.ToDouble(metroGrid2.Rows[ind].Cells[6].Value.ToString());
            double metwt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[7].Value.ToString());
            int row = 0;
            MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
            String query1 = "update stone set c_pcs=c_pcs+" + stone + ", c_qty=c_qty+" + wt + ", cr_amt=cr_amt+" + (wt * rt) + " where lot='" + lot + "';";
            MySqlCommand cmd2 = new MySqlCommand(query1, con2);
            con2.Open();
            if(metroPanel1.Visible)
                row = cmd2.ExecuteNonQuery();
            cmd2.CommandText = "Delete from " + metroLabel14.Text.ToString() + " where code='" + metroLabel13.Text + "' and lot='" + lot + "' and pcs='"+pcs+"' and wt='"+wt+"' and stones='"+stone+"' and metwt='"+metwt+"';";
            row = cmd2.ExecuteNonQuery();

            metroButton5.Visible = true;
            metroButton5.PerformClick();
            metroButton5.Visible = false;
            if (metroPanel1.Visible)
                metroButton6.PerformClick();
            else
                metroButton9.PerformClick();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            string tabel = metroLabel14.Text.ToString();
            int ind = tabel.IndexOf('_');
            string tabl = tabel.Substring(ind + 1);
            string code = metroLabel13.Text.ToString();
            int rows = 0;
            double ta_r = 0, pg_r = 0, pg_u = 0, pp_u = 0, ta_u = 0;
            for (rows = 0; rows < metroGrid2.Rows.Count; rows++)
            {
                ta_r += Convert.ToDouble(metroGrid2.Rows[rows].Cells[10].Value.ToString());
                ta_u += Convert.ToDouble(metroGrid2.Rows[rows].Cells[11].Value.ToString());
                pg_r += Convert.ToDouble(metroGrid2.Rows[rows].Cells[14].Value.ToString());
                pg_u += Convert.ToDouble(metroGrid2.Rows[rows].Cells[15].Value.ToString());
                pp_u += Convert.ToDouble(metroGrid2.Rows[rows].Cells[13].Value.ToString());
            }
            if (rows > 0)
            {
                pg_r /= rows;
                pg_u /= rows;
                pp_u /= rows;
            }
            string up = "UPDATE ledger set ta_r=" + ta_r + ", ta_u=" + ta_u + ", pg_r=" + pg_r + ", pg_u=" + pg_u + ", pp_u=" + pp_u + " where table_name='" + tabl + "' and code='" + code + "';";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd = new MySqlCommand(up, con);
            con.Open();
            int res = cmd.ExecuteNonQuery();

        }

        private void metroGrid2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
/*            foreach (DataGridViewRow row in metroGrid2.SelectedRows)
            {
                String dt = row.Cells[1].Value.ToString();
                String[] s11 = new String[3];
                dt = dt.Trim();
                s11 = dt.Split('-');
                Array.Reverse(s11);
                dt = String.Join("-", s11);
                MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                String query1 = "delete from metal_consume where dat='" + dt + "' and name='" + row.Cells[2].Value.ToString() + "' and type='" + row.Cells[3].Value.ToString() + "' and qty='" + row.Cells[5].Value.ToString() + "' and purity='" + row.Cells[4].Value.ToString() + "' and prodtype='" + row.Cells[7].Value.ToString() + "';";
                MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                con2.Open();
                cmd2.ExecuteNonQuery();




           /*     MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                String query1 = "update stone set c_pcs=c_pcs+" + row.Cells[6].Value + ", c_qty=c_qty+" + row.Cells[5].Value + ", cr_amt=cr_amt+" + (Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[8].Value)) + " where lot='" + row.Cells[1].Value + "';";
                MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                con2.Open();
                row = cmd2.ExecuteNonQuery();
                cmd2.CommandText = "Delete from " + metroLabel14.Text.ToString() + " where code='" + metroLabel13.Text + "' and lot='" + lot + "';";
                row = cmd2.ExecuteNonQuery();
                metroButton5.Visible = true;
                metroButton5.PerformClick();
                metroButton5.Visible = false;

                metroButton6.PerformClick();
            
            }*/
        }

        private void metroGrid2_Leave(object sender, EventArgs e)
        {
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            int ind = Convert.ToInt32(metroLabel17.Text.ToString());
            int pcs = Convert.ToInt32(metroGrid2.Rows[ind].Cells[4].Value.ToString());
            double lot = Convert.ToDouble(metroGrid2.Rows[ind].Cells[1].Value.ToString());
            double wt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[5].Value.ToString());
            double rt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[8].Value.ToString());
            double stone = Convert.ToDouble(metroGrid2.Rows[ind].Cells[6].Value.ToString());
            double metwt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[7].Value.ToString());

            double lot_o = lot;
            double pcs_o = pcs;
            double wt_o = wt;
            double rt_o = rt;
            double stone_o = stone;
            double metwt_o = metwt;
            int row = 0;
            MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
            String query1 = "update stone set c_pcs=c_pcs+" + stone_o + ", c_qty=c_qty+" + wt + ", cr_amt=cr_amt+" + (wt * rt) + " where lot='" + lot + "';";
            con2.Open();
            try
            {
                string date1 = metroDateTime1.Text;
                string dau = date1.Replace('-', '/');
                DateTime dvs = Convert.ToDateTime(dau);
                double exchg = Convert.ToDouble(metroLabel16.Text.ToString());
                String table = metroLabel14.Text;

                String code = metroLabel13.Text;
                pcs = Convert.ToInt32(numericUpDown2.Text.ToString());
                wt = Convert.ToDouble(numericUpDown3.Text.ToString()) * 5;
                double metal_wt = Convert.ToDouble(numericUpDown5.Text.ToString());
                stone = Convert.ToDouble(numericUpDown4.Text.ToString());
                //Select Count(*) from stone where lot=1 and c_pcs>="+pcs+" and c_qty>="+wt+";";
                    string strCheck = "Select Count(*) from stone where lot='" + lot_o + "' and c_pcs>=" + (stone-stone_o) + " and c_qty>=" + (wt-wt_o) + ";";
                    MessageBox.Show(strCheck);
                    MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                    MySqlCommand cmd = new MySqlCommand(strCheck, con);
                    con.Open();
                    MySqlDataReader dataReader1;
                    dataReader1 = cmd.ExecuteReader();
                    if (dataReader1.Read() && Convert.ToDouble(dataReader1.GetValue(0)) > 0)
                    {
                        string sp = "", unit = "";
                        double rate = 0;
                        MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                        row = cmd2.ExecuteNonQuery();
                        con2.Close();

                        MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                        String query = "Select specs, c_unit, nr from stone where lot='" + lot + "';";
                        //string query = "insert into " + table + "(code, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@j);";
                        MySqlCommand acmd = new MySqlCommand(query, con1);
                        con1.Open();
                        MySqlDataReader dataReader2;
                        dataReader2 = acmd.ExecuteReader();
                        while (dataReader2.Read())
                        {
                            sp = dataReader2.GetValue(0).ToString();
                            unit = dataReader2.GetValue(1).ToString();
                            rate = Convert.ToDouble(dataReader2.GetValue(2).ToString());
                        }
                        if (rate == 0)
                            rate = exchg;
                        dataReader2.Close();
                        double met_rt = 0;
                        if (Convert.ToDouble(numericUpDown10.Text.ToString()) > 0)
                            met_rt = Convert.ToDouble(numericUpDown10.Text.ToString());
                        else
                            met_rt = Convert.ToDouble(numericUpDown11.Text.ToString());
                        double amt = wt * rate;
                        double subtot = amt + (met_rt * metal_wt);
                        
                        query = "update " + table + " set code='"+code+"',descri='"+sp+"',unit='"+unit+"', pcs='"+pcs+"', wt='"+wt+"', stones='"+stone+"', metwt='"+metal_wt+"', rt='"+rt+"', subtot='"+subtot+"', exchg_rate='"+exchg+"' where lot='" + lot_o + "' and pcs='" + pcs_o + "' and wt='" + wt_o + "' and stones='" + stone_o + "' and metwt='" + metwt_o + "' ;";
                        acmd.CommandText = query;
                        
                        //MessageBox.Show(query);
                        int result = acmd.ExecuteNonQuery();
                        //MessageBox.Show(""+result);
                        
                        if (result > 0)
                        {

                            MySqlConnection con3 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                            query1 = "update stone set c_pcs=c_pcs-" + stone + ", c_qty=c_qty-" + wt + ", cr_amt=cr_amt-" + amt + " where lot='" + lot + "';";
                            MySqlCommand cmd3 = new MySqlCommand(query1, con3);
                            con3.Open();
                            int r1 = cmd3.ExecuteNonQuery();
                            if (r1 > 0)
                            {
                                metroButton6.PerformClick();
                            }
                            con3.Close();
                            
                        }
                        con1.Close();
                        numericUpDown1.Focus();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\n\nPlease Enter Correct Details!", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        metroButton6.PerformClick();
                    }
                    dataReader1.Close();
                    //                cmd.CommandText = "insert into " + metroLabel14.Text + "(code, lot, pcs, wt, stones, metwt, rt, subtot, nc_i, nc_u, pp_i, pp_u, pg_i, pg_u, date_pack, exchg_rate) VALUES (@a,@b);";

                    con.Close();
            }
            catch (Exception e1)
            {
                MetroMessageBox.Show(this, "\n\nSome Error Occured!!" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton11_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel22_Click(object sender, EventArgs e)
        {

        }

        private void metroButton11_Click_1(object sender, EventArgs e)
        {
            try
            {
                string date1 = metroDateTime1.Text;
                string dau = date1.Replace('-', '/');
                DateTime dvs = Convert.ToDateTime(dau);
                double exchg = Convert.ToDouble(metroLabel16.Text.ToString());
                String table = metroLabel14.Text;
                String code = metroLabel13.Text;
                double lot = 0;
                int pcs = Convert.ToInt32(numericUpDown16.Text.ToString());
                double wt = Convert.ToDouble(numericUpDown15.Text.ToString());
                double rate = Convert.ToDouble(numericUpDown14.Text.ToString());
                int stone = Convert.ToInt32(numericUpDown4.Text.ToString());
                double metwt = Convert.ToDouble(numericUpDown13.Text.ToString());
                string sp = metroTextBox1.Text, unit = "";
                        
                        MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                        String query = "Select specs, c_unit, nr from stone where lot='" + lot + "';";
                        //string query = "insert into " + table + "(code, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@j);";
                        MySqlCommand acmd = new MySqlCommand(query, con1);
                        con1.Open();
                        query = "insert into " + table + "(code,descri,unit, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l);";
                        acmd.CommandText = query;
                        acmd.Parameters.AddWithValue("@a", code);
                        acmd.Parameters.AddWithValue("@b", sp);
                        acmd.Parameters.AddWithValue("@c", unit);
                        acmd.Parameters.AddWithValue("@d", lot);
                        acmd.Parameters.AddWithValue("@e", pcs);
                        acmd.Parameters.AddWithValue("@f", wt);
                        acmd.Parameters.AddWithValue("@g", 0);
                        acmd.Parameters.AddWithValue("@h", metwt);
                        acmd.Parameters.AddWithValue("@i", rate);
                        double amt = pcs * rate;
                        acmd.Parameters.AddWithValue("@j", amt);
                        acmd.Parameters.AddWithValue("@k", dvs);
                        acmd.Parameters.AddWithValue("@l", exchg);

                        int result = acmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                                metroButton9.PerformClick();
                        }
                        con1.Close();
                        metroTextBox1.Focus();
            }
            catch (Exception e1)
            {
                MetroMessageBox.Show(this, "\n\nSome Error Occured!!" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDown16_Enter(object sender, EventArgs e)
        {
            numericUpDown16.Select(0, numericUpDown16.Text.Length);
        }

        private void numericUpDown15_Enter(object sender, EventArgs e)
        {
            numericUpDown15.Select(0, numericUpDown15.Text.Length);
        }

        private void numericUpDown14_Enter(object sender, EventArgs e)
        {
            numericUpDown14.Select(0, numericUpDown14.Text.Length);
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            //metroButton2.PerformClick();
            
            numericUpDown13.Text = "0";
            numericUpDown14.Text = "0";
            numericUpDown15.Text = "0";
            numericUpDown16.Text = "0";
            metroTextBox1.Text = "";
            metroButton8.Visible = false;
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            metroGrid2.Rows.Clear();
            metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            String tabl = metroLabel14.Text.ToString();
            String code = metroLabel13.Text.ToString();
            int ind = tabl.IndexOf('_');
            String tabel = tabl.Substring(ind + 1);
            String query = "Select * from " + tabl + ", ledger where " + tabl + ".code='" + code + "' and ledger.table_name=" + tabel + " and ledger.code='" + code + "' and ledger.code=" + tabl + ".code ;";
            //MessageBox.Show(query);
            MySqlCommand cmd1;
            cmd1 = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader dataReader1;
            dataReader1 = cmd1.ExecuteReader();
            int row = 0;
            while (dataReader1.Read())
            {
                int index = this.metroGrid2.Rows.Count;
                index++;
                this.metroGrid2.Rows.Add();
                (metroGrid2.Rows[row].Cells[0]).Value = row + 1;
                int lt1 = Convert.ToInt32(dataReader1.GetValue(4));
                if (lt1 != 103 && lt1 != 866)
                    (metroGrid2.Rows[row].Cells[1]).Value = lt1;
                else
                    (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(4);

                //                (metroGrid2.Rows[row].Cells[1]).Value = dataReader1.GetValue(4).ToString();
                (metroGrid2.Rows[row].Cells[2]).Value = dataReader1.GetValue(2).ToString();
                (metroGrid2.Rows[row].Cells[3]).Value = dataReader1.GetValue(3);
                (metroGrid2.Rows[row].Cells[4]).Value = dataReader1.GetValue(5);
                (metroGrid2.Rows[row].Cells[5]).Value = dataReader1.GetValue(6);
                (metroGrid2.Rows[row].Cells[6]).Value = dataReader1.GetValue(7);
                (metroGrid2.Rows[row].Cells[7]).Value = dataReader1.GetValue(8);
                (metroGrid2.Rows[row].Cells[8]).Value = dataReader1.GetValue(9);
                (metroGrid2.Rows[row].Cells[9]).Value = dataReader1.GetValue(10);
                (metroGrid2.Rows[row].Cells[10]).Value = dataReader1.GetValue(11);
                (metroGrid2.Rows[row].Cells[11]).Value = dataReader1.GetValue(12);
                (metroGrid2.Rows[row].Cells[12]).Value = dataReader1.GetValue(13);
                (metroGrid2.Rows[row].Cells[13]).Value = dataReader1.GetValue(14);
                (metroGrid2.Rows[row].Cells[14]).Value = dataReader1.GetValue(15);
                (metroGrid2.Rows[row].Cells[15]).Value = dataReader1.GetValue(16);

                row++;
            }
            double tot_p = 0, tot_sil = 0;
            for (int i = 0; i <= metroGrid2.RowCount - 1; i++)
            {
                tot_p += Convert.ToDouble(metroGrid2.Rows[i].Cells[6].Value);
                tot_sil += Convert.ToDouble(metroGrid2.Rows[i].Cells[7].Value);
            }
            metroTextBox3.Text = "" + tot_p;
            metroTextBox2.Text = "" + tot_sil;


        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = Convert.ToInt32(metroLabel17.Text.ToString());
                String o_sp = metroGrid2.Rows[ind].Cells[2].Value.ToString();
                double o_pcs = Convert.ToDouble(metroGrid2.Rows[ind].Cells[4].Value.ToString());
                double o_wt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[5].Value.ToString());
                double o_rt = Convert.ToDouble(metroGrid2.Rows[ind].Cells[8].Value.ToString());
                
                String table = metroLabel14.Text;
                String code = metroLabel13.Text;
                double lot = 0;
                int pcs = Convert.ToInt32(numericUpDown16.Text.ToString());
                double wt = Convert.ToDouble(numericUpDown15.Text.ToString());
                double rate = Convert.ToDouble(numericUpDown14.Text.ToString());
                int stone = Convert.ToInt32(numericUpDown4.Text.ToString());
                double metwt = Convert.ToDouble(numericUpDown13.Text.ToString());
                string sp = metroTextBox1.Text, unit = "";

                MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                String query = "Select specs, c_unit, nr from stone where lot='" + lot + "';";
                //string query = "insert into " + table + "(code, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@j);";
                MySqlCommand acmd = new MySqlCommand(query, con1);
                con1.Open();
                query = "update " + table + " set metwt=@v, descri=@b, pcs=@e, wt=@f, rt=@i, subtot=@j where descri='" + o_sp + "' and pcs='" + o_pcs + "' and wt='" + o_wt + "' and rt='" + o_rt + "';";
                acmd.CommandText = query;
                acmd.Parameters.AddWithValue("@b", sp);
                acmd.Parameters.AddWithValue("@e", pcs);
                acmd.Parameters.AddWithValue("@f", wt);
                acmd.Parameters.AddWithValue("@i", rate);
                double amt = pcs * rate;
                acmd.Parameters.AddWithValue("@j", amt);
                acmd.Parameters.AddWithValue("@v", metwt);

                int result = acmd.ExecuteNonQuery();
                if (result > 0)
                {
                    metroButton9.PerformClick();
                }
                con1.Close();
                metroTextBox1.Focus();
            }
            catch (Exception e1)
            {
                MetroMessageBox.Show(this, "\n\nSome Error Occured!!" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton12_Click(object sender, EventArgs e)
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
                str = (string)(range.Cells[1,1] as Excel.Range).Value2;
                int com = str.IndexOf(',');
                str = str.Substring(0, com);
                //MessageBox.Show(str + "  " + metroLabel13.Text.ToString().ToLower());
                if (str.ToLower().Equals(metroLabel13.Text.ToString().ToLower()))
                {
                    
                    try
                    {
                        for (rCnt = 3; rCnt <= range.Rows.Count; rCnt++)
                        {
                            //for (cCnt = 1; cCnt <= 5; cCnt++)
                            //{
                                //MessageBox.Show("Hello");
                                str = (string)(range.Cells[rCnt, 1] as Excel.Range).Value2;
                                //MessageBox.Show("Hello");
                                if (str.StartsWith("END"))
                                {
                                    end = 1;
                                    break;
                                }
                            //}
                            if (end == 1)
                                break;
                            //MessageBox.Show("Hello");
                            String[] s11 = new String[6];
                            str = str.Trim();
                            s11 = str.Split(',');
                            if (str.ToLower().Contains("non"))
                            {
                                /*try
                                {
                                    string date1 = metroDateTime1.Text;
                                    string dau = date1.Replace('-', '/');
                                    DateTime dvs = Convert.ToDateTime(dau);
                                    double exchg = Convert.ToDouble(metroLabel16.Text.ToString());
                                    String table = metroLabel14.Text;
                                    String code = metroLabel13.Text;
                                    lot = "0";
                                    int pcs = Convert.ToInt32(numericUpDown16.Text.ToString());
                                    double wt = Convert.ToDouble(numericUpDown15.Text.ToString());
                                    double rate = Convert.ToDouble(numericUpDown14.Text.ToString());
                                    int stone = Convert.ToInt32(numericUpDown4.Text.ToString());
                                    string sp = metroTextBox1.Text, unit = "";

                                    MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                                    String query = "Select specs, c_unit, nr from stone where lot='" + lot + "';";
                                    //string query = "insert into " + table + "(code, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@j);";
                                    MySqlCommand acmd = new MySqlCommand(query, con1);
                                    con1.Open();
                                    query = "insert into " + table + "(code,descri,unit, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l);";
                                    acmd.CommandText = query;
                                    acmd.Parameters.AddWithValue("@a", code);
                                    acmd.Parameters.AddWithValue("@b", sp);
                                    acmd.Parameters.AddWithValue("@c", unit);
                                    acmd.Parameters.AddWithValue("@d", lot);
                                    acmd.Parameters.AddWithValue("@e", pcs);
                                    acmd.Parameters.AddWithValue("@f", wt);
                                    acmd.Parameters.AddWithValue("@g", 0);
                                    acmd.Parameters.AddWithValue("@h", 0);
                                    acmd.Parameters.AddWithValue("@i", rate);
                                    double amt = pcs * rate;
                                    acmd.Parameters.AddWithValue("@j", amt);
                                    acmd.Parameters.AddWithValue("@k", dvs);
                                    acmd.Parameters.AddWithValue("@l", exchg);

                                    int result = acmd.ExecuteNonQuery();
                                    if (result > 0)
                                    {
                                        metroButton9.PerformClick();
                                    }
                                    con1.Close();
                                    metroTextBox1.Focus();
                                }
                                catch (Exception e1)
                                {
                                    MetroMessageBox.Show(this, "\n\nSome Error Occured!!" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }*/
                            }
                            else
                            {
                                try
                                {
                                    string date1 = metroDateTime1.Text;
                                    string dau = date1.Replace('-', '/');
                                    DateTime dvs = Convert.ToDateTime(dau);
                                    double exchg = Convert.ToDouble(metroLabel16.Text.ToString());
                              //      MessageBox.Show("Hello");
                                    String table = metroLabel14.Text;
                                    String code = metroLabel13.Text;
                                    lot = s11[0].ToString();
                                    int pcs=0,stone=0;
                                    double wt=0,metal_wt=0;
                                    if(!s11[1].ToString().Equals(""))
                                        pcs = Convert.ToInt32(s11[1].ToString());
                                    if(!s11[2].ToString().Equals(""))
                                        wt=Convert.ToDouble(s11[2].ToString())*5;
                                    if(!s11[3].ToString().Equals(""))
                                        stone = Convert.ToInt32(s11[3].ToString());
                                    if (!s11[4].ToString().Equals(""))
                                        metal_wt = Convert.ToDouble(s11[4].ToString());
                                    int exp=0;
                                    if (exp == 0)
                                    {
                                //        MessageBox.Show("Hello");
                                        string strCheck = "Select Count(*) from stone where lot='" + lot + "' and c_pcs>=" + stone + " and c_qty>=" + wt + ";";
                    //MessageBox.Show(strCheck);
                                        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                                        MySqlCommand cmd = new MySqlCommand(strCheck, con);
                                        con.Open();
                                        MySqlDataReader dataReader1;
                                        dataReader1 = cmd.ExecuteReader();
                                        if (dataReader1.Read() && Convert.ToDouble(dataReader1.GetValue(0)) > 0)
                                        {
                                            string sp = "", unit = "";
                                  //          MessageBox.Show("Hello");
                                            double rate = 0;
                                            MySqlConnection con1 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                                            String query = "Select specs, c_unit, nr from stone where lot='" + lot + "';";
                        //string query = "insert into " + table + "(code, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@j);";
                                            MySqlCommand acmd = new MySqlCommand(query, con1);
                                            con1.Open();
                                            MySqlDataReader dataReader2;
                                            dataReader2 = acmd.ExecuteReader();
                                            while (dataReader2.Read())
                                            {
                                                sp = dataReader2.GetValue(0).ToString();
                                                unit = dataReader2.GetValue(1).ToString();
                                                rate = Convert.ToDouble(dataReader2.GetValue(2).ToString());
                                            }
                                    //        MessageBox.Show("Hello");
                                            if (rate == 0)
                                            rate = exchg;
                                            dataReader2.Close();
                                            query = "insert into " + table + "(code,descri,unit, lot, pcs, wt, stones, metwt, rt, subtot, date_pack, exchg_rate) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l);";
                                            acmd.CommandText = query;
                                            acmd.Parameters.AddWithValue("@a", code);
                                            acmd.Parameters.AddWithValue("@b", sp);
                                            acmd.Parameters.AddWithValue("@c", unit);
                                            acmd.Parameters.AddWithValue("@d", lot);
                                            acmd.Parameters.AddWithValue("@e", pcs);
                                            acmd.Parameters.AddWithValue("@f", wt);
                                            acmd.Parameters.AddWithValue("@g", stone);
                                            acmd.Parameters.AddWithValue("@h", metal_wt);
                                            acmd.Parameters.AddWithValue("@i", rate);
                                            double met_rt = 0;
                                            if (Convert.ToDouble(numericUpDown10.Text.ToString()) > 0)
                                                met_rt = Convert.ToDouble(numericUpDown10.Text.ToString());
                                            else
                                                met_rt = Convert.ToDouble(numericUpDown11.Text.ToString());
                                            double amt = wt * rate;
                                            double subtot = amt + (met_rt * metal_wt);
                                            acmd.Parameters.AddWithValue("@j", subtot);
                                            acmd.Parameters.AddWithValue("@k", dvs);
                                            acmd.Parameters.AddWithValue("@l", exchg);
                                      //      MessageBox.Show("Hello");

                                            int result = acmd.ExecuteNonQuery();
                                            if (result > 0)
                                            {
                                                MySqlConnection con2 = new MySqlConnection("Server=localhost;Database=SilverCity;UID=root;Password=smhs;");
                                                String query1 = "update stone set c_pcs=c_pcs-" + stone + ", c_qty=c_qty-" + wt + ", cr_amt=cr_amt-" + amt + " where lot='" + lot + "';";
                                                MySqlCommand cmd2 = new MySqlCommand(query1, con2);
                                                con2.Open();
                                                int r1 = cmd2.ExecuteNonQuery();
                                            }
                                            con1.Close();
                                            
                                        }
                                        else
                                        {
                                            MetroMessageBox.Show(this, "\n\nPlease Enter Correct Details! \n\nError in Lot : "+lot, "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                        }
                                        dataReader1.Close();
                                        con.Close();
                                    }
                                    else
                                    {
                                        MetroMessageBox.Show(this, "\nYou Cannot enter a sub-product with Same Lot No.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                catch (Exception e1)
                                {
                                    MetroMessageBox.Show(this, "\n\nSome Error Occured!!"+e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                       }
                  }
                  catch (Exception e1)
                  {
                        MetroMessageBox.Show(this, "\nError in " + lot + "\n" + e1.GetBaseException(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                    if (metroPanel1.Visible)
                        metroButton6.PerformClick();
                    else
                        metroButton9.PerformClick();
                    MetroMessageBox.Show(this, "\nIMPORT SUCCESSFULLY COMPLETED!", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            else
            {
                MetroMessageBox.Show(this, "\nIncorrect ITEM CODE found in File...  ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
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

        private void numericUpDown13_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown13.Select(0, numericUpDown13.Text.Length);
        }

        private void numericUpDown13_Enter(object sender, EventArgs e)
        {
            numericUpDown13.Select(0, numericUpDown13.Text.Length);
        }



    }
}
