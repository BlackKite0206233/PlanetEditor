using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditorV2
{
    class Planet : Object
    {
        public Planet(Planet src) : base(src.getName())
        {
            foreach(Object sop in src._object_ptrs)
                _object_ptrs.Add(sop);
        }
        public Planet(string name, Coordinate position, double radius) : base(name)
        {
            _position = position;
            _radius = radius;
            if (!radiusCheck())
                throw new Exception("Radius should be greater than 0.0f.");
        }
        public Planet(string name, double radius) : base(name)
        {
            _radius = radius;
            if (!radiusCheck())
                throw new Exception("Radius should be greater than 0.0f.");   
        }

        public void setPosition(Coordinate position)
        {
            _position = position;
        }
        public Coordinate getPosition()
        {
            return _position;
        }
        public void setRadius(double r)
        {
            _radius = r;
        }
        public double getRadius()
        {
            return _radius;
        }
        public void addObject(Object ptr)
        {
            _object_ptrs.Add(ptr);
        }
        public void removeObject(UInt32 id)
        {
            foreach(Object it in _object_ptrs)
                if(it.getID() == id)
                {
                    _object_ptrs.Remove(it);
                    break;
                }
                    
        }
        public override void update()
        {
            if(_object_ptrs != null)
                foreach (Object it in _object_ptrs)
                {
                    Console.WriteLine(it.getID() + " " + it.getName());
                    it.update();
                    Console.WriteLine();
                }
            else
                Console.WriteLine("Please create creature first.");
        }

        private bool radiusCheck() { return _radius > 0.0f; }
        private Coordinate _position;
        private double _radius;
        private List<Object> _object_ptrs = new List<Object>();

        ~Planet() { }
    }
}
