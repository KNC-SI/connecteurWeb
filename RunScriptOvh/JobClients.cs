using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobClients : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string NameCron = "attributs";
            Parametres.RunCommand(NameCron);
            //async
        }
    }
}
