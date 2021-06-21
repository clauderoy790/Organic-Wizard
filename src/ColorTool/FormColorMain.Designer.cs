using System.Windows.Forms;

namespace ColorTool
{
    partial class FormColorMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRgb = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtDiff = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnHex = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblResearch = new System.Windows.Forms.Label();
            this.panColor = new System.Windows.Forms.Panel();
            this.btnCompare = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panSavedColor = new System.Windows.Forms.Panel();
            this.txtSavedRgb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSavedY = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSavedX = new System.Windows.Forms.TextBox();
            this.btnPointGroup = new System.Windows.Forms.Button();
            this.btnTesseract = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 196);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "RGB Color:";
            // 
            // txtRgb
            // 
            this.txtRgb.Enabled = false;
            this.txtRgb.Location = new System.Drawing.Point(128, 190);
            this.txtRgb.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtRgb.Name = "txtRgb";
            this.txtRgb.Size = new System.Drawing.Size(178, 31);
            this.txtRgb.TabIndex = 2;
            // 
            // txtX
            // 
            this.txtX.Enabled = false;
            this.txtX.Location = new System.Drawing.Point(68, 23);
            this.txtX.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(80, 31);
            this.txtX.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y:";
            // 
            // txtY
            // 
            this.txtY.Enabled = false;
            this.txtY.Location = new System.Drawing.Point(244, 23);
            this.txtY.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(80, 31);
            this.txtY.TabIndex = 7;
            // 
            // txtDiff
            // 
            this.txtDiff.Enabled = false;
            this.txtDiff.Location = new System.Drawing.Point(128, 290);
            this.txtDiff.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDiff.Name = "txtDiff";
            this.txtDiff.Size = new System.Drawing.Size(178, 31);
            this.txtDiff.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 300);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Diff Color:";
            // 
            // btnHex
            // 
            this.btnHex.Location = new System.Drawing.Point(324, 196);
            this.btnHex.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnHex.Name = "btnHex";
            this.btnHex.Size = new System.Drawing.Size(126, 44);
            this.btnHex.TabIndex = 16;
            this.btnHex.Text = "Copy";
            this.btnHex.UseVisualStyleBackColor = true;
            this.btnHex.Click += new System.EventHandler(this.btnRGB_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-50, 508);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "ImgProcessText";
            // 
            // txtProcess
            // 
            this.txtProcess.Enabled = false;
            this.txtProcess.Location = new System.Drawing.Point(128, 504);
            this.txtProcess.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(178, 31);
            this.txtProcess.TabIndex = 21;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(128, 140);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(178, 31);
            this.txtSearch.TabIndex = 23;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblResearch
            // 
            this.lblResearch.AutoSize = true;
            this.lblResearch.Location = new System.Drawing.Point(22, 146);
            this.lblResearch.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResearch.Name = "lblResearch";
            this.lblResearch.Size = new System.Drawing.Size(110, 25);
            this.lblResearch.TabIndex = 22;
            this.lblResearch.Text = "Research:";
            // 
            // panColor
            // 
            this.panColor.Location = new System.Drawing.Point(340, 23);
            this.panColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panColor.Name = "panColor";
            this.panColor.Size = new System.Drawing.Size(126, 119);
            this.panColor.TabIndex = 24;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(424, 588);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(126, 44);
            this.btnCompare.TabIndex = 25;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-14, 363);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 25);
            this.label7.TabIndex = 26;
            this.label7.Text = "Saved Color";
            // 
            // panSavedColor
            // 
            this.panSavedColor.Location = new System.Drawing.Point(346, 404);
            this.panSavedColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panSavedColor.Name = "panSavedColor";
            this.panSavedColor.Size = new System.Drawing.Size(102, 54);
            this.panSavedColor.TabIndex = 27;
            // 
            // txtSavedRgb
            // 
            this.txtSavedRgb.Enabled = false;
            this.txtSavedRgb.Location = new System.Drawing.Point(128, 454);
            this.txtSavedRgb.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSavedRgb.Name = "txtSavedRgb";
            this.txtSavedRgb.Size = new System.Drawing.Size(178, 31);
            this.txtSavedRgb.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 460);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 25);
            this.label8.TabIndex = 28;
            this.label8.Text = "RGB Color:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(198, 410);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 25);
            this.label9.TabIndex = 33;
            this.label9.Text = "Y:";
            // 
            // txtSavedY
            // 
            this.txtSavedY.Enabled = false;
            this.txtSavedY.Location = new System.Drawing.Point(244, 404);
            this.txtSavedY.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSavedY.Name = "txtSavedY";
            this.txtSavedY.Size = new System.Drawing.Size(80, 31);
            this.txtSavedY.TabIndex = 32;
            this.txtSavedY.Text = "37";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 410);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 25);
            this.label10.TabIndex = 31;
            this.label10.Text = "X:";
            // 
            // txtSavedX
            // 
            this.txtSavedX.Enabled = false;
            this.txtSavedX.Location = new System.Drawing.Point(68, 404);
            this.txtSavedX.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSavedX.Name = "txtSavedX";
            this.txtSavedX.Size = new System.Drawing.Size(80, 31);
            this.txtSavedX.TabIndex = 30;
            this.txtSavedX.Text = "216";
            // 
            // btnPointGroup
            // 
            this.btnPointGroup.Location = new System.Drawing.Point(288, 588);
            this.btnPointGroup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPointGroup.Name = "btnPointGroup";
            this.btnPointGroup.Size = new System.Drawing.Size(126, 44);
            this.btnPointGroup.TabIndex = 34;
            this.btnPointGroup.Text = "Group";
            this.btnPointGroup.UseVisualStyleBackColor = true;
            this.btnPointGroup.Click += new System.EventHandler(this.btnPointGroup_Click);
            // 
            // btnTesseract
            // 
            this.btnTesseract.Location = new System.Drawing.Point(150, 588);
            this.btnTesseract.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnTesseract.Name = "btnTesseract";
            this.btnTesseract.Size = new System.Drawing.Size(126, 44);
            this.btnTesseract.TabIndex = 35;
            this.btnTesseract.Text = "OCR";
            this.btnTesseract.UseVisualStyleBackColor = true;
            this.btnTesseract.Click += new System.EventHandler(this.btnTesseract_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(12, 588);
            this.btnFind.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(126, 44);
            this.btnFind.TabIndex = 36;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // FormColorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 648);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnTesseract);
            this.Controls.Add(this.btnPointGroup);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSavedY);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSavedX);
            this.Controls.Add(this.txtSavedRgb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panSavedColor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.panColor);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblResearch);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnHex);
            this.Controls.Add(this.txtDiff);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtRgb);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FormColorMain";
            this.Text = "Color Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRgb;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtDiff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnHex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblResearch;
        private Panel panColor;
        private Button btnCompare;
        private Label label7;
        private Panel panSavedColor;
        private TextBox txtSavedRgb;
        private Label label8;
        private Label label9;
        private TextBox txtSavedY;
        private Label label10;
        private TextBox txtSavedX;
        private Button btnPointGroup;
        private Button btnTesseract;
        private Button btnFind;
    }
}