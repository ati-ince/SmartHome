namespace modbus
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFunctionType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numDevAddr = new System.Windows.Forms.NumericUpDown();
            this.numRegAddress = new System.Windows.Forms.NumericUpDown();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.lbValue = new System.Windows.Forms.Label();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDevAddr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRegAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived_1);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(195, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(158, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 14;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 21);
            this.comboBox1.TabIndex = 15;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(195, 202);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 72);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "Device Addresses:\r\n4Noks Devs   : 16 to 150\r\nSkyStream     : 200\r\nAmmonit       :" +
                " 201\r\nXtender          : 202";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Device Address:";
            // 
            // cbFunctionType
            // 
            this.cbFunctionType.FormattingEnabled = true;
            this.cbFunctionType.Items.AddRange(new object[] {
            "Read",
            "Write"});
            this.cbFunctionType.Location = new System.Drawing.Point(289, 75);
            this.cbFunctionType.Name = "cbFunctionType";
            this.cbFunctionType.Size = new System.Drawing.Size(75, 21);
            this.cbFunctionType.TabIndex = 18;
            this.cbFunctionType.SelectedIndexChanged += new System.EventHandler(this.cbFunctionType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Function Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Register Address:";
            // 
            // numDevAddr
            // 
            this.numDevAddr.Location = new System.Drawing.Point(289, 49);
            this.numDevAddr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numDevAddr.Name = "numDevAddr";
            this.numDevAddr.Size = new System.Drawing.Size(75, 20);
            this.numDevAddr.TabIndex = 21;
            this.numDevAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDevAddr.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // numRegAddress
            // 
            this.numRegAddress.Location = new System.Drawing.Point(289, 103);
            this.numRegAddress.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numRegAddress.Name = "numRegAddress";
            this.numRegAddress.Size = new System.Drawing.Size(75, 20);
            this.numRegAddress.TabIndex = 22;
            this.numRegAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(12, 51);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(174, 225);
            this.listBoxResults.TabIndex = 23;
            // 
            // lbValue
            // 
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(382, 78);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(37, 13);
            this.lbValue.TabIndex = 24;
            this.lbValue.Text = "Value:";
            this.lbValue.Visible = false;
            // 
            // numValue
            // 
            this.numValue.Location = new System.Drawing.Point(423, 76);
            this.numValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(55, 20);
            this.numValue.TabIndex = 25;
            this.numValue.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(239, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 286);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numValue);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.numRegAddress);
            this.Controls.Add(this.numDevAddr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFunctionType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Smart Home Serial Communication Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDevAddr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRegAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFunctionType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numDevAddr;
        private System.Windows.Forms.NumericUpDown numRegAddress;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.NumericUpDown numValue;
        private System.Windows.Forms.Button button2;
    }
}

