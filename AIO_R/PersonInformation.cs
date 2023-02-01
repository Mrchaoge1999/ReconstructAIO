using CsvHelper;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
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

        private ConcurrentQueue<List<BotTask>> _que = new ConcurrentQueue<List<BotTask>>();

        public static PersonInformation Instance { get { return _personInformationInstance; } }

        public string path = Environment.CurrentDirectory + "\\task.csv";

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
                        listTask = csv.GetRecords<BotTask>().ToList();//test
                    }
                }
            }
            GlobalInfo.listTask = listTask;
            GlobalInfo.newListTask = new List<BotTask>(listTask);
            GlobalInfo.taskNumber = listTask.Count;
        }
        private void WriteCsv()
        {
            while (true)
            {
                List<BotTask> buffer = new List<BotTask>();
                while (_que.Count > 0 && _que.TryDequeue(out buffer) == false) ;
                if (buffer.Count.Equals(0)) { return; }
                bool isExist = false;
                foreach (var i in buffer)
                {
                    try
                    {
                        BotTask whereClass = GlobalInfo.newListTask.FirstOrDefault(t => t.email == i.email);
                        if (whereClass != null)
                        {
                            whereClass = i;
                            isExist = true;
                        }
                    }
                    catch (Exception) { }
                }
                while (isExist)
                {
                    try
                    {
                        using (var writer = new StreamWriter(path))
                        //  using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "task.csv"), false,Encoding.GetEncoding("gb2312")))
                        {
                            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csv.WriteRecords(GlobalInfo.newListTask);
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
        public void WriteFinalBuffer()
        {
            while (_que.Count != 0)
            {
                Thread.Sleep(500);
            }
        }
        public void WriteQue(List<BotTask> botTasks)
        {
            List<BotTask> BotTasksBuffer = new List<BotTask>(botTasks);
            _que.Enqueue(BotTasksBuffer);
        }
        public void RegisterWriteCsv()
        {
            Task proxyAddTask = Task.Factory.StartNew(WriteCsv);
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
