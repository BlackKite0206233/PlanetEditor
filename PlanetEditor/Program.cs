using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    class Program
    {
        static string input;
        static List<Planet> pList = new List<Planet>();

        static void Main(string[] args)
        {
            input = Console.ReadLine();
            while (!input.Equals("exit", StringComparison.Ordinal))
            {
                string op;
                op = input.Split(' ')[0];
                if (op.Equals("cp", StringComparison.Ordinal)) 
                    createPlanet(); 
                else if (op.Equals("ac", StringComparison.Ordinal)) 
                    addCreature(); 
                else if (op.Equals("ro", StringComparison.Ordinal)) 
                    removeCteature(); 
                else if (op.Equals("rp", StringComparison.Ordinal)) 
                    removePlanet(); 
                else if (op.Equals("up", StringComparison.Ordinal)) 
                    update(); 
                else if (op.Equals("chp", StringComparison.Ordinal)) 
                    changePlanetName(); 
                else if (op.Equals("cho", StringComparison.Ordinal)) 
                    changeo(); 
                else 
                    Console.WriteLine("Doesn't have this command"); 
                input = Console.ReadLine();
            }
        }
        private static void createPlanet()
        {
            Planet p_planet = null;
            string[] arg = input.Split(' ');
            double r = Convert.ToDouble(arg[5]);
            string name = arg[1];
            Coordinate pos = new Coordinate(Convert.ToDouble(arg[2]), Convert.ToDouble(arg[3]), Convert.ToDouble(arg[4]));
            try
            {
                p_planet = new Planet(name, pos, r);
                pList.Add(p_planet);
                Console.WriteLine(p_planet.getName() + " created!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
                Console.WriteLine("you shall not pass!!");
                if (p_planet != null) 
                    p_planet = null; 
            }
        }
        private static void addCreature()
        {
            string[] arg = input.Split(' ');
            string planet = arg[1];
            string c_type = arg[2];
            string name = arg[3];
            Object creature = null;

            if (pList == null) 
                Console.WriteLine("Please create planet first.");
            else if (c_type.Equals("Lion")) 
                creature = new Creature<Lion>(name);
            else if (c_type.Equals("Plant")) 
                creature = new Creature<Plant>(name);
            else 
                Console.WriteLine("you shall not pass!!");

            if (creature != null)
            {
                bool flag = false;
                foreach (Planet p in pList)
                    if (p.getName().Equals(planet))
                    {
                        flag = true;
                        p.addObject(creature);
                        Console.WriteLine(p.getName() + " : " + creature.getName() + " added!");
                    }
                if (!flag)
                    Console.WriteLine("can't find the planet!!");  
            }
        }
        private static void removeCteature()
        {
            string[] arg = input.Split(' ');
            string planet = arg[1];
            uint id = Convert.ToUInt32(arg[2]);
            if (pList != null)
            {
                bool flag = false;
                foreach (Planet p in pList)
                    if (p.getName().Equals(planet))
                    {
                        flag = true;
                        p.removeObject(id);
                        Console.WriteLine("removed!");
                    }
                if (!flag) 
                    Console.WriteLine("can't find the planet!!"); 
            }   
            else 
                Console.WriteLine("Please create planet first."); 
        }
        private static void removePlanet()
        {
            string[] arg = input.Split(' ');
            string planet = arg[1];
            if (pList != null)
            {
                bool flag = false;
                foreach (Planet p in pList)
                {
                    if (p.getName().Equals(planet))
                    {
                        flag = true;
                        pList.Remove(p);
                        Console.WriteLine("removed!");
                        break;
                    }
                }
                if (!flag) 
                    Console.WriteLine("can't find the planet!!"); 
            }
            else 
                Console.WriteLine("Please create planet first."); 
        }
        private static void update()
        {
            if (pList != null)
                foreach (Planet p in pList)
                {
                    Console.WriteLine("Planet : " + p.getName());
                    Console.WriteLine("x: " + p.getPosition().x + " y: " + p.getPosition().y + " z: " + p.getPosition().z);
                    Console.WriteLine("size : " + p.getRadius());
                    Console.WriteLine("===============");
                    p.update();
                }
            else 
                Console.WriteLine("Please create planet first."); 
        }
        private static void changePlanetName()
        {
            string[] arg = input.Split(' ');
            string planet = arg[1];
            string newName = arg[2];
            if (pList != null)
            {
                bool flag = false;
                foreach (Planet p in pList)
                    if (p.getName().Equals(planet))
                    {
                        flag = true;
                        p.setName(newName);
                        Console.WriteLine("update!");
                    }
                if (!flag) 
                    Console.WriteLine("can't find the planet!!"); 
            }
            else 
                Console.WriteLine("Please create planet first.");   
        }
        private static void changeCteatureName()
        {
            string[] arg = input.Split(' ');
            string planet = arg[1];
            uint id = Convert.ToUInt32(arg[2]);
            string newName = arg[3];
            if (pList != null)
            {
                bool flag = false;
                foreach (Planet p in pList)
                    if (p.getName().Equals(planet))
                    {
                        flag = true;
                        p.renameObject(id, newName);
                        Console.WriteLine("update!");
                    }
                if (!flag) 
                    Console.WriteLine("can't find the planet!!");
            }
            else 
                Console.WriteLine("Please create planet first.");
        }
    }
}
