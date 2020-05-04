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
    class JobArticlesKnco : IJob
    {
        B1Prestashop_Tools b1Prestashop_Tools = new B1Prestashop_Tools();
       
        public async Task Execute(IJobExecutionContext context)
        {
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
                
                b1Prestashop_Tools.doSynchroArticles(false);
            }


        }

    }
}