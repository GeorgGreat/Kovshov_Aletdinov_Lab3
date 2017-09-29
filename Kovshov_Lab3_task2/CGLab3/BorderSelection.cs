using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CGLab3
{
	// 3 2 1
	// 4 x 0
	// 5 6 7


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

	public partial class BorderSelection : CGLab1.Task2
    {
        LockBitmap bmData;
        List<BorderPixel> border = new List<BorderPixel>();
        Color borderColor;
        Point borderStart;

        public BorderSelection()
        {
            InitializeComponent();
            CreateGraphic(panel1.Width, panel1.Height);
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //base.OnFormClosing(e);
        }

        private Point FindObjectStartPoint()
        {
            Point p;
            //throw new NotImplementedException();
            for (int x = bmData.Width - 1; x > 0; --x)
                for (int y = 0; y < bmData.Height; ++y)
                    if (bmData.GetPixel(x, y).ToArgb() == borderColor.ToArgb())
                    {
                        p = new Point(x, y);
                        while (bmData.GetPixel(p.X - 1, p.Y).ToArgb() == borderColor.ToArgb())
                            --p.X;
                        return p;
                    }


            return new Point(-1, -1);
        }

        Point GetNextPoint(Point p, int direction)
        {
            switch (direction)
            {
                case 0:
                    return new Point(p.X + 1, p.Y);
                case 1:
                    return new Point(p.X + 1, p.Y - 1);
                case 2:
                    return new Point(p.X, p.Y - 1);
                case 3:
                    return new Point(p.X - 1, p.Y - 1);
                case 4:
                    return new Point(p.X - 1, p.Y);
                case 5:
                    return new Point(p.X - 1, p.Y + 1);
                case 6:
                    return new Point(p.X, p.Y + 1);
                case 7:
                    return new Point(p.X + 1, p.Y + 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        int GetDirection(int direction, int offset)
        {
            return direction + offset < 0 ? direction + offset % 8 + 8 : (direction + offset) % 8;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prevDir"></param>
        /// <param name="newDir"></param>
        /// <param name="p"></param>
      
        void AddBorderPoint(int prevDir, int newDir, Point p, bool left)
        {
            
            if (prevDir >= 4 && prevDir <= 7 && (newDir >= 5 && newDir <= 7 || newDir == 0))
                border.Add(new BorderPixel(p.X, p.Y, left));
            else
                if (prevDir >= 0 && prevDir <= 3 && newDir >= 1 && newDir <= 4)
                    border.Add(new BorderPixel(p.X, p.Y, !left));
                else
                {
                    border.Add(new BorderPixel(p.X, p.Y, !left));
                    border.Add(new BorderPixel(p.X, p.Y, left));
                }
     
        }

        void StartBorderSelection(int prev, Point point, bool left)
        {
            borderStart = point;
            int prevDirection = prev;
            Point p = point;
            int i;

            while (true)
            {
                int newDirection = GetDirection(prevDirection, -2);

                i = 0;
                while (true)
                {
                    if (i > 8)
                        return;

                    Point next = GetNextPoint(p, newDirection);
                    if (bmData.GetPixel(next.X, next.Y).ToArgb() == borderColor.ToArgb())
                    {
                        AddBorderPoint(prevDirection, newDirection, p, left);
                        prevDirection = newDirection;
                        p = next;
                        if (p == borderStart)
                            return;
                        break;
                    }
                    else
                    {
                        newDirection = GetDirection(newDirection, 1);
                        ++i;
                    }
                }
            }
        }

        private void DrawBorders()
        {
			foreach (BorderPixel p in border)
				if (p.IsLeft != true)
				{
					bmData.SetPixel(p.X, p.Y, Color.Green);
				}
				else
				{
					bmData.SetPixel(p.X, p.Y, Color.Red);
				}
        }

        private void DrawHorizontalLine(int x1, int x2, int y, Color c)
        {
            if (x1 == x2)
                return;

            int first = x1 > x2 ? x2 : x1, last = x1 > x2 ? x1 : x2;

            //TODO: check object
            for (int i = first + 1; i < last; ++i)
            {
                if (bmData.GetPixel(i, y).ToArgb() == borderColor.ToArgb())
                {
                    ScanObject(new Point(i, y));
                    break;
                }
                
                bmData.SetPixel(i, y, c);
            }
        }

       

        private void ScanObject(Point p)
        {
            borderStart = p;
            StartBorderSelection(4, p, true);

        }

        private void selectBorderButton_Click(object sender, EventArgs e)
        {
            border.Clear();
            borderColor = curColor;
            bmData = new LockBitmap(bm);
            bmData.LockBits();
            borderStart = FindObjectStartPoint();
            if (borderStart.X > 0)
                StartBorderSelection(6, borderStart, true);
            DrawBorders();
            bmData.UnlockBits();
            paintWind.Invalidate();
        }

       
    }

    public class BorderPixel : IComparable<BorderPixel>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsLeft { get; private set; }

        public BorderPixel(int x, int y, bool isLeft)
        {
            X = x;
            Y = y;
            IsLeft = isLeft;
        }

        public int CompareTo(BorderPixel other)
        {
            if (other == null) return 1;

            if (Y > other.Y || Y == other.Y && X > other.X)
                return 1;
            if (Y < other.Y || Y == other.Y && X < other.X)
                return -1;
            else
                return 0;
        }
    }
}
