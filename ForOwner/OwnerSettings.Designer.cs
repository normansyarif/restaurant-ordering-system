namespace ForOwner {
    partial class OwnerSettings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textDBPass = new System.Windows.Forms.TextBox();
            this.textDBUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSettingCancel = new System.Windows.Forms.Button();
            this.buttonSettingOK = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textDBPass);
            this.groupBox2.Controls.Add(this.textDBUser);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textDatabase);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(24, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 118);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database Configuration";
            // 
            // textDBPass
            // 
            this.textDBPass.Location = new System.Drawing.Point(125, 82);
            this.textDBPass.Name = "textDBPass";
            this.textDBPass.Size = new System.Drawing.Size(154, 20);
            this.textDBPass.TabIndex = 8;
            this.textDBPass.UseSystemPasswordChar = true;
            // 
            // textDBUser
            // 
            this.textDBUser.Location = new System.Drawing.Point(125, 55);
            this.textDBUser.Name = "textDBUser";
            this.textDBUser.Size = new System.Drawing.Size(154, 20);
            this.textDBUser.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Username";
            // 
            // textDatabase
            // 
            this.textDatabase.Location = new System.Drawing.Point(125, 28);
            this.textDatabase.Name = "textDatabase";
            this.textDatabase.Size = new System.Drawing.Size(154, 20);
            this.textDatabase.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database Host";
            // 
            // buttonSettingCancel
            // 
            this.buttonSettingCancel.Location = new System.Drawing.Point(169, 173);
            this.buttonSettingCancel.Name = "buttonSettingCancel";
            this.buttonSettingCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonSettingCancel.TabIndex = 10;
            this.buttonSettingCancel.Text = "Cancel";
            this.buttonSettingCancel.UseVisualStyleBackColor = true;
            this.buttonSettingCancel.Click += new System.EventHandler(this.buttonSettingOK_Click);
            // 
            // buttonSettingOK
            // 
            this.buttonSettingOK.Location = new System.Drawing.Point(264, 173);
            this.buttonSettingOK.Name = "buttonSettingOK";
            this.buttonSettingOK.Size = new System.Drawing.Size(75, 23);
            this.buttonSettingOK.TabIndex = 9;
            this.buttonSettingOK.Text = "OK";
            this.buttonSettingOK.UseVisualStyleBackColor = true;
            this.buttonSettingOK.Click += new System.EventHandler(this.buttonSettingOK_Click);
            // 
            // OwnerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 223);
            this.Controls.Add(this.buttonSettingCancel);
            this.Controls.Add(this.buttonSettingOK);
            this.Controls.Add(this.groupBox2);
            this.Name = "OwnerSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textDBPass;
        private System.Windows.Forms.TextBox textDBUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSettingCancel;
        private System.Windows.Forms.Button buttonSettingOK;
    }
}