using System;
using System.Data.Entity;
using System.Linq;

namespace GebatModel
{
    public abstract class BaseRepository
    {
        
        private GebatDataBaseEntities context = new GebatDataBaseEntities();

        /// <summary>
        /// Returns a colection of IQueayable of a TEntity from the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <returns>A collection of IQueryable of TEntity.</returns>
        protected virtual IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class
        {
            var setter = context.Set<TEntity>();
            return setter.AsQueryable();
        }

        /// <summary>
        /// Adds an entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <param name="entity">Entity to add to add in the database.</param>
        protected virtual void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            var setter = context.Set<TEntity>();
            setter.Add(entity);
            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <param name="entity">Modified entity to modify in the databse.</param>
        protected virtual void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            var setter = context.Set<TEntity>();
            setter.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes an entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <param name="entity">Entity to delete in the database.</param>
        protected virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var setter = context.Set<TEntity>();
            setter.Remove(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Constructor of the repository.
        /// </summary>
        public BaseRepository()
        {
            context.Configuration.LazyLoadingEnabled = true;
        }
    }
}
