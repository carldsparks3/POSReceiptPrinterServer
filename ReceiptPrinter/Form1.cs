using System.Net;

namespace ReceiptPrinter
{
    public partial class Form1 : Form
    {
        System.IO.StreamReader fileToPrint;
        System.Drawing.Font printFont;
        public Stream purchaseList;

        private String serverIP;
        private String serverPort;
        private String appToken;

        public Form1()
        {
            InitializeComponent();
            parseConfigFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new MyListBoxItem(Color.Green, "Validated data successfully"));
            listBox1.Items.Add(new MyListBoxItem(Color.Red, "Failed to validate data"));
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new MyListBoxItem(Color.Green, "Starting HTTP listener..."));

            var httpServer = new HttpServer();
            httpServer.Start(serverIP, serverPort);

            listBox1.Items.Add(new MyListBoxItem(Color.Green, "HTTP Server Listening on 'http://" + serverIP + ":" + serverPort + "/'"));
        }


        private void printButton_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ReceiptPrinterService printService = new ReceiptPrinterService();
            printService.SetupPrinterDevice(e);
            printService.PrintSaleByJSON(purchaseList);
        }

        private void setAppToken(String appToken)
        {
            this.appToken = appToken;
        }

        public String getAppToken()
        {
            return appToken;
        }

        private void parseConfigFile()
        {
            if (File.Exists(@"config.cfg"))
            {
                string[] configLines = File.ReadAllLines(@"config.cfg");
                foreach(string line in configLines)
                {
                    if (line.Contains("httpServerIP"))
                    {
                        String configVal = line.Split('=')[1];
                        serverIP = configVal;
                    }

                    if (line.Contains("httpServerPort"))
                    {
                        String configVal = line.Split('=')[1];
                        serverPort = configVal;
                    }

                    if (line.Contains("securityToken"))
                    {
                        String configVal = line.Split('=')[1];
                        setAppToken(configVal);
                    }
                }
            } else
            {
                String[] defaultConfig = new string[3];
                defaultConfig.Append("httpServerIP=localhost");
                defaultConfig.Append("httpServerPort=8080");
                defaultConfig.Append("securityToken=rh3u5124ywjwtj225725");

                // Populate some defaults
                File.WriteAllLines(@"config.cfg", defaultConfig);

                button2.Enabled = false;

                MessageBox.Show("A valid config file named 'config.cfg' is required to populate the application configuration. The file was not found so one has been created for you. You should close this program, update the config, and run this application again.", "Config FIle Note Found");
            }
        }
    }
}