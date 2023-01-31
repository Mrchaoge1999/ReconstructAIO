using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AIO_R
{
     class BotProxy: Profile
    {
        private static readonly BotProxy _instance = new BotProxy();

        private GlobalInfo globalInfo;

        public List<string> proxy = new List<string>();
        private BotProxy() {
            globalInfo = new GlobalInfo();
            notify += new NotifyEventHandler(globalInfo.UpdateProxy);
        }
        public static BotProxy Instance { get { return _instance; } }
        private void ReadProxy()
        {
            while (true)
            {
                try
                {
                    proxy = new List<string>(File.ReadAllLines(Environment.CurrentDirectory + "\\proxy.txt"));
                    if (notify != null) notify(this);
                }
                catch (IOException)
                {
                    Console.WriteLine("Error to Read Proxy.txt");
                }
                Thread.Sleep(1000);
            }
        }
        public void RegisterProxyThread()
        {
            Task proxyAddTask = Task.Factory.StartNew(ReadProxy);
        }
    }
}
