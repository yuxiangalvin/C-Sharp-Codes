using System;

namespace EECS214Assignment1
{
    /// <summary>
    /// A queue internally implemented as an array
    /// </summary>
    public class ArrayQueue : Queue
    {
        object[] array = new object[256];
        int head = 0;
        int tail = 0;

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            if (IsFull)
            {
                throw new QueueFullException();
            }
            array[tail] = o;
            if (tail == array.Length)
                tail = 1;
            else 
            tail = (tail + 1);
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
            object remove = array[head];
            if (head == tail)
                head = 1;
            else
            head = (head + 1);
            return remove;
            // throw new NotImplementedException();
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get {
                int count;
                // TODO: check that you implementation runs in O(1) time
                return count =(tail - head);
                // throw new NotImplementedException();
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// </summary>
        public override bool IsFull
        {
            get {
                // TODO: check that your implementation runs in O(1) time
                return (Count == 256);
                // throw new NotImplementedException();
            }
        }
    }
}
