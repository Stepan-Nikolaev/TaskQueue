using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TaskQueue
{
    public class QueueThread
    {
        private bool _isTraetmentQueueTasks = true;
        private List<Thread> _poolThreads = new List<Thread>();
        private QueueTasks _queueTasks;
        public QueueThread(int countParallelTask, QueueTasks queueTask)
        {
            _queueTasks = queueTask;

            for (int i = 0; i < countParallelTask; i++)
            {
                Task currentTask = _queueTasks.GetCurrentTask(i);
                Thread taskThread = new Thread(currentTask.Execut);
                _poolThreads.Add(taskThread);
                taskThread.Start();
            }
        }

        public void StopTreatmentQueueTasks()
        {
            _isTraetmentQueueTasks = false;
        }

        public void StartTreatmentQueueTasks()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (_isTraetmentQueueTasks)
                {
                    var completedThread = from currentThread in _poolThreads
                                          where currentThread.IsAlive == false
                                          select currentThread;
                    var poolCompletedThread = completedThread.ToList();

                    if (poolCompletedThread.Count !=  0)
                    {
                        if (_queueTasks.QueueTasksNotEmpty())
                        {
                            _poolThreads.Remove(poolCompletedThread.First());
                            Thread taskThread = new Thread(_queueTasks.GetCurrentTask(0).Execut);
                            _poolThreads.Add(taskThread);
                            taskThread.Start();
                        }
                    }
                }
            });
        }
    }
}
