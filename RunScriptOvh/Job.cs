


using System;
using Quartz;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    public class Job : IJob
    {
        /* public Task Execute(IJobExecutionContext context)
         {
             Form2 fm = new Form2();
             fm.ShowDialog();
         }
         */
        public async Task Execute(IJobExecutionContext context)
        {
            Form2 fm = new Form2();
            fm.ShowDialog();
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
            //throw new System.NotImplementedException();

        }
       
    }
}
