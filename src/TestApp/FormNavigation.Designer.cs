namespace TestApp
{
    partial class FormNavigation
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
            this.btnNavigate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.txtRight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDown = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNavPos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGoLeft = new System.Windows.Forms.Button();
            this.btnGoUp = new System.Windows.Forms.Button();
            this.btnGoDown = new System.Windows.Forms.Button();
            this.btnGoRight = new System.Windows.Forms.Button();
            this.lblUpDir = new System.Windows.Forms.Label();
            this.txtClickPoint = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClick = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScreenCenter = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCursorPos = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNavigate
            // 
            this.btnNavigate.Location = new System.Drawing.Point(8, 130);
            this.btnNavigate.Margin = new System.Windows.Forms.Padding(2);
            this.btnNavigate.Name = "btnNavigate";
            this.btnNavigate.Size = new System.Drawing.Size(55, 22);
            this.btnNavigate.TabIndex = 0;
            this.btnNavigate.Text = "Navigate";
            this.btnNavigate.UseVisualStyleBackColor = true;
            this.btnNavigate.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Left:";
            // 
            // txtLeft
            // 
            this.txtLeft.Location = new System.Drawing.Point(65, 47);
            this.txtLeft.Margin = new System.Windows.Forms.Padding(2);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(44, 20);
            this.txtLeft.TabIndex = 2;
            // 
            // txtRight
            // 
            this.txtRight.Location = new System.Drawing.Point(153, 47);
            this.txtRight.Margin = new System.Windows.Forms.Padding(2);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(44, 20);
            this.txtRight.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Right:";
            // 
            // txtUp
            // 
            this.txtUp.Location = new System.Drawing.Point(110, 7);
            this.txtUp.Margin = new System.Windows.Forms.Padding(2);
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new System.Drawing.Size(44, 20);
            this.txtUp.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Up:";
            // 
            // txtDown
            // 
            this.txtDown.Location = new System.Drawing.Point(110, 87);
            this.txtDown.Margin = new System.Windows.Forms.Padding(2);
            this.txtDown.Name = "txtDown";
            this.txtDown.Size = new System.Drawing.Size(44, 20);
            this.txtDown.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Down:";
            // 
            // txtNavPos
            // 
            this.txtNavPos.Location = new System.Drawing.Point(177, 134);
            this.txtNavPos.Margin = new System.Windows.Forms.Padding(2);
            this.txtNavPos.Name = "txtNavPos";
            this.txtNavPos.Size = new System.Drawing.Size(72, 20);
            this.txtNavPos.TabIndex = 9;
            this.txtNavPos.Text = "584,478";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 135);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Nav To:";
            // 
            // btnGoLeft
            // 
            this.btnGoLeft.Location = new System.Drawing.Point(6, 45);
            this.btnGoLeft.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoLeft.Name = "btnGoLeft";
            this.btnGoLeft.Size = new System.Drawing.Size(24, 20);
            this.btnGoLeft.TabIndex = 11;
            this.btnGoLeft.Text = "Go";
            this.btnGoLeft.UseVisualStyleBackColor = true;
            this.btnGoLeft.Click += new System.EventHandler(this.btnGoLeft_Click);
            // 
            // btnGoUp
            // 
            this.btnGoUp.Location = new System.Drawing.Point(55, 5);
            this.btnGoUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoUp.Name = "btnGoUp";
            this.btnGoUp.Size = new System.Drawing.Size(24, 20);
            this.btnGoUp.TabIndex = 12;
            this.btnGoUp.Text = "Go";
            this.btnGoUp.UseVisualStyleBackColor = true;
            this.btnGoUp.Click += new System.EventHandler(this.btnGoUp_Click);
            // 
            // btnGoDown
            // 
            this.btnGoDown.Location = new System.Drawing.Point(43, 85);
            this.btnGoDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoDown.Name = "btnGoDown";
            this.btnGoDown.Size = new System.Drawing.Size(24, 20);
            this.btnGoDown.TabIndex = 13;
            this.btnGoDown.Text = "Go";
            this.btnGoDown.UseVisualStyleBackColor = true;
            this.btnGoDown.Click += new System.EventHandler(this.btnGoDown_Click);
            // 
            // btnGoRight
            // 
            this.btnGoRight.Location = new System.Drawing.Point(236, 45);
            this.btnGoRight.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoRight.Name = "btnGoRight";
            this.btnGoRight.Size = new System.Drawing.Size(24, 20);
            this.btnGoRight.TabIndex = 14;
            this.btnGoRight.Text = "Go";
            this.btnGoRight.UseVisualStyleBackColor = true;
            this.btnGoRight.Click += new System.EventHandler(this.btnGoRight_Click);
            // 
            // lblUpDir
            // 
            this.lblUpDir.AutoSize = true;
            this.lblUpDir.Location = new System.Drawing.Point(253, 121);
            this.lblUpDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpDir.Name = "lblUpDir";
            this.lblUpDir.Size = new System.Drawing.Size(40, 13);
            this.lblUpDir.TabIndex = 16;
            this.lblUpDir.Text = "Up Dir:";
            // 
            // txtClickPoint
            // 
            this.txtClickPoint.Location = new System.Drawing.Point(299, 6);
            this.txtClickPoint.Name = "txtClickPoint";
            this.txtClickPoint.Size = new System.Drawing.Size(46, 20);
            this.txtClickPoint.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Point:";
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(299, 32);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(46, 23);
            this.btnClick.TabIndex = 19;
            this.btnClick.Text = "Click";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Screen Center:";
            // 
            // txtScreenCenter
            // 
            this.txtScreenCenter.Location = new System.Drawing.Point(299, 82);
            this.txtScreenCenter.Name = "txtScreenCenter";
            this.txtScreenCenter.Size = new System.Drawing.Size(46, 20);
            this.txtScreenCenter.TabIndex = 21;
            this.txtScreenCenter.Text = "512,460";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(160, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Cursor";
            // 
            // txtCursorPos
            // 
            this.txtCursorPos.Enabled = false;
            this.txtCursorPos.Location = new System.Drawing.Point(203, 6);
            this.txtCursorPos.Name = "txtCursorPos";
            this.txtCursorPos.Size = new System.Drawing.Size(57, 20);
            this.txtCursorPos.TabIndex = 23;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(67, 130);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(55, 22);
            this.btnStop.TabIndex = 24;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // FormNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 170);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtCursorPos);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtScreenCenter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClick);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtClickPoint);
            this.Controls.Add(this.lblUpDir);
            this.Controls.Add(this.btnGoRight);
            this.Controls.Add(this.btnGoDown);
            this.Controls.Add(this.btnGoUp);
            this.Controls.Add(this.btnGoLeft);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNavPos);
            this.Controls.Add(this.txtDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNavigate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormNavigation";
            this.Text = "FormNavigation";
            this.Load += new System.EventHandler(this.FormNavigation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNavigate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLeft;
        private System.Windows.Forms.TextBox txtRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNavPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGoLeft;
        private System.Windows.Forms.Button btnGoUp;
        private System.Windows.Forms.Button btnGoDown;
        private System.Windows.Forms.Button btnGoRight;
        private System.Windows.Forms.Label lblUpDir;
        private System.Windows.Forms.TextBox txtClickPoint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtScreenCenter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCursorPos;
        private System.Windows.Forms.Button btnStop;
    }
}