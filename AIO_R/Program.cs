using AIO_R.ABC_Buy;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Threading.Tasks.Schedulers;
using XAct;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AIO_R
{
    class Program
    {
        public static string version = "V0.01";

        public static Dictionary<string, BotTask> taskBuffer = new Dictionary<string, BotTask>();
        static void Main()
        {
            Console.Title = "UFO AIO " + version;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("UFO AIO");
            Console.WriteLine("\n\n\n1. ABC-Mart Purchase");
            Console.WriteLine("2. ABC Raffle");
            Console.WriteLine("3. ABC Raffle Check");
            int inputNumber = IsNumberic(Console.ReadLine());
            while (inputNumber == 0 && inputNumber > 3)
            {
                Console.WriteLine("Error Input");
            }
            BotProxy.Instance.RegisterProxyThread();
            PersonInformation.Instance.RegisterWriteCsv();
            List<Task> taskList = new List<Task>();
            switch (inputNumber)
            {
                case 1:
                    taskList = new RunTask(100).RunNumberTask("ABC-Mart Purchase");
                    break;
                case 2:
                    taskList = new RunTask(20).RunNumberTask("ABC Raffle");
                    break;
                case 3:
                    taskList = new RunTask(20).RunNumberTask("ABC Raffle Check");
                    break;
            }
            Task.WaitAll(taskList.ToArray());
            PersonInformation.Instance.writeque(new Dictionary<string, BotTask>(taskBuffer));
            Console.WriteLine("Waiting for Final!");
            PersonInformation.defaultWriteMre.WaitOne();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please Write Any Key to Exist!");
            Console.ReadKey();
        }
        public static int IsNumberic(string? strnum)
        {
            if (strnum.Equals(string.Empty)) return 0;
            int i = 0;
            bool result = int.TryParse(strnum, out i);
            return result == true ? i : 0;
        }
    }
    class RunTask
    {
        TaskFactory fac;
        List<Task> taskList = new List<Task>();
        public RunTask(int number)
        {
            fac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(number));
        }
        public List<Task> RunNumberTask(string typeTask)
        {
            if (typeTask.Equals("ABC-Mart Purchase"))
            {
                foreach (var i in GlobalInfo.listTask)
                {
                    taskList.Add(fac.StartNew(new AbcPurchase(i).Initial, TaskCreationOptions.LongRunning));
                }
            }
            else if (typeTask.Equals("ABC Raffle"))
            {
                foreach (var i in GlobalInfo.listTask)
                {
                    taskList.Add(fac.StartNew(new AbcPurchase(i).Initial, TaskCreationOptions.LongRunning));
                }
            }
            else
            {
                foreach (var i in GlobalInfo.listTask)
                {
                    RaffleProduct abcRaffle = new AbcRaffle(i);
                    abcRaffle.checkRaffle = true;
                    taskList.Add(fac.StartNew(new AbcPurchase(i).Initial, TaskCreationOptions.LongRunning));
                }
            }
            return taskList;
        }
    }
}