﻿namespace CommonNews.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Bytes2you.Validation;
    using Contracts;
    using Models.Contracts;

    public class EfRepository<T> : IEfRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        public EfRepository(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, "MsSqlDbContext").IsNull().Throw();

            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; }

        private MsSqlDbContext Context { get; }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public T GetById(object id)
        {
            Guard.WhenArgument(id, "GetById").IsNull().Throw();

            var item = this.DbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }

            return item;
        }

        public void Add(T entity)
        {
            Guard.WhenArgument(entity, "Add entity").IsNull().Throw();

            var entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            Guard.WhenArgument(entity, "AddOrUpdate entity").IsNull().Throw();

            this.DbSet.AddOrUpdate(entity);

            //var entry = this.Context.Entry(entity);

            //if (entry.State == EntityState.Detached)
            //{
            //    this.DbSet.AddOrUpdate(entity);
            //}

            //entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Guard.WhenArgument(entity, "Delete entity").IsNull().Throw();

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }
    }
}