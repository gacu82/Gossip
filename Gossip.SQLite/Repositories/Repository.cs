﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gossip.Domain.Models;
using Gossip.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity, IAggregateRoot
    {
        private readonly GossipContext _context;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private DbSet<T> _entities;

        protected Repository(GossipContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T Get(int id)
        {
            return _entities.SingleOrDefault(p => p.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}