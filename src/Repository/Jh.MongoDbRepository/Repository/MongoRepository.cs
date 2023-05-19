
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Repository;
using Jh.MongoDbRepository.Interfaces;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Jh.MongoDbRepository.Repository
{
    public class MongoRepository<T> : IMongoRepository<T>
        where T : class, IEntity<string>
    {
        #region Default Constructor
        public IMongoDatabase Database { get { return _data; } }
        IMongoDatabase _data;
        public IMongoCollection<T> Collection { get { return _db; } }
        public IMongoCollection<T> _db;

        public MongoRepository(IMongoDatabase data, string collectionName)
        {
            _data = data;
            _db = data.GetCollection<T>(collectionName);

        }
        public MongoRepository(IMongoDatabase data) : this(data, typeof(T).Name.ToLower())
        {

        }
        #endregion

        private void CheckAndAddId(T model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = ObjectId.GenerateNewId().ToString();
            }
        }
        public bool CheckToken(CancellationToken? token = null)
        {
            if (token.HasValue && token.GetValueOrDefault().IsCancellationRequested)
            {
                return true;
            }
            return false;
        }
        #region Add
        public async ValueTask AddAsync(T model, CancellationToken? token = null)
        {
            CheckAndAddId(model);
            _db.InsertOne(model);

        }
        public async ValueTask AddRangeAsync(System.Collections.Generic.IEnumerable<T> models, CancellationToken? token = null)
        {
            foreach (var i in models)
                CheckAndAddId(i);
            _db.InsertMany(models);


        }
        #endregion
        #region Find ...
        public async ValueTask<IQueryable<T>> FindAsync(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate, CancellationToken? token = null)
        {
            return _db.AsQueryable<T>().Where(predicate);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, CancellationToken? token = null)
        {
            return _db.AsQueryable<T>().Where(predicate);
        }
        public async ValueTask<System.Linq.IQueryable<T>> GetAllAsync(CancellationToken? token)
        {
            return await FindAsync(m => true, token);
        }
        public async ValueTask<T> GetAsync(string id, CancellationToken? token = null)
        {
            return _db.Find(m => m.Id == id).FirstOrDefault();
        }
        public T Get(string id)
        {
            return _db.Find(m => m.Id == id).FirstOrDefault();
        }
        #endregion
        #region Remove...
        public async ValueTask RemoveAsync(T model, CancellationToken? token = null)
        {
            _db.DeleteOne(m => m.Id == model.Id);

        }

        public async ValueTask<T> RemoveAsync(string id, CancellationToken? token = null)
        {
            var model = await GetAsync(id, token);
            if (model == null) { }
            await RemoveAsync(model, token);
            return model;
        }

        public ValueTask RemoveRangeAsync(System.Collections.Generic.IEnumerable<T> models, CancellationToken? token = null)
        {
            foreach (var i in models)
            {
                _db.DeleteOne(m => m.Id == i.Id);
            }
            return default;
        }
        #endregion
        #region Update ...
        public ValueTask UpdateAsync(T model, CancellationToken? token = null)
        {
            _db.FindOneAndReplace(m => m.Id == model.Id, model);
            return default;
        }

        public async ValueTask UpdateRangeAsync(IEnumerable<T> models)
        {
            var data = new List<WriteModel<T>>();
            foreach (var i in models)
            {
                data.Add(new ReplaceOneModel<T>(i.Id, i));
            }
            await _db.BulkWriteAsync(data, new BulkWriteOptions() { IsOrdered = false });

        }


        #endregion

    }
}
