using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            String input;
            input = Console.ReadLine();
            Planet p_planet = null;
            while (!input.Equals("exit", StringComparison.Ordinal))
            {
                string op;
                op = input.Split(' ')[0];
                if(op.Equals("cp", StringComparison.Ordinal))
                {
                    string[] arg = new string[6];
                    arg = input.Split(' ');
                    double r = Convert.ToDouble(arg[5]);
                    string name = arg[1];
                    Coordinate pos = new Coordinate(Convert.ToDouble(arg[2]), Convert.ToDouble(arg[3]), Convert.ToDouble(arg[4]));
                    try
                    {
                        p_planet = new Planet(name, pos, r);
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
                else if(op.Equals("ac", StringComparison.Ordinal))
                {
                    string[] arg = new string[3];
                    arg = input.Split(' ');
                    string c_type = arg[1];
                    string name = arg[2];
                    Object creature = null;
                    if(p_planet == null)
                        Console.WriteLine("Please create planet first.");
                    else
                    {
                        try
                        {
                            creature = new Creature(name, c_type);
                            p_planet.addObject(creature);
                            Console.WriteLine(creature.getName() + " added!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Data);
                        }
                    }

                }
                else if(op.Equals("ro", StringComparison.Ordinal))
                {
                    string arg = input.Split(' ')[1];
                    uint id = Convert.ToUInt32(arg);
                    if (p_planet == null)
                        Console.WriteLine("Please create planet first.");
                    else
                    {
                        p_planet.removeObject(id);
                        Console.WriteLine("removed!");
                    }
                }
                else if (op.Equals("up", StringComparison.Ordinal))
                {
                    if (p_planet == null)
                        Console.WriteLine("Please create planet first.");
                    else
                        p_planet.update();
                }
                input = Console.ReadLine();
            }
        }
    }
}
