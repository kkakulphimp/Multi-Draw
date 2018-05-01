namespace MultiDraw
{
    partial class ConnectionDialog
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
            this.UI_textBoxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UI_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.UI_buttonConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address";
            // 
            // UI_textBoxAddress
            // 
            this.UI_textBoxAddress.Location = new System.Drawing.Point(13, 30);
            this.UI_textBoxAddress.Name = "UI_textBoxAddress";
            this.UI_textBoxAddress.Size = new System.Drawing.Size(120, 20);
            this.UI_textBoxAddress.TabIndex = 1;
            this.UI_textBoxAddress.Text = "bits.net.nait.ca";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // UI_numericUpDown
            // 
            this.UI_numericUpDown.Location = new System.Drawing.Point(13, 70);
            this.UI_numericUpDown.Maximum = new decimal(new int[] {
            49152,
            0,
            0,
            0});
            this.UI_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UI_numericUpDown.Name = "UI_numericUpDown";
            this.UI_numericUpDown.Size = new System.Drawing.Size(120, 20);
            this.UI_numericUpDown.TabIndex = 3;
            this.UI_numericUpDown.Value = new decimal(new int[] {
            1666,
            0,
            0,
            0});
            // 
            // UI_buttonConnect
            // 
            this.UI_buttonConnect.Location = new System.Drawing.Point(13, 97);
            this.UI_buttonConnect.Name = "UI_buttonConnect";
            this.UI_buttonConnect.Size = new System.Drawing.Size(120, 23);
            this.UI_buttonConnect.TabIndex = 4;
            this.UI_buttonConnect.Text = "Connect";
            this.UI_buttonConnect.UseVisualStyleBackColor = true;
            this.UI_buttonConnect.Click += new System.EventHandler(this.UI_buttonConnect_Click);
            // 
            // ConnectionDialog
            // 
            this.AcceptButton = this.UI_buttonConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(146, 146);
            this.Controls.Add(this.UI_buttonConnect);
            this.Controls.Add(this.UI_numericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UI_textBoxAddress);
            this.Controls.Add(this.label1);
            this.Name = "ConnectionDialog";
            this.Text = "ConnectionDialog";
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UI_textBoxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UI_numericUpDown;
        private System.Windows.Forms.Button UI_buttonConnect;
    }
}