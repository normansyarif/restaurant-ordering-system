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
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;

namespace Ordering_System_For_Kitchen {
    public partial class KitchenDefault : Form {
        RegistryKey regkey;
        MySqlConnection conn;
        MySqlDataReader myReader;
        Socket socket;
        EndPoint epCashier, epKitchen;

        byte[] buffer;
        string kitchenIP;
        int kitchenPort;
        string cashierIP;
        int cashierPort;

        string tableNumber = "";

        List<TableData> tableList = new List<TableData>();
        DataTable table = new DataTable();

        public KitchenDefault() {
            InitializeComponent();

            regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RestaurantOrderingSystem");
            conn = DB.openConn();
            table.Columns.Add("Item Name", typeof(string));
            table.Columns.Add("Quantity", typeof(string));
            this.dataGridView1.DataSource = table;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            // Get all of the IP's
            kitchenIP = GetLocalIP();
            kitchenPort = Convert.ToInt32(regkey.GetValue("Kitchen Port").ToString());
            cashierIP = regkey.GetValue("Cashier Computer").ToString();
            cashierPort = Convert.ToInt32(regkey.GetValue("Cashier Port").ToString());

            //MessageBox.Show("Cash ip: " + cashierIP + ", cash port: " + cashierPort + ", kitch ip: " + kitchenIP + ", kitch port: " + kitchenPort);

            ConnectSocket();
        }

        public void GetData(string tableNo) {
            for (int i = 0; i < tableList.Count; i++) {
                if (tableList[i].TableNo == tableNo) {
                    ImplodeAndFillDataGrid(tableList[i].ItemName, tableList[i].Quantity);
                }
            }
        }

        public string ConvertIdToName(string id) {
            string name = "";
            MySqlCommand command = new MySqlCommand("select name from menu where id='" + id +"'", conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    name = myReader["name"].ToString();
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting menu list - " + ex.Message);
            }
            return name;
        }

        public void ImplodeAndSetToArrayList(string dataList) {
            string[] dataArray = dataList.Split(';');
            tableList.Add(new TableData {
                TableNo = dataArray[0],
                ItemName = dataArray[1],
                Quantity = dataArray[2]
            });
        }

        private void ImplodeAndFillDataGrid(string listOfId, string listOfQty) {
            string[] id = listOfId.Split(',');
            string[] qty = listOfQty.Split(',');
            int itemTotal = 0;
            for (int i = 0; i < id.Length; i++) {
                table.Rows.Add(ConvertIdToName(id[i]), qty[i]);
                itemTotal += Convert.ToInt32(qty[i]);
            }
            this.labelTotal.Text = itemTotal.ToString();
        }

        private void RemoveFromArray(string tableNumber) {
            for (int i = 0; i < tableList.Count; i++) {
                if (tableList[i].TableNo == tableNumber) {
                    tableList.Remove(tableList[i]);
                }
            }
        }

        public void ExitApp() {
            DialogResult dialog = MessageBox.Show("Do you really want to close the program?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                Application.Exit();
            }
        }


        /// <summary>
        /// SOCKET CONFIGURATION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
                epKitchen = new IPEndPoint(IPAddress.Parse(kitchenIP), kitchenPort);
                socket.Bind(epKitchen);

                // connecting to kitchen computer
                epCashier = new IPEndPoint(IPAddress.Parse(cashierIP), cashierPort);
                socket.Connect(epCashier);

                // listening the specific port
                buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epCashier, new AsyncCallback(MessageCallBack), buffer);
            } catch (Exception ex) {
                MessageBox.Show("Error while connecting to the cashier computer - " + ex.Message, "Something happened");
            }
        }

        private void MessageCallBack(IAsyncResult aResult) {
            try {
                byte[] receivedData = new byte[1500];
                receivedData = (byte[])aResult.AsyncState;

                // converting byte to string
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receivedOrder = aEncoding.GetString(receivedData);

                ImplodeAndSetToArrayList(receivedOrder);
                //table color 255, 128, 128
                string table = receivedOrder.Substring(0, 1);
                Button btn = this.plTableContainer.Controls.Find("button" + table, true).FirstOrDefault() as Button;
                btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
                tableStatus.Text = "Taken";

                buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epCashier, new AsyncCallback(MessageCallBack), buffer);
            } catch (Exception ex) {
                MessageBox.Show("Error while getting callback: " + ex.Message);
            }

        }

        // END OF SOCKET CONFIGURATION //



        private void menuItemAbout_Click(object sender, EventArgs e) {
            KitchenAbout fAbout = new KitchenAbout();
            fAbout.Show();
        }

        private void menuSettings_Click(object sender, EventArgs e) {
            KitchenSettings fKitchen = new KitchenSettings();
            fKitchen.Show();
        }

        private void btnTable_Click(object sender, EventArgs e) {

            if (table != null) {
                table.Clear();
                dataGridView1.DataSource = table;
            }
            this.labelTotal.Text = "-";

            Button btn = (Button)sender;
            string tableNo = btn.Text;
            labelTable.Text = btn.Text;
            GetData(tableNo);
            this.tableNumber = btn.Text;
        }

        private void menuExit_Clicked(object sender, EventArgs e) {
            this.ExitApp();
        }

        private void form_Closed(object sender, FormClosedEventArgs e) {
            this.ExitApp();
        }

        private void button10_Click(object sender, EventArgs e) {
            Button btn = this.plTableContainer.Controls.Find("button" + this.tableNumber, true).FirstOrDefault() as Button;
            btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            RemoveFromArray(this.tableNumber);
            tableStatus.Text = "Clear";
            MessageBox.Show("Table has been cleared.");
        }

    }
}


public class TableData {
    public string TableNo { get; set; }
    public string ItemName { get; set; }
    public string Quantity { get; set; }
}