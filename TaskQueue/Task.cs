using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskQueue
{
    public class Task
    {
        private string _message;

        public Task(string message)
        {
            _message = message;
        }
        public void Execut()
        {
            Console.WriteLine(_message);
            Thread.Sleep(2000);
        }
    }
}
