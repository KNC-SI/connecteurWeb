using Microsoft.Win32;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceProcess;
using MySql.Data.MySqlClient;
using MySql.Data;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using MG2S_SBOClass;
//using SAPbobsCOM;

namespace RunScriptOvh
{
    class Parametres
    {
       
        static public List<string> list = new List<string>() { "Minutes", "Heures" };
        static public List<jours> jours = new List<jours>();
        static public List<string> ValueKeyArticle = new List<string>();
        static public List<string> ValueKeyArticleKnco = new List<string>();
        static public List<string> ValueKeyAttributsCaracteristiques = new List<string>();
        static public List<string> ValueKeyClient = new List<string>();
        static public List<string> ValueKeyCommand = new List<string>();
        static public List<string> ValueKeyCopiePhotos = new List<string>();
        static public List<string> ValueKeyGamme = new List<string>();
        static public List<string> ValueKeyNettoyagePhotos = new List<string>();
        static public List<string> ValueKeyPhotos = new List<string>();
        static public List<string> ValueKeyPrix = new List<string>();
        static public List<string> ValueKeyStock = new List<string>();
        static public RegistryKey key = Registry.CurrentUser.CreateSubKey(@"C:\Users\choui\Desktop\test");
        static public SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
        static public int ReqSqlBuffer_NbReqMax = 300;
        static public string GlobalReqSqlBufferForPrestaShop = "";
        static public int GlobalReqSqlBufferForPrestaShop_Nb = 0;
       

        static public void startservice(String formload)
        {
            ServiceController service = new ServiceController("RunWindowsService");
            if (formload.Equals("formload"))
            {
                if ((service.Status.Equals(ServiceControllerStatus.Stopped)) ||

                (service.Status.Equals(ServiceControllerStatus.StopPending)))
                {
                    key.SetValue("active", "0");
                }
                else
                {
                    key.SetValue("active", "1");
                    
                }
            }
            else
            {
                if ((service.Status.Equals(ServiceControllerStatus.Stopped)) ||

               (service.Status.Equals(ServiceControllerStatus.StopPending)))
                {
                    key.SetValue("active", "1");

                    service.Start();

                }
                else
                {
                    key.SetValue("active", "0");
                    service.Stop();
                }
            }
            
           
        }
        static public void RunCommand(string NameCron)
        {
            using (var client = new SshClient("ssh.cluster006.hosting.ovh.net", "francoiszi", "Vhs67hjYa8om"))
            {
                client.Connect();
                client.RunCommand("/usr/local/php7.2/bin/php /homez.727/francoiszi/dev/connect/symfony/bin/console app:cron:" + NameCron);

                client.Disconnect();

            }
        }

        static public void deleteKey(List<string> list)
        {
            foreach (var item in list)
            {
                Parametres.key.DeleteValue(item);
            }
            
        }
        static public void DerniereExecution(String name)
        {
            DateTime now = DateTime.Now;
            Parametres.key.SetValue("min" + name, now.Minute.ToString());
            Parametres.key.SetValue("hour" + name, now.Hour.ToString());
            Parametres.key.SetValue("day" + name, now.Day.ToString());
            Parametres.key.SetValue("month" + name, now.Month.ToString());
            Parametres.key.SetValue("year" + name, now.Year.ToString());

        }
      static public int ConnectSqlSap(string serverName,string societe, string DbUserName, string DbPassword,string typeserver, string sapUserName, string sapPassword,string LicenseServer)
        {
            int connectionResult=1;

            //string typeserver
            
            try
            {
                //sql
                oCompany.Server = serverName;
                oCompany.CompanyDB = societe;
                oCompany.DbUserName = DbUserName;
                oCompany.DbPassword = DbPassword;
                oCompany.DbServerType = SBOTools.Str2ServerType(typeserver);
                //sap
                oCompany.UserName = sapUserName;
                oCompany.Password = sapPassword;
                oCompany.LicenseServer = LicenseServer;
                //oCompany.language = SAPbobsCOM.BoSuppLangs.ln_French;
                oCompany.UseTrusted = false;
                //oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                // Console.WriteLine("__"+oCompany.Connect());
                connectionResult = oCompany.Connect();
            }
            catch (Exception)
            {

                return connectionResult;
            }
            
            return connectionResult;
        }
        static public bool Openconnection(string serverName, string userName, string password,string dbName)
        {
            bool result = false;
            string conn = "server=" + serverName +
                       ";user=" + userName +
                       ";database=" + dbName +
                       ";port=3306"+
                       ";password='"+password+"';";
            MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                result = true;
                connection.Close();
            }
            catch(Exception)
            { 
                result = false;
            }

