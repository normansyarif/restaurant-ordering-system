using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace RestaurantOrderingSystem {
    public partial class CashierPrint : Form {

        CultureInfo culture;
        Bitmap bmp;
        MySqlConnection conn;
        MySqlDataReader myReader;

        public CashierPrint(string data, int totalDiscount, int totalPrice) {
            InitializeComponent();
            conn = DB.openConn();
            culture = CultureInfo.CreateSpecificCulture("id-ID");
            LoadItem(data, totalDiscount, totalPrice);
            labelDiscount.Text = "Discount: Rp. " + totalDiscount.ToString("N0", culture);
            labelTotal.Text = "TOTAL: RP. " + totalPrice.ToString("N0", culture);
        }

        private void LoadItem(string data, int totalDiscount, int totalPrice) {
            string date = data.Split(';')[0];
            string tableNumber = data.Split(';')[1];
            string items = data.Split(';')[2];
            string quantities = data.Split(';')[3];

            DateTime dt = Convert.ToDateTime(date);
            string dateDate = dt.ToString("dddd, dd-MM-yyyy");
            string[] itemArray = items.Split(',');
            string[] qtyArray = quantities.Split(',');

            lableTable.Text = "TABLE " + tableNumber;
            labelDate.Text = dateDate;

            for (int i = 0; i < itemArray.Length; i++) {
                Label item = new Label();
                item.AutoSize = true;
                item.Font = new System.Drawing.Font("Fake Receipt", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                item.Location = new System.Drawing.Point(3, 0);
                item.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
                item.Size = new System.Drawing.Size(144, 16);
                item.TabIndex = 0;
                item.Text = qtyArray[i] + " " + GetNameAndPrice(itemArray[i])[0].ToUpper() + ", RP. " + (Convert.ToInt32(qtyArray[i]) * Convert.ToInt32(GetNameAndPrice(itemArray[i])[1])).ToString("N0", culture);
                this.flowLayoutPanel1.Controls.Add(item);
            }
        }


        private string[] GetNameAndPrice(string id) {
            string[] nameAndPrice = new string[2];
            MySqlCommand command = new MySqlCommand("select name,price,discount from menu where id=" + Convert.ToInt32(id) + "", conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                if (myReader.Read()) {
                    nameAndPrice[0] = myReader["name"].ToString();
                    int priceWithDiscount = Convert.ToInt32(myReader["price"].ToString()) - (Convert.ToInt32(myReader["price"].ToString()) * Convert.ToInt32(myReader["discount"].ToString()) / 100);
                    nameAndPrice[1] = priceWithDiscount.ToString();
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting name and price - " + ex.Message);
            }
            return nameAndPrice;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void printDoc() {
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics img = Graphics.FromImage(bmp);
            img.CopyFromScreen((this.Location.X + 10), (this.Location.Y + 25), 0, 0, new System.Drawing.Size(300, 440));
            printPreviewDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) {
            printDoc();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();

        }

    }
}
