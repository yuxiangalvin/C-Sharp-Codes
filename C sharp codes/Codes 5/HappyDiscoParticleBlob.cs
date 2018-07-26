using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment5
{
    /// <summary>
    /// The window that displays the particle system
    /// </summary>
    public partial class HappyDiscoParticleBlob : Form
    {
        /// <summary>
        /// Number of milliseconds the application has been running
        /// </summary>
        int time = Environment.TickCount;
        
        public HappyDiscoParticleBlob()
        {
            InitializeComponent();

            Paint += DrawFrame;
            KeyDown += KeyboardHandler;
            particleSystem = new ParticleSystem(4000);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        /// <summary>
        /// Called when the user presses a key
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="args">Specifies the key that was pressed</param>
        void KeyboardHandler(object sender, KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Space:
                    isRotating = !isRotating;
                    break;

                case Keys.S:
                    useInsertion = !useInsertion;
                    break;

                case Keys.R:
                    PerformanceMonitor.ResetTimes();
                    break;
            }
            PerformanceMonitor.ResetTimes();
        }

        /// <summary>
        /// ParticleSystem to display
        /// </summary>
        readonly ParticleSystem particleSystem;
        /// <summary>
        /// Whether the system should be rotating on screen
        /// </summary>
        private bool isRotating;
        /// <summary>
        /// Whether to use insertion sort instead of quicksort
        /// </summary>
        private bool useInsertion;

        /// <summary>
        /// Redraw the window
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="args">Magic object that includes another magic object that lets you draw in the window</param>
        void DrawFrame(object sender, PaintEventArgs args)
        {
            // Update time information
            var newTime = Environment.TickCount;
            var interval = newTime - time;
            time = newTime;

            // Update particle positions
            particleSystem.Update(interval);

            // Update screen positions
            if (isRotating)
                Particle.RotateSlightly();

            // Depth sort
            Sorting.DepthSort(particleSystem.Particles, useInsertion);

            // Do the actual drawing
            args.Graphics.Clear(Color.Black);
            particleSystem.Render(args.Graphics);

            // Update performance information in the title bar
            var sort = useInsertion ? "InsertionSort" : "QuickSort";
            // ReSharper disable once LocalizableElement
            Text = $"{sort} MinTime: {PerformanceMonitor.MinTimeString()} MaxTime: {PerformanceMonitor.MaxTimeString()} AvgTime: {PerformanceMonitor.AvgTimeString()}";

            // Tell the OS to scheudle another frame redraw
            Invalidate();
        }
    }
}
