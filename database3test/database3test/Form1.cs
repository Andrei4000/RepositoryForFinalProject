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
            try
            {
                connection.Open();
                CheckConnection.Text = "Connection successful";
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = $"select * from Users where Username = '{txt_Username.Text} ' AND Pass = '{txt_Password.Text}'";
            OleDbDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            if (count == 1)
            {
                MessageBox.Show("Username and password are correct");
                connection.Close();
                connection.Dispose();
                this.Hide();
                Form2 f2 = new Form2(txt_Username.Text);
                txt_Username.Clear();
                txt_Password.Clear();
                f2.Show();

            }
            else if (count > 1)
            {
                MessageBox.Show("Duplicate username and pass");
            }
            else if (count < 1) { MessageBox.Show("Incorrect username or password"); }


            connection.Close();

        }
    }
}
