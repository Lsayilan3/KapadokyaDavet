
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SliderRepository : EfEntityRepositoryBase<Slider, ProjectDbContext>, ISliderRepository
    {
        public SliderRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
