
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrPartiStoreRepository : EfEntityRepositoryBase<OrPartiStore, ProjectDbContext>, IOrPartiStoreRepository
    {
        public OrPartiStoreRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
