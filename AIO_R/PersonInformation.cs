using CsvHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace AIO_R
{
    class PersonInformation : Profile
    {
        private static readonly PersonInformation _personInformationInstance = new PersonInformation();

        public static ManualResetEvent defaultWriteMre = new ManualResetEvent(true);

        private readonly ConcurrentQueue<Dictionary<string, BotTask>> _que = new ConcurrentQueue<Dictionary<string, BotTask>>();

        private readonly ManualResetEvent _mre = new ManualResetEvent(false);

        public static PersonInformation Instance { get { return _personInformationInstance; } }

        public string path = Environment.CurrentDirectory + "\\task.csv";

        private string successPath = Environment.CurrentDirectory + "\\CheckSuccess.csv";
        
        public List<BotTask> listTask { get; set; }
        private PersonInformation()
        {
            ReadCsv<BotTask>();
        }

        private void ReadCsv<T>()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))//CultureInfo.InvariantCulture
                    {
                        listTask = csv.GetRecords<BotTask>().ToList();
                    }
                }
            }
            GlobalInfo.listTask = listTask;
            GlobalInfo.taskNumber = listTask.Count;
        }
        private void writebecsv()
        {
            while (true)
            {
                _mre.WaitOne();
                Dictionary<string, BotTask> dic;
                while (_que.Count > 0 && _que.TryDequeue(out dic))
                {
                    bool isexist = false;
                    foreach (var i in dic)
                    {
                        try
                        {
                            BotTask whereClass = listTask.FirstOrDefault(t => t.email == i.Key);
                            if (whereClass != null)
                            {
                                whereClass = i.Value;
                                isexist = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    while (isexist)
                    {
                        try
                        {
                            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "task.csv")))
                            //  using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "task.csv"), false,Encoding.GetEncoding("gb2312")))
                            {
                                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                                {
                                    csv.WriteRecords(listTask);
                                }
                            }
                        }
                        catch (IOException ex)
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    defaultWriteMre.Set();
                    _mre.Reset();
                }
            }
        }
        public void writesuccess(List<CheckSuccess> cs)
        {
            using (var writer = new StreamWriter(successPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(cs);
                }
            }
        }
        public void writeque(Dictionary<string, BotTask> botTask)
        {
            _que.Enqueue(botTask);
            defaultWriteMre.Reset();
            _mre.Set();
        }
        public void RegisterWriteCsv()
        {
            Task proxyAddTask = Task.Factory.StartNew(writebecsv);
        }
    }
    public class BotTask
    {
        public string email { get; set; }
        public string password { get; set; }
        public string pid { get; set; }
        public string size { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string country { get; set; }
        public string cardnumber { get; set; }
        public string mmyy { get; set; }
        public string cvv { get; set; }
        public string jpfirstname { get; set; }
        public string jplastname { get; set; }
        public string token { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string refreshtoken { get; set; }
        public string proxy { get; set; }
    }
    public class CheckSuccess
    {
        public string shop { get; set; }
        public string email { get; set; }
        public string emailpassword { get; set; }
        public string size { get; set; }
        public string tittle { get; set; }
        public string birthday { get; set; }
        public string token { get; set; }
    }
}
