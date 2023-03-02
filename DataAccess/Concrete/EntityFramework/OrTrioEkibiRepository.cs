
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrTrioEkibiRepository : EfEntityRepositoryBase<OrTrioEkibi, ProjectDbContext>, IOrTrioEkibiRepository
    {
        public OrTrioEkibiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
