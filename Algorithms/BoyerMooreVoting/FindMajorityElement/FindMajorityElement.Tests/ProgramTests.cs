using FindMajorityElement.Tests.TestClasses;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace FindMajorityElement.Tests
{
    /// <summary>
    /// Used for testing the methods found in <see cref="Program"/>
    /// </summary>
    public class ProgramTests
    {
        private readonly ITestOutputHelper _output;

        public ProgramTests(ITestOutputHelper output)
        {
            _output = output;
        }

        #region Exeptected error messages
        //Error message initialization - provide a single location where they are needed to be updated in case of code changes.
        static string _nullCollectionErrorMessage = "Invalid value: collection is null.";
        static string _emptyCollectionErrorMessage = "Invalid value: collection is empty.";
        static string _noMajorityElementExistsErrorMessage = "No majority element exists in the provided collection";
        #endregion

        #region Test Data
        //Int testing data
        static List<int> IntList => new List<int> { 3, 3, 4, 2, 4, 4, 2, 4, 4 };
        static int[] IntArray => IntList.ToArray();
        //Used for testing error messages (HashSet doesn't allow for duplicate values)
        static readonly HashSet<int> IntHashSet = new HashSet<int>(IntList);

        //Int data expected parameters
        static int ExpectedIntResult => 4;
        static object[] expectedIntTestResults = { true, ExpectedIntResult };

        //String testing data
        static List<string> StringList => new List<string> { "a", "b", "a", "a", "c", "a" };
        static string[] StringArray => StringList.ToArray();

        //String data expected result
        static string ExpectedStringResult => "a";
        static object[] expectedStringTestResults = { true, ExpectedStringResult };

        //Custom object (product) testing data
        static List<Product> ProductList => new List<Product>
        {
        new Product("Apple"), new Product("Apple"), new Product("Banana"),
        new Product("Apple"), new Product("Apple"), new Product("Banana"),
        new Product("Apple")
        };
        static Product[] ProductArray => ProductList.ToArray();

        //Custom object (product) expected result
        static Product ExpectedProductResult => new Product("Apple");
        static object[] expectedProductTestResults = { true, ExpectedProductResult };
        #endregion

        /// <summary>
        /// Tests the <see cref="Program.BoyerMooreVotingFindMajorityElement{T}">Boyer-Moore Voting algorithm</see> for finding the majority element in a collection of items.
        /// <br/>
        ///Test cases are provided by the <see cref="GetTestData"> GetTestData method</see>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="items">The collection of items to test.</param>
        /// <param name="expectedTestResults">
        /// An array containing the information about the test expected results:
        /// <list type="number">
        /// <item>
        /// <description>Boolean Flag - Indicates if the test is expected to run successfully.</description>
        /// </item>
        /// <item>
        /// <description>T - The expected majority element if the test is successful (value and type)</description>
        /// </item>
        /// <item>
        /// <description>string - Error message (if methods execution is expected to fail)</description>
        /// </item>
        /// </list>
        /// </param>
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void FindMajorityElement_Test<T>(IEnumerable<T> items, object[] expectedTestResults)
        {
            ///Arrange - Extract the data from expectedTestResults"/>
            bool isExpectedToRunSuccessfully = (bool)expectedTestResults[0];
            T expectedValue = (T)expectedTestResults[1];
            string expectedError = expectedTestResults.Length > 2 ? (string)expectedTestResults[2] : null;


            ///Act
            var result = Program.BoyerMooreVotingFindMajorityElement(items);

            ///Assert
            if (isExpectedToRunSuccessfully)
            {
                Assert.True(result.IsSuccess);
                Assert.Equal(expectedValue, result.Value);
            }
            else
            {
                Assert.False(result.IsSuccess);
                Assert.Equal(expectedError, result.Error);
            }
        }


        /// <summary>
        /// Used for returning a collection of test data for the <see cref="FindMajorityElement_Test">FindMajorityElement_Test</see>
        /// <para/>
        /// The return value is comprised of the following:
        /// <br/>
        /// <list type="bullet">
        /// <item>
        /// A collection that implements the <see cref="System.Collections.Generic.IEnumerable{T}">IEnumerable interface</see>, containing the items to use for testing.
        /// </item>
        /// <item>
        /// An array with the following items:
        /// <list type="number">
        /// <item>
        /// A boolean indicating if a majority element is expected.
        /// </item>
        /// <item>
        /// The expected majority element
        /// </item>
        /// <item>
        /// An expected error message if no majority element is found or if the collection is invalid
        /// </item>
        /// </list>
        /// </item>
        /// </list> 
        /// </summary>
        /// <returns>
        /// <see cref="FindMajorityElement_Test">IEnumerable</see> collection.
        /// </returns>
        public static IEnumerable<object[]> GetTestData()
        {
            //Error Message testing
            yield return new object[] { null, new object[] { false, null, _nullCollectionErrorMessage } };
            yield return new object[] { new List<int>(), new object[] { false, default(int), _emptyCollectionErrorMessage } };
            yield return new object[] { IntHashSet, new object[] { false, default(int), _noMajorityElementExistsErrorMessage } };

            //Int testing
            yield return new object[] { IntList, expectedIntTestResults };
            yield return new object[] { IntArray, expectedIntTestResults };

            //String testing
            yield return new object[] { StringList, expectedStringTestResults };
            yield return new object[] { StringArray, expectedStringTestResults };

            //Custom Object (Product) testing
            yield return new object[] { ProductList, expectedProductTestResults };
            yield return new object[] { ProductArray, expectedProductTestResults };
        }

    }
}
