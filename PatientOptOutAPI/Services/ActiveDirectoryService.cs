using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace PatientOptOutAPI.Services
{
    public static class ActiveDirectoryService
    {
        public static string GetUsernameWithoutDomain(this IPrincipal user)
        {
            if (user.Identity.Name.Contains("\\"))
            {
                var split = user.Identity.Name.Split('\\');
                return split[split.Length - 1];
            }

            return user.Identity.Name;
        }

        public static bool CheckAccess(this IPrincipal user, string groupName)
        {
            if (user != null)
            {
                var users = GetUserSamNamesFromADGroup(groupName);
                if (users != null)
                {
                    var userName = GetUserSAMName(user);
                    if (userName != null)
                    {
                        return users.Contains(userName.ToUpper());
                    }
                }
            }
            return false;
        }

        private static string GetDomainMainName()
        {  
            using (var domainRoot = new DirectoryEntry("LDAP://rootDSE"))
            {
                using (var domainLdap = new DirectoryEntry("LDAP://" + domainRoot.Properties["defaultNamingContext"].Value))
                {
                    return domainLdap.Name.Split('=')[1];
                }
            }  
        }

        private static string GetUserSAMName(IPrincipal user)
        {
            using (var pc = new PrincipalContext(ContextType.Domain, GetDomainMainName()))
            {
                var principal = (ClaimsPrincipal)user;
                if (principal.Identity.Name != "")
                {
                    var up = UserPrincipal.FindByIdentity(pc, principal.Identity.Name);
                    return up.SamAccountName.ToUpper();
                }

                return null;
            }
        }

        private static string[] GetUserSamNamesFromADGroup(string groupName)
        {
            using (var pc = new PrincipalContext(ContextType.Domain, GetDomainMainName()))
            {
                using (var gp = GroupPrincipal.FindByIdentity(pc, groupName))
                {
                    return gp?.GetMembers(true).Select(u => u.SamAccountName.ToString().ToUpper()).ToArray();
                }
            }
        }
    }
}
