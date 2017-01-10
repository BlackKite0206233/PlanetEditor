using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    abstract class CreatureType
    {
        public CreatureType() { }
        public virtual void move() {  }
        public virtual void absorb() {  }
        public virtual bool alive() { return true; }
        public virtual int deadOrAlive() { return 0; }
        public virtual int birth() { return 0; }
    }
}
