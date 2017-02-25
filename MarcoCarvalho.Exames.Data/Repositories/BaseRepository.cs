using System.Collections.Generic;
using MarcoCarvalho.Exames.Data.Common;
using System.Data.Common;

namespace MarcoCarvalho.Exames.Data.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        public abstract IEnumerable<T> FetchAll();
        public abstract T FetchOneById(int id);
        public abstract T Insert(T entity);
        public abstract T Update(T entity);
        public abstract void DeleteOneById(int id);

        protected DbConnection _connection;

        public BaseRepository()
        {
            _connection = ConnectionFactory.GetConnection();
        }
    }
}
