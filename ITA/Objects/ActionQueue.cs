using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITA.Objects
{
    public class ActionQueue
    {
        private Queue<Action> internQueue = new Queue<Action>();
        private object key = new object();
        public bool IsExecuting = false;
        public bool ContiniousExecution = false;

        public ActionQueue() : this(true) { }

        public ActionQueue(bool ContiniousExecution)
        {
            this.ContiniousExecution = ContiniousExecution;
        }

        public void Add(Action action)
        {
            Add(action, ContiniousExecution);
        }

        public void Add(Action action,bool startExecution)
        {
            internQueue.Enqueue(action);
            if(startExecution && !IsExecuting)
                Execute();
        }

        public void Execute()
        {
                if (IsExecuting)
                    throw new NotSupportedException("Can't execute more then 1 item at the time");
                if (internQueue.Count > 0)
                {
                    internQueue.Dequeue()();
                    IsExecuting = true;
                }
        }

        public void Done()
        {
            lock (key)
            {
                IsExecuting = false;
                if (ContiniousExecution)
                    Execute();
            }
        }

    }
}
