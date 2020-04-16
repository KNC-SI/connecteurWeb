using Microsoft.Win32;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        static public RegistryKey key = Registry.CurrentUser.CreateSubKey(@"C:\Users\choui\Desktop\test");
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
    }
}
