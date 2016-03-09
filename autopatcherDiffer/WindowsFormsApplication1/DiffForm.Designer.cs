using System;
namespace Patcher.Diff
{
    partial class DiffForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.newVersionPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.browseNew = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog3 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog4 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog5 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog6 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog7 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog8 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog9 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog10 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog11 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog12 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.browseOld = new System.Windows.Forms.Button();
            this.oldVersionPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.browseDestiny = new System.Windows.Forms.Button();
            this.destinyPath = new System.Windows.Forms.TextBox();
            this.newVersion = new System.Windows.Forms.TextBox();
            this.oldVersion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // newVersionPath
            // 
            this.newVersionPath.BackColor = System.Drawing.SystemColors.Window;
            this.newVersionPath.Location = new System.Drawing.Point(101, 162);
            this.newVersionPath.Margin = new System.Windows.Forms.Padding(4);
            this.newVersionPath.Name = "newVersionPath";
            this.newVersionPath.Size = new System.Drawing.Size(493, 22);
            this.newVersionPath.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.BurlyWood;
            this.button1.ForeColor = System.Drawing.Color.Maroon;
            this.button1.Location = new System.Drawing.Point(272, 346);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // browseNew
            // 
            this.browseNew.BackColor = System.Drawing.Color.BurlyWood;
            this.browseNew.ForeColor = System.Drawing.Color.Maroon;
            this.browseNew.Location = new System.Drawing.Point(723, 161);
            this.browseNew.Margin = new System.Windows.Forms.Padding(4);
            this.browseNew.Name = "browseNew";
            this.browseNew.Size = new System.Drawing.Size(129, 25);
            this.browseNew.TabIndex = 2;
            this.browseNew.Text = "Browse";
            this.browseNew.UseVisualStyleBackColor = false;
            this.browseNew.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 36);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2,
            this.toolStripComboBox3});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(188, 68);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(121, 28);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(16, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(551, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please select directories of folders that you want to learn the differences";
            // 
            // browseOld
            // 
            this.browseOld.BackColor = System.Drawing.Color.BurlyWood;
            this.browseOld.ForeColor = System.Drawing.Color.Maroon;
            this.browseOld.Location = new System.Drawing.Point(723, 206);
            this.browseOld.Margin = new System.Windows.Forms.Padding(4);
            this.browseOld.Name = "browseOld";
            this.browseOld.Size = new System.Drawing.Size(129, 25);
            this.browseOld.TabIndex = 4;
            this.browseOld.Text = "Browse";
            this.browseOld.UseVisualStyleBackColor = false;
            this.browseOld.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // oldVersionPath
            // 
            this.oldVersionPath.Location = new System.Drawing.Point(101, 207);
            this.oldVersionPath.Margin = new System.Windows.Forms.Padding(4);
            this.oldVersionPath.Name = "oldVersionPath";
            this.oldVersionPath.Size = new System.Drawing.Size(493, 22);
            this.oldVersionPath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(33, 166);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "NEW";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(33, 207);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "OLD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(16, 250);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "DESTINY";
            // 
            // browseDestiny
            // 
            this.browseDestiny.BackColor = System.Drawing.Color.BurlyWood;
            this.browseDestiny.ForeColor = System.Drawing.Color.Maroon;
            this.browseDestiny.Location = new System.Drawing.Point(723, 246);
            this.browseDestiny.Margin = new System.Windows.Forms.Padding(4);
            this.browseDestiny.Name = "browseDestiny";
            this.browseDestiny.Size = new System.Drawing.Size(129, 25);
            this.browseDestiny.TabIndex = 9;
            this.browseDestiny.Text = "Browse";
            this.browseDestiny.UseVisualStyleBackColor = false;
            this.browseDestiny.Click += new System.EventHandler(this.button3_Click);
            // 
            // destinyPath
            // 
            this.destinyPath.BackColor = System.Drawing.SystemColors.Window;
            this.destinyPath.Location = new System.Drawing.Point(101, 250);
            this.destinyPath.Margin = new System.Windows.Forms.Padding(4);
            this.destinyPath.Name = "destinyPath";
            this.destinyPath.Size = new System.Drawing.Size(493, 22);
            this.destinyPath.TabIndex = 8;
            // 
            // newVersion
            // 
            this.newVersion.Location = new System.Drawing.Point(616, 161);
            this.newVersion.Name = "newVersion";
            this.newVersion.Size = new System.Drawing.Size(100, 22);
            this.newVersion.TabIndex = 11;
            // 
            // oldVersion
            // 
            this.oldVersion.Location = new System.Drawing.Point(616, 206);
            this.oldVersion.Name = "oldVersion";
            this.oldVersion.Size = new System.Drawing.Size(100, 22);
            this.oldVersion.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(613, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Version";
            // 
            // DiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(865, 478);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.oldVersion);
            this.Controls.Add(this.newVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.browseDestiny);
            this.Controls.Add(this.destinyPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.oldVersionPath);
            this.Controls.Add(this.browseOld);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseNew);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.newVersionPath);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DiffForm";
            this.Text = "Repos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public int testFlag = 0;
        public string versionNew, versionOld;

        public System.Windows.Forms.TextBox newVersionPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button browseNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog6;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog8;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog9;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog10;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog11;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog12;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browseOld;
        public System.Windows.Forms.TextBox oldVersionPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browseDestiny;
        public System.Windows.Forms.TextBox destinyPath;
        private System.Windows.Forms.TextBox newVersion;
        private System.Windows.Forms.TextBox oldVersion;
        private System.Windows.Forms.Label label5;
    }
}

