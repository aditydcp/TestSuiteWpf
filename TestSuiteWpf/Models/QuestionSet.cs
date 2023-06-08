using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    public class QuestionSet
    {
        public List<Question> questions { get; set; }
        public int Level { get; }

        // saves the index of the latest (max 5) given questions
        private readonly static int bufferCapacity = 5;
        private List<int> questionIndexBuffer = new();

        private readonly Random random = new();

        public QuestionSet(List<Question> questions, int level)
        {
            this.questions = questions;
            Level = level;
        }

        /// <summary>
        /// Pick a random question from this question set.
        /// Question picked won't be the same as the previous 5 questions given.
        /// </summary>
        /// <returns>A <see cref="Question"/> object</returns>
        public Question GetRandomQuestion()
        {
            bool isNew = false;
            int index = 0;
            // ensure the next question is not the same one as the previous 5 questions
            while (!isNew)
            {
                index = random.Next(0, questions.Count);
                isNew = IsNewIndex(questionIndexBuffer, index);
            }

            questionIndexBuffer = UpdateIndexBuffer(questionIndexBuffer, index);
            return questions.ElementAt(index);
        }

        private static bool IsNewIndex(List<int> indexBuffer, int newIndex)
        {
            foreach (int index in indexBuffer)
            {
                if (index == newIndex) return false;
            }
            return true;
        }

        private static List<int> UpdateIndexBuffer(List<int> indexBuffer, int newIndex)
        {
            if (indexBuffer.Count > bufferCapacity) { indexBuffer.RemoveAt(0); }
            indexBuffer.Add(newIndex);
            return indexBuffer;
        }
    }
}
