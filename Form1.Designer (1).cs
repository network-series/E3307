namespace WindowsFormsApp3
{
    partial class serialPort
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbStop = new System.Windows.Forms.ComboBox();
            this.cmbBaud = new System.Windows.Forms.ComboBox();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.group3 = new System.Windows.Forms.GroupBox();
            this.SendTbox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ReceiveTbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            this.group3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.post_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbData);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.cmbStop);
            this.groupBox1.Controls.Add(this.cmbBaud);
            this.groupBox1.Controls.Add(this.cmbPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmbData
            // 
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.cmbData.Location = new System.Drawing.Point(503, 82);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(121, 26);
            this.cmbData.TabIndex = 10;
            // 
            // cmbParity
            // 
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cmbParity.Location = new System.Drawing.Point(503, 39);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(121, 26);
            this.cmbParity.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(377, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "数据位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "校验位";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(646, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "打开串口";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbStop
            // 
            this.cmbStop.FormattingEnabled = true;
            this.cmbStop.Items.AddRange(new object[] {
            "One",
            "Two"});
            this.cmbStop.Location = new System.Drawing.Point(168, 127);
            this.cmbStop.Name = "cmbStop";
            this.cmbStop.Size = new System.Drawing.Size(121, 26);
            this.cmbStop.TabIndex = 5;
            // 
            // cmbBaud
            // 
            this.cmbBaud.FormattingEnabled = true;
            this.cmbBaud.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cmbBaud.Location = new System.Drawing.Point(168, 82);
            this.cmbBaud.Name = "cmbBaud";
            this.cmbBaud.Size = new System.Drawing.Size(121, 26);
            this.cmbBaud.TabIndex = 4;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(168, 31);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(121, 26);
            this.cmbPort.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "停止位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "通讯口";
            // 
            // group3
            // 
            this.group3.Controls.Add(this.SendTbox);
            this.group3.Location = new System.Drawing.Point(12, 302);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(579, 148);
            this.group3.TabIndex = 1;
            this.group3.TabStop = false;
            this.group3.Text = "发送数据";
            // 
            // SendTbox
            // 
            this.SendTbox.Location = new System.Drawing.Point(0, 27);
            this.SendTbox.Multiline = true;
            this.SendTbox.Name = "SendTbox";
            this.SendTbox.Size = new System.Drawing.Size(573, 115);
            this.SendTbox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ReceiveTbox);
            this.groupBox2.Location = new System.Drawing.Point(12, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 113);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收数据";
            // 
            // ReceiveTbox
            // 
            this.ReceiveTbox.Location = new System.Drawing.Point(3, 27);
            this.ReceiveTbox.Multiline = true;
            this.ReceiveTbox.Name = "ReceiveTbox";
            this.ReceiveTbox.Size = new System.Drawing.Size(767, 80);
            this.ReceiveTbox.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(693, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // serialPort
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.groupBox1);
            this.Name = "serialPort";
            this.Text = "serialPort";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox group3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbStop;
        private System.Windows.Forms.ComboBox cmbBaud;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SendTbox;
        private System.Windows.Forms.TextBox ReceiveTbox;
        private System.Windows.Forms.Button button1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}

