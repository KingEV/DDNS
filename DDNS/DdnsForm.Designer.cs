namespace DDNS
{
    partial class DdnsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DdnsForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.recordIdTextBox = new System.Windows.Forms.TextBox();
            this.secretTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.keyIdTextBox = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.resolutionTextBox = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerWave = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.87905F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.12096F));
            this.tableLayoutPanel.Controls.Add(this.recordIdTextBox, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.secretTextBox, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.keyIdTextBox, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.startBtn, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.pictureBox1, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.resolutionTextBox, 1, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.74468F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.25532F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(557, 427);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // recordIdTextBox
            // 
            this.recordIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordIdTextBox.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recordIdTextBox.Location = new System.Drawing.Point(186, 106);
            this.recordIdTextBox.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.recordIdTextBox.Name = "recordIdTextBox";
            this.recordIdTextBox.Size = new System.Drawing.Size(368, 28);
            this.recordIdTextBox.TabIndex = 6;
            // 
            // secretTextBox
            // 
            this.secretTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secretTextBox.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secretTextBox.Location = new System.Drawing.Point(186, 53);
            this.secretTextBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.secretTextBox.Name = "secretTextBox";
            this.secretTextBox.Size = new System.Drawing.Size(368, 28);
            this.secretTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "AccessKeyID：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 57);
            this.label3.TabIndex = 2;
            this.label3.Text = "RecordID：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 51);
            this.label2.TabIndex = 1;
            this.label2.Text = "AccessKeySecret：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 56);
            this.label4.TabIndex = 3;
            this.label4.Text = "解析状态：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // keyIdTextBox
            // 
            this.keyIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyIdTextBox.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.keyIdTextBox.Location = new System.Drawing.Point(186, 8);
            this.keyIdTextBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.keyIdTextBox.Name = "keyIdTextBox";
            this.keyIdTextBox.Size = new System.Drawing.Size(368, 28);
            this.keyIdTextBox.TabIndex = 4;
            // 
            // startBtn
            // 
            this.tableLayoutPanel.SetColumnSpan(this.startBtn, 2);
            this.startBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startBtn.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startBtn.Location = new System.Drawing.Point(3, 210);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(551, 55);
            this.startBtn.TabIndex = 8;
            this.startBtn.Text = "开始解析";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // resolutionTextBox
            // 
            this.resolutionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resolutionTextBox.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resolutionTextBox.Location = new System.Drawing.Point(186, 165);
            this.resolutionTextBox.Margin = new System.Windows.Forms.Padding(3, 14, 3, 3);
            this.resolutionTextBox.Name = "resolutionTextBox";
            this.resolutionTextBox.Size = new System.Drawing.Size(368, 28);
            this.resolutionTextBox.TabIndex = 7;
            // 
            // timer
            // 
            this.timer.Interval = 60000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerWave
            // 
            this.timerWave.Interval = 2500;
            this.timerWave.Tick += new System.EventHandler(this.timerWave_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DDNS.Properties.Resources.cat1;
            this.tableLayoutPanel.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::DDNS.Properties.Resources.cat1;
            this.pictureBox1.InitialImage = global::DDNS.Properties.Resources.cat1;
            this.pictureBox1.Location = new System.Drawing.Point(3, 271);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(551, 153);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // DdnsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 427);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(575, 474);
            this.MinimumSize = new System.Drawing.Size(575, 474);
            this.Name = "DdnsForm";
            this.Text = "DDNS";
            this.Load += new System.EventHandler(this.DdnsForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox recordIdTextBox;
        private System.Windows.Forms.TextBox secretTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox keyIdTextBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox resolutionTextBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerWave;
    }
}

