using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class jours
    {
        public string Name { get; set; }

        public int value { get; set; }
        public string variable { get; set; }
        public override string ToString()
        {
            return "ID: " + value + "   Name: " + Name+ "variable: "+variable;
        }
    }
}
