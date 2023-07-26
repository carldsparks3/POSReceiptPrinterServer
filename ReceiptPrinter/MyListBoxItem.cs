using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrinter
{
    public class MyListBoxItem
    {
        public MyListBoxItem(Color c, string m)
        {
            ItemColor = c;
            Message = m;
        }
        public Color ItemColor { get; set; }
        public string Message { get; set; }
    }
}
