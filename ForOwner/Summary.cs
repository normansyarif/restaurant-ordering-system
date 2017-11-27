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

namespace ForOwner {
    public partial class Summary : UserControl {
        MySqlConnection conn;
        MySqlDataReader myReader;

        DataTable tableDaily = new DataTable();
        DataTable tableWeekly = new DataTable();
        DataTable tableMonthly = new DataTable();
        DataTable tableYearly = new DataTable();

        public Summary() {
            InitializeComponent();
            conn = DB.openConn();
            LoadDailyTable();
            LoadDailyData();
            LoadWeeklyTable();
            LoadWeeklyData();
            LoadMonthlyTable();
            LoadMontlyData();
            LoadYearlyTable();
            LoadYearlyData();

        }

        public void LoadDailyTable() {
            tableDaily.Columns.Add("Date", typeof(string));
            tableDaily.Columns.Add("Food", typeof(int));
            tableDaily.Columns.Add("Drink", typeof(int));
            tableDaily.Columns.Add("Dessert", typeof(int));
            tableDaily.Columns.Add("Total items", typeof(int));
            tableDaily.Columns.Add("Total earnings (Rp. )", typeof(int));
            this.dataGridDaily.DataSource = tableDaily;
        }

        public void LoadWeeklyTable() {
            tableWeekly.Columns.Add("Week", typeof(string));
            tableWeekly.Columns.Add("Year", typeof(string));
            tableWeekly.Columns.Add("Food", typeof(int));
            tableWeekly.Columns.Add("Drink", typeof(int));
            tableWeekly.Columns.Add("Dessert", typeof(int));
            tableWeekly.Columns.Add("Total items", typeof(int));
            tableWeekly.Columns.Add("Total earnings (Rp. )", typeof(int));
            this.dataGridWeekly.DataSource = tableWeekly;
        }

        public void LoadMonthlyTable() {
            tableMonthly.Columns.Add("Month", typeof(string));
            tableMonthly.Columns.Add("Year", typeof(string));
            tableMonthly.Columns.Add("Food", typeof(int));
            tableMonthly.Columns.Add("Drink", typeof(int));
            tableMonthly.Columns.Add("Dessert", typeof(int));
            tableMonthly.Columns.Add("Total items", typeof(int));
            tableMonthly.Columns.Add("Total earnings (Rp. )", typeof(int));
            this.dataGridMonthly.DataSource = tableMonthly;
        }

        public void LoadYearlyTable() {
            tableYearly.Columns.Add("Year", typeof(string));
            tableYearly.Columns.Add("Food", typeof(int));
            tableYearly.Columns.Add("Drink", typeof(int));
            tableYearly.Columns.Add("Dessert", typeof(int));
            tableYearly.Columns.Add("Total items", typeof(int));
            tableYearly.Columns.Add("Total earnings (Rp. )", typeof(int));
            this.dataGridYearly.DataSource = tableYearly;
        }

        public void LoadDailyData() {
            if (tableDaily != null) {
                tableDaily.Clear();
                dataGridDaily.DataSource = tableDaily;
            }
            string query = "select * from sales order by date desc";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    DateTime date = myReader.GetDateTime(myReader.GetOrdinal("date"));
                    int food = Convert.ToInt32(myReader["food"].ToString());
                    int drink = Convert.ToInt32(myReader["drink"].ToString());
                    int dessert = Convert.ToInt32(myReader["dessert"].ToString());
                    int total_items = food + drink + dessert;
                    int total_earnings = Convert.ToInt32(myReader["total_price"].ToString());
                    tableDaily.Rows.Add(date.ToString("dd-MM-yyyy"), food, drink, dessert, total_items, total_earnings);
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting daily list - " + ex.Message);
            }
        }

