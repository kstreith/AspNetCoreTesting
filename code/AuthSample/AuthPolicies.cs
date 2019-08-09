using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSample
{
    public static class AuthPolicies
    {
        public static void AddToOptions(AuthorizationOptions authOptions)
        {
            authOptions.AddPolicy("GetValuesPolicy",
                configure => configure.RequireRole("GetValues"));
            authOptions.AddPolicy("GetValuePolicy",
                configure => configure.RequireRole("GetValue"));
            authOptions.AddPolicy("PostValuePolicy",
                configure => configure.RequireRole("PostValue"));
        }
    }
}
