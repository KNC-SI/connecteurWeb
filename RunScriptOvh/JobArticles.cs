using MySql.Data.MySqlClient;
using Quartz;
using Renci.SshNet;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobArticles : IJob
    {
        private SAPbobsCOM.Company company;

        public async Task Execute(IJobExecutionContext context)
        {
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
                Console.WriteLine("testmyjob");
                /*string NameCron = "articles";
                Parametres.RunCommand(NameCron);
                Parametres.DerniereExecution(NameCron);*/
            }
            //this.SynchroArticles();
        }
       
    }
}
