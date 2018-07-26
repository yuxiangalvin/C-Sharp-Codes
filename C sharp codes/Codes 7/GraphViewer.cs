using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PathPlanner
{
    public partial class GraphViewer : Form
    {
        private readonly Brush _black = new SolidBrush(Color.Black);
        private readonly Pen _thinBlack = new Pen(new SolidBrush(Color.Black), 1);
        private readonly Pen _thickGreen = new Pen(new SolidBrush(Color.Green), 3);
        private readonly SolidBrush _green = new SolidBrush(Color.Purple);

        private WeightedGraph _graph;
        private string _start, _end;
        private List<string> _highlightedPath;

        public GraphViewer () {
            InitializeComponent();
            _graph = new WeightedGraph();
        }

        protected override void OnPaint (PaintEventArgs e) {
            base.OnPaint(e);

            if (_graph == null) { return; }

            var g = e.Graphics;
            foreach (string n in _graph.Nodes()) {
                g.DrawString(n, Font, _black, MakePoint(_graph.NodeLocation(n)));
                DrawNeighbors(g, n);
                DrawHighlightedPath(g);
            }
            g.ResetTransform();
        }

        //
        // Helper shit

        private Point MakePoint (Point2D p) { return new Point(p.X, ClientSize.Height - p.Y); }

        private void DrawNeighbors (Graphics g, string n) {
            foreach (string neighbor in _graph.Neighbors(n)) {
                g.DrawLine(_thinBlack, MakePoint(_graph.NodeLocation(n)), MakePoint(_graph.NodeLocation(neighbor)));
            }
        }

        private void DrawHighlightedPath (Graphics g) {
            if (_highlightedPath == null || _highlightedPath.Count <= 0) { return; }

            var arrowStart = MakePoint(_graph.NodeLocation(_highlightedPath[0]));

            // I am sorry about this.
            for (int i = 1; i < _highlightedPath.Count; i++) {
                var arrowEnd = MakePoint(_graph.NodeLocation(_highlightedPath[i]));
                g.DrawLine(_thickGreen, arrowStart, arrowEnd);
                var offset = new PointF(arrowEnd.X - arrowStart.X, arrowEnd.Y - arrowStart.Y);
                float distance = (float)Math.Sqrt(offset.X * offset.X + offset.Y * offset.Y);

                if (distance > 0) {
                    const float tHeight = 15F;
                    var unit = new PointF(offset.X / distance, offset.Y / distance);
                    var perp = new PointF(-unit.Y, unit.X);
                    var arrowHeadStart = new PointF(arrowEnd.X - tHeight * unit.X,
                        arrowEnd.Y - tHeight * unit.Y);
                    var halfWidth = new PointF(perp.X * 0.5f * tHeight, perp.Y * 0.5f * tHeight);
                    var triangle = new[]
                    {
                        arrowEnd,
                        new PointF(arrowHeadStart.X + halfWidth.X, arrowHeadStart.Y + halfWidth.Y),
                        new PointF(arrowHeadStart.X - halfWidth.X, arrowHeadStart.Y - halfWidth.Y)
                    };
                    g.FillPolygon(_green, triangle);
                }
                else { g.DrawString("Zero-length edge.", Font, _green, arrowStart); }

                arrowStart = arrowEnd;
            }
        }

        //
        // Textbox crap

        private void FromBoxTextChanged (object sender, EventArgs e) { _start = FromBox.Text; }

        private void ToBoxTextChanged (object sender, EventArgs e) { _end = ToBox.Text; }

        //
        // Button presses, fam

        private void ReadFileClick (object sender, EventArgs e) {
            _graph = new WeightedGraph();
            _graph.ReadFile(FilePathTextBox.Text);
            _highlightedPath = null;
            Invalidate();
        }

        private void AStarClick (object sender, EventArgs e) { FindPathButtonClick(sender, e); }

        private void FindPathButtonClick (object sender, EventArgs e) {
            try {
                _highlightedPath = ((Button)sender).Text == @"A*"
                    ? _graph.ShortestPathAStar(_start, _end)
                    : _graph.ShortestPathDijkstra(_start, _end);
            }
            catch (Exception exception) {
                MessageBox.Show($@"{exception.Message}
                                {exception.StackTrace}");
            }
            Invalidate();  // Forces screen to redraw.
        }
    }
}
