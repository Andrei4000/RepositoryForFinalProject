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
           
            
            
        }
        public void updatemoneylabel()
        {
           
        }
     
        public void LoadSchedule() // DELETE THE MESSAGEBOX
        {
            
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
      
        }

        private void button2_Click(object sender, EventArgs e)
        {

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

           
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            
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
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
           
        }
        public void LoadHomePage()
        {
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
    
    
    