        public void LoadWeeklyData() {
            if (tableWeekly != null) {
                tableWeekly.Clear();
                dataGridWeekly.DataSource = tableWeekly;
            }
            string query = "select YEAR(date) as year, WEEK(date) as week, sum(food) as total_food, sum(drink) as total_drink, sum(dessert) as total_dessert, sum(total_price) as total_price from sales group by YEAR(date), WEEK(date) order by YEAR(date) desc, WEEK(date) desc";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    string year = myReader["year"].ToString();
                    string week = myReader["week"].ToString();
                    int food = Convert.ToInt32(myReader["total_food"].ToString());
                    int drink = Convert.ToInt32(myReader["total_drink"].ToString());
                    int dessert = Convert.ToInt32(myReader["total_dessert"].ToString());
                    int total_items = food + drink + dessert;
                    int total_earnings = Convert.ToInt32(myReader["total_price"].ToString());
                    tableWeekly.Rows.Add(week, year, food, drink, dessert, total_items, total_earnings);
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting weekly list - " + ex.Message);
            }
        }

        public void LoadMontlyData() {
            if (tableMonthly != null) {
                tableMonthly.Clear();
                dataGridMonthly.DataSource = tableMonthly;
            }
            string query = "select YEAR(date) as year, MONTH(date) as month, sum(food) as total_food, sum(drink) as total_drink, sum(dessert) as total_dessert, sum(total_price) as total_price from sales group by MONTH(date), YEAR(date) order by YEAR(date) desc, MONTH(date) desc";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    string year = myReader["year"].ToString();
                    string month = myReader["month"].ToString();
                    int food = Convert.ToInt32(myReader["total_food"].ToString());
                    int drink = Convert.ToInt32(myReader["total_drink"].ToString());
                    int dessert = Convert.ToInt32(myReader["total_dessert"].ToString());
                    int total_items = food + drink + dessert;
                    int total_earnings = Convert.ToInt32(myReader["total_price"].ToString());
                    tableMonthly.Rows.Add(month, year, food, drink, dessert, total_items, total_earnings);
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting monthly list - " + ex.Message);
            }
        }

        public void LoadYearlyData() {
            if (tableYearly != null) {
                tableYearly.Clear();
                dataGridYearly.DataSource = tableYearly;
            }
            string query = "select YEAR(date) as year, sum(food) as total_food, sum(drink) as total_drink, sum(dessert) as total_dessert, sum(total_price) as total_price from sales group by YEAR(date) order by YEAR(date) desc";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                while (myReader.Read()) {
                    string year = myReader["year"].ToString();
                    int food = Convert.ToInt32(myReader["total_food"].ToString());
                    int drink = Convert.ToInt32(myReader["total_drink"].ToString());
                    int dessert = Convert.ToInt32(myReader["total_dessert"].ToString());
                    int total_items = food + drink + dessert;
                    int total_earnings = Convert.ToInt32(myReader["total_price"].ToString());
                    tableYearly.Rows.Add(year, food, drink, dessert, total_items, total_earnings);
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting monthly list - " + ex.Message);
            }
        }

        private void dataGridDaily_CellClick(object sender, DataGridViewCellEventArgs e) {
            int rowIndex = e.RowIndex;
            string date = (dataGridDaily.Rows[rowIndex].Cells[0].Value).ToString();
            labelD.Text = "Date : " + date;
            string cond = date.Substring(6,4) + "-" + date.Substring(3,2) + "-" + date.Substring(0,2);
            LoadDayDetails(cond);
        }

        private void dataGridWeekly_CellClick(object sender, DataGridViewCellEventArgs e) {
            int rowIndex = e.RowIndex;
            string week = (dataGridWeekly.Rows[rowIndex].Cells[0].Value).ToString();
            string year = (dataGridWeekly.Rows[rowIndex].Cells[1].Value).ToString();
            labelW.Text = "Week : " + week + ", " + year;
            LoadWeekDetails(week, year);
        }

        private void dataGridMonthly_CellClick(object sender, DataGridViewCellEventArgs e) {
            int rowIndex = e.RowIndex;
            string month = (dataGridMonthly.Rows[rowIndex].Cells[0].Value).ToString();
            string year = (dataGridMonthly.Rows[rowIndex].Cells[1].Value).ToString();
            labelM.Text = "Month : " + month + ", " + year;
            LoadMonthDetails(month, year);
        }

        private void dataGridYearly_CellClick(object sender, DataGridViewCellEventArgs e) {
            int rowIndex = e.RowIndex;
            string year = (dataGridYearly.Rows[rowIndex].Cells[0].Value).ToString();
            labelY.Text = "Year : " + year;
            LoadYearDetails(year);
        }

        private void LoadDayDetails(string cond) {
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            if (table != null) {
                table.Clear();
                dataGridDDetail.DataSource = table;
            }
            string query = "select menu.name as name, menu.type as type, sales_details.quantity as quantity from menu, sales_details where menu.id = sales_details.id_item and sales_details.date = '" + cond + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                int i = 1;
                while (myReader.Read()) {
                    table.Rows.Add(i, myReader["name"].ToString(), myReader["type"].ToString(), Convert.ToInt32(myReader["quantity"].ToString()));
                    i++;
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting daily details - " + ex.Message);
            }
        }

        private void LoadWeekDetails(string week, string year) {
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            if (table != null) {
                table.Clear();
                dataGridWDetail.DataSource = table;
            }
            string query = "select menu.name as name, menu.type as type, sales_details.quantity as quantity from menu, sales_details where menu.id = sales_details.id_item and YEAR(sales_details.date) = '" + year + "' and WEEK(sales_details.date) = '" + week + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                int i = 1;
                while (myReader.Read()) {
                    table.Rows.Add(i, myReader["name"].ToString(), myReader["type"].ToString(), Convert.ToInt32(myReader["quantity"].ToString()));
                    i++;
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting weekly details - " + ex.Message);
            }
        }

        private void LoadMonthDetails(string month, string year) {
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            if (table != null) {
                table.Clear();
                dataGridMDetail.DataSource = table;
            }
            string query = "select menu.name as name, menu.type as type, sales_details.quantity as quantity from menu, sales_details where menu.id = sales_details.id_item and YEAR(sales_details.date) = '" + year + "' and MONTH(sales_details.date) = '" + month + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                int i = 1;
                while (myReader.Read()) {
                    table.Rows.Add(i, myReader["name"].ToString(), myReader["type"].ToString(), Convert.ToInt32(myReader["quantity"].ToString()));
                    i++;
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting monthly details - " + ex.Message);
            }
        }

        private void LoadYearDetails(string year) {
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            if (table != null) {
                table.Clear();
                dataGridYDetail.DataSource = table;
            }
            string query = "select menu.name as name, menu.type as type, sales_details.quantity as quantity from menu, sales_details where menu.id = sales_details.id_item and YEAR(sales_details.date) = '" + year + "'";
            MySqlCommand command = new MySqlCommand(query, conn);
            try {
                conn.Open();
                myReader = command.ExecuteReader();
                int i = 1;
                while (myReader.Read()) {
                    table.Rows.Add(i, myReader["name"].ToString(), myReader["type"].ToString(), Convert.ToInt32(myReader["quantity"].ToString()));
                    i++;
                }
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show("Error while getting monthly details - " + ex.Message);
            }
        }

        private void buttonChart_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            OwnerChart oc = new OwnerChart(btn.Name);
            oc.Show();
        }

    }
}
