
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class GalleryTwoRepository : EfEntityRepositoryBase<GalleryTwo, ProjectDbContext>, IGalleryTwoRepository
    {
        public GalleryTwoRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
