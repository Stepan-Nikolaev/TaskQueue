using System;
using System.Collections.Generic;
using System.Text;

namespace TaskQueue
{
    public class QueueTasks
    {
        private List<Task> _queueTasks = new List<Task>();
        public QueueTasks(int countTasks)
        {
            for (int i = 0; i < countTasks; i++)
            {
                AddTask(i.ToString());
            }
        }

        public void AddTask(string message = "Новая задача")
        {
            _queueTasks.Add(new Task(message));
        }

        public Task GetCurrentTask(int indexTask)
        {
            Task currentTask = _queueTasks[indexTask];
            _queueTasks.Remove(currentTask);

            return currentTask;
        }

        public bool QueueTasksNotEmpty()
        {
            if (_queueTasks.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearQueueTasks()
        {
            _queueTasks.Clear();
        }
    }
}
