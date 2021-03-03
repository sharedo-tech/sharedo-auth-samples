using System;
using System.Collections.Generic;

namespace Sharedo.Api.Services.Models
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Provider { get; set; }
        public Guid? UserId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public Guid? OrganisationId { get; set; }
        public string Persona { get; set; }
        public List<string> GlobalPermissions { get; set; }
        public List<string> Roles { get; set; }
        public List<UserClaim> Claims { get; set; }

        public UserInfo()
        {
            GlobalPermissions = new List<string>();
            Roles = new List<string>();
            Claims = new List<UserClaim>();
        }

        public IEnumerable<UserClaim> ToFlatClaims()
        {
            var result = new List<UserClaim>
            {
                new UserClaim("isAuthenticated", IsAuthenticated),
                new UserClaim("userName", UserName),
                new UserClaim("provider", Provider),
                new UserClaim("userId", UserId),
                new UserClaim("clientId", ClientId),
                new UserClaim("clientName", ClientName),
                new UserClaim("firstName", FirstName),
                new UserClaim("surname", Surname),
                new UserClaim("fullName", FullName),
                new UserClaim("orgId", OrganisationId),
                new UserClaim("persona", Persona)
            };

            foreach (var perm in GlobalPermissions)
            {
                result.Add(new UserClaim("globalPermission", perm));
            }

            foreach (var role in Roles)
            {
                result.Add(new UserClaim("role", role));
            }

            result.AddRange(Claims);
            return result;
        }
    }

    public class UserClaim
    {
        public string Claim { get; set; }
        public string Value { get; set; }

        public UserClaim()
        {
        }

        public UserClaim(string claim, object value)
        {
            Claim = claim;
            Value = value?.ToString() ?? "null";
        }
    }
}