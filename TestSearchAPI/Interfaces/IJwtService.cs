﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
#pragma warning disable
namespace TestSearchAPI.Interfaces
{
    public interface IJwtService
    {
        string GenerateSecurityToken(UserDTOExtension user);
    }
}
