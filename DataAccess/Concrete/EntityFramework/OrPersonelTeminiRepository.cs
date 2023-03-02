
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrPersonelTeminiRepository : EfEntityRepositoryBase<OrPersonelTemini, ProjectDbContext>, IOrPersonelTeminiRepository
    {
        public OrPersonelTeminiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
