using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class DefaultCollectionTypeRepository : Repository<DefaultCollectionType>, IDefaultCollectionTypeRepository
    {
        public DefaultCollectionTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
