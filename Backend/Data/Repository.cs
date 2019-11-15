using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Data
{
    /// <inheritdoc />
    public class Repository<T> : IRepository<T>
        where T : class, IEntity<int>, new()
    {
        /// <summary>
        ///     The <see cref="DbContext"/> for all Interactions with the Database
        /// </summary>
        protected readonly AdminContext Context;

        /// <summary>
        ///     Initializes a new Insstance of the <see cref="Repository{T}"/> class
        /// </summary>
        /// <param name="context"><see cref="DbContext"/> to Use</param>
        public Repository(AdminContext context)
        {
            this.Context = context;
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> GetAll()
        {
            return this.Context.Set<T>()
                       .AsEnumerable();
        }
        
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public virtual int Count()
        {
            return this.Context.Set<T>()
                       .Count();
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.Context.Set<T>();
            query = includeProperties.Aggregate(query,
                                                (x,
                                                 prop) => x.Include(prop));

            return query.AsEnumerable();
        }

        /// <inheritdoc />
        public virtual T GetSingle(int id)
        {
            return this.Context.Set<T>()
                       .FirstOrDefault(x => x.Id == id);
        }

        /// <inheritdoc />
        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return this.Context.Set<T>()
                       .FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public virtual T GetSingle(Expression<Func<T, bool>> predicate,
                                   params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.Context.Set<T>();

            query = includeProperties.Aggregate(query,
                                                (x,
                                                 prop) => x.Include(prop));

            return query.Where(predicate)
                        .FirstOrDefault();
        }
        
        public virtual T GetSingleValid(Func<T, bool> predicate,
                                        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.Context.Set<T>();

            query = includeProperties.Aggregate(query,
                                                (x,
                                                 prop) => x.Include(prop));

            return query.Where(predicate)
                        .FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.Context.Set<T>()
                       .Where(predicate);
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate,
                                             params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.Context.Set<T>();
            query = includeProperties.Aggregate(query,
                                                (x,
                                                 prop) => x.Include(prop));

            return query.Where(predicate)
                        .AsEnumerable();
        }

        /// <inheritdoc />
        public virtual void Add(T entity)
        {
            this.Context.Set<T>()
                .Add(entity);
        }

        /// <inheritdoc />
        public virtual async Task AddAsync(T entity)
        {
            await this.Context.Set<T>()
                      .AddAsync(entity);
        }

        /// <inheritdoc />
        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = this.Context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <inheritdoc />
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = this.Context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <inheritdoc />
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = this.Context.Set<T>()
                                          .Where(predicate);

            foreach (var entity in entities)
            {
                this.Context.Entry(entity)
                    .State = EntityState.Deleted;
            }
        }

        /// <inheritdoc />
        public virtual void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        /// <inheritdoc />
        public virtual async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Check for any Changes inside the Ef-ChangeTracker
        /// </summary>
        /// <returns>Dictionary keyed by <see cref="EntityState"/> with changed <see cref="T"/></returns>
        public virtual Dictionary<EntityState, IEnumerable<T>> CheckPendingChanges()
        {
            var dic = new Dictionary<EntityState, IEnumerable<T>>
                      {
                          {
                              EntityState.Added, this.Context.ChangeTracker.Entries<T>()
                                                     .Where(x => x.State == EntityState.Added)
                                                     .Select(x => x.Entity)
                          },
                          {
                              EntityState.Deleted, this.Context.ChangeTracker.Entries<T>()
                                                       .Where(x => x.State == EntityState.Deleted)
                                                       .Select(x => x.Entity)
                          },
                          {
                              EntityState.Modified, this.Context.ChangeTracker.Entries<T>()
                                                        .Where(x => x.State == EntityState.Modified)
                                                        .Select(x => x.Entity)
                          }
                      };

            return dic;
        }
    }
}