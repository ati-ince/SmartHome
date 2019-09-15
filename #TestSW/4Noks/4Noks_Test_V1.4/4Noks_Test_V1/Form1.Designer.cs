namespace _4Noks_Test_V1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ComboBox_Modbus4Noks = new System.Windows.Forms.ComboBox();
            this.Disconnect_ModBus4Noks = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Connect_ModBus4Noks = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstDeviceList = new System.Windows.Forms.ListBox();
            this.CheckButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PoolTimer = new System.Windows.Forms.Timer(this.components);
            this.UartTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBox_Modbus4Noks
            // 
            this.ComboBox_Modbus4Noks.BackColor = System.Drawing.Color.Ivory;
            this.ComboBox_Modbus4Noks.FormattingEnabled = true;
            this.ComboBox_Modbus4Noks.Location = new System.Drawing.Point(12, 12);
            this.ComboBox_Modbus4Noks.Name = "ComboBox_Modbus4Noks";
            this.ComboBox_Modbus4Noks.Size = new System.Drawing.Size(84, 21);
            this.ComboBox_Modbus4Noks.TabIndex = 45;
            this.ComboBox_Modbus4Noks.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Modbus4Noks_SelectedIndexChanged);
            // 
            // Disconnect_ModBus4Noks
            // 
            this.Disconnect_ModBus4Noks.BackColor = System.Drawing.Color.Red;
            this.Disconnect_ModBus4Noks.Location = new System.Drawing.Point(105, 38);
            this.Disconnect_ModBus4Noks.Name = "Disconnect_ModBus4Noks";
            this.Disconnect_ModBus4Noks.Size = new System.Drawing.Size(75, 23);
            this.Disconnect_ModBus4Noks.TabIndex = 47;
            this.Disconnect_ModBus4Noks.Text = "Disconnect";
            this.Disconnect_ModBus4Noks.UseVisualStyleBackColor = false;
            this.Disconnect_ModBus4Noks.Click += new System.EventHandler(this.Disconnect_ModBus4Noks_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "4Noks";
            // 
            // Connect_ModBus4Noks
            // 
            this.Connect_ModBus4Noks.BackColor = System.Drawing.Color.SeaGreen;
            this.Connect_ModBus4Noks.Location = new System.Drawing.Point(12, 38);
            this.Connect_ModBus4Noks.Name = "Connect_ModBus4Noks";
            this.Connect_ModBus4Noks.Size = new System.Drawing.Size(75, 23);
            this.Connect_ModBus4Noks.TabIndex = 46;
            this.Connect_ModBus4Noks.Text = "Connect";
            this.Connect_ModBus4Noks.UseVisualStyleBackColor = false;
            this.Connect_ModBus4Noks.Click += new System.EventHandler(this.Connect_ModBus4Noks_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstDeviceList);
            this.groupBox1.Location = new System.Drawing.Point(12, 75);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(190, 93);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cihaz Adres Listesi";
            // 
            // lstDeviceList
            // 
            this.lstDeviceList.FormattingEnabled = true;
            this.lstDeviceList.Location = new System.Drawing.Point(8, 20);
            this.lstDeviceList.Margin = new System.Windows.Forms.Padding(2);
            this.lstDeviceList.Name = "lstDeviceList";
            this.lstDeviceList.Size = new System.Drawing.Size(179, 69);
            this.lstDeviceList.TabIndex = 0;
            this.lstDeviceList.SelectedIndexChanged += new System.EventHandler(this.lstDeviceList_SelectedIndexChanged);
            // 
            // CheckButton
            // 
            this.CheckButton.BackColor = System.Drawing.Color.SeaGreen;
            this.CheckButton.Location = new System.Drawing.Point(124, 183);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(75, 23);
            this.CheckButton.TabIndex = 51;
            this.CheckButton.Text = "Check";
            this.CheckButton.UseVisualStyleBackColor = false;
            this.CheckButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 52;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PoolTimer
            // 
            this.PoolTimer.Interval = 250;
            this.PoolTimer.Tick += new System.EventHandler(this.PoolTimer_Tick);
            // 
            // UartTimer
            // 
            this.UartTimer.Interval = 150;
            this.UartTimer.Tick += new System.EventHandler(this.UartTimer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(208, 75);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(69, 93);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plugs";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(8, 20);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(58, 69);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox2);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.No;
            this.groupBox3.Location = new System.Drawing.Point(281, 75);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(69, 93);
            this.groupBox3.TabIndex = 54;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Power(W)";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(8, 20);
            this.listBox2.Margin = new System.Windows.Forms.Padding(2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(58, 69);
            this.listBox2.TabIndex = 0;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox3);
            this.groupBox4.Cursor = System.Windows.Forms.Cursors.No;
            this.groupBox4.Location = new System.Drawing.Point(354, 75);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(69, 93);
            this.groupBox4.TabIndex = 55;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Checsum";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(8, 20);
            this.listBox3.Margin = new System.Windows.Forms.Padding(2);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(58, 69);
            this.listBox3.TabIndex = 0;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 262);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Connect_ModBus4Noks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Disconnect_ModBus4Noks);
            this.Controls.Add(this.ComboBox_Modbus4Noks);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_Modbus4Noks;
        private System.Windows.Forms.Button Disconnect_ModBus4Noks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Connect_ModBus4Noks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstDeviceList;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer PoolTimer;
        private System.Windows.Forms.Timer UartTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listBox3;
    }
}

