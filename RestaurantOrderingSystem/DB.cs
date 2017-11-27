
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace RestaurantOrderingSystem {
    class DB {

        static RegistryKey regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RestaurantOrderingSystem");

        public static MySqlConnection openConn() {
            string[] ipAndPort = regkey.GetValue("Database Server").ToString().Split(':');
            string ipAddr = ipAndPort[0];
            string port = ipAndPort[1];
            string dbUser = regkey.GetValue("Database User").ToString();
            string dbPass = regkey.GetValue("Database Password").ToString();

            string stringConn = "datasource="+ipAddr+";port="+port+";username="+dbUser+";password="+dbPass+";database=orderingsystem;";
            MySqlConnection myConn = new MySqlConnection(stringConn);
            return myConn;
        }

    }
}
