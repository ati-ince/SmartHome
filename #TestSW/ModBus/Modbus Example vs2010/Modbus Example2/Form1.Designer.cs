namespace Modbus_Example2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.write0 = new System.Windows.Forms.Button();
            this.write1 = new System.Windows.Forms.Button();
            this.readBit = new System.Windows.Forms.Button();
            this.write16 = new System.Windows.Forms.Button();
            this.read16 = new System.Windows.Forms.Button();
            this.MessageRecieved = new System.Windows.Forms.TextBox();
            this.MessageSent = new System.Windows.Forms.TextBox();
            this.readBitValue = new System.Windows.Forms.TextBox();
            this.read16Value = new System.Windows.Forms.TextBox();
            this.write16Value = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PLCStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.write16Value)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Write Bit (0x05)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Read Bit (0x01)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Write 16Bit Value (0x06)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Read 16Bit Value (0x03)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Value:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(473, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Value:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Message Sent:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Message Recieved:";
            // 
            // write0
            // 
            this.write0.Location = new System.Drawing.Point(39, 44);
            this.write0.Name = "write0";
            this.write0.Size = new System.Drawing.Size(75, 23);
            this.write0.TabIndex = 8;
            this.write0.Text = "0";
            this.write0.UseVisualStyleBackColor = true;
            this.write0.Click += new System.EventHandler(this.write0_Click);
            // 
            // write1
            // 
            this.write1.Location = new System.Drawing.Point(39, 73);
            this.write1.Name = "write1";
            this.write1.Size = new System.Drawing.Size(75, 23);
            this.write1.TabIndex = 9;
            this.write1.Text = "1";
            this.write1.UseVisualStyleBackColor = true;
            this.write1.Click += new System.EventHandler(this.write1_Click);
            // 
            // readBit
            // 
            this.readBit.Location = new System.Drawing.Point(172, 44);
            this.readBit.Name = "readBit";
            this.readBit.Size = new System.Drawing.Size(75, 23);
            this.readBit.TabIndex = 10;
            this.readBit.Text = "Read";
            this.readBit.UseVisualStyleBackColor = true;
            this.readBit.Click += new System.EventHandler(this.readBit_Click);
            // 
            // write16
            // 
            this.write16.Location = new System.Drawing.Point(334, 73);
            this.write16.Name = "write16";
            this.write16.Size = new System.Drawing.Size(75, 23);
            this.write16.TabIndex = 11;
            this.write16.Text = "Write";
            this.write16.UseVisualStyleBackColor = true;
            this.write16.Click += new System.EventHandler(this.write16_Click);
            // 
            // read16
            // 
            this.read16.Location = new System.Drawing.Point(481, 44);
            this.read16.Name = "read16";
            this.read16.Size = new System.Drawing.Size(75, 23);
            this.read16.TabIndex = 12;
            this.read16.Text = "Read";
            this.read16.UseVisualStyleBackColor = true;
            this.read16.Click += new System.EventHandler(this.read16_Click);
            // 
            // MessageRecieved
            // 
            this.MessageRecieved.Location = new System.Drawing.Point(169, 165);
            this.MessageRecieved.Name = "MessageRecieved";
            this.MessageRecieved.Size = new System.Drawing.Size(344, 20);
            this.MessageRecieved.TabIndex = 13;
            // 
            // MessageSent
            // 
            this.MessageSent.Location = new System.Drawing.Point(169, 139);
            this.MessageSent.Name = "MessageSent";
            this.MessageSent.Size = new System.Drawing.Size(344, 20);
            this.MessageSent.TabIndex = 14;
            // 
            // readBitValue
            // 
            this.readBitValue.Location = new System.Drawing.Point(203, 75);
            this.readBitValue.Name = "readBitValue";
            this.readBitValue.Size = new System.Drawing.Size(44, 20);
            this.readBitValue.TabIndex = 15;
            // 
            // read16Value
            // 
            this.read16Value.Location = new System.Drawing.Point(512, 76);
            this.read16Value.Name = "read16Value";
            this.read16Value.Size = new System.Drawing.Size(44, 20);
            this.read16Value.TabIndex = 16;
            // 
            // write16Value
            // 
            this.write16Value.Location = new System.Drawing.Point(334, 44);
            this.write16Value.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.write16Value.Name = "write16Value";
            this.write16Value.Size = new System.Drawing.Size(75, 20);
            this.write16Value.TabIndex = 17;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PLCStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 220);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(604, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PLCStatusLabel
            // 
            this.PLCStatusLabel.Name = "PLCStatusLabel";
            this.PLCStatusLabel.Size = new System.Drawing.Size(128, 17);
            this.PLCStatusLabel.Text = "PLC Connection Status";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 242);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.write16Value);
            this.Controls.Add(this.read16Value);
            this.Controls.Add(this.readBitValue);
            this.Controls.Add(this.MessageSent);
            this.Controls.Add(this.MessageRecieved);
            this.Controls.Add(this.read16);
            this.Controls.Add(this.write16);
            this.Controls.Add(this.readBit);
            this.Controls.Add(this.write1);
            this.Controls.Add(this.write0);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.write16Value)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button write0;
        private System.Windows.Forms.Button write1;
        private System.Windows.Forms.Button readBit;
        private System.Windows.Forms.Button write16;
        private System.Windows.Forms.Button read16;
        private System.Windows.Forms.TextBox MessageRecieved;
        private System.Windows.Forms.TextBox MessageSent;
        private System.Windows.Forms.TextBox readBitValue;
        private System.Windows.Forms.TextBox read16Value;
        private System.Windows.Forms.NumericUpDown write16Value;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel PLCStatusLabel;
    }
}

