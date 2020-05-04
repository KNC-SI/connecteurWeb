using ADODB;
using B1_CrystalBox_Tools;
//using B1Prestashop_tools.My;
using MG2S_ClassLib;
using MG2S_CrystalInterface;
using MG2S_SBOClass;
using MG2S_Mail;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using MySql.Data.MySqlClient;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Utilities.FTP;

namespace RunScriptOvh
{
    class B1Prestashop_Tools
    {
        public object cleCrypt;

        public SAPbobsCOM.Company SBOComp;

        public MySqlConnection cnMySQL;

        public MySqlConnection cnMySQLShema;

        public FTPclient cnFTP;

        public Connection cnADO;

        public DataSet dsOSbuffers;

        public DataTable TbOSproducts;

        public DataTable TbOSproducts_options;

        public DataTable TbOSproducts_options_values;

        public DataTable TbOScountries;

        public string LastLogMessage;

        public TextBox InfoTextBox;

        public ProgressBar pbGlobale;

        public ProgressBar pbEtape;

        public Application App;

        private const string AppName = "B1-Prestashop";

        private const int NbCats = 64;

        private const int NbPicts = 5;

        public string SQLServer;

        public string SQLTypeServer;

        public string SQLUser;

        public string SQLPassword;

        public string SBOUser;

        public string SBOPassword;

        public string SBODBCompany;

        public bool SQLAuthModeWindows;

        public int LogNiveauDetail;

        public string LogFile;

        public string ErrorPath;

        public int ListePrix;

        public int ReqSqlBuffer_NbReqMax;

        private string GlobalReqSqlBufferForPrestaShop;

        private int GlobalReqSqlBufferForPrestaShop_Nb;

        private FTPdirectory FTP_ListeDocs;

        private FTPdirectory FTP_ListeImages;

        public string LogErreurs;

        public string LogNouvCdes;

        public string LogConflits;

        public int Ret;

        public int longName;

        public string pathimg;

        public string pathDoc;

        private string SiteCode;

        private string SiteName;

        public string tableMySQLAdresses;

        public string tableMySQLClients;

        public string tableMySQLFabricants;

        public string tableMySQLArticles;

        public string cheminSAPImages;

        public string tableMySQLLangues;

        public string tableMySQLNomsArticles;

        public string tableMySQLImagesArticles;

        public string tableMySQLPrixSpecifiques;

        public string tableMySQLCaracsArticles;

        public string tableMySQLCaracs;

        public string tableMySQLCaracsLangues;

        public string tableMySQLTransporteurs;

        public string tableMySQLCommandes;

        public string tableMySQLLignesCommandes;

        public string tableMySQLHistoriqueCommandes;

        public string tableMySQLTU;

        public string tableTU;

        public string tableMySQLNomCategory;

        public string default_category;

        public string champMySQLPanier;

