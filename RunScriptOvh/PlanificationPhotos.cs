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
    public partial class PlanificationPhotos : Form
    {
        public PlanificationPhotos()
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

                    if (Parametres.key.GetValue("intervalPhotos") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyPhotos);
                        Parametres.ValueKeyPhotos.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalPhotos", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourPhotos", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutPhotos", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finPhotos", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyPhotos.Add("intervalPhotos");
                            Parametres.ValueKeyPhotos.Add("intervalMinOrHourPhotos");
                            Parametres.ValueKeyPhotos.Add("debutPhotos");
                            Parametres.ValueKeyPhotos.Add("finPhotos");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiPhotos", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiPhotos", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediPhotos", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiPhotos", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediPhotos", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediPhotos", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimanchePhotos", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyPhotos.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobPhotos(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationPhotos_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalPhotos") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("dayphotos") + "/" + (string)Parametres.key.GetValue("monthphotos") + "/" + (string)Parametres.key.GetValue("yearphotos") + " a " + (string)Parametres.key.GetValue("hourphotos") + ":" + (string)Parametres.key.GetValue("minphotos");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalPhotos");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutPhotos"), Parametres.key.GetValue("finPhotos"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourPhotos");
                    if ("MON" == (string)Parametres.key.GetValue("LundiPhotos"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiPhotos"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediPhotos"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiPhotos"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediPhotos"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediPhotos"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimanchePhotos"))
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
