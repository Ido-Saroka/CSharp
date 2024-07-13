namespace FindMajorityElement
{
    /// <summary>
    /// Used for returning function results.
    /// <br/>
    /// Using the class provides a cleaner way to handle thrown exceptions instead of relaying on the caller to implement error handling,
    /// <br/>
    /// while also obscuring the "business" logic from the calling method.
    /// <pr/>
    /// <remarks>
    ///     Example: Using the result class
    ///     <code lang="C#"><![CDATA[
    ///          public static int FindMaxNumber(int[] values){
    ///             if(!values || values.Length == 0) {throw new ArgumentException("Invalid argument:received array is null or empty.");}
    ///             for (int i = 0; i < values.Length; i++)
    ///             {
    ///                   ...
    ///             }
    ///          }
    ///          
    ///          public static void Main()
    ///          {
    ///             int[] nums = { 3, 4, 2, 7};
    ///             var maxNumberResult = FindMaxNumber(nums);
    ///             if(!result.IsSuccess){
    ///                Console.WriteLine($"Error: {maxNumberResult.Error}");
    ///             }
    ///             else{
    ///                Console.WriteLine($"Max number is{maxNumberResult.Value}");
    ///             }
    ///          }
    ///     ]]>
    ///     </code>
    /// </remarks>
    /// <para/>
    /// <b>Additional Info:</b>
    /// <br/>
    /// *<see href="https://enterprisecraftsmanship.com/posts/functional-c-handling-failures-input-errors/">Functional C#: Handling failures, input errors</see>    
    /// </summary>
    /// <typeparam name="T">
    /// The type of the returned result (in case of success)
    /// </typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Functions result.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Exception message (if thrown)
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Indicates if the methods execution was successful (read-only).
        /// </summary>
        public bool IsSuccess => Error == null;

        protected Result(T value, string error)
        {
            Value = value;
            Error = error;
        }

        /// <summary>
        /// Used for creating a successful result (the function finished its execution without any errors and produced a valid result).
        /// </summary>
        /// <param name="value">
        /// The functions result
        /// </param>
        /// <returns>
        /// An instance of the <see cref="Result{T}"/> class with the functions return value
        /// </returns>
        public static Result<T> Success(T value) => new Result<T>(value, null);

        /// <summary>
        /// Used when the function failed to produce a valid result (received arguments were invalid, an exception was thrown, etc..)
        /// </summary>
        /// <param name="error">
        /// The error message which will be returned to the caller.
        /// </param>
        /// <returns>
        /// An instance of the <see cref="Result{T}"/> class with information about the failed function call.
        /// </returns>
        public static Result<T> Failure(string error) => new Result<T>(default, error);
    }
}
