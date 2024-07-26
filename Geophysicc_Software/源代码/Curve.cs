using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace GeophysicalDataVisualization_FinalVersion
{
    internal class Curve
    {
        public List<PointXY> Points = new List<PointXY>(); // 该曲线的点集合

        /******************************************** 属性 *********************************************/
        public float minx, maxx; // 曲线的范围值(映射为设备坐标时需要)
        public float miny, maxy;
        Rectangle winRect; // 当前PictureBox的边界
        public doubleRect dataRect = new doubleRect(); // 数据范围
        public doubleRect lpRect = new doubleRect(); // 绘制数据范围

        [Browsable(true)]
        public string Name { get; set; } = "未命名";

        [Browsable(true)]
        public Color LineColor { get; set; } = Color.Red;

        [Browsable(true)]
        public float LineWidth { get; set; } = 1;

        [Browsable(true)]
        public DashStyle LineStyle { get; set; } = DashStyle.Solid;

        [Browsable(true)]
        public int Count
        {
            get
            {
                return Points.Count;
            }
        }
        /******************************************** 属性 *********************************************/

        public Curve DeepCopy()         // 深拷贝
        {
             Curve curve = new Curve();

            // curve.Test_Add_Data();

            PointXY p = new PointXY();
            for (int i = 0; i < this.Points.Count; i++)
            {
                curve.Points.Add(p);
            }

            for (int i = 0; i < this.Points.Count; i++)
            {
                curve.Points[i] = this.Points[i].DeepCopy();
                /*curve.Points[i].x = this.Points[i].x;
                curve.Points[i].y = this.Points[i].y;
                curve.Points[i].Selected = this.Points[i].Selected;
                Console.WriteLine("x:{0}, y:{1}, status:{2}", curve.Points[i].x, curve.Points[i].y, curve.Points[i].Selected);*/
            }

            Console.WriteLine("打印深拷贝的信息");
            for (int i = 0; i < curve.Points.Count; i++)
            {
                Console.WriteLine("x:{0}, y:{1}, status:{2}", curve.Points[i].x, curve.Points[i].y, curve.Points[i].Selected);
            }

            curve.minx = this.minx;
            curve.maxx = this.maxx;
            curve.miny = this.miny;
            curve.maxy = this.maxy;
            curve.winRect = this.winRect;
            curve.dataRect = this.dataRect;
            curve.lpRect = this.lpRect;
            curve.LineWidth = this.LineWidth;
            curve.LineColor = this.LineColor;

            return curve;
        }

        public void Debug()         // 打印验证Debug
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", Points[i].x, Points[i].y, Points[i].Selected);
            }
        }

        public void Add(float x, float y) // 添加数据
        {
            PointXY newPoint = new PointXY(x, y);
            Points.Add(newPoint);
        }

        public void Test_Add_Data() // 一组测试数据
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

        public void GetWinRect(Rectangle WinRect) // 获取当前绘制窗口的边界
        {
            winRect = WinRect;
        }

        public void Save(BinaryWriter bw) // 保存为二进制数据
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
            bw.Write(LineColor.B);
            LineColor = Color.FromArgb(red, blue, green);*/
        }

        public void Stream_Save(StreamWriter sw) // 以文本形式保存
        {
            int n = Points.Count;
            sw.Write(n);
            sw.WriteLine(); // 换行
            for (int i = 0; i < Points.Count; i ++)
            {
                PointXY p = Points[i];
                sw.Write(p.x);
                sw.Write(" "); // 用空格分隔浮点数
                sw.Write(p.y);
                sw.WriteLine(); // 换行
            }
        }

        public void Read(BinaryReader br) // 读取二进制数据
        {
            Points.Clear(); // 读之前先清空

            int n = br.ReadInt32(); // 读取数据个数

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

            // lineStyle = br.Read();
        }

        public void GetRange() // 求一条曲线的MinMax
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

        public PointXY LPtoDP(PointXY LP)         // 逻辑坐标 -> 设备坐标
        {
            float minx = Math.Min(lpRect.x1, lpRect.x2);
            float miny = Math.Min(lpRect.y1, lpRect.y2);
            float maxx = Math.Max(lpRect.x1, lpRect.x2);
            float maxy = Math.Max(lpRect.y1, lpRect.y2);

            float DPx = winRect.Left + (LP.x - minx) * winRect.Width / (maxx - minx);
            float DPy = winRect.Bottom - (LP.y - miny) * winRect.Height / (maxy - miny);

            return new PointXY(DPx, DPy, LP.Selected); // 构造函数需要状态一致
        }

        public PointXY DPtoLP(PointXY DP)        // 设备坐标 -> 逻辑坐标
        {
            float minx = Math.Min(lpRect.x1, lpRect.x2);
            float miny = Math.Min(lpRect.y1, lpRect.y2);
            float maxx = Math.Max(lpRect.x1, lpRect.x2);
            float maxy = Math.Max(lpRect.y1, lpRect.y2);

            float LPx = (DP.x - winRect.Left) * (maxx - minx) / (winRect.Width) + minx;
            float LPy = -(DP.y - winRect.Bottom) * (maxy - miny) / (winRect.Height) + miny;

            return new PointXY(LPx, LPy, DP.Selected); // 构造函数需要状态一致
        }

        public void Draw(Graphics g, bool Now_SelectedLine, bool Ever_SelectedLine) // 绘制
        {
            float dotSize = 12;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                PointXY p1 = Points[i];  //获取逻辑点坐标
                PointXY p2 = Points[i + 1];  //获取逻辑点坐标
                p1 = LPtoDP(p1);
                p2 = LPtoDP(p2);  //转换成屏幕设备坐标

                if (Now_SelectedLine) // 如果是当前选择的曲线, 则标记为红色
                {
                    Pen redPen = new Pen(LineColor, LineWidth);
                    g.DrawLine(redPen, p1.x, p1.y, p2.x, p2.y);

                    if (p1.Selected) // 给当前选中的点绘制红色矩形框, 表示当前选中该点
                    {
                        g.DrawRectangle(Pens.Red,
                        p1.x - dotSize / 2,
                        p1.y - dotSize / 2,
                        dotSize, dotSize);
                    }
                    else // 如果没有选中该点, 则绘制为亮蓝色
                    {
                        g.DrawRectangle(Pens.DarkCyan,
                        p1.x - dotSize / 2,
                        p1.y - dotSize / 2,
                        dotSize, dotSize);
                    }
                }
                else if(!Now_SelectedLine)// 如果不是当前选择的曲线, 则标记为灰色
                {
                    if (Ever_SelectedLine)
                    {
                        Pen pen = new Pen(LineColor, LineWidth);
                        g.DrawLine(pen, p1.x, p1.y, p2.x, p2.y);
                    }
                    else if (!Ever_SelectedLine)
                    {
                        Pen GrayPen = new Pen(Color.Gray, LineWidth);
                        g.DrawLine(GrayPen, p1.x, p1.y, p2.x, p2.y);
                    }
                }
            }
        }

        public void ElectricalGetRange()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                float x = Points[i].x;
                if (i == 0)
                {
                    minx = maxx = Points[i].x;
                }
                else
                {
                    if (x < minx) minx = x;
                    if (x > maxx) maxx = x;
                }
            }
            miny = winRect.Top;
            maxy = winRect.Bottom;
        }
         
        public void ElectricalDraw(Graphics g, bool SelectedLine, float Eminx, float Emaxx)
        {
            Color BlackColor = Color.Black;
            Pen BlackPen = new Pen(BlackColor);
            float dotSize = 6;

            // ElectricalGetRange(); // 会导致局部的minx maxx
            minx = Eminx;
            maxx = Emaxx;
            miny = winRect.Top;
            maxy = winRect.Bottom;

            for (int i = 0; i < Points.Count - 1; i++)
            {
                PointXY p1 = Points[i];
                PointXY p2 = Points[i + 1];

                p1 = LPtoDP(p1);
                p2 = LPtoDP(p2);  //转换成屏幕设备坐标
                /*p1.x = winRect.Left + (p1.x - minx) * winRect.Width / (maxx - minx);
                p1.y = winRect.Bottom - (p1.y - miny) * winRect.Height / (maxy - miny);
                p2.x = winRect.Left + (p2.x - minx) * winRect.Width / (maxx - minx);
                p2.y = winRect.Bottom - (p2.y - miny) * winRect.Height / (maxy - miny);*/


                 if (SelectedLine) // 如果是当前选择的曲线, 则标记为红色
                {
                    Color redColor = Color.Red;
                    float LineWidth = 2;
                    Pen redPen = new Pen(redColor, LineWidth);
                    g.DrawLine(redPen, p1.x, p1.y, p2.x, p2.y);

                    if (p1.Selected) // 给当前选中的点绘制红色矩形框, 表示当前选中该点
                    {
                        g.DrawRectangle(Pens.Red,
                        p1.x - dotSize / 2,
                        p1.y - dotSize / 2,
                        dotSize, dotSize);
                    }
                    else // 如果没有选中该点, 则绘制为亮蓝色
                    {
                        g.DrawRectangle(Pens.DarkCyan,
                        p1.x - dotSize / 2,
                        p1.y - dotSize / 2,
                        dotSize, dotSize);
                    }
                }
                else // 如果不是当前选择的曲线, 则标记为灰色
                {
                    g.DrawLine(BlackPen, p1.x, p1.y, p2.x, p2.y);

                    int crossSize = 10;
                    g.DrawLine(Pens.Black, p1.x - crossSize / 2, p1.y, p1.x + crossSize / 2, p1.y); // 横线
                    g.DrawLine(Pens.Black, p1.x, p1.y - crossSize / 2, p1.x, p1.y + crossSize / 2); // 竖线
                }
            }
        }

        public bool DoSelect(Point p) // 将鼠标点击的一点与某一条曲线的所有点进行对比
        {
            float dotSize = 12;
            for (int i = 0; i < Points.Count; i++)
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

        public void ModifyOneCurve_SelectedPoint(Point p1, Point p2)
        {
            // 屏幕坐标偏移: 设备坐标之差
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            for (int i = 0; i < Points.Count; i++)
            {
                PointXY p = Points[i];
                if (p.Selected)
                {
                    p = LPtoDP(p);
                    p.x += dx;
                    p.y += dy;
                    p = DPtoLP(p);
                    Points[i] = p;
                }
            }
        }

        public void ModifyOneCurve_AllPoints(Point p1, Point p2)
        {
            // 屏幕坐标偏移: 设备坐标之差
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            for (int i = 0; i < Points.Count; i++)
            {
                PointXY p = Points[i];
                p = LPtoDP(p);
                p.x += dx;
                p.y += dy;
                p = DPtoLP(p);
                Points[i] = p;
            }
        }
    }

    struct doubleRect // 窗口上下左右边界
    {
        public float x1;
        public float x2;
        public float y1;
        public float y2;

        public doubleRect(float _x1, float _x2, float _y1, float _y2)
        {
            x1 = _x1;
            x2 = _x2;
            y1 = _y1;
            y2 = _y2;
        }
    }
}
