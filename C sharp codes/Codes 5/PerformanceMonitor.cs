using System;
using System.Diagnostics;

namespace Assignment5
{
    public static class PerformanceMonitor
    {/// <summary>
        /// An object that can be used to compute elapsed time to a high resolution.
        /// Used to time the execution of the sort algorithms
        /// </summary>
        private static readonly Stopwatch SortingWatch = new Stopwatch();

        /// <summary>
        ///  Number of runs
        /// </summary>
        private static long count;

        /// <summary>
        /// Execution times of the runs
        /// </summary>
        public static float MinTime = 9999, MaxTime, AvgTime, TotalTime;

        /// <summary>
        /// Run proc, time its execution, and add it to the execution statistics
        /// </summary>
        /// <param name="proc"></param>
        public static void TimeExecution(Action proc)
        {
            SortingWatch.Restart();
            proc();
            SortingWatch.Stop();
            UpdateTimes((float)SortingWatch.ElapsedTicks / Stopwatch.Frequency);

        }

        private static void UpdateTimes(float time)
        {
            time *= 1000;
            TotalTime += time;
            count++;
            MinTime = MinTime < time ? MinTime : time;
            MaxTime = MaxTime > time ? MaxTime : time;
            AvgTime = TotalTime / count;
        }

        /// <summary>
        /// Reset execution times
        /// </summary>
        public static void ResetTimes()
        {
            MinTime = 9999;
            MaxTime = 0;
            AvgTime = 0;
            TotalTime = 0;
            count = 0;
        }

        public static string MinTimeString()
        {
            return MinTime.ToString("n6") + "ms";
        }

        public static string MaxTimeString()
        {
            return MaxTime.ToString("n6") + "ms";
        }

        public static string AvgTimeString()
        {
            return AvgTime.ToString("n6") + "ms";
        }
    }
}
