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
    public partial class OwnerChart : Form {
        string type = "";
        public OwnerChart(string type) {
            InitializeComponent();
            this.type = type;
            LoadChartData();
        }

        public void LoadChartData() {
            Summary sm = new Summary();
            if (this.type == "daily") {
                label1.Text = "Daily";
                int rowCount = Convert.ToInt32(sm.dataGridDaily.RowCount.ToString()) - 1;
                int length = 0;
                if (rowCount < 10) length = rowCount;
                else length = 10;
                for (int i = 0; i < length; i++) {
                    string x = (sm.dataGridDaily.Rows[i].Cells[0].Value).ToString();
                    int y = Convert.ToInt32((sm.dataGridDaily.Rows[i].Cells[5].Value).ToString());
                    chart1.ChartAreas[0].AxisX.IsReversed = true;
                    chart1.Series["Total Earnings"].Points.AddXY(x, y);
                }
            } else if (this.type == "weekly") {
                label1.Text = "Weekly";
                int rowCount = Convert.ToInt32(sm.dataGridWeekly.RowCount.ToString()) - 1;
                int length = 0;
                if (rowCount < 10) length = rowCount;
                else length = 10;
                for (int i = 0; i < length; i++) {
                    string x = (sm.dataGridWeekly.Rows[i].Cells[0].Value).ToString() + ", " + (sm.dataGridWeekly.Rows[i].Cells[1].Value).ToString();
                    int y = Convert.ToInt32((sm.dataGridWeekly.Rows[i].Cells[6].Value).ToString());
                    chart1.ChartAreas[0].AxisX.IsReversed = true;
                    chart1.Series["Total Earnings"].Points.AddXY(x, y);
                }
            } else if (this.type == "monthly") {
                label1.Text = "Monthly";
                int rowCount = Convert.ToInt32(sm.dataGridMonthly.RowCount.ToString()) - 1;
                int length = 0;
                if (rowCount < 10) length = rowCount;
                else length = 10;
                for (int i = 0; i < length; i++) {
                    string x = (sm.dataGridMonthly.Rows[i].Cells[0].Value).ToString() + ", " + (sm.dataGridMonthly.Rows[i].Cells[1].Value).ToString();
                    int y = Convert.ToInt32((sm.dataGridMonthly.Rows[i].Cells[6].Value).ToString());
                    chart1.ChartAreas[0].AxisX.IsReversed = true;
                    chart1.Series["Total Earnings"].Points.AddXY(x, y);
                }
            } else {
                label1.Text = "Yearly";
                int rowCount = Convert.ToInt32(sm.dataGridYearly.RowCount.ToString()) - 1;
                int length = 0;
                if (rowCount < 10) length = rowCount;
                else length = 10;
                for (int i = 0; i < length; i++) {
                    string x = (sm.dataGridYearly.Rows[i].Cells[0].Value).ToString();
                    int y = Convert.ToInt32((sm.dataGridYearly.Rows[i].Cells[5].Value).ToString());
                    chart1.ChartAreas[0].AxisX.IsReversed = true;
                    chart1.Series["Total Earnings"].Points.AddXY(x, y);
                }
            }
        }
    }
}
