using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    class Plant : CreatureType
    {
        public Plant() { }
        public virtual void move() { Console.WriteLine("Plant cannot move :("); }
        public virtual void absorb() { Console.WriteLine("Growth"); }
        public virtual bool alive() { return true; }
        public virtual int deadOrAlive() { Console.WriteLine("Alive"); return 0; }
        public virtual int birth() { Console.WriteLine("Spread seeds"); return 0; }
    }
}
