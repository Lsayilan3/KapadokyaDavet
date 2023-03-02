
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrSunnetRepository : EfEntityRepositoryBase<OrSunnet, ProjectDbContext>, IOrSunnetRepository
    {
        public OrSunnetRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
