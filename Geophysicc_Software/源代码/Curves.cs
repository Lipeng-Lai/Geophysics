using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeophysicalDataVisualization_FinalVersion
{
    internal class Curves
    {
        public List<Curve> Pcurves = new List<Curve>(); // 一条(多曲线)是 多条曲线的列表

        public Curves DeepCopy() // 深拷贝
        {
            Curves curves = new Curves();
            Curve curve = new Curve();

            for (int i = 0; i < Pcurves.Count; i++)
            {
                curves.Pcurves.Add(curve);
            }

            for (int i = 0; i < Pcurves.Count; i++)
            {
                curves.Pcurves[i] = this.Pcurves[i].DeepCopy();
            }

            return curves;
        }
    }
}
