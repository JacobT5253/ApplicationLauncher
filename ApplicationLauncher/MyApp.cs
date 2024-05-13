using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLauncher
{
    public class MyApp
    {

        public string Name { get; set; }
        public string Path { get; set; }
        public int Delay { get; set; }

        public MyApp(string name, string path, int delay) 
        { 
            Name = name;
            Path = path;
            Delay = delay;
        }

    }
}
