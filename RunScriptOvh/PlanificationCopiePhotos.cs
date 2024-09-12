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
    public partial class PlanificationCopiePhotos : Form
    {
        public PlanificationCopiePhotos()
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

                    if (Parametres.key.GetValue("intervalCopiePhotos") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyCopiePhotos);
                        Parametres.ValueKeyCopiePhotos.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalCopiePhotos", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourCopiePhotos", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutCopiePhotos", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finCopiePhotos", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyCopiePhotos.Add("intervalCopiePhotos");
                            Parametres.ValueKeyCopiePhotos.Add("intervalMinOrHourCopiePhotos");
                            Parametres.ValueKeyCopiePhotos.Add("debutCopiePhotos");
                            Parametres.ValueKeyCopiePhotos.Add("finCopiePhotos");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiCopiePhotos", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiCopiePhotos", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediCopiePhotos", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiCopiePhotos", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediCopiePhotos", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediCopiePhotos", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheCopiePhotos", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyCopiePhotos.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobCopiePhotos(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationCopiePhotos_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalCopiePhotos") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("daycopytoftp") + "/" + (string)Parametres.key.GetValue("monthcopytoftp") + "/" + (string)Parametres.key.GetValue("yearcopytoftp") + " a " + (string)Parametres.key.GetValue("hourcopytoftp") + ":" + (string)Parametres.key.GetValue("mincopytoftp");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalCopiePhotos");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutCopiePhotos"), Parametres.key.GetValue("finCopiePhotos"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourCopiePhotos");
                    if ("MON" == (string)Parametres.key.GetValue("LundiCopiePhotos"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiCopiePhotos"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediCopiePhotos"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiCopiePhotos"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediCopiePhotos"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediCopiePhotos"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimancheCopiePhotos"))
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
