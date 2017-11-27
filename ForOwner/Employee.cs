using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ForOwner {
    public partial class Employee : UserControl {
        MySqlConnection conn;
        MySqlDataReader myReader;
        DataTable table = new DataTable();
        List<string> idList = new List<string>();
        string id = "";
        public Employee() {
            InitializeComponent();
            conn = DB.openConn();
            LoadTable();
            LoadItem();
        }

        public void LoadTable() {
            table.Columns.Add("User ID", typeof(string));
            table.Columns.Add("Full Name", typeof(string));
            table.Columns.Add("Gender", typeof(string));
            table.Columns.Add("Role", typeof(string));
            this.dataGridEditEmp.DataSource = table;
        }

        public void LoadItem() {
            if (table != null) {
                table.Clear();
                dataGridEditEmp.DataSource = table;
            }
            string query = "select * from user";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    table.Rows.Add(myReader["user_id"].ToString(), myReader["name"].ToString(), myReader["gender"].ToString(), myReader["role"].ToString());
                    idList.Add(myReader["user_id"].ToString());
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting user list - " + ex.Message);
            }
        }

        private void dataGridEditEmp_CellClick(object sender, DataGridViewCellEventArgs e) {
            int rowIndex = e.RowIndex;
            this.id = (dataGridEditEmp.Rows[rowIndex].Cells[0].Value).ToString();
            textEditName.Text = (dataGridEditEmp.Rows[rowIndex].Cells[1].Value).ToString();
            comboEditRole.Text = (dataGridEditEmp.Rows[rowIndex].Cells[3].Value).ToString();
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
            buttonPassword.Enabled = true;
        }

        private void buttonUpdate_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Update data?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                if (this.id != "") {
                    string query = "update user set name=@name,role=@role where user_id=@id";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@id", MySqlDbType.VarChar);
                    command.Parameters.Add("@name", MySqlDbType.VarChar);
                    command.Parameters.Add("@role", MySqlDbType.VarChar);

                    command.Parameters["@id"].Value = this.id;
                    command.Parameters["@name"].Value = textEditName.Text;
                    command.Parameters["@role"].Value = comboEditRole.Text;
                    try {
                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully updated!");
                        conn.Close();
                        textEditName.Text = "";
                        comboEditRole.Text = "";
                        buttonUpdate.Enabled = false;
                        buttonDelete.Enabled = false;
                        LoadItem();
                    } catch (Exception ex) {
                        MessageBox.Show("Error while updating data - " + ex.Message);
                    }
                }
            }   
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Save data?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                string query = "insert into user (user_id,name,gender,role,password) values(@id,@name,@gender,@role,@pass)";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add("@id", MySqlDbType.VarChar);
                command.Parameters.Add("@name", MySqlDbType.VarChar);
                command.Parameters.Add("@gender", MySqlDbType.VarChar);
                command.Parameters.Add("@role", MySqlDbType.VarChar);
                command.Parameters.Add("@pass", MySqlDbType.VarChar);

                command.Parameters["@id"].Value = textBoxID.Text;
                command.Parameters["@name"].Value = textBoxName.Text;
                command.Parameters["@gender"].Value = comboBoxGender.Text;
                command.Parameters["@role"].Value = comboBoxRole.Text;
                command.Parameters["@pass"].Value = Hash.md5(textBoxPassword.Text);
                try {
                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully added!");
                    conn.Close();
                    buttonClear_Click(null, null);
                    LoadItem();
                } catch (Exception ex) {
                    MessageBox.Show("Error while saving data - " + ex.Message);
                }
            }   
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Delete data?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                if (this.id != "") {
                    string query = "delete from user where user_id=@id";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@id", MySqlDbType.VarChar);

                    command.Parameters["@id"].Value = this.id;
                    try {
                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully deleted!");
                        conn.Close();
                        textEditName.Text = "";
                        comboEditRole.Text = "";
                        buttonUpdate.Enabled = false;
                        buttonDelete.Enabled = false;
                        LoadItem();

                    } catch (Exception ex) {
                        MessageBox.Show("Error while deleting data - " + ex.Message);
                    }
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            textBoxName.Text = "";
            textBoxID.Text = "";
            comboBoxGender.Text = "";
            comboBoxRole.Text = "";
            textBoxPassword.Text = "";
        }

        private void buttonPassword_Click(object sender, EventArgs e) {
            OwnerPassword op = new OwnerPassword(this.id);
            op.Show();
        }
    }
}
