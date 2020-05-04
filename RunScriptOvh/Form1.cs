using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAPbobsCOM;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.InteropServices;
using MG2S_SBOClass;
using static MG2S_GeneralServiceSettings.clsGeneralSetting;
using Utilities.FTP;

namespace RunScriptOvh
{
    public partial class Form1 : Form
    {
        B1Prestashop_Tools b1Prestashop_Tools = new B1Prestashop_Tools();

         clsGeneralSetting SAVSettings =new clsGeneralSetting();

        public bool NeedFtp;

        public bool NeedSMTP;

        public Form1()
        {
            InitializeComponent();
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

            //textBox19.Text, comboBox2.Text, textBox18.Text, textBox17.Text, comboBox3.Text, textBox16.Text, textBox15.Text, comboBox1.Text

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            textBox11.Text = (string)Parametres.key.GetValue("ServeurMySql" + textBox12.Text);
            textBox10.Text = (string)Parametres.key.GetValue("UserMySql" + textBox12.Text);
            textBox9.Text = (string)Parametres.key.GetValue("PassMySql" + textBox12.Text);

            //sap-sql
            textBox19.Text= Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SQLServer", "", null));
            comboBox2.SelectedItem = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "TypeServeur", "", null));
            textBox18.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SQLUser", "", null));
            textBox17.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SQLPassword", "", null));
            if (Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SQLAuthModeWindows", "", null))!="False")
            {
                checkBox15.Checked = true;
            }
            else
            {
                checkBox15.Checked = false;
            }
            comboBox3.SelectedItem = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SBODbCompany", "", null));
            comboBox1.SelectedItem = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "LicenceServer", "", null));
            textBox16.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SBOUser", "", null));
            textBox15.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "SBOPassword", "", null));

            //mysql
            textBox7.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "Databasetest", "", null));
            textBox8.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "ServeurMySqltest", "", null));
            textBox13.Text = Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "UserMySqltest", "", null));
            textBox14.Text = Objets.StrDecrypt(Conversions.ToString(Objets.GlobalGetSettingtest("B1-Prestashop", "Config", "PassMySqltest", "", null)), Conversions.ToString(b1Prestashop_Tools.cleCrypt), false);
            Parametres.startservice("formload");
                if ("1"==((string)Parametres.key.GetValue("active")))
                {
                    pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Go_icon_V;
                button26.Enabled = false;
                button25.Enabled = true;
            }
                else
                {
                    pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Shutdown_icon_R;
                    button25.Enabled = false;
                    button26.Enabled = true;
            }
            
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new PlanificationAttributsCaracteristiques().Show();
             
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            new PlanificationGammes().Show();
             
        }
       
        private void button10_Click_1(object sender, EventArgs e)
        {
            new PlanificationClients().Show();
             
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            new PlanificationArticles().Show();
             
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            new PlanificationStock().Show();
             
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
             new PlanificationPrix().Show();
             
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            new PlanificationCopiePhotos().Show();
             
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            new PlanificationPhotos().Show();
             
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            new PlanificationNettoyagePhotos().Show();
             
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            new PlanificationCommande().Show();
             
        }

        private void button23_Click(object sender, EventArgs e)
        {
            
                string cron = "attributs";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string cron = "models";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }
       

        private void button20_Click(object sender, EventArgs e)
        {
            string cron = "articles";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string cron = "stock";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string cron = "prix";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            
                string cron = "copytoftp";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string cron = "clean";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string cron = "photos";
            Parametres.RunCommand(cron);
            Parametres.DerniereExecution(cron);
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox3.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked )
                    {
                        if (cb.Name!= "checkBox11")
                        {
                            Parametres.RunCommand(cb.Name);
                            Parametres.DerniereExecution(cb.Name);
                        }
                    }
                    
                }
            }
        }
        private void checkBox11_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox3.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == false)
                    {
                        cb.Checked = true;
                        checkBox11.Checked = true;
                        

                    }
                    else
                    {
                        cb.Checked = false;
                        checkBox11.Checked = false;

                    }
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Parametres.startservice("");
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Go_icon_V;
                button26.Enabled = false;
                button25.Enabled = true;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Shutdown_icon_R;
                button25.Enabled = false;
                button26.Enabled = true;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Parametres.startservice("");
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Go_icon_V;
                button26.Enabled = false;
                button25.Enabled = true;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.Cute_Ball_Shutdown_icon_R;
                button25.Enabled = false;
                button26.Enabled = true;
            }
        }

       

        /*private void button28_Click(object sender, EventArgs e)
        {
            /*Parametres.key.SetValue("Database"+textBox12.Text, textBox12.Text);
             Parametres.key.SetValue("ServeurMySql" + textBox12.Text, textBox11.Text);
             Parametres.key.SetValue("UserMySql" + textBox12.Text, textBox10.Text);
             Parametres.key.SetValue("PassMySql" + textBox12.Text, textBox9.Text);
          
         int connectionResult = (int)Parametres.ConnectSqlSap(textBox4.Text, textBox5.Text, textBox6.Text, textBox8.Text, textBox7.Text);
            if (connectionResult == 0)
            {
                //MessageBox.Show("Connected");
                B1Prestashop_Tools prestashop_Tools=new B1Prestashop_Tools();
                prestashop_Tools.doSynchroArticles();
            }
            else
            {
                MessageBox.Show("Not connected");
            }    
        }*/

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
        

        private void button29_Click(object sender, EventArgs e)
        {
            Parametres.key.SetValue("Database", textBox12.Text);
            Parametres.key.SetValue("ServeurMySql" + textBox12.Text, textBox11.Text);
            Parametres.key.SetValue("UserMySql" + textBox12.Text, textBox10.Text);
            Parametres.key.SetValue("PassMySql" + textBox12.Text, textBox9.Text);
            Parametres.Openconnection(textBox11.Text, textBox10.Text, textBox9.Text, textBox12.Text);
            //Parametres.Closeconnection();

        }

        private void bTester_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int Connexion = Parametres.ConnectSqlSap(tSQLServer.Text, cbBases.Text, tSQLLogin.Text, tSQLPassword.Text, cbSQLType.Text, tSBOLogin.Text, tSBOPassword.Text, tLicenceServ.Text);
               
                if (Connexion != 0)
                {
                    Interaction.MsgBox("Connexion échouée : ", MsgBoxStyle.Exclamation, this.Text);
                }
                else
                {
                    Interaction.MsgBox("Connexion réussie.", MsgBoxStyle.Information, this.Text);
                }
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox(string.Concat("Exception lors du test : ", exception.Message, "\r\n", exception.StackTrace), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        private void cAuthWin_CheckedChanged(object sender, EventArgs e)
        {
            this.tSQLLogin.Enabled = !this.cAuthWin.Checked;
            this.tSQLPassword.Enabled = !this.cAuthWin.Checked;
        }

        private void bAjouter_Click(object sender, EventArgs e)
        {
            //DataBaseSettings setting;
            ////if (Operators.CompareString(this.tNom.Text, "", false) == 0)
            ////{
            ////    this.lSaveInfos.Text = "Veuillez renseigner un nom à votre connexion !";
            ////}
            ////else
            ////{
            //    clsGeneralSetting.DataBaseSettings item = null;
            //    int selectedIndex = lbCon.SelectedIndex;
            //    if (selectedIndex != -1)
            //    {
            //        item = (clsGeneralSetting.DataBaseSettings)this.lbCon.Items[selectedIndex];
            //    }
            //    bool flag = Operators.CompareString(this.bAjouter.Text, "Ajouter", false) == 0;
            //    if (item == null || flag)
            //    {
            //        item =setting.AddDB(this.tNom.Text);
            //    }
            //    clsGeneralSetting.DataBaseSettings text = item;
            //    text.name = this.tNom.Text;
            //    text.P_SERVEUR_Name = this.tSQLServer.Text;
            //    text.P_SERVEUR_Type = this.cbSQLType.Text;
            //    text.P_SERVEUR_AuthWin = this.cAuthWin.Checked;
            //    text.P_SERVEUR_Login = this.tSQLLogin.Text;
            //    text.P_SERVEUR_Pass = this.tSQLPassword.Text;
            //    text.P_SERVEUR_DB = this.cbBases.Text;
            //    text.P_SERVEUR_SAPUser = this.tSBOLogin.Text;
            //    text.P_SERVEUR_SAPPass = this.tSBOPassword.Text;
            //    //text.S_SERVEUR_Activated = this.cSecondaryActivated.Checked;
            //    //text.S_SERVEUR_Name = this.tSQLServer2.Text;
            //    //text.S_SERVEUR_Type = this.cbSQLType2.Text;
            //    //text.S_SERVEUR_AuthWin = this.cAuthWin2.Checked;
            //    //text.S_SERVEUR_Login = this.tSQLLogin2.Text;
            //    //text.S_SERVEUR_Pass = this.tSQLPassword2.Text;
            //    //text.S_SERVEUR_DB = this.cbBases2.Text;
            //    text.P_LicServ = this.tLicenceServ.Text;
            //    text = null;
            //    this.Clear();
            //    if (selectedIndex == -1 || flag)
            //    {
            //        selectedIndex = this.lbCon.Items.Add(item);
            //    }
            //    else
            //    {
            //        this.lbCon.Items.RemoveAt(selectedIndex);
            //        this.lbCon.Items.Insert(selectedIndex, item);
            //    }
            //    if (!flag)
            //    {
            //        this.lbCon.SelectedIndex = selectedIndex;
            //        this.lbCon_SelectedIndexChanged(null, null);
            //    }
            //    else
            //    {
            //        this.bNouveau_Click(null, null);
            //    }
            //    //this.lSaveInfos.Text = string.Concat("Connexion sauvegardée : ", item.name);
            //}
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //SAPbobsCOM.Company SBOComp =new SAPbobsCOM.Company ();
            // SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
            b1Prestashop_Tools.doSynchroArticles(false);
            //if (this.progressBar1 != null)
            //{
            //    this.progressBar1.Maximum = 2;
            //    this.progressBar1.Value = 0;
            //}
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox6.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == false)
                    {
                        cb.Checked = true;
                        checkBox1.Checked = true;


                    }
                    else
                    {
                        cb.Checked = false;
                        checkBox1.Checked = false;

                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox6.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked)
                    {
                            Console.WriteLine(cb.Name);
                            if (cb.Name=="articlesKnco")
                            {
                            b1Prestashop_Tools.doSynchroArticles(false);
                        }
                            if (cb.Name == "commandeKnco")
                            {

                            }
                            if (cb.Name == "clientsKnco")
                            {

                            }

                    }

                }
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "Databasetest", textBox7.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "ServeurMySqltest", textBox8.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "UserMySqltest", textBox13.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "PassMySqltest", Objets.StrCrypt(textBox14.Text, Conversions.ToString(b1Prestashop_Tools.cleCrypt)));
            bool Connexion = Parametres.Openconnection(textBox8.Text, textBox13.Text, textBox14.Text, textBox7.Text);
            if (!Connexion)
            {
                Interaction.MsgBox("Connexion échouée : ", MsgBoxStyle.Exclamation, this.Text);
            }
            else
            {
                Interaction.MsgBox("Connexion réussie.", MsgBoxStyle.Information, this.Text);
            }

        }

        private void button51_Click(object sender, EventArgs e)
        {
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SQLServer", textBox19.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "TypeServeur", comboBox2.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SQLUser", textBox18.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SQLPassword", textBox17.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SQLAuthModeWindows", checkBox15.Checked);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SBODbCompany", comboBox3.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "LicenceServer", comboBox1.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SBOUser", textBox16.Text);
            Objets.LocalSaveSetting("B1-Prestashop", "Config", "SBOPassword", textBox15.Text);
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int Connexion = Parametres.ConnectSqlSap(textBox19.Text, comboBox2.Text, textBox18.Text, textBox17.Text, comboBox3.Text, textBox16.Text, textBox15.Text, comboBox1.Text);

                if (Connexion != 0)
                {
                    Interaction.MsgBox("Connexion échouée : ", MsgBoxStyle.Exclamation, this.Text);
                }
                else
                {
                    Interaction.MsgBox("Connexion réussie.", MsgBoxStyle.Information, this.Text);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox(string.Concat("Exception lors du test : ", exception.Message, "\r\n", exception.StackTrace), MsgBoxStyle.OkOnly, null);
                ProjectData.ClearProjectError();
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox18.Enabled = !this.checkBox15.Checked;
            this.textBox17.Enabled = !this.checkBox15.Checked;
        }

        private void button48_Click(object sender, EventArgs e)
        {
            new PlanificationArticlesKnco().Show();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "Connexion : Ok";
                bool flag = false;
                FTPclient fTPclient = new FTPclient()
                {
                    Retry = 0
                };
                try
                {
                    fTPclient.Hostname = this.tFTPServ.Text;
                    fTPclient.Username = this.tFTPLog.Text;
                    fTPclient.Password = this.tFTPPass.Text;
                    fTPclient.UseSSL = this.cFTPSSL.Checked;
                    fTPclient.CurrentDirectory = this.tFTPRacine.Text;
                    fTPclient.ListDirectory("");
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception exception = exception1;
                    Interaction.MsgBox(string.Concat("Erreur lors de la connexion au serveur FTP, vérifier que vous avez accès au serveur FTP via un client FTP (FileZilla, InternetExplorer, ...)\r\n", exception.Message), MsgBoxStyle.Critical, null);
                    ProjectData.ClearProjectError();
                    return;
                }
                try
                {
                    fTPclient.ListDirectory("");
                    str = string.Concat(str, "\r\nListe des fichiers : OK");
                }
                catch (Exception exception2)
                {
                    ProjectData.SetProjectError(exception2);
                    str = string.Concat(str, "\r\nErreur lors de la liste des fichiers : ", exception2.Message);
                    flag = true;
                    ProjectData.ClearProjectError();
                }
                try
                {
                    fTPclient.FtpCreateDirectory("MG2S_FTP_CLIENT_TEST");
                    str = string.Concat(str, "\r\nCreation de répertoire : OK");
                }
                catch (Exception exception3)
                {
                    ProjectData.SetProjectError(exception3);
                    str = string.Concat(str, "\r\nErreur lors de la creation de répertoire : ", exception3.Message);
                    flag = true;
                    ProjectData.ClearProjectError();
                }
                try
                {
                    fTPclient.FtpDeleteDirectory("MG2S_FTP_CLIENT_TEST");
                    str = string.Concat(str, "\r\nSuppression de répertoire : OK");
                }
                catch (Exception exception4)
                {
                    ProjectData.SetProjectError(exception4);
                    str = string.Concat(str, "\r\nErreur lors de la suppression de répertoire : ", exception4.Message);
                    flag = true;
                    ProjectData.ClearProjectError();
                }
                if (!flag)
                {
                    Interaction.MsgBox("Connexion et tests de transfert réussi.", MsgBoxStyle.Information, this.Text);
                }
                else
                {
                    Interaction.MsgBox(str, MsgBoxStyle.Exclamation, this.Text);
                }
            }
            catch (Exception exception6)
            {
                ProjectData.SetProjectError(exception6);
                Exception exception5 = exception6;
                Interaction.MsgBox(exception5.Message, MsgBoxStyle.Critical, this.Text);
                ProjectData.ClearProjectError();
            }
        }


        //private void Clear()
        //{
        //    this.tNom.Enabled = true;
        //    this.tNom.Text = "";
        //    this.tSQLServer.Text = "";
        //    this.cAuthWin.Checked = true;
        //    this.cbSQLType.Text = "";
        //    this.tSQLLogin.Text = "";
        //    this.tSQLPassword.Text = "";
        //    this.cbBases.Text = "";
        //    this.tSBOLogin.Text = "";
        //    this.tSBOPassword.Text = "";
        //    //this.cSecondaryActivated.Checked = false;
        //    //this.tSQLServer2.Text = "";
        //    //this.cbSQLType2.Text = "";
        //    //this.cAuthWin2.Checked = true;
        //    //this.tSQLLogin2.Text = "";
        //    //this.tSQLPassword2.Text = "";
        //    //this.cbBases2.Text = "";
        //    this.bAjouter.Text = "Ajouter";
        //}


        //public void SynchroArticles()
        //{
        //    //string tableMySQLClients = GlobalGetSetting("B1-Prestashop", "Client", "TableMySQL", "customer", null).ToString();
        //    string tableMySQLArticles = Parametres.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLArticles", "product", null).ToString();
        //    //string tableMySQLFabricants = GlobalGetSetting("B1-Prestashop", "Fabricant", "TableMySQL", "manufacturer", null).ToString();
        //    //string tableMySQLLangues = GlobalGetSetting("B1-Prestashop", "Langue", "TableMySQL", "lang", null).ToString();
        //    string tableMySQLNomsArticles = Parametres.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLNoms", "product_lang", null).ToString();
        //    string tableMySQLImagesArticles = Parametres.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLImages", "product_image", null).ToString();
        //    string tableMySQLPrixSpecifiques = Parametres.GlobalGetSetting("B1-Prestashop", "PrixSpec", "TableMySQL", "specific_price", null).ToString();
        //    //string default_category = GlobalGetSetting("B1-Prestashop", "Article", "CategorieParDefaut", "", null).ToString();
        //    string strdelete="";
        //    DataSet dataSet = new DataSet();

        //Recordset businessObject = (Recordset)Parametres.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
        //    string str2 = "";
        //    try
        //    {

        //        str2 = "SELECT BitmapPath FROM OADP";
        //        //Console.WriteLine(str2);
        //        businessObject.DoQuery(str2);
        //        if (!businessObject.EoF)
        //        {

        //            str2 = "SELECT * FROM SAP_product p;";
        //            //Console.WriteLine(str2);
        //            string conn = "server=51.210.4.17;user=karationcom0;database=karavanproduction;port=3306;password=789ssszfeg568d;";
        //            MySqlConnection connection = new MySqlConnection(conn);
        //            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(str2, connection);
        //            mySqlDataAdapter.Fill(dataSet, "ArticlesBaseIntermédiaire");
        //            DataTable item = dataSet.Tables["ArticlesBaseIntermédiaire"];
        //            //item.CaseSensitive = false;

        //            long num = (long)0;
        //            long count = (long)(checked(item.Rows.Count - 1));
        //            for (long i = num; i <= count; i = checked(i + (long)1))
        //            {
        //                string str3 = (string)item.Rows[checked((int)i)]["id_product_extern"];
        //                //Console.WriteLine(str3);
        //                if (str3 != "")
        //                {
        //                    str2 = string.Concat("SELECT ItemCode FROM OITM art WHERE art.ItemCode = '",
        //                        str3, "' AND art.U_MG2S_SyncWeb = 'Y'");
        //                    businessObject.DoQuery(str2);

        //                    if (businessObject.RecordCount > 0)
        //                    {

        //                        Console.WriteLine(str2);
        //                        string str = "UPDATE  " + tableMySQLArticles + "  SET active = 0, state = 1 WHERE id_product ="
        //                            + RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]).ToString();
        //                        MySqlDataReader MyReader2;
        //                        MySqlCommand MyCommand2 = new MySqlCommand(str, connection);
        //                        connection.Open();
        //                        MyReader2 = MyCommand2.ExecuteReader();
        //                        Console.WriteLine(MyReader2);
        //                        //Parametres.PushMySqlQuery(string.Concat(str));
        //                        Console.WriteLine(str);
        //                        connection.Close();
        //                    }
        //                }
        //                else
        //                {
        //                    strdelete = "DELETE FROM " + tableMySQLImagesArticles + " WHERE id_product = " + RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]).ToString();
        //                    //Parametres.PushMySqlQuery(strdelete);

        //                    ////strdelete = new string[] { "DELETE FROM ", tableMySQLPrixSpecifiques, " WHERE id_product = ", RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]).ToString().Trim(), ";" };
        //                    //Parametres.PushMySqlQuery(string.Concat(strdelete));
        //                    ////strdelete = new string[] { "DELETE FROM ", tableMySQLCaracsArticles, " WHERE id_product = ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false), ";" };
        //                    ////this.PushMySqlQuery(string.Concat(str));
        //                    //strdelete = new string[] { "DELETE FROM ", tableMySQLNomsArticles, " WHERE id_product = ", RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]).ToString(), ";" };
        //                    //Parametres.PushMySqlQuery(string.Concat(strdelete));
        //                    //strdelete = new string[] { "DELETE FROM ", tableMySQLArticles, " WHERE id_product = ", RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]).ToString(), ";" };
        //                    //Parametres.PushMySqlQuery(string.Concat(strdelete));

        //                }

        //            }
        //            //Parametres.FlushMySqlQuery();
        //            //this.AddLog(string.Concat(this.SiteName, ": ", str1, ": Récupération des articles créés ou modifiés dans SBO depuis la dernière synchro"), 5);
        //            Console.WriteLine(Parametres.getCondPersoArticles());
        //            str2 = string.Concat("SELECT art.*, gart.ItmsGrpNam FROM OITM art LEFT JOIN OITB gart ON gart.ItmsGrpCod = art.ItmsGrpCod WHERE art.U_MG2S_Update = 'Y' AND art.SellItem = 'Y' AND art.U_MG2S_SyncWeb = 'Y' AND art.U_MG2S_Actif IS NOT NULL AND art.U_MG2S_ExcluWeb IS NOT NULL AND art.frozenFor = 'N' ", Parametres.getCondPersoArticles());
        //            //this.AddLog(string.Concat(str1, " : Execution DI de la requête -> '", str2, "'"), 6);
        //            businessObject.DoQuery(str2);


        //            while (!businessObject.EoF)
        //            {
        //                DataSet dataSet1 = new DataSet();
        //               // str = new string[] { this.SiteName, ": ", str1, " : Récupération de l'article ayant 'id_product_extern' = '", businessObject.Fields.Item("ItemCode").Value.ToString(), "'" };
        //                this.AddLog(string.Concat(str), 5);
        //                str = new string[] { "SELECT * FROM ", tableMySQLArticles, " WHERE id_product_extern = '", businessObject.Fields.Item("ItemCode").Value.ToString(), "';" };
        //                str2 = string.Concat(str);
        //                //this.AddLog(string.Concat(str1, " : Execution MySQL de la requête -> '", str2, "'"), 6);
        //                MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter(str2, connection);
        //                mySqlDataAdapter1.Fill(dataSet1, "articleExt");
        //                DataTable dataTable = dataSet1.Tables["articleExt"];
        //                dataTable.CaseSensitive = false;
        //                DataRow[] dataRowArray = dataTable.Select(null, "id_product");
        //                if (checked((int)dataRowArray.Length) == 0)
        //                {
        //                    DataRow dataRow = null;
        //                    this.CopyArticleSBOToPrestaShop(ref businessObject, ref dataRow);
        //                }
        //                else
        //                {
        //                    this.CopyArticleSBOToPrestaShop(ref businessObject, ref dataRowArray[0]);
        //                }
        //                this.UpdateArticleSBO(businessObject.Fields.Item("ItemCode").Value.ToString());
        //                businessObject.MoveNext();
        //                if (this.pbEtape == null)
        //                {
        //                    continue;
        //                }
        //                value = this.pbEtape;
        //                value.Value = checked(value.Value + 1);
        //            }
        //            this.FlushMySqlQuery();
        //            Objets.StrCleanModeMySql = false;
        //            this.AddLog(string.Concat(this.SiteName, ": ", str1, " : Done"), 3);
        //            Marshal.ReleaseComObject(businessObject);

        //        }

        //    }
        //    catch (Exception exception1)
        //    {

        //        MessageBox.Show(exception1.Message);

        //        Console.WriteLine("my ex :" + exception1.Message);
        //    }
        //}

    }
}
