namespace ColorTool
{
    partial class FormColorCompare
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
            this.panColor = new System.Windows.Forms.Panel();
            this.txtRGB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIntColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHexColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colDiag = new System.Windows.Forms.ColorDialog();
            this.btnPick1 = new System.Windows.Forms.Button();
            this.btnPick2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panColor2 = new System.Windows.Forms.Panel();
            this.txtRGB2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInt2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHex2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCompare = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtSearchCol1 = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearchCol2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panColor
            // 
            this.panColor.Location = new System.Drawing.Point(380, 131);
            this.panColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panColor.Name = "panColor";
            this.panColor.Size = new System.Drawing.Size(126, 119);
            this.panColor.TabIndex = 31;
            // 
            // txtRGB
            // 
            this.txtRGB.Enabled = false;
            this.txtRGB.Location = new System.Drawing.Point(156, 223);
            this.txtRGB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtRGB.Name = "txtRGB";
            this.txtRGB.Size = new System.Drawing.Size(178, 31);
            this.txtRGB.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 237);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 25);
            this.label5.TabIndex = 29;
            this.label5.Text = "RGB Color:";
            // 
            // txtIntColor
            // 
            this.txtIntColor.Enabled = false;
            this.txtIntColor.Location = new System.Drawing.Point(156, 173);
            this.txtIntColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtIntColor.Name = "txtIntColor";
            this.txtIntColor.Size = new System.Drawing.Size(178, 31);
            this.txtIntColor.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "Int Color:";
            // 
            // txtHexColor
            // 
            this.txtHexColor.Location = new System.Drawing.Point(156, 123);
            this.txtHexColor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtHexColor.Name = "txtHexColor";
            this.txtHexColor.Size = new System.Drawing.Size(178, 31);
            this.txtHexColor.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 129);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "Hex Color:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 32;
            this.label3.Text = "Color1";
            // 
            // btnPick1
            // 
            this.btnPick1.Location = new System.Drawing.Point(564, 163);
            this.btnPick1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPick1.Name = "btnPick1";
            this.btnPick1.Size = new System.Drawing.Size(150, 44);
            this.btnPick1.TabIndex = 33;
            this.btnPick1.Text = "Pick";
            this.btnPick1.UseVisualStyleBackColor = true;
            this.btnPick1.Click += new System.EventHandler(this.btnPick1_Click);
            // 
            // btnPick2
            // 
            this.btnPick2.Location = new System.Drawing.Point(564, 440);
            this.btnPick2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPick2.Name = "btnPick2";
            this.btnPick2.Size = new System.Drawing.Size(150, 44);
            this.btnPick2.TabIndex = 42;
            this.btnPick2.Text = "Pick";
            this.btnPick2.UseVisualStyleBackColor = true;
            this.btnPick2.Click += new System.EventHandler(this.btnPick2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 304);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 41;
            this.label4.Text = "Color2";
            // 
            // panColor2
            // 
            this.panColor2.Location = new System.Drawing.Point(380, 408);
            this.panColor2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panColor2.Name = "panColor2";
            this.panColor2.Size = new System.Drawing.Size(126, 119);
            this.panColor2.TabIndex = 40;
            // 
            // txtRGB2
            // 
            this.txtRGB2.Enabled = false;
            this.txtRGB2.Location = new System.Drawing.Point(156, 500);
            this.txtRGB2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtRGB2.Name = "txtRGB2";
            this.txtRGB2.Size = new System.Drawing.Size(178, 31);
            this.txtRGB2.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 506);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 25);
            this.label6.TabIndex = 38;
            this.label6.Text = "RGB Color:";
            // 
            // txtInt2
            // 
            this.txtInt2.Enabled = false;
            this.txtInt2.Location = new System.Drawing.Point(156, 450);
            this.txtInt2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInt2.Name = "txtInt2";
            this.txtInt2.Size = new System.Drawing.Size(178, 31);
            this.txtInt2.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 460);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 25);
            this.label7.TabIndex = 36;
            this.label7.Text = "Int Color:";
            // 
            // txtHex2
            // 
            this.txtHex2.Location = new System.Drawing.Point(156, 400);
            this.txtHex2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtHex2.Name = "txtHex2";
            this.txtHex2.Size = new System.Drawing.Size(178, 31);
            this.txtHex2.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 406);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 25);
            this.label8.TabIndex = 34;
            this.label8.Text = "Hex Color:";
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(52, 608);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(150, 44);
            this.btnCompare.TabIndex = 43;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(248, 608);
            this.lblResult.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(142, 37);
            this.lblResult.TabIndex = 44;
            this.lblResult.Text = "Result: 0";
            // 
            // txtSearchCol1
            // 
            this.txtSearchCol1.Location = new System.Drawing.Point(156, 73);
            this.txtSearchCol1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSearchCol1.Name = "txtSearchCol1";
            this.txtSearchCol1.Size = new System.Drawing.Size(178, 31);
            this.txtSearchCol1.TabIndex = 46;
            this.txtSearchCol1.TextChanged += new System.EventHandler(this.txtSearch1Changed);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(2, 79);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(143, 25);
            this.lblSearch.TabIndex = 45;
            this.lblSearch.Text = "Search Color:";
            // 
            // txtSearchCol2
            // 
            this.txtSearchCol2.Location = new System.Drawing.Point(156, 346);
            this.txtSearchCol2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSearchCol2.Name = "txtSearchCol2";
            this.txtSearchCol2.Size = new System.Drawing.Size(178, 31);
            this.txtSearchCol2.TabIndex = 48;
            this.txtSearchCol2.TextChanged += new System.EventHandler(this.txtSearch2Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 352);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 25);
            this.label9.TabIndex = 47;
            this.label9.Text = "Search Color:";
            // 
            // FormColorCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 671);
            this.Controls.Add(this.txtSearchCol2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSearchCol1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnPick2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panColor2);
            this.Controls.Add(this.txtRGB2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInt2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtHex2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPick1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panColor);
            this.Controls.Add(this.txtRGB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIntColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHexColor);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FormColorCompare";
            this.Text = "FormColorCompare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panColor;
        private System.Windows.Forms.TextBox txtRGB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIntColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHexColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog colDiag;
        private System.Windows.Forms.Button btnPick1;
        private System.Windows.Forms.Button btnPick2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panColor2;
        private System.Windows.Forms.TextBox txtRGB2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInt2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHex2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtSearchCol1;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearchCol2;
        private System.Windows.Forms.Label label9;
    }
}