namespace WindowsFormsApplication1
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
            this.comboBox_ComPorts = new System.Windows.Forms.ComboBox();
            this.XtenderDisconnectButtom = new System.Windows.Forms.Button();
            this.XtenderConnectButtom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.XtenderDataWriteButton = new System.Windows.Forms.Button();
            this.XtenderDataReadButton = new System.Windows.Forms.Button();
            this.comboBox_XtenderRead = new System.Windows.Forms.ComboBox();
            this.comboBox_XtenderWrite = new System.Windows.Forms.ComboBox();
            this.textBox__XtenderRead = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ZigBee4NoksConnectButtom = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ZigBee4NoksDisconnectButtom = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.RemoteControlConnectButtom = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RemoteControlDisconnectButtom = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SoftStartButtom = new System.Windows.Forms.Button();
            this.SoftStopButtom = new System.Windows.Forms.Button();
            this.XtenderPanel = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Z4NoksPanel = new System.Windows.Forms.Panel();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4NoksAddress = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox51 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.XtenderPanel.SuspendLayout();
            this.Z4NoksPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_ComPorts
            // 
            this.comboBox_ComPorts.BackColor = System.Drawing.Color.Ivory;
            this.comboBox_ComPorts.FormattingEnabled = true;
            this.comboBox_ComPorts.Location = new System.Drawing.Point(24, 12);
            this.comboBox_ComPorts.Name = "comboBox_ComPorts";
            this.comboBox_ComPorts.Size = new System.Drawing.Size(84, 21);
            this.comboBox_ComPorts.TabIndex = 1;
            this.comboBox_ComPorts.SelectedIndexChanged += new System.EventHandler(this.comboBox_ComPorts_SelectedIndexChanged);
            // 
            // XtenderDisconnectButtom
            // 
            this.XtenderDisconnectButtom.BackColor = System.Drawing.Color.Red;
            this.XtenderDisconnectButtom.Location = new System.Drawing.Point(117, 39);
            this.XtenderDisconnectButtom.Name = "XtenderDisconnectButtom";
            this.XtenderDisconnectButtom.Size = new System.Drawing.Size(75, 23);
            this.XtenderDisconnectButtom.TabIndex = 20;
            this.XtenderDisconnectButtom.Text = "Disconnect";
            this.XtenderDisconnectButtom.UseVisualStyleBackColor = false;
            this.XtenderDisconnectButtom.Click += new System.EventHandler(this.button2_Click);
            // 
            // XtenderConnectButtom
            // 
            this.XtenderConnectButtom.BackColor = System.Drawing.Color.SeaGreen;
            this.XtenderConnectButtom.Location = new System.Drawing.Point(24, 39);
            this.XtenderConnectButtom.Name = "XtenderConnectButtom";
            this.XtenderConnectButtom.Size = new System.Drawing.Size(75, 23);
            this.XtenderConnectButtom.TabIndex = 19;
            this.XtenderConnectButtom.Text = "Connect";
            this.XtenderConnectButtom.UseVisualStyleBackColor = false;
            this.XtenderConnectButtom.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "XtenderComPort";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // XtenderDataWriteButton
            // 
            this.XtenderDataWriteButton.BackColor = System.Drawing.Color.Orchid;
            this.XtenderDataWriteButton.Location = new System.Drawing.Point(56, 137);
            this.XtenderDataWriteButton.Name = "XtenderDataWriteButton";
            this.XtenderDataWriteButton.Size = new System.Drawing.Size(75, 23);
            this.XtenderDataWriteButton.TabIndex = 23;
            this.XtenderDataWriteButton.Text = "DataWrite";
            this.XtenderDataWriteButton.UseVisualStyleBackColor = false;
            this.XtenderDataWriteButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // XtenderDataReadButton
            // 
            this.XtenderDataReadButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.XtenderDataReadButton.Location = new System.Drawing.Point(96, 23);
            this.XtenderDataReadButton.Name = "XtenderDataReadButton";
            this.XtenderDataReadButton.Size = new System.Drawing.Size(81, 23);
            this.XtenderDataReadButton.TabIndex = 22;
            this.XtenderDataReadButton.Text = "DataRead";
            this.XtenderDataReadButton.UseVisualStyleBackColor = false;
            this.XtenderDataReadButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox_XtenderRead
            // 
            this.comboBox_XtenderRead.BackColor = System.Drawing.Color.Ivory;
            this.comboBox_XtenderRead.FormattingEnabled = true;
            this.comboBox_XtenderRead.Location = new System.Drawing.Point(3, 25);
            this.comboBox_XtenderRead.Name = "comboBox_XtenderRead";
            this.comboBox_XtenderRead.Size = new System.Drawing.Size(84, 21);
            this.comboBox_XtenderRead.TabIndex = 24;
            this.comboBox_XtenderRead.SelectedIndexChanged += new System.EventHandler(this.comboBox_XtenderRead_SelectedIndexChanged);
            // 
            // comboBox_XtenderWrite
            // 
            this.comboBox_XtenderWrite.BackColor = System.Drawing.Color.Ivory;
            this.comboBox_XtenderWrite.FormattingEnabled = true;
            this.comboBox_XtenderWrite.Location = new System.Drawing.Point(3, 84);
            this.comboBox_XtenderWrite.Name = "comboBox_XtenderWrite";
            this.comboBox_XtenderWrite.Size = new System.Drawing.Size(84, 21);
            this.comboBox_XtenderWrite.TabIndex = 25;
            // 
            // textBox__XtenderRead
            // 
            this.textBox__XtenderRead.Location = new System.Drawing.Point(3, 55);
            this.textBox__XtenderRead.Name = "textBox__XtenderRead";
            this.textBox__XtenderRead.Size = new System.Drawing.Size(174, 20);
            this.textBox__XtenderRead.TabIndex = 26;
            this.textBox__XtenderRead.TextChanged += new System.EventHandler(this.textBox__XtenderRead_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 111);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(174, 20);
            this.textBox2.TabIndex = 27;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ZigBee4NoksConnectButtom
            // 
            this.ZigBee4NoksConnectButtom.BackColor = System.Drawing.Color.SeaGreen;
            this.ZigBee4NoksConnectButtom.Location = new System.Drawing.Point(24, 95);
            this.ZigBee4NoksConnectButtom.Name = "ZigBee4NoksConnectButtom";
            this.ZigBee4NoksConnectButtom.Size = new System.Drawing.Size(75, 23);
            this.ZigBee4NoksConnectButtom.TabIndex = 29;
            this.ZigBee4NoksConnectButtom.Text = "Connect";
            this.ZigBee4NoksConnectButtom.UseVisualStyleBackColor = false;
            this.ZigBee4NoksConnectButtom.Click += new System.EventHandler(this.ZigBee4NoksConnectButtom_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "4NoksComPort";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ZigBee4NoksDisconnectButtom
            // 
            this.ZigBee4NoksDisconnectButtom.BackColor = System.Drawing.Color.Red;
            this.ZigBee4NoksDisconnectButtom.Location = new System.Drawing.Point(117, 95);
            this.ZigBee4NoksDisconnectButtom.Name = "ZigBee4NoksDisconnectButtom";
            this.ZigBee4NoksDisconnectButtom.Size = new System.Drawing.Size(75, 23);
            this.ZigBee4NoksDisconnectButtom.TabIndex = 30;
            this.ZigBee4NoksDisconnectButtom.Text = "Disconnect";
            this.ZigBee4NoksDisconnectButtom.UseVisualStyleBackColor = false;
            this.ZigBee4NoksDisconnectButtom.Click += new System.EventHandler(this.ZigBee4NoksDisconnectButtom_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.Ivory;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(24, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(84, 21);
            this.comboBox1.TabIndex = 28;
            // 
            // RemoteControlConnectButtom
            // 
            this.RemoteControlConnectButtom.BackColor = System.Drawing.Color.SeaGreen;
            this.RemoteControlConnectButtom.Location = new System.Drawing.Point(24, 151);
            this.RemoteControlConnectButtom.Name = "RemoteControlConnectButtom";
            this.RemoteControlConnectButtom.Size = new System.Drawing.Size(75, 23);
            this.RemoteControlConnectButtom.TabIndex = 33;
            this.RemoteControlConnectButtom.Text = "Connect";
            this.RemoteControlConnectButtom.UseVisualStyleBackColor = false;
            this.RemoteControlConnectButtom.Click += new System.EventHandler(this.RemoteControlConnectButtom_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "RemoteCPort";
            // 
            // RemoteControlDisconnectButtom
            // 
            this.RemoteControlDisconnectButtom.BackColor = System.Drawing.Color.Red;
            this.RemoteControlDisconnectButtom.Location = new System.Drawing.Point(117, 151);
            this.RemoteControlDisconnectButtom.Name = "RemoteControlDisconnectButtom";
            this.RemoteControlDisconnectButtom.Size = new System.Drawing.Size(75, 23);
            this.RemoteControlDisconnectButtom.TabIndex = 34;
            this.RemoteControlDisconnectButtom.Text = "Disconnect";
            this.RemoteControlDisconnectButtom.UseVisualStyleBackColor = false;
            this.RemoteControlDisconnectButtom.Click += new System.EventHandler(this.RemoteControlDisconnectButtom_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.Color.Ivory;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(24, 124);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(84, 21);
            this.comboBox2.TabIndex = 32;
            // 
            // SoftStartButtom
            // 
            this.SoftStartButtom.BackColor = System.Drawing.Color.SkyBlue;
            this.SoftStartButtom.Location = new System.Drawing.Point(73, 186);
            this.SoftStartButtom.Name = "SoftStartButtom";
            this.SoftStartButtom.Size = new System.Drawing.Size(75, 23);
            this.SoftStartButtom.TabIndex = 36;
            this.SoftStartButtom.Text = "SoftStart";
            this.SoftStartButtom.UseVisualStyleBackColor = false;
            this.SoftStartButtom.Click += new System.EventHandler(this.button9_Click);
            // 
            // SoftStopButtom
            // 
            this.SoftStopButtom.BackColor = System.Drawing.Color.Salmon;
            this.SoftStopButtom.Location = new System.Drawing.Point(73, 215);
            this.SoftStopButtom.Name = "SoftStopButtom";
            this.SoftStopButtom.Size = new System.Drawing.Size(75, 23);
            this.SoftStopButtom.TabIndex = 37;
            this.SoftStopButtom.Text = "SoftStop";
            this.SoftStopButtom.UseVisualStyleBackColor = false;
            this.SoftStopButtom.Click += new System.EventHandler(this.SoftStopButtom_Click);
            // 
            // XtenderPanel
            // 
            this.XtenderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.XtenderPanel.Controls.Add(this.textBox4);
            this.XtenderPanel.Controls.Add(this.label4);
            this.XtenderPanel.Controls.Add(this.comboBox_XtenderRead);
            this.XtenderPanel.Controls.Add(this.XtenderDataReadButton);
            this.XtenderPanel.Controls.Add(this.XtenderDataWriteButton);
            this.XtenderPanel.Controls.Add(this.comboBox_XtenderWrite);
            this.XtenderPanel.Controls.Add(this.textBox__XtenderRead);
            this.XtenderPanel.Controls.Add(this.textBox2);
            this.XtenderPanel.Location = new System.Drawing.Point(220, 15);
            this.XtenderPanel.Name = "XtenderPanel";
            this.XtenderPanel.Size = new System.Drawing.Size(183, 165);
            this.XtenderPanel.TabIndex = 38;
            this.XtenderPanel.TabStop = true;
            this.XtenderPanel.Tag = "";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(93, 84);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(84, 20);
            this.textBox4.TabIndex = 76;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Xtender Test";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Z4NoksPanel
            // 
            this.Z4NoksPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Z4NoksPanel.Controls.Add(this.comboBox5);
            this.Z4NoksPanel.Controls.Add(this.comboBox3);
            this.Z4NoksPanel.Controls.Add(this.comboBox4NoksAddress);
            this.Z4NoksPanel.Controls.Add(this.label5);
            this.Z4NoksPanel.Controls.Add(this.comboBox4);
            this.Z4NoksPanel.Controls.Add(this.textBox1);
            this.Z4NoksPanel.Controls.Add(this.textBox3);
            this.Z4NoksPanel.Location = new System.Drawing.Point(220, 186);
            this.Z4NoksPanel.Name = "Z4NoksPanel";
            this.Z4NoksPanel.Size = new System.Drawing.Size(183, 186);
            this.Z4NoksPanel.TabIndex = 39;
            // 
            // comboBox5
            // 
            this.comboBox5.BackColor = System.Drawing.Color.Ivory;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(93, 108);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(84, 21);
            this.comboBox5.TabIndex = 79;
            // 
            // comboBox3
            // 
            this.comboBox3.BackColor = System.Drawing.Color.Ivory;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(93, 27);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(84, 21);
            this.comboBox3.TabIndex = 78;
            // 
            // comboBox4NoksAddress
            // 
            this.comboBox4NoksAddress.BackColor = System.Drawing.Color.Ivory;
            this.comboBox4NoksAddress.FormattingEnabled = true;
            this.comboBox4NoksAddress.Location = new System.Drawing.Point(3, 28);
            this.comboBox4NoksAddress.Name = "comboBox4NoksAddress";
            this.comboBox4NoksAddress.Size = new System.Drawing.Size(84, 21);
            this.comboBox4NoksAddress.TabIndex = 77;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "4Noks Test";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // comboBox4
            // 
            this.comboBox4.BackColor = System.Drawing.Color.Ivory;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(3, 108);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(84, 21);
            this.comboBox4.TabIndex = 25;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 20);
            this.textBox1.TabIndex = 26;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(3, 135);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(174, 20);
            this.textBox3.TabIndex = 27;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 378);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(825, 417);
            this.dataGridView1.TabIndex = 40;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // textBox51
            // 
            this.textBox51.Location = new System.Drawing.Point(461, 127);
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new System.Drawing.Size(174, 20);
            this.textBox51.TabIndex = 77;
            this.textBox51.TextChanged += new System.EventHandler(this.textBox51_TextChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(461, 153);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(552, 121);
            this.listBox1.TabIndex = 81;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 374);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox51);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Z4NoksPanel);
            this.Controls.Add(this.XtenderPanel);
            this.Controls.Add(this.SoftStopButtom);
            this.Controls.Add(this.SoftStartButtom);
            this.Controls.Add(this.RemoteControlConnectButtom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RemoteControlDisconnectButtom);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.ZigBee4NoksConnectButtom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ZigBee4NoksDisconnectButtom);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.XtenderConnectButtom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.XtenderDisconnectButtom);
            this.Controls.Add(this.comboBox_ComPorts);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.XtenderPanel.ResumeLayout(false);
            this.XtenderPanel.PerformLayout();
            this.Z4NoksPanel.ResumeLayout(false);
            this.Z4NoksPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_ComPorts;
        private System.Windows.Forms.Button XtenderDisconnectButtom;
        private System.Windows.Forms.Button XtenderConnectButtom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button XtenderDataWriteButton;
        private System.Windows.Forms.Button XtenderDataReadButton;
        private System.Windows.Forms.ComboBox comboBox_XtenderRead;
        private System.Windows.Forms.ComboBox comboBox_XtenderWrite;
        private System.Windows.Forms.TextBox textBox__XtenderRead;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button ZigBee4NoksConnectButtom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ZigBee4NoksDisconnectButtom;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button RemoteControlConnectButtom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RemoteControlDisconnectButtom;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button SoftStartButtom;
        private System.Windows.Forms.Button SoftStopButtom;
        private System.Windows.Forms.Panel XtenderPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Z4NoksPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4NoksAddress;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox51;
        private System.Windows.Forms.ListBox listBox1;


    }
}

