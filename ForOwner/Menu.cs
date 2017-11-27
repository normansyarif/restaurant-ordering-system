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
using System.IO;

namespace ForOwner {
    public partial class Menu : UserControl {
        MySqlConnection conn;
        MySqlDataReader myReader;
        DataTable table = new DataTable();
        private string fileName = "";
        private int id = 0;
        public Menu() {
            InitializeComponent();
            conn = DB.openConn();
            LoadTable();
            LoadItem();
        }

        public void LoadTable() {
            table.Columns.Add("Item ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price (Rp.)", typeof(int));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Discount (%)", typeof(int));
            this.dataGridEditMenu.DataSource = table;
        }

        public void LoadItem() {
            if (table != null) {
                table.Clear();
                dataGridEditMenu.DataSource = table;
            }
            string query = "select id,name,price,type,discount from menu";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    table.Rows.Add(Convert.ToInt32(myReader["id"].ToString()), myReader["name"].ToString(), Convert.ToInt32(myReader["price"].ToString()), myReader["type"].ToString(), Convert.ToInt32(myReader["discount"].ToString()));
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting menu list - " + ex.Message);
            }
        }

        private void buttonBrowseImage_Click(object sender, EventArgs e) {
            try {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image files | *.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                    fileName = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            fileName = "";
            textBoxName.Text = "";
            textBoxPrice.Text = "";
            comboBoxType.Text = "";
            textBoxDiscount.Text = "";
            pictureBox1.Image = null;
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Save data?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {

                FileStream fs;
                BinaryReader br;

                byte[] imageBinary;
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                imageBinary = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();

                string query = "insert into menu (name,price,type,image,discount) values(@name,@price,@type,@image,@discount)";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add("@name", MySqlDbType.VarChar);
                command.Parameters.Add("@price", MySqlDbType.Int32);
                command.Parameters.Add("@type", MySqlDbType.VarChar);
                command.Parameters.Add("@image", MySqlDbType.MediumBlob);
                command.Parameters.Add("@discount", MySqlDbType.Int32);

                command.Parameters["@name"].Value = textBoxName.Text;
                command.Parameters["@price"].Value = textBoxPrice.Text;
                command.Parameters["@type"].Value = comboBoxType.Text;
                command.Parameters["@image"].Value = imageBinary;
                command.Parameters["@discount"].Value = textBoxDiscount.Text;
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

        private void buttonUpdate_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Update data?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                if (this.id != 0) {
                    string query = "update menu set name=@name,price=@price,type=@type,discount=@discount where id=@id";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@id", MySqlDbType.Int32);
                    command.Parameters.Add("@name", MySqlDbType.VarChar);
                    command.Parameters.Add("@price", MySqlDbType.Int32);
                    command.Parameters.Add("@type", MySqlDbType.VarChar);
                    command.Parameters.Add("@discount", MySqlDbType.Int32);

                    command.Parameters["@id"].Value = this.id;
                    command.Parameters["@name"].Value = textEditName.Text;
                    command.Parameters["@price"].Value = textEditPrice.Text;
                    command.Parameters["@type"].Value = comboEditType.Text;
                    command.Parameters["@discount"].Value = textEditDiscount.Text;
                    try {
                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully updated!");
                        conn.Close();
                        textEditName.Text = "";
                        textEditPrice.Text = "";
                        comboEditType.Text = "";
                        textEditDiscount.Text = "";
                        buttonUpdate.Enabled = false;
                        buttonDelete.Enabled = false;
                        LoadItem();
                    } catch (Exception ex) {
                        MessageBox.Show("Error while updating data - " + ex.Message);
                    }
                }
            }   
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Delete data?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                if (this.id != 0) {
                    string query = "delete from menu where id=@id";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@id", MySqlDbType.Int32);

                    command.Parameters["@id"].Value = this.id;
                    try {
                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully deleted!");
                        conn.Close();
                        textEditName.Text = "";
                        textEditPrice.Text = "";
                        comboEditType.Text = "";
                        textEditDiscount.Text = "";
                        buttonUpdate.Enabled = false;
                        buttonDelete.Enabled = false;
                        LoadItem();

                    } catch (Exception ex) {
                        MessageBox.Show("Error while deleting item - " + ex.Message);
                    }
                }
            }   
        }

        private void dataGridEditMenu_CellClick(object sender, DataGridViewCellEventArgs e) {
            int rowIndex = e.RowIndex;
            this.id = Convert.ToInt32((dataGridEditMenu.Rows[rowIndex].Cells[0].Value).ToString());
            textEditName.Text = (dataGridEditMenu.Rows[rowIndex].Cells[1].Value).ToString();
            textEditPrice.Text = (dataGridEditMenu.Rows[rowIndex].Cells[2].Value).ToString();
            comboEditType.Text = (dataGridEditMenu.Rows[rowIndex].Cells[3].Value).ToString();
            textEditDiscount.Text = (dataGridEditMenu.Rows[rowIndex].Cells[4].Value).ToString();
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
        }
    }
}
