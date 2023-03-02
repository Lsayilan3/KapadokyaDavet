
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrKokteylRepository : EfEntityRepositoryBase<OrKokteyl, ProjectDbContext>, IOrKokteylRepository
    {
        public OrKokteylRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
