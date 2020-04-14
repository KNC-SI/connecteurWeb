using Microsoft.Win32;
using System;
using System.Collections;
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
    public partial class PlanificationArticles : Form
    {
        List<string> list = new List<string>() { "Secondes", "Minutes", "Heures" };
        List<jours> jours = new List<jours>();
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"C:\Users\choui\Desktop\test");

        public PlanificationArticles()
        {
            InitializeComponent();
        }
        

        private void planification_Load(object sender, EventArgs e)
        {
            foreach (string item in list)
            {
                comboBox1.Items.Add(item);
          
            }
            
            //textBox1.Text = key.GetValue("value").ToString();
            //comboBox1.SelectedValue = key.GetValue("value2");
            //MessageBox.Show(key.GetValue("Setting1").ToString());
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               


                //storing the values  
                key.SetValue("value", textBox1.Text.ToString());
                key.SetValue("value2", comboBox1.SelectedItem);
                /*int width = int.Parse(key.GetValue("value").ToString());
                int height = int.Parse(key.GetValue("value2").ToString());*/
               /* if (comboBox1.SelectedItem == "Secondes")
                {
                    MyScheduler.IntervalInSeconds(10, 19,13,44, int.Parse(textBox1.Text),
                           () => {
                               Console.WriteLine("//here write the code that you want to schedule 15 Seconds");
                               MessageBox.Show("//here write the code that you want to schedule 15 Seconds");
                           });
                }
                else if (comboBox1.SelectedItem == "Minutes")
                {
                    MyScheduler.IntervalInMinutes(12,20,16,20, int.Parse(textBox1.Text),
                        () => {
                            Console.WriteLine("//here write the code that you want to schedule 30 Minutes");
                            MessageBox.Show("//here write the code that you want to schedule 30 Minutes");
                        });
                }
                else
                {
                    MyScheduler.IntervalInHours(17, 31, 17,31, int.Parse(textBox1.Text),
                        () => {
                            Console.WriteLine("//here write the code that you want to schedule 1 Hour");
                        });
                }*/
                key.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("test " + ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // DateTime date = dateTimePicker1.Value;
            Scheduler sc = new Scheduler();
            //sc.Start(12, 20, 14, 42, int.Parse(textBox1.Text));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                //test
                jours.Clear();
                key.SetValue("value", textBox1.Text.ToString());
                key.SetValue("value2", comboBox1.SelectedItem);
                key.SetValue("value3", dataGridView1.Rows[0].Cells[0].Value.ToString());
                key.SetValue("value4", dataGridView1.Rows[0].Cells[1].Value.ToString());
                if (checkBox1.Checked)
                {
                    jours.Add(new jours() { Name = "LundiArticle", value = 1,variable= "MON" });
                }
                if (checkBox2.Checked)
                {
                    jours.Add(new jours() { Name = "MardiArticle", value = 1, variable = "TUE" });
                }
                if (checkBox3.Checked)
                {
                    jours.Add(new jours() { Name = "MecrediArticle", value = 1, variable = "WED" });
                }
                if (checkBox4.Checked)
                {
                    jours.Add(new jours() { Name = "JeudiArticle", value = 1, variable = "THU" });
                }
                if (checkBox5.Checked)
                {
                    jours.Add(new jours() { Name = "VendrediArticle", value = 1, variable = "FRI" });
                }
                if (checkBox6.Checked)
                {
                    jours.Add(new jours() { Name = "SamediArticle", value = 1, variable = "SAT" });
                }
                if (checkBox7.Checked)
                {
                    jours.Add(new jours() { Name = "DimancheArticle", value = 1, variable = "SUN" });
                }
                foreach (jours jour in jours)
                {
                    key.SetValue(jour.Name, jour);
                }
                MessageBox.Show(key.GetValue("value") + "" + key.GetValue("value2") + "" + key.GetValue("value3") + "" + key.GetValue("value4"));
                foreach (jours jour in jours)
                {
                    Console.WriteLine(key.GetValue(jour.Name));
                }
                key.Close();
                Scheduler sc = new Scheduler();
                TimeSpan tstar = TimeSpan.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                TimeSpan tend = TimeSpan.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                int StratM= tstar.Minutes;
                int StartH = tstar.Hours;
                int EndM = tend.Minutes;
                int EndH = tend.Hours;

                sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text),new Job(), jours,comboBox1.SelectedItem.ToString());
             


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
