namespace SmartHomeFrameworkV2._1
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
            this.Connect_RemoteCOMM = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Disconnect_RemoteCOMM = new System.Windows.Forms.Button();
            this.ComboBox__RemoteCOMM = new System.Windows.Forms.ComboBox();
            this.Connect_ModBus4Noks = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Disconnect_ModBus4Noks = new System.Windows.Forms.Button();
            this.ComboBox_Modbus4Noks = new System.Windows.Forms.ComboBox();
            this.Connect_Xtender = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Disconnect_Xtender = new System.Windows.Forms.Button();
            this.ComboBox_Xtender = new System.Windows.Forms.ComboBox();
            this.Connect_Ammonit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Disconnect_Ammonit = new System.Windows.Forms.Button();
            this.Start_Algorithm = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Stop_Algorithm = new System.Windows.Forms.Button();
            this.TextBoxTest = new System.Windows.Forms.TextBox();
            this.TimerAlgorithm = new System.Windows.Forms.Timer(this.components);
            this.DataGridViewXtenderExcel = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewXtenderExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // Connect_RemoteCOMM
            // 
            this.Connect_RemoteCOMM.BackColor = System.Drawing.Color.SeaGreen;
            this.Connect_RemoteCOMM.Location = new System.Drawing.Point(14, 228);
            this.Connect_RemoteCOMM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Connect_RemoteCOMM.Name = "Connect_RemoteCOMM";
            this.Connect_RemoteCOMM.Size = new System.Drawing.Size(112, 35);
            this.Connect_RemoteCOMM.TabIndex = 45;
            this.Connect_RemoteCOMM.Text = "Connect";
            this.Connect_RemoteCOMM.UseVisualStyleBackColor = false;
            this.Connect_RemoteCOMM.Click += new System.EventHandler(this.Connect_RemoteCOMM_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 47;
            this.label3.Text = "RemoteCOMM";
            // 
            // Disconnect_RemoteCOMM
            // 
            this.Disconnect_RemoteCOMM.BackColor = System.Drawing.Color.Red;
            this.Disconnect_RemoteCOMM.Location = new System.Drawing.Point(153, 228);
            this.Disconnect_RemoteCOMM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Disconnect_RemoteCOMM.Name = "Disconnect_RemoteCOMM";
            this.Disconnect_RemoteCOMM.Size = new System.Drawing.Size(112, 35);
            this.Disconnect_RemoteCOMM.TabIndex = 46;
            this.Disconnect_RemoteCOMM.Text = "Disconnect";
            this.Disconnect_RemoteCOMM.UseVisualStyleBackColor = false;
            this.Disconnect_RemoteCOMM.Click += new System.EventHandler(this.Disconnect_RemoteCOMM_Click);
            // 
            // ComboBox__RemoteCOMM
            // 
            this.ComboBox__RemoteCOMM.BackColor = System.Drawing.Color.Ivory;
            this.ComboBox__RemoteCOMM.FormattingEnabled = true;
            this.ComboBox__RemoteCOMM.Location = new System.Drawing.Point(14, 188);
            this.ComboBox__RemoteCOMM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboBox__RemoteCOMM.Name = "ComboBox__RemoteCOMM";
            this.ComboBox__RemoteCOMM.Size = new System.Drawing.Size(124, 28);
            this.ComboBox__RemoteCOMM.TabIndex = 44;
            // 
            // Connect_ModBus4Noks
            // 
            this.Connect_ModBus4Noks.BackColor = System.Drawing.Color.SeaGreen;
            this.Connect_ModBus4Noks.Location = new System.Drawing.Point(14, 142);
            this.Connect_ModBus4Noks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Connect_ModBus4Noks.Name = "Connect_ModBus4Noks";
            this.Connect_ModBus4Noks.Size = new System.Drawing.Size(112, 35);
            this.Connect_ModBus4Noks.TabIndex = 41;
            this.Connect_ModBus4Noks.Text = "Connect";
            this.Connect_ModBus4Noks.UseVisualStyleBackColor = false;
            this.Connect_ModBus4Noks.Click += new System.EventHandler(this.Connect_ModBus4Noks_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "4Noks";
            // 
            // Disconnect_ModBus4Noks
            // 
            this.Disconnect_ModBus4Noks.BackColor = System.Drawing.Color.Red;
            this.Disconnect_ModBus4Noks.Location = new System.Drawing.Point(153, 142);
            this.Disconnect_ModBus4Noks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Disconnect_ModBus4Noks.Name = "Disconnect_ModBus4Noks";
            this.Disconnect_ModBus4Noks.Size = new System.Drawing.Size(112, 35);
            this.Disconnect_ModBus4Noks.TabIndex = 42;
            this.Disconnect_ModBus4Noks.Text = "Disconnect";
            this.Disconnect_ModBus4Noks.UseVisualStyleBackColor = false;
            this.Disconnect_ModBus4Noks.Click += new System.EventHandler(this.Disconnect_ModBus4Noks_Click);
            // 
            // ComboBox_Modbus4Noks
            // 
            this.ComboBox_Modbus4Noks.BackColor = System.Drawing.Color.Ivory;
            this.ComboBox_Modbus4Noks.FormattingEnabled = true;
            this.ComboBox_Modbus4Noks.Location = new System.Drawing.Point(14, 102);
            this.ComboBox_Modbus4Noks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboBox_Modbus4Noks.Name = "ComboBox_Modbus4Noks";
            this.ComboBox_Modbus4Noks.Size = new System.Drawing.Size(124, 28);
            this.ComboBox_Modbus4Noks.TabIndex = 40;
            // 
            // Connect_Xtender
            // 
            this.Connect_Xtender.BackColor = System.Drawing.Color.SeaGreen;
            this.Connect_Xtender.Location = new System.Drawing.Point(14, 55);
            this.Connect_Xtender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Connect_Xtender.Name = "Connect_Xtender";
            this.Connect_Xtender.Size = new System.Drawing.Size(112, 35);
            this.Connect_Xtender.TabIndex = 37;
            this.Connect_Xtender.Text = "Connect";
            this.Connect_Xtender.UseVisualStyleBackColor = false;
            this.Connect_Xtender.Click += new System.EventHandler(this.Connect_Xtender_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Xtender";
            // 
            // Disconnect_Xtender
            // 
            this.Disconnect_Xtender.BackColor = System.Drawing.Color.Red;
            this.Disconnect_Xtender.Location = new System.Drawing.Point(153, 55);
            this.Disconnect_Xtender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Disconnect_Xtender.Name = "Disconnect_Xtender";
            this.Disconnect_Xtender.Size = new System.Drawing.Size(112, 35);
            this.Disconnect_Xtender.TabIndex = 38;
            this.Disconnect_Xtender.Text = "Disconnect";
            this.Disconnect_Xtender.UseVisualStyleBackColor = false;
            this.Disconnect_Xtender.Click += new System.EventHandler(this.Disconnect_Xtender_Click);
            // 
            // ComboBox_Xtender
            // 
            this.ComboBox_Xtender.BackColor = System.Drawing.Color.Ivory;
            this.ComboBox_Xtender.FormattingEnabled = true;
            this.ComboBox_Xtender.Location = new System.Drawing.Point(14, 14);
            this.ComboBox_Xtender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboBox_Xtender.Name = "ComboBox_Xtender";
            this.ComboBox_Xtender.Size = new System.Drawing.Size(124, 28);
            this.ComboBox_Xtender.TabIndex = 36;
            // 
            // Connect_Ammonit
            // 
            this.Connect_Ammonit.BackColor = System.Drawing.Color.SeaGreen;
            this.Connect_Ammonit.Location = new System.Drawing.Point(14, 311);
            this.Connect_Ammonit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Connect_Ammonit.Name = "Connect_Ammonit";
            this.Connect_Ammonit.Size = new System.Drawing.Size(112, 35);
            this.Connect_Ammonit.TabIndex = 48;
            this.Connect_Ammonit.Text = "Connect";
            this.Connect_Ammonit.UseVisualStyleBackColor = false;
            this.Connect_Ammonit.Click += new System.EventHandler(this.Connect_Ammonit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 277);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 50;
            this.label4.Text = "Ammonit";
            // 
            // Disconnect_Ammonit
            // 
            this.Disconnect_Ammonit.BackColor = System.Drawing.Color.Red;
            this.Disconnect_Ammonit.Location = new System.Drawing.Point(153, 311);
            this.Disconnect_Ammonit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Disconnect_Ammonit.Name = "Disconnect_Ammonit";
            this.Disconnect_Ammonit.Size = new System.Drawing.Size(112, 35);
            this.Disconnect_Ammonit.TabIndex = 49;
            this.Disconnect_Ammonit.Text = "Disconnect";
            this.Disconnect_Ammonit.UseVisualStyleBackColor = false;
            this.Disconnect_Ammonit.Click += new System.EventHandler(this.Disconnect_Ammonit_Click);
            // 
            // Start_Algorithm
            // 
            this.Start_Algorithm.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Start_Algorithm.Location = new System.Drawing.Point(14, 392);
            this.Start_Algorithm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Start_Algorithm.Name = "Start_Algorithm";
            this.Start_Algorithm.Size = new System.Drawing.Size(112, 35);
            this.Start_Algorithm.TabIndex = 51;
            this.Start_Algorithm.Text = "Start";
            this.Start_Algorithm.UseVisualStyleBackColor = false;
            this.Start_Algorithm.Click += new System.EventHandler(this.Start_Algorithm_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 358);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 53;
            this.label5.Text = "Algorithm";
            // 
            // Stop_Algorithm
            // 
            this.Stop_Algorithm.BackColor = System.Drawing.Color.LightPink;
            this.Stop_Algorithm.Location = new System.Drawing.Point(153, 392);
            this.Stop_Algorithm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Stop_Algorithm.Name = "Stop_Algorithm";
            this.Stop_Algorithm.Size = new System.Drawing.Size(112, 35);
            this.Stop_Algorithm.TabIndex = 52;
            this.Stop_Algorithm.Text = "Stop";
            this.Stop_Algorithm.UseVisualStyleBackColor = false;
            this.Stop_Algorithm.Click += new System.EventHandler(this.Stop_Algorithm_Click);
            // 
            // TextBoxTest
            // 
            this.TextBoxTest.Location = new System.Drawing.Point(305, 59);
            this.TextBoxTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextBoxTest.Name = "TextBoxTest";
            this.TextBoxTest.Size = new System.Drawing.Size(148, 26);
            this.TextBoxTest.TabIndex = 54;
            // 
            // TimerAlgorithm
            // 
            this.TimerAlgorithm.Interval = 60000;
            this.TimerAlgorithm.Tick += new System.EventHandler(this.TimerAlgorithm_Tick);
            // 
            // DataGridViewXtenderExcel
            // 
            this.DataGridViewXtenderExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewXtenderExcel.Location = new System.Drawing.Point(14, 457);
            this.DataGridViewXtenderExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DataGridViewXtenderExcel.Name = "DataGridViewXtenderExcel";
            this.DataGridViewXtenderExcel.Size = new System.Drawing.Size(1238, 642);
            this.DataGridViewXtenderExcel.TabIndex = 55;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 447);
            this.Controls.Add(this.DataGridViewXtenderExcel);
            this.Controls.Add(this.TextBoxTest);
            this.Controls.Add(this.Start_Algorithm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Stop_Algorithm);
            this.Controls.Add(this.Connect_Ammonit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Disconnect_Ammonit);
            this.Controls.Add(this.Connect_RemoteCOMM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Disconnect_RemoteCOMM);
            this.Controls.Add(this.ComboBox__RemoteCOMM);
            this.Controls.Add(this.Connect_ModBus4Noks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Disconnect_ModBus4Noks);
            this.Controls.Add(this.ComboBox_Modbus4Noks);
            this.Controls.Add(this.Connect_Xtender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Disconnect_Xtender);
            this.Controls.Add(this.ComboBox_Xtender);
            this.Name = "Form1";
            this.Text = "ShFramework";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewXtenderExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect_RemoteCOMM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Disconnect_RemoteCOMM;
        private System.Windows.Forms.ComboBox ComboBox__RemoteCOMM;
        private System.Windows.Forms.Button Connect_ModBus4Noks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Disconnect_ModBus4Noks;
        private System.Windows.Forms.ComboBox ComboBox_Modbus4Noks;
        private System.Windows.Forms.Button Connect_Xtender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Disconnect_Xtender;
        private System.Windows.Forms.ComboBox ComboBox_Xtender;
        private System.Windows.Forms.Button Connect_Ammonit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Disconnect_Ammonit;
        private System.Windows.Forms.Button Start_Algorithm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Stop_Algorithm;
        private System.Windows.Forms.TextBox TextBoxTest;
        private System.Windows.Forms.Timer TimerAlgorithm;
        private System.Windows.Forms.DataGridView DataGridViewXtenderExcel;
    }
}

