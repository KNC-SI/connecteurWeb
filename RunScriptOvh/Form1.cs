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

            //Parametres.startservice();
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            
            

        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
                Parametres.startservice("formload");
                if ("1"==((string)Parametres.key.GetValue("active")))
                {
                    pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Go_icon_V;
                button26.Enabled = false;
                button25.Enabled = true;
            }
                else
                {
                    pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Shutdown_icon_R;
                    button25.Enabled = false;
                    button26.Enabled = true;
            }
            

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
            Parametres.DerniereExecution(cron);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string cron = "models";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }
       

        private void button20_Click(object sender, EventArgs e)
        {
            string cron = "articles";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string cron = "stock";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string cron = "prix";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            
                string cron = "copytoftp";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string cron = "clean";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string cron = "photos";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
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

        private void button24_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox3.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked )
                    {
                        if (cb.Name!= "checkBox11")
                        {
                            Parametres.RunCommand(cb.Name);
                            Parametres.DerniereExecution(cb.Name);
                        }
                    }
                    
                }
            }
        }
        private void checkBox11_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox3.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == false)
                    {
                        cb.Checked = true;
                        checkBox11.Checked = true;
                        

                    }
                    else
                    {
                        cb.Checked = false;
                        checkBox11.Checked = false;

                    }
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Parametres.startservice("");
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Go_icon_V;
                button26.Enabled = false;
                button25.Enabled = true;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Shutdown_icon_R;
                button25.Enabled = false;
                button26.Enabled = true;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Parametres.startservice("");
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Go_icon_V;
                button26.Enabled = false;
                button25.Enabled = true;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Shutdown_icon_R;
                button25.Enabled = false;
                button26.Enabled = true;
            }
        }
    }
}
