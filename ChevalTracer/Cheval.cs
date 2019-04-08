using System;
using Cheval.Models;
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
            var projectile = new Projectile(new ChevalPoint(0, 1, 0), Normalize(new ChevalVector(1, 1, 0)));
            // gravity -0.1 unit/tick, and wind is -0.01 unit/tick.
            var env = new Environment(new ChevalVector(0, -0.1, 0), new ChevalVector(-0.01, 0, 0));

            var ticks = 0;
            while (projectile.Position.Y > 0)
            {
                ticks++;
                projectile = Tick(env, projectile);
                Console.WriteLine($"{ticks:000}: Position - ({projectile.Position.X}, {projectile.Position.Y}, {projectile.Position.Z})");
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
