using AuthSample.Tests.MockAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace AuthSample.Tests
{
    public class AuthorizedForOnlyThisRole : DataAttribute
    {
        private readonly string _roleName;

        public AuthorizedForOnlyThisRole(string roleName)
        {
            _roleName = roleName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var testExecutionData = MockRoles.GetAllRoles().Select(role =>
            {
                return new object[] { role, role == _roleName ? true : false };
            });
            return testExecutionData;
        }
    }
}
