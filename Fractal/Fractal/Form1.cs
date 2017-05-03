using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Form1 : Form
    {
        private Point _xPoint = new Point(100,400);
        private Point _yPoint = new Point(700,400);
        private Point _zPoint = new Point(400,50);
        private Point _currentPoint = new Point();
        private Random _random = new Random();
        private bool _isStopped = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void drawAreaPanel_MouseClick(object sender, MouseEventArgs e)
        {
            _currentPoint = new Point(e.X, e.Y);
            CreateDot(drawAreaPanel, _currentPoint);
            startButton.Enabled = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = true;
            clearButton.Enabled = false;
            startButton.Enabled = false;
            var thread = new Thread(new ThreadStart(CreateFractal));
            thread.Start();
        }

        private void drawAreaPanel_Paint(object sender, PaintEventArgs e)
        {
            using (var g = e.Graphics)
            {
                CreateDot(g, _xPoint);
                CreateDot(g, _yPoint);
                CreateDot(g, _zPoint);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _isStopped = true;
            stopButton.Enabled = false;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawAreaPanel.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CreateFractal()
        {
            using (var g = drawAreaPanel.CreateGraphics())
            {
                var i = 1;
                while (!_isStopped)
                {
                    iterationLabel.Text = (i++).ToString("##,###");
                    _currentPoint = GetMiddlePoint(_currentPoint, GetRandomPoint());
                    CreateDot(g, _currentPoint);
                    Thread.Sleep((int)(1000 / numericUpDown1.Value));
                }
            }
            stopButton.Enabled = false;
            startButton.Enabled = true;
            clearButton.Enabled = true;
        }

        private void CreateDot(Control control, Point point)
        {
            using (var g = control.CreateGraphics())
            {
                g.FillRectangle((Brush)Brushes.Yellow, point.X, point.Y, 3, 3);
            }
        }

        private void CreateDot(Graphics g, Point point)
        {
            g.FillRectangle((Brush)Brushes.Yellow, point.X, point.Y, 3, 3);
        }
        

        private Point GetRandomPoint()
        {
            var randNumber = _random.Next(1, 7);
            switch (randNumber)
            {
                case 1:
                case 2:
                    return _xPoint;
                case 3:
                case 4:
                    return _yPoint;
                case 5:
                case 6:
                    return _zPoint;
                default:
                    return _xPoint;
            }
        }

        private Point GetMiddlePoint(Point firstPoint, Point secondPoint)
        {
            return new Point((firstPoint.X + secondPoint.X)/2, (firstPoint.Y + secondPoint.Y)/2);
        }
    }
}
