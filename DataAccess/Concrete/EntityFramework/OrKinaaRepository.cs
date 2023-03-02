
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrKinaaRepository : EfEntityRepositoryBase<OrKinaa, ProjectDbContext>, IOrKinaaRepository
    {
        public OrKinaaRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
