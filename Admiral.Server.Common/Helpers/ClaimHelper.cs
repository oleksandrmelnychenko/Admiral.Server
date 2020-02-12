using System.Linq;
using System.Security.Claims;

namespace Admiral.Server.Common.Helpers {
    public static class ClaimHelper {
        public static long GetUserId(ClaimsPrincipal currentUser) {
            Claim claim = currentUser.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
            return long.Parse(claim.Value);
        }
    }
}
