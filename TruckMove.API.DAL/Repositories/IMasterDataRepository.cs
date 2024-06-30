﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories
{
    public interface IMasterDataRepository
    {
        Task<List<Role>> GetAllRoles();
        Task<List<User>> GetUsersByRoleAsync(int roleId);

        Task<List<HookupType>> GetAllRolesHookupTypes();
    }
}
