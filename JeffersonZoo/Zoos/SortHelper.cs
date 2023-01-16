using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;

namespace Zoos
{
    /// <summary>
    /// Helps sort a list in order to get the sort result.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// Sorts by name using Bubble Sort.
        /// </summary>
        /// <param name="list">Animals to be sorted.</param>
        /// /// <param name="comparer">The comparer (name or weight).</param>
        /// <returns>The result of sorting the list of animals.</returns>
        public static SortResult BubbleSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int compareCounter = 0;
            int swapCounter = 0;

            // use a for loop to loop backward through the list
            for (int i = list.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    compareCounter += 1;

                    if (comparer(list[j], list[j + 1]) > 0)
                    {
                        Swap(list, j, j + 1);
                        swapCounter += 1;
                    }
                }
            }

            stopwatch.Stop();

            return new SortResult { SwapCount = swapCounter, Objects = list.Cast<object>().ToList(), CompareCount = compareCounter, ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Sorts by name using Selection Swap.
        /// </summary>
        /// <param name="list">Animals to be swapped.</param>
        /// /// <param name="comparer">The comparer (name or weight).</param>
        /// <returns>The result of swapping the animal list.</returns>
        public static SortResult SelectionSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int compareCounter = 0;
            int swapCounter = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                object animalName = list[i];

                for (int j = i + 1; j < list.Count; j++)
                {
                    compareCounter += 1;

                    if (comparer(animalName, list[j]) > 0)
                    {
                        animalName = list[j];
                    }
                }

                if (list[i] != animalName)
                {
                    Swap(list, i, list.IndexOf(animalName));
                    swapCounter += 1;
                }
            }

            stopwatch.Stop();

            return new SortResult { SwapCount = swapCounter, Objects = list.Cast<object>().ToList(), CompareCount = compareCounter, ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Sorts by name using Insertion Sort.
        /// </summary>
        /// <param name="list">Animals to be swapped.</param>
        /// /// <param name="comparer">The comparer (name or weight).</param>
        /// <returns>The result of swapping the animal list.</returns>
        public static SortResult InsertionSort(this IList list, Func<object, object, int> comparer)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int compareCounter = 0;
            int swapCounter = 0;

            for (int i = 1; i < list.Count; i++)
            {
                compareCounter += 1;

                for (int j = i; j > 0 && comparer(list[j], list[j - 1]) < 0; j--)
                {
                    Swap(list, list.IndexOf(list[j]), list.IndexOf(list[j - 1]));
                    swapCounter += 1;
                }
            }

            stopwatch.Stop();

            return new SortResult { SwapCount = swapCounter, Objects = list.Cast<object>().ToList(), CompareCount = compareCounter, ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds };
        }

        /// <summary>
        /// Sorts the animals by name using quick sort.
        /// </summary>
        /// <param name="list">Animals to be sorted.</param>
        /// <param name="leftIndex">Left index of list.</param>
        /// <param name="rightIndex">Right index of list.</param>
        /// <param name="sortResult">The sort result of list.</param>
        /// <param name="comparer">The comparer (name or weight).</param>
        /// <returns>Result of sorting the animals.</returns>
        public static SortResult QuickSort(this IList list, int leftIndex, int rightIndex, SortResult sortResult, Func<object, object, int> comparer)
        {
            int leftPointer = leftIndex;
            int rightPointer = rightIndex;

            // Gets the animal between the index points.
            object pivotAnimal = list[(leftIndex + rightIndex) / 2];

            bool done = false;

            while (done == false)
            {
                int pivotPosition = list.IndexOf(pivotAnimal);

                // "Woah there's something bigger than you in this section."
                while (comparer(list[leftPointer], pivotAnimal) < 0)
                {
                    leftPointer += 1;
                    sortResult.CompareCount += 1;
                }

                // "Woah there's something smaller than you in this section."
                while (comparer(pivotAnimal, list[rightPointer]) < 0)
                {
                    rightPointer -= 1;
                    sortResult.CompareCount += 1;
                }

                // "We have to get these animals in the right section! Let's swap them! Then let's close in on a smaller section."
                if (leftPointer <= rightPointer)
                {
                    Swap(list, leftPointer, rightPointer);
                    sortResult.SwapCount += 1;
                    leftPointer += 1;
                    rightPointer -= 1;
                }

                // "Have we completed this section or do we need to check again?"
                if (leftPointer > rightPointer)
                {
                    done = true;
                }
            }

            // If the LEFT "section" of the list isn't sorted, sort it.
            if (leftIndex < rightPointer)
            {
                QuickSort(list, leftIndex, rightPointer, sortResult, comparer);
            }

            // If the RIGHT "section" of the list isn't sorted, sort it.
            if (rightIndex > leftPointer)
            {
                QuickSort(list, leftPointer, rightIndex, sortResult, comparer);
            }

            sortResult.Objects = list.Cast<object>().ToList();

            return sortResult;
        }

        private static void Swap(this IList list, int index1, int index2)
        {
            object firstAnimal = list[index1];
            object secondAnimal = list[index2];
            list[index2] = firstAnimal;
            list[index1] = secondAnimal;
        }
    }
}
