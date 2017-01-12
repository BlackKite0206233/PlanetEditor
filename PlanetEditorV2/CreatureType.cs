using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditorV2
{
    abstract class CreatureType
    {
        public CreatureType() { }
        public abstract void move();
        public abstract void absorb();
        public abstract bool alive();
        public abstract int deadOrAlive();
        public abstract int birth();
    }
}
