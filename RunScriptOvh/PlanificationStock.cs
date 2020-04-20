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
    public partial class PlanificationStock : Form
    {
        public PlanificationStock()
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

                    if (Parametres.key.GetValue("intervalStock") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyStock);
                        Parametres.ValueKeyStock.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Parametres.key.SetValue("intervalStock", textBox1.Text.ToString());
                            Parametres.key.SetValue("intervalMinOrHourStock", comboBox1.SelectedItem);
                            Parametres.key.SetValue("debutStock", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Parametres.key.SetValue("finStock", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyStock.Add("intervalStock");
                            Parametres.ValueKeyStock.Add("intervalMinOrHourStock");
                            Parametres.ValueKeyStock.Add("debutStock");
                            Parametres.ValueKeyStock.Add("finStock");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiStock", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiStock", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediStock", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiStock", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediStock", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediStock", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheStock", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Parametres.key.SetValue(jour.Name, jour.variable);
                                Parametres.ValueKeyStock.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;
                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobStock(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void PlanificationStock_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                comboBox1.SelectedItem = "Minutes";
                if (Parametres.key.GetValue("intervalStock") != null)
                {
                    label6.Text = "Le " + (string)Parametres.key.GetValue("daystock") + "/" + (string)Parametres.key.GetValue("monthstock") + "/" + (string)Parametres.key.GetValue("yearstock") + " a " + (string)Parametres.key.GetValue("hourstock") + ":" + (string)Parametres.key.GetValue("minstock");
                    textBox1.Text = (string)Parametres.key.GetValue("intervalStock");
                    dataGridView1.Rows.Add(Parametres.key.GetValue("debutStock"), Parametres.key.GetValue("finStock"));
                    comboBox1.SelectedItem = (string)Parametres.key.GetValue("intervalMinOrHourStock");
                    if ("MON" == (string)Parametres.key.GetValue("LundiStock"))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == (string)Parametres.key.GetValue("MardiStock"))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == (string)Parametres.key.GetValue("MecrediStock"))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == (string)Parametres.key.GetValue("JeudiStock"))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == (string)Parametres.key.GetValue("VendrediStock"))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == (string)Parametres.key.GetValue("SamediStock"))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == (string)Parametres.key.GetValue("DimancheStock"))
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
