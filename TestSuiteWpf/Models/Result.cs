using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    /// <summary>
    /// Enum class for trial result.
    /// </summary>
    public enum Result
    {
        Correct = 1,
        Incorrect = 0,
        Timeout = 2
    }

    public static class ResultExtensions
    {
        /// <summary>
        /// My own ToString()
        /// </summary>
        /// <param name="result"></param>
        /// <returns>String</returns>
        public static string ToMyString(this Result result)
        {
            return result switch
            {
                Result.Correct => "Correct (1)",
                Result.Incorrect => "Incorrect (0)",
                Result.Timeout => "Timeout(2)",
                _ => "Nothing"
            };
        }
    }
}
