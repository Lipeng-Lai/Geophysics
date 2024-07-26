namespace GeophysicalDataVisualization_FinalVersion
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.文件FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读入二进制数据LoadBinaryDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存为二进制数据SaveBinaryDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出数据ExportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据DataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mT正演数据ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大地电磁数据EdiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.磁测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电测深数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高密度电阻率数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重做RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ZoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平移PanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还原ResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.正常选中SelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.教程TuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Location = new System.Drawing.Point(0, 32);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1281, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FileToolStripMenuItem,
            this.数据DataToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.查看ViewToolStripMenuItem,
            this.帮助HelpToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1281, 32);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // 文件FileToolStripMenuItem
            // 
            this.文件FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.读入二进制数据LoadBinaryDataToolStripMenuItem,
            this.保存为二进制数据SaveBinaryDataToolStripMenuItem,
            this.导出数据ExportDataToolStripMenuItem,
            this.退出ExitToolStripMenuItem});
            this.文件FileToolStripMenuItem.Name = "文件FileToolStripMenuItem";
            this.文件FileToolStripMenuItem.Size = new System.Drawing.Size(104, 28);
            this.文件FileToolStripMenuItem.Text = "文件(File)";
            this.文件FileToolStripMenuItem.Click += new System.EventHandler(this.文件FileToolStripMenuItem_Click);
            // 
            // 读入二进制数据LoadBinaryDataToolStripMenuItem
            // 
            this.读入二进制数据LoadBinaryDataToolStripMenuItem.Name = "读入二进制数据LoadBinaryDataToolStripMenuItem";
            this.读入二进制数据LoadBinaryDataToolStripMenuItem.Size = new System.Drawing.Size(415, 34);
            this.读入二进制数据LoadBinaryDataToolStripMenuItem.Text = "读入二进制数据(Load Binary Data)";
            this.读入二进制数据LoadBinaryDataToolStripMenuItem.Click += new System.EventHandler(this.读入二进制数据LoadBinaryDataToolStripMenuItem_Click);
            // 
            // 保存为二进制数据SaveBinaryDataToolStripMenuItem
            // 
            this.保存为二进制数据SaveBinaryDataToolStripMenuItem.Name = "保存为二进制数据SaveBinaryDataToolStripMenuItem";
            this.保存为二进制数据SaveBinaryDataToolStripMenuItem.Size = new System.Drawing.Size(415, 34);
            this.保存为二进制数据SaveBinaryDataToolStripMenuItem.Text = "保存为二进制数据(Save Binary Data) ";
            this.保存为二进制数据SaveBinaryDataToolStripMenuItem.Click += new System.EventHandler(this.保存为二进制数据SaveBinaryDataToolStripMenuItem_Click);
            // 
            // 导出数据ExportDataToolStripMenuItem
            // 
            this.导出数据ExportDataToolStripMenuItem.Name = "导出数据ExportDataToolStripMenuItem";
            this.导出数据ExportDataToolStripMenuItem.Size = new System.Drawing.Size(415, 34);
            this.导出数据ExportDataToolStripMenuItem.Text = "导出数据(Export Data)";
            this.导出数据ExportDataToolStripMenuItem.Click += new System.EventHandler(this.导出数据ExportDataToolStripMenuItem_Click);
            // 
            // 退出ExitToolStripMenuItem
            // 
            this.退出ExitToolStripMenuItem.Name = "退出ExitToolStripMenuItem";
            this.退出ExitToolStripMenuItem.Size = new System.Drawing.Size(415, 34);
            this.退出ExitToolStripMenuItem.Text = "退出(Exit)";
            this.退出ExitToolStripMenuItem.Click += new System.EventHandler(this.退出ExitToolStripMenuItem_Click);
            // 
            // 数据DataToolStripMenuItem
            // 
            this.数据DataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mT正演数据ExcelToolStripMenuItem,
            this.大地电磁数据EdiToolStripMenuItem,
            this.磁测ToolStripMenuItem,
            this.电测深数据ToolStripMenuItem,
            this.高密度电阻率数据ToolStripMenuItem});
            this.数据DataToolStripMenuItem.Name = "数据DataToolStripMenuItem";
            this.数据DataToolStripMenuItem.Size = new System.Drawing.Size(115, 28);
            this.数据DataToolStripMenuItem.Text = "数据(Data)";
            this.数据DataToolStripMenuItem.Click += new System.EventHandler(this.数据DataToolStripMenuItem_Click);
            // 
            // mT正演数据ExcelToolStripMenuItem
            // 
            this.mT正演数据ExcelToolStripMenuItem.Name = "mT正演数据ExcelToolStripMenuItem";
            this.mT正演数据ExcelToolStripMenuItem.Size = new System.Drawing.Size(332, 34);
            this.mT正演数据ExcelToolStripMenuItem.Text = "MT正演数据(Excel)";
            this.mT正演数据ExcelToolStripMenuItem.Click += new System.EventHandler(this.mT正演数据ExcelToolStripMenuItem_Click);
            // 
            // 大地电磁数据EdiToolStripMenuItem
            // 
            this.大地电磁数据EdiToolStripMenuItem.Name = "大地电磁数据EdiToolStripMenuItem";
            this.大地电磁数据EdiToolStripMenuItem.Size = new System.Drawing.Size(332, 34);
            this.大地电磁数据EdiToolStripMenuItem.Text = "大地电磁数据(Edi)";
            this.大地电磁数据EdiToolStripMenuItem.Click += new System.EventHandler(this.大地电磁数据EdiToolStripMenuItem_Click);
            // 
            // 磁测ToolStripMenuItem
            // 
            this.磁测ToolStripMenuItem.Name = "磁测ToolStripMenuItem";
            this.磁测ToolStripMenuItem.Size = new System.Drawing.Size(332, 34);
            this.磁测ToolStripMenuItem.Text = "磁测数据(Megntic)";
            this.磁测ToolStripMenuItem.Click += new System.EventHandler(this.磁测ToolStripMenuItem_Click);
            // 
            // 电测深数据ToolStripMenuItem
            // 
            this.电测深数据ToolStripMenuItem.Name = "电测深数据ToolStripMenuItem";
            this.电测深数据ToolStripMenuItem.Size = new System.Drawing.Size(332, 34);
            this.电测深数据ToolStripMenuItem.Text = "电测深数据(Eletric)";
            this.电测深数据ToolStripMenuItem.Click += new System.EventHandler(this.电测深数据ToolStripMenuItem_Click);
            // 
            // 高密度电阻率数据ToolStripMenuItem
            // 
            this.高密度电阻率数据ToolStripMenuItem.Name = "高密度电阻率数据ToolStripMenuItem";
            this.高密度电阻率数据ToolStripMenuItem.Size = new System.Drawing.Size(332, 34);
            this.高密度电阻率数据ToolStripMenuItem.Text = "高密度电阻率数据(Res2lnv)";
            this.高密度电阻率数据ToolStripMenuItem.Click += new System.EventHandler(this.高密度电阻率数据ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤销UndoToolStripMenuItem,
            this.重做RedoToolStripMenuItem,
            this.清空ClearToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(108, 28);
            this.编辑ToolStripMenuItem.Text = "编辑(Edit)";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 撤销UndoToolStripMenuItem
            // 
            this.撤销UndoToolStripMenuItem.Name = "撤销UndoToolStripMenuItem";
            this.撤销UndoToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.撤销UndoToolStripMenuItem.Text = "撤销(Undo)";
            this.撤销UndoToolStripMenuItem.Click += new System.EventHandler(this.撤销UndoToolStripMenuItem_Click);
            // 
            // 重做RedoToolStripMenuItem
            // 
            this.重做RedoToolStripMenuItem.Name = "重做RedoToolStripMenuItem";
            this.重做RedoToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.重做RedoToolStripMenuItem.Text = "重做(Redo)";
            this.重做RedoToolStripMenuItem.Click += new System.EventHandler(this.重做RedoToolStripMenuItem_Click);
            // 
            // 清空ClearToolStripMenuItem
            // 
            this.清空ClearToolStripMenuItem.Name = "清空ClearToolStripMenuItem";
            this.清空ClearToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.清空ClearToolStripMenuItem.Text = "清空(Clear)";
            this.清空ClearToolStripMenuItem.Click += new System.EventHandler(this.清空ClearToolStripMenuItem_Click);
            // 
            // 查看ViewToolStripMenuItem
            // 
            this.查看ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放大ZoomInToolStripMenuItem,
            this.缩小ZoomOutToolStripMenuItem,
            this.平移PanToolStripMenuItem,
            this.还原ResetToolStripMenuItem,
            this.正常选中SelectedToolStripMenuItem});
            this.查看ViewToolStripMenuItem.Name = "查看ViewToolStripMenuItem";
            this.查看ViewToolStripMenuItem.Size = new System.Drawing.Size(115, 28);
            this.查看ViewToolStripMenuItem.Text = "查看(View)";
            this.查看ViewToolStripMenuItem.Click += new System.EventHandler(this.查看ViewToolStripMenuItem_Click);
            // 
            // 放大ZoomInToolStripMenuItem
            // 
            this.放大ZoomInToolStripMenuItem.Name = "放大ZoomInToolStripMenuItem";
            this.放大ZoomInToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.放大ZoomInToolStripMenuItem.Text = "放大(Zoom In)";
            this.放大ZoomInToolStripMenuItem.Click += new System.EventHandler(this.放大ZoomInToolStripMenuItem_Click);
            // 
            // 缩小ZoomOutToolStripMenuItem
            // 
            this.缩小ZoomOutToolStripMenuItem.Name = "缩小ZoomOutToolStripMenuItem";
            this.缩小ZoomOutToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.缩小ZoomOutToolStripMenuItem.Text = "缩小(Zoom Out)";
            this.缩小ZoomOutToolStripMenuItem.Click += new System.EventHandler(this.缩小ZoomOutToolStripMenuItem_Click);
            // 
            // 平移PanToolStripMenuItem
            // 
            this.平移PanToolStripMenuItem.Name = "平移PanToolStripMenuItem";
            this.平移PanToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.平移PanToolStripMenuItem.Text = "平移(Pan)";
            this.平移PanToolStripMenuItem.Click += new System.EventHandler(this.平移PanToolStripMenuItem_Click);
            // 
            // 还原ResetToolStripMenuItem
            // 
            this.还原ResetToolStripMenuItem.Name = "还原ResetToolStripMenuItem";
            this.还原ResetToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.还原ResetToolStripMenuItem.Text = "还原(Reset)";
            this.还原ResetToolStripMenuItem.Click += new System.EventHandler(this.还原ResetToolStripMenuItem_Click);
            // 
            // 正常选中SelectedToolStripMenuItem
            // 
            this.正常选中SelectedToolStripMenuItem.Name = "正常选中SelectedToolStripMenuItem";
            this.正常选中SelectedToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.正常选中SelectedToolStripMenuItem.Text = "正常选中(Select)";
            this.正常选中SelectedToolStripMenuItem.Click += new System.EventHandler(this.正常选中SelectedToolStripMenuItem_Click);
            // 
            // 帮助HelpToolStripMenuItem
            // 
            this.帮助HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于AboutToolStripMenuItem,
            this.教程TuToolStripMenuItem});
            this.帮助HelpToolStripMenuItem.Name = "帮助HelpToolStripMenuItem";
            this.帮助HelpToolStripMenuItem.Size = new System.Drawing.Size(115, 28);
            this.帮助HelpToolStripMenuItem.Text = "帮助(Help)";
            this.帮助HelpToolStripMenuItem.Click += new System.EventHandler(this.帮助HelpToolStripMenuItem_Click);
            // 
            // 关于AboutToolStripMenuItem
            // 
            this.关于AboutToolStripMenuItem.Name = "关于AboutToolStripMenuItem";
            this.关于AboutToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.关于AboutToolStripMenuItem.Text = "关于(About)";
            this.关于AboutToolStripMenuItem.Click += new System.EventHandler(this.关于AboutToolStripMenuItem_Click);
            // 
            // 教程TuToolStripMenuItem
            // 
            this.教程TuToolStripMenuItem.Name = "教程TuToolStripMenuItem";
            this.教程TuToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.教程TuToolStripMenuItem.Text = "教程(Tutorial)";
            this.教程TuToolStripMenuItem.Click += new System.EventHandler(this.教程TuToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer1.Location = new System.Drawing.Point(0, 56);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(252, 643);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(249, 320);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(258, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1014, 628);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(0, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(252, 305);
            this.propertyGrid1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 699);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "地球物理数据可视化(GeophysicalDataVisualization)";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 文件FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读入二进制数据LoadBinaryDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存为二进制数据SaveBinaryDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出数据ExportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据DataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mT正演数据ExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大地电磁数据EdiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 磁测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 电测深数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高密度电阻率数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重做RedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ZoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩小ZoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平移PanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还原ResetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 正常选中SelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 教程TuToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}

