using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Common.Models;

namespace Monitor.Common.Extensions
{
    public static class ClaimExtension
    {
        public static UserInfo GetUser(this IEnumerable<Claim> claims)
            => new UserInfo
            {
                Id = claims.GetUserId().Value,
                Role = claims.GetRole(),
                CompanyId = claims.GetCompany(),
                IsAnonymuos = claims.IsAnonymuos()
            };

        public static int? GetUserId(this IEnumerable<Claim> claims) => claims?.Any(z => z.Type == Constants.USER_ID) == true ?
            (int?)int.Parse(claims.Single(z => z.Type == Constants.USER_ID).Value) : null;

        public static int? GetCompany(this IEnumerable<Claim> claims)
        {
            if (claims.IsAnonymuos())
            { return null; }
            else if (!claims.Any(z => z.Type == Constants.COMPANY))
            { throw new CustomException("You have to be assigned to Company."); }

            return int.Parse(claims.Single(z => z.Type == Constants.COMPANY).Value);
        }

        public static RoleCode GetRole(this IEnumerable<Claim> claims) => claims?.Any(z => z.Type == Constants.USER_ROLE) == true ?
            (RoleCode)Enum.Parse(typeof(RoleCode), claims.Single(z => z.Type == Constants.USER_ROLE).Value) :
            throw new CustomException("User doesn't have role.");

        public static bool IsAnonymuos(this IEnumerable<Claim> claims) =>
            bool.Parse(claims.Single(z => z.Type == Constants.IS_ANONYMUOS).Value);
    }
}
