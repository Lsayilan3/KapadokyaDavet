
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrSokakLezzetiRepository : EfEntityRepositoryBase<OrSokakLezzeti, ProjectDbContext>, IOrSokakLezzetiRepository
    {
        public OrSokakLezzetiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
