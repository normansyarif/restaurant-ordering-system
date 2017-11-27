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

namespace ForOwner {
    public partial class OwnerSettings : Form {
        RegistryKey regKey = null;
        public OwnerSettings() {
            InitializeComponent();
            regKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RestaurantOrderingSystem");
            LoadData();
        }

        private void LoadData() {
            textDatabase.Text = regKey.GetValue("Database Server").ToString();
            textDBUser.Text = regKey.GetValue("Database User").ToString();
            textDBPass.Text = regKey.GetValue("Database Password").ToString();
        }

        private void buttonSettingOK_Click(object sender, EventArgs e) {
            Button b = (Button)sender;
            if (b.Name == "buttonSettingOK") {
                regKey.SetValue("Database Server", textDatabase.Text.ToString());
                regKey.SetValue("Database User", textDBUser.Text.ToString());
                regKey.SetValue("Database Password", textDBPass.Text.ToString());

                DialogResult dialog = MessageBox.Show("Please restart the application in order the changes to take effect?", "Restart", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) {
                    Application.Restart();
                }
            }
            this.Close();
        }
    }
}
