namespace ForOwner {
    partial class Employee {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tabEmployee = new System.Windows.Forms.TabControl();
            this.tabAdd = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxRole = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabEdit = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonPassword = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.comboEditRole = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textEditName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridEditEmp = new System.Windows.Forms.DataGridView();
            this.tabEmployee.SuspendLayout();
            this.tabAdd.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabEdit.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEditEmp)).BeginInit();
            this.SuspendLayout();
            // 
            // tabEmployee
            // 
            this.tabEmployee.Controls.Add(this.tabAdd);
            this.tabEmployee.Controls.Add(this.tabEdit);
            this.tabEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEmployee.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabEmployee.Location = new System.Drawing.Point(0, 0);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.SelectedIndex = 0;
            this.tabEmployee.Size = new System.Drawing.Size(917, 464);
            this.tabEmployee.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabEmployee.TabIndex = 2;
            // 
            // tabAdd
            // 
            this.tabAdd.Controls.Add(this.panel1);
            this.tabAdd.Location = new System.Drawing.Point(4, 26);
            this.tabAdd.Name = "tabAdd";
            this.tabAdd.Size = new System.Drawing.Size(909, 434);
            this.tabAdd.TabIndex = 0;
            this.tabAdd.Text = "Add new";
            this.tabAdd.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxRole);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.comboBoxGender);
            this.panel1.Controls.Add(this.textBoxPassword);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15);
            this.panel1.Size = new System.Drawing.Size(909, 434);
            this.panel1.TabIndex = 0;
            // 
            // comboBoxRole
            // 
            this.comboBoxRole.FormattingEnabled = true;
            this.comboBoxRole.Items.AddRange(new object[] {
            "Owner",
            "Cashier",
            "Chef",
            "Waiter"});
            this.comboBoxRole.Location = new System.Drawing.Point(384, 206);
            this.comboBoxRole.Name = "comboBoxRole";
            this.comboBoxRole.Size = new System.Drawing.Size(257, 25);
            this.comboBoxRole.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Role";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(399, 326);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(496, 326);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.comboBoxGender.Location = new System.Drawing.Point(384, 164);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(257, 25);
            this.comboBoxGender.TabIndex = 8;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(384, 248);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(257, 23);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(277, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Gender";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(384, 124);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(257, 23);
            this.textBoxName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Full Name";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(384, 84);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(257, 23);
            this.textBoxID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID";
            // 
            // tabEdit
            // 
            this.tabEdit.Controls.Add(this.panel3);
            this.tabEdit.Controls.Add(this.panel2);
            this.tabEdit.Location = new System.Drawing.Point(4, 26);
            this.tabEdit.Name = "tabEdit";
            this.tabEdit.Size = new System.Drawing.Size(909, 434);
            this.tabEdit.TabIndex = 1;
            this.tabEdit.Text = "Edit data";
            this.tabEdit.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonPassword);
            this.panel3.Controls.Add(this.buttonDelete);
            this.panel3.Controls.Add(this.buttonUpdate);
            this.panel3.Controls.Add(this.comboEditRole);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.textEditName);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(502, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(407, 434);
            this.panel3.TabIndex = 2;
            // 
            // buttonPassword
            // 
            this.buttonPassword.Enabled = false;
            this.buttonPassword.Location = new System.Drawing.Point(35, 200);
            this.buttonPassword.Name = "buttonPassword";
            this.buttonPassword.Size = new System.Drawing.Size(151, 23);
            this.buttonPassword.TabIndex = 18;
            this.buttonPassword.Text = "Change password";
            this.buttonPassword.UseVisualStyleBackColor = true;
            this.buttonPassword.Click += new System.EventHandler(this.buttonPassword_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(203, 200);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 18;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Enabled = false;
            this.buttonUpdate.Location = new System.Drawing.Point(294, 200);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 17;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // comboEditRole
            // 
            this.comboEditRole.FormattingEnabled = true;
            this.comboEditRole.Items.AddRange(new object[] {
            "Owner",
            "Cashier",
            "Chef",
            "Waiter"});
            this.comboEditRole.Location = new System.Drawing.Point(140, 148);
            this.comboEditRole.Name = "comboEditRole";
            this.comboEditRole.Size = new System.Drawing.Size(229, 25);
            this.comboEditRole.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Role";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(140, 103);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(229, 23);
            this.textEditName.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Full Name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridEditEmp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.Size = new System.Drawing.Size(502, 434);
            this.panel2.TabIndex = 1;
            // 
            // dataGridEditEmp
            // 
            this.dataGridEditEmp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridEditEmp.BackgroundColor = System.Drawing.Color.White;
            this.dataGridEditEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEditEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridEditEmp.Location = new System.Drawing.Point(15, 15);
            this.dataGridEditEmp.Name = "dataGridEditEmp";
            this.dataGridEditEmp.Size = new System.Drawing.Size(472, 404);
            this.dataGridEditEmp.TabIndex = 0;
            this.dataGridEditEmp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridEditEmp_CellClick);
            // 
            // Employee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabEmployee);
            this.Name = "Employee";
            this.Size = new System.Drawing.Size(917, 464);
            this.tabEmployee.ResumeLayout(false);
            this.tabAdd.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabEdit.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEditEmp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabEmployee;
        private System.Windows.Forms.TabPage tabAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabEdit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.ComboBox comboEditRole;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textEditName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridEditEmp;
        private System.Windows.Forms.ComboBox comboBoxRole;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonPassword;
    }
}
