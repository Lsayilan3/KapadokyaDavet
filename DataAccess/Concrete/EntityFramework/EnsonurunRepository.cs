
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EnsonurunRepository : EfEntityRepositoryBase<Ensonurun, ProjectDbContext>, IEnsonurunRepository
    {
        public EnsonurunRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
