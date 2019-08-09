using System.Collections.Generic;

namespace AuthSample.Tests.MockAuthentication
{
    public class MockRoles
    {
        public static List<string> GetAllRoles()
        {
            var roles = new List<string>
            {
                "GetValue",
                "GetValues",
                "PostValue"
            };
            return roles;
        }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
