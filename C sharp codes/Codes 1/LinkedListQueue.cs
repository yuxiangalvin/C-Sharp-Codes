using System;

namespace EECS214Assignment1
{
    /// <summary>
    /// A queue internally implemented as a linked list of objects
    /// </summary>
    /// 

    public class LinkedListQueue : Queue
    {
        public Cell head = null;
        public Cell tail = null;
        public int counter = 0;

        public class Cell
        {
            public object value;
            public Cell next;
        }

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            if (head == null)
            {
                head = new Cell()
                {
                    value = o,
                    next = null
                };
                tail = head;
            }
            else
            {
                tail.next = new Cell()
                {
                    value = o,
                    next = null
                };
                tail = tail.next;
            }

            counter = counter + 1;
            // TODO: Replace this line with your code.
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
            if (IsEmpty)
            {
                throw new QueueEmptyException();
            }

            object result = head.value;

            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                Cell temp = head;
                head = head.next;
                temp.next = null;
            }

            counter = counter - 1;
            return result;

            // throw new NotImplementedException();
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get
            {
                return counter;    
                //throw new NotImplementedException();
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// Note: LinkedListQueues can be grown to arbitrary length, and so can
        /// never fill.
        /// </summary>
        public override bool IsFull
        {
            get
            {
                return false;
            }
        }
    }
}
