
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CikolataRepository : EfEntityRepositoryBase<Cikolata, ProjectDbContext>, ICikolataRepository
    {
        public CikolataRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
