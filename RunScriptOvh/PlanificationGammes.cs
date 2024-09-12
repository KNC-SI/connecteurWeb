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
    public partial class PlanificationGammes : Form
    {
        public PlanificationGammes()
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

                    if (Parametres.key.GetValue("intervalGamme") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyGamme);
                        Parametres.ValueKeyGamme.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalGamme", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourGamme", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutGamme", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finGamme", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyGamme.Add("intervalGamme");
                            Parametres.ValueKeyGamme.Add("intervalMinOrHourGamme");
                            Parametres.ValueKeyGamme.Add("debutGamme");
                            Parametres.ValueKeyGamme.Add("finGamme");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiGamme", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiGamme", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediGamme", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiGamme", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediGamme", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediGamme", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheGamme", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyGamme.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobGamme(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationGammes_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalGamme") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("daymodels") + "/" + (string)Parametres.key.GetValue("monthmodels") + "/" + (string)Parametres.key.GetValue("yearmodels") + " a " + (string)Parametres.key.GetValue("hourmodels") + ":" + (string)Parametres.key.GetValue("minmodels");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalGamme");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutGamme"), Parametres.key.GetValue("finGamme"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourGamme");
                    if ("MON" == (string)Parametres.key.GetValue("LundiGamme"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiGamme"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediGamme"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiGamme"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediGamme"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediGamme"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimancheGamme"))
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
