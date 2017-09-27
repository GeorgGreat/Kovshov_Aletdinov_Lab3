using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aletdinov_task3
{
    
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap bmpImage;
        Pen myPen, myFillPen;
        int myPenThickness;
        Color myPenColor;
        Color oldColor;
        Color myFillColor;
        Point curPoint, prevPoint;
        bool isPressed;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            myPenThickness = 2;
            myPenColor = Color.Red;
            myFillColor = Color.Yellow;
            myPen = new Pen(myPenColor, myPenThickness);
            myFillPen = new Pen(myFillColor, 1);
            isPressed = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                prevPoint = curPoint;
                curPoint = e.Location;
                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; 

                g.DrawLine(myPen, curPoint, prevPoint);
                
                g.Dispose();
                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();
   
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isPressed = false;
            prevPoint = curPoint;
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isPressed = true;
            curPoint = e.Location;
            
            {
                oldColor = bmp.GetPixel(curPoint.X, curPoint.Y);
                if (oldColor.ToArgb()!=myFillColor.ToArgb())
                if (radioButtonFill.Checked)
                {
                    fill(curPoint, oldColor, myFillColor);
                }
                if (radioButtonImage.Checked)
                    fillImage(curPoint);
            }
        }

        private void fill(Point startPoint, Color oldColor, Color newColor)
        {
            if(bmp.GetPixel(startPoint.X,startPoint.Y)==oldColor)
            {
                Graphics g = Graphics.FromImage(bmp);

                var left = find_left(startPoint, oldColor);
                var right = find_right(startPoint, oldColor);
                g.DrawLine(myFillPen, left, right);
                g.Dispose();
                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();

                for (var x = left.X; x < right.X; x++)
                {
                    if (startPoint.Y > 1)
                        fill(new Point(x, startPoint.Y - 1), oldColor, newColor);
                    else break;
                }
                for (var x = left.X; x < right.X; x++)
                {
                    if (startPoint.Y < bmp.Height - 1)
                        fill(new Point(x, startPoint.Y + 1), oldColor, newColor);
                    else break;
                }
            }            
        }

        private void fillImage(Point startPoint)
        {
            if (bmp.GetPixel(startPoint.X, startPoint.Y) == oldColor)
            {
                Graphics g = Graphics.FromImage(bmp);
                var left = find_left(startPoint, oldColor);
                var right = find_right(startPoint, oldColor);

                for (var x = left.X; x < right.X; x++)
                {
                    int xW = x % bmpImage.Width;
                    int yH = startPoint.Y % bmpImage.Height;
                    Color c = bmpImage.GetPixel(xW, yH);
                    bmp.SetPixel(x, startPoint.Y, c);
                }   
                
                
                g.Dispose();
                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();

                for (var x = left.X; x < right.X; x++)
                {
                    if (startPoint.Y > 1)
                        fillImage(new Point(x, startPoint.Y - 1));
                    else break;
                }
                for (var x = left.X; x < right.X; x++)
                {
                    if (startPoint.Y < bmp.Height - 1)
                        fillImage(new Point(x, startPoint.Y + 1));
                    else break;
                }
                
            }

        }
        private Point find_left(Point startPoint, Color oldColor)
        {
            int x=startPoint.X;
            while((x>1) && (bmp.GetPixel(x, startPoint.Y) == oldColor))
            {
                x--;
            }
            return new Point(x+1, startPoint.Y);
        }
        private Point find_right(Point startPoint, Color oldColor)
        {
            int x = startPoint.X;
            while ((x < bmp.Width) && (bmp.GetPixel(x, startPoint.Y) == oldColor))
            {
                x++;
            }
            return new Point(x-1, startPoint.Y);
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                try
                {
                    bmpImage = new Bitmap(openFileDialog1.FileName);
                    radioButtonImage.Enabled = true;
                }
                catch
                {
                    radioButtonImage.Enabled = false;
                    MessageBox.Show("Не могу открыть изображение.");
                }
                return;
            }
        }
    }
}
