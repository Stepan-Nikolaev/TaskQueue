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

            Console.WriteLine("В процессе работы програмы нажмите Esc, чтобы очистить очередь задач.");
            Console.WriteLine("Нажмите Backspace, чтобы остановить обработчик задач.");
            Console.WriteLine("Введите количество задач в очереди на обработку");
            countTasks = Convert.ToInt32(Console.ReadLine());

            QueueTasks tasks = new QueueTasks(countTasks);

            Console.WriteLine("Введите количество параллельно выполняемых задач");
            countParallelTask = Convert.ToInt32(Console.ReadLine());

            QueueThread queueThread = new QueueThread(countParallelTask, tasks);

            UserInputReceiver(tasks, queueThread);

            queueThread.StartTreatmentQueueTasks();

            Console.ReadKey();
        }

        static void UserInputReceiver(QueueTasks queueTasks, QueueThread queueThread)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (true)
                {
                    var ch = Console.ReadKey(false).Key;
                    switch (ch)
                    {
                        case ConsoleKey.Escape:
                            queueTasks.ClearQueueTasks();
                            return;
                        case ConsoleKey.Backspace:
                            queueThread.StopTreatmentQueueTasks();
                            break;
                        case ConsoleKey.OemPlus:
                            queueTasks.AddTask();
                            break;
                    }
                }
            });
        }
    }
}
