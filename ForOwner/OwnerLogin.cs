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
    public partial class OwnerLogin : Form {

        MySqlConnection conn;
        MySqlDataReader myReader;

        public OwnerLogin() {
            InitializeComponent();
            conn = DB.openConn();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            string query = "select name from user where role='Owner' and user_id='" + textBox1.Text + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                if (myReader.Read()) {
                    label4.Text = myReader["name"].ToString();
                    button1.Enabled = true;
                } else {
                    label4.Text = "";
                    button1.Enabled = false;
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting data - " + ex.Message);
            }
        }
        

        private void label5_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void login_Click(object sender, EventArgs e) {
            string query = "select name,password from user where role='Owner' and user_id='" + textBox1.Text + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                if (myReader.Read()) {
                    
                    string pass = myReader["password"].ToString();
                    if (pass == Hash.md5(textBox2.Text)) {
                        OwnerDefault od = new OwnerDefault();
                        od.Show();
                        this.Hide();
                    } else {
                        MessageBox.Show("Wrong password!");
                    }
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting data - " + ex.Message);
            }
        }

        private void db_Click(object sender, EventArgs e) {
            OwnerSettings os = new OwnerSettings();
            os.Show();
        }
    }
}
