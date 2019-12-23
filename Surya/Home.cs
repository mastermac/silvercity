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
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions; 

namespace Surya
{
    public partial class Home : MetroForm
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Add_Stock c = new Add_Stock();
            c.Visible = true;
            c.metroButton2.PerformClick();
            //invent c1 = new invent();
            //c1.Visible = true;

        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            Mbox_Search s = new Mbox_Search();
            s.Visible = true;
            s.metroLabel2.Text = "search";

        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            Item_Addd s = new Item_Addd();
            s.Visible = true;
            
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            all_invent a = new all_invent();
            a.metroLabel7.Text = "available";
            a.Text = "AVAILABLE INVENTORY DETAILS";
            a.Visible = true;
            
            //a.metroLabel1.Text = "all";
            //a.Text = "ALL STOCK DETAILS";
            //a.metroButton1.PerformClick();
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            all_invent a = new all_invent();
            a.metroLabel7.Text = "sold";
            a.Text = "SOLD OUT INVENTORY DETAILS";
            a.Visible = true;
            //a.metroButton1.PerformClick();
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            metal_invent m = new metal_invent();
            m.Visible = true;
//            Transaction t = new Transaction();
  //          t.Text = "PAYMENT BREAKUP DETAILS";
    //        t.Visible = true;
        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            Item_list b = new Item_list();
            b.Visible = true;

