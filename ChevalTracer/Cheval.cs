using System;
using Cheval.Models;
using Cheval.Services;
using static Cheval.Models.ChevalVector;

namespace Cheval
{
    public class Cheval
    {
        public const double Epsilon = 0.00001;

        static void Main(string[] args)
        {
            // projectile starts one unit above the origin.
            // velocity is normalized to 1 unit/tick.
            var start = new ChevalPoint(0,1,0);
            var velocity = Normalize(new ChevalVector(1, 1.8, 0))* 11.25;
            var gravity = new ChevalVector(0, -0.1, 0);
            var wind = new ChevalVector(-0.01, 0, 0);

            var projectile = new Projectile(start, (ChevalVector)velocity);
            // gravity -0.1 unit/tick, and wind is -0.01 unit/tick.
            var env = new Environment(gravity , wind);
            var canvas = new Canvas(900,550);
           

            var ticks = 0;
            while (projectile.Position.Y > 0)
            {
                ticks++;
                projectile = Tick(env, projectile);
                Console.WriteLine($"{ticks:000}: Position - ({projectile.Position.X}, {projectile.Position.Y}, {projectile.Position.Z})");
                WritePixels(canvas, projectile.Position);
            }
            
            System.IO.File.WriteAllText(@".\Bullet.ppm", canvas.ToPPM());
        }

        private static void WritePixels(Canvas canvas, ChevalPoint position)
        {
            var colour = new ChevalColour(1, .5, 0);
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    canvas.WritePixel((int)position.X+i, 550 - (int)position.Y+j, colour);
                } 
            }
            
        }

        public static Projectile Tick(Environment env, Projectile proj)
        {
            var position = (ChevalPoint) (proj.Position + proj.Velocity);
            var velocity = (ChevalVector) (proj.Velocity + env.Gravity + env.Wind);
            return new Projectile(position, velocity);
        }

    }

    public class Projectile
    {
        public ChevalPoint Position { get; set; }
        public ChevalVector Velocity { get; set; }

        public Projectile(ChevalPoint position, ChevalVector velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }

    public class Environment
    {
        public ChevalVector Gravity { get;  set; }
        public ChevalVector Wind { get; set; }

        public Environment(ChevalVector gravity, ChevalVector wind)
        {
            Gravity = gravity;
            Wind = wind;
        }
    }
}
