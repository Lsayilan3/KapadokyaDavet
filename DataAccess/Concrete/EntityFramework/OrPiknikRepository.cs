
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrPiknikRepository : EfEntityRepositoryBase<OrPiknik, ProjectDbContext>, IOrPiknikRepository
    {
        public OrPiknikRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
