using System;
using System.CodeDom.Compiler;

namespace Assignment5
{
    /// <summary>
    /// Static class implementing the sort algorithms
    /// </summary>
    public static class Sorting
    {
        public static void DepthSort(Particle[] particles, bool useInsertion)
        {
            
            // You can select which sorting algorithm you'll be using by uncommenting one of the two function calls below
            // You can visually test both of your algorithms this way

            PerformanceMonitor.TimeExecution(() =>
                {
                    if (useInsertion)
                        InsertionDepthSort(particles);
                    else
                        QuicksortDepthSort(particles);
                }
            );
        }

        /// <summary>
        /// Sort the particles by decreasing depth
        /// </summary>
        /// <param name="particles">Array of particles to sort</param>
        public static void InsertionDepthSort(Particle[] particles)
        {
            int end = particles.Length - 1;
            int f = 1;
            
            while (f != end + 1)
            {
                int f_temp = f - 1;
                Particle f_particle = particles[f];
                while (particles[f_temp].DistanceFromCamera  < f_particle.DistanceFromCamera)
                {
                    particles[f_temp + 1] = particles[f_temp];
                    f_temp = f_temp - 1;
                    if (f_temp == -1)
                        break;
                }

                particles[f_temp + 1] = f_particle;
                f = f + 1;
            }
        }

        /// <summary>
        /// Sort the particles by decreasing depth
        /// </summary>
        /// <param name="particles">Array of particles to sort</param>

        ///Helper function - Partition
        public static int Partition(Particle[] p, int pIndex, int start, int end)
        {
            Particle pivot = p[pIndex];

            //Move pivot to the end
            p[pIndex] = p[end];
            p[end] = pivot;

            //Move small values left
            int nextleft = start;
            for (int i = start; i < end; i++)
            {
                if (p[i].DistanceFromCamera >= pivot.DistanceFromCamera)
                {
                    //Swap
                    Particle temp = p[i];
                    p[i] = p[nextleft];
                    p[nextleft] = temp;
                    nextleft ++;
                }
            }
            // Move the pivot back;
            p[end] = p[nextleft];
            p[nextleft] = pivot;

            return nextleft;
        }

        public static void QuicksortRecur(Particle[] p, int start, int end)
        {
            if (end > start)
            {
                int pIndex = (start + end) / 2;
                int newIndex = Partition(p, pIndex, start, end);
                QuicksortRecur(p, start, newIndex - 1);
                QuicksortRecur(p, newIndex + 1, end);
            }
        }

        public static void QuicksortDepthSort(Particle[] particles)
        {
            QuicksortRecur(particles, 0, particles.Length - 1);
        }
    }
}
