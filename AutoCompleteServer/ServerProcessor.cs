using System;
using System.Net;
using System.Text;

namespace AutoCompleteServer
{
    class ServerProcessor
    {
        readonly HttpListener _listener;
        public ServerProcessor(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(string.Format("http://*:{0}/", port));
            Console.WriteLine("Listening on port {0}...", port);
        }

        public void Start()
        {
            _listener.Start();

            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    Console.WriteLine("{0} request was caught: {1}",
                        request.HttpMethod, request.Url);
                    var urlParts = request.Url.AbsolutePath.Replace("%20", "/").Split('/');
                    string responseInfo;

                    try
                    {
                        responseInfo = CompleteUtils.GetComlete(urlParts[3]);
                        response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    catch (Exception e)
                    {
                        responseInfo = "Request error! " + e.Message;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }

                    byte[] b = Encoding.UTF8.GetBytes(responseInfo);
                    context.Response.ContentLength64 = b.Length;
                    context.Response.OutputStream.Write(b, 0, b.Length);
                    context.Response.Headers.Add(HttpResponseHeader.ContentEncoding, "ASCII");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Network trouble\r\n" + exception.Message);
                }
            }
        }

        public void Stop()
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
        }

        ~ServerProcessor()
        {
            Stop();
        }
    }
}