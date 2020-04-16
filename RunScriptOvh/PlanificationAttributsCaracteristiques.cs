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
            Parametres.key.Close();
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
    }
}
