using System;
using System.ComponentModel;
using System.Threading;

namespace background_worker
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            backgroundWorker.WorkerReportsProgress = true;
            initializeBGWorker(backgroundWorker);
            triggerDoWork(backgroundWorker);
            for (int i = 0; i < 100; i += 10)
            {
                triggerProgressChanged(backgroundWorker, i);
            }

            Console.Read();
        }

        private static void triggerProgressChanged(BackgroundWorker backgroundWorker, int percentage)
        {
           
                backgroundWorker.ReportProgress(percentage);
        }

        private static void triggerDoWork(BackgroundWorker backgroundWorker)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private static void initializeBGWorker(BackgroundWorker backgroundWorker1)
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        private static void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"In backgroundWorker1_ProgressChanged work progress percentage is {e.ProgressPercentage}");
        }

        private static void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("backgroundWorker1_RunWorkerCompleted");
        }

        private static void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            Console.WriteLine("backgroundWorker1_DoWork");
        }
    }
}
