
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrBabyRepository : EfEntityRepositoryBase<OrBaby, ProjectDbContext>, IOrBabyRepository
    {
        public OrBabyRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
