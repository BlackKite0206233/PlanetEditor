using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    class Lion : CreatureType
    {
        public Lion() { }
        public virtual void move() { Console.WriteLine("Run Run Run"); }
        public virtual void absorb() { Console.WriteLine("Eat Eat Eat"); }
        public virtual bool alive() { return true; }
        public virtual int deadOrAlive() { Console.WriteLine("Alive"); return 0; }
        public virtual int birth() { return 0; }
    }
}
