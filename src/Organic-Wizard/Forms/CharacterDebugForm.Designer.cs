namespace Organic_Wizard
{
    partial class CharacterDebugForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterDebugForm));
            this.lblHpPercent = new System.Windows.Forms.Label();
            this.lblMpPercent = new System.Windows.Forms.Label();
            this.lblSelectedPartyMemberHp = new System.Windows.Forms.Label();
            this.lblIsInParty = new System.Windows.Forms.Label();
            this.lblIsPartyMemberSelected = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTestTesseract = new System.Windows.Forms.Button();
            this.lblPartySize = new System.Windows.Forms.Label();
            this.lblCurrentWindow = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblKingDisplay = new System.Windows.Forms.Label();
            this.lblIsMonterSelected = new System.Windows.Forms.Label();
            this.lblSelectedMonsterHp = new System.Windows.Forms.Label();
            this.lblSelectedPartyMemberIndex = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblInitialPosition = new System.Windows.Forms.Label();
            this.btnSkillBar = new System.Windows.Forms.Button();
            this.lblDirection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHpPercent
            // 
            this.lblHpPercent.AutoSize = true;
            this.lblHpPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHpPercent.ForeColor = System.Drawing.Color.White;
            this.lblHpPercent.Location = new System.Drawing.Point(27, 26);
            this.lblHpPercent.Name = "lblHpPercent";
            this.lblHpPercent.Size = new System.Drawing.Size(51, 20);
            this.lblHpPercent.TabIndex = 0;
            this.lblHpPercent.Text = "label1";
            // 
            // lblMpPercent
            // 
            this.lblMpPercent.AutoSize = true;
            this.lblMpPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMpPercent.ForeColor = System.Drawing.Color.White;
            this.lblMpPercent.Location = new System.Drawing.Point(27, 57);
            this.lblMpPercent.Name = "lblMpPercent";
            this.lblMpPercent.Size = new System.Drawing.Size(51, 20);
            this.lblMpPercent.TabIndex = 1;
            this.lblMpPercent.Text = "label1";
            // 
            // lblSelectedPartyMemberHp
            // 
            this.lblSelectedPartyMemberHp.AutoSize = true;
            this.lblSelectedPartyMemberHp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedPartyMemberHp.ForeColor = System.Drawing.Color.White;
            this.lblSelectedPartyMemberHp.Location = new System.Drawing.Point(27, 86);
            this.lblSelectedPartyMemberHp.Name = "lblSelectedPartyMemberHp";
            this.lblSelectedPartyMemberHp.Size = new System.Drawing.Size(51, 20);
            this.lblSelectedPartyMemberHp.TabIndex = 3;
            this.lblSelectedPartyMemberHp.Text = "label1";
            // 
            // lblIsInParty
            // 
            this.lblIsInParty.AutoSize = true;
            this.lblIsInParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsInParty.ForeColor = System.Drawing.Color.White;
            this.lblIsInParty.Location = new System.Drawing.Point(280, 57);
            this.lblIsInParty.Name = "lblIsInParty";
            this.lblIsInParty.Size = new System.Drawing.Size(51, 20);
            this.lblIsInParty.TabIndex = 4;
            this.lblIsInParty.Text = "label1";
            // 
            // lblIsPartyMemberSelected
            // 
            this.lblIsPartyMemberSelected.AutoSize = true;
            this.lblIsPartyMemberSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsPartyMemberSelected.ForeColor = System.Drawing.Color.White;
            this.lblIsPartyMemberSelected.Location = new System.Drawing.Point(280, 86);
            this.lblIsPartyMemberSelected.Name = "lblIsPartyMemberSelected";
            this.lblIsPartyMemberSelected.Size = new System.Drawing.Size(51, 20);
            this.lblIsPartyMemberSelected.TabIndex = 5;
            this.lblIsPartyMemberSelected.Text = "label1";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.ForeColor = System.Drawing.Color.White;
            this.lblPosition.Location = new System.Drawing.Point(27, 117);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(51, 20);
            this.lblPosition.TabIndex = 6;
            this.lblPosition.Text = "label1";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Black;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(539, 328);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTestTesseract
            // 
            this.btnTestTesseract.BackColor = System.Drawing.Color.Black;
            this.btnTestTesseract.ForeColor = System.Drawing.Color.White;
            this.btnTestTesseract.Location = new System.Drawing.Point(507, 294);
            this.btnTestTesseract.Name = "btnTestTesseract";
            this.btnTestTesseract.Size = new System.Drawing.Size(108, 23);
            this.btnTestTesseract.TabIndex = 8;
            this.btnTestTesseract.Text = "Test Tesseract Engine";
            this.btnTestTesseract.UseVisualStyleBackColor = false;
            this.btnTestTesseract.Click += new System.EventHandler(this.btnTestTesseract_Click);
            // 
            // lblPartySize
            // 
            this.lblPartySize.AutoSize = true;
            this.lblPartySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartySize.ForeColor = System.Drawing.Color.White;
            this.lblPartySize.Location = new System.Drawing.Point(280, 117);
            this.lblPartySize.Name = "lblPartySize";
            this.lblPartySize.Size = new System.Drawing.Size(51, 20);
            this.lblPartySize.TabIndex = 9;
            this.lblPartySize.Text = "label1";
            // 
            // lblCurrentWindow
            // 
            this.lblCurrentWindow.AutoSize = true;
            this.lblCurrentWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentWindow.ForeColor = System.Drawing.Color.White;
            this.lblCurrentWindow.Location = new System.Drawing.Point(280, 146);
            this.lblCurrentWindow.Name = "lblCurrentWindow";
            this.lblCurrentWindow.Size = new System.Drawing.Size(51, 20);
            this.lblCurrentWindow.TabIndex = 10;
            this.lblCurrentWindow.Text = "label1";
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Location = new System.Drawing.Point(1, 258);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(330, 90);
            this.txtConsole.TabIndex = 12;
            this.txtConsole.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Black;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(362, 328);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblKingDisplay
            // 
            this.lblKingDisplay.AutoSize = true;
            this.lblKingDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKingDisplay.ForeColor = System.Drawing.Color.White;
            this.lblKingDisplay.Location = new System.Drawing.Point(27, 177);
            this.lblKingDisplay.Name = "lblKingDisplay";
            this.lblKingDisplay.Size = new System.Drawing.Size(51, 20);
            this.lblKingDisplay.TabIndex = 14;
            this.lblKingDisplay.Text = "label1";
            // 
            // lblIsMonterSelected
            // 
            this.lblIsMonterSelected.AutoSize = true;
            this.lblIsMonterSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsMonterSelected.ForeColor = System.Drawing.Color.White;
            this.lblIsMonterSelected.Location = new System.Drawing.Point(27, 208);
            this.lblIsMonterSelected.Name = "lblIsMonterSelected";
            this.lblIsMonterSelected.Size = new System.Drawing.Size(51, 20);
            this.lblIsMonterSelected.TabIndex = 15;
            this.lblIsMonterSelected.Text = "label1";
            // 
            // lblSelectedMonsterHp
            // 
            this.lblSelectedMonsterHp.AutoSize = true;
            this.lblSelectedMonsterHp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedMonsterHp.ForeColor = System.Drawing.Color.White;
            this.lblSelectedMonsterHp.Location = new System.Drawing.Point(280, 177);
            this.lblSelectedMonsterHp.Name = "lblSelectedMonsterHp";
            this.lblSelectedMonsterHp.Size = new System.Drawing.Size(51, 20);
            this.lblSelectedMonsterHp.TabIndex = 16;
            this.lblSelectedMonsterHp.Text = "label1";
            // 
            // lblSelectedPartyMemberIndex
            // 
            this.lblSelectedPartyMemberIndex.AutoSize = true;
            this.lblSelectedPartyMemberIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedPartyMemberIndex.ForeColor = System.Drawing.Color.White;
            this.lblSelectedPartyMemberIndex.Location = new System.Drawing.Point(280, 209);
            this.lblSelectedPartyMemberIndex.Name = "lblSelectedPartyMemberIndex";
            this.lblSelectedPartyMemberIndex.Size = new System.Drawing.Size(51, 20);
            this.lblSelectedPartyMemberIndex.TabIndex = 17;
            this.lblSelectedPartyMemberIndex.Text = "label1";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.White;
            this.lblState.Location = new System.Drawing.Point(280, 26);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(51, 20);
            this.lblState.TabIndex = 18;
            this.lblState.Text = "label1";
            // 
            // lblInitialPosition
            // 
            this.lblInitialPosition.AutoSize = true;
            this.lblInitialPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitialPosition.ForeColor = System.Drawing.Color.White;
            this.lblInitialPosition.Location = new System.Drawing.Point(27, 146);
            this.lblInitialPosition.Name = "lblInitialPosition";
            this.lblInitialPosition.Size = new System.Drawing.Size(51, 20);
            this.lblInitialPosition.TabIndex = 19;
            this.lblInitialPosition.Text = "label1";
            // 
            // btnSkillBar
            // 
            this.btnSkillBar.BackColor = System.Drawing.Color.Black;
            this.btnSkillBar.ForeColor = System.Drawing.Color.White;
            this.btnSkillBar.Location = new System.Drawing.Point(539, 256);
            this.btnSkillBar.Name = "btnSkillBar";
            this.btnSkillBar.Size = new System.Drawing.Size(75, 23);
            this.btnSkillBar.TabIndex = 20;
            this.btnSkillBar.Text = "Skill Bar";
            this.btnSkillBar.UseVisualStyleBackColor = false;
            this.btnSkillBar.Click += new System.EventHandler(this.btnSkillBar_Click);
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirection.ForeColor = System.Drawing.Color.White;
            this.lblDirection.Location = new System.Drawing.Point(27, 235);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(51, 20);
            this.lblDirection.TabIndex = 21;
            this.lblDirection.Text = "label1";
            // 
            // CharacterDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(626, 363);
            this.Controls.Add(this.lblDirection);
            this.Controls.Add(this.btnSkillBar);
            this.Controls.Add(this.lblInitialPosition);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblSelectedPartyMemberIndex);
            this.Controls.Add(this.lblSelectedMonsterHp);
            this.Controls.Add(this.lblIsMonterSelected);
            this.Controls.Add(this.lblKingDisplay);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.lblCurrentWindow);
            this.Controls.Add(this.lblPartySize);
            this.Controls.Add(this.btnTestTesseract);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblIsPartyMemberSelected);
            this.Controls.Add(this.lblIsInParty);
            this.Controls.Add(this.lblSelectedPartyMemberHp);
            this.Controls.Add(this.lblMpPercent);
            this.Controls.Add(this.lblHpPercent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CharacterDebugForm";
            this.Text = "Character Info";
            this.Load += new System.EventHandler(this.CharacterInfoDebugForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHpPercent;
        private System.Windows.Forms.Label lblMpPercent;
        private System.Windows.Forms.Label lblSelectedPartyMemberHp;
        private System.Windows.Forms.Label lblIsInParty;
        private System.Windows.Forms.Label lblIsPartyMemberSelected;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTestTesseract;
        private System.Windows.Forms.Label lblPartySize;
        private System.Windows.Forms.Label lblCurrentWindow;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblKingDisplay;
        private System.Windows.Forms.Label lblIsMonterSelected;
        private System.Windows.Forms.Label lblSelectedMonsterHp;
        private System.Windows.Forms.Label lblSelectedPartyMemberIndex;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblInitialPosition;
        private System.Windows.Forms.Button btnSkillBar;
        private System.Windows.Forms.Label lblDirection;
    }
}