using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;
using wpfzoo.entities.bases;

namespace wpfzoo.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    public class MySQLDBManager<T> : DbContext where T : BaseDBEntity
    {
        public DbSet<T> DbSetT { get; set; }
        
        public MySQLDBManager() : base("Server=127.0.0.1;Port=3306;Database=zoo;Uid=root;Pwd=''")
        {
        }

        public void insert(T item)
        {
            this.DbSetT.Add(item);
            this.SaveChanges();
        }

        public void delete (T item)
        {
            if (item != null)
            {
                this.DbSetT.Remove(item);
                this.SaveChanges();
            }
        }

        public void update (T item)
        {
            if (item != null)
            {
                this.Entry(item).CurrentValues.SetValues(new object());
                this.SaveChanges();
            }
        }


    }

}
