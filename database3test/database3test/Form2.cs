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
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Joro\Desktop\users1.accdb;Persist Security Info=False;";
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
                LoadReport();
            }
            else
            {
                nrusers = users()-1;
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
        public void updatemoneylabel()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Users where Username='{User}'";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lblMyBalance.Text = reader["Balance"].ToString() + "$";
                    lblGroceryMoney.Text = reader["Balance"].ToString()+"$";
                    usermoney = Convert.ToDouble(reader["Balance"]);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }
        public int users()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = $"select * from Users";
            command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();
            int togive = 0;
            while (reader.Read())
            {
                togive++;
            }
            connection.Close();
            return togive;
            
        }// WORKS
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
                        if(DateCompare(appDate,reader["Dateof"].ToString()))
                        lbGarbage.Items.Add("You're on for: "+reader["Dateof"]); 

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
        public bool DateCompare(string sysdate, string databasedate)
        {
            
           
                string[] words = sysdate.Split('/');
                int i = 1;
                string d1="", d2="", m1="", m2="", y1="", y2="";
                foreach (var word in words)
                {
                    if (i==1)
                    {
                        d1 = word;
                    }else if(i==2)
                    {
                        m1 = word;
                    }else if( i==3)
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
        
        public void LoadGroceries() // DELETE MESSAGEBOX
        {
            lbGroceries.Items.Clear();
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Groceries";
                //MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["PaidFor"].ToString() == "NO")
                    {
                        double quantity = Convert.ToDouble(reader["Quantity"]);
                        double price = Convert.ToDouble(reader["Price"]);
                        double yourshare = price * quantity / nrusers;
                        string add = yourshare.ToString("0.00");
                        string toadd = reader["Quantity"].ToString() + " " + reader["Item"].ToString() + " at price: " + reader["Price"].ToString() + $", your share: { add}";
                        lbGroceries.Items.Add(toadd);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }

        public void LoadParty() // DELETE THE MESSAGEBOX
        {
            cbParty.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Party";
                MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (DateCompare(appDate, reader["Dateof"].ToString()))
                        cbParty.Items.Add(reader["PartyName"]);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }

        public void LoadReport() // DELETE THE MESSAGEBOX
        {
            cbReport.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Reports";
                MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    cbReport.Items.Add(reader["Title"]);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
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
            string dateparty = dtpParty.Value.ToString("dd/MM/yyyy");
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"insert into Party (PartyName,Description,YesVotes,NoVotes,WhoVoted,Dateof) values('{tbPartyName.Text}','{rtbDescParty.Text}','1','0','{User}','{dateparty}')";
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
            LoadParty();
            LoadHomePage();
            tbPartyName.Text = "";
            rtbDescParty.Text = "";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Party WHERE PartyName='{cbParty.SelectedItem}'";
                MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    MessageBox.Show(reader["Description"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
        }// works

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Party WHERE PartyName='{cbParty.SelectedItem}'";
                MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                string tosplit = "";
                string votes = "";
                while (reader.Read())
                {
                    tosplit= reader["WhoVoted"].ToString();
                    votes = reader["YesVotes"].ToString();
                }
                string tosave = tosplit+$" {User}";// updating the who voted list
                // see who voted
                bool alreadyvoted = false;
                string[] words=tosplit.Split(' ');
                foreach(var word in words)
                {
                    if (word == User)
                    {
                        alreadyvoted = true;
                    }
                }
                connection.Close();
                // count the  votes
               
                votes = (Convert.ToInt32(votes) + 1).ToString();
                if (alreadyvoted == true)
                    MessageBox.Show("You have already voted on this party");
                else
                {
                    connection.Open();
                   OleDbCommand  work = new OleDbCommand();
                    work.Connection = connection;
                     query = $"update Party set WhoVoted='{tosave}', YesVotes='{votes}' where PartyName='{cbParty.SelectedItem.ToString()}'";
                    MessageBox.Show(query);
                    work.CommandText = query;
                    work.ExecuteNonQuery();
                    MessageBox.Show("Data Edit Successful");
                    connection.Close();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
            LoadHomePage();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string date = System.DateTime.Now.ToString();
                command.CommandText = $"insert into Reports (Title,Username,Description,Dateissued) values('{tbReport.Text}','{User}','{rtbReport.Text}','{date}')";
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
            LoadReport();
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

                    MessageBox.Show("The report was made on "+reader["Dateissued"]+" by the user "+reader["Username"]+" with the following description: " +reader["Description"]);
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
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"select * from Party WHERE PartyName='{cbParty.SelectedItem}'";
                MessageBox.Show(query); // TO DELETE
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                string tosplit = "";
                string votes = "";
                while (reader.Read())
                {
                    tosplit = reader["WhoVoted"].ToString();
                    votes = reader["NoVotes"].ToString();
                }
                string tosave = tosplit + $" {User}";// updating the who voted list
                // see who voted
                bool alreadyvoted = false;
                string[] words = tosplit.Split(' ');
                foreach (var word in words)
                {
                    if (word == User)
                    {
                        alreadyvoted = true;
                    }
                }
                connection.Close();
                // count the  votes

                votes = (Convert.ToInt32(votes) + 1).ToString();
                if (alreadyvoted == true)
                    MessageBox.Show("You have already voted on this party");
                else
                {
                    connection.Open();
                    OleDbCommand work = new OleDbCommand();
                    work.Connection = connection;
                    query = $"update Party set WhoVoted='{tosave}', NoVotes='{votes}' where PartyName='{cbParty.SelectedItem.ToString()}'";
                    MessageBox.Show(query);
                    work.CommandText = query;
                    work.ExecuteNonQuery();
                    MessageBox.Show("Data Edit Successful");
                    connection.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex}");
            }
            LoadHomePage();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            appDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            LoadSchedule();
            LoadHomePage();
            LoadParty();
        }

        private void btnGroceryPay_Click(object sender, EventArgs e)
        {
            //split the string
            List<string> payers = new List<string>();
            List<string> items=new List<string>();
            List<string> itemsprice = new List<string>();
            List<string> buyers = new List<string>();
            double topay = 0;
            string whopaid = "";
            int nr = 0;
            foreach (var item in lbGroceries.SelectedItems)
                {
                int i = 1;
                string[] words = item.ToString().Split(' ');
                foreach (var word in words)
                {
                  if(i==2)
                    {
                        items.Add(word);
                        
                    }else if(i==8)
                    {
                        topay= topay + Convert.ToDouble(word);
                        itemsprice.Add(word);
                    }
                    i++;
                }

            }
            if(topay>usermoney)
            {
                MessageBox.Show("Not enough money to pay for it all");
            }
            else
            {
                bool dontupdate = false;
                foreach (string item in items)
                {
                     whopaid = "";
                    {//pull the whopaid list
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = $"select * from Groceries where Item='{item}'";
                        // MessageBox.Show(query);
                        command.CommandText=query;
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            whopaid = reader["WhoPaid"].ToString();
                            payers.Add(whopaid);
                        }
                        // MessageBox.Show("Data Edit Successful");
                        connection.Close();
                    }
                    bool paidfor = false;
                     nr = 0;
                    string[] words = whopaid.Split(' ');
                    foreach (string word in words)
                    {
                        if (word == User)
                            paidfor = true;
                        if (nr == 0)
                            buyers.Add(word);
                        nr++;
                    }
                    
                    if (!paidfor)
                    {
                     
                    }
                    else
                    {
                        MessageBox.Show("You've already paid for one of these items");
                        dontupdate = true;
                        break;
                    }
                }
                if(!dontupdate)
                {// updating user's money
                    try
                    {
                        connection.Open();
                        usermoney = usermoney - topay;
                        OleDbCommand work = new OleDbCommand();
                        work.Connection = connection;
                        string query = $"update Users set Balance='{usermoney}' where Username='{User}'";
                         //MessageBox.Show(query);
                        work.CommandText = query;
                        work.ExecuteNonQuery();
                        // MessageBox.Show("Data Edit Successful");
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error, {ex}");
                    }
                    int i = 0;
                    foreach (string item in items)
                    {
                        connection.Open();
                        OleDbCommand work = new OleDbCommand();
                        work.Connection = connection;
                        whopaid = payers[i] + " " + User;
                        string query = "";
                        if (nr + 1 != nrusers)
                            query = $"update Groceries set WhoPaid='{whopaid}' where Item='{item}'";
                        else
                        {
                            query = $"update Groceries set WhoPaid='{whopaid}',PaidFor='YES' where Item='{item}'";
                            { // giving the buyer their money back
                                double moneytoadd=0;
                                
                                OleDbCommand buymoney = new OleDbCommand();
                                buymoney.Connection = connection;
                                string toaddmoney = $"select * from Users where Username='{buyers[i]}'";
                                // MessageBox.Show(query);
                                buymoney.CommandText = toaddmoney;
                                OleDbDataReader reader = buymoney.ExecuteReader();
                                while (reader.Read())
                                {
                                    moneytoadd = Convert.ToDouble(reader["Balance"]);
                                }
                                // MessageBox.Show("Data Edit Successful");
                                connection.Close();
                                connection.Open();

                                string toupdate= $"update Users set Balance='{moneytoadd+Convert.ToDouble(itemsprice[i])*(nrusers-1)}' where Username='{buyers[i]}'";
                                OleDbCommand buy = new OleDbCommand();
                                buy.Connection = connection;
                                MessageBox.Show(toupdate);
                                buy.CommandText = toupdate;
                                buy.ExecuteNonQuery();
                                connection.Close();
                                connection.Open();
                            }
                        }
                        MessageBox.Show(query);
                        work.CommandText = query;
                        work.ExecuteNonQuery();
                        // MessageBox.Show("Data Edit Successful");
                        connection.Close();
                        i++;
                    }
                }
                LoadGroceries();
                updatemoneylabel();

            }
            
                //count how many paid for it
               
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           spArduino.Open();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void btnGAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbGName.Text) && !string.IsNullOrEmpty(tbGPrice.Text) && !string.IsNullOrEmpty(tbGQuantity.Text))
            {
                double price = Convert.ToDouble(tbGPrice.Text);
                int quantity = Convert.ToInt32(tbGQuantity.Text);
                usermoney = usermoney  - (price * quantity) / nrusers;
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = $"insert into Groceries (Item,Price,Quantity,WhoPaid,PaidFor) values('{tbGName.Text}','{price.ToString("0.00")}','{quantity}','{User}','NO')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error, {ex}");
                }
                try
                {
                    connection.Open();
                    OleDbCommand work = new OleDbCommand();
                    work.Connection = connection;
                    string query = $"update Users set Balance='{usermoney}' where Username='{User}'";
                    MessageBox.Show(query);
                    work.CommandText = query;
                    work.ExecuteNonQuery();
                    // MessageBox.Show("Data Edit Successful");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error, {ex}");
                }

                LoadGroceries();
                updatemoneylabel();
            }
            else
            {
                MessageBox.Show("Enter an accepted value");
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
           spArduino.Close();
        }

       

        private void timer2_Tick(object sender, EventArgs e)
        {
            clock = DateTime.Now.ToString("HH:mm:ss");
            if (spArduino.BytesToRead > 4)
            {
                line = spArduino.ReadLine();
                line = line.Trim();
                double lightValue = Convert.ToDouble(line);
                if (lightValue > 500 && alarm == false)
                {
                    lbxOutput.Items.Add($"{clock} Bathroom is occupied");
                    alarm = true;
                }
                else if (alarm == true && lightValue < 500)
                {
                    lbxOutput.Items.Add($"{clock} Bathroom is free");
                    alarm = false;
                }
            }
            if (lbxOutput.Items.Count > 10)
            {
                lbxOutput.Items.RemoveAt(0);
            }
            if (alarm == true)
            {
                pbAlarm.BackColor = Color.Red;
            }
            else
            {
                pbAlarm.BackColor = Color.Green;
            }
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
            if (!string.IsNullOrWhiteSpace(tbAddBalance.Text))
            {
                usermoney = usermoney + Convert.ToDouble(tbAddBalance.Text);
                connection.Open();
                OleDbCommand work = new OleDbCommand();
                work.Connection = connection;
                string query = $"update Users set Balance='{usermoney}' where Username='{User}'";
                MessageBox.Show(query);
                work.CommandText = query;
                work.ExecuteNonQuery();
                // MessageBox.Show("Data Edit Successful");
                connection.Close();
                updatemoneylabel();
                tbAddBalance.Text = "Enter an amount";
                tbAddBalance.ForeColor = Color.Gray;
            }
            else
            {
                MessageBox.Show("Input a valid value.");
            }
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
            tbAddBalance.Text = "";
        }

        private void lblGroceryMoney_Click(object sender, EventArgs e)
        {

        }

        private void btnSchedu_Click(object sender, EventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = $"update Users set Pass ='{tbChangePassword.Text}' where Username='{User}'";
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

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void lblGroceryMoney_Click_1(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void rtbDescParty_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    
    
    


