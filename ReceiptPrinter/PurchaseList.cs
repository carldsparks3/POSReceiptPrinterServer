using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrinter
{
    internal class PurchaseList
    {
        public String securityToken { get; set; }
        public String kitchen { get; set; }

        public int orderNumber { get; set; }

        public List<PurchaseItem> purchaseItems { get; set; }
    }
}
