namespace RS
{
    partial class SelectBandsForm
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
            this.CLB_Band = new System.Windows.Forms.CheckedListBox();
            this.btn_DrawCompareHistogram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CLB_Band
            // 
            this.CLB_Band.FormattingEnabled = true;
            this.CLB_Band.Location = new System.Drawing.Point(12, 12);
            this.CLB_Band.Name = "CLB_Band";
            this.CLB_Band.Size = new System.Drawing.Size(118, 228);
            this.CLB_Band.TabIndex = 0;
            // 
            // btn_DrawCompareHistogram
            // 
            this.btn_DrawCompareHistogram.Location = new System.Drawing.Point(188, 226);
            this.btn_DrawCompareHistogram.Name = "btn_DrawCompareHistogram";
            this.btn_DrawCompareHistogram.Size = new System.Drawing.Size(75, 23);
            this.btn_DrawCompareHistogram.TabIndex = 1;
            this.btn_DrawCompareHistogram.Text = "绘制";
            this.btn_DrawCompareHistogram.UseVisualStyleBackColor = true;
            this.btn_DrawCompareHistogram.Click += new System.EventHandler(this.btn_DrawCompareHistogram_Click);
            // 
            // SelectBandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btn_DrawCompareHistogram);
            this.Controls.Add(this.CLB_Band);
            this.Name = "SelectBandsForm";
            this.Text = "SelectBandsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CLB_Band;
        private System.Windows.Forms.Button btn_DrawCompareHistogram;
    }
}