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

namespace Ordering_System_For_Kitchen {
    public partial class KitchenLogin : Form {

        MySqlConnection conn;
        MySqlDataReader myReader;

        public KitchenLogin() {
            InitializeComponent();
            conn = DB.openConn();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            string query = "select name from user where role='Chef' and user_id='" + textBox1.Text + "'";
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

        public static string md5(string input) {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++) {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        private void login_Click(object sender, EventArgs e) {
            string query = "select name,password from user where role='Chef' and user_id='" + textBox1.Text + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                if (myReader.Read()) {

                    string pass = myReader["password"].ToString();
                    if (pass == md5(textBox2.Text)) {
                        KitchenDefault kd = new KitchenDefault();
                        kd.Show();
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

        private void label5_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void DB_Click(object sender, EventArgs e) {
            KitchenSettings ks = new KitchenSettings();
            ks.Show();
        }
    }
}
