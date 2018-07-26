using System;

namespace EECS214Assignment1
{
    /// <summary>
    /// A double-ended queue
    /// Implement this as a doubly-linked list
    /// </summary>
    public class Deque
    {
        Cell head = null;
        Cell tail = null;
        public int counter = 0;


        public class Cell
        {
            public object value;
            public Cell prev;
            public Cell next;
        }
        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public void AddFront(object o)
        {
            if (head == null)
            {
                head = new Cell()
                {
                    value = o,
                    next = null,
                    prev = null
                };
                tail = head;
            }
            else
            {
                head.prev = new Cell()
                {
                    value = o,
                    next = head,
                    prev = null
                };
                head = head.prev;
            }

            counter = counter + 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public object RemoveFront()
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
                head.prev = null;
            }

            counter = counter - 1;
            return result;

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public void AddEnd(object o)
        {
            if (head == null)
            {
                head = new Cell()
                {
                    value = o,
                    next = null,
                    prev = null
                };
                tail = head;
            }
            else
            {
                tail.next = new Cell()
                {
                    value = o,
                    next = null,
                    prev = tail
                };
                tail = tail.next;
            }
            counter = counter + 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public object RemoveEnd()
        {
            if (IsEmpty)
            {
                throw new QueueEmptyException();
            }

            object result = tail.value;

            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                Cell temp = tail;
                tail = tail.prev;
                temp.prev = null;
                tail.next = null;
            }
            counter = counter - 1;
            return result;

            //throw new NotImplementedException();
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return counter;
                //throw new NotImplementedException();
            }
        }

        /// <summary>
        /// True if the queue is empty and dequeuing is forbidden.
        /// </summary>
        public bool IsEmpty => Count == 0;
    }
}
