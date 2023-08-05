using System;
using System.Net;
using System.IO;
using System.Collections;
using ReceiptPrinter;
using System.Windows.Forms;

public class HttpServer
{
    public int Port = 8080;

    private HttpListener _listener;

    public void Start()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add("http://localhost:" + Port.ToString() + "/");
        _listener.Start();
        Receive();
    }

    public void Stop()
    {
        _listener.Stop();
    }

    private void Receive()
    {
        _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
    }

    private void ListenerCallback(IAsyncResult result)
    {
        if (_listener.IsListening)
        {
            var context = _listener.EndGetContext(result);
            var request = context.Request;

            // do something with the request
            IEnumerator test = Application.OpenForms.GetEnumerator();
            if (test != null)
            {
                test.MoveNext();
                if (test.Current is ReceiptPrinter.Form1)
                {
                    Form1 form1= (Form1)test.Current;
                    //form1.listBox1.Items.Add(new MyListBoxItem(Color.Green, $"{request.Url}"));

                    form1.listBox1.Invoke((MethodInvoker)delegate {
                        // Running on the UI thread
                        form1.listBox1.Items.Add(new MyListBoxItem(Color.Green, $"{request.Url}"));
                    });

                    //MessageBox.Show($"{request.Url}", "HTTP Listener");
                }
            }

            Console.WriteLine($"{request.Url}");

            Receive();
        }
    }
}
