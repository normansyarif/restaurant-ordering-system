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

namespace RestaurantOrderingSystem {
    public partial class CashierSettings : Form {

        RegistryKey regKey = null;

        public CashierSettings() {
            InitializeComponent();
            regKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RestaurantOrderingSystem");
            LoadData();
            GetLocalIP();
        }

        private void LoadData() {
            textDatabase.Text = regKey.GetValue("Database Server").ToString();
            textDBUser.Text = regKey.GetValue("Database User").ToString();
            textDBPass.Text = regKey.GetValue("Database Password").ToString();
            textIP.Text = GetLocalIP();
            textPort.Text = regKey.GetValue("Cashier Port").ToString();
            textKitchenIP.Text = regKey.GetValue("Kitchen Computer").ToString();
            textKitchenPort.Text = regKey.GetValue("Kitchen Port").ToString();
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

        private void btnExit_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            if (btn.Name == "buttonSettingOK") {
                regKey.SetValue("Database Server", textDatabase.Text.ToString());
                regKey.SetValue("Database User", textDBUser.Text.ToString());
                regKey.SetValue("Database Password", textDBPass.Text.ToString());
                regKey.SetValue("Kitchen Computer", textKitchenIP.Text.ToString());
                regKey.SetValue("Kitchen Port", textKitchenPort.Text.ToString());
                regKey.SetValue("Cashier Port", textPort.Text.ToString());

                DialogResult dialog = MessageBox.Show("Please restart the application for these changes to take effect.", "Restart", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) {
                    Application.Restart();
                }
            }
            regKey.Close();
            this.Close();
        }
    }
}