            return result;

        }
        static public void Closeconnection(MySqlConnection connection)
        {
           
            /*string conn = "server=" + serverName +
                       ";user=" + userName +
                       ";database=" + dbName +
                       ";port=3308" +
                       ";password='" + password + "';";
            MySqlConnection connection = new MySqlConnection(conn);*/
            connection.Close();

        }
        public static object GlobalGetSetting(string pAppName, string pSection, string pClee, object pValDefaut = null, string pRemoteMachine = null)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            if (pRemoteMachine != null)
            {
                localMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pRemoteMachine);
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S") == null)
            {
                if (pValDefaut == null)
                {
                    return "";
                }
                return pValDefaut;
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName) == null)
            {
                return pValDefaut;
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection) == null)
            {
                return pValDefaut;
            }
            return localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection).GetValue(pClee, RuntimeHelpers.GetObjectValue(pValDefaut));
        }
        //static public void FlushMySqlQuery()
        //{
        //    string conn = "server=51.210.4.17;user=karationcom0;database=karavanproduction;port=3306;password=789ssszfeg568d;";
        //    MySqlConnection connection = new MySqlConnection(conn);
        //    MySqlDataReader reader;
        //    try
        //    {
        //        if (Operators.CompareString(GlobalReqSqlBufferForPrestaShop, "", false) != 0)
        //        {
        //            Console.WriteLine(string.Concat("Execution du FlushMySQL avec la requête '", GlobalReqSqlBufferForPrestaShop, "'"), 6);

        //            (new MySqlCommand(GlobalReqSqlBufferForPrestaShop, connection)).ExecuteNonQuery();

        //            GlobalReqSqlBufferForPrestaShop = "";
        //            GlobalReqSqlBufferForPrestaShop_Nb = 0;
        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        Exception exception = exception1;
        //        Console.WriteLine(string.Concat("Erreur lors de l'execution d'une requete MySQL : ", exception.Message), 1);
        //        Console.WriteLine(GlobalReqSqlBufferForPrestaShop, 6);
        //        GlobalReqSqlBufferForPrestaShop = "";
        //        GlobalReqSqlBufferForPrestaShop_Nb = 0;
        //        ProjectData.ClearProjectError();
        //    }
        //}
        //static public void PushMySqlQuery(string pReqSql)
        //{
        //    Console.WriteLine(string.Concat("Push MySQL de la requête -> '", pReqSql, "'"), 6);
        //   GlobalReqSqlBufferForPrestaShop = string.Concat(GlobalReqSqlBufferForPrestaShop, pReqSql);
        //    GlobalReqSqlBufferForPrestaShop_Nb = checked(GlobalReqSqlBufferForPrestaShop_Nb + 1);
        //    if (GlobalReqSqlBufferForPrestaShop_Nb >= ReqSqlBuffer_NbReqMax)
        //    {
        //        FlushMySqlQuery();
        //    }
        //}
        //static public string getCondPersoArticles()
        //{
        //    string str = Conversions.ToString(GlobalGetSetting("B1-Prestashop", "Config", "CondSynchroArticle", "", null));
        //    if (!str.Contains("@Stock"))
        //    {
        //        return str;
        //    }
        //    string str1 = "(SELECT SUM(OnHand)-SUM(IsCommited) AS 'QtyStock' FROM OITW WHERE (";
        //    string str2 = Conversions.ToString(GlobalGetSetting("B1-Prestashop", "Config", "MagasinsChecked", "", null));
        //    string[] strArrays = str2.Split(new char[] { '*' });
        //    string[] strArrays1 = strArrays;
        //    for (int i = 0; i < checked((int)strArrays1.Length); i = checked(i + 1))
        //    {
        //        string str3 = strArrays1[i];
        //        str1 = (Operators.CompareString(str3, "", false) == 0 ? Strings.Left(str1, checked(str1.Length - 3)) : string.Concat(str1, " WhsCode = '", str3, "' OR"));
        //    }
        //    str1 = string.Concat(str1, ") AND ItemCode = art.ItemCode)");
        //    str = str.Replace("@Stock", str1);
        //    if (Operators.CompareString(str, "", false) == 0)
        //    {
        //        return "";
        //    }
        //    return string.Concat(" AND ", str);
        //}

    }
}
