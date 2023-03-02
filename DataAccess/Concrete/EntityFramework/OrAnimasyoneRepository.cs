
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrAnimasyoneRepository : EfEntityRepositoryBase<OrAnimasyone, ProjectDbContext>, IOrAnimasyoneRepository
    {
        public OrAnimasyoneRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
