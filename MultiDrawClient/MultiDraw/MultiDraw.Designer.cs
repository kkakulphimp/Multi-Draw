namespace MultiDraw
{
    partial class MultiDraw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiDraw));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnConnect = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnColor = new System.Windows.Forms.ToolStripDropDownButton();
            this.lbThickness = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbRXedFrames = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbFragment = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbDestack = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbBytesRxed = new System.Windows.Forms.ToolStripStatusLabel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnColor,
            this.lbThickness,
            this.lbRXedFrames,
            this.lbFragment,
            this.lbDestack,
            this.lbBytesRxed,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnConnect
            // 
            this.btnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(71, 20);
            this.btnConnect.Text = "Connect..";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnColor
            // 
            this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(49, 20);
            this.btnColor.Text = "Color";
            this.btnColor.Click += new System.EventHandler(this.lbColor_Click);
            // 
            // lbThickness
            // 
            this.lbThickness.Name = "lbThickness";
            this.lbThickness.Size = new System.Drawing.Size(62, 17);
            this.lbThickness.Text = "Thickness:";
            // 
            // lbRXedFrames
            // 
            this.lbRXedFrames.Name = "lbRXedFrames";
            this.lbRXedFrames.Size = new System.Drawing.Size(93, 17);
            this.lbRXedFrames.Text = "Frames RX\'ed : 0";
            // 
            // lbFragment
            // 
            this.lbFragment.Name = "lbFragment";
            this.lbFragment.Size = new System.Drawing.Size(78, 17);
            this.lbFragment.Text = "Fragments : 0";
            // 
            // lbDestack
            // 
            this.lbDestack.Name = "lbDestack";
            this.lbDestack.Size = new System.Drawing.Size(90, 17);
            this.lbDestack.Text = "Destack Avg. : 0";
            // 
            // lbBytesRxed
            // 
            this.lbBytesRxed.Name = "lbBytesRxed";
            this.lbBytesRxed.Size = new System.Drawing.Size(80, 17);
            this.lbBytesRxed.Text = "Bytes RXed : 0";
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 50;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // MultiDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MultiDraw";
            this.Text = "MultiDraw";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiDraw_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MultiDraw_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MultiDraw_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MultiDraw_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MultiDraw_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MultiDraw_MouseUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton btnConnect;
        private System.Windows.Forms.ToolStripDropDownButton btnColor;
        private System.Windows.Forms.ToolStripStatusLabel lbThickness;
        private System.Windows.Forms.ToolStripStatusLabel lbRXedFrames;
        private System.Windows.Forms.ToolStripStatusLabel lbFragment;
        private System.Windows.Forms.ToolStripStatusLabel lbDestack;
        private System.Windows.Forms.ToolStripStatusLabel lbBytesRxed;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    }
}

