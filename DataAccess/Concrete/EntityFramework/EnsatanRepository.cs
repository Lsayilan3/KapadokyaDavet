
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EnsatanRepository : EfEntityRepositoryBase<Ensatan, ProjectDbContext>, IEnsatanRepository
    {
        public EnsatanRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
