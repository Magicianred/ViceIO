﻿using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using ViceIO.Data.Common.Models;
using ViceIO.Data.Common.Repositories;

namespace ViceIO.Data.Repositories
{
    public class EfDeletableEntityRepository<TEntity> : EfRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        public EfDeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<TEntity> All()
            => base.All().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking()
            => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted()
            => base.All().IgnoreQueryFilters();

        public IQueryable<TEntity> AllAsNoTrackingWithDeleted()
            => base.AllAsNoTracking().IgnoreQueryFilters();

        public void HardDelete(TEntity entity)
            => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;

            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            this.Update(entity);
        }
    }
}
