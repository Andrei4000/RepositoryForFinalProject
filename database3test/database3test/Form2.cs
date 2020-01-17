﻿using System;
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
    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        string User;
        int nrusers;
        double usermoney = 0;
        string appDate = System.DateTime.Now.ToString("dd/MM/yyyy");
        //string appDate = "23/02/2018";  //15/03/2019  
        string line;
        string clock;
        bool alarm = false;

        public Form2(string Username)
        {

            InitializeComponent();
            User = Username;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\andre\Desktop\users1.accdb;Persist Security Info=False;";
            if (User == "admin")
            {
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPage6);
                tabControl1.TabPages.Remove(tabPage7);
                tabControl1.TabPages.Remove(tabPage8);
                // tabControl1.TabPages.Remove(tabPage9);
            }
            else
            {
                //nrusers = users() - 1;
                LoadSchedule();
                LoadGroceries();
                updatemoneylabel();
                LoadParty();
                LoadReport();
                LoadHomePage();
                MessageBox.Show(appDate);
                tabControl1.TabPages.Remove(tabPage1);
                label17.Text = $"Welcome, {User}";
            }

        }
        public bool DateCompare(string sysdate, string databasedate)
        {


            string[] words = sysdate.Split('/');
            int i = 1;
            string d1 = "", d2 = "", m1 = "", m2 = "", y1 = "", y2 = "";
            foreach (var word in words)
            {
                if (i == 1)
                {
                    d1 = word;
                }
                else if (i == 2)
                {
                    m1 = word;
                }
                else if (i == 3)
                {
                    y1 = word;
                }
                i++;
            }
            i = 1;
            string[] words1 = databasedate.Split('/');
            foreach (var word in words1)
            {
                if (i == 1)
                {
                    d2 = word;
                }
                else if (i == 2)
                {
                    m2 = word;
                }
                else if (i == 3)
                {
                    y2 = word;
                }
                i++;
            }
            if (Convert.ToInt32(y2) > Convert.ToInt32(y1))
            { return true; }
            else
            {
                if (Convert.ToInt32(y2) >= Convert.ToInt32(y1) && Convert.ToInt32(m2) > Convert.ToInt32(m1))
                { return true; }
                else
                {
                    if (Convert.ToInt32(y2) >= Convert.ToInt32(y1) && Convert.ToInt32(m2) >= Convert.ToInt32(m1) && Convert.ToInt32(d2) >= Convert.ToInt32(d1))
                        return true;
                }
            }



            return false;
        } // WORKS FINE
        public void updatemoneylabel()
        {
           
        }
     
        public void LoadSchedule() // DELETE THE MESSAGEBOX
        {
            lbGarbage.Items.Clear();
            lbCleaning.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Schedule where username='{User}'";
                // MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    if (reader["Typeof"].ToString() == "Garbage")
                    {
                        if (DateCompare(appDate, reader["Dateof"].ToString()))
                            lbGarbage.Items.Add("You're on for: " + reader["Dateof"]);

                    }
                    if (reader["Typeof"].ToString() == "Cleaning")
                    {
                        if (DateCompare(appDate, reader["Dateof"].ToString()))
                            lbCleaning.Items.Add("You're on for: " + reader["Dateof"]);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }
     
        public void LoadGroceries() // DELETE MESSAGEBOX
        {
   
        }

        public void LoadParty() // DELETE THE MESSAGEBOX
        {
       
        }

        public void LoadReport() // DELETE THE MESSAGEBOX
        {
       
        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string username = txt_fname.Text + txt_room.Text;
                command.CommandText = $"insert into Users (Username, Pass, Balance, FirstName, LastName, Room) values ('{username}', '{txt_Password.Text}', '0', '{txt_fname.Text}', '{txt_lname.Text}', '{txt_room.Text}')";
                //command.CommandText = $"insert into Users (FirstName,LastName,Pay) values('{txt_fname.Text.}','{txt_lname.Text}','{Convert.ToInt32(txt_pay.Text)}')";
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"update Users set Pass ='{txt_Password.Text}', FirstName='{txt_fname.Text}', LastName='{txt_lname.Text}' where Room='{txt_room.Text}'";
                //string query = $"update Users set FirstName='{txt_fname.Text}', LastName='{txt_lname.Text}', Pay ='{txt_pay.Text}' where ID={txt_id.Text}  ";

                MessageBox.Show(query);
                command.CommandText = query;
                command.ExecuteNonQuery();
                MessageBox.Show("Data Edit Successful");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void btnAddParty_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
  
        }// works

        private void btnYes_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
           
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Reports Where Title='{cbReport.SelectedItem}'";
                MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    MessageBox.Show("The report was made on " + reader["Dateissued"] + " by the user " + reader["Username"] + " with the following description: " + reader["Description"]);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
            LoadReport();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            appDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            LoadSchedule();
            LoadHomePage();
        }

        private void btnGroceryPay_Click(object sender, EventArgs e)
        {
           
               
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           // spArduino.Open();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void btnGAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
           // spArduino.Close();
        }

       

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }

        private void btnScheduleAdd_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string susername = "";

            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Users where Room='{tbScheduleRoom.Text}'";
                // MessageBox.Show(query);
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    susername = reader["Username"].ToString();
                }
                // MessageBox.Show("Data Edit Successful");
                connection.Close();
            }
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"insert into Schedule (TypeOf,Room,username,Dateof) values('{cbSchedule.SelectedItem}','{tbScheduleRoom.Text}','{susername}','{date}')";
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                connection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Users WHERE Room='{txt_room.Text}';";
            command.ExecuteNonQuery();
            MessageBox.Show("Data Saved");
            connection.Close();
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
           
        }
        public void LoadHomePage()
        {
            lbHomepage.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Party";
                // MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bool compare = DateCompare(appDate, reader["Dateof"].ToString());
                    if (Convert.ToInt32(reader["YesVotes"]) >= nrusers / 2)
                    {
                        if (compare)
                            lbHomepage.Items.Add(reader["PartyName"] + " is happening as it got enough votes.Check it out in the parties tab.");

                    }
                    else
                    {
                        if (compare)
                            lbHomepage.Items.Add(reader["PartyName"] + " doesnt have enough votes go vote for it");
                    }

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }

        private void tbAddBalance_Click(object sender, EventArgs e)
        {
        }

        private void lblGroceryMoney_Click(object sender, EventArgs e)
        {

        }

        private void btnSchedu_Click(object sender, EventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
           
        }
    }
}
    
    
    


