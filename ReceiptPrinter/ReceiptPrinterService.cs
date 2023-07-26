using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ReceiptPrinter
{
    internal class ReceiptPrinterService
    {
        private SerialPrinter printer = null;

        public ReceiptPrinterService() {
            SetupPrinterDevice();
        }

        public void SetupPrinterDevice()
        {
            // A list of all possible printer types are included in this file. Modify this method as needed to support your printer.

            // Ethernet or WiFi (This uses an Immediate Printer, no live paper status events, but is easier to use)
            //var hostnameOrIp = "192.168.1.50";
            //var port = 9100;
            //var printer = new ImmediateNetworkPrinter(new ImmediateNetworkPrinterSettings() { ConnectionString = $"{hostnameOrIp}:{port}", PrinterName = "TestPrinter" });

            // USB, Bluetooth, or Serial
            var printer = new SerialPrinter(portName: "COM5", baudRate: 115200);

            // Linux output to USB / Serial file
            //var printer = new FilePrinter(filePath: "/dev/usb/lp0");

            // Samba
            //var printer = new SambaPrinter(tempFileBasePath: @"C:\Temp", filePath: "\\computer\printer");
        }

        public void PrintSaleByJSON(string json)
        {
            var list = JsonConvert.DeserializeObject<List<TransactionItem>>(json);

            var e = new EPSON();
            printer.Write( // or, if using and immediate printer, use await printer.WriteAsync
              ByteSplicer.Combine(
                e.CenterAlign(),
                e.PrintImage(File.ReadAllBytes("images/pd-logo-300.png"), true),
                e.PrintLine(""),
                e.SetBarcodeHeightInDots(360),
                e.SetBarWidth(BarWidth.Default),
                e.SetBarLabelPosition(BarLabelPrintPosition.None),
                e.PrintBarcode(BarcodeType.ITF, "0123456789"),
                e.PrintLine(""),
                e.PrintLine("B&H PHOTO & VIDEO"),
                e.PrintLine("420 NINTH AVE."),
                e.PrintLine("NEW YORK, NY 10001"),
                e.PrintLine("(212) 502-6380 - (800)947-9975"),
                e.SetStyles(PrintStyle.Underline),
                e.PrintLine("www.bhphotovideo.com"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(""),
                e.LeftAlign(),
                e.PrintLine("Order: 123456789        Date: 02/01/19"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("1   TRITON LOW-NOISE IN-LINE MICROPHONE PREAMP"),
                e.PrintLine("    TRFETHEAD/FETHEAD                        89.95         89.95"),
                e.PrintLine("----------------------------------------------------------------"),
                e.RightAlign(),
                e.PrintLine("SUBTOTAL         89.95"),
                e.PrintLine("Total Order:         89.95"),
                e.PrintLine("Total Payment:         89.95"),
                e.PrintLine(""),
                e.LeftAlign(),
                e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
                e.PrintLine("SOLD TO:                        SHIP TO:"),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
                e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
                e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
                e.PrintLine("  (123)456-7890                   (123)456-7890"),
                e.PrintLine("  CUST: 87654321"),
                e.PrintLine(""),
                e.PrintLine("")
              )
            );
        }

        
    }
}
