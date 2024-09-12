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
    public partial class PlanificationAttributsCaracteristiques : Form
    {
        public PlanificationAttributsCaracteristiques()
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

                    if (Parametres.key.GetValue("intervalAttributsCaracteristiques") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyAttributsCaracteristiques);
                        Parametres.ValueKeyAttributsCaracteristiques.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalAttributsCaracteristiques", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourAttributsCaracteristiques", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutAttributsCaracteristiques", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finAttributsCaracteristiques", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyAttributsCaracteristiques.Add("intervalAttributsCaracteristiques");
                            Parametres.ValueKeyAttributsCaracteristiques.Add("intervalMinOrHourAttributsCaracteristiques");
                            Parametres.ValueKeyAttributsCaracteristiques.Add("debutAttributsCaracteristiques");
                            Parametres.ValueKeyAttributsCaracteristiques.Add("finAttributsCaracteristiques");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiAttributsCaracteristiques", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiAttributsCaracteristiques", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediAttributsCaracteristiques", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiAttributsCaracteristiques", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediAttributsCaracteristiques", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediAttributsCaracteristiques", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheAttributsCaracteristiques", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyAttributsCaracteristiques.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobAttributsCaracteristiques(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationAttributsCaracteristiques_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalAttributsCaracteristiques") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("dayattributs") + "/" + (string)Parametres.key.GetValue("monthattributs") + "/" + (string)Parametres.key.GetValue("yearattributs") + " a " + (string)Parametres.key.GetValue("hourattributs") + ":" + (string)Parametres.key.GetValue("minattributs");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalAttributsCaracteristiques");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutAttributsCaracteristiques"), Parametres.key.GetValue("finAttributsCaracteristiques"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourAttributsCaracteristiques");
                    if ("MON" == (string)Parametres.key.GetValue("LundiAttributsCaracteristiques"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiAttributsCaracteristiques"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediAttributsCaracteristiques"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiAttributsCaracteristiques"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediAttributsCaracteristiques"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediAttributsCaracteristiques"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimancheAttributsCaracteristiques"))
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
