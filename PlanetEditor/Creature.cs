using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    class Creature : Object
    {
        public Creature(Creature src, string type) : base(src.getName())
        {
            _amount = src._amount;
            init_p_implement(type);
        }
        public Creature(string name, string type) : base(name)
        {
            init_p_implement(type);
        }

        public void update()
        {
            _p_implement.move();
            _p_implement.absorb();
            _amount -= _p_implement.deadOrAlive();
            if (_p_implement.alive())
                _amount += _p_implement.birth();
            _amount = _amount < 0 ? 0 : _amount;
        }

        private int _amount = 0;
        private CreatureType _p_implement = null;
        private void init_p_implement(string type)
        {
            if (type.Equals("Lion"))
                _p_implement = new Lion();
            else if (type.Equals("Plant"))
                _p_implement = new Plant();
            else
                throw new Exception("You shall not pass!!");
        }

        ~Creature()
        {
            if (_p_implement != null)
                _p_implement = null;
        }

    }
}
