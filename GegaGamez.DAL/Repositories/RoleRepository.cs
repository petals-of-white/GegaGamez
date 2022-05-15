using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;
public class RoleRepository : Repository<Role>, IRoleRepository
{

    public RoleRepository(DbContext dbContext):base(dbContext)
    {

    }
}
