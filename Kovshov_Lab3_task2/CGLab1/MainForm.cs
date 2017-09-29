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
    public partial class MainForm : Form
    {
        Task1 t1 = new Task1();
        Task2 t2 = new Task2();


        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            t1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            t2.Show();
        }

      
    }

    public class MainWindow
    {
        private static MainForm instance;

        private MainWindow() { }

        public static MainForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainForm();
                }
                return instance;
            }
        }
    }
}
