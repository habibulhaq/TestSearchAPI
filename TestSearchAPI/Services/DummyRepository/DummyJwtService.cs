using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Interfaces;
#pragma warning disable
namespace TestSearchAPI.Services.DummyRepository
{
    public class DummyJwtService : IJwtService
    {
        public string GenerateSecurityToken(UserDTOExtension user)
        {
            return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiMSIsImVtYWlsIjoiYWJjQGdtYWlsLmNvbSIsInVuaXF1ZV9uYW1lIjoiSGFiaWIiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MDY3NjcxOTMsImV4cCI6MTcwNjg1MzU5MywiaWF0IjoxNzA2NzY3MTkzfQ.NxntJgpqZ232tR0m6bOeM57zaYE3Ycol-9d2L0Nj56k";
        }
    }
}
