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
        public PlanificationArticles()
        {
            InitializeComponent();    
        }
          


        private void planification_Load(object sender, EventArgs e)
        {
            
           foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);
          
            }
            
            try
            {
                comboBox1.SelectedItem = Parametres.list.First();
                if (Parametres.key.GetValue("intervalArticle") != null)
                {
                    label6.Text = "Le "+ (string)Parametres.key.GetValue("dayarticles") + "/"+ (string)Parametres.key.GetValue("montharticles") + "/"+ (string)Parametres.key.GetValue("yeararticles") + " a "+ (string)Parametres.key.GetValue("hourarticles") + ":"+ (string)Parametres.key.GetValue("minarticles");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalArticle");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutArticle"), Parametres.key.GetValue("finArticle"));
                    comboBox1.SelectedItem= (string)Parametres.key.GetValue("intervalMinOrHourArticle");
                    if ("MON" == (string)Parametres.key.GetValue("LundiArticle"))
                    {
                        
                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiArticle"))
                    {
                        checkBox2.Checked = true;
                       
                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediArticle"))
                    {
                        checkBox3.Checked = true;
                        
                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiArticle"))
                    {
                        checkBox4.Checked = true;
                        
                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediArticle"))
                    {
                        checkBox5.Checked = true;
                        
                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediArticle"))
                    {
                        checkBox6.Checked = true;
                        
                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimancheArticle"))
                    {
                        checkBox7.Checked = true;
                        
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
     
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Parametres.jours.Clear();
                TimeSpan tstar = TimeSpan.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                TimeSpan tend = TimeSpan.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                if (tstar.CompareTo(tend)==-1)
                {
                    
                        if (Parametres.key.GetValue("intervalArticle") != null)
                        {

                            Parametres.deleteKey(Parametres.ValueKeyArticle);
                            Parametres.ValueKeyArticle.Clear();
                    }
                    
                    
                    
                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalArticle", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourArticle", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutArticle", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finArticle", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyArticle.Add("intervalArticle");
                            Parametres.ValueKeyArticle.Add("intervalMinOrHourArticle");
                            Parametres.ValueKeyArticle.Add("debutArticle");
                            Parametres.ValueKeyArticle.Add("finArticle");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiArticle",  variable = "MON" });
                               
                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiArticle",  variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediArticle",  variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiArticle",  variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediArticle",  variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediArticle",  variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheArticle",  variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyArticle.Add(jour.Name);

                            }
                            
                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobArticles(), Parametres.jours, comboBox1.SelectedItem.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Merci de Sélectionnez votre champ interval !!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Merci de remplir votre champ interval !!");
                        textBox1.Focus();
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("Vérifier votre date !!");
                }
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
