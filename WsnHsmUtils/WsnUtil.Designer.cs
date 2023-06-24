namespace WsnHsmUtils
{
    partial class WsnUtil
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
            this.components = new System.ComponentModel.Container();
            this.btnAutoGroup = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txbxGroups = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbxRand = new System.Windows.Forms.TextBox();
            this.btnReDraw = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txbxZones = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnNodes = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAutoGroup
            // 
            this.btnAutoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAutoGroup.Location = new System.Drawing.Point(328, 495);
            this.btnAutoGroup.Name = "btnAutoGroup";
            this.btnAutoGroup.Size = new System.Drawing.Size(70, 23);
            this.btnAutoGroup.TabIndex = 197;
            this.btnAutoGroup.Text = "Auto Group";
            this.btnAutoGroup.UseVisualStyleBackColor = true;
            this.btnAutoGroup.Click += new System.EventHandler(this.btnAutoGroup_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(146, 494);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 193;
            this.label7.Text = "Groups";
            // 
            // txbxGroups
            // 
            this.txbxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txbxGroups.Location = new System.Drawing.Point(148, 511);
            this.txbxGroups.Name = "txbxGroups";
            this.txbxGroups.Size = new System.Drawing.Size(35, 20);
            this.txbxGroups.TabIndex = 192;
            this.txbxGroups.Text = "2";
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(363, 520);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(10, 13);
            this.lblState.TabIndex = 191;
            this.lblState.Text = "-";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 494);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 188;
            this.label4.Text = "Rand(0-10)";
            // 
            // txbxRand
            // 
            this.txbxRand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txbxRand.Location = new System.Drawing.Point(92, 511);
            this.txbxRand.Name = "txbxRand";
            this.txbxRand.Size = new System.Drawing.Size(50, 20);
            this.txbxRand.TabIndex = 187;
            this.txbxRand.Text = "5";
            // 
            // btnReDraw
            // 
            this.btnReDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReDraw.Location = new System.Drawing.Point(252, 515);
            this.btnReDraw.Name = "btnReDraw";
            this.btnReDraw.Size = new System.Drawing.Size(60, 23);
            this.btnReDraw.TabIndex = 186;
            this.btnReDraw.Text = "Re-Draw";
            this.btnReDraw.UseVisualStyleBackColor = true;
            this.btnReDraw.Click += new System.EventHandler(this.btnReDraw_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 494);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 184;
            this.label5.Text = "Zones";
            // 
            // txbxZones
            // 
            this.txbxZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txbxZones.Location = new System.Drawing.Point(54, 511);
            this.txbxZones.Name = "txbxZones";
            this.txbxZones.Size = new System.Drawing.Size(35, 20);
            this.txbxZones.TabIndex = 183;
            this.txbxZones.Text = "6,4";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(205, 491);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(45, 23);
            this.btnClear.TabIndex = 182;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 494);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 181;
            this.label1.Text = "Nodes";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(15, 511);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(35, 20);
            this.textBox1.TabIndex = 180;
            this.textBox1.Text = "33";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(14, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1010, 487);
            this.pictureBox1.TabIndex = 178;
            this.pictureBox1.TabStop = false;
            // 
            // btnNodes
            // 
            this.btnNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNodes.Location = new System.Drawing.Point(205, 515);
            this.btnNodes.Name = "btnNodes";
            this.btnNodes.Size = new System.Drawing.Size(45, 23);
            this.btnNodes.TabIndex = 179;
            this.btnNodes.Text = "Nodes";
            this.btnNodes.UseVisualStyleBackColor = true;
            this.btnNodes.Click += new System.EventHandler(this.btnNodes_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(964, 526);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 23);
            this.btnExit.TabIndex = 177;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // WsnUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 551);
            this.Controls.Add(this.btnAutoGroup);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txbxGroups);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbxRand);
            this.Controls.Add(this.btnReDraw);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txbxZones);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnNodes);
            this.Controls.Add(this.btnExit);
            this.Name = "WsnUtil";
            this.Text = "WSN Utilities - HSM";
            this.Shown += new System.EventHandler(this.WsnUtil_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAutoGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbxGroups;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbxRand;
        private System.Windows.Forms.Button btnReDraw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbxZones;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnNodes;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnExit;
    }
}

