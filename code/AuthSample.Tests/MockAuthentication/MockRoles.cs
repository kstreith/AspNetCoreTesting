using System.Collections.Generic;

namespace AuthSample.Tests.MockAuthentication
{
    public class MockRoles
    {
        public MockRoles()
        {
            Roles.Add("Admin");
        }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
