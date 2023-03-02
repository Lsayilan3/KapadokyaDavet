
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrEkipmanRepository : EfEntityRepositoryBase<OrEkipman, ProjectDbContext>, IOrEkipmanRepository
    {
        public OrEkipmanRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
