namespace RestaurantOrderingSystem {
    partial class CashierSettings {
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
            this.textIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSettingOK = new System.Windows.Forms.Button();
            this.buttonSettingCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textKitchenPort = new System.Windows.Forms.TextBox();
            this.textKitchenIP = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox2.Location = new System.Drawing.Point(31, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 118);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database Server";
            // 
            // textDBPass
            // 
            this.textDBPass.Location = new System.Drawing.Point(125, 82);
            this.textDBPass.Name = "textDBPass";
            this.textDBPass.Size = new System.Drawing.Size(154, 20);
            this.textDBPass.TabIndex = 2;
            this.textDBPass.UseSystemPasswordChar = true;
            // 
            // textDBUser
            // 
            this.textDBUser.Location = new System.Drawing.Point(125, 55);
            this.textDBUser.Name = "textDBUser";
            this.textDBUser.Size = new System.Drawing.Size(154, 20);
            this.textDBUser.TabIndex = 1;
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
            this.textDatabase.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Host:port";
            // 
            // textIP
            // 
            this.textIP.Enabled = false;
            this.textIP.Location = new System.Drawing.Point(125, 24);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(154, 20);
            this.textIP.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "IP Address";
            // 
            // buttonSettingOK
            // 
            this.buttonSettingOK.Location = new System.Drawing.Point(256, 363);
            this.buttonSettingOK.Name = "buttonSettingOK";
            this.buttonSettingOK.Size = new System.Drawing.Size(75, 23);
            this.buttonSettingOK.TabIndex = 7;
            this.buttonSettingOK.Text = "OK";
            this.buttonSettingOK.UseVisualStyleBackColor = true;
            this.buttonSettingOK.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // buttonSettingCancel
            // 
            this.buttonSettingCancel.Location = new System.Drawing.Point(161, 363);
            this.buttonSettingCancel.Name = "buttonSettingCancel";
            this.buttonSettingCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonSettingCancel.TabIndex = 8;
            this.buttonSettingCancel.Text = "Cancel";
            this.buttonSettingCancel.UseVisualStyleBackColor = true;
            this.buttonSettingCancel.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textPort);
            this.groupBox3.Controls.Add(this.textIP);
            this.groupBox3.Location = new System.Drawing.Point(31, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 91);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Network Setting";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(125, 51);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(154, 20);
            this.textPort.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textKitchenPort);
            this.groupBox1.Controls.Add(this.textKitchenIP);
            this.groupBox1.Location = new System.Drawing.Point(31, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 91);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kitchen Computer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "IP Address";
            // 
            // textKitchenPort
            // 
            this.textKitchenPort.Location = new System.Drawing.Point(125, 51);
            this.textKitchenPort.Name = "textKitchenPort";
            this.textKitchenPort.Size = new System.Drawing.Size(154, 20);
            this.textKitchenPort.TabIndex = 6;
            // 
            // textKitchenIP
            // 
            this.textKitchenIP.Location = new System.Drawing.Point(125, 24);
            this.textKitchenIP.Name = "textKitchenIP";
            this.textKitchenIP.Size = new System.Drawing.Size(154, 20);
            this.textKitchenIP.TabIndex = 5;
            // 
            // CashierSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 413);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonSettingCancel);
            this.Controls.Add(this.buttonSettingOK);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "CashierSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.TextBox textDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSettingOK;
        private System.Windows.Forms.Button buttonSettingCancel;
        private System.Windows.Forms.TextBox textDBPass;
        private System.Windows.Forms.TextBox textDBUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textKitchenPort;
        private System.Windows.Forms.TextBox textKitchenIP;
    }
}