using FarmApp.Domain.Core.Entities;
using FarmApp.Domain.Interfaces.Repositories;
using FarmApp.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Infrastructure.Data.Repositoreies
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FarmAppContext context)
        {
            farmAppContext = context;
        }
    }
}
