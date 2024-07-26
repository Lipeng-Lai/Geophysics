using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeophysicalDataVisualization_FinalVersion
{
    internal class PointXY
    {
        public float x; // 横坐标
        public float y; // 纵坐标
        public bool Selected; // 该点是否被鼠标选中

        public PointXY(float _x = -1, float _y = -1, bool status = false) // 构造函数
        {
            x = _x;
            y = _y;
            Selected = status; // 默认为未选中状态
        }

        public PointXY DeepCopy() // 深拷贝
        {
            PointXY p = new PointXY();
            p.x = this.x;
            p.y = this.y;
            p.Selected = this.Selected;
            return p;
        }
    }
}
