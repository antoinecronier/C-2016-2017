using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.entities
{
    public class ConnectionString
    {
        private String server;
        private String port;
        private String database;
        private String uuid;
        private String pwd;

        public string Server
        {
            get
            {
                return server;
            }

            set
            {
                server = value;
            }
        }

        public string Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public string Database
        {
            get
            {
                return database;
            }

            set
            {
                database = value;
            }
        }

        public string Uuid
        {
            get
            {
                return uuid;
            }

            set
            {
                uuid = value;
            }
        }

        public string Pwd
        {
            get
            {
                return pwd;
            }

            set
            {
                pwd = value;
            }
        }

        public ConnectionString()
        {

        }
    }
}
