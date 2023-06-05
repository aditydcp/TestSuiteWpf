using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    /// <summary>
    /// Enum class for the stages of the test.
    /// </summary>
    public enum Stages
    {
        Trial,
        First,
        Second,
        Third
    }

    public static class StagesExtensions
    {
        /// <summary>
        /// My own ToString()
        /// </summary>
        /// <param name="stage"></param>
        /// <returns>String</returns>
        public static string ToMyString(this Stages stage)
        {
            return stage switch
            {
                Stages.Trial => "Trial stage",
                Stages.First => "First stage",
                Stages.Second => "Second stage",
                Stages.Third => "Third stage",
                _ => "Nothing"
            };
        }
    }
}
