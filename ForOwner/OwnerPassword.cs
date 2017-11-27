using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ForOwner {
    public partial class OwnerPassword : Form {
        MySqlConnection conn;
        string id;
        public OwnerPassword(string id) {
            InitializeComponent();
            this.id = id;
            conn = DB.openConn();
        }

        private bool Match(string pass1, string pass2) {
            if (pass1 == pass2) return true;
            else return false;
        }

        private void button_Click(object sender, EventArgs e) {
            Button b = (Button)sender;
            if (b.Name == "buttonOk") {
                if ((textPass1.Text != "") && (textPass2.Text != "")) {
                    if (Match(textPass1.Text, textPass2.Text)) {
                        InsertPassword(this.id, Hash.md5(textPass1.Text));
                        this.Close();
                    } else {
                        MessageBox.Show("Password does not match");
                    }
                } else {
                    MessageBox.Show("Please enter the password first");
                }
            } else {
                this.Close();
            }
        }

        private void InsertPassword(string id, string md5Pass) {
            string query = "update user set password=@password where user_id=@id";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("@id", MySqlDbType.VarChar);
            command.Parameters.Add("@password", MySqlDbType.VarChar);

            command.Parameters["@id"].Value = this.id;
            command.Parameters["@password"].Value = md5Pass;
            try {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully changed!");
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while changing password - " + ex.Message);
            }
        }


    }
}