            /*Buyer_List b = new Buyer_List();
            b.Visible = true;
            b.Text = "SELLER LIST";
            b.metroLabel1.Text = "S";
            b.metroButton1.PerformClick();
              */
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroStyleManager.Theme = metroStyleManager.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
        }

        private void metroTile10_Click(object sender, EventArgs e)
        {
            /*Buyer_Add b = new Buyer_Add();
            b.Visible = true;
            b.metroButton3.PerformClick();*/
        }

        private void metroTile11_Click(object sender, EventArgs e)
        {
            //All_Stock a = new All_Stock();
            //a.Visible = true;
            //a.metroLabel1.Text = "hold";
            //a.Text = "ON-HOLD STOCK LIST";
            //a.metroButton1.PerformClick();

            //Seller_Add s = new Seller_Add();
            //s.Visible = true;

        }

        private void metroTile12_Click(object sender, EventArgs e)
        {
            /*All_Stock a = new All_Stock();
            a.Visible = true;
            a.metroLabel1.Text = "sold";
            a.Text = "CLEARED TRANSACTIONS DETAIL";
            a.metroButton1.PerformClick();
             */ 
        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            //Mbox_Search s = new Mbox_Search();
            //s.Visible = true;
            //s.metroLabel2.Text = "Search";

            Packing a = new Packing();
            a.Visible = true;
        }

        private void metroTile17_Click(object sender, EventArgs e)
        {
/*            Buyer_List b = new Buyer_List();
            b.Visible = true;
            b.Text = "BUYER LIST";
            b.metroLabel1.Text = "B";
            b.metroButton1.PerformClick();*/

            all_invent a = new all_invent();
            a.metroLabel7.Text = "all";
            a.Text = "ALL INVENTORY DETAILS";
            a.Visible = true;

        }

        private void metroTile18_Click(object sender, EventArgs e)
        {
            /*Cash_Depo c = new Cash_Depo();
            c.Visible = true;*/

        }

        private void metroTile19_Click(object sender, EventArgs e)
        {
            //rapchange c = new rapchange();
            //c.Visible = true;
        }

        private void metroTile15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C://Silver City");
        }

        private void metroTile14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("WWW.GMAIL.COM");
        }

        private void metroTile16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("WWW.GOOGLE.COM");
        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            p.WaitForInputIdle();
           
            //SNativeMethods.SetParent(p.MainWindowHandle, this.Handle);
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
//            e.CloseReason == CloseReason.UserClosing;
        }

        private void metroTile21_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
        
        }

        private void metroTile22_Click(object sender, EventArgs e)
        {
            while (Application.OpenForms.Count>1)
            {
                Application.OpenForms[1].Close();
                
            }
            Close();
            Form1 f = new Form1();
            f.Visible = true;
            f.metroButton2.PerformClick();
            
            
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroTile10.Visible = false;
            metroTile12.Visible = false;
            metroTile17.Visible = false;
            metroTile18.Visible = false;
            metroTile9.Visible = false;
            metroTile19.Visible = false;
            metroTile20.Visible = false;
            metroButton2.Visible = false;
        }

        private void metroTile20_Click(object sender, EventArgs e)
        {
            Set s = new Set();
            s.Visible = true;
        }

        private void metroTile4_Click_1(object sender, EventArgs e)
        {
            Metal_Consume s = new Metal_Consume();
            s.Visible = true;
        }

        private void metroTile6_Click_1(object sender, EventArgs e)
        {
            Metal_Purchase s = new Metal_Purchase();
            s.Visible = true;
//            s.metroLabel2.Text = "delete";
        }

        private void metroTile1_Enter(object sender, EventArgs e)
        {
            metroTile1.UseCustomForeColor = true;

            metroTile1.ForeColor = Color.Black;
        }
        private void metroTile2_Enter(object sender, EventArgs e)
        {
            metroTile2.UseCustomForeColor = true;

            metroTile2.ForeColor = Color.Black;
        }
        private void metroTile3_Enter(object sender, EventArgs e)
        {
            metroTile3.UseCustomForeColor = true;

            metroTile3.ForeColor = Color.Black;
        }
        private void metroTile4_Enter(object sender, EventArgs e)
        {
            metroTile4.UseCustomForeColor = true;

            metroTile4.ForeColor = Color.Black;
        }


        private void metroTile6_Enter(object sender, EventArgs e)
        {
            metroTile6.UseCustomForeColor = true;

//            MessageBox.Show("hello");
            metroTile6.ForeColor = Color.Black;
        }
        private void metroTile5_Enter(object sender, EventArgs e)
        {
            metroTile5.UseCustomForeColor = true;
            metroTile5.ForeColor = Color.Black;
        }
        private void metroTile7_Enter(object sender, EventArgs e)
        {
            metroTile7.UseCustomForeColor = true;

            metroTile7.ForeColor = Color.Black;
        }
        private void metroTile8_Enter(object sender, EventArgs e)
        {
            metroTile8.UseCustomForeColor = true;
            metroTile8.ForeColor = Color.Black;
        }
        private void metroTile9_Enter(object sender, EventArgs e)
        {
            metroTile9.UseCustomForeColor = true;
            metroTile9.ForeColor = Color.Black;
        }
        private void metroTile10_Enter(object sender, EventArgs e)
        {
            metroTile10.UseCustomForeColor = true;
            metroTile10.ForeColor = Color.Black;
        }
        private void metroTile11_Enter(object sender, EventArgs e)
        {
            metroTile11.UseCustomForeColor = true;
            metroTile11.ForeColor = Color.Black;
        }
        private void metroTile12_Enter(object sender, EventArgs e)
        {
            metroTile12.UseCustomForeColor = true;
            metroTile12.ForeColor = Color.Black;
        }
        private void metroTile17_Enter(object sender, EventArgs e)
        {
            metroTile17.UseCustomForeColor = true;
            metroTile17.ForeColor = Color.Black;
        }
        private void metroTile18_Enter(object sender, EventArgs e)
        {
            metroTile18.UseCustomForeColor = true;
            metroTile18.ForeColor = Color.Black;
        }
        private void metroTile19_Enter(object sender, EventArgs e)
        {
            metroTile19.UseCustomForeColor = true;
            metroTile19.ForeColor = Color.Black;
        }
        private void metroTile20_Enter(object sender, EventArgs e)
        {
            metroTile20.UseCustomForeColor = true;
            metroTile20.ForeColor = Color.Black;
        }
        private void metroTile21_Enter(object sender, EventArgs e)
        {
            metroTile21.UseCustomForeColor = true;
            metroTile21.ForeColor = Color.Black;
        }
        private void metroTile22_Enter(object sender, EventArgs e)
        {
            metroTile22.UseCustomForeColor = true;
            metroTile22.ForeColor = Color.Black;
        }

        private void metroTile1_Leave(object sender, EventArgs e)
        {
            metroTile1.ForeColor = Color.White;
        }
        private void metroTile2_Leave(object sender, EventArgs e)
        {
            metroTile2.ForeColor = Color.White;
        }
        private void metroTile3_Leave(object sender, EventArgs e)
        {
            metroTile3.ForeColor = Color.White;
        }
        private void metroTile4_Leave(object sender, EventArgs e)
        {
            metroTile4.ForeColor = Color.White;
        }
        private void metroTile5_Leave(object sender, EventArgs e)
        {
            metroTile5.ForeColor = Color.White;
        }
        private void metroTile6_Leave(object sender, EventArgs e)
        {
            metroTile6.ForeColor = Color.White;
        }
        private void metroTile7_Leave(object sender, EventArgs e)
        {
            metroTile7.ForeColor = Color.White;
        }
        private void metroTile8_Leave(object sender, EventArgs e)
        {
            metroTile8.ForeColor = Color.White;
        }
        private void metroTile9_Leave(object sender, EventArgs e)
        {
            metroTile9.ForeColor = Color.White;
        }
        private void metroTile10_Leave(object sender, EventArgs e)
        {
            metroTile10.ForeColor = Color.White;
        }
        private void metroTile11_Leave(object sender, EventArgs e)
        {
            metroTile11.ForeColor = Color.White;
        }
        private void metroTile12_Leave(object sender, EventArgs e)
        {
            metroTile12.ForeColor = Color.White;
        }
        private void metroTile17_Leave(object sender, EventArgs e)
        {
            metroTile17.ForeColor = Color.White;
        }
        private void metroTile18_Leave(object sender, EventArgs e)
        {
            metroTile18.ForeColor = Color.White;
        }
        private void metroTile19_Leave(object sender, EventArgs e)
        {
            metroTile19.ForeColor = Color.White;
        }
        private void metroTile20_Leave(object sender, EventArgs e)
        {
            metroTile20.ForeColor = Color.White;
        }
        private void metroTile21_Leave(object sender, EventArgs e)
        {
            metroTile21.ForeColor = Color.White;
        }
        private void metroTile22_Leave(object sender, EventArgs e)
        {
            metroTile22.ForeColor = Color.White;
        }








    }
}
