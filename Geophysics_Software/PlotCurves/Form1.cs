using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotCurves
{
    public partial class Form1 : Form
    {
        List<Curve> curves = new List<Curve>();
        public Form1()
        {
            InitializeComponent();
        }

        private void 读入数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hello World");
            Curve a = new Curve();
            a.Add(0.5f, 10);
            a.Add(1, 18);
            a.Add(2, 25);
            a.Add(4, 30);
            a.Add(8, 22);
            a.Add(15, 16);
            a.Add(20, 45);
            a.Add(30, 75);
            a.Add(60, 100);
            a.Print();
            a.GetRange();
            a.PrintMaxMin();
            curves.Add(a); // 向列表传
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            DrawCurves(curves);
        }

        void DrawCurves(List<Curve> curves) // curves为列表类, Curve为曲线类
        {

            Graphics g = this.CreateGraphics();
            Rectangle winRect = new Rectangle(0, 0, this.Width, this.Height);
            for (int i = 0; i < curves.Count; i++)
            {
                Curve a = curves[i];
                a.Draw(g, winRect);
            }
        }

        private void 读入数据ToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            DrawCurves(curves);
        }
    }
}
