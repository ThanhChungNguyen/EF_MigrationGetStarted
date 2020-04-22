using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication8.Models
{
    public class BaseDbContext : DbContext 
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> contextOptions, IAuthorizationService authorizationService) : base(contextOptions)
        {
            _authorizationService = authorizationService;
        }
    }
}
