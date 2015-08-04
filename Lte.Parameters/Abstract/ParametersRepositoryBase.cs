using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Concrete;

namespace Lte.Parameters.Abstract
{
    public abstract class ParametersRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<EFParametersContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ParametersRepositoryBase(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public abstract class ParametersRepositoryBase<TEntity> : ParametersRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ParametersRepositoryBase(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
