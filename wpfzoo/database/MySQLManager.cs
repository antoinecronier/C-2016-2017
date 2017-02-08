using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;
using wpfzoo.entities.json;

namespace wpfzoo.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySQLManager<TEntity> : DbContext where TEntity : class
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< Updated upstream
        public MySQLManager() 
            : base(JsonManager.Instance.ReadFile<ConnectionString>(@"C:\Users\tactfactory\Documents\Visual Studio 2015\Projects\wpfzoo\jsonconfig\", @"MysqlConfig.json").ToString())
=======
        public MySQLManager()
        : base(JsonManager.Instance.ReadFile<ConnectionString>(@"..\..\..\jsonconfig\", @"MysqlConfig.json").ToString())
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
=======
>>>>>>> master
        public MySQLManager()
            : base(JsonManager.Instance.ReadFile<ConnectionString>(@"..\..\..\jsonconfig\", @"MysqlConfig.json").ToString())
<<<<<<< HEAD
>>>>>>> 08354390412fe1ce8853681aa2af91b9e442c7d4
>>>>>>> master
=======
>>>>>>> master
        {
            MySQLFullDB initDBIfNotExist = new MySQLFullDB();
        }

        public DbSet<TEntity> DbSetT { get; set; }

        public async Task<TEntity> Insert(TEntity item)
        {
            this.DbSetT.Add(item);
            await this.SaveChangesAsync();
            return item;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> items)
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
    public async Task<TEntity> Update(TEntity item)
    {
        await Task.Factory.StartNew(() =>
>>>>>>> master
=======
        public async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> items)
>>>>>>> master
        {
            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            await this.SaveChangesAsync();
            return items;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public async Task<TEntity> Update(TEntity item)
<<<<<<< Updated upstream
>>>>>>> Stashed changes
        {
            await Task.Factory.StartNew(() =>
            {
<<<<<<< Updated upstream
                this.DbSetT.Add(item);
            }
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Update(TEntity item)
=======
    public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
    {
        await Task.Factory.StartNew(() =>
>>>>>>> master
=======
        public async Task<TEntity> Update(TEntity item)
>>>>>>> master
        {
            await Task.Factory.StartNew(() =>
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            await this.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
<<<<<<< HEAD
<<<<<<< HEAD
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach((items as List<TEntity>)[0]);
                this.DbSetT.RemoveRange(items);
            });
            var res = await this.SaveChangesAsync();
            return res;
        }
=======
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            await this.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
=======
>>>>>>> master
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            await Task.Factory.StartNew(() =>
            {
                temp = base.Set<TEntity>();
            });
            result.AddRange(temp);
            return result;
        }
<<<<<<< HEAD

        public async Task<Int32> Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
        }

=======
        {
            await Task.Factory.StartNew(() =>
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            await this.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
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
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
        }

>>>>>>> Stashed changes
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
>>>>>>> Stashed changes
=======
            this.DbSetT.Attach(item);
            this.DbSetT.Remove(item);
        });
        return await this.SaveChangesAsync();
    }
>>>>>>> master
=======
>>>>>>> master

        public async Task<Int32> Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
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

