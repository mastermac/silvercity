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
namespace Surya
{
    public partial class item_history : MetroForm
    {
        public item_history()
        {
            InitializeComponent();
        }

        private void item_history_Load(object sender, EventArgs e)
        {
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            metroGrid2.Rows.Clear();
            metroGrid2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            metroGrid2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            string tabl = metroLabel1.Text.ToString();
            string strCheck = "SHOW TABLES LIKE 'inv_%';";
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
            MySqlCommand cmd = new MySqlCommand(strCheck, con);
            con.Open();
            //cmd.Prepare();
            MySqlDataReader dataReader1;
            dataReader1 = cmd.ExecuteReader();
            int index = 0, row = 0;
            double tot = 0;
            String s1 = "",inv="";
            int t=0;
            while (dataReader1.Read())
            {
                inv = dataReader1.GetValue(0).ToString();
                String one = "Select date_pack,descri,pcs,wt,unit,rt,subtot from " + inv + " where lot='" + tabl + "';";
                MySqlConnection con1 = new MySqlConnection("SERVER=localhost;DATABASE=SilverCity;UID=root;PASSWORD=smhs;");
                MySqlCommand cmd1 = new MySqlCommand(one, con1);
                con1.Open();
                MySqlDataReader dataReader2;
                dataReader2 = cmd1.ExecuteReader();
                while (dataReader2.Read())
                {
                    if (index != 0)
                        index = this.metroGrid2.Rows.Count;

                    index++;
                    this.metroGrid2.Rows.Add();
                    (metroGrid2.Rows[row].Cells[0]).Value = row + 1;
                    s1 = dataReader2.GetValue(0).ToString();
                    DateTime d = Convert.ToDateTime(s1);
                    (metroGrid2.Rows[row].Cells[1]).Value = d.ToShortDateString();
                    (metroGrid2.Rows[row].Cells[2]).Value = inv;
                    (metroGrid2.Rows[row].Cells[3]).Value = dataReader2.GetValue(1);
                    (metroGrid2.Rows[row].Cells[4]).Value = dataReader2.GetValue(2);
                    (metroGrid2.Rows[row].Cells[5]).Value = dataReader2.GetValue(3);
                    (metroGrid2.Rows[row].Cells[6]).Value = dataReader2.GetValue(4);
                    (metroGrid2.Rows[row].Cells[7]).Value = dataReader2.GetValue(5);
                    (metroGrid2.Rows[row].Cells[8]).Value = dataReader2.GetValue(6);
                    tot = tot + Convert.ToDouble(dataReader2.GetValue(6));
                    row++;
                    t = 1;
                }
                dataReader2.Close();
                con1.Close();
            }

            if(t==0)
            {
                MetroMessageBox.Show(this, "No History to show!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Dispose();
                Application.OpenForms["Home"].BringToFront();
                Application.OpenForms["all_invent"].BringToFront();
            }
            
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.OpenForms["Home"].BringToFront();
            Application.OpenForms["all_invent"].BringToFront();
            Application.OpenForms["all_invent"].Focus();

        }

        private void metroGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void item_history_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void item_history_Enter(object sender, EventArgs e)
        {
        }

        private void item_history_Shown(object sender, EventArgs e)
        {
            metroButton6.PerformClick();

        }
    }
}
