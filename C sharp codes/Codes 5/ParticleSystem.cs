using System;
using System.Drawing;

namespace Assignment5
{
    /// <summary>
    /// Implements a large set of moving particles
    /// </summary>
    public class ParticleSystem
    {
        /// <summary>
        /// Make a system with the specified number of particles
        /// </summary>
        /// <param name="n"></param>
        public ParticleSystem(int n)
        {
            Random = new Random();
            Particles = new Particle[n];
            for (int i = 0; i < n; i++)
                Particles[i] = new Particle();
        }

        /// <summary>
        /// Initial size of the particle system
        /// </summary>
        public const float ParticleBounds = 3f;

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random Random;

        /// <summary>
        /// All the particles in the system
        /// </summary>
        public readonly Particle[] Particles;
        
        /// <summary>
        /// Focal length of the simulated camera, for rendering purposes
        /// </summary>
        const float FocalLength = 350f;

        /// <summary>
        /// Draw all the particles on the screen.
        /// </summary>
        /// <param name="graphics">A magic object that lets you scribble on the screen</param>
        public void Render(Graphics graphics)
        {
            for (var i = 0; i < Particles.Length ; i++)
            {
                var p = Particles[i];
                var position3D = p.DisplayPosition;
                var depth = position3D.Z+8;
                var location = new PointF(380 + FocalLength * position3D.X / depth,
                    260 + FocalLength * position3D.Y / depth);
                // Clip it if it moves behind the camera
                if (depth > 0.5f)
                    // Change this to FillRectangle if it draws too slowly on your machine 
                    graphics.FillEllipse(brushes[p.SerialNumber%4], new RectangleF(location, new SizeF(80 / depth, 80 / depth)));
            }
        }

        /// <summary>
        /// Colors for the particles
        /// </summary>
        private readonly SolidBrush[] brushes = {
            new SolidBrush(Color.FromArgb(128, 255, 0, 0)),
            new SolidBrush(Color.FromArgb(128, 0, 255, 0)),
            new SolidBrush(Color.FromArgb(128, 0, 0, 255)),
            new SolidBrush(Color.FromArgb(128, 128, 128, 128))
        };

        /// <summary>
        /// A general scale factor to make tuning speed easy
        /// </summary>
        const float SpeedScale = 0.0005f;

        /// <summary>
        /// Update the positions of all the particles
        /// </summary>
        /// <param name="ms">Number of milliseconds that have ellapsed</param>
        public void Update(int ms)
        {
            foreach (var p in Particles) {
                var position = p.Position;
                var speed = p.Speed;
                position.X += speed.X * ms * SpeedScale;
                position.Y += speed.Y * ms * SpeedScale;
                position.Z += speed.Z * ms * SpeedScale;
                p.Position = position;
            }
        }
    }
}
