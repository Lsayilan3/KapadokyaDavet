
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrSirketEglenceRepository : EfEntityRepositoryBase<OrSirketEglence, ProjectDbContext>, IOrSirketEglenceRepository
    {
        public OrSirketEglenceRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
