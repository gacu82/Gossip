﻿using System;
using System.Threading.Tasks;

namespace Gossip.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitChangesAsync();
    }

    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> CreateAsync();
    }
}
