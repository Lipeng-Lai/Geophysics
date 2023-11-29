using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PlotCurves
{
    public partial class Form1 : Form
    {
        List<Curve> curves = new List<Curve>(); // 全局曲线列表
        Point p1, p2;
        bool bMouseDown = false; // 初始状态未选中
        int curSelected = -1; // 判断当前选中的是第几条曲线

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void 读入数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Begin!");
            
            // 生成数据代码
            /*
            Curve a = new Curve();
            a.Test_Add_Data(); // 测试增加生成数据
            a.Print(); // 测试数据是否读入曲线
            a.GetRange(); // 求取Min-Max
            a.PrintMaxMin(); // 测试Min-Max
            curves.Add(a); // 向曲线列表传入一条曲线
            panel1.Invalidate(); // 调用在panel1里面绘图
            */

            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                FileStream fs = new FileStream(filename, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                int n = br.ReadInt32();
                for (int i = 0; i < n; i ++)
                {
                    Curve a = new Curve();
                    a.Read(br);
                    curves.Add(a); // 向曲线列表传入一条曲线

                    /* 移动至Curves.cs类绘制函数时再求取
                    a.Print(); // 测试数据是否读入曲线
                    a.GetRange(); // 求取Min-Max
                    a.PrintMaxMin(); // 测试Min-Max
                    */
                }
                // panel1.Invalidate(); // 调用在panel1里面绘图
                pictureBox1.Invalidate(); // 调用在picutureBox1中绘图
                br.Close();
                fs.Close();

                string ListBox_filename = Path.GetFileName(filename);
                listBox1.Items.Add(ListBox_filename);
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //double-buffering
            // DrawCurves(e.Graphics);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawCurves(e.Graphics);
        }

        void DrawCurves(Graphics g) // curves为列表类, Curve为曲线类
        {
            // Graphics g = this.panel1.CreateGraphics();
            g.Clear(Color.White); // 刷新窗口
            Rectangle winRect = new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
            for (int i = 0; i < curves.Count; i++) // 绘制curves.Count条曲线
            {
                Curve a = curves[i];
                a.GetWinRect(winRect); // 获取panel的属性
                if (i == curSelected)
                {
                    Console.WriteLine($"###After: {curSelected}");
                    a.Draw(g, true);
                }
                else
                {
                    a.Draw(g, false);
                }
            }
        }

        bool DoSelect(Point p) // 判断鼠标点击的某点是否在曲线点内
        {
            for (int i = 0; i < curves.Count; i++)
            {
                Curve a = curves[i]; // 取出一条曲线
                if (a.DoSelect(p))
                {
                    a.Print();
                    return true;
                }
            }
            return false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true; // 鼠标点击, 状态改变为true

            Point p = e.Location;
            p1 = p; // p1记录被点击时的位置

            if (DoSelect(p))
            {
                panel1.Invalidate(); //若判断成功则重新绘图
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true; // 鼠标点击, 状态改变为true

            Point p = e.Location;
            p1 = p; // p1记录被点击时的位置

            if (DoSelect(p))
            {
                pictureBox1.Invalidate(); //若判断成功则重新绘图
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = e.Location; 
            p2 = p; // p2记录鼠标松手时的位置

            bMouseDown = false; // 对松手的该点标记状态

            /*
            if (DoSelect(p))
            {
                panel1.Invalidate(); //若判断成功则重新绘图
            }*/
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            p2 = p; // p2记录鼠标松手时的位置

            bMouseDown = false; // 对松手的该点标记状态

            /*
            if (DoSelect(p))
            {
                panel1.Invalidate(); //若判断成功则重新绘图
            }*/
        }

        int CalAllcurves_SelectedCount() // 计算所有曲线被选中的点数
        {
            int cnt = 0;
            for (int i = 0; i < curves.Count; i++)
            {
                Curve a = curves[i];
                int n = a.Calonecurve_SelectedCount(); // 计算一条曲线被选中的点数
                cnt += n;
            }
            return cnt;
        }


        void ModifyAllcurves_SeletedPoint(Point p1, Point p2)
        {
            for (int i = 0; i < curves.Count; i++)
            {
                Curve a = curves[i];
                a.Modifyonecurve_SelectedPoint(p1, p2);
            }
        }

        private void 保存数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFiledialog = new SaveFileDialog();
            if (saveFiledialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFiledialog.FileName;
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                int n = curves.Count;
                bw.Write(n); // 记录曲线条数信息

                for (int i = 0; i < curves.Count; i ++)
                {
                    Curve a = curves[i];
                    a.Save(bw);
                }

                bw.Close();
                fs.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curSelected = listBox1.SelectedIndex;
            Console.WriteLine($"###Before: {curSelected}");
            pictureBox1.Invalidate();
            // curSelected = -1;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.Location;

            if (bMouseDown)
            {
                if (CalAllcurves_SelectedCount() > 0)
                {
                    
                    ModifyAllcurves_SeletedPoint(p1, p);
                    p1 = p;
                    panel1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.Location;

            if (bMouseDown)
            {
                if (CalAllcurves_SelectedCount() > 0)
                {

                    ModifyAllcurves_SeletedPoint(p1, p);
                    p1 = p;
                    pictureBox1.Invalidate(true);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e) // 当窗口缩放时, 在PictureBox中重新绘图
        {
            pictureBox1.Invalidate();
        }
    }
}
