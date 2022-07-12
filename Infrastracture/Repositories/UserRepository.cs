using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Persistence;

namespace Infrastracture.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(MigrationContext context) : base(context)
        {
        }


    }
}
