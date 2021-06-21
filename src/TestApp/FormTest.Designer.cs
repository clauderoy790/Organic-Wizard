namespace TestApp
{
    partial class FormTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtTest = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.lblEnabled = new System.Windows.Forms.Label();
            this.btnNavigate = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lblIsDown = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTest
            // 
            this.txtTest.Location = new System.Drawing.Point(12, 137);
            this.txtTest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(128, 20);
            this.txtTest.TabIndex = 2;
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(12, 13);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(100, 20);
            this.txt1.TabIndex = 3;
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(12, 40);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(100, 20);
            this.txt2.TabIndex = 4;
            // 
            // lblEnabled
            // 
            this.lblEnabled.AutoSize = true;
            this.lblEnabled.Location = new System.Drawing.Point(38, 107);
            this.lblEnabled.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(35, 13);
            this.lblEnabled.TabIndex = 5;
            this.lblEnabled.Text = "label1";
            // 
            // btnNavigate
            // 
            this.btnNavigate.Location = new System.Drawing.Point(192, 13);
            this.btnNavigate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNavigate.Name = "btnNavigate";
            this.btnNavigate.Size = new System.Drawing.Size(62, 35);
            this.btnNavigate.TabIndex = 6;
            this.btnNavigate.Text = "Navigate";
            this.btnNavigate.UseVisualStyleBackColor = true;
            this.btnNavigate.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(288, 19);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(202, 75);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(186, 147);
            this.txtConsole.TabIndex = 8;
            this.txtConsole.Text = "";
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(24, 67);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(49, 23);
            this.btnUp.TabIndex = 9;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(79, 67);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(49, 23);
            this.btnDown.TabIndex = 10;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lblIsDown
            // 
            this.lblIsDown.AutoSize = true;
            this.lblIsDown.Location = new System.Drawing.Point(134, 72);
            this.lblIsDown.Name = "lblIsDown";
            this.lblIsDown.Size = new System.Drawing.Size(35, 13);
            this.lblIsDown.TabIndex = 11;
            this.lblIsDown.Text = "label1";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(134, 97);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(35, 13);
            this.lblValue.TabIndex = 12;
            this.lblValue.Text = "label1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(288, 46);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear Logs";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 234);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblIsDown);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnNavigate);
            this.Controls.Add(this.lblEnabled);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormTest";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.Label lblEnabled;
        private System.Windows.Forms.Button btnNavigate;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label lblIsDown;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

