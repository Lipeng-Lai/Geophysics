using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace GeophysicalDataVisualization_FinalVersion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Curve> curves = new List<Curve>();
        /******************************************** 全局变量定义区 *********************************************/
        Curves CurrentCurves = new Curves(); // 当前绘制的一组曲线
        Curves BackCurves = new Curves(); // 初始数据曲线: 用来还原的
        Stack<Curves> undoCurves = new Stack<Curves>(); // 撤销过的历史记录
        Stack<Curves> redoCurves = new Stack<Curves>(); // 重做过的历史记录
        bool Modified = false; // 曲线是否已经被修改

        Point p1, p2; // 分别表示鼠标点击和松开时的坐标
        bool bMouseDown = false; // 初始状态未选中 : 鼠标选点并标记
        bool bMouseDrag = false; // 放大功能的标注
        int curSelected = -1; // 判断当前选中的是第几条曲线
        float global_minx, global_maxx; // 所有曲线的MinMax
        float global_miny, global_maxy;
        bool UndoFlag = false; // 判断此时画图是画 鼠标移动还是撤销还原

        bool HighDensityElectrical = false; // 高密度电阻率绘图方式不一样
        List<(float, float)> grids = new List<(float, float)>(); // 高密度电阻率所需要的网格划分

        List<int> Ever_SelectedLine = new List<int>(); // 曾经选择过的曲线

        enum MouseStateEnum // 鼠标状态
        {
            Normal = 0,
            ZoomIn = 1,
            ZoomOut = 2,
            ZoomMove = 3 // 平移操作的鼠标拖动
        }
        MouseStateEnum MouseState = MouseStateEnum.Normal;

        /******************************************** 全局变量定义区 *********************************************/



        /******************************************** 文件Begin *********************************************/
        private void 文件FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 读入二进制数据LoadBinaryDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDataWhenReadNewData(); // 读之前清空数据
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true; // 多文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        int n = br.ReadInt32(); // 曲线的条数
                        for (int i = 0; i < n; i ++)
                        {
                            Curve a = new Curve();
                            a.Read(br);
                            CurrentCurves.Pcurves.Add(a); // 当前组曲线增加这条曲线
                        }
                        // 关闭文件流
                        br.Close();
                        fs.Close();
                    }
                    
                    {
                        // 数据备份
                    }

                    // 对于每一条曲线, 增加它的文件名到ListBox中
                    string ListBox_filename = Path.GetFileName(filename);
                    listBox1.Items.Add(ListBox_filename);
                }
                UpdateDataRange(); // 更新全局数据范围

                // 撤销操作: 需要把当前元素压栈

                // Debug
                /*Console.WriteLine("打印CurrentCuves");
                for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                {
                    Curve a = CurrentCurves.Pcurves[i];
                    a.Debug();
                }*/

                Curves Temp = CurrentCurves.DeepCopy(); // 深拷贝
                BackCurves = CurrentCurves.DeepCopy();
                // 鉴定为深拷贝
                undoCurves.Push(Temp);
                // 撤销操作

                pictureBox1.Invalidate(); // 调用在picutureBox1中绘图 
            }
        }

        void UpdateDataRange() // 得到该组曲线的范围min, max
        {
            int n = CurrentCurves.Pcurves.Count; // 当前曲线的数量
            for (int i = 0; i < n; i++)
            {
                Curve a = CurrentCurves.Pcurves[i]; // 取出一条曲线
                a.GetRange(); // 得到一条曲线的范围
                if (i == 0) // 如果是第一条曲线, 则将全局的范围初始化为第一条曲线的范围
                {
                    global_minx = a.minx;
                    global_maxx = a.maxx;
                    global_miny = a.miny;
                    global_maxy = a.maxy;
                }
                else // 否则, 将全局的范围和一条曲线的范围作比较, 得到全局的范围
                {
                    if (a.minx < global_minx) global_minx = a.minx;
                    if (a.miny < global_miny) global_miny = a.miny;
                    if (a.maxx > global_maxx) global_maxx = a.maxx;
                    if (a.maxy > global_maxy) global_maxy = a.maxy;
                }
            }

            for (int i = 0; i < n; i++) // 初始化边界
            {
                Curve a = CurrentCurves.Pcurves[i];
                a.minx = global_minx;
                a.miny = global_miny;
                a.maxx = global_maxx;
                a.maxy = global_maxy;
                a.dataRect = new doubleRect(global_minx, global_maxx, global_miny, global_maxy);
                a.lpRect = new doubleRect(global_minx, global_maxx, global_miny, global_maxy);

            }
        }

        private void 保存为二进制数据SaveBinaryDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFiledialog = new SaveFileDialog();
            if (saveFiledialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFiledialog.FileName;
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                int n = CurrentCurves.Pcurves.Count;
                bw.Write(n); // 记录曲线条数信息

                for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                {
                    Curve a = CurrentCurves.Pcurves[i];
                    a.Save(bw);
                }

                bw.Close();
                fs.Close();
            }
        }

        private void 导出数据ExportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    int n = CurrentCurves.Pcurves.Count;
                    sw.WriteLine(n); // 记录曲线条数信息

                    for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                    {
                        Curve a = CurrentCurves.Pcurves[i];
                        a.Stream_Save(sw);
                    }
                }
            }
        }

        private void 退出ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定退出吗？ 请确认你已经保存好了数据", "退出确认", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                // 用户点击了 "确定", 退出程序
                Application.Exit();
            }
            // 如果用户点击了 "取消", 则什么都不做
        }

        /******************************************** 文件End *********************************************/

        /******************************************** 数据Begin *********************************************/

        private void 数据DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ClearDataWhenReadNewData();
        }

        void ClearDataWhenReadNewData()
        {
            /******************************************** 全局变量定义区 *********************************************/
            CurrentCurves.Pcurves = new List<Curve>(); // 全局曲线列表
            bMouseDown = false; // 初始状态未选中 : 鼠标选点并标记
            bMouseDrag = false; // 放大功能的标注
            curSelected = -1; // 判断当前选中的是第几条曲线

            HighDensityElectrical = false; // 高密度电阻率绘图方式不一样
            grids = new List<(float, float)>(); // 高密度电阻率所需要的网格划分

            // 撤销和重做的属性定义
            // Pcurves = new Curves();
            // curves = new Curves(); // 当前绘制多条曲线数据
            undoCurves = new Stack<Curves>(); // 撤销过的历史记录
            redoCurves = new Stack<Curves>(); // 重做过的历史记录
            Modified = false; // 曲线是否已经被修改

            UndoFlag = false;
            List<int> Ever_SelectedLine = new List<int>(); // 曾经选择过的曲线

            UpdateDataRange();
            UpdateListBox();
            pictureBox1.Invalidate();
            /******************************************** 全局变量定义区 *********************************************/
        }

        void UpdateListBox()
        {

            listBox1.Items.Clear();
            for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
            {
                string name = CurrentCurves.Pcurves[i].Name;
                listBox1.Items.Add(name);
            }
        }

        private void mT正演数据ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                FileStream fs = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                string line;
                char[] spilteChars = new char[] { ' ', ',', ';', '\t' }; // 空格, 逗号, 分号, Tab

                int LineIndex = 0;
                Curve a = new Curve();

                // float global_minx = 0, global_miny = 0;
                // float global_maxx = 0, global_

                while ((line = sr.ReadLine()) != null)
                {
                    if (LineIndex == 0)
                    {
                        LineIndex++;
                        continue;
                    }

                    string[] ss = line.Split(spilteChars);
                    Name = ss[0]; // 点号
                    float x = float.Parse(ss[2]); // 频率
                    float y = float.Parse(ss[3]); // 相位

                    if (LineIndex == 1)
                    {
                        global_minx = global_maxx = x;
                        global_miny = global_maxy = y;
                    }
                    else
                    {
                        if (x < global_minx) global_minx = x;
                        if (y < global_miny) global_miny = y;
                        if (x > global_maxx) global_maxx = x;
                        if (y > global_maxy) global_maxy = y;
                    }

                    if (a.Name == Name) // 旧曲线
                    {
                        a.Add(x, y);
                    }
                    else // 说明此时点数不同, 旧曲线的坐标已经读完, 需要添加换一条新曲线读 或 此时是空曲线
                    {
                        if (a.Count > 0) CurrentCurves.Pcurves.Add(a);
                        a = new Curve();
                        a.Name = Name;
                        a.Add(x, y);
                    }

                    LineIndex++;
                }
                CurrentCurves.Pcurves.Add(a); // 补充最后一条
                // Curve.GetGlobalMaxMin(global_minx, global_miny, global_maxx, global_maxy); // 单例模式
                UpdateDataRange();

                UpdateListBox();

                sr.Close();
                fs.Close();

                BackCurves = CurrentCurves.DeepCopy();
                pictureBox1.Invalidate(); // 调用在picutureBox1中绘图

                // string ListBox_filename = Path.GetFileName(filename);
                // listBox1.Items.Add(ListBox_filename);
            }
        }

        private void 大地电磁数据EdiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true; // 多文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialog.FileNames)
                {

                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        SeekMark(sr, ">FREQ");
                        ReadFrequncies(sr);

                        SeekMark(sr, "ZXXR");
                        ReadResZXX(sr);
                    }
                    UpdateDataRange();
                    string ListBox_filename = Path.GetFileName(filename);
                    listBox1.Items.Add(ListBox_filename);
                }
                BackCurves = CurrentCurves.DeepCopy();
                // curves.Add(a);
                // Curve.GetGlobalMaxMin(global_minx, global_miny, global_maxx, global_maxy);
                // sr.Close(); using 自动关闭文件流
                // fs.Close();
                // UpdateListBox(); // 加了会使得左边栏为 "未命名"
                pictureBox1.Invalidate();

            }
        }

        bool SeekMark(StreamReader sr, string mark)
        {
            string line;
            int LineIdx = 1;
            char[] spliteChars = new char[] { ' ', ',', ';', '\t' };
            while ((line = sr.ReadLine()) != null)
            {
                if (line == String.Empty) continue;
                if (line.Contains(mark))
                {
                    // Console.WriteLine($"####Test LineIdx: {0}", LineIdx);
                    return true;
                }
                LineIdx++;
            }
            return false;
        }

        void ReadFrequncies(StreamReader sr)
        {
            string line;
            char[] spliteChars = new char[] { ' ', ',', ';', '\t' };
            Curve a = new Curve();
            while ((line = sr.ReadLine()) != null)
            {
                if (line == String.Empty) continue;
                if (line[0] == '>')
                {
                    break;
                }
                string[] ss = line.Split(spliteChars);
                for (int i = 0; i < ss.Length; i++)
                {
                    // float x = float.Parse(ss[i]);
                    // a.Add(x, 0);
                    if (float.TryParse(ss[i], out float x))
                    {
                        x = (float)Math.Log(x);
                        if (i == 0) global_minx = global_maxx = x;
                        else
                        {
                            if (x < global_minx) global_minx = x;
                            if (x > global_maxx) global_maxx = x;
                        }
                        a.Add(x, 0);
                    }
                    else
                    {

                    }
                }
            }
            CurrentCurves.Pcurves.Add(a);
        }

        void ReadResZXX(StreamReader sr)
        {
            string line;
            char[] spliteChars = new char[] { ' ', ',', ';', '\t' };
            int n = CurrentCurves.Pcurves.Count;
            Curve a = CurrentCurves.Pcurves[n - 1];

            while ((line = sr.ReadLine()) != null)
            {
                if (line == String.Empty) continue;

                string[] ss = line.Split(spliteChars);
                for (int i = 0; i < ss.Length; i++)
                {
                    /*
                    float y = float.Parse(ss[i]);
                    PointXY p = a.Points[i];
                    p.y = y;
                    a.Points[i] = p;*/
                    if (float.TryParse(ss[i], out float y))
                    {
                        // y = (float)Math.Log10(y);
                        if (i == 0) global_miny = global_maxy = y;
                        else
                        {
                            if (y < global_miny) global_miny = y;
                            if (y > global_maxy) global_maxy = y;
                        }
                        PointXY p = a.Points[i];
                        p.y = y;
                        a.Points[i] = p;
                    }
                    else
                    {

                    }
                }
            }
        }

        private void 磁测ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 电测深数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm; *.csv"; // 仅显示 Excel 文件
            openFileDialog.ValidateNames = true;
            openFileDialog.Multiselect = false; // 单文件
            openFileDialog.Title = "选择 Excel 文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(fs);

                // 读取数据
                DataSet result = reader.AsDataSet();

                // 获取第一个工作表
                DataTable table = result.Tables[0];

                // 处理数据
                Curve a = new Curve();
                for (int i = 1; i < table.Rows.Count; i++) // 从索引 1 开始，跳过第一行
                {
                    DataRow row = table.Rows[i];
                    float x = 0, y = 0;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        string cellValue = row[j].ToString();

                        if (j == 0) x = float.Parse(cellValue);
                        else if (j == 1)
                        {
                            y = float.Parse(cellValue);
                            Console.WriteLine($"###TEST:   x:{0}    y:{0}", x, y);
                            a.Add(x, y);
                        }
                    }
                }
                CurrentCurves.Pcurves.Add(a);

                // 关闭数据阅读器和文件流
                reader.Close();
                fs.Close();
                UpdateListBox();
                UpdateDataRange();
                BackCurves = CurrentCurves.DeepCopy();
                pictureBox1.Invalidate();
            }
        }

        private void 高密度电阻率数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm; *.csv"; // 仅显示 Excel 文件
            openFileDialog.ValidateNames = true;
            openFileDialog.Multiselect = false; // 单文件
            openFileDialog.Title = "选择 Excel 文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(fs);

                // 读取数据
                DataSet result = reader.AsDataSet();

                // 获取第一个工作表
                DataTable table = result.Tables[0];

                // 点距为x, 视电阻率为y
                string IsolationFactor = "1";
                Curve a = new Curve();
                for (int i = 1; i < table.Rows.Count; i++) // 从索引 1 开始，跳过第一行
                {
                    DataRow row = table.Rows[i];
                    float x = 0, y = 0;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        string cellValue = row[j].ToString();

                        if (j == 0)
                        {
                            // 如果是第一条 或 与前一条不同 就说明此时是新的曲线
                            if (cellValue.Equals(IsolationFactor) == false)
                            {
                                CurrentCurves.Pcurves.Add(a); // 先把旧的加进去
                                IsolationFactor = cellValue;
                                a = new Curve(); // 后创建新的
                            }
                        }
                        else if (j == 1)
                        {
                            x = float.Parse(cellValue);
                            if (x < global_minx) global_minx = x;
                            if (x > global_maxx) global_maxx = x;
                        }
                        else if (j == 2)
                        {
                            y = float.Parse(cellValue);
                            a.Add(x, y);
                        }

                    }
                }
                CurrentCurves.Pcurves.Add(a); // 防止遗漏最后一条曲线
                Console.WriteLine("The Count of curves:", CurrentCurves.Pcurves.Count);

                float GridHeight = pictureBox1.Height / (CurrentCurves.Pcurves.Count) + 30; // 格子高度

                // 添加上下边界
                for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                {
                    float GridUp = i * GridHeight / 2;
                    float GridDown = (i + 1) * GridHeight / 2;
                    grids.Add((GridUp, GridDown));
                }

                Console.WriteLine("The Count of grid: ", grids.Count);

                UpdateListBox();
                UpdateDataRange(); // 要求出全局的minx maxx

                reader.Close();
                fs.Close();
                BackCurves = CurrentCurves.DeepCopy();
                HighDensityElectrical = true;
                pictureBox1.Invalidate(); // 调用在picutureBox1中绘图
            }
          }

        /******************************************** 数据End *********************************************/

        /******************************************** 编辑Begin *********************************************/
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 撤销UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Console.WriteLine("撤销!!!");
            Console.WriteLine("栈元素个数：{0}", undoCurves.Count);

            redoCurves = new Stack<Curves>(undoCurves);
            while (redoCurves.Count > 0)
            {
                Curves a = redoCurves.Pop();
                for (int i = 0; i < a.Pcurves.Count; i++)
                {
                    a.Pcurves[i].Debug();
                }
            }*/
            try
            {
                Curves a = undoCurves.Pop(); // 取出了当前的栈顶, 那么画图时, 实际上是栈顶的前一个元素
                redoCurves.Push(a);
            }
            catch(Exception ex)
            {
                MessageBox.Show("撤销操作需要至少一个初始状态, 错误信息为：" + ex.Message);
            }


            /*if (undoCurves.Count > 0)
            {
                
            }*/
            UndoFlag = true;
            pictureBox1.Invalidate(); // 撤销后 重绘
        }

        private void 重做RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 清空ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDataWhenReadNewData();
        }

        private void 查看ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 放大ZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("放大");
            MouseState = MouseStateEnum.ZoomIn; //鼠标状态切换
            Cursor = Cursors.Cross; //改变鼠标形状
            p1 = p2 = new Point(-1, -1);//初始化点坐标
        }

        private void 缩小ZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("缩小");
            MouseState = MouseStateEnum.ZoomOut; //鼠标状态切换
            Cursor = Cursors.Cross; //改变鼠标形状
            p1 = p2 = new Point(-1, -1);//初始化点坐标
        }

        private void 平移PanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("平移");
            MouseState = MouseStateEnum.ZoomMove; //鼠标状态切换
            Cursor = Cursors.Cross; //改变鼠标形状
            p1 = p2 = new Point(-1, -1);//初始化点坐标
        }

        private void 还原ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ClearDataWhenReadNewData();
            curSelected = -1;
            CurrentCurves = BackCurves.DeepCopy();
            UpdateDataRange(); // 重新计算数据范围
            pictureBox1.Invalidate();
        }

        private void 正常选中SelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("回归正常选点模式");
            MouseState = MouseStateEnum.Normal; //鼠标状态切换
            Cursor = Cursors.Arrow;
        }


        /******************************************** 编辑End *********************************************/

        /******************************************** 帮助Begin *********************************************/

        private void 帮助HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void 教程TuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }

        /******************************************** 帮助End *********************************************/

        /******************************************** 绘图Begin *********************************************/
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawCurves(e.Graphics);
        }

        void DrawCurves(Graphics g)
        {
            g.Clear(Color.White); // 刷新窗口

            /******************************************** 查看功能的矩形框 *********************************************/
            if ((MouseState == MouseStateEnum.ZoomIn || MouseState == MouseStateEnum.ZoomOut) && bMouseDrag)
            {
                Console.WriteLine("重绘矩形");
                Point p2 = pictureBox1.PointToClient(MousePosition); // 获取当前鼠标位置
                Rectangle rect = new Rectangle(
                    Math.Min(p1.X, p2.X), // 左上角的 X 坐标
                    Math.Min(p1.Y, p2.Y), // 左上角的 Y 坐标
                    Math.Abs(p2.X - p1.X), // 宽度
                    Math.Abs(p2.Y - p1.Y) // 高度
                );

                // 获取 Graphics 对象
                {
                    // 绘制矩形
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
            }
            /******************************************** 查看功能的矩形框 *********************************************/
            Rectangle winRect = new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height); // 获取当前窗口范围
            if (!UndoFlag)
            {
                for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                {
                    Curve a = CurrentCurves.Pcurves[i];

                    if (HighDensityElectrical)
                    {
                        winRect = new Rectangle(0, (int)grids[i].Item1 + 10, this.pictureBox1.Width,
                            (int)grids[i].Item2 - (int)grids[i].Item1); // 左端点 + 向右的宽度 + 向下的高度
                        Console.WriteLine($"###Test: {i}  Item1: {grids[i].Item1 + 10}, Item2: " +
                            $"{grids[i].Item2 + 10}, Height:{this.pictureBox1.Height}");

                        a.GetWinRect(winRect); // 获取picturebox的属性

                        if (i == curSelected)
                        {
                            a.ElectricalDraw(g, true, global_minx, global_maxx);
                        }
                        else
                        {
                            a.ElectricalDraw(g, false, global_minx, global_maxx);
                        }

                        continue;
                    }


                    a.GetWinRect(winRect); // 传递窗口范围
                    if (i == curSelected)
                    {
                        a.Draw(g, true, true);
                        Ever_SelectedLine.Add(curSelected); // 将被选中的这条曲线的索引加入列表
                    }
                    else
                    {
                        if (Ever_SelectedLine.Contains(i))
                        {
                            a.Draw(g, false, true);
                        }
                        else
                        {
                            a.Draw(g, false, false);
                        }
                    }
                }

                
            }
            else
            {
                if (undoCurves.Count > 0)
                {
                    /*因为曲线移动过程中, CurrentCurve在变化, 而直至松手时, 才会进行压栈，故
                    * 要解决这个问题, 需要满足以下条件：
                     *(1): 必须对移动的曲线CurrentCurves绘图
                     *(2): 由于画图这个操作本质上不修改曲线数据, 故可以对其进行软拷贝
                     */
                    CurrentCurves = undoCurves.Peek().DeepCopy();
                    // CurrentCurves = undoCurves.Peek().DeepCopy();
                    for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                    {
                        Curve a = CurrentCurves.Pcurves[i];
                        a.Debug();
                    }

                    for (int i = 0; i < CurrentCurves.Pcurves.Count(); i++)
                    {
                        Curve a = CurrentCurves.Pcurves[i];
                        a.GetWinRect(winRect); // 传递窗口范围

                        if (i == curSelected)
                        {
                            a.Draw(g, true, true);
                            Ever_SelectedLine.Add(curSelected); // 将被选中的这条曲线的索引加入列表
                        }
                        else
                        {
                            if (Ever_SelectedLine.Contains(i))
                            {
                                a.Draw(g, false, true);
                            }
                            else
                            {
                                a.Draw(g, false, false);
                            }
                        }
                    }

                    UndoFlag = false; // 还原状态
                }
            }
        }

        bool DoSelect(Point p) // 判断鼠标点击的某点是否在曲线点内
        {
            for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
            {
                Curve a = CurrentCurves.Pcurves[i]; // 取出一条曲线
                if (a.DoSelect(p))
                {
                    return true;
                }
            }
            return false;
         }

        int CalAllcurves_SelectedCount() // 计算所有曲线被选中的点数
        {
            int cnt = 0;
            for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
            {
                Curve a = CurrentCurves.Pcurves[i];
                int n = a.Calonecurve_SelectedCount(); // 计算一条曲线被选中的点数
                cnt += n;
            }
            return cnt;
        }

        void ModifyAllcurves_SeletedPoint(Point p1, Point p2) // 只移动选择的点
        {

            for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
            {
                Curve a = CurrentCurves.Pcurves[i];
                a.ModifyOneCurve_SelectedPoint(p1, p2); // 根据一条曲线移动的位置修改一条曲线
            }
        }

        void ModifyAllcurvesByMoving(Point p1, Point p2) // 平移操作：移动所有的点
        {
            for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
            {
                Curve a = CurrentCurves.Pcurves[i];
                a.ModifyOneCurve_AllPoints(p1, p2);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curSelected = listBox1.SelectedIndex;

            // 增加空间Property 增加属性框的关键, 但是目前还没有改变属性框中的属性, 现实曲线也跟着改变
            Curve selectedCurve = CurrentCurves.Pcurves[curSelected];
            propertyGrid1.SelectedObject = selectedCurve; // 将选中的曲线设置为属性网格的选定对象

            pictureBox1.Invalidate();
        }

        private void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            if (curSelected >= 0 && curSelected < CurrentCurves.Pcurves.Count)
            {
                Curve selectedCurve = CurrentCurves.Pcurves[curSelected];
                propertyGrid1.SelectedObject = selectedCurve; // 将选中的曲线设置为属性网格的选定对象
                pictureBox1.Invalidate();
            }
            // pictureBox1.Invalidate();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // 检查所选曲线是否存在并且属性网格中的属性值已更改
            if (curSelected >= 0 && curSelected < CurrentCurves.Pcurves.Count && e.ChangedItem != null)
            {
                // 获取所选曲线
                Curve selectedCurve = CurrentCurves.Pcurves[curSelected];

                // 检查更改的属性名称，并根据不同的属性名称更新曲线属性
                switch (e.ChangedItem.Label)
                {
                    case "LineColor":
                        selectedCurve.LineColor = (Color)e.ChangedItem.Value;
                        break;
                    case "LineWidth":
                        selectedCurve.LineWidth = (float)e.ChangedItem.Value;
                        break;
                        // 添加其他属性的处理逻辑...
                }

                // 重新绘制曲线
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /******************************************** 绘图End *********************************************/


        /******************************************** 鼠标操作 *********************************************/
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            p1 = p; // p1记录被点击时的位置
            Console.WriteLine("点击鼠标");

            if (p1.X >= 0 && p1.X <= pictureBox1.Width && p1.Y >= 0 && p1.Y <= pictureBox1.Height)
            {
                if (Cursor == Cursors.Arrow) // 箭头
                {
                    
                    if (DoSelect(p))
                    {
                        bMouseDown = true; // 选点鼠标点击, 状态改变为true
                        pictureBox1.Invalidate(); //若判断成功则重新绘图
                    }
                }

                if (Cursor == Cursors.Cross) // 十字标
                {
                    bMouseDrag = true; // 矩形拉框鼠标点击, 状态改变为true
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            // 如果鼠标正在拖动且是放大状态
            if ((MouseState == MouseStateEnum.ZoomIn || MouseState == MouseStateEnum.ZoomOut) && bMouseDrag && Cursor == Cursors.Cross)
            {
                pictureBox1.Invalidate();
            }

            if (p.X >= 0 && p.X <= pictureBox1.Width && p.Y >= 0 && p.Y <= pictureBox1.Height)
            {
                if (CalAllcurves_SelectedCount() > 0 && Cursor == Cursors.Arrow && bMouseDown)
                {
                    ModifyAllcurves_SeletedPoint(p1, p); // 移动选择的点
                    p1 = p;
                    Console.WriteLine("修改!!!");
                    Modified = true; // 在鼠标拖动中, 修改点后, 标记曲线已经被修改
                    pictureBox1.Invalidate();
                }
                if (MouseState == MouseStateEnum.ZoomMove && Cursor == Cursors.Cross && bMouseDrag)
                {
                    ModifyAllcurvesByMoving(p1, p); // 随着鼠标的移动，所有点都动
                    p1 = p;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            p2 = p; // p2记录鼠标松手时的位置

            if (p2.X >= 0 && p2.X <= pictureBox1.Width && p2.Y >= 0 && p2.Y <= pictureBox1.Height && Cursor == Cursors.Arrow)
            {
                bMouseDown = false; // 选点鼠标点击, 状态改变为true 
                if (Modified) // 如果修改过了, 就把当前所有的数据压入栈中
                {
                    Curves Temp = CurrentCurves.DeepCopy();
                    undoCurves.Push(Temp);
                    Modified = false;
                }
                pictureBox1.Invalidate();
            }

            if (MouseState == MouseStateEnum.ZoomIn && Cursor == Cursors.Cross)
            {
                // 获得拉框的四个角点坐标轴 屏幕坐标 设备坐标
                float x1 = Math.Min(p1.X, p2.X);
                float x2 = Math.Max(p1.X, p2.X);
                float y1 = Math.Min(p1.Y, p2.Y);
                float y2 = Math.Max(p1.Y, p2.Y);

                for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                {
                    Curve a = CurrentCurves.Pcurves[i];
                    PointXY LeftPoint = new PointXY(x1, y1);
                    PointXY RightPoint = new PointXY(x2, y2);
                    LeftPoint = a.DPtoLP(LeftPoint); // 设备坐标转为逻辑坐标
                    RightPoint = a.DPtoLP(RightPoint);
                    a.lpRect.x1 = Math.Min(LeftPoint.x, RightPoint.x);
                    a.lpRect.y1 = Math.Min(LeftPoint.y, RightPoint.y);
                    a.lpRect.x2 = Math.Max(LeftPoint.x, RightPoint.x);
                    a.lpRect.y2 = Math.Max(LeftPoint.y, RightPoint.y);
                }

                pictureBox1.Invalidate();
                bMouseDrag = false; // 对松手的该点标记状态
                p1 = p2 = new Point(-1, -1);
            }
            else if (MouseState == MouseStateEnum.ZoomOut && Cursor == Cursors.Cross)
            {
                // 获得拉框的四个角点坐标轴 屏幕坐标 设备坐标
                float x1 = Math.Min(p1.X, p2.X);
                float x2 = Math.Max(p1.X, p2.X);
                float y1 = Math.Min(p1.Y, p2.Y);
                float y2 = Math.Max(p1.Y, p2.Y);

                // 扩展边界 -> 左上角(x1, y1)减 -> 右下角(x2, y2)加
                const int Offset = 200; // 偏移量
                x1 = Math.Min(0, x1 - Offset);
                x2 = Math.Max(this.pictureBox1.Width, x2 + Offset);
                y1 = Math.Min(0, y1 - Offset);
                y2 = Math.Max(this.pictureBox1.Height, y2 + Offset);

                for (int i = 0; i < CurrentCurves.Pcurves.Count; i++)
                {
                    // Curve a = curves[i];
                    Curve a = CurrentCurves.Pcurves[i];
                    PointXY LeftPoint = new PointXY(x1, y1);
                    PointXY RightPoint = new PointXY(x2, y2);
                    LeftPoint = a.DPtoLP(LeftPoint); // 设备坐标转为逻辑坐标
                    RightPoint = a.DPtoLP(RightPoint);
                    a.lpRect.x1 = Math.Min(LeftPoint.x, RightPoint.x);
                    a.lpRect.y1 = Math.Min(LeftPoint.y, RightPoint.y);
                    a.lpRect.x2 = Math.Max(LeftPoint.x, RightPoint.x);
                    a.lpRect.y2 = Math.Max(LeftPoint.y, RightPoint.y);
                }

                pictureBox1.Invalidate();
                bMouseDrag = false; // 对松手的该点标记状态
                p1 = p2 = new Point(-1, -1);
            }
            bMouseDrag = false; // 对松手的该点标记状态
        }


        /******************************************** 鼠标操作 *********************************************/
    }
}
