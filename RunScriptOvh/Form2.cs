using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;

namespace RunScriptOvh
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public IJobDetail job;

        private void button1_Click(object sender, EventArgs e)
        {
            object MyJob = new Job();
            var type = MyJob.GetType();

            switch (type.Name)
            {
                case "Job":
                    job = JobBuilder.Create<Job>().Build();
                    Console.WriteLine("Case 1");
                    break;
                case "Job1":
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
    }
}
