using System;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExpressionBug
{
    public class DbContextFixture<T> where T : DbContext
    {
        protected T Context { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DbContextFixture{T}"/>-class.
        /// </summary>
        public DbContextFixture()
        {
            this.InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<T>().UseInMemoryDatabase("Fixture")
                                                          .Options;

            
            this.Context = (T)Activator.CreateInstance(typeof(T),options);
            this.Context.Database.EnsureCreated();
        }
    }
}