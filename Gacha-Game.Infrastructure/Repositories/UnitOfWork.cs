﻿using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gacha_Game.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GachaDbContext _db;

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public UnitOfWork(GachaDbContext db)
        {
            _db = db;
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IGenericRepository<T>;
            }
            var repository = new GenericRepository<T>(_db);
            _repositories.Add(typeof(T), repository);

            return repository;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _db.Database.CommitTransactionAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _db.Database.RollbackTransactionAsync();
        }
        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
