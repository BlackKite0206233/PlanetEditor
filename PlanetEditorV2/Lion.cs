using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditorV2
{
    class Lion : CreatureType
    {
        public Lion() { }
        public override void move()
        {
            Console.WriteLine("Run Run Run");
        }
        public override void absorb()
        {
            Console.WriteLine("Eat Eat Eat");
        }
        public override bool alive()
        {
            return true;
        }
        public override int deadOrAlive()
        {
            Console.WriteLine("Alive");
            return 0;
        }
        public override int birth()
        {
            return 0;
        }
    }
}
