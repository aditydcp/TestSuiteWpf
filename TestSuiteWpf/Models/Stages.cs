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
            StringBuilder stringBuilder = new();
            stringBuilder.Append(stage switch
            {
                Stages.Trial => "Trial stage",
                Stages.First => "First stage",
                Stages.Second => "Second stage",
                Stages.Third => "Third stage",
                _ => "Nothing"
            });
            stringBuilder.Append(" (" + (int)stage + ")");
            return stringBuilder.ToString();
        }

        public static int ToMyInt(this Stages stage)
        {
            return stage switch
            {
                Stages.Trial => 0,
                Stages.First => 1,
                Stages.Second => 2,
                Stages.Third => 3,
                _ => -1
            };
        }
    }
}
