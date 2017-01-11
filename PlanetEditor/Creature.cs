using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    class Creature<T> : Object where T : CreatureType, new()
    {
        public Creature(Creature<T> src) : base(src.getName())
        {
            _amount = src._amount;
            _p_implement = new T();
        }
        public Creature(string name) : base(name)
        {
            _p_implement = new T();
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
        private T _p_implement = null;

        ~Creature()
        {
            if (_p_implement != null)
                _p_implement = null;
        }

    }
}
