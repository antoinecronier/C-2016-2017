using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;
using wpfzoo.json;
using wpfzoo.logger;

namespace wpfzoo.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySQLManager<TEntity> : DbContext where TEntity : class
    {
        Logger logger = new Logger("MySQLManagerLogger",LogMode.CURRENT_FOLDER,AlertMode.MESSAGE_BOX);
        public MySQLManager()
            : base(JsonManager.Instance.ReadFile<ConnectionString>(@"..\..\..\jsonconfig\", @"MysqlConfig.json").ToString())

        {
            MySQLFullDB initDBIfNotExist = new MySQLFullDB();
        }

        public DbSet<TEntity> DbSetT { get; set; }

        public async Task<TEntity> Insert(TEntity item)
        {
            bool isDetached = this.Entry(item).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(item);
            this.DbSetT.Add(item);
            await this.SaveChangesAsync();
            logger.Log(item);
            return item;
        }

        public async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                bool isDetached = this.Entry(item).State == EntityState.Detached;
                if (isDetached)
                    this.DbSetT.Attach(item);
                this.DbSetT.Add(item);
            }
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Update(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                bool isDetached = this.Entry(item).State == EntityState.Detached;
                if (isDetached)
                    this.DbSetT.Attach(item);
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            await this.SaveChangesAsync();
            logger.Log(item);
            return item;
        }

        public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    bool isDetached = this.Entry(item).State == EntityState.Detached;
                    if (isDetached)
                        this.DbSetT.Attach(item);
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Get(Int32 id)
        {
            TEntity item = await this.DbSetT.FindAsync(id) as TEntity;
            bool isDetached = this.Entry(item).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(item);
            logger.Log(item);
            return item;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            await Task.Factory.StartNew(() =>
            {
                temp = base.Set<TEntity>();
            });
            result.AddRange(temp);
            return result;
        }

        public async Task<Int32> Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                bool isDetached = this.Entry(item).State == EntityState.Detached;
                if (isDetached)
                    this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
            logger.Log(item);
            return await this.SaveChangesAsync();
        }

        public async Task<Int32> Delete(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach((items as List<TEntity>)[0]);
                this.DbSetT.RemoveRange(items);
            });
            var res = await this.SaveChangesAsync();
            return res;
        }
    }
}

/*public async Task<IEnumerable<TEntity>> CustomQuery(Criteria criteria)
{
    return await this.DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
}*/

