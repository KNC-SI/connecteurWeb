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
        /*public void RunCommand(string NameCron)
        {
            using (var client = new SshClient("ssh.cluster006.hosting.ovh.net", "francoiszi", "Vhs67hjYa8om"))
            {
                client.Connect();
                client.RunCommand("/usr/local/php7.2/bin/php /homez.727/francoiszi/dev/connect/symfony/bin/console app:cron:" + NameCron);

                client.Disconnect();
            }
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            
            // For Interval in Seconds 
            // This Scheduler will start at 11:10 and call after every 15 Seconds
            // IntervalInSeconds(start_hour, start_minute, seconds)
            /*MyScheduler.IntervalInSeconds(12, 31, 60,
            () => {
                Console.WriteLine("//here write the code that you want to schedule 15 Seconds");
            });*/
            // For Interval in Minutes 
            // This Scheduler will start at 22:00 and call after every 30 Minutes
            // IntervalInSeconds(start_hour, start_minute, minutes)
            /*MyScheduler.IntervalInMinutes(22, 00, 30,
            () => {
                Console.WriteLine("//here write the code that you want to schedule 30 Minutes");
            });*/
            // For Interval in Hours 
            // This Scheduler will start at 9:44 and call after every 1 Hour
            // IntervalInSeconds(start_hour, start_minute, hours)
            /*MyScheduler.IntervalInHours(9, 44, 1,
            () => {
                Console.WriteLine("//here write the code that you want to schedule 1 Hour");
            });*/
            // For Interval in Seconds 
            // This Scheduler will start at 17:22 and call after every 3 Days
            // IntervalInSeconds(start_hour, start_minute, days)
           // DateTime date1 = new DateTime(2020, 4, 4);
            /*DayOfWeek day = DateTime.Now.DayOfWeek;
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                Console.WriteLine("This is a weekend :" + day);
                MyScheduler.IntervalInDays(17, 22, 3,
           () => {
               Console.WriteLine("//here write the code that you want to schedule 3 Days");
           });
            }
            else
            {
                Console.WriteLine("This is not a weekend :" + day);
            }
           
            Console.ReadLine();

            /*using (var client = new SshClient("ssh.cluster006.hosting.ovh.net", "francoiszi", "Vhs67hjYa8om"))
            {
                client.Connect();
                client.RunCommand("/usr/local/php7.2/bin/php /homez.727/francoiszi/dev/connect/symfony/bin/console app:cron:prix");

                client.Disconnect();
            }*/

        }
       

        private void Form1_Load(object sender, EventArgs e)
        {

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
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string cron = "models";
            Parametres.RunCommand(cron);
        }
       

        private void button20_Click(object sender, EventArgs e)
        {
            string cron = "articles";
            Parametres.RunCommand(cron);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string cron = "stock";
            Parametres.RunCommand(cron);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string cron = "prix";
            Parametres.RunCommand(cron);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            
                string cron = "copytoftp";
            Parametres.RunCommand(cron);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string cron = "clean";
            Parametres.RunCommand(cron);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string cron = "photos";
            Parametres.RunCommand(cron);
        }

       
    }
}
