
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class LazerRepository : EfEntityRepositoryBase<Lazer, ProjectDbContext>, ILazerRepository
    {
        public LazerRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
