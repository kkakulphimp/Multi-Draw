namespace MultiDrawServer
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.UI_dataGridView = new System.Windows.Forms.DataGridView();
            this.UI_statusStrip = new System.Windows.Forms.StatusStrip();
            this.UI_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_timer = new System.Windows.Forms.Timer(this.components);
            this.UI_toolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            ((System.ComponentModel.ISupportInitialize)(this.UI_dataGridView)).BeginInit();
            this.UI_statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_dataGridView
            // 
            this.UI_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.UI_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UI_dataGridView.Location = new System.Drawing.Point(0, 0);
            this.UI_dataGridView.Name = "UI_dataGridView";
            this.UI_dataGridView.Size = new System.Drawing.Size(610, 599);
            this.UI_dataGridView.TabIndex = 0;
            // 
            // UI_statusStrip
            // 
            this.UI_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UI_toolStripStatusLabel,
            this.UI_toolStripSplitButton});
            this.UI_statusStrip.Location = new System.Drawing.Point(0, 577);
            this.UI_statusStrip.Name = "UI_statusStrip";
            this.UI_statusStrip.Size = new System.Drawing.Size(610, 22);
            this.UI_statusStrip.TabIndex = 1;
            this.UI_statusStrip.Text = "statusStrip1";
            // 
            // UI_toolStripStatusLabel
            // 
            this.UI_toolStripStatusLabel.Name = "UI_toolStripStatusLabel";
            this.UI_toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.UI_toolStripStatusLabel.Text = "words";
            // 
            // UI_timer
            // 
            this.UI_timer.Enabled = true;
            this.UI_timer.Interval = 20;
            this.UI_timer.Tick += new System.EventHandler(this.UI_timer_Tick);
            // 
            // UI_toolStripSplitButton
            // 
            this.UI_toolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UI_toolStripSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("UI_toolStripSplitButton.Image")));
            this.UI_toolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UI_toolStripSplitButton.Name = "UI_toolStripSplitButton";
            this.UI_toolStripSplitButton.Size = new System.Drawing.Size(32, 20);
            this.UI_toolStripSplitButton.Text = "toolStripSplitButton1";
            this.UI_toolStripSplitButton.ButtonClick += new System.EventHandler(this.UI_toolStripSplitButton_ButtonClick);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 599);
            this.Controls.Add(this.UI_statusStrip);
            this.Controls.Add(this.UI_dataGridView);
            this.Name = "Server";
            this.Text = "MultiDraw Server";
            this.Load += new System.EventHandler(this.Server_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UI_dataGridView)).EndInit();
            this.UI_statusStrip.ResumeLayout(false);
            this.UI_statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UI_dataGridView;
        private System.Windows.Forms.StatusStrip UI_statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel;
        private System.Windows.Forms.Timer UI_timer;
        private System.Windows.Forms.ToolStripSplitButton UI_toolStripSplitButton;
    }
}

