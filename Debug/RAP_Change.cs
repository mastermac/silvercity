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


namespace Surya
{
    public partial class rapchange : MetroForm
    {
        public rapchange()
        {
            InitializeComponent();
        }

        private void RAP_Change_Load(object sender, EventArgs e)
        {
            metroButton1.PerformClick();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            String q1 = "Select id,wt,color,purity,shape,cps,cpc,rap,disc,rate,amt from invent where status!='S' order by date_pur;";

            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            int row = 0;
            int count = 1;

            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=Sales;UID=root;PASSWORD=smhs; Connection Timeout=30; Min Pool Size=20; Max Pool Size=300;");
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dataReader1;
            con1.Open();
            dataReader1 = cmd1.ExecuteReader();
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
                (metroGrid1.Rows[row].Cells[3]).Value = dataReader1.GetValue(2);
                (metroGrid1.Rows[row].Cells[4]).Value = dataReader1.GetValue(3);
                (metroGrid1.Rows[row].Cells[5]).Value = dataReader1.GetValue(4);
                (metroGrid1.Rows[row].Cells[6]).Value = dataReader1.GetValue(5);
                (metroGrid1.Rows[row].Cells[7]).Value = dataReader1.GetValue(6);
                (metroGrid1.Rows[row].Cells[8]).Value = Convert.ToInt32(dataReader1.GetValue(7));
                (metroGrid1.Rows[row].Cells[9]).Value = dataReader1.GetValue(8);
                (metroGrid1.Rows[row].Cells[10]).Value = dataReader1.GetValue(9);
                (metroGrid1.Rows[row].Cells[11]).Value = dataReader1.GetValue(10);

                count++;
                row++;
            }
                                
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string id = "",color="",purity="",shape="",cps="";
            double wt = 0, rap = 0, disc = 0,disc1=0, dol = 0, rate = 0, amt = 0;
            MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=sales;UID=root;PASSWORD=smhs;");
            MySqlConnection con2 = new MySqlConnection("SERVER=localhost;DATABASE=sales;UID=root;PASSWORD=smhs;");
            con1.Open();
            String q1 = "Select dollar from glob;";
            MySqlCommand cmd1 = new MySqlCommand(q1, con1);
            MySqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                dol = Convert.ToDouble(dr.GetValue(0));
            }
            dr.Close();
            for (int rows = 0; rows < metroGrid1.Rows.Count; rows++)
            {
                id = metroGrid1.Rows[rows].Cells[1].Value.ToString();
                color = metroGrid1.Rows[rows].Cells[3].Value.ToString();
                purity = metroGrid1.Rows[rows].Cells[4].Value.ToString();
                shape = metroGrid1.Rows[rows].Cells[5].Value.ToString();
                cps = metroGrid1.Rows[rows].Cells[6].Value.ToString();
                wt = Convert.ToDouble(metroGrid1.Rows[rows].Cells[2].Value.ToString());
                rap = Convert.ToDouble(metroGrid1.Rows[rows].Cells[7].Value.ToString());
                disc = Convert.ToDouble(metroGrid1.Rows[rows].Cells[8].Value.ToString());
                rate = Convert.ToDouble(metroGrid1.Rows[rows].Cells[9].Value.ToString());
                amt = Convert.ToDouble(metroGrid1.Rows[rows].Cells[10].Value.ToString());

                disc1 = (100 - disc) * .01;
                rate = rap * dol * disc1;
                amt = rate * wt;
                con2.Open();
                String q2 = "Update invent set rate=" + rate + ", amt=" + amt + ", rap="+rap+", disc="+disc+", wt="+wt+", color='"+color+"', purity='"+purity+"', shape='"+shape+"', cps='"+cps+"' where id='" + id + "';";
                MySqlCommand cmd2 = new MySqlCommand(q2, con2);
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
            MetroMessageBox.Show(this, "DATABASE IS UPDATED!!", "UPDATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            metroButton1.PerformClick();
            Application.OpenForms["Home"].BringToFront();
            Application.OpenForms["RAP_Change"].BringToFront();

        }
    }
}
