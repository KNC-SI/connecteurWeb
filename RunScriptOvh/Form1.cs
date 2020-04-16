using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunScriptOvh
{
    public partial class Form1 : Form
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
            this.Opacity = 100;
            this.Show();
            this.SendToBack();
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new PlanificationAttributsCaracteristiques().Show();
             
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            new PlanificationGammes().Show();
             
        }
       
        private void button10_Click_1(object sender, EventArgs e)
        {
            new PlanificationClients().Show();
             
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            new PlanificationArticles().Show();
             
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            new PlanificationStock().Show();
             
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
             new PlanificationPrix().Show();
             
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            new PlanificationCopiePhotos().Show();
             
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            new PlanificationPhotos().Show();
             
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            new PlanificationNettoyagePhotos().Show();
             
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            new PlanificationCommande().Show();
             
        }

        private void button23_Click(object sender, EventArgs e)
        {
            
                string cron = "attributs";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("AttributsCaracteristiques");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string cron = "models";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("Gamme");
        }
       

        private void button20_Click(object sender, EventArgs e)
        {
            string cron = "articles";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("Articles");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string cron = "stock";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("Stock");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string cron = "prix";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("Prix");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            
                string cron = "copytoftp";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("CopiePhotos");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string cron = "clean";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("NettoyagePhotos");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string cron = "photos";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution("Photos");
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
