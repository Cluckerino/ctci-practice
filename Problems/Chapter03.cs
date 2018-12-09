using System;
using System.Collections.Generic;

namespace Problems
{
    public static class Chapter03
    {
        /// <summary>
        /// Three stacks that share the same array.
        /// </summary>
        public class P01ThreeInOne<T>
        {
            public T[] array = new T[0];

            private int len1;
            private int len2;
            private int len3;

            private int start1;
            private int start2;
            private int start3;

            /// <summary>
            /// List the contents of stack 1, starting from the bottom of the stack.
            /// </summary>
            public List<T> List1() => ListX(start1, len1);

            /// <summary>
            /// List the contents of stack 2, starting from the bottom of the stack.
            /// </summary>
            public List<T> List2() => ListX(start2, len2);

            /// <summary>
            /// List the contents of stack 3, starting from the bottom of the stack.
            /// </summary>
            public List<T> List3() => ListX(start3, len3);

            /// <summary>
            /// Pop the item from stack 1;
            /// </summary>
            public T Pop1()
            {
                return PopX(start1, ref len1);
            }

            /// <summary>
            /// Pop the item from stack 2;
            /// </summary>
            public T Pop2()
            {
                return PopX(start2, ref len2);
            }

            /// <summary>
            /// Pop the item from stack 3;
            /// </summary>
            public T Pop3()
            {
                return PopX(start3, ref len3);
            }

            /// <summary>
            /// Print contents to console.
            /// </summary>
            public void Print()
            {
                Console.WriteLine("Printing backing array, 1, 2, 3:");
                Console.WriteLine($"[{string.Join(", ", array)}]");
                Console.WriteLine($"S1:{start1}+{len1} [{string.Join(", ", ListX(start1, len1))}]");
                Console.WriteLine($"S2:{start2}+{len2} [{string.Join(", ", ListX(start2, len2))}]");
                Console.WriteLine($"S3:{start3}+{len3} [{string.Join(", ", ListX(start3, len3))}]");
            }

            /// <summary>
            /// Push the item into stack 1.
            /// </summary>
            public void Push1(T item)
            {
                PushX(item, start1, ref len1, ref start2, len2, ref start3, len3);
            }

            /// <summary>
            /// Push the item into stack 2.
            /// </summary>
            public void Push2(T item)
            {
                PushX(item, start2, ref len2, ref start3, len3, ref start1, len1);
            }

            /// <summary>
            /// Push the item into stack 3.
            /// </summary>
            public void Push3(T item)
            {
                PushX(item, start3, ref len3, ref start1, len1, ref start2, len2);
            }

            /// <summary>
            /// Grow the array for the given stack; if growing S1, X=1, Y=2, Z=3; if growing S2, X=2,
            /// Y=3, Z=1, etc.
            /// </summary>
            private void GrowX(int startX, int lenX,
                ref int startY, int lenY,
                ref int startZ, int lenZ)
            {
                // We need to double X so get it's current length (or set to 1, if originally empty).
                var padding = lenX > 0 ?
                    lenX :
                    1;

                // Create new expanded array.
                var newArray = new T[array.Length + padding];

                // Push indices by this length if necessary
                var newStartY = startY >= startX ?
                    startY + padding :
                    startY;

                var newStartZ = startZ >= startX ?
                    startZ + padding :
                    startZ;

                // Copy X to same indices
                LoopingCopy(array, newArray, startX, startX, lenX);
                // Copy Y and Z to new indices
                LoopingCopy(array, newArray, startY, newStartY, lenY);
                LoopingCopy(array, newArray, startZ, newStartZ, lenZ);

                // Store new values
                array = newArray;
                startY = newStartY;
                startZ = newStartZ;
            }

            /// <summary>
            /// First item is bottom of stack.
            /// </summary>
            private List<T> ListX(int startX, int lenX)
            {
                var list = new List<T>(lenX);
                for (int i = 0; i < lenX; i++)
                {
                    var itemInd = ShiftIndex(startX, i);
                    list.Add(array[itemInd]);
                }
                return list;
            }

