using System;

namespace Assignment5
{
    /// <summary>
    /// Represents a single particle
    /// </summary>
    public class Particle
    {
        /// <summary>
        /// X coordinate of this particle
        /// </summary>
        private float x;
        /// <summary>
        /// Y coordinate of this particle
        /// </summary>
        private float y;
        /// <summary>
        /// Z coordinate of this particle
        /// </summary>
        private float z;
        /// <summary>
        /// Speed along X axis of this particle
        /// </summary>
        private float xSpeed;
        /// <summary>
        /// Speed along Y axis of this particle
        /// </summary>
        private float ySpeed;
        /// <summary>
        /// Speed along Z axis of this particle
        /// </summary>
        private float zSpeed;

        /// <summary>
        /// Depth of this particle from the camera
        /// </summary>
        public float DistanceFromCamera => z * sinTheta + x * cosTheta;

        /// <summary>
        /// Serial number for the next particle to be created.
        /// </summary>
        private static int serialNumberCounter;

        /// <summary>
        /// Serial number of this particle
        /// </summary>
        public readonly int SerialNumber;


        public Particle()
        {
            // Set x,y to be a random value between [-D, D]
            x = (float)(ParticleSystem.ParticleBounds * (2 * ParticleSystem.Random.NextDouble() - 1));
            y = (float)(ParticleSystem.ParticleBounds * (2 * ParticleSystem.Random.NextDouble() - 1));
            z = (float)(ParticleSystem.ParticleBounds * (2 * ParticleSystem.Random.NextDouble() - 1));

           //make a color here

            xSpeed = (float)(2 * ParticleSystem.Random.NextDouble() - 1);
            ySpeed = (float)(2 * ParticleSystem.Random.NextDouble() - 1);
            zSpeed = (float)(2 * ParticleSystem.Random.NextDouble() - 1);

            SerialNumber = serialNumberCounter++;
        }

        // Sets a random value for the speed in each directon, in the range [-1, 1]
        public void RandomizeSpeeds()
        {
            xSpeed = (float)(2 * ParticleSystem.Random.NextDouble() - 1);
            ySpeed = (float)(2 * ParticleSystem.Random.NextDouble() - 1);
            zSpeed = (float)(2 * ParticleSystem.Random.NextDouble() - 1);
        }

        /// <summary>
        /// Position of the particle
        /// </summary>
        public Vector3 Position
        {
            get { return new Vector3(x, y, z); }
            set
            {
                x = value.X;
                y = value.Y;
                z = value.Z;
            }
        }

        /// <summary>
        /// Rotation about the vertical axis of the particle system, in radians
        /// </summary>
        private static float theta;

        /// <summary>
        /// Sin(Theta) computed once in advance because it gets used repeatedly to compute screen position
        /// </summary>
        private static float sinTheta;
        /// <summary>
        /// Cos(Theta) computed once in advance because it gets used repeatedly to compute screen position
        /// </summary>
        private static float cosTheta = 1;
        
        /// <summary>
        /// Change the rotation of the particles on screen
        /// </summary>
        private static void SetRotation(float newTheta)
        {
            theta = newTheta;
            sinTheta = (float) Math.Sin(newTheta);
            cosTheta = (float) Math.Cos(newTheta);
        }

        /// <summary>
        /// Rotate all the particles a little bit more.
        /// </summary>
        public static void RotateSlightly()
        {
            theta += 0.005f;
            SetRotation(theta);
        }

        /// <summary>
        /// Position on the screen of this particular particle
        /// </summary>
        public Vector3 DisplayPosition => new Vector3(x*sinTheta-z*cosTheta, y, z*sinTheta+x*cosTheta);

        /// <summary>
        /// Speed of this particular particle
        /// </summary>
        public Vector3 Speed => new Vector3(xSpeed, ySpeed, zSpeed);
    }
}
