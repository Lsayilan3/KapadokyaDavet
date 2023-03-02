﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SpotCategoryyRepository : EfEntityRepositoryBase<SpotCategoryy, ProjectDbContext>, ISpotCategoryyRepository
    {
        public SpotCategoryyRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
