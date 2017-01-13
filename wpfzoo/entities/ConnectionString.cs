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

<<<<<<< HEAD
        public string Port
=======
        public String Port
>>>>>>> master
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

<<<<<<< HEAD
        public string Database
=======
        public String Database
>>>>>>> master
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

<<<<<<< HEAD
        public string Uuid
=======
        public String Uuid
>>>>>>> master
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

<<<<<<< HEAD
        public string Pwd
=======
        public String Pwd
>>>>>>> master
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
<<<<<<< HEAD
    }
}
=======

        public override String ToString()
        {
            String connectionString = "Server="+this.server+";Port="+this.port+";Database="+this.database+";Uid="+this.uuid+";Pwd=" +this.pwd;
            return connectionString;
        }
    }
}
>>>>>>> master
