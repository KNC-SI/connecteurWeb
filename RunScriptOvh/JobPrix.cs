﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobPrix : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string NameCron = "prix";
            Parametres.RunCommand(NameCron);
            Parametres.DerniereExecution("Prix");
        }
    }
}
