
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class MuzikRepository : EfEntityRepositoryBase<Muzik, ProjectDbContext>, IMuzikRepository
    {
        public MuzikRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
