using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    abstract class Object
    {
        public Object(Object src)
        {
            _name = src.getName();
            _ID = s_next_id++;
        }
        public Object(string name)
        {
            _name = name;
            _ID = s_next_id++;
        }

        public uint getID() { return _ID; }
        public string getName() { return _name; }
        public virtual void update() { }

        private static uint s_next_id = 0;
        private UInt32 _ID;
        private string _name;

        ~Object() { }
    }
}
