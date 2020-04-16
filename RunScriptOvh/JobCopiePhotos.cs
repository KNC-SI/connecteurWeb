using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobCopiePhotos : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string NameCron = "copytoftp";
            Parametres.RunCommand(NameCron);
            //async
        }
    }
}
