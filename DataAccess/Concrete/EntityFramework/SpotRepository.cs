
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SpotRepository : EfEntityRepositoryBase<Spot, ProjectDbContext>, ISpotRepository
    {
        public SpotRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
