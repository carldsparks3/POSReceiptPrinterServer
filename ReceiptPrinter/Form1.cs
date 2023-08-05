using System.Net;

namespace ReceiptPrinter
{
    public partial class Form1 : Form
    {
        System.IO.StreamReader fileToPrint;
        System.Drawing.Font printFont;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new MyListBoxItem(Color.Green, "Validated data successfully"));
            listBox1.Items.Add(new MyListBoxItem(Color.Red, "Failed to validate data"));

            //ReceiptPrinterService rps = new ReceiptPrinterService();
            //rps.PrintSaleByJSON("nothing is done with this yet....");

            //Class1.Print();


        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            MyListBoxItem item = listBox1.Items[e.Index] as MyListBoxItem; 
            
            if (item != null)
            {
                e.Graphics.DrawString( // Draw the appropriate text in the ListBox  
                    item.Message, // The message linked to the item  
                    listBox1.Font, // Take the font from the listbox  
                    new SolidBrush(item.ItemColor), // Set the color   
                    0, // X pixel coordinate  
                    e.Index * listBox1.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.  
                );
            }
            else
            {
                string newitem = listBox1.Items[e.Index].ToString();
                e.Graphics.DrawString( // Draw the appropriate text in the ListBox  
                   newitem, // The message linked to the item  
                   listBox1.Font, // Take the font from the listbox  
                   new SolidBrush(Color.Black), // Set the color   
                   0, // X pixel coordinate  
                   e.Index * listBox1.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.  
               );
            }
        }

        private void StartRESTListener(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            //listener.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // StartRESTListener(new string[1] { "http://localhost:8080/test/" });

            listBox1.Items.Add(new MyListBoxItem(Color.Green, "Starting HTTP listener..."));

            var httpServer = new HttpServer();
            httpServer.Start();
        }

        
        private void printButton_Click(object sender, EventArgs e)
        {
            string printPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileToPrint = new System.IO.StreamReader(printPath + @"\myFile.txt");
            printFont = new System.Drawing.Font("Arial", 10);
            printDocument1.Print();
            fileToPrint.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float yPos = 0f;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (count < linesPerPage)
            {
                line = fileToPrint.ReadLine();
                if (line == null)
                {
                    break;
                }
                yPos = topMargin + count * printFont.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }
            if (line != null)
            {
                e.HasMorePages = true;
            }
        }
    }
}