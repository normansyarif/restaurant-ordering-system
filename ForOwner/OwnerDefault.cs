using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForOwner {
    public partial class OwnerDefault : Form {
        public OwnerDefault() {
            InitializeComponent();
            buttonSummary_Click(null, null);
        }

        private void buttonSummary_Click(object sender, EventArgs e) {
            sidePanel.Height = buttonSummary.Height;
            sidePanel.Top = buttonSummary.Top;
            Summary summary = new Summary();
            labelMenu.Text = "Sales Summary";
            summary.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(summary);
        }

        private void buttonMenu_Click(object sender, EventArgs e) {
            sidePanel.Height = buttonMenu.Height;
            sidePanel.Top = buttonMenu.Top;
            Menu menu = new Menu();
            labelMenu.Text = "Menu Management";
            menu.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(menu);
        }

        private void buttonEmp_Click(object sender, EventArgs e) {
            sidePanel.Height = buttonEmp.Height;
            sidePanel.Top = buttonEmp.Top;
            Employee emp = new Employee();
            labelMenu.Text = "Employee Management";
            emp.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(emp);
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Do you really want to close the program?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                Application.Exit();
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e) {
            OwnerSettings settings = new OwnerSettings();
            settings.Show();
        }

        private void buttonAbout_Click(object sender, EventArgs e) {
            OwnerAbout about = new OwnerAbout();
            about.Show();
        }

    }
}
