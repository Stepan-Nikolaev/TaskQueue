using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            int countTasks;
            int countParallelTask;

            List<Task> queueTasks = new List<Task>();

            Console.WriteLine("Введите количество задач в очереди на обработку");
            countTasks = Convert.ToInt32(Console.ReadLine());

            QueueTasks tasks = new QueueTasks(countTasks);

            Console.WriteLine("Введите количество параллельно выполняемых задач");
            countParallelTask = Convert.ToInt32(Console.ReadLine());

            QueueThread queueThread = new QueueThread(countParallelTask, tasks);

            queueThread.StartTreatmentQueueTasks();

            Console.ReadKey();
        }
    }
}
