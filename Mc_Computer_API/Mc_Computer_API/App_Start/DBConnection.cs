using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mc_Computer_API.App_Start
{
    public class DBConnection
    {

        private static volatile DBConnection instance;
        private static object syncRoot = new Object();
        private static MySqlConnection McComputers;

        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DBConnection();
                    }
                }
                return instance;
            }
        }

        public static MySqlConnection ConnMcComputers
        {
            get { return DBConnection.McComputers; }
        }

        public static ConnectionState StatesMcComputers()
        {
            return DBConnection.ConnMcComputers.State;
        }
    }
}