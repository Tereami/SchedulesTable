
namespace SchedulesTable
{
    partial class FormSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxGetLinkFiles = new System.Windows.Forms.CheckBox();
            this.textBoxComplectParamName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonUseComplects = new System.Windows.Forms.RadioButton();
            this.radioAllProject = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxAltSheetParamName = new System.Windows.Forms.TextBox();
            this.radioButtonCustomSheetNumber = new System.Windows.Forms.RadioButton();
            this.radioButtonUseStandardSheetNumber = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericRowCoeff = new System.Windows.Forms.NumericUpDown();
            this.numericRowHeight = new System.Windows.Forms.NumericUpDown();
            this.numericMaxChars = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowCoeff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxChars)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxGetLinkFiles);
            this.groupBox1.Controls.Add(this.textBoxComplectParamName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonUseComplects);
            this.groupBox1.Controls.Add(this.radioAllProject);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск листов";
            // 
            // checkBoxGetLinkFiles
            // 
            this.checkBoxGetLinkFiles.AutoSize = true;
            this.checkBoxGetLinkFiles.Location = new System.Drawing.Point(6, 108);
            this.checkBoxGetLinkFiles.Name = "checkBoxGetLinkFiles";
            this.checkBoxGetLinkFiles.Size = new System.Drawing.Size(177, 17);
            this.checkBoxGetLinkFiles.TabIndex = 3;
            this.checkBoxGetLinkFiles.Text = "Учитывать связанные файлы";
            this.checkBoxGetLinkFiles.UseVisualStyleBackColor = true;
            // 
            // textBoxComplectParamName
            // 
            this.textBoxComplectParamName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComplectParamName.Enabled = false;
            this.textBoxComplectParamName.Location = new System.Drawing.Point(6, 78);
            this.textBoxComplectParamName.Name = "textBoxComplectParamName";
            this.textBoxComplectParamName.Size = new System.Drawing.Size(204, 20);
            this.textBoxComplectParamName.TabIndex = 2;
            this.textBoxComplectParamName.Text = "Орг.КомплектЧертежей";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Параметр комплекта:";
            // 
            // radioButtonUseComplects
            // 
            this.radioButtonUseComplects.AutoSize = true;
            this.radioButtonUseComplects.Location = new System.Drawing.Point(6, 42);
            this.radioButtonUseComplects.Name = "radioButtonUseComplects";
            this.radioButtonUseComplects.Size = new System.Drawing.Size(177, 17);
            this.radioButtonUseComplects.TabIndex = 0;
            this.radioButtonUseComplects.Text = "Только в текущем комплекте";
            this.radioButtonUseComplects.UseVisualStyleBackColor = true;
            this.radioButtonUseComplects.CheckedChanged += new System.EventHandler(this.radioButtonUseComplects_CheckedChanged);
            // 
            // radioAllProject
            // 
            this.radioAllProject.AutoSize = true;
            this.radioAllProject.Checked = true;
            this.radioAllProject.Location = new System.Drawing.Point(6, 19);
            this.radioAllProject.Name = "radioAllProject";
            this.radioAllProject.Size = new System.Drawing.Size(102, 17);
            this.radioAllProject.TabIndex = 0;
            this.radioAllProject.TabStop = true;
            this.radioAllProject.Text = "Во всём файле";
            this.radioAllProject.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBoxAltSheetParamName);
            this.groupBox2.Controls.Add(this.radioButtonCustomSheetNumber);
            this.groupBox2.Controls.Add(this.radioButtonUseStandardSheetNumber);
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Номер листа";
            // 
            // textBoxAltSheetParamName
            // 
            this.textBoxAltSheetParamName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAltSheetParamName.Enabled = false;
            this.textBoxAltSheetParamName.Location = new System.Drawing.Point(6, 65);
            this.textBoxAltSheetParamName.Name = "textBoxAltSheetParamName";
            this.textBoxAltSheetParamName.Size = new System.Drawing.Size(204, 20);
            this.textBoxAltSheetParamName.TabIndex = 1;
            this.textBoxAltSheetParamName.Text = "SHT_Номер листа";
            // 
            // radioButtonCustomSheetNumber
            // 
            this.radioButtonCustomSheetNumber.AutoSize = true;
            this.radioButtonCustomSheetNumber.Location = new System.Drawing.Point(6, 42);
            this.radioButtonCustomSheetNumber.Name = "radioButtonCustomSheetNumber";
            this.radioButtonCustomSheetNumber.Size = new System.Drawing.Size(177, 17);
            this.radioButtonCustomSheetNumber.TabIndex = 0;
            this.radioButtonCustomSheetNumber.Text = "Пользовательский параметр:";
            this.radioButtonCustomSheetNumber.UseVisualStyleBackColor = true;
            this.radioButtonCustomSheetNumber.CheckedChanged += new System.EventHandler(this.radioButtonCustomSheetNumber_CheckedChanged);
            // 
            // radioButtonUseStandardSheetNumber
            // 
            this.radioButtonUseStandardSheetNumber.AutoSize = true;
            this.radioButtonUseStandardSheetNumber.Checked = true;
            this.radioButtonUseStandardSheetNumber.Location = new System.Drawing.Point(6, 19);
            this.radioButtonUseStandardSheetNumber.Name = "radioButtonUseStandardSheetNumber";
            this.radioButtonUseStandardSheetNumber.Size = new System.Drawing.Size(159, 17);
            this.radioButtonUseStandardSheetNumber.TabIndex = 0;
            this.radioButtonUseStandardSheetNumber.TabStop = true;
            this.radioButtonUseStandardSheetNumber.Text = "Стандартный номер листа";
            this.radioButtonUseStandardSheetNumber.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numericRowCoeff);
            this.groupBox3.Controls.Add(this.numericRowHeight);
            this.groupBox3.Controls.Add(this.numericMaxChars);
            this.groupBox3.Location = new System.Drawing.Point(12, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 148);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Размеры ячеек";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Коэфф. увеличения высоты строки:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Высота строки, мм:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Перенос строки при символах более:";
            // 
            // numericRowCoeff
            // 
            this.numericRowCoeff.DecimalPlaces = 1;
            this.numericRowCoeff.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericRowCoeff.Location = new System.Drawing.Point(6, 119);
            this.numericRowCoeff.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericRowCoeff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRowCoeff.Name = "numericRowCoeff";
            this.numericRowCoeff.Size = new System.Drawing.Size(204, 20);
            this.numericRowCoeff.TabIndex = 0;
            this.numericRowCoeff.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // numericRowHeight
            // 
            this.numericRowHeight.Location = new System.Drawing.Point(6, 35);
            this.numericRowHeight.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericRowHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRowHeight.Name = "numericRowHeight";
            this.numericRowHeight.Size = new System.Drawing.Size(204, 20);
            this.numericRowHeight.TabIndex = 0;
            this.numericRowHeight.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // numericMaxChars
            // 
            this.numericMaxChars.Location = new System.Drawing.Point(6, 77);
            this.numericMaxChars.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericMaxChars.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericMaxChars.Name = "numericMaxChars";
            this.numericMaxChars.Size = new System.Drawing.Size(204, 20);
            this.numericMaxChars.TabIndex = 0;
            this.numericMaxChars.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(86, 417);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(68, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(12, 417);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(68, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "ОК";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(160, 417);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Справка";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(240, 452);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowCoeff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxChars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxGetLinkFiles;
        private System.Windows.Forms.TextBox textBoxComplectParamName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonUseComplects;
        private System.Windows.Forms.RadioButton radioAllProject;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxAltSheetParamName;
        private System.Windows.Forms.RadioButton radioButtonCustomSheetNumber;
        private System.Windows.Forms.RadioButton radioButtonUseStandardSheetNumber;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericMaxChars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericRowCoeff;
        private System.Windows.Forms.NumericUpDown numericRowHeight;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button button1;
    }
}