            /// <summary>
            /// Copy from old array to new array the given number of items. Uses wrap-around.
            /// </summary>
            private void LoopingCopy(T[] oldArr, T[] newArr,
                int oldStart, int newStart, int length)
            {
                // Assume/assert: GetLen(old) == GetLen(new)

                // Assume/assert: if start == array.Length (i.e. outside the bound), then len == 0,
                // (i.e. empty stack, so won't iterate and access array anyway).
                int oldInd = oldStart;
                int newInd = newStart;
                // Counter is a dummy, we wanna use the properly shifted indices.
                for (int _ = 0; _ < length; _++)
                {
                    newArr[newInd] = oldArr[oldInd];
                    oldInd = ShiftIndex(oldInd, 1, oldArr);
                    newInd = ShiftIndex(newInd, 1, newArr);
                }
            }

            /// <summary>
            /// Pop the item from stack X.
            /// </summary>
            private T PopX(int startX, ref int lenX)
            {
                if (lenX == 0) throw new InvalidOperationException();
                // Index of actual item = (startX + lenX) % array.Length;
                var itemInd = ShiftIndex(startX, lenX);
                var item = array[itemInd];
                // Update states
                array[itemInd] = default(T);
                lenX--;
                return item;
            }

            /// <summary>
            /// Push the item into stack X. Refs are just needed when growing.
            /// </summary>
            private void PushX(T item, int startX, ref int lenX,
                ref int startY, int lenY,
                ref int startZ, int lenZ)
            {
                // Essentially startX + lenX = ind(lastX) + 1, so only increment lenX at the very end
                // (i.e. if you have startX = 0 and lenX = 1, your only item is at ind = 0).

                // Target item index.
                int itemInd;
                // First item.
                if (lenX == 0)
                {
                    itemInd = startX;
                    GrowX(startX, lenX, ref startY, lenY, ref startZ, lenZ);
                }
                // Subsequent items
                else
                {
                    itemInd = ShiftIndex(startX, lenX);
                    // If adding this item will clash with the next stack, then grow.
                    if (itemInd == startY)
                    {
                        GrowX(startX, lenX, ref startY, lenY, ref startZ, lenZ);
                        // Special case: We grew out the tail, so if startY = 0, recalculate the
                        // itemInd so it isn't 0.
                        if (startY == 0)
                            itemInd = ShiftIndex(startX, lenX);
                    }
                }

                array[itemInd] = item;
                lenX++;
            }

            /// <summary>
            /// Shift the index by the given amount, taking into account the current array's length.
            /// </summary>
            private int ShiftIndex(int start, int amount) => ShiftIndex(start, amount, array);

            /// <summary>
            /// Shift the index by the given amount, taking into account the given array's length.
            /// </summary>
            private int ShiftIndex(int index, int amount, Array targetArray)
            {
                var length = targetArray.Length;
                if (length == 0) return 0;
                // Assumes abs(amount) < length
                if (amount > 0)
                    return (index + amount) % length;
                // Add length before adding negative
                else if (amount < 0)
                    return index + amount + length;
                return index;
            }
        }

        /// <summary>
        /// Implement a Queue using two Stacks.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class P04QueueViaStacks<T>
        {
            // Top of storage stack should be the first item in.
            private Stack<T> storageStack = new Stack<T>();

            /// <summary>
            /// Removes an item from the Queue.
            /// </summary>
            public T Dequeue()
            {
                return storageStack.Pop();
            }

            /// <summary>
            /// Adds an item to the Queue.
            /// </summary>
            /// <param name="item"></param>
            public void Enqueue(T item)
            {
                // Top of temp stack should be the last item in.
                var tempStack = new Stack<T>(storageStack.Count + 1);

                while (storageStack.TryPop(out var oldItem))
                    tempStack.Push(oldItem);

                storageStack.Push(item);

                while (tempStack.TryPop(out var oldItem))
                    storageStack.Push(oldItem);
            }

            /// <summary>
            /// Check if the Queue is empty.
            /// </summary>
            public bool IsEmpty()
            {
                return storageStack.Count == 0;
            }

            /// <summary>
            /// Look at the top of the Queue (i.e. the first item to be removed).
            /// </summary>
            public T Peek()
            {
                return storageStack.Peek();
            }
        }
    }
}