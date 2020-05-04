using Microsoft.VisualBasic.CompilerServices;
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
    public partial class PlanificationArticlesKnco : Form
    {
        public PlanificationArticlesKnco()
        {
            InitializeComponent();
        }

        private void PlanificationArticlesKnco_Load(object sender, EventArgs e)
        {
            foreach (string item in Parametres.list)
            {
                comboBox1.Items.Add(item);

            }

            try
            {
                
                comboBox1.SelectedItem = Parametres.list.First();
                if (Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","intervalArticleKnco", "", null)) != null)
                {
                    label6.Text = "Le " + Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","dayArticleKncos", "", null)) + "/" + Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","monthArticleKncos", "", null)) + "/" + Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","yearArticleKncos", "", null)) + " a " + Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","hourArticleKncos", "", null)) + ":" + Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","minArticleKncos", "", null));
                    textBox1.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","intervalArticleKnco", "", null));
                    dataGridView1.Rows.Add(Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","debutArticleKnco", "", null)), Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","finArticleKnco", "", null)));
                    comboBox1.SelectedItem = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","intervalMinOrHourArticleKnco", "", null));
                    if ("MON" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","LundiArticleKnco", "", null)))
                    {

                        checkBox1.Checked = true;
                    }
                    if ("TUE" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","MardiArticleKnco", "", null)))
                    {
                        checkBox2.Checked = true;

                    }
                    if ("WED" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","MecrediArticleKnco", "", null)))
                    {
                        checkBox3.Checked = true;

                    }
                    if ("THU" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","JeudiArticleKnco", "", null)))
                    {
                        checkBox4.Checked = true;

                    }
                    if ("FRI" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","VendrediArticleKnco", "", null)))
                    {
                        checkBox5.Checked = true;

                    }
                    if ("SAT" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","SamediArticleKnco", "", null)))
                    {
                        checkBox6.Checked = true;

                    }
                    if ("SUN" == Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config","DimancheArticleKnco", "", null)))
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Parametres.jours.Clear();
                TimeSpan tstar = TimeSpan.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                TimeSpan tend = TimeSpan.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                if (tstar.CompareTo(tend) == -1)
                {

                    if (Parametres.key.GetValue("intervalArticleKnco") != null)
                    {

                        Parametres.deleteKey(Parametres.ValueKeyArticleKnco);
                        Parametres.ValueKeyArticleKnco.Clear();
                    }



                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            Objets.LocalSaveSetting("B1-Prestashop", "Config","intervalArticleKnco", textBox1.Text.ToString());
                            Objets.LocalSaveSetting("B1-Prestashop", "Config","intervalMinOrHourArticleKnco", comboBox1.SelectedItem);
                            Objets.LocalSaveSetting("B1-Prestashop", "Config","debutArticleKnco", dataGridView1.Rows[0].Cells[0].Value.ToString());
                            Objets.LocalSaveSetting("B1-Prestashop", "Config","finArticleKnco", dataGridView1.Rows[0].Cells[1].Value.ToString());
                            Parametres.ValueKeyArticleKnco.Add("intervalArticleKnco");
                            Parametres.ValueKeyArticleKnco.Add("intervalMinOrHourArticleKnco");
                            Parametres.ValueKeyArticleKnco.Add("debutArticleKnco");
                            Parametres.ValueKeyArticleKnco.Add("finArticleKnco");
                            if (checkBox1.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "LundiArticleKnco", variable = "MON" });

                            }
                            if (checkBox2.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MardiArticleKnco", variable = "TUE" });

                            }
                            if (checkBox3.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "MecrediArticleKnco", variable = "WED" });
                            }
                            if (checkBox4.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "JeudiArticleKnco", variable = "THU" });
                            }
                            if (checkBox5.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "VendrediArticleKnco", variable = "FRI" });
                            }
                            if (checkBox6.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "SamediArticleKnco", variable = "SAT" });
                            }
                            if (checkBox7.Checked)
                            {
                                Parametres.jours.Add(new jours() { Name = "DimancheArticleKnco", variable = "SUN" });
                            }
                            foreach (jours jour in Parametres.jours)
                            {
                                Objets.LocalSaveSetting("B1-Prestashop", "Config",jour.Name, jour.variable);
                                Parametres.ValueKeyArticleKnco.Add(jour.Name);

                            }

                            Scheduler sc = new Scheduler();
                            int StratM = tstar.Minutes;
                            int StartH = tstar.Hours;
                            int EndM = tend.Minutes;
                            int EndH = tend.Hours;

                            sc.Start(StartH, StratM, EndH, EndM, int.Parse(textBox1.Text), new JobArticlesKnco(), Parametres.jours, comboBox1.SelectedItem.ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
