using AIO_R.ABC_Buy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Security;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIO_R
{
   
    internal class Request : NetInterface
    {
        public string responseUrl { get; set; }
        public CookieCollection siteCookie { get; set; }
        WebRequestInfo webRequestInfo { get; set; }
        public string Get(WebRequestInfo webRequestInfo)
        {
        A: ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webRequestInfo.url);
            byte[] contentByte = Encoding.UTF8.GetBytes(webRequestInfo.info);
            req.Proxy = Proxy(webRequestInfo.proxy);
            if (webRequestInfo.isUseCookie)
            {
                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(siteCookie);
                req.CookieContainer = cookieContainer;
            }
            req.Timeout = webRequestInfo.timeout;
            req.ContentLength = contentByte.Length;
            req.Accept = "application/json";
            req.Headers.Add(webRequestInfo.webHeader);
            req.UserAgent = webRequestInfo.userAgent;
            req.AllowAutoRedirect = webRequestInfo.autoRedirect;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    responseUrl = response.ResponseUri.ToString();
                    if (webRequestInfo.isUseCookie && response.Headers["Set-Cookie"] != null)
                    {
                        RegexCookie(response.Headers["Set-Cookie"].ToString(), webRequestInfo.host);
                    }
                    StreamReader readhtmlStream = null;
                    string readtoend = null;
                    if (response.ContentEncoding == "gzip")
                    {
                        Stream tokenStream = response.GetResponseStream();
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        readhtmlStream = new StreamReader(new GZipStream(tokenStream, CompressionMode.Decompress), Encoding.UTF8);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    else if (response.ContentEncoding == "br")
                    {
                        Stream tokenStream = response.GetResponseStream();
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        readhtmlStream = new StreamReader(new BrotliStream(tokenStream, CompressionMode.Decompress), Encoding.UTF8);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    else
                    {
                        Stream tokenStream = response.GetResponseStream();
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        readhtmlStream = new StreamReader(tokenStream, Encoding.UTF8);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    webRequestInfo.webHeader.Clear();
                    return readtoend;
                }

            }
            catch (WebException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Task" + Thread.CurrentThread.ManagedThreadId.ToString() + "]" + "[" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "]" + ex.Status);
                webRequestInfo.webHeader.Clear();
                goto A;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Task" + Thread.CurrentThread.ManagedThreadId.ToString() + "]" + "[" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "]" + ex.Message);
                webRequestInfo.webHeader.Clear();
                goto A;
            }
        }
        public string Post(WebRequestInfo webRequestInfo)
        {
        A:  ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webRequestInfo.url);
            byte[] contentByte = Encoding.UTF8.GetBytes(webRequestInfo.info);
            req.Proxy = Proxy(webRequestInfo.proxy);
            if (webRequestInfo.isUseCookie)
            {
                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(siteCookie);
                req.CookieContainer = cookieContainer;
            }
            req.Timeout = webRequestInfo.timeout;
            req.Method = "POST";
            req.ContentLength = contentByte.Length;
            req.Accept = "application/json";
            req.Headers.Add(webRequestInfo.webHeader);
            req.UserAgent = webRequestInfo.userAgent;
            req.AllowAutoRedirect = webRequestInfo.autoRedirect;
            req.ContentType = webRequestInfo.contentType;
            Stream webstream = req.GetRequestStream();
            webstream.Write(contentByte, 0, contentByte.Length);
            webstream.Close();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    responseUrl = response.ResponseUri.ToString();
                    if (webRequestInfo.isUseCookie && response.Headers["Set-Cookie"] != null)
                    {
                        RegexCookie(response.Headers["Set-Cookie"].ToString(), webRequestInfo.host);
                    }
                    StreamReader readhtmlStream = null;
                    string readtoend = null;
                    if (response.ContentEncoding == "gzip")
                    {
                        Stream tokenStream = response.GetResponseStream();
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        readhtmlStream = new StreamReader(new GZipStream(tokenStream, CompressionMode.Decompress), Encoding.UTF8);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    else if (response.ContentEncoding == "br")
                    {
                        Stream tokenStream = response.GetResponseStream();
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        readhtmlStream = new StreamReader(new BrotliStream(tokenStream, CompressionMode.Decompress), Encoding.UTF8);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    else
                    {
                        Stream tokenStream = response.GetResponseStream();
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        readhtmlStream = new StreamReader(tokenStream, Encoding.UTF8);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    webRequestInfo.webHeader.Clear();
                    return readtoend;
                }

            }
            catch (WebException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Task" + Thread.CurrentThread.ManagedThreadId.ToString() + "]" + "[" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "]" + ex.Status);
                webRequestInfo.webHeader.Clear();
                goto A;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Task" + Thread.CurrentThread.ManagedThreadId.ToString() + "]" + "[" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "]" + ex.Message);
                webRequestInfo.webHeader.Clear();
                goto A;
            }
        }
        public void RegexCookie(string cookie, string host)
        {
            Regex re = new Regex("([^;,]+)=([^;,]+);", RegexOptions.IgnoreCase);
            Regex re2 = new Regex("^[^=]*(?==)|(?<==).+$", RegexOptions.IgnoreCase);
            foreach (Match m in re.Matches(cookie))
            {
                if (m.Value.Contains("domain", StringComparison.OrdinalIgnoreCase) || m.Value.Contains("path", StringComparison.OrdinalIgnoreCase) || m.Value == " " || m.Value.Contains("max-age", StringComparison.OrdinalIgnoreCase))
                {
                }
                else
                {
                    Cookie c = new Cookie(re2.Matches(m.Value)[0].Value.Replace(" ", ""), re2.Matches(m.Value)[1].Value.Replace(" ", "").Replace(";", ""), "/", host);
                    bool exist = false;
                    for (int i = 0; i < siteCookie.Count; i++)
                    {
                        if (siteCookie[i].Name == c.Name)
                        {
                            siteCookie[i].Value = re2.Matches(m.Value)[1].Value.Replace(" ", "").Replace(";", "");//有问题，对照atoms//已修改好
                            exist = true;
                        }
                    }
                    if (exist == false)
                    {
                        siteCookie.Add(c);
                    }

                }
            }

        }
        public WebProxy Proxy(string proxy)
        {
            WebProxy wp = new WebProxy();
            string[] proxygroup = proxy.Split(":");
            if (proxygroup.Length == 2)
            {
                wp.Address = new Uri("http://" + proxygroup[0] + ":" + proxygroup[1] + "/");

            }
            else if (proxygroup.Length == 4)
            {
                wp.Address = new System.Uri("http://" + proxygroup[0] + ":" + proxygroup[1] + "/");
                wp.Credentials = new NetworkCredential(proxygroup[2], proxygroup[3]);
            }
            return wp;
        }
        public bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
