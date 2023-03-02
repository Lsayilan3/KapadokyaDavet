
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrNisanRepository : EfEntityRepositoryBase<OrNisan, ProjectDbContext>, IOrNisanRepository
    {
        public OrNisanRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
