using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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


        public void Add(float x, float y) // 添加数据
        {
            PointXY newPoint = new PointXY(x, y);
            Points.Add(newPoint);
        }

        public void Print() // 测试有没有读入数据
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Console.WriteLine(Points[i].x);
                Console.WriteLine(Points[i].y);
            }
        }

        public void PrintMaxMin()
        {
            Console.WriteLine($"minx: {minx:F}");
            Console.WriteLine($"miny: {miny:F}");
            Console.WriteLine($"maxx: {maxx:F}");
            Console.WriteLine($"maxy: {maxy:F}");

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

        public PointXY LPtoDP(PointXY p, Rectangle winRect)
        {

            float x = winRect.Left + (p.x - minx) * winRect.Width / (maxx - minx);
            float y = winRect.Bottom - (p.y - miny) * winRect.Height / (maxy - miny);

            Console.WriteLine($"{winRect.Width}");
            Console.WriteLine($"{winRect.Width}");
            Console.WriteLine($"Input point: ({p.x}, {p.y})");
            Console.WriteLine($"Output point: ({x}, {y})");

            return new PointXY(x, y);
        }

        public void Draw(Graphics g, Rectangle rect)
        {
            GetRange(); // 获取参数minx, miny, maxx, maxy
            Pen pen = new Pen(LineColor, LineWidth);
            for (int i = 0; i < Points.Count - 1; i++)
            {
                PointXY p1 = Points[i];  //获取逻辑点坐标
                PointXY p2 = Points[i + 1];  //获取逻辑点坐标
                p1 = LPtoDP(p1, rect);  //转换成屏幕设备坐标
                p2 = LPtoDP(p2, rect);  //转换成屏幕设备坐标
                Console.Write("# test:");
                Console.WriteLine(p1.x + " ", p1.y + " ", p2.x + " ", p2.y + " ");

                g.DrawLine(pen, p1.x, p1.y, p2.x, p2.y);
            }
        }
    }

    struct PointXY
    {
        public float x;
        public float y;


        public PointXY(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    }
}
