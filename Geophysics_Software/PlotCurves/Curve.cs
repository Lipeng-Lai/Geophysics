using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotCurves
{
    internal class Curve
    {
        List<PointXY> Points = new List<PointXY>();
        Color LineColor = Color.Black;
        float LineWidth = 1;
        float minx, maxx;
        float miny, maxy;

        Rectangle winRect;

        public void GetWinRect(Rectangle WinRect)
        {
            winRect.Width = WinRect.Width;
            winRect.Height = WinRect.Height;
        }


        public void Add(float x, float y) // 添加数据
        {
            PointXY newPoint = new PointXY(x, y);
            Points.Add(newPoint);
        }

        public void Test_Add_Data()
        {
            Add(0.5f, 10);
            Add(1, 18);
            Add(2, 25);
            Add(4, 30);
            Add(8, 22);
            Add(15, 16);
            Add(20, 45);
            Add(30, 75);
            Add(60, 100);
        }


        public void Print() // 测试有没有读入数据
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Console.WriteLine("#Test Data x:{0} y:{1} Seleted:{2}",
                    Points[i].x, Points[i].y, Points[i].Selected);
            }
        }

        public void PrintMaxMin()
        {
            Console.WriteLine($"minx: {minx:F}");
            Console.WriteLine($"miny: {miny:F}");
            Console.WriteLine($"maxx: {maxx:F}");
            Console.WriteLine($"maxy: {maxy:F}");
        }

        public void Save(BinaryWriter bw)
        {
            int n = Points.Count;
            bw.Write(n); // 先存入数据个数

            for (int i = 0; i < Points.Count; i++)
            {
                PointXY p = Points[i];
                bw.Write(p.x);
                bw.Write(p.y);
            }
            /*
            bw.Write(LineWidth);
            bw.Write(LineColor.R);
            bw.Write(LineColor.G);
            bw.Write(LineColor.B);*/
        }


        public void Read(BinaryReader br)
        {
            Points.Clear(); // 读之前先清空

            int n = br.ReadInt32(); // 读取数据个数
            Console.WriteLine(n);

            for (int i = 0; i < n; i++)
            {
                float x = br.ReadSingle();
                float y = br.ReadSingle();
                PointXY p = new PointXY(x, y);
                Points.Add(p);
            }

            /*
            LineWidth = float.Parse(sr.ReadLine());
            byte red = byte.Parse(sr.ReadLine());
            byte green = byte.Parse(sr.ReadLine());
            byte blue = byte.Parse(sr.ReadLine());
            LineColor = Color.FromArgb(red, green, blue);*/
        }
    

        public void GetRange()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                PointXY pt = Points[i];
                if (i == 0)
                {
                    minx = maxx = pt.x;
                    miny = maxy = pt.y;
                }
                else
                {
                    if (pt.x < minx) minx = pt.x;
                    if (pt.y < miny) miny = pt.y;
                    if (pt.x > maxx) maxx = pt.x;
                    if (pt.y > maxy) maxy = pt.y;
                }
            }
        }
           
        // 逻辑坐标 -> 设备坐标
        public PointXY LPtoDP(PointXY LP)
        {

            float DPx = winRect.Left + (LP.x - minx) * winRect.Width / (maxx - minx);
            float DPy = winRect.Bottom - (LP.y - miny) * winRect.Height / (maxy - miny);

            /*
            Console.WriteLine($"winRect.Width: {winRect.Width}");
            Console.WriteLine($"winRect.Height: {winRect.Height}");
            Console.WriteLine($"Input point: ({LP.x}, {LP.y})");
            Console.WriteLine($"Output point: ({DPx}, {DPy})");
            */

            return new PointXY(DPx, DPy, LP.Selected); // 构造函数需要状态一致
        }

        // 设备坐标 -> 逻辑坐标
        public PointXY DPtoLP(PointXY DP)
        {
            float LPx = (DP.x - winRect.Left) * (maxx - minx) / (winRect.Width) + minx;
            float LPy = -(DP.y - winRect.Bottom) * (maxy - miny) / (winRect.Height) + miny;
            
            /*
            Console.WriteLine($"winRect.Width: {winRect.Width}");
            Console.WriteLine($"winRect.Height: {winRect.Height}");
            Console.WriteLine($"Input point: ({LPx}, {LPy})");
            Console.WriteLine($"Output point: ({DP.x}, {DP.y})");
            */

            return new PointXY(LPx, LPy, DP.Selected); // 构造函数需要状态一致
        }


        public void Draw(Graphics g, bool SelectedLine)
        {
            float dotSize = 12;
            // this.Print(); // 测试数据是否读入曲线
            this.GetRange(); // 求取Min-Max
            // this.PrintMaxMin(); // 测试Min-Max
            // g.Clear(Color.White); 加了之后不能出现两条曲线在一个框
            for (int i = 0; i < Points.Count - 1; i++)
            {
                PointXY p1 = Points[i];  //获取逻辑点坐标
                PointXY p2 = Points[i + 1];  //获取逻辑点坐标
                p1 = LPtoDP(p1);  //转换成屏幕设备坐标
                p2 = LPtoDP(p2);  //转换成屏幕设备坐标
                // Console.Write("# test:");
                // Console.WriteLine("{0} {1} {2} {3}", p1.x, p1.y, p2.x, p2.y);

                Color grayColor = Color.Gray;
                // Color redColor = Color.Red;
                Pen grayPen = new Pen(grayColor);
                // Pen redPen = new Pen(redColor);

                if (SelectedLine)
                {
                    Color redColor = Color.Red;
                    float LineWidth = 2;
                    Pen redPen = new Pen(redColor, LineWidth);
                    g.DrawLine(redPen, p1.x, p1.y, p2.x, p2.y);
                }
                else
                {
                    g.DrawLine(grayPen, p1.x, p1.y, p2.x, p2.y);
                }

                // g.DrawLine(pen, p1.x, p1.y, p2.x, p2.y);

                if (SelectedLine)
                {
                    if (p1.Selected)
                    {
                        Console.WriteLine("#Test Modify Color Draw Dots");
                        g.DrawRectangle(Pens.Red,
                        p1.x - dotSize / 2,
                        p1.y - dotSize / 2,
                        dotSize, dotSize);
                    }
                    else
                    {
                        g.DrawRectangle(Pens.DarkCyan,
                        p1.x - dotSize / 2,
                        p1.y - dotSize / 2,
                        dotSize, dotSize);
                    }
                }
            }
        }

        public bool DoSelect(Point p) // 将鼠标点击的一点与某一条曲线的所有点进行对比
        {
            float dotSize = 12;
            for (int i = 0; i < Points.Count; i ++)
            {
                PointXY p1 = Points[i]; // 逻辑坐标
                PointXY p2 = LPtoDP(p1); // 设备坐标
                RectangleF rect = new RectangleF(
                    p2.x - dotSize / 2, // 矩阵中心的x坐标
                    p2.y - dotSize / 2, // 矩阵中心的y坐标
                    dotSize, dotSize // 半长, 半宽
                    );

                if (rect.Contains(p))
                {
                    Console.WriteLine("#Test Modify status:");
                    p1.Selected = !p1.Selected; // 选中之后再点即是取消:状态取反
                    Points[i] = p1; // 状态改变后需要重新赋值
                    return true;
                }
            }
            return false;
        }

        public int Calonecurve_SelectedCount() // 计算一条曲线中被选中的点数
        {
            int n = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Selected) n++; // 遍历一条曲线的所有点, 若状态改变, ++
            }
            return n;
        }

        public void Modifyonecurve_SelectedPoint(Point p1, Point p2)
        {
            // 屏幕坐标偏移: 设备坐标之差
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            for (int i = 0; i < Points.Count; i++)
            {
                PointXY p = Points[i];
                if (p.Selected)
                {
                    Console.WriteLine("### Test Moving");
                    Console.WriteLine("#Test Move Before: x:{0} y:{1}", p.x, p.y);
                    p = LPtoDP(p);
                    p.x += dx;
                    p.y += dy;
                    p = DPtoLP(p);
                    Console.WriteLine("#Test Move After: x:{0} y:{1}", p.x, p.y);
                    Points[i] = p;
                }
            }
        }
    }

    struct PointXY
    {
        public float x;
        public float y;
        public bool Selected; // 该点是否被鼠标选中

        public PointXY(float _x, float _y, bool status=false)
        {
            x = _x;
            y = _y;
            Selected = status; // 默认为未选中状态
        }
    }
}
