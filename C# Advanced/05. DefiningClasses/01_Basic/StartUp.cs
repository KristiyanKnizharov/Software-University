namespace DefiningClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        static void Main(string[] args)
        {
            DoublyLinkedList doublyLinkedList = new DoublyLinkedList();

            for (int i = 0; i < 3; i++)
            {
                doublyLinkedList.AddFirst(i);
            }
        }
    }
}
