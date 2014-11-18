using System.Data.Entity;
using System.Linq;

namespace GebatModel
{
    public abstract class BaseRepository
    {
        #region//Private Methods

        private GebatDataBaseEntities entities = new GebatDataBaseEntities();

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Returns a colection of IQueayable of a TEntity from the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <returns>A collection of IQueryable of TEntity.</returns>
        protected virtual IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class
        {
            var setter = entities.Set<TEntity>();
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
            var setter = entities.Set<TEntity>();
            setter.Add(entity);
            entities.SaveChanges();
        }

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <param name="entity">Modified entity to modify in the databse.</param>
        protected virtual void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            var setter = entities.Set<TEntity>();
            setter.Attach(entity);
            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        /// <summary>
        /// Deletes an entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the collection.</typeparam>
        /// <param name="entity">Entity to delete in the database.</param>
        protected virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var setter = entities.Set<TEntity>();
            setter.Remove(entity);
            entities.SaveChanges();
        }

        #endregion

        /// <summary>
        /// Constructor of the repository.
        /// </summary>
        public BaseRepository()
        {
            entities.Configuration.LazyLoadingEnabled = true;
        }
    }
}
