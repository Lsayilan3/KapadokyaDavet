
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrCikolataRepository : EfEntityRepositoryBase<OrCikolata, ProjectDbContext>, IOrCikolataRepository
    {
        public OrCikolataRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
