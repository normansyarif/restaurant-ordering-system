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
using System.Net;
using System.Net.Sockets;

namespace Ordering_System_For_Kitchen {
    
    public partial class KitchenSettings : Form {
        RegistryKey regKey = null;

        public KitchenSettings() {
            InitializeComponent();
            regKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RestaurantOrderingSystem");
            LoadData();
        }

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

        private void LoadData() {
            textDatabase.Text = regKey.GetValue("Database Server").ToString();
            textDBUser.Text = regKey.GetValue("Database User").ToString();
            textDBPass.Text = regKey.GetValue("Database Password").ToString();
            textIP.Text = GetLocalIP();
            textPort.Text = regKey.GetValue("Kitchen Port").ToString();
            textCashierIP.Text = regKey.GetValue("Cashier Computer").ToString();
            textCashierPort.Text = regKey.GetValue("Cashier Port").ToString();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            if (btn.Name == "buttonSettingOK") {
                regKey.SetValue("Database Server", textDatabase.Text.ToString());
                regKey.SetValue("Database User", textDBUser.Text.ToString());
                regKey.SetValue("Database Password", textDBPass.Text.ToString());
                regKey.SetValue("Kitchen Port", textPort.Text.ToString());
                regKey.SetValue("Cashier Computer", textCashierIP.Text.ToString());
                regKey.SetValue("Cashier Port", textCashierPort.Text.ToString());

                DialogResult dialog = MessageBox.Show("Please restart the application in order the changes to take effect?", "Restart", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) {
                    Application.Restart();
                }
            }
            regKey.Close();
            this.Close();
        }
    }
}
