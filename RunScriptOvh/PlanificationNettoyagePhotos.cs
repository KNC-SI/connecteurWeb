﻿using System;
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
    public partial class PlanificationNettoyagePhotos : Form
    {
        public PlanificationNettoyagePhotos()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Parametres.jours.Clear();
                TimeSpan tstar = TimeSpan.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                TimeSpan tend = TimeSpan.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                if (tstar.CompareTo(tend) == -1)
                {

                    if (Parametres.key.GetValue("intervalNettoyagePhotos") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyNettoyagePhotos);
                        Parametres.ValueKeyNettoyagePhotos.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalNettoyagePhotos", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourNettoyagePhotos", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutNettoyagePhotos", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finNettoyagePhotos", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyNettoyagePhotos.Add("intervalNettoyagePhotos");
                            Parametres.ValueKeyNettoyagePhotos.Add("intervalMinOrHourNettoyagePhotos");
                            Parametres.ValueKeyNettoyagePhotos.Add("debutNettoyagePhotos");
                            Parametres.ValueKeyNettoyagePhotos.Add("finNettoyagePhotos");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiNettoyagePhotos", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiNettoyagePhotos", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediNettoyagePhotos", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiNettoyagePhotos", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediNettoyagePhotos", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediNettoyagePhotos", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheNettoyagePhotos", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyNettoyagePhotos.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobNettoyagePhotos(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationNettoyagePhotos_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalNettoyagePhotos") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("dayNettoyagePhotos") + "/" + (string)Parametres.key.GetValue("monthNettoyagePhotos") + "/" + (string)Parametres.key.GetValue("yearNettoyagePhotos") + " a " + (string)Parametres.key.GetValue("hourNettoyagePhotos") + ":" + (string)Parametres.key.GetValue("minNettoyagePhotos");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalNettoyagePhotos");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutNettoyagePhotos"), Parametres.key.GetValue("finNettoyagePhotos"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourNettoyagePhotos");
                    if ("MON" == (string)Parametres.key.GetValue("LundiNettoyagePhotos"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiNettoyagePhotos"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediNettoyagePhotos"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiNettoyagePhotos"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediNettoyagePhotos"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediNettoyagePhotos"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimancheNettoyagePhotos"))
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
    }
}
