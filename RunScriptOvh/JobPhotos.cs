using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobPhotos : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string NameCron = "photos";
            Parametres.RunCommand(NameCron);
            Parametres.DerniereExecution("Photos");
        }
    }
}
