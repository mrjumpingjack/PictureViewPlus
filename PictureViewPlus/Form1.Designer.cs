namespace PictureViewPlus
{
    partial class PictureViewPlus
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.öffnenInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllMetaDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picbox = new DDPanBox.DDPanBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(284, 262);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick_1);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.öffnenInToolStripMenuItem,
            this.showInExplorerToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 92);
            // 
            // öffnenInToolStripMenuItem
            // 
            this.öffnenInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.öffnenInToolStripMenuItem.Name = "öffnenInToolStripMenuItem";
            this.öffnenInToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.öffnenInToolStripMenuItem.Text = "Open in";
            this.öffnenInToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.öffnenInToolStripMenuItem_DropDownItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // showInExplorerToolStripMenuItem
            // 
            this.showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
            this.showInExplorerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.showInExplorerToolStripMenuItem.Text = "Show in explorer";
            this.showInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInExplorerToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateLeftToolStripMenuItem,
            this.rotateRightToolStripMenuItem,
            this.flipToolStripMenuItem,
            this.mirrorToolStripMenuItem,
            this.showAllMetaDataToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // rotateLeftToolStripMenuItem
            // 
            this.rotateLeftToolStripMenuItem.Name = "rotateLeftToolStripMenuItem";
            this.rotateLeftToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.rotateLeftToolStripMenuItem.Text = "Rotate left";
            this.rotateLeftToolStripMenuItem.Click += new System.EventHandler(this.rotateLeftToolStripMenuItem_Click);
            // 
            // rotateRightToolStripMenuItem
            // 
            this.rotateRightToolStripMenuItem.Name = "rotateRightToolStripMenuItem";
            this.rotateRightToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.rotateRightToolStripMenuItem.Text = "Rotate Right";
            this.rotateRightToolStripMenuItem.Click += new System.EventHandler(this.rotateRightToolStripMenuItem_Click);
            // 
            // flipToolStripMenuItem
            // 
            this.flipToolStripMenuItem.Name = "flipToolStripMenuItem";
            this.flipToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.flipToolStripMenuItem.Text = "Flip";
            this.flipToolStripMenuItem.Click += new System.EventHandler(this.flipToolStripMenuItem_Click);
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            this.mirrorToolStripMenuItem.Click += new System.EventHandler(this.mirrorToolStripMenuItem_Click);
            // 
            // showAllMetaDataToolStripMenuItem
            // 
            this.showAllMetaDataToolStripMenuItem.Name = "showAllMetaDataToolStripMenuItem";
            this.showAllMetaDataToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.showAllMetaDataToolStripMenuItem.Text = "Show all meta data";
            this.showAllMetaDataToolStripMenuItem.Click += new System.EventHandler(this.showAllMetaDataToolStripMenuItem_Click);
            // 
            // picbox
            // 
            this.picbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbox.Image = null;
            this.picbox.Location = new System.Drawing.Point(0, 0);
            this.picbox.Name = "picbox";
            this.picbox.Size = new System.Drawing.Size(284, 262);
            this.picbox.TabIndex = 0;
            this.picbox.TabStop = false;
            this.picbox.Text = "ddPanBox1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 28);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // PictureViewPlus
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picbox);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PictureViewPlus";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DDPanBox.DDPanBox picbox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem öffnenInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllMetaDataToolStripMenuItem;
    }
}

