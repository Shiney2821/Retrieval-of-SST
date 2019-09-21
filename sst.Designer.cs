namespace RS
{
    partial class sst
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
            this.tb_B22 = new System.Windows.Forms.TextBox();
            this.tb_B23 = new System.Windows.Forms.TextBox();
            this.btn_SST = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_SZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "热红外通道选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Band22";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Band23";
            // 
            // tb_B22
            // 
            this.tb_B22.Location = new System.Drawing.Point(130, 58);
            this.tb_B22.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_B22.Name = "tb_B22";
            this.tb_B22.Size = new System.Drawing.Size(148, 28);
            this.tb_B22.TabIndex = 3;
            this.tb_B22.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_B22_MouseDown);
            // 
            // tb_B23
            // 
            this.tb_B23.Location = new System.Drawing.Point(130, 102);
            this.tb_B23.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_B23.Name = "tb_B23";
            this.tb_B23.Size = new System.Drawing.Size(148, 28);
            this.tb_B23.TabIndex = 4;
            this.tb_B23.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_B23_MouseDown);
            // 
            // btn_SST
            // 
            this.btn_SST.Location = new System.Drawing.Point(93, 267);
            this.btn_SST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_SST.Name = "btn_SST";
            this.btn_SST.Size = new System.Drawing.Size(112, 34);
            this.btn_SST.TabIndex = 5;
            this.btn_SST.Text = "确认";
            this.btn_SST.UseVisualStyleBackColor = true;
            this.btn_SST.Click += new System.EventHandler(this.btn_SST_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "传感器天顶角数据选择";
            // 
            // tb_SZ
            // 
            this.tb_SZ.Location = new System.Drawing.Point(130, 194);
            this.tb_SZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_SZ.Name = "tb_SZ";
            this.tb_SZ.Size = new System.Drawing.Size(148, 28);
            this.tb_SZ.TabIndex = 7;
            this.tb_SZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_SZ_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 198);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "SensorZenith";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 298);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 18);
            this.label7.TabIndex = 10;
            // 
            // sst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 310);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_SZ);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_SST);
            this.Controls.Add(this.tb_B23);
            this.Controls.Add(this.tb_B22);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "sst";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "sst";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_B22;
        private System.Windows.Forms.TextBox tb_B23;
        private System.Windows.Forms.Button btn_SST;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_SZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}