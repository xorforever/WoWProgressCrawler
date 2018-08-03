using System;
using System.Net;
using NHttp;
using System.Configuration;
using WoWProgressCrawler.Core.APIRequestHandlers;
using System.IO;
using System.Web.Script.Serialization;

namespace WoWProgressCrawler.Core
{
    public class EmbeddedHTTP :IDisposable
    {
        private HttpServer server;

        public EmbeddedHTTP()
        {
            server = new HttpServer();
            server.EndPoint = new IPEndPoint
                (IPAddress.Parse(ConfigurationManager.AppSettings.Get("EmbeddedHTTP_ListenAddr")),
                Convert.ToInt32(ConfigurationManager.AppSettings.Get("EmbeddedHTTP_ListenPort")));
            server.RequestReceived += Server_RequestReceived;
        }

        private void Server_RequestReceived(object sender, HttpRequestEventArgs e)
        {
            APIBase b = new APIBase();
            try
            {
                b.RunMethod(e);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void Start()
        {
            server.Start();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    server.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EmbeddedHTTP() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
