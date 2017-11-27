using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RestaurantOrderingSystem {
    public partial class CashierDefault : Form {

        CultureInfo culture;
        RegistryKey regkey;
        MySqlConnection conn;
        MySqlDataReader myReader;
        Socket socket;
        EndPoint epCashier, epKitchen;

        byte[] buffer;
        string cashierIP;
        int cashierPort;
        string kitchenIP;
        int kitchenPort;

        private List<string> id = new List<string>();
        private List<int> price = new List<int>();
        private List<int> qty = new List<int>();
        private List<string> type = new List<string>();
        private List<int> discount = new List<int>();
        private List<int> subTotal = new List<int>();
        private List<int> selectedItem = new List<int>();
        private List<int> types = new List<int>();

        public CashierDefault() {
           
            InitializeComponent();
            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RestaurantOrderingSystem");
            conn = DB.openConn();
            culture = CultureInfo.CreateSpecificCulture("id-ID");

            // Create menu list
            this.GetMenuList("select * from menu;", conn);
            this.types = new List<int>() { 0, 0, 0 };

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            // Get all of the IP's
            cashierIP = GetLocalIP();
            cashierPort = Convert.ToInt32(regkey.GetValue("Cashier Port").ToString());
            kitchenIP = regkey.GetValue("Kitchen Computer").ToString();
            kitchenPort = Convert.ToInt32(regkey.GetValue("Kitchen Port").ToString());

            //MessageBox.Show("Cash ip: " + cashierIP + ", cash port: " + cashierPort + ", kitch ip: " + kitchenIP + ", kitch port: " + kitchenPort);

            ConnectSocket();
        }

        /// <summary>
        /// Get item "data" to be shown in the menu,
        /// just get the data, not create the controls
        /// </summary>
        /// <param name="query">Database query</param>
        /// <param name="conn">Database connection</param>
        public void GetMenuList(string query, MySqlConnection conn) {
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    // This one that creates the controls
                    byte[] img = (byte[])myReader["image"];
                    this.LoadMenuControls(myReader["id"].ToString(), myReader["name"].ToString(), Convert.ToInt32(myReader["price"].ToString()), img);
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting menu list - " + ex.Message);
            }
        }

        /// <summary>
        /// Get item "data" to be shown in the basket list. Just data, not controls
        /// It will return an array of item name, price, and type.
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns></returns>
        public string[] GetItemNameAndPriceAndTypeAndDiscount(string id) {
            MySqlCommand command = new MySqlCommand("select name,price,type,discount from menu where id=" + id, conn);
            string[] value = new string[4];
            string name, price, type, discount;
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    name = myReader["name"].ToString();
                    price = myReader["price"].ToString();
                    type = myReader["type"].ToString();
                    discount = myReader["discount"].ToString();
                    value = new string[4] { name, price, type, discount };
                    Console.WriteLine("Found!");
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting item name - " + ex.Message);
            }
            return value;
        }

        public void CalculateSubTotal(string id, int newQty) {
            for (int i = 0; i < this.id.Count; i++) {
                if (this.id[i].Equals(id)) {

                    // Update types array
                    switch (this.type[i]) {
                        case "food":
                            if (this.qty[i] > newQty) this.types[0] -= qty[i] - newQty;
                            else if (this.qty[i] < newQty) this.types[0] += newQty - qty[i];
                            break;
                        case "drink":
                            if (this.qty[i] > newQty) this.types[1] -= qty[i] - newQty;
                            else if (this.qty[i] < newQty) this.types[1] += newQty - qty[i];
                            break;
                        case "dessert":
                            if (this.qty[i] > newQty) this.types[2] -= qty[i] - newQty;
                            else if (this.qty[i] < newQty) this.types[2] += newQty - qty[i];
                            break;
                        default:
                            break;
                    }
                    // Update qty and subTotal array
                    this.qty[i] = newQty;
                    this.subTotal[i] = this.qty[i] * this.price[i];
                    int finalsub = this.subTotal[i] - (this.qty[i] * this.discount[i]);
                    // Update subTotal text of the item in the basket list
                    Label totalPriceOfId = this.flowLayoutPanel2.Controls.Find("totalPriceOfId" + id, true).FirstOrDefault() as Label;
                    totalPriceOfId.Text = "Rp. " + finalsub.ToString("N0", culture);
                    return;
                }
            }
        }

        public int CalculateDiscount() {
            int discount = 0;
            for (int i = 0; i < this.discount.Count; i++) {
                discount += this.qty[i] * this.discount[i];
            }
            labelDiscount.Text = "Discount Rp. "+discount.ToString("N0", culture);
            return discount;
        }

        public int CalculateGrandTotal() {
            int grandGrandTotal = 0;
            int grandTotal = 0;
            int discount = 0;
            for (int i = 0; i < this.subTotal.Count; i++) {
                grandTotal += this.subTotal[i];
                discount += this.qty[i] * this.discount[i];
            }
            grandGrandTotal = grandTotal - discount;
            labelGrandTotal.Text = "Rp. " + (grandTotal-discount).ToString("N0", culture);
            return grandGrandTotal;
        }

        /// <summary>
        /// Get the summary of the items in the basket list (id, and quantity)
        /// It will return a string that will be sent to the kitchen computer
        /// </summary>
        /// <returns></returns>
        public string GetCheckData() {
            DateTime now = DateTime.Now;
            string date = now.ToString("yyyy-MM-dd");
            string stringOfId = "";
            string stringOfQty = "";
            string tableNo = "";
            string data = "";

            for (int i = 0; i < this.id.Count; i++) {
                if (this.id[i] != "") {
                    stringOfId += this.id[i] + ",";
                    stringOfQty += this.qty[i] + ",";
                }
            }

            if (stringOfId.Length > 0) {
                if (comboTable.Text != "") {
                    stringOfId = stringOfId.Substring(0, (stringOfId.Length - 1));
                    stringOfQty = stringOfQty.Substring(0, (stringOfQty.Length - 1));
                    tableNo = comboTable.Text;
                    data = date + ";" + tableNo + ";" + stringOfId + ";" + stringOfQty;
                } else {
                    MessageBox.Show("Please specify the table number.");
                }
                
            }
            return data;
        }

        /// <summary>
        /// Change the thousand into 'k', like what we see in most cafe.
        /// </summary>
        /// <param name="value">Normal format</param>
        /// <returns></returns>
        public string ConvertToGaulCurrency(int value) {
            int newValue = value / 1000;
            string result = newValue.ToString() + "k";
            return result;
        }

        public int ConvertToDuit(int original, int discountInPercent) {
            int discount = original * discountInPercent / 100;
            return discount;
        }

        /// <summary>
        /// Select item(s) in the basket list.
        /// The selected item can be removed afterward from the basket list
        /// </summary>
        /// <param name="id"></param>
        private void selectItem(string id) {
            for (int i = 0; i < this.id.Count; i++) {
                if (this.id[i] == id) {
                    Panel pnl = this.flowLayoutPanel2.Controls.Find(id, true).FirstOrDefault() as Panel;
                    if (this.selectedItem[i] == 0) {
                        this.selectedItem[i] = 1;
                        pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
                    } else {
                        this.selectedItem[i] = 0;
                        pnl.BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
        }

        private bool DateExists(string date) {
            bool dateExists = false;
            MySqlCommand command = new MySqlCommand("select date from sales where date='" + date + "'", conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    DateTime dateDB = myReader.GetDateTime(myReader.GetOrdinal("date"));
                    if (dateDB.ToString("yyyy-MM-dd") == date) {
                        dateExists = true;
                        break;
                    }
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while checking date - " + ex.Message);
            }
            return dateExists;
        }


        /// <summary>
        /// SOCKET THINGY
        /// </summary>
        /// <returns></returns>

        private string GetLocalIP() {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        private void ConnectSocket() {
            try {
                // binding the socket
                epCashier = new IPEndPoint(IPAddress.Parse(cashierIP), cashierPort);
                socket.Bind(epCashier);

                // connecting to kitchen computer
                epKitchen = new IPEndPoint(IPAddress.Parse(kitchenIP), kitchenPort);
                socket.Connect(epKitchen);

                // listening the specific port
                buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epKitchen, new AsyncCallback(MessageCallBack), buffer);
            } catch (Exception ex) {
                MessageBox.Show("Error while connecting to the kitchen computer - " + ex.Message, "Something happened");
            }
        }

        private void MessageCallBack(IAsyncResult aResult) {
            try {
                byte[] receivedData = new byte[1500];
                receivedData = (byte[])aResult.AsyncState;

                // converting byte to string
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receivedMessage = aEncoding.GetString(receivedData);

                // adding this message into listbox
                //listMessage.Items.Add("Friend: " + receivedMessage);

                buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epKitchen, new AsyncCallback(MessageCallBack), buffer);
            } catch (Exception ex) {
                MessageBox.Show("Error while getting callback: " + ex.Message);
            }

        }

        private bool SendOrder(string data) {
            bool sendOrder = false;
            try {
                // convert string to byte
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                byte[] sendingMessage = new byte[1500];
                sendingMessage = aEncoding.GetBytes(data);

                // sending encoded orders
                socket.Send(sendingMessage);
                sendOrder = true;
                //listMessage.Items.Add("Me: " + textMessage.Text);
                //textMessage.Text = "";
            } catch (Exception ex) {
                sendOrder = false;
                MessageBox.Show("Error while sending data to the kitchen: " + ex.Message);
            }
            return sendOrder;
        }


        ////////////////////
        // EVENT HANDLERS //
        ////////////////////

        // Events in the menu container
        private void ListMenuByCategory(object sender, EventArgs e) {
            this.flowLayoutPanel1.Controls.Clear();
            Button btn = (Button)sender;
            string category = "";
            switch (btn.Name) {
                case "buttonMenuAll":
                    category = "all";
                    break;
                case "buttonMenuFood":
                    category = "food";
                    break;
                case "buttonMenuDrink":
                    category = "drink";
                    break;
                case "buttonMenuDessert":
                    category = "dessert";
                    break;
                default:
                    break;
            }

            if (category == "all") this.GetMenuList("select * from menu", conn);
            else this.GetMenuList("select * from menu where type='" + category + "'", conn);
        }

        private void textSearch_TextChanged(object sender, EventArgs e) {
            this.flowLayoutPanel1.Controls.Clear();
            this.GetMenuList("select * from menu where name like '" + textSearch.Text + "%'", conn);
        }

        public void AddToBasket(object sender, EventArgs e) {
            
            PictureBox b = (PictureBox)sender;
            string id = b.Parent.Name;
            this.CreateBasketControls(id);
            this.getItemCount();
            this.CalculateDiscount();
            this.CalculateGrandTotal();
        }

        // Events in the basket list container
        private void itemQty_TextChanged(object sender, EventArgs e) {
            TextBox tb = (TextBox)sender;
            int newQty = 0;
            if (tb.Text == "" || tb.Text == null) newQty = 0;
            else newQty = Convert.ToInt32(tb.Text);
            this.CalculateSubTotal(tb.Parent.Name, newQty);
            this.getItemCount();
            this.CalculateDiscount();
            this.CalculateGrandTotal();
        }

        private void item_Click(object sender, EventArgs e) {
            Label l = (Label)sender;
            string id = l.Parent.Name.ToString();
            this.selectItem(id);
        }

        // Events for the buttons in the basket list container
        private void btnRemove_Click(object sender, EventArgs e) {
            for (int i = 0; i < this.id.Count; i++) {
                if (this.selectedItem[i] == 1) {
                    Panel pnlItem = this.flowLayoutPanel2.Controls.Find(this.id[i], true).FirstOrDefault() as Panel;
                    this.flowLayoutPanel2.Controls.Remove(pnlItem);

                    if (this.type[i] == "food") this.types[0] -= this.qty[i];
                    else if (this.type[i] == "drink") this.types[1] -= this.qty[i];
                    else if (this.type[i] == "dessert") this.types[2] -= this.qty[i];
                    this.getItemCount();

                    this.id[i] = "";
                    this.price[i] = 0;
                    this.qty[i] = 0;
                    this.type[i] = "";
                    this.discount[i] = 0;
                    this.subTotal[i] = 0;
                    this.selectedItem[i] = 0;

                    this.CalculateDiscount();
                    this.CalculateGrandTotal();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            // Clear arrays
            this.id.Clear();
            this.price.Clear();
            this.qty.Clear();
            this.type.Clear();
            this.discount.Clear();
            this.subTotal.Clear();
            this.selectedItem.Clear();
            this.types = new List<int>() { 0, 0, 0 };

            // Clear controls
            this.flowLayoutPanel2.Controls.Clear();

            // Set back the counters to 0
            this.labelFood.Text = "-";
            this.labelTotal.Text = "-";
            this.labelDrink.Text = "-";
            this.labelDiscount.Text = "Discount Rp. -";
            this.labelDessert.Text = "-";
            this.labelGrandTotal.Text = "Rp. -";
        }


        private void AddSale(string date, int food, int drink, int dessert, int total_price) {
            string query = "insert into sales (date,food,drink,dessert,total_price) values(@date,@food,@drink,@dessert,@total_price)";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("@date", MySqlDbType.Date);
            command.Parameters.Add("@food", MySqlDbType.Int32);
            command.Parameters.Add("@drink", MySqlDbType.Int32);
            command.Parameters.Add("@dessert", MySqlDbType.Int32);
            command.Parameters.Add("@total_price", MySqlDbType.Int32);
            command.Parameters["@date"].Value = date;
            command.Parameters["@food"].Value = food;
            command.Parameters["@drink"].Value = drink;
            command.Parameters["@dessert"].Value = dessert;
            command.Parameters["@total_price"].Value = total_price;
            try {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while adding new sale - " + ex.Message);
            }
        }

        private void EditSale(string date, int food, int drink, int dessert, int total_price) {
            string selectQuery = "select * from sales where date='" + date + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, conn);
            try {
                int newFood = 0;
                int newDrink = 0;
                int newDessert = 0;
                int newTotalPrice = 0;
                conn.Open();
                myReader = command.ExecuteReader();
                bool isFound = false;
                if (myReader.Read()) {
                    isFound = true;
                    newFood = food + Convert.ToInt32(myReader["food"].ToString());
                    newDrink = drink + Convert.ToInt32(myReader["drink"].ToString());
                    newDessert = dessert + Convert.ToInt32(myReader["dessert"].ToString());
                    newTotalPrice = total_price + Convert.ToInt32(myReader["total_price"].ToString());

                }
                conn.Close();

                if (isFound) {
                    string updateQuery = "update sales set food=@food,drink=@drink,dessert=@dessert,total_price=@total_price where date=@date";
                    MySqlCommand update = new MySqlCommand(updateQuery, conn);
                    update.Parameters.Add("@date", MySqlDbType.Date);
                    update.Parameters.Add("@food", MySqlDbType.Int32);
                    update.Parameters.Add("@drink", MySqlDbType.Int32);
                    update.Parameters.Add("@dessert", MySqlDbType.Int32);
                    update.Parameters.Add("@total_price", MySqlDbType.Int32);
                    update.Parameters["@date"].Value = date;
                    update.Parameters["@food"].Value = newFood;
                    update.Parameters["@drink"].Value = newDrink;
                    update.Parameters["@dessert"].Value = newDessert;
                    update.Parameters["@total_price"].Value = newTotalPrice;
                    try {
                        conn.Open();
                        update.ExecuteNonQuery();
                        conn.Close();
                    } catch (Exception ex) {
                        MessageBox.Show("Error while updating sale - " + ex.Message);
                    }
                }

            } catch (Exception ex) {
                MessageBox.Show("Error while getting original data - " + ex.Message);
            }
        }

        private bool IsFoundDetails(string date, string id_item) {
            bool isFoundDetails = false;
            MySqlCommand command = new MySqlCommand("select * from sales_details where date='" + date + "' and id_item=" + Convert.ToInt32(id_item) + "", conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    DateTime dateDB = myReader.GetDateTime(myReader.GetOrdinal("date"));
                    if ((dateDB.ToString("yyyy-MM-dd") == date) && (Convert.ToInt32(myReader["id_item"].ToString()) == Convert.ToInt32(id_item))) {
                        isFoundDetails = true;
                        break;
                    }
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while checking details - " + ex.Message);
            }
            return isFoundDetails;
        }

        private void AddSaleDetails(string date, string id_item, int qty) {
            string query = "insert into sales_details (date, id_item, quantity) values(@date,@id_item,@quantity)";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("@date", MySqlDbType.Date);
            command.Parameters.Add("@id_item", MySqlDbType.VarChar);
            command.Parameters.Add("@quantity", MySqlDbType.Int32);
            command.Parameters["@date"].Value = date;
            command.Parameters["@id_item"].Value = id_item;
            command.Parameters["@quantity"].Value = qty;
            try {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while adding new details - " + ex.Message);
            }
        }

        private void EditSaleDetails(string date, string id_item, int qty) {
            string selectQuery = "select quantity from sales_details where date='" + date + "' and id_item=" + Convert.ToInt32(id_item) + "";
            MySqlCommand command = new MySqlCommand(selectQuery, conn);
            try {
                int newQty = 0;
                conn.Open();
                myReader = command.ExecuteReader();
                bool isFound = false;
                if (myReader.Read()) {
                    isFound = true;
                    newQty = qty + Convert.ToInt32(myReader["quantity"].ToString());

                }
                conn.Close();

                if (isFound) {
                    string updateQuery = "update sales_details set quantity=@quantity where date=@date and id_item=@id_item";
                    MySqlCommand update = new MySqlCommand(updateQuery, conn);
                    update.Parameters.Add("@date", MySqlDbType.Date);
                    update.Parameters.Add("@id_item", MySqlDbType.Int32);
                    update.Parameters.Add("@quantity", MySqlDbType.Int32);
                    update.Parameters["@date"].Value = date;
                    update.Parameters["@id_item"].Value = Convert.ToInt32(id_item);
                    update.Parameters["@quantity"].Value = newQty;
                    try {
                        conn.Open();
                        update.ExecuteNonQuery();
                        conn.Close();
                    } catch (Exception ex) {
                        MessageBox.Show("Error while updating details - " + ex.Message);
                    }
                }

            } catch (Exception ex) {
                MessageBox.Show("Error while getting original details data - " + ex.Message);
            }
        }


        private void btnSend_Click(object sender, EventArgs e) {
            string data = this.GetCheckData();
            if (data != "") {
                DialogResult dialog = MessageBox.Show("Your total is " + labelGrandTotal.Text + ", continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) {

                    // DO SOMETHING NETWORKINGY
                    string sendData = data.Substring(11);
                    if (SendOrder(sendData)) {
                        string date = data.Split(';')[0];
                        string stringOfId = data.Split(';')[2];
                        string stringOfQty = data.Split(';')[3];
                        int numOfFood = Convert.ToInt32(labelFood.Text);
                        int numOfDrink = Convert.ToInt32(labelDrink.Text);
                        int numOfDessert = Convert.ToInt32(labelDessert.Text);
                        int totalPrice = CalculateGrandTotal();
                        int totalDiscount = CalculateDiscount();
                        if (DateExists(date)) {
                            EditSale(date, numOfFood, numOfDrink, numOfDessert, totalPrice);
                        } else {
                            AddSale(date, numOfFood, numOfDrink, numOfDessert, totalPrice);
                        }
                        string[] arrayOfId = stringOfId.Split(',');
                        string[] arrayOfQty = stringOfQty.Split(',');
                        for (int i = 0; i < arrayOfId.Length; i++) {
                            if (IsFoundDetails(date, arrayOfId[i])) {
                                EditSaleDetails(date, arrayOfId[i], Convert.ToInt32(arrayOfQty[i]));
                            } else {
                                AddSaleDetails(date, arrayOfId[i], Convert.ToInt32(arrayOfQty[i]));
                            }
                        }
                        PrintCheck(data, totalDiscount, totalPrice);
                    }
                }
            } else {
                MessageBox.Show("Please choose your order first");
            }
        }

        private void PrintCheck(string data, int totalDiscount, int totalPrice) {
            CashierPrint cp = new CashierPrint(data, totalDiscount, totalPrice);
            cp.Show();
        }

        // Events for the buttons in the window control
        private void btnSettings_Click(object sender, EventArgs e) {
            CashierSettings fSetting = new CashierSettings();
            fSetting.Show();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Do you really want to close the program?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                Application.Exit();
            }
        }



        ////////////////////////
        // CONTROL MANAGEMENT //
        ////////////////////////

        /// <summary>
        /// Generate item controls in the menu,
        /// each parater used in this method is retrieved from the database.
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <param name="name">Item name</param>
        /// <param name="price">Item price</param>
        public void LoadMenuControls(string id, string name, int price, byte[] img) {
            string databaseHost = regkey.GetValue("Database Server").ToString().Split(':')[0];

            MemoryStream ms = new MemoryStream(img);

            // Create panel
            Panel menuPanel = new Panel();
            menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            menuPanel.Location = new System.Drawing.Point(5, 5);
            menuPanel.Margin = new System.Windows.Forms.Padding(5);
            menuPanel.Name = id;
            menuPanel.Size = new System.Drawing.Size(147, 148);
            this.flowLayoutPanel1.Controls.Add(menuPanel);

            // Create image 
            PictureBox menuPicture = new PictureBox();
            //menuPicture.ImageLocation = "http://" + databaseHost + "/images/" + id + ".jpg";
            menuPicture.Image = Image.FromStream(ms);
            menuPicture.Location = new System.Drawing.Point(3, 3);
            menuPicture.Size = new System.Drawing.Size(147, 108);
            menuPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            menuPicture.Cursor = System.Windows.Forms.Cursors.Hand;
            menuPicture.Click += new System.EventHandler(this.AddToBasket);
            menuPanel.Controls.Add(menuPicture);

            // Create menu name
            Label menuName1 = new Label();
            menuName1.BackColor = System.Drawing.Color.Transparent;
            menuName1.Font = new System.Drawing.Font("Century Gothic", 9.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuName1.ForeColor = System.Drawing.Color.Blue;
            menuName1.Location = new System.Drawing.Point(3, 109);
            menuName1.Size = new System.Drawing.Size(101, 45);
            menuName1.Text = name;
            menuName1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            menuName1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 0);
            menuPanel.Controls.Add(menuName1);

            // Create price tag
            Label menuPrice1 = new Label();
            menuPrice1.BackColor = System.Drawing.Color.Transparent;
            menuPrice1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuPrice1.ForeColor = System.Drawing.Color.Red;
            menuPrice1.Location = new System.Drawing.Point(110, 115);
            menuPrice1.Size = new System.Drawing.Size(34, 37);
            menuPrice1.Text = this.ConvertToGaulCurrency(price);
            menuPrice1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            menuPanel.Controls.Add(menuPrice1);
        }

        /// <summary>
        /// After an item in the menu is clicked, it will be added to the basket list.
        /// This method is to create the controls for each item in the basket list.
        /// </summary>
        /// <param name="id">Item ID</param>
        public void CreateBasketControls(string id) {
            string name = this.GetItemNameAndPriceAndTypeAndDiscount(id)[0];
            int price = Convert.ToInt32(this.GetItemNameAndPriceAndTypeAndDiscount(id)[1]);
            string type = this.GetItemNameAndPriceAndTypeAndDiscount(id)[2];
            int discountInPercent = Convert.ToInt32(this.GetItemNameAndPriceAndTypeAndDiscount(id)[3]);

            // Add new item data to arrays
            this.id.Add(id);
            this.price.Add(price);
            this.qty.Add(1);
            this.type.Add(type);
            this.discount.Add(ConvertToDuit(price, discountInPercent));
            this.selectedItem.Add(0); //0 Unselected, 1 selected
            this.subTotal.Add(price * 1);

            if (type == "food") this.types[0]++;
            else if (type == "drink") this.types[1]++;
            else if (type == "dessert") this.types[2]++;
            // Create panel
            Panel itemPanel = new Panel();
            itemPanel.Name = id;
            itemPanel.Location = new System.Drawing.Point(0, 0);
            itemPanel.Margin = new System.Windows.Forms.Padding(0);
            itemPanel.Size = new System.Drawing.Size(444, 73);
            flowLayoutPanel2.Controls.Add(itemPanel);

            // Create Divider
            Label itemDivider = new Label();
            itemDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            itemDivider.Location = new System.Drawing.Point(22, 71);
            itemDivider.Size = new System.Drawing.Size(400, 2);
            itemPanel.Controls.Add(itemDivider);

            // Create name
            Label itemName = new Label();
            itemName.Dock = System.Windows.Forms.DockStyle.Left;
            itemName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            itemName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(118)))), ((int)(((byte)(179)))));
            itemName.Location = new System.Drawing.Point(0, 0);
            itemName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            itemName.Size = new System.Drawing.Size(156, 75);
            itemName.Text = name;
            itemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            itemName.Cursor = System.Windows.Forms.Cursors.Hand;
            itemName.Click += new System.EventHandler(this.item_Click);
            itemPanel.Controls.Add(itemName);

            // Create quantity textbox
            TextBox itemQty = new TextBox();
            itemQty.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            itemQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(118)))), ((int)(((byte)(179)))));
            itemQty.Location = new System.Drawing.Point(162, 15);
            itemQty.Size = new System.Drawing.Size(44, 41);
            itemQty.Text = "1";
            itemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            itemQty.TextChanged += new System.EventHandler(this.itemQty_TextChanged);
            itemPanel.Controls.Add(itemQty);

            // Create price tag label
            Label itemPrice = new Label();
            itemPrice.AutoSize = true;
            itemPrice.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            itemPrice.Location = new System.Drawing.Point(215, 27);
            itemPrice.Size = new System.Drawing.Size(70, 16);
            if (discountInPercent == 0) {
                itemPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(118)))), ((int)(((byte)(179)))));
                itemPrice.Text = "x  Rp. " + ConvertToGaulCurrency(price);
            } else {
                itemPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                itemPrice.Text = "x  Rp. " + ConvertToGaulCurrency(price) + " (" + discountInPercent + "%)";
            }
            itemPanel.Controls.Add(itemPrice);

            // Create total price label
            Label itemTotal = new Label();
            itemTotal.Name = "totalPriceOfId" + id;
            itemTotal.Dock = System.Windows.Forms.DockStyle.Right;
            itemTotal.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (discountInPercent == 0) {
                itemTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(118)))), ((int)(((byte)(179)))));
            } else {
                itemTotal.ForeColor = System.Drawing.Color.Red;
            }
            itemTotal.Location = new System.Drawing.Point(298, 0);
            itemTotal.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            itemTotal.Size = new System.Drawing.Size(140, 75);
            itemTotal.Text = "Rp. " + (price * Convert.ToInt32(itemQty.Text)-ConvertToDuit(price, discountInPercent)).ToString("N0", culture);
            itemTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            itemPanel.Controls.Add(itemTotal);

           
        }

        /// <summary>
        /// Set the item counter based on the type of 
        /// item (food, drink, or dessert)
        /// </summary>
        public void getItemCount() {
            labelTotal.Text = (types[0] + types[1] + types[2]).ToString();
            labelFood.Text = types[0].ToString();
            labelDrink.Text = types[1].ToString();
            labelDessert.Text = types[2].ToString();
        }

    }
}

