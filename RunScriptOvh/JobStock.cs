﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class JobStock : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string NameCron = "stock";
            Parametres.RunCommand(NameCron);
            //async
        }
    }
}
