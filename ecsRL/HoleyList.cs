using System.Collections.Generic;
using System.Linq;

namespace ecsRL
{
    public class HoleyList<T>
    {
        /*  
         *  This datastructure contains an array
         *  
         *  New items always get added to the first available spot in the array
         * 
         *  If the array is full, a new one twice the size is created
         *  
         */


        T[] items;
        Stack<int> freeIndices;

        public HoleyList()
        {
            items = new T[1];
            freeIndices = new Stack<int>();
            freeIndices.Push(0);
        }

        // Adds an item to the array and returns the index it was added at
        public int add(T toAdd)
        {
            if(freeIndices.Count != 0)
            {
                int index = freeIndices.Pop();
                items[index] = toAdd;
                return index;
            }
            else // array is full! 
            {
                // push all free indices (of the array we are about to create) in reverse order to the stack
                // items.Length will therefore be the first free index
                for(int i = (items.Length * 2) - 1; i >= items.Length; i--)
                {
                    freeIndices.Push(i);
                }

                // create new array twice the size
                T[] newItems = new T[items.Length * 2];
                items.CopyTo(newItems, 0);
                items = newItems;

                // add the item
                int index = freeIndices.Pop();
                items[index] = toAdd;
                return index;
            }
        }

        public void remove(int index)
        {
            items[index] = default(T); // default(T) = null for classes
            freeIndices.Push(index);
        }

        public T get(int index)
        {
            return items[index];
        }

    }
}
