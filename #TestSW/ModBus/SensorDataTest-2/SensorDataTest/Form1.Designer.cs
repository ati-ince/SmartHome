namespace SensorDataTest
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
            this.nThread = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nDevice = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nParameter = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblPerParameter = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMinInsertTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // nThread
            // 
            this.nThread.Location = new System.Drawing.Point(118, 12);
            this.nThread.Name = "nThread";
            this.nThread.Size = new System.Drawing.Size(120, 22);
            this.nThread.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thread Sayısı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cihaz Sayısı:";
            // 
            // nDevice
            // 
            this.nDevice.Location = new System.Drawing.Point(349, 13);
            this.nDevice.Name = "nDevice";
            this.nDevice.Size = new System.Drawing.Size(120, 22);
            this.nDevice.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Parametre Sayısı:";
            // 
            // nParameter
            // 
            this.nParameter.Location = new System.Drawing.Point(607, 13);
            this.nParameter.Name = "nParameter";
            this.nParameter.Size = new System.Drawing.Size(120, 22);
            this.nParameter.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(571, 53);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(652, 53);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Geçen Zaman:";
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.Location = new System.Drawing.Point(124, 53);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(0, 17);
            this.lblElapsed.TabIndex = 9;
            // 
            // lblPerParameter
            // 
            this.lblPerParameter.AutoSize = true;
            this.lblPerParameter.Location = new System.Drawing.Point(245, 82);
            this.lblPerParameter.Name = "lblPerParameter";
            this.lblPerParameter.Size = new System.Drawing.Size(0, 17);
            this.lblPerParameter.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(219, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Parametre Başına Geçen Zaman:";
            // 
            // lblMinInsertTime
            // 
            this.lblMinInsertTime.AutoSize = true;
            this.lblMinInsertTime.Location = new System.Drawing.Point(185, 110);
            this.lblMinInsertTime.Name = "lblMinInsertTime";
            this.lblMinInsertTime.Size = new System.Drawing.Size(0, 17);
            this.lblMinInsertTime.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "En küçük Insert Zamanı:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 149);
            this.Controls.Add(this.lblMinInsertTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblPerParameter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblElapsed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nParameter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nThread);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nThread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nParameter;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label lblPerParameter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMinInsertTime;
        private System.Windows.Forms.Label label7;
    }
}

