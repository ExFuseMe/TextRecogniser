using Emgu.CV.Flann;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TextRecogniser
{
    public partial class PaintWindow : Form
    {
        public PaintWindow()
        {
            InitializeComponent();
            SetSize();
            pen.Color = Color.Black;
        }
        bool drawing = false;
        int start_x, start_y;
        int end_x, end_y;
        int index;
        public class ArrayPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrayPoints(int size)
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }
            public void ResetPoint()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }

        private ArrayPoints arrayPoints = new ArrayPoints(2);

        Bitmap map = new Bitmap(100, 100);

        private void SetSize()
        {
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(map);
            g.Clear(Color.White);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
        Graphics g;
        Pen pen = new Pen(Color.Black, 10f);

        private void drawpanel_mdown(object sender, MouseEventArgs e)
        {
            drawing = true;
            start_x = e.X;
            start_y = e.Y;
        }
        private void drawpanel_mmove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                arrayPoints.SetPoint(e.X, e.Y);
                if (arrayPoints.GetCountPoints() >= 2)
                {
                    g.DrawLines(pen, arrayPoints.GetPoints());
                    drawpanel.Image = map;
                    arrayPoints.SetPoint(e.X, e.Y);
                }
            }
            else
            {
                return;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map = (Bitmap)drawpanel.Image;
        }

        private void drawpanel_mup(object sender, MouseEventArgs e)
        {
            drawing = false;
            arrayPoints.ResetPoint();
            drawpanel.Image = map;


        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(drawpanel.BackColor);
            drawpanel.Image = map;
        }
    }
}