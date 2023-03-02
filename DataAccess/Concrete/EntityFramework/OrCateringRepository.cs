
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrCateringRepository : EfEntityRepositoryBase<OrCatering, ProjectDbContext>, IOrCateringRepository
    {
        public OrCateringRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
