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

namespace Surya
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("C:\\Windows\\Resources\\imp.mac"))
            {
                MetroMessageBox.Show(this, "Sorry! You are not Authorized to use this Software!!\n                     Contact : \nSHUBHAM GUPTA - 9024350461 - shubham.g9@outlook.com !!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();                
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            String user = metroTextBox1.Text.ToString().ToLower();
            String pass = metroTextBox2.Text.ToString();
            MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=login;UID=root;PASSWORD=smhs;");
            String q1 = "Select * from log where user='"+user+"' and pass='"+pass+"';";
            MySqlCommand cmd1 = new MySqlCommand(q1, con);
            MySqlDataReader dataReader1;
            con.Open();
            dataReader1 = cmd1.ExecuteReader();
            if (dataReader1.Read())
            {
                string subPath = @"C:\Silver City"; // your code goes here
                System.IO.Directory.CreateDirectory(subPath);
                subPath = @"C:\Silver City\Files";
                System.IO.Directory.CreateDirectory(subPath);
                subPath = @"C:\Silver City\Certi";
                System.IO.Directory.CreateDirectory(subPath);
                Home h = new Home();
                h.Visible = true;
                h.Text = "WELCOME SilverCity Jewels";
                if (user.Equals("employ"))
                    h.metroButton2.PerformClick();
                Visible = false;
                h.metroButton2.Visible = false;

            }
            else
            {
                MetroMessageBox.Show(this, "\n\nInvalid Username or Password!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            metroTextBox2.Text = "";
            metroTextBox1.Select();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
