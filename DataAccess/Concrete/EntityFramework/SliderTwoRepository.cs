
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SliderTwoRepository : EfEntityRepositoryBase<SliderTwo, ProjectDbContext>, ISliderTwoRepository
    {
        public SliderTwoRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
