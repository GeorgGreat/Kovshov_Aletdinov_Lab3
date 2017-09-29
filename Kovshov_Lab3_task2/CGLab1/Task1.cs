using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGLab1
{
    public partial class Task1 : Form
    {
        Bitmap drawArea;
        Graphics gr;
        bool drawWindResized = false;

        public Task1()
        {
            InitializeComponent();
            InitFuncComboBox();
            CreateGraphic();
        }

        void CreateGraphic()
        {
            drawArea = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            gr = Graphics.FromImage(drawArea);
        }

        void InitFuncComboBox()
        {
            funcComboBox.Items.Add(Functions.sqr);
            funcComboBox.Items.Add(Functions.sin);
            funcComboBox.SelectedIndex = 0;
        }

        Tuple<double, double> GetMinMaxValues(MathFunc func, double left, double right)
        {
            double x = left;
            double maxY = func(x), minY = func(x);
            double s = (right - left) / this.pictureBox1.Width;
            for (int i = 0; i < this.pictureBox1.Width; ++i)
            {
                maxY = Math.Max(maxY, func(x));
                minY = Math.Min(minY, func(x));
                x += s;
            }

            return new Tuple<double, double>(minY, maxY);
        }

        void SetScale(float yOffset)
        {
            gr.ResetTransform();
            gr.TranslateTransform(0, yOffset);
            gr.ScaleTransform(1, -1);
        }

        void Draw(MathFunc func)
        {
            gr.Clear(Color.White);
            double rangeLeft = (double)rangeLeftNumeric.Value, rangeRight = (double)rangeRightNumeric.Value;
            double s = (rangeRight - rangeLeft) / this.pictureBox1.Width;

            Tuple<double, double> t = GetMinMaxValues(func, rangeLeft, rangeRight);
            double maxY = t.Item1, minY = t.Item2;

            double x = rangeLeft;
            double yScale = pictureBox1.Height / (Math.Abs(maxY - minY));
            SetScale(Convert.ToSingle(yScale * minY));
            for (int i = 1; i < this.pictureBox1.Width; ++i)
            {
                gr.DrawLine(Pens.Black,
                            i - 1,
                            (int)Math.Round(yScale * func(x)),
                            i,
                            (int)Math.Round(yScale * func(x + s)));
                x += s;
            }
            pictureBox1.Invalidate();
        }

        MathFunc GetFunction()
        {
            switch (funcComboBox.Items[funcComboBox.SelectedIndex].ToString())
            {
                case Functions.sqr:
                    return x => x * x;
                case Functions.sin:
                    return Math.Sin;
                default:
                    throw new NotImplementedException();
            }
        }

        private void drawFunc_Click(object sender, EventArgs e)
        {
            if (rangeLeftNumeric.Value >= rangeRightNumeric.Value)
            {
                MessageBox.Show("Недопустимый диапазон", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (drawWindResized)
                CreateGraphic();

            Draw(GetFunction());
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(drawArea, 0, 0);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            drawWindResized = true;
        }
        

        private void Task1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
            MainWindow.Instance.Show();
        }
    }

    delegate double MathFunc(double x);

    static class Functions
    {
        public const string sqr = "F(x) = x^2";
        public const string sin = "F(x) = sin(x)";
    }
}
