namespace WindowsFormsApplication1
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
            Telerik.Reporting.TypeReportSource typeReportSource14 = new Telerik.Reporting.TypeReportSource();
            this.reportViewer1 = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.ChangeReport = new System.Windows.Forms.Button();
            this.ServerList = new System.Windows.Forms.ComboBox();
            this.CheckListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FieldBox = new System.Windows.Forms.TextBox();
            this.SortDir = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equalityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterOptions = new System.Windows.Forms.GroupBox();
            this.SetDateFilter = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ValueGroup = new System.Windows.Forms.GroupBox();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ValueOperator = new System.Windows.Forms.ComboBox();
            this.TopBot = new System.Windows.Forms.ComboBox();
            this.ValueFilterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.FilterOptions.SuspendLayout();
            this.ValueGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.reportViewer1.Location = new System.Drawing.Point(0, 24);
            this.reportViewer1.Name = "reportViewer1";
            typeReportSource14.TypeName = "ReportLibrary2.Report1, ReportLibrary2, Version=1.0.0.0, Culture=neutral, PublicK" +
    "eyToken=null";
            this.reportViewer1.ReportSource = typeReportSource14;
            this.reportViewer1.Size = new System.Drawing.Size(652, 656);
            this.reportViewer1.TabIndex = 0;
            // 
            // ChangeReport
            // 
            this.ChangeReport.Location = new System.Drawing.Point(928, 645);
            this.ChangeReport.Name = "ChangeReport";
            this.ChangeReport.Size = new System.Drawing.Size(137, 23);
            this.ChangeReport.TabIndex = 2;
            this.ChangeReport.Text = "Change Report";
            this.ChangeReport.UseVisualStyleBackColor = true;
            this.ChangeReport.Click += new System.EventHandler(this.ChangeReport_Click);
            // 
            // ServerList
            // 
            this.ServerList.FormattingEnabled = true;
            this.ServerList.Items.AddRange(new object[] {
            "Baker",
            "Benton",
            "Clackamas",
            "Clatsop",
            "Columbia",
            "Coos",
            "Crook",
            "Curry",
            "Douglas",
            "Gilliam",
            "GrantCo",
            "Harney",
            "Hood",
            "Jackson",
            "Jefferson",
            "Josephine",
            "Klamath",
            "Lake",
            "Lincoln",
            "Linn",
            "Marion",
            "Morrow",
            "Polk",
            "Sherman",
            "Tillamook",
            "Umatilla",
            "UnionCo",
            "Wallowa",
            "Wasco",
            "Wheeler"});
            this.ServerList.Location = new System.Drawing.Point(830, 41);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(121, 21);
            this.ServerList.TabIndex = 3;
            this.ServerList.SelectedIndexChanged += new System.EventHandler(this.ServerList_SelectedIndexChanged);
            // 
            // CheckListBox
            // 
            this.CheckListBox.CheckOnClick = true;
            this.CheckListBox.FormattingEnabled = true;
            this.CheckListBox.Items.AddRange(new object[] {
            "Activity_Key",
            "Name",
            "Description",
            "NameDesc",
            "DescName",
            "Perform_Standard",
            "Work_Unit",
            "WorkComp_Key",
            "UOM_Key",
            "Work_Methods",
            "Inspection",
            "Authorize",
            "Active",
            "User1",
            "User2",
            "User3",
            "User4",
            "User5",
            "User6",
            "User7",
            "User8",
            "User9",
            "User10",
            "CreateDate",
            "DateStamp",
            "SecurityUser_Key"});
            this.CheckListBox.Location = new System.Drawing.Point(658, 27);
            this.CheckListBox.Name = "CheckListBox";
            this.CheckListBox.Size = new System.Drawing.Size(120, 424);
            this.CheckListBox.TabIndex = 5;
            this.CheckListBox.SelectedIndexChanged += new System.EventHandler(this.CheckListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(831, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Sort by Field";
            // 
            // FieldBox
            // 
            this.FieldBox.Location = new System.Drawing.Point(912, 68);
            this.FieldBox.Name = "FieldBox";
            this.FieldBox.Size = new System.Drawing.Size(142, 20);
            this.FieldBox.TabIndex = 7;
            this.FieldBox.Text = "Description";
            this.FieldBox.TextChanged += new System.EventHandler(this.FieldBox_TextChanged);
            // 
            // SortDir
            // 
            this.SortDir.FormattingEnabled = true;
            this.SortDir.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.SortDir.Location = new System.Drawing.Point(912, 95);
            this.SortDir.Name = "SortDir";
            this.SortDir.Size = new System.Drawing.Size(121, 21);
            this.SortDir.TabIndex = 8;
            this.SortDir.SelectedIndexChanged += new System.EventHandler(this.SortDir_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(834, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Direction";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(912, 135);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Sort?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1077, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterOptionsToolStripMenuItem,
            this.percentToolStripMenuItem,
            this.equalityToolStripMenuItem,
            this.valueToolStripMenuItem,
            this.stringToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // filterOptionsToolStripMenuItem
            // 
            this.filterOptionsToolStripMenuItem.Name = "filterOptionsToolStripMenuItem";
            this.filterOptionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.filterOptionsToolStripMenuItem.Text = "Date";
            this.filterOptionsToolStripMenuItem.Click += new System.EventHandler(this.filterOptionsToolStripMenuItem_Click);
            // 
            // percentToolStripMenuItem
            // 
            this.percentToolStripMenuItem.Name = "percentToolStripMenuItem";
            this.percentToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.percentToolStripMenuItem.Text = "Percent";
            // 
            // equalityToolStripMenuItem
            // 
            this.equalityToolStripMenuItem.Name = "equalityToolStripMenuItem";
            this.equalityToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.equalityToolStripMenuItem.Text = "Equality";
            // 
            // valueToolStripMenuItem
            // 
            this.valueToolStripMenuItem.Name = "valueToolStripMenuItem";
            this.valueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.valueToolStripMenuItem.Text = "Value";
            this.valueToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // stringToolStripMenuItem
            // 
            this.stringToolStripMenuItem.Name = "stringToolStripMenuItem";
            this.stringToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.stringToolStripMenuItem.Text = "String";
            // 
            // FilterOptions
            // 
            this.FilterOptions.Controls.Add(this.label4);
            this.FilterOptions.Controls.Add(this.label3);
            this.FilterOptions.Controls.Add(this.SetDateFilter);
            this.FilterOptions.Controls.Add(this.dateTimePicker2);
            this.FilterOptions.Controls.Add(this.dateTimePicker1);
            this.FilterOptions.Location = new System.Drawing.Point(0, 24);
            this.FilterOptions.Name = "FilterOptions";
            this.FilterOptions.Size = new System.Drawing.Size(681, 91);
            this.FilterOptions.TabIndex = 12;
            this.FilterOptions.TabStop = false;
            this.FilterOptions.Text = "Date";
            this.FilterOptions.Visible = false;
            // 
            // SetDateFilter
            // 
            this.SetDateFilter.Location = new System.Drawing.Point(526, 27);
            this.SetDateFilter.Name = "SetDateFilter";
            this.SetDateFilter.Size = new System.Drawing.Size(75, 23);
            this.SetDateFilter.TabIndex = 2;
            this.SetDateFilter.Text = "Set Filter";
            this.SetDateFilter.UseVisualStyleBackColor = true;
            this.SetDateFilter.Click += new System.EventHandler(this.SetDateFilter_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(285, 44);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(21, 44);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2015, 9, 11, 13, 46, 52, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // ValueGroup
            // 
            this.ValueGroup.Controls.Add(this.ValueFilterButton);
            this.ValueGroup.Controls.Add(this.ValueTextBox);
            this.ValueGroup.Controls.Add(this.ValueOperator);
            this.ValueGroup.Controls.Add(this.TopBot);
            this.ValueGroup.Location = new System.Drawing.Point(0, 24);
            this.ValueGroup.Name = "ValueGroup";
            this.ValueGroup.Size = new System.Drawing.Size(589, 84);
            this.ValueGroup.TabIndex = 13;
            this.ValueGroup.TabStop = false;
            this.ValueGroup.Text = "Value Filter";
            this.ValueGroup.Visible = false;
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(335, 29);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.ValueTextBox.TabIndex = 2;
            this.ValueTextBox.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // ValueOperator
            // 
            this.ValueOperator.FormattingEnabled = true;
            this.ValueOperator.Items.AddRange(new object[] {
            "Value",
            "Percent"});
            this.ValueOperator.Location = new System.Drawing.Point(169, 29);
            this.ValueOperator.Name = "ValueOperator";
            this.ValueOperator.Size = new System.Drawing.Size(121, 21);
            this.ValueOperator.TabIndex = 1;
            this.ValueOperator.SelectedIndexChanged += new System.EventHandler(this.ValueOperator_SelectedIndexChanged);
            // 
            // TopBot
            // 
            this.TopBot.DisplayMember = "(none)";
            this.TopBot.FormattingEnabled = true;
            this.TopBot.Items.AddRange(new object[] {
            "Top",
            "Bottom"});
            this.TopBot.Location = new System.Drawing.Point(21, 29);
            this.TopBot.Name = "TopBot";
            this.TopBot.Size = new System.Drawing.Size(121, 21);
            this.TopBot.TabIndex = 0;
            this.TopBot.SelectedIndexChanged += new System.EventHandler(this.TopBot_SelectedIndexChanged);
            // 
            // ValueFilterButton
            // 
            this.ValueFilterButton.Location = new System.Drawing.Point(491, 29);
            this.ValueFilterButton.Name = "ValueFilterButton";
            this.ValueFilterButton.Size = new System.Drawing.Size(75, 23);
            this.ValueFilterButton.TabIndex = 3;
            this.ValueFilterButton.Text = "Set Filter";
            this.ValueFilterButton.UseVisualStyleBackColor = true;
            this.ValueFilterButton.Click += new System.EventHandler(this.ValueFilterButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Begin Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "End Date";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(837, 362);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(217, 20);
            this.textBox1.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 680);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.FilterOptions);
            this.Controls.Add(this.ValueGroup);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SortDir);
            this.Controls.Add(this.FieldBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckListBox);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.ChangeReport);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.FilterOptions.ResumeLayout(false);
            this.FilterOptions.PerformLayout();
            this.ValueGroup.ResumeLayout(false);
            this.ValueGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button ChangeReport;
        private System.Windows.Forms.ComboBox ServerList;
        private System.Windows.Forms.CheckedListBox CheckListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FieldBox;
        private System.Windows.Forms.ComboBox SortDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterOptionsToolStripMenuItem;
        private System.Windows.Forms.GroupBox FilterOptions;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ToolStripMenuItem percentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equalityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringToolStripMenuItem;
        private System.Windows.Forms.Button SetDateFilter;
        private System.Windows.Forms.GroupBox ValueGroup;
        private System.Windows.Forms.ComboBox ValueOperator;
        private System.Windows.Forms.ComboBox TopBot;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.Button ValueFilterButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

