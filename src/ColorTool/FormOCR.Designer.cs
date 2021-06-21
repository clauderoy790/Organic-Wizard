namespace ColorTool
{
    partial class FormOCR
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
            this.txtTopLeft = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.lblTopLeft = new System.Windows.Forms.Label();
            this.btnSetTopLeft = new System.Windows.Forms.Button();
            this.btnBottomRight = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBottomRight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtZoom = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTopLeft
            // 
            this.txtTopLeft.Enabled = false;
            this.txtTopLeft.Location = new System.Drawing.Point(170, 21);
            this.txtTopLeft.Multiline = true;
            this.txtTopLeft.Name = "txtTopLeft";
            this.txtTopLeft.Size = new System.Drawing.Size(126, 43);
            this.txtTopLeft.TabIndex = 0;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(170, 328);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(146, 55);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // lblTopLeft
            // 
            this.lblTopLeft.AutoSize = true;
            this.lblTopLeft.Location = new System.Drawing.Point(23, 33);
            this.lblTopLeft.Name = "lblTopLeft";
            this.lblTopLeft.Size = new System.Drawing.Size(97, 25);
            this.lblTopLeft.TabIndex = 2;
            this.lblTopLeft.Text = "Top Left:";
            // 
            // btnSetTopLeft
            // 
            this.btnSetTopLeft.Location = new System.Drawing.Point(302, 21);
            this.btnSetTopLeft.Name = "btnSetTopLeft";
            this.btnSetTopLeft.Size = new System.Drawing.Size(91, 43);
            this.btnSetTopLeft.TabIndex = 3;
            this.btnSetTopLeft.Text = "Set";
            this.btnSetTopLeft.UseVisualStyleBackColor = true;
            this.btnSetTopLeft.Click += new System.EventHandler(this.btnSetTopLeft_Click);
            // 
            // btnBottomRight
            // 
            this.btnBottomRight.Location = new System.Drawing.Point(302, 70);
            this.btnBottomRight.Name = "btnBottomRight";
            this.btnBottomRight.Size = new System.Drawing.Size(91, 43);
            this.btnBottomRight.TabIndex = 6;
            this.btnBottomRight.Text = "Set";
            this.btnBottomRight.UseVisualStyleBackColor = true;
            this.btnBottomRight.Click += new System.EventHandler(this.btnBottomRight_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Bottom Right:";
            // 
            // txtBottomRight
            // 
            this.txtBottomRight.Enabled = false;
            this.txtBottomRight.Location = new System.Drawing.Point(170, 70);
            this.txtBottomRight.Multiline = true;
            this.txtBottomRight.Name = "txtBottomRight";
            this.txtBottomRight.Size = new System.Drawing.Size(126, 43);
            this.txtBottomRight.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output:";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(170, 178);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(313, 125);
            this.txtOutput.TabIndex = 8;
            this.txtOutput.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Zoom:";
            // 
            // txtZoom
            // 
            this.txtZoom.Location = new System.Drawing.Point(170, 119);
            this.txtZoom.Multiline = true;
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.Size = new System.Drawing.Size(126, 43);
            this.txtZoom.TabIndex = 9;
            // 
            // FormOCR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 403);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtZoom);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBottomRight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBottomRight);
            this.Controls.Add(this.btnSetTopLeft);
            this.Controls.Add(this.lblTopLeft);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtTopLeft);
            this.Name = "FormOCR";
            this.Text = "Tesseract OCR";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTopLeft;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblTopLeft;
        private System.Windows.Forms.Button btnSetTopLeft;
        private System.Windows.Forms.Button btnBottomRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBottomRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtZoom;
    }
}