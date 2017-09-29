using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGLab2
{
    public partial class task_2_1 : CGLab1.Task2
    {
        BrushMode bMode;
        LockBitmap bmData;
        LockBitmap floodFillData;
        Bitmap floodFillPicture;
        Point floodFillPoint;
        List<List<bool>> processed = new List<List<bool>>();
        

        public task_2_1()
        {
            InitializeComponent();
            CreateGraphic(panel1.Width, panel1.Height);
        }

        void Fill(Point p, Color c)
        {
            if (bm.GetPixel(p.X, p.Y).ToArgb() == c.ToArgb())
                return;

            foreach (List<bool> x in processed)
                x.Clear();
            processed.Clear();
            for (int i = 0; i < bm.Width; ++i)
                processed.Add(new List<bool>(Enumerable.Repeat(false, bm.Height)));
                
            bmData = new LockBitmap(bm);
            bmData.LockBits();
            FillNested(p, (x, y) => c, bmData.GetPixel(p.X, p.Y));
            bmData.UnlockBits();
            paintWind.Invalidate();
        }

        void Fill(Point p, Bitmap ffBm)
        {
            //if (bm.GetPixel(p.X, p.Y).ToArgb() == c.ToArgb())
            //    return;
            floodFillPoint = p;
            foreach (List<bool> x in processed)
                x.Clear();
            processed.Clear();
            for (int i = 0; i < bm.Width; ++i)
                processed.Add(new List<bool>(Enumerable.Repeat(false, bm.Height)));

            floodFillData = new LockBitmap(floodFillPicture);
            floodFillData.LockBits();

            bmData = new LockBitmap(bm);
            bmData.LockBits();
            FillNested(p, GetRelativeFloodFillPixel, bmData.GetPixel(p.X, p.Y));
            bmData.UnlockBits();
            floodFillData.UnlockBits();
            paintWind.Invalidate();
        }

        void FillNested(Point p, PixelColor pc, Color bgColor)
        {
            if (p.X < 0 || 
                p.X >= bm.Width ||
                p.Y < 0 ||
                p.Y >= bm.Height ||
                processed[p.X][p.Y] == true)//bmData.GetPixel(p.X, p.Y) != bgColor)
                return;

            Tuple<int, int> range = FindBorders(p, bgColor);
            for (int i = range.Item1; i <= range.Item2; ++i)
            {
                bmData.SetPixel(i, p.Y, pc(i, p.Y));
                processed[i][p.Y] = true;
            }
            for (int i = range.Item1; i <= range.Item2; ++i)
            {
                FillNested(new Point(i, p.Y + 1), pc, bgColor);
            }
            for (int i = range.Item1; i <= range.Item2; ++i)
            {
                FillNested(new Point(i, p.Y - 1), pc, bgColor);
            }
        }

        Tuple<int, int> FindBorders(Point p, Color bgColor)
        {
            int left = -1, right = -1;
            for (int i = p.X; i >= -1; --i)
                if (i == -1 || bmData.GetPixel(i, p.Y).ToArgb() != bgColor.ToArgb())
                {
                    left = i + 1;
                    break;
                }
            for (int i = p.X; i <= bm.Width; ++i)
                if (i == bm.Width || bmData.GetPixel(i, p.Y).ToArgb() != bgColor.ToArgb())
                {
                    right = i - 1;
                    break;
                }
            return new Tuple<int, int>(left, right);
        }

        Color GetRelativeFloodFillPixel(int x, int y)
        {
            if (x >= floodFillPoint.X)
                if (y >= floodFillPoint.Y)
                    return floodFillData.GetPixel((x - floodFillPoint.X) % floodFillPicture.Width, (y - floodFillPoint.Y) % floodFillPicture.Height);
                else
                    return floodFillData.GetPixel((x - floodFillPoint.X) % floodFillPicture.Width, floodFillPicture.Height - (floodFillPoint.Y - y) % floodFillPicture.Height - 1);
            else
                if (y >= floodFillPoint.Y)
                    return floodFillData.GetPixel(floodFillPicture.Width - (floodFillPoint.X - x) % floodFillPicture.Width - 1, (y - floodFillPoint.Y) % floodFillPicture.Height);
                else
                    return floodFillData.GetPixel(floodFillPicture.Width - (floodFillPoint.X - x) % floodFillPicture.Width - 1, floodFillPicture.Height - (floodFillPoint.Y - y) % floodFillPicture.Height - 1);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {

        }

        protected override void paintWind_MouseDown(object sender, MouseEventArgs e)
        {
            switch (bMode)
            {
                case BrushMode.Paint:
                    base.paintWind_MouseDown(sender, e);
                    break;
                case BrushMode.Fill:
                    Fill(e.Location, curColor);
                    break;
                case BrushMode.FillWithPicture:
                    Fill(e.Location, floodFillPicture);
                    break;
                default:
                    break;
            }
        }

        private void paintingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (paintingRadioButton.Checked)
                bMode = BrushMode.Paint;
        }

        private void fillRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fillRadioButton.Checked)
                bMode = BrushMode.Fill;
        }

        private void pictureFfRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (pictureFfRadioButton.Checked == false)
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bMode = BrushMode.FillWithPicture;
                floodFillPicture = new Bitmap(openFileDialog1.FileName);
            }
            else
            {
                pictureFfRadioButton.Checked = false;
                switch (bMode)
                {
                    case BrushMode.Paint:
                        paintingRadioButton.Checked = true;
                        break;
                    case BrushMode.Fill:
                        fillRadioButton.Checked = true;
                        break;
                    case BrushMode.FillWithPicture:
                        pictureFfRadioButton.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public delegate Color PixelColor(int x, int y);

    public enum BrushMode { Paint, Fill, FillWithPicture };

    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr ptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        public void LockBits()
        {
            Width = source.Width;
            Height = source.Height;

            int PixelCount = Width * Height;
            
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            
            Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);
            
            if (Depth != 8 && Depth != 24 && Depth != 32)
            {
                throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
            }
            
            bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                         source.PixelFormat);
            
            int step = Depth / 8;
            Pixels = new byte[PixelCount * step];
            ptr = bitmapData.Scan0;
            
            Marshal.Copy(ptr, Pixels, 0, Pixels.Length);
        }

        public void UnlockBits()
        {
            Marshal.Copy(Pixels, 0, ptr, Pixels.Length);

            source.UnlockBits(bitmapData);
        }

        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;
            
            int cCount = Depth / 8;
            
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32)
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24)
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }
        
        public void SetPixel(int x, int y, Color color)
        {
            int cCount = Depth / 8;
            
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32)
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24)
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            {
                Pixels[i] = color.B;
            }
        }
    }
}
