using Quartz;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobArticles : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string NameCron = "articles";
           Parametres.RunCommand(NameCron);
           Parametres.DerniereExecution("Article");
            Console.WriteLine("job");



        }
    }
}
