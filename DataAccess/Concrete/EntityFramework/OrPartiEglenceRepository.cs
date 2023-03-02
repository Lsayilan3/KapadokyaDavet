
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrPartiEglenceRepository : EfEntityRepositoryBase<OrPartiEglence, ProjectDbContext>, IOrPartiEglenceRepository
    {
        public OrPartiEglenceRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
