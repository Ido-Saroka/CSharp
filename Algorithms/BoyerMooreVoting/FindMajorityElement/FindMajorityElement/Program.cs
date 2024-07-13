using System;
using System.Collections.Generic;
using System.Linq;

namespace FindMajorityElement
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Example call
            int[] nums = new int[] { 2, 2, 3, 5, 2, 1, 2, 6, 6, 2, 1, 3, 2, 2 }; // expected result: 2
            var majorityElemResult = BoyerMooreVotingFindMajorityElement(nums);
            if (!majorityElemResult.IsSuccess)
            {
                Console.WriteLine($"Error: {majorityElemResult.Error}");
            }
            else
            {
                Console.WriteLine($"Majority element is {majorityElemResult.Value}");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// This function uses the <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore_majority_vote_algorithm">Boyer Moore Voting</see> algorithm to find the majority element in a given array.
        /// <br/>
        /// <strong>The algorithms pre-condition</strong> is that a majority element (appears at least n/2, n - the length of the array) exists.
        /// <para/>
        /// For validation purposes, the suspected element occurrences will be counted before it is returned 
        /// <br/>
        /// to the caller (can be disabled by sending false as the 
        /// <br/>
        /// second argument (validateResult)
        /// <para/>
        /// <remarks>
        ///     <strong>Example:</strong> calling the method using an int array
        ///     <code lang="C#"><![CDATA[
        ///          public static void Main()
        ///          {
        ///             int[] values = { 2, 2, 3, 5, 2, 1, 2, 6, 6, 2, 1, 3, 2, 2 , 10};
        ///             var majorityElemResult = BoyerMooreVotingFindMajorityElement(values);
        ///             if(!majorityElemResult.IsSuccess){
        ///                Console.WriteLine($"Error: {majorityElemResult.Error}");
        ///             }
        ///             else{
        ///                Console.WriteLine($"Majority element is {majorityElemResult.Value}");
        ///             }
        ///          }
        ///     ]]>
        ///     </code>
        /// </remarks>
        /// <pr/>
        /// </summary>
        /// <typeparam name="T">
        /// The type of elements inside the received collection
        /// </typeparam>
        /// <param name="items">
        /// The collection from which to find a majority element.
        /// </param>
        /// <param name="validateResult">
        /// Set to false to avoid validating the result (only advised if it is 
        /// <br/>
        /// known for a fact that the collection contains a majority
        /// <br/>
        /// element (appears at least n/2 times)
        /// </param>
        /// <returns>
        /// Instance of the <see cref="Result{T}">Results class</see> storing either the majority element if the
        /// <br/>
        /// an error message if the functions execution was un-successful.
        /// </returns>
        public static Result<T> BoyerMooreVotingFindMajorityElement<T>(IEnumerable<T> items, bool validateResult = true)
        {
            if (items == null) { return Result<T>.Failure("Invalid value: collection is null."); }

            //C# 8.0 Feature - instead of wrapping the code in using statement 
            using var itemsEnumerator = items.GetEnumerator();
            if (!itemsEnumerator.MoveNext()) { return Result<T>.Failure("Invalid value: collection is empty."); }

            /*Variables initialization:
             * candidate - stores the potential candidate for the majority element,initialized to the first element in the collection
             * counter - saves the number of occurrences the current candidate was encountered (will decrease if the currently checked element has a different value)
             * stopCondition - if the current candidate appeared n/2 times (n - the length of the collection) then we can return it as the majority element 
             *                 without continuing the iteration on the collection.
             */
            T candidate = items.First<T>();
            int counter = 1;
            int stopCondition = (int)Math.Round((double)items.Count() / 2);

            //First value was already processed 
            while (itemsEnumerator.MoveNext())
            {
                if (!EqualityComparer<T>.Default.Equals(itemsEnumerator.Current, candidate)) { counter--; }
                else { counter++; }

                //Current item isn't the majority element and will be replaced with the current value
                if (counter == 0)
                {
                    candidate = itemsEnumerator.Current;
                    counter = 1;
                }

                //We have found the majority element and can stop iterating over the rest of the collection
                if (counter == stopCondition)
                {
                    break;
                }
            }

            //Validate that the found candidate is the majority element
            if (validateResult)
            {
                counter = 0;
                foreach (var item in items)
                {
                    if (EqualityComparer<T>.Default.Equals(item, candidate)) { counter++; }
                }
                if (counter < stopCondition) { return Result<T>.Failure("No majority element exists in the provided collection"); }
            }

            return Result<T>.Success(candidate);
        }
    }
}
