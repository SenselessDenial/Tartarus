using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class ParticleManager : Component
    {
        private Particle[] particles;

        private static readonly int MAX_PARTICLE_COUNT = 100;
        private static float gravity = 0.05f;
        private static float maxAge = 60;

        public ParticleManager(Entity entity)
            : base(entity, true, true)
        {
            particles = new Particle[MAX_PARTICLE_COUNT];

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].ID = ParticleID.Inactive;
            }
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].ID != ParticleID.Inactive)
                {
                    Particle p = particles[i];
                    p.Age++;
                    if (p.Age > maxAge)
                        p.ID = ParticleID.Inactive;
                    p.Acceleration.Y += gravity;
                    Vector2 velocity = p.Position - p.PrevPosition;
                    p.PrevPosition = p.Position;
                    p.Position = p.Position + velocity + p.Acceleration;
                    p.Acceleration = Vector2.Zero;

                    particles[i] = p;
                }
            }


        }

        public override void Render()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].ID != ParticleID.Inactive)
                {
                    Particle p = particles[i];
                    float alpha = 1 - (float)p.Age / maxAge;
                    Drawing.Font.Draw(p.Value.ToString(), p.Position, new Color(1f, 1f, 1f, alpha));
                }
            }
        }

        public void Spawn(ParticleID id, int value, Vector2 pos, Vector2 acceleration)
        {
            Particle p = new Particle(id, value, pos);
            p.Acceleration = acceleration;

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].ID == ParticleID.Inactive)
                {
                    particles[i] = p;
                    return;
                }
            }
        }

        public void Spawn(int value, Vector2 pos)
        {
            Vector2 abc = Calc.RandomVectorFromAngle(-MathHelper.PiOver4, 3 * -MathHelper.PiOver4, 1.3f);
            Spawn(ParticleID.Damage, value, pos, abc);
        }

        public struct Particle
        {
            public ParticleID ID;
            public Vector2 Position;
            public Vector2 PrevPosition;
            public Vector2 Acceleration;
            public int Value;
            public short Age;
            
            public Particle(ParticleID iD, int value, Vector2 position)
            {
                ID = iD;
                Position = position;
                PrevPosition = position;
                Value = value;
                Age = 0;
                Acceleration = Vector2.Zero;
            }
        }

        public enum ParticleID
        {
            Inactive,
            Damage
        }

    }
}
