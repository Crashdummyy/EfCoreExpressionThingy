using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;

namespace Data
{
    /// <summary>
    ///     Basic Repository containing all neccessary CRUD-Operations
    /// </summary>
    /// <typeparam name="T">As of now on valid for <see cref="IEntity{T}"/>-types</typeparam>
    public interface IRepository<T>
        where T : class, IEntity<int>, new()
    {
        /// <summary>
        ///     Try to get all <see cref="T"/>
        /// </summary>
        /// <param name="includeProperties">Inclusion of NavigationProperties</param>
        /// <returns>Ef's DbSet with expanded Properties as IEnumerable</returns>
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        /// <inheritdoc cref="GetAll(System.Linq.Expressions.Expression{System.Func{T,object}}[])"/>
        IEnumerable<T> GetAll();

        /// <summary>
        ///     Counts all <see cref="T"/> regardless of Data-integrity
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        ///     Get a single <see cref="T"/> by its Id ( if that really is its PKey and of type INT )
        /// </summary>
        /// <param name="id">id to lookup</param>
        /// <returns>Single <see cref="T"/> or null if not found</returns>
        T GetSingle(int id);

        /// <summary>
        ///     Get a single <see cref="T"/>
        /// </summary>
        /// <param name="predicate">predicate to check against</param>
        /// <returns>Single <see cref="T"/> or null if not found</returns>
        T GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Get a single <see cref="T"/>
        /// </summary>
        /// <param name="predicate">predicate to check against</param>
        /// <param name="includeProperties">Inclusion of NavigationProperties</param>
        /// <returns>Single <see cref="T"/> or null if not found</returns>
        T GetSingle(Expression<Func<T, bool>> predicate,
                    params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        ///     Performs a FROM Set{<see cref="T"/>} WHER <param name="predicate"></param> Query
        /// </summary>
        /// <param name="predicate">predicate to check against</param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Performs a FROM Set{<see cref="T"/>} WHER <param name="predicate"></param> Query
        /// </summary>
        /// <param name="predicate">predicate to check against</param>
        /// <param name="includeProperties">Inclusion of NavigationProperties</param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate,
                              params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        ///     Stages a Single <see cref="T"/> for Insertion
        /// </summary>
        /// <param name="entity"><see cref="T"/> to save into the DB</param>
        void Add(T entity);

        /// <inheritdoc cref="Add"/>
        Task AddAsync(T entity);

        /// <summary>
        ///     Marks the Entity as Modified ( Mandatory for Saving in Stateless Environment )
        /// </summary>
        /// <param name="entity"><see cref="T"/> to save Changes to</param>
        void Update(T entity);

        /// <summary>
        ///     Marks a <see cref="T"/> to be deleted on <see cref="SaveChanges"/>
        /// </summary>
        /// <param name="entity">Entitiy to Delele</param>
        void Delete(T entity);

        /// <summary>
        ///     Marks one or more <see cref="T"/> to be deleted on <see cref="SaveChanges"/>
        /// </summary>
        /// <param name="predicate">predicate to build Where Clause from</param>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Calls the SaveChanges Method on the Ef-Context
        /// </summary>
        void SaveChanges();

        /// <inheritdoc cref="SaveChanges"/>
        Task<int> SaveChangesAsync();
    }
}