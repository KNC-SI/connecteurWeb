using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobCommand : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            
            if ("1" == ((string)Parametres.key.GetValue("active")))
            {
string NameCron = "attributs";
            Parametres.RunCommand(NameCron);
            Parametres.DerniereExecution("Command");
            }
        }
    }
}
