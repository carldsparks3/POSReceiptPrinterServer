using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ReceiptPrinter
{
    internal class ReceiptPrinterService
    {
        System.Drawing.Font printFont = new System.Drawing.Font("Arial", 12); // Font used for the item list
        int maxLineLength = 50; // We're saying that each printed receipt line should be a max of 50 chars

        float yPos, leftMargin, topMargin, linesPerPage;
        int count;
        string line;
        System.Drawing.Printing.PrintPageEventArgs printPageEvts;

        public ReceiptPrinterService() {
            //SetupPrinterDevice();
        }

        // Code that needs to run when the Print setup is initialized. 
        public void SetupPrinterDevice(System.Drawing.Printing.PrintPageEventArgs e)
        {
            printPageEvts = e;
            yPos = 0f;
            count = 0;
            leftMargin = 10;//e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top;
            line = null;
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);

            printFont = new System.Drawing.Font("Arial", 12);
        }

        // Includes a custom defined header template for the Receipt
        private void ReceiptHeader(String boldHeading, int orderNumber)
        {
            if (boldHeading != null)
            {
                printPageEvts.Graphics.DrawString(boldHeading, new System.Drawing.Font("Arial", 55), Brushes.Black, leftMargin - 10, yPos, new StringFormat());
                yPos = topMargin + count * printFont.GetHeight(printPageEvts.Graphics);
                count++;
            }

            printPageEvts.Graphics.DrawString("Saint Peter & Paul Church Picnic", new System.Drawing.Font("Arial", 12), Brushes.Black, leftMargin, yPos, new StringFormat());
            yPos = topMargin + count * printFont.GetHeight(printPageEvts.Graphics);
            count++;

            DateTime saleDateTime = DateTime.Now;
            String saleDateTimeStr = saleDateTime.ToString("MMMM dd, yyyy hh:mm tt");

            printPageEvts.Graphics.DrawString(saleDateTimeStr, new System.Drawing.Font("Arial", 12), Brushes.Black, leftMargin, yPos, new StringFormat());
            yPos = topMargin + count * printFont.GetHeight(printPageEvts.Graphics);
            count++;

            printPageEvts.Graphics.DrawString("", new System.Drawing.Font("Arial", 18), Brushes.Black, leftMargin, yPos, new StringFormat());
            yPos = topMargin + count * printFont.GetHeight(printPageEvts.Graphics);
            count++;

            printPageEvts.Graphics.DrawString("Order Number: " + orderNumber, new System.Drawing.Font("Arial", 16), Brushes.Black, leftMargin, yPos, new StringFormat());
            yPos = topMargin + count * printFont.GetHeight(printPageEvts.Graphics);
            count++;
        }

        public void PrintSaleByJSON(Stream purchaseRequestBody)
        {
            // Parse the purchase request body
            var serializer = new JsonSerializer();
            PurchaseList purchaseOrder;

            using (var sr = new StreamReader(purchaseRequestBody))
            {
                string text = sr.ReadToEnd();

                purchaseOrder = (PurchaseList)JsonConvert.DeserializeObject<PurchaseList>(text);
            }

            if (purchaseOrder != null)
            {
                // Validate that the request is adequately authenticated. Check for the correct security token.
                // We don't want to accept just any old request. Only accept requests we can confirm.
                SecurityService securityService = new SecurityService();
                securityService.authenticateWithAPISecurityToken(purchaseOrder.securityToken, getAppSecurityToken());

                if (securityService.IsAuthenticated)
                {
                    ReceiptHeader(purchaseOrder.kitchen, purchaseOrder.orderNumber);

                    // Loop through the parsed JSON request body to build out the receipt body
                    if (purchaseOrder.purchaseItems != null)
                    {
                        foreach (PurchaseItem item in purchaseOrder.purchaseItems)
                        {
                            line = item.item + getDotString(item.item.Length + item.itemCount + 3) + item.itemCount; // 3 is the count of our control chars " ", " ", and "x"
                            if (line == null)
                            {
                                break;
                            }
                            yPos = topMargin + count * printFont.GetHeight(printPageEvts.Graphics);
                            printPageEvts.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                            count++;
                        }
                        if (line != null)
                        {
                            printPageEvts.HasMorePages = true;
                        }
                    }
                }
            }
        }

        // Method that returns a String with a variable length of "..." characters. 
        // Resulting string length is determined by subtracting the current string length from the max line length
        private String getDotString(int currStringLength)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            for(int i = currStringLength; i < maxLineLength; i++)
            {
                sb.Append(".");
            }
            sb.Append(" x");

            return sb.ToString();
        }


        private String getAppSecurityToken()
        {
            IEnumerator test = Application.OpenForms.GetEnumerator();
            if (test != null)
            {
                test.MoveNext();
                if (test.Current is ReceiptPrinter.Form1)
                {
                    Form1 form1 = (Form1)test.Current;
                    if (form1 != null)
                    {
                        return form1.getAppToken();
                    }
                }
            }

            return null;
        }
    }
}
