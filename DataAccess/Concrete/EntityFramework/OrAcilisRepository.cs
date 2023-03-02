﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrAcilisRepository : EfEntityRepositoryBase<OrAcilis, ProjectDbContext>, IOrAcilisRepository
    {
        public OrAcilisRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
