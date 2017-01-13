using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.entities
{
    public class ConnectionString
    {
        private String server = "";
        private String port = "";
        private String database = "";
        private String uuid = "";
        private String pwd = "";

        public String Server
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

        public String Port
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

        public String Database
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

        public String Uuid
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

        public String Pwd
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

        public override String ToString()
        {
            String connectionString = "Server="+this.server+";Port="+this.port+";Database="+this.database+";Uid="+this.uuid+";Pwd=" +this.pwd;
            return connectionString;
        }
    }
}
