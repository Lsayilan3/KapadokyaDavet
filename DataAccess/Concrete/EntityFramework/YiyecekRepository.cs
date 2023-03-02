
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class YiyecekRepository : EfEntityRepositoryBase<Yiyecek, ProjectDbContext>, IYiyecekRepository
    {
        public YiyecekRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
