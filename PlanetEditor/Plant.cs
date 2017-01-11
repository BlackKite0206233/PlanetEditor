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
        public override void move() { Console.WriteLine("Plant cannot move :("); }
        public override void absorb() { Console.WriteLine("Growth"); }
        public override bool alive() { return true; }
        public override int deadOrAlive() { Console.WriteLine("Alive"); return 0; }
        public override int birth() { Console.WriteLine("Spread seeds"); return 0; }
    }
}
