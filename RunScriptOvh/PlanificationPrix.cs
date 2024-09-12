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
    public partial class PlanificationPrix : Form
    {
        public PlanificationPrix()
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

                    if (Parametres.key.GetValue("intervalPrix") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyPrix);
                        Parametres.ValueKeyPrix.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalPrix", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourPrix", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutPrix", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finPrix", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyPrix.Add("intervalPrix");
                            Parametres.ValueKeyPrix.Add("intervalMinOrHourPrix");
                            Parametres.ValueKeyPrix.Add("debutPrix");
                            Parametres.ValueKeyPrix.Add("finPrix");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiPrix", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiPrix", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediPrix", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiPrix", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediPrix", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediPrix", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimanchePrix", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyPrix.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobPrix(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationPrix_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalPrix") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("dayprix") + "/" + (string)Parametres.key.GetValue("monthprix") + "/" + (string)Parametres.key.GetValue("yearprix") + " a " + (string)Parametres.key.GetValue("hourprix") + ":" + (string)Parametres.key.GetValue("minprix");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalPrix");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutPrix"), Parametres.key.GetValue("finPrix"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourPrix");
                    if ("MON" == (string)Parametres.key.GetValue("LundiPrix"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiPrix"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediPrix"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiPrix"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediPrix"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediPrix"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimanchePrix"))
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
