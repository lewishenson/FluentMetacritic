using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace FluentMetacritic.Net
{
    public class WebClient : IWebClient
    {
        private const string ChromeUserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.11 Safari/537.36";

        private readonly string _userAgent;

        public WebClient()
            : this(ChromeUserAgent)
        {
        }

        public WebClient(string userAgent)
        {
            _userAgent = userAgent;
        }

        public string UserAgent
        {
            get
            {
                return _userAgent;
            }
        }

        public string GetContent(string uri)
        {
            var request = CreateRequest(uri);

            return GetContent(request);
        }

        private HttpWebRequest CreateRequest(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.UserAgent = UserAgent;

            return request;
        }

        private string GetContent(HttpWebRequest request)
        {
            string content = null;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                content = streamReader.ReadToEnd().Trim();
                            }
                        }
                    }

                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }

            return content;
        }
    }
}