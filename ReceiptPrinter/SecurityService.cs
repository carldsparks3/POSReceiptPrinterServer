using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrinter
{
    internal class SecurityService
    {
        public Boolean IsAuthenticated { get; set; }

        // Very basic example of a security token comparison. This assumes that there is only 1 security token established and the
        // token established will be handled with care. In this case, treat the security token like a password.
        public bool authenticateWithAPISecurityToken(String requestToken, String appToken)
        {
            if (requestToken.Equals(appToken))
            {
                IsAuthenticated = true;
                return true;
            } else
            {
                IsAuthenticated = false;
                return false;
            }
        }
    }
}
