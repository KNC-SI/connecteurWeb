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

namespace RunScriptOvh
{
    class Parametres
    {
        static public List<string> list = new List<string>() { "Minutes", "Heures" };
        static public List<jours> jours = new List<jours>();
        static public List<string> ValueKeyArticle = new List<string>();
        static public List<string> ValueKeyAttributsCaracteristiques = new List<string>();
        static public List<string> ValueKeyClient = new List<string>();
        static public List<string> ValueKeyCommand = new List<string>();
        static public List<string> ValueKeyCopiePhotos = new List<string>();
        static public List<string> ValueKeyGamme = new List<string>();
        static public List<string> ValueKeyNettoyagePhotos = new List<string>();
        static public List<string> ValueKeyPhotos = new List<string>();
        static public List<string> ValueKeyPrix = new List<string>();
        static public List<string> ValueKeyStock = new List<string>();
        static public RegistryKey key = Registry.CurrentUser.CreateSubKey(@"C:\Users\DIRSAP\Desktop\test");
       // static public ServiceGet.HelloServiceClient client = new ServiceGet.HelloServiceClient();

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
       static public bool check_connections(string serverName, string userName, string password,string dbName)

        {

            bool result = false;
            //string serverName = address; // Address server (for local database "localhost")
            //string userName = name;  // user name
            //string dbName = basename; //Name database
            //string port = "3306"; // Port for connection
            //string password = password; // Password for connection 
            string conn = "server=" + serverName +
                       ";user=" + userName +
                       ";database=" + dbName +
                       ";port=3308"+
                       ";password='"+password+"';";
            //string conn = "Server = "+ serverName+ "; UserId = " + userName + "; Password = " + password + "; Database ="+ dbName;

            MySqlConnection connection = new MySqlConnection(conn);

            try

            {
                

                connection.Open();

                result = true;

                connection.Close();

            }

            catch(Exception ex)

            {

                Console.WriteLine(ex.Message);
                
                result = false;

            }

            return result;

        }

    }
}