        public B1Prestashop_Tools()
        {
            this.cleCrypt = "jt-gr$6!mà0è-(guè-bvgrdf1264361651";
            this.SBOComp = Parametres.oCompany;
            this.cnMySQL = new MySqlConnection();
            this.cnMySQLShema = new MySqlConnection();
            this.cnFTP = new FTPclient();
            //this.cnADO = new ConnectionClass();
            this.dsOSbuffers = new DataSet();
            this.LastLogMessage = "";
            this.InfoTextBox = null;
            this.pbGlobale = null;
            this.pbEtape = null;
            this.App = null;
            this.SQLServer = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SQLServer", "localhost", null));
            this.SQLTypeServer = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "TypeServeur", "MSSQL2008", null));
            this.SQLUser = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SQLUser", "", null));
            this.SQLPassword = Objets.StrDecrypt(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SQLPassword", "", null)), Conversions.ToString(this.cleCrypt), false);
            this.SBOUser = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SBOUser", "manager", null));
            this.SBOPassword = Objets.StrDecrypt(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SBOPassword", "", null)), Conversions.ToString(this.cleCrypt), false);
            this.SBODBCompany = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SBODbCompany", "SBO_AVISO", null));
            this.SQLAuthModeWindows = Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SQLAuthModeWindows", true, null));
            this.LogNiveauDetail = Conversions.ToInteger(Objets.GlobalGetSetting("B1-Prestashop", "Config", "NiveauDetail", 3, null));
            this.LogFile = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "LogFile", "c:\\B1-Prestashop.log", null));
            this.ErrorPath = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "ErrorPath", "c:\\erreurs\\", null));
            this.ListePrix = Conversions.ToInteger(Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", 2, null));
            this.ReqSqlBuffer_NbReqMax = 300;
            this.GlobalReqSqlBufferForPrestaShop = "";
            this.GlobalReqSqlBufferForPrestaShop_Nb = 0;
            this.LogErreurs = "";
            this.LogNouvCdes = "";
            this.LogConflits = "";
            this.Ret = 0;
            this.longName = 32;
            this.pathimg = "";
            this.pathDoc = "";
            this.tableMySQLAdresses = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Adresse", "TableMySQL", "address", null));
            this.tableMySQLClients = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Client", "TableMySQL", "customer", null));
            this.tableMySQLFabricants = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Fabricant", "TableMySQL", "manufacturer", null));
            this.tableMySQLArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLArticles", "product", null));
            this.cheminSAPImages = "";
            this.tableMySQLLangues = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Langue", "TableMySQL", "lang", null));
            this.tableMySQLNomsArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLNoms", "product_lang", null));
            this.tableMySQLImagesArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLImages", "product_image", null));
            this.tableMySQLPrixSpecifiques = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "PrixSpec", "TableMySQL", "specific_price", null));
            this.tableMySQLCaracsArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Carac", "TableMySQLArticles", "product_feature", null));
            this.tableMySQLCaracs = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Carac", "TableMySQLCaracs", "feature", null));
            this.tableMySQLCaracsLangues = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Carac", "TableMySQLCaracsLangues", "feature_lang", null));
            this.tableMySQLTransporteurs = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Transporteur", "TableMySQL", "carrier", null));
            this.tableMySQLCommandes = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Commande", "TableMySQL", "`order`", null));
            this.tableMySQLLignesCommandes = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Commande", "TableMySQLLignes", "order_product", null));
            this.tableMySQLHistoriqueCommandes = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Commande", "TableMySQLHistoriques", "order_history", null));
            this.tableMySQLTU = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "TU", "TU1MySQL", "", null));
            this.tableTU = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "TU", "TU", "", null));
            this.tableMySQLNomCategory = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLNomCategory", "category_lang", null));
            this.default_category = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "CategorieParDefaut", "", null));
            this.champMySQLPanier = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Paniers", "ChampMySQL", "", null));
        }
        public void doSynchroArticles(bool forcer = false)
        {
            string[] str;
            ProgressBar value;
            string str1 = "doSynchroArticles";
            try
            {
                if (!this.cryptageMdp())
                {
                    Console.WriteLine(string.Concat(str1, " : Erreur ==> Le cryptage des mots de passe à une erreur."), 1);
                }
                Console.WriteLine(string.Concat(str1, " : Start"), 3);
                Console.WriteLine(string.Concat(str1, " : SELECT * FROM [@MG2S_SITES]"), 6);
                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                businessObject.DoQuery("SELECT * FROM [@MG2S_SITES]");
                if (businessObject.EoF)
                {
                    Console.WriteLine(string.Concat(str1, ": Il n'y a pas de Sites dans la table utilisateur @MG2S_SITES."), 1);
                }
                while (!businessObject.EoF)
                {

                    this.LogErreurs = "";
                    this.LogNouvCdes = "";
                    this.LogConflits = "";
                    this.SiteCode = businessObject.Fields.Item("Code").Value.ToString();
                    this.SiteName = businessObject.Fields.Item("Name").Value.ToString();
                    this.pathimg = businessObject.Fields.Item("U_WebLinkImg").Value.ToString();
                    this.pathDoc = businessObject.Fields.Item("U_WebLinkFact").Value.ToString();
                    Console.WriteLine(string.Concat(str1, ": ", this.SiteName, " : Start"), 3);
                    Console.WriteLine(string.Concat(str1, ": ", this.SiteName, " : Connecting"), 4);
                    this.cnMySQL.Dispose();
                    this.cnMySQL = null;
                    GC.WaitForFullGCComplete();
                    this.cnMySQL = new MySqlConnection();
                    MySqlConnection mySqlConnection = this.cnMySQL;
                    
                    str = new string[] { "Data Source=", businessObject.Fields.Item("U_SERVEUR").Value.ToString(), ";Database=", businessObject.Fields.Item("U_MYSQLDB").Value.ToString(), ";User Id=", businessObject.Fields.Item("U_MYSQLUSER").Value.ToString(), ";Password=", this.decryptPassword(businessObject.Fields.Item("U_MYSQLPASS").Value.ToString()) };
                    mySqlConnection.ConnectionString = string.Concat(str);
                    MySqlConnection mySqlConnection1 = this.cnMySQL;
                    mySqlConnection1.ConnectionString = string.Concat(mySqlConnection1.ConnectionString, ";use compression=true");
                    this.cnMySQL.Open();
                    this.cnMySQLShema.Dispose();
                    this.cnMySQLShema = null;
                    GC.WaitForFullGCComplete();
                    this.cnMySQLShema = new MySqlConnection();
                    MySqlConnection mySqlConnection2 = this.cnMySQLShema;
                    str = new string[] { "Data Source=", businessObject.Fields.Item("U_SERVEUR").Value.ToString(), ";Database=information_schema;User Id=", businessObject.Fields.Item("U_MYSQLUSER").Value.ToString(), ";Password=", this.decryptPassword(businessObject.Fields.Item("U_MYSQLPASS").Value.ToString()) };
                    mySqlConnection2.ConnectionString = string.Concat(str);
                    mySqlConnection1 = this.cnMySQLShema;
                    mySqlConnection1.ConnectionString = string.Concat(mySqlConnection1.ConnectionString, ";use compression=true");
                    this.cnMySQLShema.Open();
                    string str2 = businessObject.Fields.Item("U_FTPUSER").Value.ToString();
                    if (Operators.CompareString(str2, "testftpOO", false) == 0)
                    {
                        this.cnFTP.Hostname = "fichiers.opti-one.fr";
                        this.cnFTP.Username = "admin_jonathan";
                        this.cnFTP.Password = "Moreau11$";
                        this.cnFTP.CurrentDirectory = "/Interne/Add On/B1-Prestashop/TEST/";
                    }
                    else
                    {
                        this.cnFTP.Hostname = businessObject.Fields.Item("U_SERVEUR").Value.ToString();
                        this.cnFTP.Username = str2;
                        this.cnFTP.Password = this.decryptPassword(businessObject.Fields.Item("U_FTPPASS").Value.ToString());
                        this.cnFTP.CurrentDirectory = businessObject.Fields.Item("U_FTPDIR").Value.ToString();
                        
                    }
                    Console.WriteLine(string.Concat(str1, ": ", this.SiteName, ":Retrieve FTP file list"), 4);
                    this.FTP_ListeImages = this.cnFTP.ListDirectoryDetail(string.Concat(this.cnFTP.CurrentDirectory, "/"));
                    Console.WriteLine(string.Concat(str1, ": ", this.SiteName, ":Connected"), 4);
                    Console.WriteLine(string.Concat(str1, ": ", this.SiteName, ":Load buffers..."), 4);
                    this.RefeshMySQLdataBuffer("all");
                    Console.WriteLine(string.Concat(str1, ": ", this.SiteName, ":Buffers Ready."), 4);
                    //if (this.pbGlobale != null)
                    //{
                    //    this.pbGlobale.Maximum = 2;
                    //    this.pbGlobale.Value = 0;
                    //}
                    this.SynchroFabricant();
                    //if (this.pbGlobale != null)
                    //{
                    //    value = this.pbGlobale;
                    //    value.Value = checked(value.Value + 1);
                    //}
                    this.SynchroArticles(forcer);
                    //if (this.pbGlobale != null)
                    //{
                    //    value = this.pbGlobale;
                    //    value.Value = checked(value.Value + 1);
                    //}
                    Console.WriteLine(string.Concat(str1, " : ", this.SiteName, " : Done"), 4);
                    if (Conversions.ToBoolean((!Conversions.ToBoolean(Operators.CompareString(this.LogErreurs, "", false) != 0) || !Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Config", "MailErr", false, null)) ? false : true)) && (Operators.ConditionalCompareObjectEqual(Objets.GlobalGetSetting("B1-Prestashop", "Config", "cLimitErrByMinutes", "True", null), "False", false) || Operators.ConditionalCompareObjectGreater(DateAndTime.DateDiff(DateInterval.Minute, Conversions.ToDate(Objets.GlobalGetSetting("B1-Prestashop", "Config", "LastMailErr", Conversions.ToDate("01/01/1980"), null)), DateAndTime.Now, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1), Objets.GlobalGetSetting("B1-Prestashop", "Config", "tLimitErrByMinutes", "180", null), false)))
                    {
                        this.QuickSendMail(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "MailErrAdr", "", null)), string.Concat("Erreur(s) à la synchronisation du site ", this.SiteName), this.LogErreurs, false);
                        Objets.GlobalSaveSetting("B1-Prestashop", "Config", "LastMailErr", DateAndTime.Now, null);
                    }
                    if (Conversions.ToBoolean((!Conversions.ToBoolean(Operators.CompareString(this.LogNouvCdes, "", false) != 0) || !Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Config", "MailNouvCdes", false, null)) ? false : true)))
                    {
                        this.QuickSendMail(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "MailNouvCdesAdr", "", null)), string.Concat("Nouvelle(s) commande(s) importées du site ", this.SiteName), this.LogNouvCdes, false);
                    }
                    this.cnMySQL.Close();
                    businessObject.MoveNext();
                }
                Marshal.ReleaseComObject(businessObject);
                Console.WriteLine(string.Concat(str1, " : MaximizeRam"), 5);
                Objets.MaximizeRam(true);
                Console.WriteLine(string.Concat(str1, " : Done"), 3);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                str = new string[] { str1, " : Une exception s'est produite : ", exception.Message, ", StackTrace=", exception.StackTrace };
                Console.WriteLine(string.Concat(str), 1);
                ProjectData.ClearProjectError();
            }
        }
        public void SynchroArticles(bool forcer)
        {
            string[] str;
            ProgressBar value;
            string str1 = "SynchroArticles";
            this.tableMySQLClients = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Client", "TableMySQL", "customer", null));
            this.tableMySQLArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLArticles", "product", null));
            this.tableMySQLFabricants = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Fabricant", "TableMySQL", "manufacturer", null));
            this.tableMySQLLangues = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Langue", "TableMySQL", "lang", null));
            this.tableMySQLNomsArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLNoms", "product_lang", null));
            this.tableMySQLImagesArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "TableMySQLImages", "product_image", null));
            this.tableMySQLPrixSpecifiques = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "PrixSpec", "TableMySQL", "specific_price", null));
            this.default_category = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article", "CategorieParDefaut", "", null));
            DataSet dataSet = new DataSet();
            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
            string str2 = "";
            try
            {
                Console.WriteLine(string.Concat(this.SiteName, ": ", str1, " : Start"), 3);
                str2 = "SELECT BitmapPath FROM OADP";
                Console.WriteLine(string.Concat(str1, " : Execution DI de la requête -> '", str2, "'"), 6);
                businessObject.DoQuery(str2);
                if (!businessObject.EoF)
                {
                    this.cheminSAPImages = businessObject.Fields.Item("BitmapPath").Value.ToString();
                    Console.WriteLine(string.Concat(this.SiteName, ": ", str1, " : Récupération des articles dans Prestashop pour suppression"), 5);
                    str2 = string.Concat("SELECT * FROM ", this.tableMySQLArticles, " p;");
                    Console.WriteLine(string.Concat(str1, " : Execution MySQL de la requête -> '", str2, "'"), 6);
                    //str = new string[] { "Data Source=", businessObject.Fields.Item("U_SERVEUR").Value.ToString(), ";Database=", businessObject.Fields.Item("U_MYSQLDB").Value.ToString(), ";User Id=", businessObject.Fields.Item("U_MYSQLUSER").Value.ToString(), ";Password=", this.decryptPassword(businessObject.Fields.Item("U_MYSQLPASS").Value.ToString())
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(str2, this.cnMySQL);
                    mySqlDataAdapter.Fill(dataSet, "ArticlesBaseIntermédiaire");
                    DataTable item = dataSet.Tables["ArticlesBaseIntermédiaire"];
                    item.CaseSensitive = false;
                    if (this.pbEtape != null)
                    {
                        this.pbEtape.Value = this.pbEtape.Minimum;
                        this.pbEtape.Maximum = item.Rows.Count;
                    }
                    long num = (long)0;
                    long count = (long)(checked(item.Rows.Count - 1));
                   /* for (long i = num; i <= count; i = checked(i + (long)1))
                    {
                        string str3 = Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product_extern"]), false);
                        if (Operators.CompareString(str3, "", false) != 0)
                        {
                            str2 = string.Concat("SELECT ItemCode FROM OITM art WHERE art.ItemCode = '", str3, "' AND art.U_MG2S_SyncWeb = 'Y'", this.getCondPersoArticles());
                            Console.WriteLine(string.Concat(str1, " : Execution DI de la requête -> '", str2, "'"), 6);
                            businessObject.DoQuery(str2);
                            if (businessObject.EoF)
                            {
                                str = new string[] { "UPDATE ", this.tableMySQLArticles, " SET active = 0, state = 1 WHERE id_product =  ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false), ";" };
                                this.PushMySqlQuery(string.Concat(str));
                            }
                        }
                        else
                        {
                            str = new string[] { "DELETE FROM ", this.tableMySQLImagesArticles, " WHERE id_product = ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false), ";" };
                            this.PushMySqlQuery(string.Concat(str));
                            str = new string[] { "DELETE FROM ", this.tableMySQLPrixSpecifiques, " WHERE id_product = ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false).Trim(), ";" };
                            this.PushMySqlQuery(string.Concat(str));
                            str = new string[] { "DELETE FROM ", this.tableMySQLCaracsArticles, " WHERE id_product = ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false), ";" };
                            this.PushMySqlQuery(string.Concat(str));
                            str = new string[] { "DELETE FROM ", this.tableMySQLNomsArticles, " WHERE id_product = ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false), ";" };
                            this.PushMySqlQuery(string.Concat(str));
                            str = new string[] { "DELETE FROM ", this.tableMySQLArticles, " WHERE id_product = ", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[checked((int)i)]["id_product"]), false), ";" };
                            this.PushMySqlQuery(string.Concat(str));
                        }
                        if (this.pbEtape != null)
                        {
                            value = this.pbEtape;
                            value.Value = checked(value.Value + 1);
                        }
                    }*/
                    //this.FlushMySqlQuery();
                    Console.WriteLine(string.Concat(this.SiteName, ": ", str1, ": Récupération des articles créés ou modifiés dans SBO depuis la dernière synchro"), 5);
                    str2 = string.Concat("SELECT art.*, gart.ItmsGrpNam FROM OITM art LEFT JOIN OITB gart ON gart.ItmsGrpCod = art.ItmsGrpCod WHERE art.U_MG2S_Update = 'Y' AND art.SellItem = 'Y' AND art.U_MG2S_SyncWeb = 'Y' AND art.U_MG2S_Actif IS NOT NULL AND art.U_MG2S_ExcluWeb IS NOT NULL AND art.frozenFor = 'N' ", this.getCondPersoArticles());
                    Console.WriteLine(string.Concat(str1, " : Execution DI de la requête -> '", str2, "'"), 6);
                    businessObject.DoQuery(str2);
                    if (this.pbEtape != null)
                    {
                        this.pbEtape.Value = this.pbEtape.Minimum;
                        this.pbEtape.Maximum = businessObject.RecordCount;
                    }
                    this.GlobalReqSqlBufferForPrestaShop = "";
                    this.GlobalReqSqlBufferForPrestaShop_Nb = 0;
                    while (!businessObject.EoF)
                    {
                        DataSet dataSet1 = new DataSet();
                        str = new string[] { this.SiteName, ": ", str1, " : Récupération de l'article ayant 'id_product_extern' = '", businessObject.Fields.Item("ItemCode").Value.ToString(), "'" };
                        Console.WriteLine(string.Concat(str), 5);
                        str = new string[] { "SELECT * FROM ", this.tableMySQLArticles, " WHERE id_product_extern = '", businessObject.Fields.Item("ItemCode").Value.ToString(), "';" };
                        str2 = string.Concat(str);
                        Console.WriteLine(string.Concat(str1, " : Execution MySQL de la requête -> '", str2, "'"), 6);
                        MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter(str2, this.cnMySQL);
                        mySqlDataAdapter1.Fill(dataSet1, "articleExt");
                        DataTable dataTable = dataSet1.Tables["articleExt"];
                        dataTable.CaseSensitive = false;
                        DataRow[] dataRowArray = dataTable.Select(null, "id_product");
                        if (checked((int)dataRowArray.Length) == 0)
                        {
                            
                            DataRow dataRow = null;
                            this.CopyArticleSBOToPrestaShop( businessObject,  dataRow);
                        }
                        else
                        {
                            
                            this.CopyArticleSBOToPrestaShop( businessObject,  dataRowArray[0]);
                        }
                        this.UpdateArticleSBO(businessObject.Fields.Item("ItemCode").Value.ToString());
                        businessObject.MoveNext();
                        if (this.pbEtape == null)
                        {
                            continue;
                        }
                        value = this.pbEtape;
                        value.Value = checked(value.Value + 1);
                    }
                    this.FlushMySqlQuery();
                    Objets.StrCleanModeMySql = false;
                    Console.WriteLine(string.Concat(this.SiteName, ": ", str1, " : Done"), 3);
                    Marshal.ReleaseComObject(businessObject);
                }
                else
                {
                    Console.WriteLine(string.Concat(this.SiteName, " : ", str1, " : Il(n) 'y a pas de chemin pour les images dans les options générales."), 1);
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                str = new string[] { this.SiteName, ": ", str1, " : Une erreur s'est produite : ", exception.Message, ", stacktrace=", exception.StackTrace };
                Console.WriteLine(string.Concat(str), 1);
                Marshal.ReleaseComObject(businessObject);
                ProjectData.ClearProjectError();
            }
        }
        private string PersoToVal(string str, string itemCode)
        {
            string str1;
            string[] siteName;
            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {
                string str2 = str;
                string str3 = "SELECT ";
                if (str2.Contains("@Stock"))
                {
                    string str4 = "(SELECT SUM(OnHand)-SUM(IsCommited) AS 'QtyStock' FROM OITW WHERE (";
                    string str5 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "MagasinsChecked", "", null));
                    string[] strArrays = str5.Split(new char[] { '*' });
                    string[] strArrays1 = strArrays;
                    for (int i = 0; i < checked((int)strArrays1.Length); i = checked(i + 1))
                    {
                        string str6 = strArrays1[i];
                        str4 = (Operators.CompareString(str6, "", false) == 0 ? Strings.Left(str4, checked(str4.Length - 3)) : string.Concat(str4, " WhsCode = '", str6, "' OR"));
                    }
                    str4 = string.Concat(str4, ") AND ItemCode = OITM.ItemCode) AS 'Stock'");
                    str3 = string.Concat(str3, str.Replace("@Stock", str4));
                    str3 = string.Concat(str3, "FROM OITM WHERE OITM.ItemCode = '", itemCode, "'");
                    Console.WriteLine(string.Concat("PersoToVal : Execution DI de la requête -> '", str3, "'"), 6);
                    businessObject.DoQuery(str3);
                    if (!businessObject.EoF)
                    {
                        str2 = businessObject.Fields.Item("Stock").Value.ToString();
                        Marshal.ReleaseComObject(businessObject);
                        str1 = str2;
                    }
                    else
                    {
                        Console.WriteLine(string.Concat(this.SiteName, " : PersoToVal : L'article '", itemCode, "' n'existe pas dans la base de données SBO"), 1);
                        Marshal.ReleaseComObject(businessObject);
                        str1 = "";
                    }
                }
                else if (str2.Contains("@Prix"))
                {
                    siteName = new string[] { str3, "Price FROM ITM1 WHERE ItemCode = '", itemCode, "' AND PriceList = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", "", null).ToString() };
                    str3 = string.Concat(siteName);
                   // Console.WriteLine(string.Concat("PersoToVal : Execution DI de la requête -> '", str3, "'"), 6);
                    businessObject.DoQuery(str3);
                    if (!businessObject.EoF)
                    {
                        str2 = businessObject.Fields.Item("Price").Value.ToString();
                        str1 = str2;
                    }
                    else
                    {
                        siteName = new string[] { this.SiteName, " : PersoToVal : L'article '", itemCode, "' n'a pas de prix dans la base de données SBO sur la liste de prix ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", "", null).ToString() };
                        Console.WriteLine(string.Concat(siteName), 1);
                        Marshal.ReleaseComObject(businessObject);
                        str1 = "";
                    }
                }
                else if (!str2.Contains("@DateReception"))
                {
                    siteName = new string[] { str3, str2, " AS 'Perso' FROM OITM WHERE OITM.ItemCode = '", itemCode, "'" };
                    str3 = string.Concat(siteName);
                    Console.WriteLine(string.Concat("PersoToVal : Execution DI de la requête -> '", str3, "'"), 6);
                    businessObject.DoQuery(str3);
                    if (!businessObject.EoF)
                    {
                        str2 = businessObject.Fields.Item("Perso").Value.ToString();
                        Marshal.ReleaseComObject(businessObject);
                        str1 = str2;
                    }
                    else
                    {
                        siteName = new string[] { this.SiteName, " : PersoToVal : L'article '", itemCode, "' n'existe pas dans la base de données SBO sur la liste de prix ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", "", null).ToString() };
                        Console.WriteLine(string.Concat(siteName), 1);
                        Marshal.ReleaseComObject(businessObject);
                        str1 = "";
                    }
                }
                else
                {
                    double num = Conversions.ToDouble(this.PersoToVal("@Stock", itemCode));
                    if (num <= 0)
                    {
                        str3 = string.Concat(str3, "Shipdate, Sum(OpenQty) AS 'Quantity' FROM POR1 T0 LEFT JOIN OITM T1 ON T1.ItemCode=T0.ItemCode WHERE OpenQty<>0 AND T0.ItemCode = '", itemCode, "' GROUP BY ShipDate ORDER BY ShipDate");
                        Console.WriteLine(string.Concat("PersoToVal : Execution DI de la requête -> '", str3, "'"), 6);
                        businessObject.DoQuery(str3);
                        if (!businessObject.EoF)
                        {
                            str2 = "";
                            double num1 = 0;
                            while (!businessObject.EoF)
                            {
                                num1 = Conversions.ToDouble(Operators.AddObject(num1, businessObject.Fields.Item("Quantity").Value));
                                if (num + num1 <= 0)
                                {
                                    businessObject.MoveNext();
                                }
                                else
                                {
                                    str2 = Strings.Left(businessObject.Fields.Item("ShipDate").Value.ToString(), 10);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            str2 = "";
                        }
                    }
                    else
                    {
                        str2 = "";
                    }
                    str1 = str2;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Marshal.ReleaseComObject(businessObject);
                Console.WriteLine(string.Concat(this.SiteName, " : PersoToVal : ", exception.Message), 1);
                str1 = "";
                ProjectData.ClearProjectError();
            }
            return str1;
        }
        private void CopyArticleSBOToPrestaShop(SAPbobsCOM.Recordset RSItemCode,  DataRow drIdProduct)
        {
            int i;
            string[] database;
            char[] chrArray;
            object[] objArray;
            object[] objArray1;
            object[] objArray2;
            object[] objArray3;
            string str = "CopyArticleSBOToPrestaShop";
            try
            {
                bool flag = false;
                string str1 = Conversions.ToString(Interaction.IIf(Operators.CompareString(RSItemCode.Fields.Item("U_MG2S_Actif").Value.ToString(), "Y", false) == 0, "1", "0"));
                string str2 = Conversions.ToString(Interaction.IIf(Operators.CompareString(RSItemCode.Fields.Item("U_MG2S_ExcluWeb").Value.ToString(), "Y", false) == 0, "1", "0"));
                database = new string[] { "SELECT m.id_manufacturer, cl.id_category FROM ", this.tableMySQLFabricants, " m, ", this.tableMySQLNomCategory, " cl WHERE m.id_manufacturer_extern = ", Objets.StrClean(RSItemCode.Fields.Item("FirmCode").Value.ToString()), " AND cl.name = '", Objets.StrClean(this.default_category), "';" };
                string str3 = string.Concat(database);
                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(str3, this.cnMySQL);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "mcl");
                DataTable item = dataSet.Tables["mcl"];
                item.CaseSensitive = false;
                string str4 = Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["id_manufacturer"]), false).Trim();
                string str5 = Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["id_category"]), false).Trim();
                string str6 = RSItemCode.Fields.Item("ItemCode").Value.ToString();
                string str7 = "";
                if (drIdProduct != null)
                {
                    str7 = Objets.GetStr(RuntimeHelpers.GetObjectValue(drIdProduct["id_product"]), false).Trim();
                    str3 = string.Concat("UPDATE ", this.tableMySQLArticles, " SET ");
                    if (Operators.ConditionalCompareObjectNotEqual(str4, drIdProduct["id_manufacturer"], false))
                    {
                        str3 = string.Concat(str3, "id_manufacturer = ", str4, ", ");
                        flag = true;
                    }
                    if (Operators.CompareString(str2, drIdProduct["online_only"].ToString(), false) != 0)
                    {
                        str3 = string.Concat(str3, "online_only = ", str2, ", ");
                        flag = true;
                    }
                    if (Operators.CompareString(str1, drIdProduct["active"].ToString(), false) != 0)
                    {
                        str3 = string.Concat(str3, "active = ", str1, ", ");
                        flag = true;
                    }
                    for (i = 0; Operators.ConditionalCompareObjectNotEqual(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", i.ToString()), "", null), "", false); i = checked(i + 1))
                    {
                        string str8 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", i.ToString()), "", null));
                        if (str8.Contains("product."))
                        {
                            chrArray = new char[] { '.' };
                            str8 = str8.Split(chrArray)[1];
                            string str9 = Objets.GetStr(RuntimeHelpers.GetObjectValue(drIdProduct[str8]), false);
                            string str10 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("1*", i.ToString()), "", null));
                            if (Operators.CompareString(str10, "Perso", false) != 0)
                            {
                                object obj = RSItemCode.Fields.Item(str10).Value.ToString();
                                if (Operators.ConditionalCompareObjectNotEqual(obj, str9, false))
                                {
                                    str3 = string.Concat(str3, str8, " = ");
                                    database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLArticles, "' AND COLUMN_NAME = '", str8, "';" };
                                    string str11 = string.Concat(database);
                                    Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str11, "'"), 6);
                                    MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter(str11, this.cnMySQLShema);
                                    DataSet dataSet1 = new DataSet();
                                    mySqlDataAdapter1.Fill(dataSet1, "datacolBI");
                                    object item1 = dataSet1.Tables["datacolBI"];
                                    objArray = new object[] { false };
                                    NewLateBinding.LateSet(item1, null, "CaseSensitive", objArray, null, null);
                                    objArray = new object[] { 0 };
                                    object obj1 = NewLateBinding.LateGet(item1, null, "Rows", objArray, null, null, null);
                                    objArray1 = new object[] { "DATA_TYPE" };
                                    bool flag1 = NewLateBinding.LateIndexGet(obj1, objArray1, null).ToString().Contains("int");
                                    objArray2 = new object[] { 0 };
                                    object obj2 = NewLateBinding.LateGet(item1, null, "Rows", objArray2, null, null, null);
                                    objArray3 = new object[] { "DATA_TYPE" };
                                    str3 = (!(flag1 | Operators.CompareString(NewLateBinding.LateIndexGet(obj2, objArray3, null).ToString(), "decimal", false) == 0) ? string.Concat(str3, "'", Objets.StrClean(obj.ToString()), "', ") : string.Concat(str3, Objets.StrClean(obj.ToString()), ", "));
                                    flag = true;
                                }
                            }
                            else
                            {
                                string str12 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("2*", i.ToString()), "", null));
                                string val = this.PersoToVal(str12, str6);
                                if (Operators.CompareString(val, "", false) != 0 & Operators.CompareString(val, str9, false) != 0)
                                {
                                    database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLArticles, "' AND COLUMN_NAME = '", str8, "';" };
                                    string str13 = string.Concat(database);
                                    Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str13, "'"), 6);
                                    MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(str13, this.cnMySQLShema);
                                    DataSet dataSet2 = new DataSet();
                                    mySqlDataAdapter2.Fill(dataSet2, "datacolBI");
                                    DataTable dataTable = dataSet2.Tables["datacolBI"];
                                    dataTable.CaseSensitive = false;
                                    if (dataTable.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(dataTable.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0)
                                    {
                                        if (Conversions.ToDouble(val.Replace(".", ",")) != Conversions.ToDouble(Interaction.IIf(Operators.CompareString(str9, "", false) == 0, "0", str9)))
                                        {
                                            str3 = string.Concat(str3, str8, " = ");
                                            str3 = string.Concat(str3, Objets.StrClean(val.Replace(",", ".")), ", ");
                                            flag = true;
                                        }
                                    }
                                    else if (Operators.CompareString(val, str9, false) != 0)
                                    {
                                        str3 = string.Concat(str3, str8, " = ");
                                        str3 = string.Concat(str3, "'", Objets.StrClean(val), "', ");
                                        flag = true;
                                    }
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        str3 = string.Concat(str3, "state = 1 WHERE id_product = ", str7, ";");
                        this.PushMySqlQuery(str3);
                    }
                    database = new string[] { "SELECT * FROM ", this.tableMySQLNomsArticles, " WHERE id_product = ", str7, " AND id_lang = (SELECT id_lang FROM ", this.tableMySQLLangues, " WHERE iso_code = 'FR');" };
                    str3 = string.Concat(database);
                    Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                    mySqlDataAdapter = new MySqlDataAdapter(str3, this.cnMySQL);
                    dataSet = new DataSet();
                    mySqlDataAdapter.Fill(dataSet, "product_lang");
                    item = dataSet.Tables["product_lang"];
                    item.CaseSensitive = false;
                    Console.WriteLine(item.Rows.Count);
                    switch (item.Rows.Count)
                    {
                        case 0:
                            {
                                str3 = string.Concat("INSERT INTO ", this.tableMySQLNomsArticles, " (id_product, id_lang, ");
                                database = new string[] { "VALUES (", str7, ", (SELECT id_lang FROM ", this.tableMySQLLangues, " WHERE iso_code = 'FR'), " };
                                string str14 = string.Concat(database);
                                for (i = 0; Operators.ConditionalCompareObjectNotEqual(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", i.ToString()), "", null), "", false); i = checked(i + 1))
                                {
                                    string str15 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", i.ToString()), "", null));
                                    if (str15.Contains("product_lang."))
                                    {
                                        chrArray = new char[] { '.' };
                                        str15 = str15.Split(chrArray)[1];
                                        string str16 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("1*", i.ToString()), "", null));
                                        if (Operators.CompareString(str16, "Perso", false) != 0)
                                        {
                                            object obj3 = RSItemCode.Fields.Item(str16).Value.ToString();
                                            str3 = string.Concat(str3, str15, ", ");
                                            database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLNomsArticles, "' AND COLUMN_NAME = '", str15, "';" };
                                            string str17 = string.Concat(database);
                                            Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str17, "'"), 6);
                                            mySqlDataAdapter = new MySqlDataAdapter(str17, this.cnMySQLShema);
                                            dataSet = new DataSet();
                                            mySqlDataAdapter.Fill(dataSet, "datacolBI");
                                            item = dataSet.Tables["datacolBI"];
                                            item.CaseSensitive = false;
                                            str14 = (!(item.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(item.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0) ? string.Concat(str14, "'", Objets.StrClean(obj3.ToString()), "', ") : string.Concat(str14, Objets.StrClean(obj3.ToString()), ", "));
                                        }
                                        else
                                        {
                                            string str18 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("2*", i.ToString()), "", null));
                                            string val1 = this.PersoToVal(str18, str6);
                                            if (Operators.CompareString(val1, "", false) != 0)
                                            {
                                                str3 = string.Concat(str3, str15, ", ");
                                                database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLNomsArticles, "' AND COLUMN_NAME = '", str15, "';" };
                                                string str19 = string.Concat(database);
                                                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str19, "'"), 6);
                                                mySqlDataAdapter = new MySqlDataAdapter(str19, this.cnMySQLShema);
                                                dataSet = new DataSet();
                                                mySqlDataAdapter.Fill(dataSet, "datacolBI");
                                                item = dataSet.Tables["datacolBI"];
                                                item.CaseSensitive = false;
                                                str14 = (!(item.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(item.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0) ? string.Concat(str14, "'", Objets.StrClean(val1), "', ") : string.Concat(str14, Objets.StrClean(val1), ", "));
                                            }
                                        }
                                    }
                                }
                                str3 = string.Concat(Strings.Left(str3, checked(str3.Length - 2)), ")", Strings.Left(str14, checked(str14.Length - 2)), ");");
                                this.PushMySqlQuery(str3);
                                database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                                this.PushMySqlQuery(string.Concat(database));
                                flag = true;
                                break;
                            }
                        case 1:
                            {
                                bool flag2 = false;
                                str3 = string.Concat("UPDATE ", this.tableMySQLNomsArticles, " SET ");
                                for (i = 0; Operators.ConditionalCompareObjectNotEqual(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", i.ToString()), "", null), "", false); i = checked(i + 1))
                                {
                                    string str20 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", i.ToString()), "", null));
                                    if (str20.Contains("product_lang."))
                                    {
                                        chrArray = new char[] { '.' };
                                        str20 = str20.Split(chrArray)[1];
                                        string str21 = Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0][str20]), false);
                                        string str22 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("1*", i.ToString()), "", null));
                                        if (Operators.CompareString(str22, "Perso", false) != 0)
                                        {
                                            object obj4 = RSItemCode.Fields.Item(str22).Value.ToString();
                                            if (Operators.ConditionalCompareObjectNotEqual(obj4, str21, false))
                                            {
                                                str3 = string.Concat(str3, str20, " = ");
                                                database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLNomsArticles, "' AND COLUMN_NAME = '", str20, "';" };
                                                string str23 = string.Concat(database);
                                                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str23, "'"), 6);
                                                MySqlDataAdapter mySqlDataAdapter3 = new MySqlDataAdapter(str23, this.cnMySQLShema);
                                                DataSet dataSet3 = new DataSet();
                                                mySqlDataAdapter3.Fill(dataSet3, "datacolBI");
                                                object item2 = dataSet3.Tables["datacolBI"];
                                                objArray3 = new object[] { false };
                                                NewLateBinding.LateSet(item2, null, "CaseSensitive", objArray3, null, null);
                                                objArray3 = new object[] { 0 };
                                                object obj5 = NewLateBinding.LateGet(item2, null, "Rows", objArray3, null, null, null);
                                                objArray2 = new object[] { "DATA_TYPE" };
                                                bool flag3 = NewLateBinding.LateIndexGet(obj5, objArray2, null).ToString().Contains("int");
                                                objArray1 = new object[] { 0 };
                                                object obj6 = NewLateBinding.LateGet(item2, null, "Rows", objArray1, null, null, null);
                                                objArray = new object[] { "DATA_TYPE" };
                                                str3 = (!(flag3 | Operators.CompareString(NewLateBinding.LateIndexGet(obj6, objArray, null).ToString(), "decimal", false) == 0) ? string.Concat(str3, "'", Objets.StrClean(obj4.ToString()), "', ") : string.Concat(str3, Objets.StrClean(obj4.ToString()), ", "));
                                                flag2 = true;
                                            }
                                        }
                                        else
                                        {
                                            string str24 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("2*", i.ToString()), "", null));
                                            string val2 = this.PersoToVal(str24, str6);
                                            if (Operators.CompareString(val2, "", false) != 0 & Operators.CompareString(val2, str21, false) != 0)
                                            {
                                                str3 = string.Concat(str3, str20, " = ");
                                                database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLNomsArticles, "' AND COLUMN_NAME = '", str20, "';" };
                                                string str25 = string.Concat(database);
                                                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str25, "'"), 6);
                                                MySqlDataAdapter mySqlDataAdapter4 = new MySqlDataAdapter(str25, this.cnMySQLShema);
                                                DataSet dataSet4 = new DataSet();
                                                mySqlDataAdapter4.Fill(dataSet4, "datacolBI");
                                                object item3 = dataSet4.Tables["datacolBI"];
                                                objArray3 = new object[] { false };
                                                NewLateBinding.LateSet(item3, null, "CaseSensitive", objArray3, null, null);
                                                objArray3 = new object[] { 0 };
                                                object obj7 = NewLateBinding.LateGet(item3, null, "Rows", objArray3, null, null, null);
                                                objArray2 = new object[] { "DATA_TYPE" };
                                                bool flag4 = NewLateBinding.LateIndexGet(obj7, objArray2, null).ToString().Contains("int");
                                                objArray1 = new object[] { 0 };
                                                object obj8 = NewLateBinding.LateGet(item3, null, "Rows", objArray1, null, null, null);
                                                objArray = new object[] { "DATA_TYPE" };
                                                str3 = (!(flag4 | Operators.CompareString(NewLateBinding.LateIndexGet(obj8, objArray, null).ToString(), "decimal", false) == 0) ? string.Concat(str3, "'", Objets.StrClean(val2), "', ") : string.Concat(str3, Objets.StrClean(val2), ", "));
                                                flag2 = true;
                                            }
                                        }
                                    }
                                }
                                if (!flag2)
                                {
                                    break;
                                }
                                database = new string[] { Strings.Left(str3, checked(str3.Length - 2)), " WHERE id_product = ", str7, " AND id_lang = ", item.Rows[0]["id_lang"].ToString(), ";" };
                                str3 = string.Concat(database);
                                this.PushMySqlQuery(str3);
                                if (!flag)
                                {
                                    database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                                    this.PushMySqlQuery(string.Concat(database));
                                    flag = true;
                                }
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("CopyArticleSBOToPrestashop : Il y a plusieurs noms pour un article dans la base intermédiaire", 1);
                                return;
                            }
                    }
                    //this.SynchroCaracteristiques(str7,RSItemCode,flag);
                    i = 1;
                    do
                    {
                        string str26 = RSItemCode.Fields.Item(string.Concat("U_MG2S_Image", i.ToString())).Value.ToString();
                        if (Operators.CompareString(str26, "", false) == 0)
                        {
                            break;
                        }
                        database = new string[] { "SELECT * FROM ", this.tableMySQLImagesArticles, " WHERE id_product = ", str7, " AND position = ", i.ToString(), ";" };
                        string str27 = string.Concat(database);
                        Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str27, "'"), 6);
                        mySqlDataAdapter = new MySqlDataAdapter(str27, this.cnMySQL);
                        dataSet = new DataSet();
                        mySqlDataAdapter.Fill(dataSet, "product_image");
                        item = dataSet.Tables["product_image"];
                        item.CaseSensitive = false;
                        if (item.Rows.Count != 1)
                        {
                            this.PushMySqlQuery(Conversions.ToString(Operators.AddObject(Operators.AddObject(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("INSERT INTO ", this.tableMySQLImagesArticles), "(id_product, url, position, cover) VALUES ("), str7), ", '"), this.pathimg), Objets.StrClean(str26)), "', "), i.ToString()), ", "), Interaction.IIf(i == 1, "1", "0")), ");")));
                            database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                            this.PushMySqlQuery(string.Concat(database));
                            flag = true;
                            string str28 = string.Concat(this.cheminSAPImages, str26);
                            if (File.Exists(str28) && DateTime.Compare(File.GetLastWriteTime(str28), this.GetDateOfFile(  this.FTP_ListeImages, Path.GetFileName(str28))) > 0)
                            {
                                database = new string[] { "FTP Upload ", str28, " -> ", this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str28) };
                                Console.WriteLine(string.Concat(database), 5);
                                this.cnFTP.Upload(str28, string.Concat(this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str28)));
                            }
                        }
                        else if (Operators.CompareString(string.Concat(this.pathimg, str26), Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["url"]), false)), false) == 0)
                        {
                            string str29 = string.Concat(this.cheminSAPImages, str26);
                            if (File.Exists(str29))
                            {
                                if (!this.cnFTP.FtpFileExists(str26))
                                {
                                    database = new string[] { "FTP Upload ", str29, " -> ", this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str29) };
                                    Console.WriteLine(string.Concat(database), 5);
                                    this.cnFTP.Upload(str29, string.Concat(this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str29)));
                                    if (Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Article", "NULLImg", false, null)))
                                    {
                                        database = new string[] { "UPDATE ", this.tableMySQLImagesArticles, " SET id_image_prestashop = NULL WHERE id_product='", str7, "' AND position=", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["position"]), false).Trim(), ";" };
                                        str3 = string.Concat(database);
                                        this.PushMySqlQuery(str3);
                                    }
                                    database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                                    this.PushMySqlQuery(string.Concat(database));
                                    flag = true;
                                }
                                else if (DateTime.Compare(File.GetLastWriteTime(str29), this.GetDateOfFile(  this.FTP_ListeImages, Path.GetFileName(str29))) > 0)
                                {
                                    database = new string[] { "FTP Upload ", str29, " -> ", this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str29) };
                                    Console.WriteLine(string.Concat(database), 5);
                                    this.cnFTP.Upload(str29, string.Concat(this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str29)));
                                    if (Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Article", "NULLImg", false, null)))
                                    {
                                        database = new string[] { "UPDATE ", this.tableMySQLImagesArticles, " SET id_image_prestashop = NULL WHERE id_product='", str7, "' AND position=", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["position"]), false).Trim(), ";" };
                                        str3 = string.Concat(database);
                                        this.PushMySqlQuery(str3);
                                    }
                                    database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                                    this.PushMySqlQuery(string.Concat(database));
                                    flag = true;
                                }
                            }
                        }
                        else
                        {
                            str3 = Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("UPDATE ", this.tableMySQLImagesArticles), " SET url = '"), this.pathimg), Objets.StrClean(RSItemCode.Fields.Item(string.Concat("U_MG2S_Image", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["position"]), false).Trim())).Value.ToString())), "', cover = "), Interaction.IIf(i == 1, "1", "0")), Interaction.IIf(Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Article", "NULLImg", false, null)), ", id_image_prestashop = NULL", "")), " WHERE id_product='"), str7), "' AND position="), Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["position"]), false).Trim()), ";"));
                            this.PushMySqlQuery(str3);
                            database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                            this.PushMySqlQuery(string.Concat(database));
                            flag = true;
                            string str30 = string.Concat(this.cheminSAPImages, str26);
                            if (File.Exists(str30))
                            {
                                if (DateTime.Compare(File.GetLastWriteTime(str30), this.GetDateOfFile(  this.FTP_ListeImages, Path.GetFileName(str30))) > 0)
                                {
                                    database = new string[] { "FTP Upload ", str30, " -> ", this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str30) };
                                    Console.WriteLine(string.Concat(database), 5);
                                    this.cnFTP.Upload(str30, string.Concat(this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str30)));
                                    str3 = Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("UPDATE ", this.tableMySQLImagesArticles), " SET url = '"), this.pathimg), Objets.StrClean(RSItemCode.Fields.Item(string.Concat("U_MG2S_Image", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["position"]), false).Trim())).Value.ToString())), "', cover = "), Interaction.IIf(i == 1, "1", "0")), Interaction.IIf(Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Article", "NULLImg", false, null)), ", id_image_prestashop = NULL", "")), " WHERE id_product='"), str7), "' AND position="), Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["position"]), false).Trim()), ";"));
                                    this.PushMySqlQuery(str3);
                                }
                            }
                        }
                        i = checked(i + 1);
                    }
                    while (i <= 6);
                    if (Operators.CompareString(this.tableMySQLPrixSpecifiques, "", false) != 0)
                    {
                        SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                        database = new string[] { "SELECT v1.*, T0.DocEntry FROM (SELECT T0.ItemCode, T0.CardCode, T0.ItemCode + '/' + T0.CardCode + '/-1/-1' AS 'IDExt', T0.Discount AS 'Discount', T0.Price AS 'Price', T0.Currency AS 'Currency', '99991231' AS 'FromDate', '99991231' AS 'ToDate', NULL AS 'Amount' FROM OSPP T0 WHERE T0.ListNum = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", null, null).ToString(), " AND T0.CardCode <> '*2' AND T0.ItemCode = '", str6, "' UNION ALL SELECT T0.ItemCode, T0.CardCode, T0.ItemCode + '/' + T0.CardCode + '/' + CONVERT(VARCHAR(10),T1.LINENUM) + '/-1' AS 'IDExt', T1.Discount AS 'Discount', T1.Price AS 'Price', T1.Currency AS 'Currency', T1.FromDate, CASE WHEN ISNULL(T1.ToDate, '') = '' THEN '99991231' ELSE T1.ToDate END AS 'ToDate', NULL AS 'Amount' FROM OSPP T0 INNER JOIN SPP1 T1 ON T1.CardCode = T0.CardCode AND T1.ItemCode = T0.ItemCode WHERE T0.ListNum = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", null, null).ToString(), " AND T0.CardCode <> '*2' AND T0.ItemCode = '", str6, "' UNION ALL SELECT T0.ItemCode, T0.CardCode, T0.ItemCode + '/' + T0.CardCode + '/' + CONVERT(VARCHAR(10),T1.LINENUM) + '/' + CONVERT(VARCHAR(10),T2.SPP2LNum) AS 'IDExt', T2.Discount AS 'Discount', T2.Price AS 'Price', T2.Currency AS 'Currency', T1.FromDate, CASE WHEN ISNULL(T1.ToDate, '') = '' THEN '99991231' ELSE T1.ToDate END AS 'ToDate', T2.Amount FROM OSPP T0 INNER JOIN SPP1 T1 ON T1.CardCode = T0.CardCode AND T1.ItemCode = T0.ItemCode INNER JOIN SPP2 T2 ON T2.CardCode = T1.CardCode AND T2.ItemCode = T1.ItemCode AND T2.SPP1LNum = T1.LINENUM WHERE T0.ListNum = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", null, null).ToString(), " AND T0.CardCode <> '*2' AND T0.ItemCode = '", str6, "') v1 INNER JOIN OCRD T0 ON T0.CardCode = v1.CardCode WHERE (T0.E_Mail IS NOT NULL AND (SELECT COUNT(E_Mail) AS 'NbEmail' FROM OCRD WHERE E_Mail = T0.E_Mail AND U_MG2S_Valid = 'Y') = 1) AND T0.CardName IS NOT NULL AND T0.U_MG2S_Valid = 'Y' AND (T0.UpdateDate >= T0.U_MG2S_DateSync OR T0.U_MG2S_DateSync IS NULL)" };
                        str3 = string.Concat(database);
                        Console.WriteLine(string.Concat(str, " : Execution DI de la requête -> '", str3, "'"), 6);
                        businessObject.DoQuery(str3);
                        while (!businessObject.EoF)
                        {
                            database = new string[] { "SELECT * FROM ", this.tableMySQLPrixSpecifiques, " sf INNER JOIN ", this.tableMySQLArticles, " p ON p.id_product = sf.id_product INNER JOIN ", this.tableMySQLClients, " c ON c.id_customer = sf.id_customer WHERE id_product_extern = '", businessObject.Fields.Item("ItemCode").Value.ToString(), "' AND id_customer_extern = '", businessObject.Fields.Item("DocEntry").Value.ToString(), "' AND id_specific_price_extern = '", businessObject.Fields.Item("IDExt").Value.ToString(), "';" };
                            str3 = string.Concat(database);
                            Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                            mySqlDataAdapter = new MySqlDataAdapter(str3, this.cnMySQL);
                            dataSet = new DataSet();
                            mySqlDataAdapter.Fill(dataSet, "price_item_client");
                            item = dataSet.Tables["price_item_client"];
                            item.CaseSensitive = false;
                            switch (item.Rows.Count)
                            {
                                case 0:
                                    {
                                        database = new string[] { "SELECT p.id_product, c.id_customer FROM ", this.tableMySQLArticles, " p, ", this.tableMySQLClients, " c WHERE id_product_extern = '", businessObject.Fields.Item("ItemCode").Value.ToString(), "' AND id_customer_extern = '", businessObject.Fields.Item("DocEntry").Value.ToString(), "';" };
                                        str3 = string.Concat(database);
                                        Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                                        mySqlDataAdapter = new MySqlDataAdapter(str3, this.cnMySQL);
                                        dataSet = new DataSet();
                                        mySqlDataAdapter.Fill(dataSet, "pc");
                                        item = dataSet.Tables["pc"];
                                        item.CaseSensitive = false;
                                        database = new string[] { "INSERT INTO ", this.tableMySQLPrixSpecifiques, "(id_specific_price_extern, id_customer, id_product, price, from_quantity, reduction, reduction_type, `from`, `to`, state) VALUES ('", businessObject.Fields.Item("IDExt").Value.ToString(), "', ", Objets.StrClean(Conversions.ToString(item.Rows[0]["id_customer"])).Trim(), ", ", Objets.StrClean(Conversions.ToString(item.Rows[0]["id_product"])).Trim(), ", ", businessObject.Fields.Item("Price").Value.ToString().Replace(",", "."), ", ", Objets.StrClean(businessObject.Fields.Item("Amount").Value.ToString()), ", ", businessObject.Fields.Item("Discount").Value.ToString().Replace(",", "."), ", 'percentage', '", Objets.Date2SQL(Conversions.ToDate(businessObject.Fields.Item("FromDate").Value), false), "', '", Objets.Date2SQL(Conversions.ToDate(businessObject.Fields.Item("ToDate").Value), false), "', 1);" };
                                        this.PushMySqlQuery(string.Concat(database));
                                        if (flag)
                                        {
                                            break;
                                        }
                                        database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                                        this.PushMySqlQuery(string.Concat(database));
                                        flag = true;
                                        break;
                                    }
                                case 1:
                                    {
                                        str3 = string.Concat("UPDATE ", this.tableMySQLPrixSpecifiques, " SET ");
                                        bool flag5 = false;
                                        if (Operators.CompareString(Objets.StrClean(businessObject.Fields.Item("Price").Value.ToString().Replace(",", ".")), Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["price"]), false)), false) != 0)
                                        {
                                            str3 = string.Concat(str3, "price = ", businessObject.Fields.Item("Price").Value.ToString().Replace(",", "."));
                                            flag5 = true;
                                        }
                                        if (Operators.CompareString(Objets.StrClean(businessObject.Fields.Item("Amount").Value.ToString()), Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["from_quantity"]), false)), false) != 0)
                                        {
                                            if (flag5)
                                            {
                                                str3 = string.Concat(str3, ", ");
                                            }
                                            str3 = string.Concat(str3, "from_quantity = ", Objets.StrClean(businessObject.Fields.Item("Amount").Value.ToString()));
                                            flag5 = true;
                                        }
                                        if (Operators.CompareString(Objets.StrClean(businessObject.Fields.Item("Discount").Value.ToString().Replace(",", ".")), Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["reduction"]), false)), false) != 0)
                                        {
                                            if (flag5)
                                            {
                                                str3 = string.Concat(str3, ", ");
                                            }
                                            str3 = string.Concat(str3, "reduction = ", businessObject.Fields.Item("Discount").Value.ToString().Replace(",", "."));
                                            flag5 = true;
                                        }
                                        if (Operators.CompareString(Objets.Date2SQL(Conversions.ToDate(businessObject.Fields.Item("FromDate").Value), false), Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["from"]), false)), false) != 0)
                                        {
                                            if (flag5)
                                            {
                                                str3 = string.Concat(str3, ", ");
                                            }
                                            str3 = string.Concat(str3, "`from` = '", Objets.Date2SQL(Conversions.ToDate(businessObject.Fields.Item("FromDate").Value), false), "'");
                                            flag5 = true;
                                        }
                                        if (Operators.CompareString(Objets.Date2SQL(Conversions.ToDate(businessObject.Fields.Item("ToDate").Value), false), Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["to"]), false)), false) != 0)
                                        {
                                            if (flag5)
                                            {
                                                str3 = string.Concat(str3, ", ");
                                            }
                                            str3 = string.Concat(str3, "`to` = '", Objets.Date2SQL(Conversions.ToDate(businessObject.Fields.Item("ToDate").Value), false), "'");
                                            flag5 = true;
                                        }
                                        if (!flag5)
                                        {
                                            break;
                                        }
                                        str3 = string.Concat(str3, ", state = 1 WHERE id_specific_price_extern='", businessObject.Fields.Item("IDExt").Value.ToString(), "';");
                                        this.PushMySqlQuery(str3);
                                        if (!flag)
                                        {
                                            database = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", str7, ";" };
                                            this.PushMySqlQuery(string.Concat(database));
                                            flag = true;
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine(string.Concat(this.SiteName, " : CopyArticleSBOToPrestashop : Il y a plusieurs nom pour un article dans la base intermédiaire"), 1);
                                        return;
                                    }
                            }
                            businessObject.MoveNext();
                        }
                        Marshal.ReleaseComObject(businessObject);
                    }
                }
                else
                {
                    str3 = string.Concat("INSERT INTO ", this.tableMySQLArticles, " (id_product_extern, id_manufacturer, id_category_default, ");
                    database = new string[] { "VALUES ('", str6, "', ", str4, ", ", str5, ", " };
                    string str31 = string.Concat(database);
                    int j = 0;
                    string str32 = "";
                    while (Operators.ConditionalCompareObjectNotEqual(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", j.ToString()), "", null), "", false))
                    {
                        string str33 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", j.ToString()), "", null));
                        if (str33.Contains("product."))
                        {
                            chrArray = new char[] { '.' };
                            str33 = str33.Split(chrArray)[1];
                            string str34 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("1*", j.ToString()), "", null));
                            if (Operators.CompareString(str34, "Perso", false) != 0)
                            {
                                object obj9 = RSItemCode.Fields.Item(str34).Value.ToString();
                                str3 = string.Concat(str3, str33, ", ");
                                database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLArticles, "' AND COLUMN_NAME = '", str33, "';" };
                                str32 = string.Concat(database);
                                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str32, "'"), 6);
                                mySqlDataAdapter = new MySqlDataAdapter(str32, this.cnMySQLShema);
                                dataSet = new DataSet();
                                mySqlDataAdapter.Fill(dataSet, "datacolBI");
                                item = dataSet.Tables["datacolBI"];
                                item.CaseSensitive = false;
                                str31 = (!(item.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(item.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0) ? string.Concat(str31, "'", Objets.StrClean(obj9.ToString()), "', ") : string.Concat(str31, Objets.StrClean(obj9.ToString()), ", "));
                            }
                            else
                            {
                                string str35 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("2*", j.ToString()), "", null));
                                string val3 = this.PersoToVal(str35, str6);
                                if (Operators.CompareString(val3, "", false) != 0)
                                {
                                    str3 = string.Concat(str3, str33, ", ");
                                    database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLArticles, "' AND COLUMN_NAME = '", str33, "';" };
                                    str32 = string.Concat(database);
                                    Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str32, "'"), 6);
                                    mySqlDataAdapter = new MySqlDataAdapter(str32, this.cnMySQLShema);
                                    dataSet = new DataSet();
                                    mySqlDataAdapter.Fill(dataSet, "datacolBI");
                                    item = dataSet.Tables["datacolBI"];
                                    item.CaseSensitive = false;
                                    str31 = (!(item.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(item.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0) ? string.Concat(str31, "'", Objets.StrClean(val3), "', ") : string.Concat(str31, Objets.StrClean(val3.Replace(",", ".")), ", "));
                                }
                            }
                        }
                        j = checked(j + 1);
                    }
                    str3 = string.Concat(str3, "active, online_only, state)");
                    database = new string[] { str31, str1, ", ", str2, ", 1)" };
                    str31 = string.Concat(database);
                    str3 = string.Concat(str3, " ", str31, ";");
                    this.PushMySqlQuery(str3);
                    this.FlushMySqlQuery();
                    database = new string[] { "SELECT id_product FROM ", this.tableMySQLArticles, " WHERE id_product_extern = '", str6, "';" };
                    str32 = string.Concat(database);
                    Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str32, "'"), 6);
                    mySqlDataAdapter = new MySqlDataAdapter(str32, this.cnMySQL);
                    dataSet = new DataSet();
                    mySqlDataAdapter.Fill(dataSet, "productID");
                    item = dataSet.Tables["productID"];
                    item.CaseSensitive = false;
                    str7 = Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["id_product"]), false);
                    str3 = string.Concat("INSERT INTO ", this.tableMySQLNomsArticles, " (id_product, id_lang, ");
                    database = new string[] { "VALUES (", str7, ", (SELECT id_lang FROM ", this.tableMySQLLangues, " WHERE iso_code = 'FR'), " };
                    str31 = string.Concat(database);
                    for (j = 0; Operators.ConditionalCompareObjectNotEqual(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", j.ToString()), "", null), "", false); j = checked(j + 1))
                    {
                        string str36 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("0*", j.ToString()), "", null));
                        if (str36.Contains("product_lang."))
                        {
                            chrArray = new char[] { '.' };
                            str36 = str36.Split(chrArray)[1];
                            string str37 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("1*", j.ToString()), "", null));
                            if (Operators.CompareString(str37, "Perso", false) != 0)
                            {
                                object obj10 = RSItemCode.Fields.Item(str37).Value.ToString();
                                str3 = string.Concat(str3, str36, ", ");
                                database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLNomsArticles, "' AND COLUMN_NAME = '", str36, "';" };
                                str32 = string.Concat(database);
                                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str32, "'"), 6);
                                mySqlDataAdapter = new MySqlDataAdapter(str32, this.cnMySQLShema);
                                dataSet = new DataSet();
                                mySqlDataAdapter.Fill(dataSet, "datacolBI");
                                item = dataSet.Tables["datacolBI"];
                                item.CaseSensitive = false;
                                str31 = (!(item.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(item.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0) ? string.Concat(str31, "'", Objets.StrClean(obj10.ToString()), "', ") : string.Concat(str31, Objets.StrClean(obj10.ToString()), ", "));
                            }
                            else
                            {
                                string str38 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Article\\Map", string.Concat("2*", j.ToString()), "", null));
                                string val4 = this.PersoToVal(str38, str6);
                                if (Operators.CompareString(val4, "", false) != 0)
                                {
                                    str3 = string.Concat(str3, str36, ", ");
                                    database = new string[] { "SELECT * FROM `COLUMNS` C WHERE TABLE_SCHEMA = '", this.cnMySQL.Database, "' AND TABLE_NAME = '", this.tableMySQLNomsArticles, "' AND COLUMN_NAME = '", str36, "';" };
                                    str32 = string.Concat(database);
                                    Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str32, "'"), 6);
                                    mySqlDataAdapter = new MySqlDataAdapter(str32, this.cnMySQLShema);
                                    dataSet = new DataSet();
                                    mySqlDataAdapter.Fill(dataSet, "datacolBI");
                                    item = dataSet.Tables["datacolBI"];
                                    item.CaseSensitive = false;
                                    str31 = (!(item.Rows[0]["DATA_TYPE"].ToString().Contains("int") | Operators.CompareString(item.Rows[0]["DATA_TYPE"].ToString(), "decimal", false) == 0) ? string.Concat(str31, "'", Objets.StrClean(val4), "', ") : string.Concat(str31, Objets.StrClean(val4), ", "));
                                }
                            }
                        }
                    }
                    str3 = string.Concat(Strings.Left(str3, checked(str3.Length - 2)), ") ", Strings.Left(str31, checked(str31.Length - 2)), ");");
                    this.PushMySqlQuery(str3);
                    bool flag6 = true;
                    this.SynchroCaracteristiques(str7,  RSItemCode,  flag6);
                    j = 1;
                    do
                    {
                        object obj11 = RSItemCode.Fields.Item(string.Concat("U_MG2S_Image", j.ToString())).Value.ToString();
                        if (!Operators.ConditionalCompareObjectNotEqual(obj11, "", false))
                        {
                            break;
                        }
                        this.PushMySqlQuery(Conversions.ToString(Operators.AddObject(Operators.AddObject(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("INSERT INTO ", this.tableMySQLImagesArticles), "(id_product, url, position, cover) VALUES ("), str7), ", '"), this.pathimg), Objets.StrClean(Conversions.ToString(obj11))), "', "), j.ToString()), ", "), Interaction.IIf(j == 1, "1", "0")), ");")));
                        string str39 = Conversions.ToString(Operators.AddObject(this.cheminSAPImages, obj11));
                        if (File.Exists(str39) && DateTime.Compare(File.GetLastWriteTime(str39), this.GetDateOfFile(  this.FTP_ListeImages, Path.GetFileName(str39))) > 0)
                        {
                            database = new string[] { "FTP Upload ", str39, " -> ", this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str39) };
                            Console.WriteLine(string.Concat(database), 5);
                            this.cnFTP.Upload(str39, string.Concat(this.cnFTP.CurrentDirectory, "/images/", Path.GetFileName(str39)));
                        }
                        j = checked(j + 1);
                    }
                    while (j <= 6);
                    SAPbobsCOM.Recordset recordset = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                    database = new string[] { "SELECT v1.*, T0.DocEntry FROM (SELECT T0.ItemCode, T0.CardCode, T0.ItemCode + '/' + T0.CardCode + '/-1/-1' AS 'IDExt', T0.Discount AS 'Discount', T0.Price AS 'Price', T0.Currency AS 'Currency', '99991231' AS 'FromDate', '99991231' AS 'ToDate', NULL AS 'Amount' FROM OSPP T0 WHERE T0.ListNum = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", null, null).ToString(), " AND T0.CardCode <> '*2' AND T0.ItemCode = '", str6, "' UNION ALL SELECT T0.ItemCode, T0.CardCode, T0.ItemCode + '/' + T0.CardCode + '/' + CONVERT(VARCHAR(10),T1.LINENUM) + '/-1' AS 'IDExt', T1.Discount AS 'Discount', T1.Price AS 'Price', T1.Currency AS 'Currency', T1.FromDate, CASE WHEN ISNULL(T1.ToDate, '') = '' THEN '99991231' ELSE T1.ToDate END AS 'ToDate', NULL AS 'Amount' FROM OSPP T0 INNER JOIN SPP1 T1 ON T1.CardCode = T0.CardCode AND T1.ItemCode = T0.ItemCode WHERE T0.ListNum = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", null, null).ToString(), " AND T0.CardCode <> '*2' AND T0.ItemCode = '", str6, "' UNION ALL SELECT T0.ItemCode, T0.CardCode, T0.ItemCode + '/' + T0.CardCode + '/' + CONVERT(VARCHAR(10),T1.LINENUM) + '/' + CONVERT(VARCHAR(10),T2.SPP2LNum) AS 'IDExt', T2.Discount AS 'Discount', T2.Price AS 'Price', T2.Currency AS 'Currency', T1.FromDate, CASE WHEN ISNULL(T1.ToDate, '') = '' THEN '99991231' ELSE T1.ToDate END AS 'ToDate', T2.Amount FROM OSPP T0 INNER JOIN SPP1 T1 ON T1.CardCode = T0.CardCode AND T1.ItemCode = T0.ItemCode INNER JOIN SPP2 T2 ON T2.CardCode = T1.CardCode AND T2.ItemCode = T1.ItemCode AND T2.SPP1LNum = T1.LINENUM WHERE T0.ListNum = ", Objets.GlobalGetSetting("B1-Prestashop", "Config", "ListePrix", null, null).ToString(), " AND T0.CardCode <> '*2' AND T0.ItemCode = '", str6, "') v1 INNER JOIN OCRD T0 ON T0.CardCode = v1.CardCode WHERE (T0.E_Mail IS NOT NULL AND (SELECT COUNT(E_Mail) AS 'NbEmail' FROM OCRD WHERE E_Mail = T0.E_Mail AND U_MG2S_Valid = 'Y') = 1) AND T0.CardName IS NOT NULL AND T0.U_MG2S_Valid = 'Y' AND (T0.UpdateDate >= T0.U_MG2S_DateSync OR T0.U_MG2S_DateSync IS NULL)" };
                    str3 = string.Concat(database);
                    Console.WriteLine(string.Concat(str, " : Execution DI de la requête -> '", str3, "'"), 6);
                    recordset.DoQuery(str3);
                    while (!recordset.EoF)
                    {
                        database = new string[] { "SELECT p.id_product, c.id_customer FROM ", this.tableMySQLArticles, " p, ", this.tableMySQLClients, " c WHERE id_product_extern = '", recordset.Fields.Item("ItemCode").Value.ToString(), "' AND id_customer_extern = '", recordset.Fields.Item("DocEntry").Value.ToString(), "';" };
                        str3 = string.Concat(database);
                        Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                        mySqlDataAdapter = new MySqlDataAdapter(str3, this.cnMySQL);
                        dataSet = new DataSet();
                        mySqlDataAdapter.Fill(dataSet, "pc");
                        item = dataSet.Tables["pc"];
                        item.CaseSensitive = false;
                        database = new string[] { "INSERT INTO ", this.tableMySQLPrixSpecifiques, "(id_specific_price_extern, id_customer, id_product, price, from_quantity, reduction, reduction_type, `from`, `to`, state) VALUES ('", recordset.Fields.Item("IDExt").Value.ToString(), "', ", Objets.StrClean(Conversions.ToString(item.Rows[0]["id_customer"])).Trim(), ", ", Objets.StrClean(Conversions.ToString(item.Rows[0]["id_product"])).Trim(), ", ", recordset.Fields.Item("Price").Value.ToString().Replace(",", "."), ", ", Objets.StrClean(recordset.Fields.Item("Amount").Value.ToString()), ", ", recordset.Fields.Item("Discount").Value.ToString().Replace(",", "."), ", 'percentage', '", Objets.Date2SQL(Conversions.ToDate(recordset.Fields.Item("FromDate").Value), false), "', '", Objets.Date2SQL(Conversions.ToDate(recordset.Fields.Item("ToDate").Value), false), "', 1);" };
                        this.PushMySqlQuery(string.Concat(database));
                        recordset.MoveNext();
                    }
                    Marshal.ReleaseComObject(recordset);
                }
                this.FlushMySqlQuery();
                if (flag & Operators.CompareString(str7, "", false) != 0 | Operators.CompareString(str7, "", false) == 0)
                {
                    this.UpdateArticleSBO(str6);
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                database = new string[] { this.SiteName, " : CopyArticleSBOToPrestaShop --> ", exception.Message, "\r\n", exception.StackTrace };
                Console.WriteLine(string.Concat(database), 1);
                ProjectData.ClearProjectError();
            }
        }
        private void SynchroCaracteristiqueBaseIntermediaire(string nomChampsPrestaShop, string id_productPrestaShop, string valeurSBO,  bool modifP)
        {
            try
            {
                string[] idProductPrestaShop = new string[] { "SELECT pf.id_product, f.id_feature, f.id_feature_extern, fl.name, fl.value FROM ", this.tableMySQLCaracs, " f INNER JOIN ", this.tableMySQLCaracsArticles, " pf ON pf.id_feature = f.id_feature AND pf.id_product = ", id_productPrestaShop, " INNER JOIN ", this.tableMySQLCaracsLangues, " fl ON fl.id_feature = f.id_feature AND fl.id_lang = (SELECT id_lang FROM ", this.tableMySQLLangues, " WHERE iso_code = 'FR') AND fl.name = '", Objets.StrClean(nomChampsPrestaShop), "' AND fl.value = '", Objets.StrClean(valeurSBO), "';" };
                string str = string.Concat(idProductPrestaShop);
                Console.WriteLine(string.Concat("SynchroCaracteristiqueBaseIntermediaire : Execution MySQL de la requête -> '", str, "'"), 6);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(str, this.cnMySQL);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "table");
                DataTable item = dataSet.Tables["table"];
                item.CaseSensitive = false;
                switch (item.Rows.Count)
                {
                    case 0:
                        {
                            idProductPrestaShop = new string[] { "SELECT f.id_feature FROM ", this.tableMySQLCaracs, " f INNER JOIN ", this.tableMySQLCaracsLangues, " fl ON fl.id_feature = f.id_feature AND fl.id_lang = (SELECT id_lang FROM ", this.tableMySQLLangues, " WHERE iso_code = 'FR') WHERE fl.name = '", Objets.StrClean(nomChampsPrestaShop), "' AND fl.value = '", Objets.StrClean(valeurSBO), "';" };
                            str = string.Concat(idProductPrestaShop);
                            Console.WriteLine(string.Concat("SynchroCaracteristiqueBaseIntermediaire : Execution MySQL de la requête -> '", str, "'"), 6);
                            MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter(str, this.cnMySQL);
                            DataSet dataSet1 = new DataSet();
                            mySqlDataAdapter1.Fill(dataSet1, "table");
                            DataTable dataTable = dataSet1.Tables["table"];
                            dataTable.CaseSensitive = false;
                            if (dataTable.Rows.Count != 1)
                            {
                                idProductPrestaShop = new string[] { "INSERT INTO ", this.tableMySQLCaracs, "(id_feature_extern, state) VALUES ('", Objets.StrClean(nomChampsPrestaShop), "/", Objets.StrClean(valeurSBO), "', 1);" };
                                str = string.Concat(idProductPrestaShop);
                                this.PushMySqlQuery(str);
                                this.FlushMySqlQuery();
                                idProductPrestaShop = new string[] { "SELECT id_feature FROM ", this.tableMySQLCaracs, " WHERE id_feature_extern = '", Objets.StrClean(nomChampsPrestaShop), "/", Objets.StrClean(valeurSBO), "';" };
                                str = string.Concat(idProductPrestaShop);
                                Console.WriteLine(string.Concat("SynchroCaracteristiqueBaseIntermediaire : Execution MySQL de la requête -> '", str, "'"), 6);
                                mySqlDataAdapter1 = new MySqlDataAdapter(str, this.cnMySQL);
                                dataSet1 = new DataSet();
                                mySqlDataAdapter1.Fill(dataSet1, "idf");
                                dataTable = dataSet1.Tables["idf"];
                                dataTable.CaseSensitive = false;
                                idProductPrestaShop = new string[] { "INSERT INTO ", this.tableMySQLCaracsLangues, "(id_feature, id_lang, name, value) VALUES (", Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["id_feature"]), false).Trim()), ", (SELECT id_lang FROM ", this.tableMySQLLangues, " WHERE iso_code = 'FR'), '", Objets.StrClean(nomChampsPrestaShop), "', '", Objets.StrClean(valeurSBO), "');" };
                                this.PushMySqlQuery(string.Concat(idProductPrestaShop));
                                idProductPrestaShop = new string[] { "INSERT INTO ", this.tableMySQLCaracsArticles, "(id_product, id_feature) VALUES (", Objets.StrClean(id_productPrestaShop), ", ", Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["id_feature"]), false).Trim()), ");" };
                                this.PushMySqlQuery(string.Concat(idProductPrestaShop));
                                if (!modifP)
                                {
                                    idProductPrestaShop = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", Objets.StrClean(id_productPrestaShop), ";" };
                                    this.PushMySqlQuery(string.Concat(idProductPrestaShop));
                                    modifP = true;
                                }
                            }
                            else
                            {
                                idProductPrestaShop = new string[] { "INSERT INTO ", this.tableMySQLCaracsArticles, "(id_product, id_feature) VALUES(", Objets.StrClean(id_productPrestaShop), ", ", Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["id_feature"]), false).Trim()), ");" };
                                str = string.Concat(idProductPrestaShop);
                                this.PushMySqlQuery(str);
                                if (!modifP)
                                {
                                    idProductPrestaShop = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state=1 WHERE id_product=", Objets.StrClean(id_productPrestaShop), ";" };
                                    str = string.Concat(idProductPrestaShop);
                                    this.PushMySqlQuery(str);
                                    modifP = true;
                                    return;
                                }
                            }
                            return;
                        }
                    case 1:
                        {
                            if (Operators.CompareString(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["value"]), false).Trim(), valeurSBO, false) == 0)
                            {
                                return;
                            }
                            else
                            {
                                idProductPrestaShop = new string[] { "UPDATE ", this.tableMySQLCaracsLangues, " SET value = '", Objets.StrClean(valeurSBO), "' WHERE id_feature = ", Objets.StrClean(Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[0]["id_feature"]), false).Trim()), " AND name = '", Objets.StrClean(nomChampsPrestaShop), "';" };
                                str = string.Concat(idProductPrestaShop);
                                this.PushMySqlQuery(str);
                                if (!modifP)
                                {
                                    idProductPrestaShop = new string[] { "UPDATE ", this.tableMySQLArticles, " SET state = 1 WHERE id_product = ", Objets.StrClean(id_productPrestaShop), ";" };
                                    str = string.Concat(idProductPrestaShop);
                                    this.PushMySqlQuery(str);
                                    modifP = true;
                                }
                                return;
                            }
                        }
                }
               // Console.WriteLine(string.Concat("SynchroCaracteristiqueBaseIntermediaire : il y a plusieurs duo caractéristique/valeur pour l' article avec l'idBI '", id_productPrestaShop, "'"), 1);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Console.WriteLine(string.Concat(this.SiteName, " : SynchroCaractéristiqueBaseIntermediaire : ", exception.Message), 1);
                ProjectData.ClearProjectError();
            }
        }
        private void SynchroCaracteristiques(string id_product, SAPbobsCOM.Recordset RSItemCode,  bool modifP)
        {
            string[] idProduct;
            string str = "SynchroCaracteristiques";
            string str1 = "";
            this.tableMySQLCaracsArticles = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Carac", "TableMySQLArticles", "product_feature", null));
            this.tableMySQLCaracs = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Carac", "TableMySQLCaracs", "feature", null));
            this.tableMySQLCaracsLangues = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Carac", "TableMySQLCaracsLangues", "feature_lang", null));
            try
            {
                string str2 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "ZUChecked", "", null));
                string[] strArrays = str2.Split(new char[] { '*' });
               // int j = 0;
                
                string[] strArrays1 = strArrays;
                
                for (int i = 0; i < checked((int)strArrays1.Length); i = checked(i + 1))
                {
                    
                        
                        string str3 = strArrays1[i];
                        if (Operators.CompareString(str3, "", false) != 0)
                        {
                            string str4 = str3;
                            if (Operators.CompareString(str4, "SAPGrpArt", false) == 0)
                            {
                                this.SynchroCaracteristiqueBaseIntermediaire("Groupe Article", id_product, RSItemCode.Fields.Item("ItmsGrpNam").Value.ToString(), modifP);
                            }
                            else if (Operators.CompareString(str4, "SAPModèle", false) == 0)
                            {
                                this.SynchroCaracteristiqueBaseIntermediaire("Modèle", id_product, RSItemCode.Fields.Item("SWW").Value.ToString(), modifP);
                            }
                            else if (Operators.CompareString(str4, "SAPMarque", false) != 0)
                            {
                                string str5 = Conversions.ToString(RSItemCode.Fields.Item(string.Concat("U_", str3)).Value);
                                idProduct = new string[] { "SELECT TZU.Descr, TZU.TypeID, TZU.EditType, TZU.AliasID, VV.Descr AS 'ValidValueDescr' FROM CUFD TZU LEFT JOIN UFD1 VV ON VV.TableID = TZU.TableID AND VV.FieldID = TZU.FieldID AND VV.FldValue = '", Objets.StrClean(str5), "' WHERE TZU.TableID = 'OITM' AND TZU.AliasID = '", Objets.StrClean(str3), "'" };
                                string str6 = string.Concat(idProduct);
                                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                                Console.WriteLine(string.Concat(str, " : Execution DI de la requête -> '", str6, "'"), 6);
                                businessObject.DoQuery(str6);
                                if (!businessObject.EoF)
                                {
                                    string str7 = businessObject.Fields.Item("TypeID").Value.ToString();
                                    string str8 = businessObject.Fields.Item("Descr").Value.ToString();
                                    string str9 = businessObject.Fields.Item("ValidValueDescr").Value.ToString();
                                    if (Operators.CompareString(str7, "D", false) == 0)
                                    {
                                        if (Operators.CompareString(str5, null, false) != 0)
                                        {
                                            string shortDateString = "";
                                            if (!(Operators.CompareString(str5.ToString(), null, false) == 0 | Operators.CompareString(str5.ToString(), "01/01/1900 00:00:00", false) == 0))
                                            {
                                                shortDateString = Conversions.ToDate(str5).ToShortDateString();
                                                if (Operators.CompareString(shortDateString, "01/01/0001", false) == 0)
                                                {
                                                    shortDateString = "NULL";
                                                }
                                            }
                                            else
                                            {
                                                shortDateString = "NULL";
                                            }
                                            this.SynchroCaracteristiqueBaseIntermediaire(str8, id_product, shortDateString, modifP);
                                        }
                                    }
                                    else if (Operators.CompareString(str5.ToString(), null, false) != 0)
                                    {
                                        if (Operators.CompareString(str9, "", false) != 0)
                                        {
                                            this.SynchroCaracteristiqueBaseIntermediaire(str8, id_product, str9, modifP);
                                        }
                                        else
                                        {
                                            this.SynchroCaracteristiqueBaseIntermediaire(str8, id_product, str5.ToString(), modifP);
                                        }
                                    }
                                    Marshal.ReleaseComObject(businessObject);
                                }
                                else
                                {
                                    Console.WriteLine(string.Concat("SynchroCaracteristiques : Le champs U_", str3, " n'existe pas."), 1);
                                    Marshal.ReleaseComObject(businessObject);
                                    return;
                                }
                            }
                            else
                            {
                                SAPbobsCOM.Recordset recordset = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                                try
                                {
                                    str1 = string.Concat("SELECT Firmname FROM OMRC WHERE FirmCode = ", RSItemCode.Fields.Item("FirmCode").Value.ToString());
                                    Console.WriteLine(string.Concat(str, " : Execution DI de la requête -> '", str1, "'"), 6);
                                    recordset.DoQuery(str1);
                                    this.SynchroCaracteristiqueBaseIntermediaire("Marque", id_product, recordset.Fields.Item("FirmName").Value.ToString(), modifP);
                                    Marshal.ReleaseComObject(recordset);
                                }
                                catch (Exception exception)
                                {
                                    ProjectData.SetProjectError(exception);
                                    Marshal.ReleaseComObject(recordset);
                                    Console.WriteLine("SynchroCaractéristiques : Erreur lors de la synchronisation de la marque.", 1);
                                    ProjectData.ClearProjectError();
                                    return;
                                }
                            }
                        }
                    
                }
                idProduct = new string[] { "SELECT p.id_product, f.id_feature, f.id_feature_extern FROM ", this.tableMySQLCaracsArticles, " p INNER JOIN ", this.tableMySQLCaracs, " f ON f.id_feature = p.id_feature WHERE p.id_product = ", id_product, ";" };
                str1 = string.Concat(idProduct);
                Console.WriteLine(string.Concat(str, " : Execution MySQL de la requête -> '", str1, "'"), 6);
                object mySqlDataAdapter = new MySqlDataAdapter(str1, this.cnMySQL);
                DataSet dataSet = new DataSet();
                object[] objArray = new object[] { dataSet, "pf" };
                bool[] flagArray = new bool[] { true, false };
                NewLateBinding.LateCall(mySqlDataAdapter, null, "Fill", objArray, null, null, flagArray, true);
                if (flagArray[0])
                {
                    dataSet = (DataSet)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray[0]), typeof(DataSet));
                }
                DataTable item = dataSet.Tables["pf"];
                item.CaseSensitive = false;
                int count = checked(item.Rows.Count - 1);
              
                for (int j = 0;  j <= count;  j = checked( j + 1))
                {
                    bool flag = false;
                    string[] strArrays2 = strArrays;
                    for (int k = 0; k < checked((int)strArrays2.Length); k = checked(k + 1))
                    {
                        string str10 = strArrays2[k];
                        Console.WriteLine("str10 " + str10);
                        if (Operators.CompareString(str10, "", false) != 0)
                        {
                            string str11 = "";
                            string str12 = str10;
                            if (Operators.CompareString(str12, "SAPGrpArt", false) == 0)
                            {
                                str11 = Conversions.ToString(RSItemCode.Fields.Item("ItmsGrpNam").Value);
                                if (item.Rows[j]["id_feature_extern"].ToString().Contains("Groupe Article") & item.Rows[j]["id_feature_extern"].ToString().Contains(str11))
                                {
                                    flag = true;
                                }
                            }
                            else if (Operators.CompareString(str12, "SAPModèle", false) == 0)
                            {
                                str11 = Conversions.ToString(RSItemCode.Fields.Item("SWW").Value);
                                if (item.Rows[j]["id_feature_extern"].ToString().Contains("Modèle") & item.Rows[j]["id_feature_extern"].ToString().Contains(str11))
                                {
                                    flag = true;
                                }
                            }
                            else if (Operators.CompareString(str12, "SAPMarque", false) != 0)
                            {
                                str11 = Conversions.ToString(RSItemCode.Fields.Item(string.Concat("U_", str10)).Value);
                                idProduct = new string[] { "SELECT TZU.Descr, TZU.TypeID, TZU.EditType, TZU.AliasID, VV.Descr AS 'ValidValueDescr' FROM CUFD TZU LEFT JOIN UFD1 VV ON VV.TableID = TZU.TableID AND VV.FieldID = TZU.FieldID AND VV.FldValue = '", Objets.StrClean(str11), "' WHERE TZU.TableID = 'OITM' AND TZU.AliasID = '", Objets.StrClean(str10), "'" };
                                string str13 = string.Concat(idProduct);
                                SAPbobsCOM.Recordset businessObject1 = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                                Console.WriteLine(string.Concat(str, " : Execution DI de la requête -> '", str13, "'"), 6);
                                businessObject1.DoQuery(str13);
                                businessObject1.Fields.Item("TypeID").Value.ToString();
                                string str14 = businessObject1.Fields.Item("Descr").Value.ToString();
                                string str15 = businessObject1.Fields.Item("ValidValueDescr").Value.ToString();
                                if (!businessObject1.EoF)
                                {
                                    if (Operators.CompareString(str11, "", false) != 0)
                                    {
                                        if (item.Rows[j]["id_feature_extern"].ToString().Contains(str14) & item.Rows[j]["id_feature_extern"].ToString().Contains(str15))
                                        {
                                            flag = true;
                                        }
                                    }
                                    else if (item.Rows[j]["id_feature_extern"].ToString().Contains(str14) & item.Rows[j]["id_feature_extern"].ToString().Contains(str11))
                                    {
                                        flag = true;
                                    }
                                    Marshal.ReleaseComObject(businessObject1);
                                }
                                else
                                {
                                    Console.WriteLine(string.Concat(this.SiteName, " : SynchroCaracteristiques : Le champs U_", str10, " n'existe pas."), 1);
                                    Marshal.ReleaseComObject(businessObject1);
                                    return;
                                }
                            }
                            else
                            {
                                str11 = Conversions.ToString(RSItemCode.Fields.Item("FirmCode").Value);
                                if (item.Rows[j]["id_feature_extern"].ToString().Contains("Marque") & item.Rows[j]["id_feature_extern"].ToString().Contains(str11))
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                    if (!flag)
                    {
                        idProduct = new string[] { "DELETE FROM ", this.tableMySQLCaracsArticles, " WHERE id_product = ", Objets.StrClean(id_product), " AND id_feature = ", Objets.StrClean(item.Rows[j]["id_feature"].ToString()), ";" };
                        this.PushMySqlQuery(string.Concat(idProduct));
                        modifP = true;
                    }
                }
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                Exception exception1 = exception2;
                Console.WriteLine(string.Concat("SynchroCaracteristiques : ", exception1.Message), 1);
                ProjectData.ClearProjectError();
            }
        }
        private void UpdateArticleSBO(string itemCode)
        {
            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
            businessObject.DoQuery(string.Concat("UPDATE OITM SET U_MG2S_DateSync = CURRENT_TIMESTAMP, U_MG2S_Update = 'N' WHERE ItemCode = '", itemCode, "';"));
            Console.WriteLine(string.Concat("UPDATE OITM SET U_MG2S_DateSync = CURRENT_TIMESTAMP, U_MG2S_Update = 'N' WHERE ItemCode = '", itemCode, "';"), 6);
        }
        private string getCondPersoArticles()
        {
            string str = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "CondSynchroArticle", "", null));
            if (!str.Contains("@Stock"))
            {
                return str;
            }
            string str1 = "(SELECT SUM(OnHand)-SUM(IsCommited) AS 'QtyStock' FROM OITW WHERE (";
            string str2 = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "MagasinsChecked", "", null));
            string[] strArrays = str2.Split(new char[] { '*' });
            string[] strArrays1 = strArrays;
            for (int i = 0; i < checked((int)strArrays1.Length); i = checked(i + 1))
            {
                string str3 = strArrays1[i];
                str1 = (Operators.CompareString(str3, "", false) == 0 ? Strings.Left(str1, checked(str1.Length - 3)) : string.Concat(str1, " WhsCode = '", str3, "' OR"));
            }
            str1 = string.Concat(str1, ") AND ItemCode = art.ItemCode)");
            str = str.Replace("@Stock", str1);
            if (Operators.CompareString(str, "", false) == 0)
            {
                return "";
            }
            return string.Concat(" AND ", str);
        }
        public bool cryptageMdp()
        {
            bool flag;
            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {
                string str = "SELECT * FROM [@MG2S_SITES]";
                UserTable userTable = this.SBOComp.UserTables.Item("MG2S_SITES");
                businessObject.DoQuery(str);
                if (!businessObject.EoF)
                {
                    while (!businessObject.EoF)
                    {
                        if (userTable.GetByKey(Conversions.ToString(businessObject.Fields.Item("Code").Value)))
                        {
                            string str1 = Conversions.ToString(businessObject.Fields.Item("U_MYSQLPASS").Value);
                            if (!str1.Contains("s:"))
                            {
                                userTable.UserFields.Fields.Item("U_MYSQLPASS").Value = string.Concat("s:", Objets.StrCrypt(str1, Conversions.ToString(this.cleCrypt)));
                            }
                            str1 = Conversions.ToString(businessObject.Fields.Item("U_FTPPASS").Value);
                            if (!str1.Contains("s:"))
                            {
                                userTable.UserFields.Fields.Item("U_FTPPASS").Value = string.Concat("s:", Objets.StrCrypt(str1, Conversions.ToString(this.cleCrypt)));
                            }
                            this.Ret = userTable.Update();
                            if (this.Ret != 0)
                            {
                                Console.WriteLine(string.Concat("B1-Prestashop: ", businessObject.Fields.Item("Name").Value.ToString(), " + cryptageMdp: La mise à jour de la table utilisateur 'MG2S_SITES' a échoué"), 1);
                                break;
                            }
                        }
                        businessObject.MoveNext();
                    }
                }
                else
                {
                    Console.WriteLine("B1-Prestashop : Il n'y a pas de site paramétré dans votre société", 1);
                }
                Marshal.ReleaseComObject(businessObject);
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Marshal.ReleaseComObject(businessObject);
                Console.WriteLine(string.Concat("B1-Prestashop: cryptageMdp: ", exception.Message), 1);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }
        public string decryptPassword(string strPass)
        {
            if (!strPass.Contains("s:"))
            {
                return strPass;
            }
            return Objets.StrDecrypt(strPass.Replace("s:", ""), Conversions.ToString(this.cleCrypt), false);
        }
        public void RefeshMySQLdataBuffer(string pTable = "all")
        {
            MySqlDataAdapter mySqlDataAdapter;
            string[] strArrays = new string[] { "RefeshMySQLdataBuffer(\"", pTable, "\") : ", this.SiteName, " : Start" };
            Console.WriteLine(string.Concat(strArrays), 5);
            string str = pTable;
            if (Operators.CompareString(str, "products", false) == 0)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select products_id, products_model from products", this.cnMySQL);
                mySqlDataAdapter.Fill(this.dsOSbuffers, "products");
                this.TbOSproducts = this.dsOSbuffers.Tables["products"];
            }
            else if (Operators.CompareString(str, "products_options", false) == 0)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select products_options_id, products_options_name from products_options", this.cnMySQL);
                mySqlDataAdapter.Fill(this.dsOSbuffers, "products_options");
                this.TbOSproducts_options = this.dsOSbuffers.Tables["products_options"];
            }
            else if (Operators.CompareString(str, "products_options_values", false) == 0)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select products_options_values_id, products_options_values_name from products_options_values", this.cnMySQL);
                mySqlDataAdapter.Fill(this.dsOSbuffers, "products_options_values");
                this.TbOSproducts_options_values = this.dsOSbuffers.Tables["products_options_values"];
            }
            else if (Operators.CompareString(str, "countries", false) != 0)
            {
                if (Operators.CompareString(str, "all", false) != 0)
                {

                }
            }
            else
            {
                mySqlDataAdapter = new MySqlDataAdapter("select countries_id, countries_iso_code_2 from countries", this.cnMySQL);
                mySqlDataAdapter.Fill(this.dsOSbuffers, "countries");
                this.TbOScountries = this.dsOSbuffers.Tables["countries"];
            }
            strArrays = new string[] { "RefeshMySQLdataBuffer(\"", pTable, "\") : ", this.SiteName, " : Done" };
           // Console.WriteLine(string.Concat(strArrays), 5);
        }
        public bool QuickSendMail(string pDestinataire, string pSujet, string pMessage, bool pFormatHTML = false)
        {
            MailTools mailTool;
            mailTool = (!Conversions.ToBoolean(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPAuth", false, null)) ? new MailTools(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPexpe", pDestinataire, null)), pDestinataire, pSujet, pMessage, Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPServer", "", null)), "", "", pFormatHTML, null, false) : new MailTools(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPexpe", pDestinataire, null)), pDestinataire, pSujet, pMessage, Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPServer", "", null)), Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPLogin", "", null)), Objets.StrDecrypt(Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Config", "SMTPPassword", "", null)), Conversions.ToString(this.cleCrypt), false), pFormatHTML, null, false));
            return mailTool.Send(true);
        }
        private void SynchroFabricant()
        {
            string[] str;
            ProgressBar value;
            string str1 = "SynchroFabricant";
            string str2 = "";
            this.tableMySQLFabricants = Conversions.ToString(Objets.GlobalGetSetting("B1-Prestashop", "Fabricant", "TableMySQL", "manufacturer", null));
            try
            {
                Console.WriteLine(string.Concat(this.SiteName, " : ", str1, ": Start"), 3);
                string str3 = string.Concat("SELECT * FROM ", this.tableMySQLFabricants, " m;");
                Console.WriteLine(string.Concat(str1, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(str3, this.cnMySQL);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "manufacturerBI");
                DataTable item = dataSet.Tables["manufacturerBI"];
                item.CaseSensitive = false;
                str3 = "SELECT FirmCode, FirmName FROM OMRC";
                Console.WriteLine(string.Concat(str1, " : Execution DI de la requête -> '", str3, "'"), 6);
                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                businessObject.DoQuery(str3);
                if (this.pbEtape != null)
                {
                    this.pbEtape.Value = this.pbEtape.Minimum;
                    this.pbEtape.Maximum = checked(businessObject.RecordCount + item.Rows.Count);
                }
                SAPbobsCOM.Recordset recordset = (SAPbobsCOM.Recordset)this.SBOComp.GetBusinessObject(BoObjectTypes.BoRecordset);
                int count = checked(item.Rows.Count - 1);
                for (int i = 0; i <= count; i = checked(i + 1))
                {
                    str2 = Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[i]["id_manufacturer_extern"]), false).Trim();
                    if (Operators.CompareString(str2, "", false) != 0)
                    {
                        str3 = string.Concat("SELECT FirmCode FROM OMRC WHERE FirmCode = ", str2);
                        Console.WriteLine(string.Concat(str1, " : Execution DI de la requête -> '", str3, "'"), 6);
                        recordset.DoQuery(str3);
                        if (recordset.EoF)
                        {
                            str = new string[] { "DELETE FROM ", this.tableMySQLFabricants, " WHERE id_manufacturer = '", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[i]["id_manufacturer"]), false).Trim(), "';" };
                            this.PushMySqlQuery(string.Concat(str));
                        }
                    }
                    else
                    {
                        str = new string[] { "DELETE FROM ", this.tableMySQLFabricants, " WHERE id_manufacturer = '", Objets.GetStr(RuntimeHelpers.GetObjectValue(item.Rows[i]["id_manufacturer"]), false).Trim(), "';" };
                        this.PushMySqlQuery(string.Concat(str));
                    }
                    if (this.pbEtape != null)
                    {
                        value = this.pbEtape;
                        value.Value = checked(value.Value + 1);
                    }
                }
                Marshal.ReleaseComObject(recordset);
                this.FlushMySqlQuery();
                while (!businessObject.EoF)
                {
                    str = new string[] { "SELECT * FROM ", this.tableMySQLFabricants, " m WHERE m.id_manufacturer_extern = ", businessObject.Fields.Item("FirmCode").Value.ToString(), ";" };
                    str3 = string.Concat(str);
                    Console.WriteLine(string.Concat(str1, " : Execution MySQL de la requête -> '", str3, "'"), 6);
                    MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter(str3, this.cnMySQL);
                    DataSet dataSet1 = new DataSet();
                    mySqlDataAdapter1.Fill(dataSet1, "manufacturerBI");
                    DataTable dataTable = dataSet1.Tables["manufacturerBI"];
                    dataTable.CaseSensitive = false;
                    if (dataTable.Rows.Count != 1)
                    {
                        str = new string[] { "INSERT INTO ", this.tableMySQLFabricants, "(id_manufacturer_extern, name, state) VALUES ('", Objets.StrClean(businessObject.Fields.Item("FirmCode").Value.ToString()), "', '", Objets.StrClean(businessObject.Fields.Item("FirmName").Value.ToString()), "', 1);" };
                        this.PushMySqlQuery(string.Concat(str));
                    }
                    else if (Operators.CompareString(Objets.GetStr(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["name"]), false).Trim(), businessObject.Fields.Item("FirmName").Value.ToString(), false) != 0)
                    {
                        str = new string[] { "UPDATE ", this.tableMySQLFabricants, " SET id_manufacturer_extern = '", Objets.StrClean(businessObject.Fields.Item("FirmCode").Value.ToString()), "', name = '", Objets.StrClean(businessObject.Fields.Item("FirmName").Value.ToString()), "', state = 1 WHERE id_manufacturer = '", Objets.GetStr(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["id_manufacturer"]), false).Trim(), "';" };
                        this.PushMySqlQuery(string.Concat(str));
                    }
                    businessObject.MoveNext();
                    if (this.pbEtape == null)
                    {
                        continue;
                    }
                    value = this.pbEtape;
                    value.Value = checked(value.Value + 1);
                }
                this.FlushMySqlQuery();
                Marshal.ReleaseComObject(businessObject);
                Marshal.ReleaseComObject(recordset);
               // Console.WriteLine(string.Concat(this.SiteName, " : ", str1, ": Done"), 3);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                str = new string[] { this.SiteName, " : ", str1, ": Une erreur s'est produite : ", exception.Message, ", stacktrace=", exception.StackTrace };
                Console.WriteLine(string.Concat(str), 1);
                ProjectData.ClearProjectError();
            }
        }
        public void PushMySqlQuery(string pReqSql)
        {
            Console.WriteLine(string.Concat("Push MySQL de la requête -> '", pReqSql, "'"), 6);
            GlobalReqSqlBufferForPrestaShop = string.Concat(GlobalReqSqlBufferForPrestaShop, pReqSql);
            GlobalReqSqlBufferForPrestaShop_Nb = checked(GlobalReqSqlBufferForPrestaShop_Nb + 1);
            if (GlobalReqSqlBufferForPrestaShop_Nb >= ReqSqlBuffer_NbReqMax)
            {
                this.FlushMySqlQuery();
            }
        //this.FlushMySqlQuery();
    }
        private void FlushMySqlQuery()
        {
            try
            {
                if (Operators.CompareString(this.GlobalReqSqlBufferForPrestaShop, "", false) != 0)
                {
                    Console.WriteLine(string.Concat("Execution du FlushMySQL avec la requête '", this.GlobalReqSqlBufferForPrestaShop, "'"), 6);
                    (new MySqlCommand(this.GlobalReqSqlBufferForPrestaShop, this.cnMySQL)).ExecuteNonQuery();
                    this.GlobalReqSqlBufferForPrestaShop = "";
                    this.GlobalReqSqlBufferForPrestaShop_Nb = 0;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
               // Console.WriteLine(string.Concat("Erreur lors de l'execution d'une requete MySQL : ", exception.Message), 1);
                Console.WriteLine(this.GlobalReqSqlBufferForPrestaShop, 6);
                this.GlobalReqSqlBufferForPrestaShop = "";
                this.GlobalReqSqlBufferForPrestaShop_Nb = 0;
                ProjectData.ClearProjectError();
            }
        }
        public DateTime GetDateOfFile( FTPdirectory FTP_ListeFichiers, string pFileName)
        {
            long num = (long)0;
            while (num < (long)FTP_ListeFichiers.Count && Operators.CompareString(FTP_ListeFichiers[checked((int)num)].Filename, pFileName, false) != 0)
            {
                num = checked(num + (long)1);
            }
            if (num >= (long)FTP_ListeFichiers.Count || Operators.CompareString(FTP_ListeFichiers[checked((int)num)].Filename, pFileName, false) != 0)
            {
                return DateTime.MinValue;
            }
            return FTP_ListeFichiers[checked((int)num)].FileDateTime;
        }







        //
    }
}
