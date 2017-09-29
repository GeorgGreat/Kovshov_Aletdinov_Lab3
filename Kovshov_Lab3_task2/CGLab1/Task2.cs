using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace CGLab1
{
    public partial class Task2 : Form
    {
        protected Bitmap bm;
        protected Graphics gr;

        bool mouseDown = false;
        MouseButtons button;
        Point prevMousePos;

        Pen colorPen = new Pen(Color.Black);
        Pen whitePen = new Pen(Color.White);

        protected Color curColor = Color.Black;

        public Task2()
        {
            InitializeComponent();

            colorPen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
            colorPen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Square;

            CreateGraphic(panel1.Width, panel1.Height);

            widthNumeric.Value = panel1.Width;
            heightNumeric.Value = panel1.Height;
        }

        protected void CreateGraphic(int width, int height)
        {
            if (bm != null)
                bm.Dispose();
            bm = new Bitmap(width, height);
            paintWind.Width = bm.Width;
            paintWind.Height = bm.Height;
            if (gr != null)
                gr.Dispose();
            gr = Graphics.FromImage(bm);
            gr.Clear(Color.White);
        }

        protected virtual void paintWind_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            button = e.Button;
            prevMousePos = new Point(e.X, e.Y);
        }

        private void paintWind_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void paintWind_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point pt = new Point(e.X, e.Y);
                if (button == MouseButtons.Left)
                {
                    gr.DrawLine(colorPen, prevMousePos, pt);
                }
                else
                    if (button == MouseButtons.Right)
                    {
                        gr.DrawLine(whitePen, prevMousePos, pt);
                    }

                prevMousePos.X = e.X;
                prevMousePos.Y = e.Y;
                paintWind.Invalidate();
            }
        }

        private void paintWind_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void colorDialogButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                colorPen.Color = colorDialog1.Color;
                curColor = colorDialog1.Color;
            }
        }

        private void acceptSizeButton_Click(object sender, EventArgs e)
        {
            Bitmap tmp = new Bitmap(bm);
            CreateGraphic(Convert.ToInt32(widthNumeric.Value), Convert.ToInt32(heightNumeric.Value));
            gr.DrawImage(tmp, 
                         new Rectangle(0, 0, bm.Width, bm.Height),
                         new Rectangle(0, 0, bm.Width, bm.Height),
                         GraphicsUnit.Pixel);
            tmp.Dispose();
            paintWind.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            colorPen.Width = trackBar1.Value;
            whitePen.Width = trackBar1.Value;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format;
                var extension = Path.GetExtension(saveFileDialog1.FileName);

                switch (extension.ToLower())
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        MessageBox.Show("Ошибка при сохранении: неверный формат файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                bm.Save(saveFileDialog1.FileName, format);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            gr.Clear(Color.White);
            paintWind.Invalidate();
        }

        private void Task2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
            MainWindow.Instance.Show();
        }
    }
}
