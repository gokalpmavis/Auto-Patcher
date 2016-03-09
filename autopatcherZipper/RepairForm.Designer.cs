namespace Patcher.Zipper
{
    partial class RepairForm
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCompress = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.levelComboBox = new System.Windows.Forms.ComboBox();
            this.targetChooserBtn = new System.Windows.Forms.Button();
            this.sourceChooserBtn = new System.Windows.Forms.Button();
            this.targetFolderText = new System.Windows.Forms.TextBox();
            this.sourceFolderText = new System.Windows.Forms.TextBox();
            this.publishBtn = new System.Windows.Forms.Button();
            this.tabIgnore = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.extentions = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.ignoredExtentions = new System.Windows.Forms.ListBox();
            this.deleteIgnoredExtentionbtn = new System.Windows.Forms.Button();
            this.newIgnoredExtention = new System.Windows.Forms.TextBox();
            this.addNewIgnoredExtention = new System.Windows.Forms.Button();
            this.files = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.ignoredFiles = new System.Windows.Forms.ListBox();
            this.deleteIgnoredFilebtn = new System.Windows.Forms.Button();
            this.newIgnoredFile = new System.Windows.Forms.TextBox();
            this.addNewIgnoredFile = new System.Windows.Forms.Button();
            this.folders = new System.Windows.Forms.TabPage();
            this.IgnoredFolderslbl = new System.Windows.Forms.Label();
            this.ignoredFolders = new System.Windows.Forms.ListBox();
            this.deleteIgnoredFolderbtn = new System.Windows.Forms.Button();
            this.newIgnoredFolder = new System.Windows.Forms.TextBox();
            this.addNewIgnoredFolderbtn = new System.Windows.Forms.Button();
            this.includeTab = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.incExtentions = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.includedExtentions = new System.Windows.Forms.ListBox();
            this.deleteIncludedExtention = new System.Windows.Forms.Button();
            this.newIncludedExtention = new System.Windows.Forms.TextBox();
            this.addNewIncludedExtention = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabCompress.SuspendLayout();
            this.tabIgnore.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.extentions.SuspendLayout();
            this.files.SuspendLayout();
            this.folders.SuspendLayout();
            this.includeTab.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.incExtentions.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCompress);
            this.tabControl1.Controls.Add(this.tabIgnore);
            this.tabControl1.Controls.Add(this.includeTab);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 420);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCompress
            // 
            this.tabCompress.Controls.Add(this.label6);
            this.tabCompress.Controls.Add(this.textBox4);
            this.tabCompress.Controls.Add(this.label5);
            this.tabCompress.Controls.Add(this.levelComboBox);
            this.tabCompress.Controls.Add(this.targetChooserBtn);
            this.tabCompress.Controls.Add(this.sourceChooserBtn);
            this.tabCompress.Controls.Add(this.targetFolderText);
            this.tabCompress.Controls.Add(this.sourceFolderText);
            this.tabCompress.Controls.Add(this.publishBtn);
            this.tabCompress.Location = new System.Drawing.Point(4, 34);
            this.tabCompress.Name = "tabCompress";
            this.tabCompress.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompress.Size = new System.Drawing.Size(510, 382);
            this.tabCompress.TabIndex = 0;
            this.tabCompress.Text = "Compress";
            this.tabCompress.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 25);
            this.label6.TabIndex = 21;
            this.label6.Text = "Version";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(363, 200);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 30);
            this.textBox4.TabIndex = 20;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Zip Compression Level (0-9)";
            // 
            // levelComboBox
            // 
            this.levelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelComboBox.FormattingEnabled = true;
            this.levelComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.levelComboBox.Location = new System.Drawing.Point(293, 252);
            this.levelComboBox.Name = "levelComboBox";
            this.levelComboBox.Size = new System.Drawing.Size(51, 33);
            this.levelComboBox.TabIndex = 17;
            this.levelComboBox.SelectedIndexChanged += new System.EventHandler(this.levelComboBox_SelectedIndexChanged);
            // 
            // targetChooserBtn
            // 
            this.targetChooserBtn.Location = new System.Drawing.Point(429, 117);
            this.targetChooserBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.targetChooserBtn.Name = "targetChooserBtn";
            this.targetChooserBtn.Size = new System.Drawing.Size(34, 26);
            this.targetChooserBtn.TabIndex = 13;
            this.targetChooserBtn.Text = "...";
            this.targetChooserBtn.UseVisualStyleBackColor = true;
            this.targetChooserBtn.Click += new System.EventHandler(this.targetChooserBtn_Click);
            // 
            // sourceChooserBtn
            // 
            this.sourceChooserBtn.Location = new System.Drawing.Point(429, 44);
            this.sourceChooserBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sourceChooserBtn.Name = "sourceChooserBtn";
            this.sourceChooserBtn.Size = new System.Drawing.Size(34, 26);
            this.sourceChooserBtn.TabIndex = 12;
            this.sourceChooserBtn.Text = "...";
            this.sourceChooserBtn.UseVisualStyleBackColor = true;
            this.sourceChooserBtn.Click += new System.EventHandler(this.sourceChooserBtn_Click);
            // 
            // targetFolderText
            // 
            this.targetFolderText.Location = new System.Drawing.Point(47, 117);
            this.targetFolderText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.targetFolderText.Name = "targetFolderText";
            this.targetFolderText.Size = new System.Drawing.Size(385, 30);
            this.targetFolderText.TabIndex = 11;
            this.targetFolderText.Text = "C:\\inetpub\\wwwroot\\crafts";
            this.targetFolderText.TextChanged += new System.EventHandler(this.targetFolderText_TextChanged);
            // 
            // sourceFolderText
            // 
            this.sourceFolderText.Location = new System.Drawing.Point(47, 44);
            this.sourceFolderText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sourceFolderText.Name = "sourceFolderText";
            this.sourceFolderText.Size = new System.Drawing.Size(385, 30);
            this.sourceFolderText.TabIndex = 10;
            this.sourceFolderText.Text = "E:\\Develop\\Crafts";
            this.sourceFolderText.TextChanged += new System.EventHandler(this.sourceFolderText_TextChanged);
            // 
            // publishBtn
            // 
            this.publishBtn.Location = new System.Drawing.Point(351, 253);
            this.publishBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.publishBtn.Name = "publishBtn";
            this.publishBtn.Size = new System.Drawing.Size(112, 28);
            this.publishBtn.TabIndex = 9;
            this.publishBtn.Text = "Publish";
            this.publishBtn.UseVisualStyleBackColor = true;
            this.publishBtn.Click += new System.EventHandler(this.publishBtn_Click);
            // 
            // tabIgnore
            // 
            this.tabIgnore.Controls.Add(this.tabControl2);
            this.tabIgnore.Location = new System.Drawing.Point(4, 34);
            this.tabIgnore.Name = "tabIgnore";
            this.tabIgnore.Padding = new System.Windows.Forms.Padding(3);
            this.tabIgnore.Size = new System.Drawing.Size(510, 382);
            this.tabIgnore.TabIndex = 1;
            this.tabIgnore.Text = "Ignore List";
            this.tabIgnore.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.extentions);
            this.tabControl2.Controls.Add(this.files);
            this.tabControl2.Controls.Add(this.folders);
            this.tabControl2.Location = new System.Drawing.Point(6, 16);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(483, 365);
            this.tabControl2.TabIndex = 13;
            // 
            // extentions
            // 
            this.extentions.Controls.Add(this.label1);
            this.extentions.Controls.Add(this.ignoredExtentions);
            this.extentions.Controls.Add(this.deleteIgnoredExtentionbtn);
            this.extentions.Controls.Add(this.newIgnoredExtention);
            this.extentions.Controls.Add(this.addNewIgnoredExtention);
            this.extentions.Location = new System.Drawing.Point(4, 34);
            this.extentions.Name = "extentions";
            this.extentions.Padding = new System.Windows.Forms.Padding(3);
            this.extentions.Size = new System.Drawing.Size(475, 327);
            this.extentions.TabIndex = 0;
            this.extentions.Text = "extentions";
            this.extentions.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(64, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ignored Extentions";
            // 
            // ignoredExtentions
            // 
            this.ignoredExtentions.FormattingEnabled = true;
            this.ignoredExtentions.ItemHeight = 25;
            this.ignoredExtentions.Location = new System.Drawing.Point(67, 34);
            this.ignoredExtentions.Name = "ignoredExtentions";
            this.ignoredExtentions.Size = new System.Drawing.Size(173, 279);
            this.ignoredExtentions.TabIndex = 0;
            // 
            // deleteIgnoredExtentionbtn
            // 
            this.deleteIgnoredExtentionbtn.Location = new System.Drawing.Point(261, 66);
            this.deleteIgnoredExtentionbtn.Name = "deleteIgnoredExtentionbtn";
            this.deleteIgnoredExtentionbtn.Size = new System.Drawing.Size(54, 26);
            this.deleteIgnoredExtentionbtn.TabIndex = 10;
            this.deleteIgnoredExtentionbtn.Text = "Del";
            this.deleteIgnoredExtentionbtn.UseVisualStyleBackColor = true;
            this.deleteIgnoredExtentionbtn.Click += new System.EventHandler(this.deleteExtentionbtn_Click);
            // 
            // newIgnoredExtention
            // 
            this.newIgnoredExtention.Location = new System.Drawing.Point(321, 34);
            this.newIgnoredExtention.Name = "newIgnoredExtention";
            this.newIgnoredExtention.Size = new System.Drawing.Size(57, 30);
            this.newIgnoredExtention.TabIndex = 4;
            // 
            // addNewIgnoredExtention
            // 
            this.addNewIgnoredExtention.Location = new System.Drawing.Point(261, 34);
            this.addNewIgnoredExtention.Name = "addNewIgnoredExtention";
            this.addNewIgnoredExtention.Size = new System.Drawing.Size(54, 26);
            this.addNewIgnoredExtention.TabIndex = 6;
            this.addNewIgnoredExtention.Text = "Add";
            this.addNewIgnoredExtention.UseVisualStyleBackColor = true;
            this.addNewIgnoredExtention.Click += new System.EventHandler(this.addNewIgnoredExtention_Click);
            // 
            // files
            // 
            this.files.Controls.Add(this.label2);
            this.files.Controls.Add(this.ignoredFiles);
            this.files.Controls.Add(this.deleteIgnoredFilebtn);
            this.files.Controls.Add(this.newIgnoredFile);
            this.files.Controls.Add(this.addNewIgnoredFile);
            this.files.Location = new System.Drawing.Point(4, 34);
            this.files.Name = "files";
            this.files.Padding = new System.Windows.Forms.Padding(3);
            this.files.Size = new System.Drawing.Size(475, 327);
            this.files.TabIndex = 1;
            this.files.Text = "files";
            this.files.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ignored Files";
            // 
            // ignoredFiles
            // 
            this.ignoredFiles.FormattingEnabled = true;
            this.ignoredFiles.ItemHeight = 25;
            this.ignoredFiles.Location = new System.Drawing.Point(17, 33);
            this.ignoredFiles.Name = "ignoredFiles";
            this.ignoredFiles.Size = new System.Drawing.Size(239, 254);
            this.ignoredFiles.TabIndex = 2;
            // 
            // deleteIgnoredFilebtn
            // 
            this.deleteIgnoredFilebtn.Location = new System.Drawing.Point(262, 108);
            this.deleteIgnoredFilebtn.Name = "deleteIgnoredFilebtn";
            this.deleteIgnoredFilebtn.Size = new System.Drawing.Size(54, 26);
            this.deleteIgnoredFilebtn.TabIndex = 11;
            this.deleteIgnoredFilebtn.Text = "Del";
            this.deleteIgnoredFilebtn.UseVisualStyleBackColor = true;
            this.deleteIgnoredFilebtn.Click += new System.EventHandler(this.deleteIgnoredFilebtn_Click);
            // 
            // newIgnoredFile
            // 
            this.newIgnoredFile.Location = new System.Drawing.Point(262, 65);
            this.newIgnoredFile.Name = "newIgnoredFile";
            this.newIgnoredFile.Size = new System.Drawing.Size(160, 30);
            this.newIgnoredFile.TabIndex = 7;
            // 
            // addNewIgnoredFile
            // 
            this.addNewIgnoredFile.Location = new System.Drawing.Point(262, 33);
            this.addNewIgnoredFile.Name = "addNewIgnoredFile";
            this.addNewIgnoredFile.Size = new System.Drawing.Size(54, 26);
            this.addNewIgnoredFile.TabIndex = 9;
            this.addNewIgnoredFile.Text = "Add";
            this.addNewIgnoredFile.UseVisualStyleBackColor = true;
            this.addNewIgnoredFile.Click += new System.EventHandler(this.addNewIgnoredFile_Click);
            // 
            // folders
            // 
            this.folders.Controls.Add(this.IgnoredFolderslbl);
            this.folders.Controls.Add(this.ignoredFolders);
            this.folders.Controls.Add(this.deleteIgnoredFolderbtn);
            this.folders.Controls.Add(this.newIgnoredFolder);
            this.folders.Controls.Add(this.addNewIgnoredFolderbtn);
            this.folders.Location = new System.Drawing.Point(4, 34);
            this.folders.Name = "folders";
            this.folders.Padding = new System.Windows.Forms.Padding(3);
            this.folders.Size = new System.Drawing.Size(475, 327);
            this.folders.TabIndex = 2;
            this.folders.Text = "folders";
            this.folders.UseVisualStyleBackColor = true;
            // 
            // IgnoredFolderslbl
            // 
            this.IgnoredFolderslbl.AutoSize = true;
            this.IgnoredFolderslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.IgnoredFolderslbl.Location = new System.Drawing.Point(33, 24);
            this.IgnoredFolderslbl.Name = "IgnoredFolderslbl";
            this.IgnoredFolderslbl.Size = new System.Drawing.Size(305, 24);
            this.IgnoredFolderslbl.TabIndex = 13;
            this.IgnoredFolderslbl.Text = "Ignored Folders (Relative path )";
            // 
            // ignoredFolders
            // 
            this.ignoredFolders.FormattingEnabled = true;
            this.ignoredFolders.ItemHeight = 25;
            this.ignoredFolders.Location = new System.Drawing.Point(36, 45);
            this.ignoredFolders.Name = "ignoredFolders";
            this.ignoredFolders.Size = new System.Drawing.Size(422, 204);
            this.ignoredFolders.TabIndex = 12;
            // 
            // deleteIgnoredFolderbtn
            // 
            this.deleteIgnoredFolderbtn.Location = new System.Drawing.Point(404, 256);
            this.deleteIgnoredFolderbtn.Name = "deleteIgnoredFolderbtn";
            this.deleteIgnoredFolderbtn.Size = new System.Drawing.Size(54, 26);
            this.deleteIgnoredFolderbtn.TabIndex = 16;
            this.deleteIgnoredFolderbtn.Text = "Del";
            this.deleteIgnoredFolderbtn.UseVisualStyleBackColor = true;
            this.deleteIgnoredFolderbtn.Click += new System.EventHandler(this.deleteIgnoredFolderbtn_Click);
            // 
            // newIgnoredFolder
            // 
            this.newIgnoredFolder.Location = new System.Drawing.Point(36, 288);
            this.newIgnoredFolder.Name = "newIgnoredFolder";
            this.newIgnoredFolder.Size = new System.Drawing.Size(422, 30);
            this.newIgnoredFolder.TabIndex = 14;
            // 
            // addNewIgnoredFolderbtn
            // 
            this.addNewIgnoredFolderbtn.Location = new System.Drawing.Point(36, 256);
            this.addNewIgnoredFolderbtn.Name = "addNewIgnoredFolderbtn";
            this.addNewIgnoredFolderbtn.Size = new System.Drawing.Size(54, 26);
            this.addNewIgnoredFolderbtn.TabIndex = 15;
            this.addNewIgnoredFolderbtn.Text = "Add";
            this.addNewIgnoredFolderbtn.UseVisualStyleBackColor = true;
            this.addNewIgnoredFolderbtn.Click += new System.EventHandler(this.addNewIgnoredFolderbtn_Click);
            // 
            // includeTab
            // 
            this.includeTab.Controls.Add(this.tabControl3);
            this.includeTab.Location = new System.Drawing.Point(4, 34);
            this.includeTab.Name = "includeTab";
            this.includeTab.Padding = new System.Windows.Forms.Padding(3);
            this.includeTab.Size = new System.Drawing.Size(510, 382);
            this.includeTab.TabIndex = 2;
            this.includeTab.Text = "Include List";
            this.includeTab.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.incExtentions);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Location = new System.Drawing.Point(14, 11);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(483, 365);
            this.tabControl3.TabIndex = 14;
            // 
            // incExtentions
            // 
            this.incExtentions.Controls.Add(this.label3);
            this.incExtentions.Controls.Add(this.includedExtentions);
            this.incExtentions.Controls.Add(this.deleteIncludedExtention);
            this.incExtentions.Controls.Add(this.newIncludedExtention);
            this.incExtentions.Controls.Add(this.addNewIncludedExtention);
            this.incExtentions.Location = new System.Drawing.Point(4, 34);
            this.incExtentions.Name = "incExtentions";
            this.incExtentions.Padding = new System.Windows.Forms.Padding(3);
            this.incExtentions.Size = new System.Drawing.Size(475, 327);
            this.incExtentions.TabIndex = 0;
            this.incExtentions.Text = "Extentions";
            this.incExtentions.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(64, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Included Extentions";
            // 
            // includedExtentions
            // 
            this.includedExtentions.FormattingEnabled = true;
            this.includedExtentions.ItemHeight = 25;
            this.includedExtentions.Location = new System.Drawing.Point(67, 34);
            this.includedExtentions.Name = "includedExtentions";
            this.includedExtentions.Size = new System.Drawing.Size(173, 279);
            this.includedExtentions.TabIndex = 0;
            // 
            // deleteIncludedExtention
            // 
            this.deleteIncludedExtention.Location = new System.Drawing.Point(261, 66);
            this.deleteIncludedExtention.Name = "deleteIncludedExtention";
            this.deleteIncludedExtention.Size = new System.Drawing.Size(54, 26);
            this.deleteIncludedExtention.TabIndex = 10;
            this.deleteIncludedExtention.Text = "Del";
            this.deleteIncludedExtention.UseVisualStyleBackColor = true;
            this.deleteIncludedExtention.Click += new System.EventHandler(this.deleteIncludedExtention_Click);
            // 
            // newIncludedExtention
            // 
            this.newIncludedExtention.Location = new System.Drawing.Point(321, 34);
            this.newIncludedExtention.Name = "newIncludedExtention";
            this.newIncludedExtention.Size = new System.Drawing.Size(57, 30);
            this.newIncludedExtention.TabIndex = 4;
            // 
            // addNewIncludedExtention
            // 
            this.addNewIncludedExtention.Location = new System.Drawing.Point(261, 34);
            this.addNewIncludedExtention.Name = "addNewIncludedExtention";
            this.addNewIncludedExtention.Size = new System.Drawing.Size(54, 26);
            this.addNewIncludedExtention.TabIndex = 6;
            this.addNewIncludedExtention.Text = "Add";
            this.addNewIncludedExtention.UseVisualStyleBackColor = true;
            this.addNewIncludedExtention.Click += new System.EventHandler(this.addNewIncludedExtention_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(475, 327);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(14, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ignored Files";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(262, 273);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 24);
            this.button5.TabIndex = 12;
            this.button5.Text = "Test";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 25;
            this.listBox2.Location = new System.Drawing.Point(17, 33);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(239, 254);
            this.listBox2.TabIndex = 2;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(262, 108);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(54, 26);
            this.button6.TabIndex = 11;
            this.button6.Text = "Del";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(262, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 30);
            this.textBox2.TabIndex = 7;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(262, 33);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(54, 26);
            this.button7.TabIndex = 9;
            this.button7.Text = "Add";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.listBox3);
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.button9);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(475, 327);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(33, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 24);
            this.label8.TabIndex = 13;
            this.label8.Text = "Ignored Folders";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 25;
            this.listBox3.Location = new System.Drawing.Point(36, 45);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(422, 204);
            this.listBox3.TabIndex = 12;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(404, 256);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(54, 26);
            this.button8.TabIndex = 16;
            this.button8.Text = "Del";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(36, 288);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(422, 30);
            this.textBox3.TabIndex = 14;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(36, 256);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(54, 26);
            this.button9.TabIndex = 15;
            this.button9.Text = "Add";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // RepairForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(547, 454);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(565, 499);
            this.MinimumSize = new System.Drawing.Size(565, 499);
            this.Name = "RepairForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabCompress.ResumeLayout(false);
            this.tabCompress.PerformLayout();
            this.tabIgnore.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.extentions.ResumeLayout(false);
            this.extentions.PerformLayout();
            this.files.ResumeLayout(false);
            this.files.PerformLayout();
            this.folders.ResumeLayout(false);
            this.folders.PerformLayout();
            this.includeTab.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.incExtentions.ResumeLayout(false);
            this.incExtentions.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCompress;
        private System.Windows.Forms.ComboBox levelComboBox;
        private System.Windows.Forms.Button targetChooserBtn;
        private System.Windows.Forms.Button sourceChooserBtn;
        public System.Windows.Forms.TextBox targetFolderText;
        public System.Windows.Forms.TextBox sourceFolderText;
        private System.Windows.Forms.Button publishBtn;
        private System.Windows.Forms.TabPage tabIgnore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ignoredFiles;
        private System.Windows.Forms.Button addNewIgnoredFile;
        private System.Windows.Forms.TextBox newIgnoredFile;
        private System.Windows.Forms.Button deleteIgnoredFilebtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button deleteIgnoredExtentionbtn;
        private System.Windows.Forms.Button addNewIgnoredExtention;
        private System.Windows.Forms.TextBox newIgnoredExtention;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ignoredExtentions;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage extentions;
        private System.Windows.Forms.TabPage files;
        private System.Windows.Forms.TabPage folders;
        private System.Windows.Forms.Label IgnoredFolderslbl;
        private System.Windows.Forms.ListBox ignoredFolders;
        private System.Windows.Forms.Button deleteIgnoredFolderbtn;
        private System.Windows.Forms.TextBox newIgnoredFolder;
        private System.Windows.Forms.Button addNewIgnoredFolderbtn;
        private System.Windows.Forms.TabPage includeTab;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage incExtentions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox includedExtentions;
        private System.Windows.Forms.Button deleteIncludedExtention;
        private System.Windows.Forms.TextBox newIncludedExtention;
        private System.Windows.Forms.Button addNewIncludedExtention;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBox4;
    }
}

