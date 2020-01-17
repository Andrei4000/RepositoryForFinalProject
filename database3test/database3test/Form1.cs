using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; 

namespace database3test
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\andre\Desktop\users1.accdb;Persist Security Info=False;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
           
          
        }
    }
}
